using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using MilkTeaPOS.Models;
using Npgsql;

namespace MilkTeaPOS
{
    public partial class frmOrders : Form
    {
        private readonly List<Product> _allProducts = new();
        private readonly List<CartItem> _cartItems = new();
        private readonly List<Topping> _allToppings = new();
        private readonly Dictionary<Guid, List<SizeOption>> _productSizeCache = new();
        private readonly Dictionary<string, List<string>> _categoryImageFiles = new(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<string, Bitmap> _imageCache = new(StringComparer.OrdinalIgnoreCase);
        private readonly List<Category> _allCategories = new();
        private Guid? _selectedCategoryId; // Use ID for matching
        private Button? _selectedCategoryButton;
        private Bitmap? _placeholderImage;
        private Customer? _selectedCustomer;
        private Membership? _selectedMembership;

        private Panel? _popupOverlay;
        private Panel? _popupSheet;
        private VoucherSnapshot? _appliedVoucher;
        private decimal _voucherDiscount;
        private readonly string? _initialOrderSelection;
        private readonly string? _initialTableName;

        public frmOrders() : this(null, null)
        {
        }

        public frmOrders(string? initialOrderSelection, string? initialTableName)
        {
            InitializeComponent();
            _initialOrderSelection = initialOrderSelection;
            _initialTableName = initialTableName;
        }

        private async void frmOrders_Load(object sender, EventArgs e)
        {
            StartClock();
            lblInvoiceInfo.Text = $"HD-{DateTime.Now:yyMMddHHmm}";
            lbltrangthai.Text = "Đang tải";
            lblCustomerInfo.Text = "Khách lẻ";
            cbotrangthai.DropDownStyle = ComboBoxStyle.DropDownList;

            // Disable voucher until member is selected
            txtVoucher.Enabled = false;
            btnApdungvoucher.Enabled = false;

            // Set splitter ratio dynamically (75% products, 25% cart)
            // Use Width - Panel2MinSize to guarantee safe range
            int safeMax = this.ClientSize.Width - splitContainer.Panel2MinSize - 10;
            splitContainer.SplitterDistance = Math.Min((int)(this.ClientSize.Width * 0.75), safeMax);

            await LoadCategoriesAsync();
            await LoadOrderTypeOptionsAsync();
            await LoadProductsAsync();
            await LoadProductSizesAsync();
            await LoadToppingsAsync();
            RenderProducts();
            UpdateTotals();
        }

        private System.Windows.Forms.Timer? _clockTimer;

        private void StartClock()
        {
            _clockTimer = new System.Windows.Forms.Timer();
            _clockTimer.Interval = 1000;
            _clockTimer.Tick += (s, e) => lblClock.Text = DateTime.Now.ToString("HH:mm:ss");
            _clockTimer.Start();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _clockTimer?.Stop();
            _clockTimer?.Dispose();
            base.OnFormClosing(e);
        }

        private async Task LoadCategoriesAsync()
        {
            try
            {
                using var context = new PostgresContext();
                var categories = await context.Categories
                    .AsNoTracking()
                    .Where(c => c.IsActive != false)
                    .OrderBy(c => c.DisplayOrder)
                    .ThenBy(c => c.Name)
                    .ToListAsync();

                _allCategories.Clear();
                _allCategories.AddRange(categories);

                pnlCategoryButtons.SuspendLayout();
                pnlCategoryButtons.Controls.Clear();

                // "Tất cả" button
                var btnAll = CreateCategoryButton("🏷️ Tất cả", null, true);
                pnlCategoryButtons.Controls.Add(btnAll);
                _selectedCategoryButton = btnAll;

                // Dynamic category buttons
                foreach (var cat in categories)
                {
                    var emoji = GetCategoryEmoji(cat.Name);
                    var btn = CreateCategoryButton($"{emoji} {cat.Name}", cat.Id, false);
                    pnlCategoryButtons.Controls.Add(btn);
                }

                pnlCategoryButtons.ResumeLayout();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không tải được danh mục.\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Button CreateCategoryButton(string text, Guid? categoryId, bool isActive)
        {
            var font = new Font("Segoe UI", 9F, isActive ? FontStyle.Bold : FontStyle.Regular);
            var displayText = text.Replace("&", "&&"); // Escape & để hiển thị đúng
            var textSize = TextRenderer.MeasureText(displayText, font);
            var btnWidth = textSize.Width + 30; // padding

            var btn = new Button
            {
                AutoSize = false,
                Width = Math.Max(80, btnWidth),
                Height = 34,
                Margin = new Padding(0, 0, 6, 0),
                Text = displayText,
                FlatStyle = FlatStyle.Flat,
                Font = font,
                Cursor = Cursors.Hand,
                Tag = categoryId
            };

            if (isActive)
            {
                btn.BackColor = Color.FromArgb(255, 238, 238);
                btn.ForeColor = Color.FromArgb(220, 53, 69);
            }
            else
            {
                btn.BackColor = Color.FromArgb(247, 249, 252);
                btn.ForeColor = Color.FromArgb(45, 55, 72);
            }

            btn.FlatAppearance.BorderSize = 0;
            btn.Click += CategoryButton_Click;
            return btn;
        }

        private void CategoryButton_Click(object? sender, EventArgs e)
        {
            if (sender is not Button btn) return;

            // Reset all buttons to inactive
            foreach (Control c in pnlCategoryButtons.Controls)
            {
                if (c is Button b)
                {
                    b.BackColor = Color.FromArgb(247, 249, 252);
                    b.ForeColor = Color.FromArgb(45, 55, 72);
                    b.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
                }
            }

            // Set clicked button to active
            btn.BackColor = Color.FromArgb(255, 238, 238);
            btn.ForeColor = Color.FromArgb(220, 53, 69);
            btn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            _selectedCategoryButton = btn;

            _selectedCategoryId = btn.Tag as Guid?;
            RenderProducts();
        }

        private static string GetCategoryEmoji(string categoryName)
        {
            var normalized = NormalizeText(categoryName);

            if (normalized.Contains("ca phe") || normalized.Contains("coffee")) return "☕";
            if (normalized.Contains("tra sua") || normalized.Contains("milk tea")) return "🧋";
            if (normalized.Contains("tra trai cay") || normalized.Contains("fruit tea")) return "🍹";
            if (normalized.Contains("sua tuoi") || normalized.Contains("sua chua") || normalized.Contains("yogurt")) return "🥛";
            if (normalized.Contains("da xay") || normalized.Contains("frappe") || normalized.Contains("sinh to")) return "🧊";
            if (normalized.Contains("tra pure") || normalized.Contains("pure tea") || normalized.Contains("tra nguyen chat")) return "🍵";
            return "📦";
        }

        private void txtfind_TextChanged(object? sender, EventArgs e) => RenderProducts();

        private async System.Threading.Tasks.Task LoadProductSizesAsync()
        {
            try
            {
                using var context = new PostgresContext();
                var count = await context.ProductSizes.CountAsync();
                
                if (count == 0)
                {
                    // Try raw SQL to verify connection
                    var conn = context.Database.GetDbConnection();
                    lbltrangthai.Text = $"⚠️ product_sizes trống (product count: {_allProducts.Count})";
                    return;
                }

                lbltrangthai.Text = $"✅ Đã tải {count} size cho {_allProducts.Count} sản phẩm";

                var allSizes = await context.ProductSizes
                    .AsNoTracking()
                    .OrderBy(ps => ps.ProductId)
                    .ThenBy(ps => ps.Price)
                    .ToListAsync();

                var labels = new[] { "S", "M", "L" };
                
                // Group by product first
                var grouped = allSizes.GroupBy(ps => ps.ProductId);
                foreach (var group in grouped)
                {
                    var productId = group.Key;
                    var sorted = group.OrderBy(ps => ps.Price).ToList();
                    var sizes = new List<SizeOption>();
                    
                    for (int i = 0; i < sorted.Count && i < labels.Length; i++)
                    {
                        sizes.Add(new SizeOption { Label = labels[i], Price = sorted[i].Price });
                    }
                    
                    if (sizes.Count > 0)
                        _productSizeCache[productId] = sizes;
                }
                
                // Debug: log first product
                if (_allProducts.Count > 0)
                {
                    var firstProduct = _allProducts[0];
                    var cachedSizes = GetSizeOptions(firstProduct);
                    System.Diagnostics.Debug.WriteLine($"DEBUG: Product '{firstProduct.Name}' ({firstProduct.Id}) has {cachedSizes.Count} sizes: {string.Join(", ", cachedSizes.Select(s => $"{s.Label}={s.Price}"))}");
                    System.Diagnostics.Debug.WriteLine($"DEBUG: Total products in cache: {_productSizeCache.Count}");
                }
            }
            catch (Exception ex)
            {
                lbltrangthai.Text = $"⚠️ Lỗi tải size: {ex.Message}";
                System.Diagnostics.Debug.WriteLine($"ERROR LoadProductSizes: {ex}");
            }
        }

        private List<SizeOption> GetSizeOptions(Product product)
        {
            if (_productSizeCache.TryGetValue(product.Id, out var sizes) && sizes.Count > 0)
                return sizes;

            return new List<SizeOption> { new SizeOption { Label = "M", Price = product.BasePrice } };
        }

        private async System.Threading.Tasks.Task LoadToppingsAsync()
        {
            try
            {
                using var context = new PostgresContext();
                var toppings = await context.Toppings
                    .AsNoTracking()
                    .Where(t => t.IsAvailable != false)
                    .OrderBy(t => t.Name)
                    .ToListAsync();

                _allToppings.Clear();
                _allToppings.AddRange(toppings);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không tải được topping.\n{ex.Message}", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async Task LoadOrderTypeOptionsAsync()
        {
            var items = new List<string> { "Tại quán", "Mang về", "Giao hàng" };

            cbotrangthai.BeginUpdate();
            cbotrangthai.Items.Clear();
            foreach (var item in items)
            {
                cbotrangthai.Items.Add(item);
            }
            cbotrangthai.EndUpdate();

            // Default to "Tại quán"
            if (cbotrangthai.Items.Contains("Tại quán"))
            {
                cbotrangthai.SelectedItem = "Tại quán";
            }
            else if (cbotrangthai.Items.Count > 0)
            {
                cbotrangthai.SelectedIndex = 0;
            }

            // Load table names for cboBan
            await LoadTableOptionsAsync();
        }

        private async Task LoadTableOptionsAsync()
        {
            try
            {
                using var context = new PostgresContext();
                var tables = await context.Tables
                    .AsNoTracking()
                    .OrderBy(t => t.Name)
                    .ToListAsync();

                cboBan.BeginUpdate();
                cboBan.Items.Clear();
                foreach (var t in tables)
                {
                    cboBan.Items.Add(t.Name);
                }
                cboBan.EndUpdate();

                if (cboBan.Items.Count > 0)
                {
                    cboBan.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không tải được danh sách bàn.\n{ex.Message}", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void cbotrangthai_SelectedIndexChanged(object? sender, EventArgs e)
        {
            var isTableOrder = cbotrangthai.SelectedItem?.ToString() == "Tại quán";
            lblBanLabel.Visible = isTableOrder;
            cboBan.Visible = isTableOrder;

            // Shift other controls if table selector is hidden
            var offsetX = isTableOrder ? 0 : -(cboBan.Width + lblBanLabel.Width + btnTraBan.Width + 12);
            lblSearchIcon.Left = (isTableOrder ? 556 : 280);
            txtfind.Left = (isTableOrder ? 580 : 304);
            btnTimMember.Left = (isTableOrder ? 846 : 570);
            lblCustomerInfo.Left = (isTableOrder ? 962 : 686);

            // Check if selected table is occupied to show/hide "Trả bàn" button
            btnTraBan.Visible = false;
            if (isTableOrder && cboBan.SelectedIndex >= 0)
            {
                var tableName = cboBan.SelectedItem?.ToString();
                if (!string.IsNullOrWhiteSpace(tableName))
                {
                    try
                    {
                        using var context = new PostgresContext();
                        var table = await context.Tables
                            .AsNoTracking()
                            .FirstOrDefaultAsync(t => t.Name == tableName);

                        if (table != null && table.Status == "occupied")
                        {
                            btnTraBan.Visible = true;
                        }
                    }
                    catch { }
                }
            }
        }

        private async void cboBan_SelectedIndexChanged(object? sender, EventArgs e)
        {
            // Check if selected table is occupied to show/hide "Trả bàn" button
            btnTraBan.Visible = false;
            if (cboBan.SelectedIndex >= 0)
            {
                var tableName = cboBan.SelectedItem?.ToString();
                if (!string.IsNullOrWhiteSpace(tableName))
                {
                    try
                    {
                        using var context = new PostgresContext();
                        var table = await context.Tables
                            .AsNoTracking()
                            .FirstOrDefaultAsync(t => t.Name == tableName);

                        if (table != null && table.Status == "occupied")
                        {
                            btnTraBan.Visible = true;
                        }
                    }
                    catch { }
                }
            }
        }

        private async void btnTraBan_Click(object? sender, EventArgs e)
        {
            if (cboBan.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn bàn cần trả.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var tableName = cboBan.SelectedItem?.ToString();
            var result = MessageBox.Show($"Xác nhận trả bàn {tableName}?\nBàn sẽ trở về trạng thái trống.", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            try
            {
                using var context = new PostgresContext();
                var table = await context.Tables
                    .AsNoTracking()
                    .FirstOrDefaultAsync(t => t.Name == tableName);

                if (table == null)
                {
                    MessageBox.Show("Không tìm thấy bàn trong hệ thống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Use raw SQL with explicit enum cast to avoid EF Core mapping error
                // 1. Cancel any pending/preparing/ready orders for this table
                await context.Database.ExecuteSqlInterpolatedAsync($@"
UPDATE orders
SET status = 'cancelled'::order_status,
    updated_at = {DateTime.UtcNow}
WHERE table_id = {table.Id}
  AND status IN ('pending', 'preparing', 'ready');
");

                // 2. Free the table
                await context.Database.ExecuteSqlInterpolatedAsync($@"
UPDATE tables
SET status = 'available'::table_status,
    updated_at = {DateTime.UtcNow}
WHERE id = {table.Id};
");

                MessageBox.Show($"Đã trả bàn {tableName}.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Audit log
                try
                {
                    var userId = PostgresContext.CurrentUserId;
                    if (userId.HasValue)
                    {
                        using var auditContext = new PostgresContext();
                        auditContext.AuditLogs.Add(new AuditLog
                        {
                            Id = Guid.NewGuid(),
                            UserId = userId.Value,
                            Action = "free_table",
                            TableName = "tables",
                            RecordId = table.Id,
                            NewValues = $"{{\"details\": \"Freed table: {tableName}\"}}",
                            IpAddress = PostgresContext.CurrentUserIP,
                            CreatedAt = DateTime.UtcNow
                        });
                        await auditContext.SaveChangesAsync();
                    }
                }
                catch { }

                // Refresh button visibility (table is now available, hide the button)
                btnTraBan.Visible = false;
                cboBan_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể trả bàn.\n{ex.InnerException?.Message ?? ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static bool IsDeliverySelection(string? selection)
        {
            return string.Equals(selection, "Giao hàng", StringComparison.OrdinalIgnoreCase)
                || string.Equals(selection, "Mang về", StringComparison.OrdinalIgnoreCase);
        }

        private static async Task<Guid?> ResolveSelectedTableIdAsync(PostgresContext context, string? tableName)
        {
            if (string.IsNullOrWhiteSpace(tableName))
            {
                return null;
            }

            return await context.Tables
                .AsNoTracking()
                .Where(t => t.Name == tableName)
                .Select(t => (Guid?)t.Id)
                .FirstOrDefaultAsync();
        }

        private async System.Threading.Tasks.Task LoadProductsAsync()
        {
            try
            {
                using var context = new PostgresContext();
                var products = await context.Products
                    .AsNoTracking()
                    .Include(p => p.Category)
                    .Where(p => p.IsAvailable != false)
                    .OrderBy(p => p.Name)
                    .ToListAsync();

                _allProducts.Clear();
                _allProducts.AddRange(products);
                lbltrangthai.Text = $"{_allProducts.Count} món";
            }
            catch (Exception ex)
            {
                lbltrangthai.Text = "Lỗi tải";
                MessageBox.Show($"Không tải được sản phẩm.\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RenderProducts()
        {
            pnlContent.SuspendLayout();
            pnlContent.Controls.Clear();

            var filteredProducts = _allProducts
                .Where(MatchesSearch)
                .Where(MatchesCategory)
                .ToList();

            foreach (var product in filteredProducts)
            {
                pnlContent.Controls.Add(CreateProductCard(product));
            }

            if (filteredProducts.Count == 0)
            {
                pnlContent.Controls.Add(new Label
                {
                    Width = pnlContent.Width - 40,
                    Height = 60,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 11F),
                    ForeColor = Color.DimGray,
                    Text = "Không có sản phẩm phù hợp"
                });
            }

            pnlContent.ResumeLayout();
        }

        private bool MatchesSearch(Product product)
        {
            var keyword = NormalizeText(txtfind.Text.Trim().ToLowerInvariant());
            if (string.IsNullOrWhiteSpace(keyword)) return true;

            var productName = NormalizeText(product.Name).ToLowerInvariant();
            var categoryName = NormalizeText(product.Category?.Name).ToLowerInvariant();

            return productName.Contains(keyword) || categoryName.Contains(keyword);
        }

        private bool MatchesCategory(Product product)
        {
            if (!_selectedCategoryId.HasValue) return true;
            return product.CategoryId == _selectedCategoryId.Value;
        }

        private Panel CreateProductCard(Product product)
        {
            var card = new Panel
            {
                Width = 170,
                Height = 140,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Margin = new Padding(8)
            };

            var image = new PictureBox
            {
                Left = 8,
                Top = 8,
                Width = 152,
                Height = 56,
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = LoadProductImage(product)
            };

            var name = new Label
            {
                Left = 8,
                Top = 68,
                Width = 152,
                Height = 36,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Text = product.Name
            };

            var price = new Label
            {
                Left = 8,
                Top = 106,
                Width = 152,
                Height = 22,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(220, 53, 69),
                Text = $"{product.BasePrice:N0}đ"
            };

            card.Controls.Add(image);
            card.Controls.Add(name);
            card.Controls.Add(price);

            card.Cursor = Cursors.Hand;
            image.Cursor = Cursors.Hand;
            name.Cursor = Cursors.Hand;
            price.Cursor = Cursors.Hand;

            card.Click += async (_, _) => await OpenCustomizePopupAsync(product);
            image.Click += async (_, _) => await OpenCustomizePopupAsync(product);
            name.Click += async (_, _) => await OpenCustomizePopupAsync(product);
            price.Click += async (_, _) => await OpenCustomizePopupAsync(product);

            return card;
        }

        private async System.Threading.Tasks.Task OpenCustomizePopupAsync(Product product)
        {
            CloseCustomizePopup();

            var selectedSizeIdx = 0;
            var selectedSugar = "100";
            var selectedIce = "100";
            var selectedToppings = new HashSet<Guid>();

            // Create popup overlay
            _popupOverlay = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(80, 0, 0, 0)
            };
            _popupOverlay.Click += (_, _) => CloseCustomizePopup();

            // Main popup sheet - shifted up by 80px from center
            var popupWidth = 520;
            var popupHeight = 580;
            _popupSheet = new Panel
            {
                Width = popupWidth,
                Height = popupHeight,
                Left = (this.ClientSize.Width - popupWidth) / 2,
                Top = (this.ClientSize.Height - popupHeight) / 2 - 80,
                BackColor = Color.FromArgb(252, 252, 253),
                BorderStyle = BorderStyle.None
            };
            SetTopRoundedRegion(_popupSheet, 20);
            _popupSheet.Click += (_, _) => { };

            // === HEADER ===
            var pnlHeader = new Panel
            {
                Left = 0,
                Top = 0,
                Width = popupWidth,
                Height = 72,
                BackColor = Color.FromArgb(45, 55, 72)
            };

            var lblProductName = new Label
            {
                Left = 20,
                Top = 14,
                Width = popupWidth - 40,
                Height = 24,
                Text = product.Name,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.White
            };

            var lblBasePrice = new Label
            {
                Left = 20,
                Top = 40,
                Width = popupWidth - 40,
                Height = 20,
                Text = $"{product.BasePrice:N0}đ",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(255, 193, 7)
            };

            var btnClose = new Button
            {
                Left = popupWidth - 44,
                Top = 16,
                Width = 30,
                Height = 30,
                Text = "✕",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.FromArgb(180, 190, 200),
                BackColor = Color.Transparent
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (_, _) => CloseCustomizePopup();
            btnClose.MouseEnter += (_, _) => { btnClose.ForeColor = Color.White; };
            btnClose.MouseLeave += (_, _) => { btnClose.ForeColor = Color.FromArgb(180, 190, 200); };

            pnlHeader.Controls.Add(lblProductName);
            pnlHeader.Controls.Add(lblBasePrice);
            pnlHeader.Controls.Add(btnClose);
            _popupSheet.Controls.Add(pnlHeader);

            // === SCROLLABLE CONTENT AREA ===
            var contentPanel = new Panel
            {
                Left = 0,
                Top = 80,
                Width = popupWidth,
                Height = popupHeight - 158,
                AutoScroll = true,
                BackColor = Color.FromArgb(252, 252, 253)
            };

            // === BOTTOM BAR (buttons) ===
            var pnlBottom = new Panel
            {
                Left = 0,
                Top = popupHeight - 78,
                Width = popupWidth,
                Height = 78,
                BackColor = Color.White
            };

            var sepLine = new Panel
            {
                Left = 0,
                Top = 0,
                Width = popupWidth,
                Height = 1,
                BackColor = Color.FromArgb(230, 236, 242)
            };

            var btnCancel = new Button
            {
                Left = 16,
                Top = 20,
                Width = 100,
                Height = 40,
                Text = "Hủy",
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(108, 117, 125)
            };
            btnCancel.FlatAppearance.BorderSize = 1;
            btnCancel.FlatAppearance.BorderColor = Color.Silver;
            SetRoundedRegion(btnCancel, 8);

            var btnAdd = new Button
            {
                Left = 128,
                Top = 16,
                Width = popupWidth - 144,
                Height = 48,
                Text = "✓ Thêm vào đơn",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(72, 187, 120),
                ForeColor = Color.White
            };
            btnAdd.FlatAppearance.BorderSize = 0;
            SetRoundedRegion(btnAdd, 10);

            btnAdd.Click += (_, _) =>
            {
                var selectedToppingModels = _allToppings
                    .Where(t => selectedToppings.Contains(t.Id))
                    .Select(t => new CartTopping { ToppingId = t.Id, Name = t.Name, Price = t.Price })
                    .ToList();

                decimal unitPrice = product.BasePrice;
                string sizeLabel = "M";
                var flSizeCtrl = contentPanel.Controls["flSize"] as FlowLayoutPanel;
                if (flSizeCtrl != null && flSizeCtrl.Tag is List<SizeOption> sizes && selectedSizeIdx >= 0 && selectedSizeIdx < sizes.Count)
                {
                    unitPrice = sizes[selectedSizeIdx].Price;
                    sizeLabel = sizes[selectedSizeIdx].Label;
                }

                AddConfiguredProductToCart(product, new SizeOption { Label = sizeLabel, Price = unitPrice }, selectedSugar, selectedIce, selectedToppingModels);
                CloseCustomizePopup();
            };

            btnCancel.Click += (_, _) => CloseCustomizePopup();

            pnlBottom.Controls.Add(sepLine);
            pnlBottom.Controls.Add(btnCancel);
            pnlBottom.Controls.Add(btnAdd);
            _popupSheet.Controls.Add(pnlBottom);

            // === SECTION: Size ===
            int y = 16;
            contentPanel.Controls.Add(CreateSectionLabel("Kích cỡ", 16, y));
            y += 30;

            var flSize = new FlowLayoutPanel
            {
                Name = "flSize",
                Left = 16,
                Top = y,
                Width = popupWidth - 32,
                Height = 48,
                WrapContents = true,
                FlowDirection = FlowDirection.LeftToRight,
                BackColor = Color.Transparent,
                Padding = new Padding(0)
            };
            contentPanel.Controls.Add(flSize);
            y += 56;

            // === SECTION: Sugar ===
            contentPanel.Controls.Add(CreateSectionLabel("Đường", 16, y));
            y += 30;

            var flSugar = new FlowLayoutPanel
            {
                Left = 16,
                Top = y,
                Width = popupWidth - 32,
                Height = 48,
                WrapContents = true,
                FlowDirection = FlowDirection.LeftToRight,
                BackColor = Color.Transparent,
                Padding = new Padding(0)
            };
            contentPanel.Controls.Add(flSugar);
            y += 56;

            // === SECTION: Ice ===
            contentPanel.Controls.Add(CreateSectionLabel("Đá", 16, y));
            y += 30;

            var flIce = new FlowLayoutPanel
            {
                Left = 16,
                Top = y,
                Width = popupWidth - 32,
                Height = 48,
                WrapContents = true,
                FlowDirection = FlowDirection.LeftToRight,
                BackColor = Color.Transparent,
                Padding = new Padding(0)
            };
            contentPanel.Controls.Add(flIce);
            y += 56;

            // === SECTION: Toppings ===
            contentPanel.Controls.Add(CreateSectionLabel("Topping thêm", 16, y));
            y += 30;

            var flTop = new FlowLayoutPanel
            {
                Left = 16,
                Top = y,
                Width = popupWidth - 32,
                Height = 120,
                WrapContents = true,
                FlowDirection = FlowDirection.LeftToRight,
                AutoScroll = true,
                BackColor = Color.Transparent,
                Padding = new Padding(0)
            };
            contentPanel.Controls.Add(flTop);

            // Set scrollable area height (dynamic based on toppings)
            // We'll set this after controls are populated, so use a variable

            _popupSheet.Controls.Add(contentPanel);

            _popupOverlay.Controls.Add(_popupSheet);
            this.Controls.Add(_popupOverlay);
            _popupOverlay.BringToFront();

            // Get size options
            var sizeOptions = GetSizeOptions(product);
            if (sizeOptions.Count == 0)
            {
                sizeOptions = new List<SizeOption> { new SizeOption { Label = "M", Price = product.BasePrice } };
            }

            // === Build Size buttons ===
            void SelectSingle(FlowLayoutPanel panel, Button selected)
            {
                foreach (Control c in panel.Controls)
                {
                    if (c is Button b) SetChipStyle(b, b == selected);
                }
            }

            Button? firstSizeBtn = null;
            foreach (var s in sizeOptions)
            {
                var priceStr = s.Price.ToString("N0") + "đ";
                var b = CreateChipButton($"{s.Label}  •  {priceStr}");
                if (firstSizeBtn == null) { SetChipStyle(b, true); firstSizeBtn = b; }
                var idx = sizeOptions.IndexOf(s);
                b.Click += (_, _) => { selectedSizeIdx = idx; SelectSingle(flSize, b); UpdatePriceDisplay(); };
                flSize.Controls.Add(b);
            }
            flSize.Tag = sizeOptions;

            // Update price display function
            void UpdatePriceDisplay()
            {
                var currentSizePrice = sizeOptions[selectedSizeIdx].Price;
                var toppingTotal = _allToppings.Where(t => selectedToppings.Contains(t.Id)).Sum(t => t.Price);
                var totalPrice = currentSizePrice + toppingTotal;
                lblBasePrice.Text = $"{totalPrice:N0}đ";
                if (toppingTotal > 0)
                {
                    lblBasePrice.Text += $" (+{toppingTotal:N0}đ topping)";
                }
            }

            // === Build Sugar buttons ===
            var sugars = new[] { "0", "25", "50", "75", "100" };
            Button? firstSugarBtn = null;
            foreach (var sugar in sugars)
            {
                var b = CreateChipButton($"{sugar}%");
                if (sugar == selectedSugar) { SetChipStyle(b, true); firstSugarBtn = b; }
                if (firstSugarBtn == null) { firstSugarBtn = b; }
                var s = sugar;
                b.Click += (_, _) => { selectedSugar = s; SelectSingle(flSugar, b); };
                flSugar.Controls.Add(b);
            }

            // === Build Ice buttons ===
            var iceOptions = new[] { ("0", "Không đá"), ("25", "Ít đá"), ("50", "Bình thường"), ("75", "Nhiều đá"), ("100", "Đá full") };
            Button? firstIceBtn = null;
            foreach (var ice in iceOptions)
            {
                var b = CreateChipButton(ice.Item2);
                if (ice.Item1 == selectedIce) { SetChipStyle(b, true); firstIceBtn = b; }
                if (firstIceBtn == null) { firstIceBtn = b; }
                var ic = ice;
                b.Click += (_, _) => { selectedIce = ic.Item1; SelectSingle(flIce, b); };
                flIce.Controls.Add(b);
            }

            // === Build Topping buttons ===
            foreach (var topping in _allToppings)
            {
                var b = CreateChipButton($"{topping.Name}  +{topping.Price:N0}đ");
                var topId = topping.Id;
                b.Click += (_, _) =>
                {
                    if (selectedToppings.Contains(topId))
                    {
                        selectedToppings.Remove(topId);
                        SetChipStyle(b, false);
                    }
                    else
                    {
                        selectedToppings.Add(topId);
                        SetChipStyle(b, true);
                    }
                    UpdatePriceDisplay();
                };
                flTop.Controls.Add(b);
            }

            // Setup vertical scroll: calculate total content height
            contentPanel.PerformLayout();
            int totalContentHeight = y + flTop.Height + 16; // extra padding at bottom
            contentPanel.AutoScrollMinSize = new Size(0, totalContentHeight);

            _popupOverlay.BringToFront();
            _popupSheet.BringToFront();
        }

        private static Label CreateSectionLabel(string text, int x, int y)
        {
            return new Label
            {
                Left = x,
                Top = y,
                Width = 200,
                Height = 22,
                Text = text,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(45, 55, 72)
            };
        }

        private void CloseCustomizePopup()
        {
            if (_popupOverlay != null)
            {
                this.Controls.Remove(_popupOverlay);
                _popupOverlay.Dispose();
                _popupOverlay = null;
                _popupSheet = null;
            }
        }

        private static Button CreateChipButton(string text)
        {
            var button = new Button
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Padding = new Padding(14, 6, 14, 6),
                Height = 36,
                Text = text,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(242, 242, 238),
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(0, 0, 8, 8),
                MinimumSize = new Size(0, 36)
            };

            button.FlatAppearance.BorderSize = 1;
            button.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 200);
            SetRoundedRegion(button, 8);
            return button;
        }

        private static void SetChipStyle(Button button, bool selected)
        {
            button.BackColor = selected ? Color.FromArgb(255, 249, 249) : Color.FromArgb(242, 242, 238);
            button.ForeColor = selected ? Color.FromArgb(220, 53, 69) : Color.Black;
            button.FlatAppearance.BorderColor = selected ? Color.FromArgb(220, 53, 69) : Color.Silver;
        }

        private static void SetTopRoundedRegion(Control control, int radius)
        {
            if (control.Width <= 0 || control.Height <= 0) return;

            var path = new GraphicsPath();
            var diameter = radius * 2;
            var rect = new Rectangle(0, 0, control.Width - 1, control.Height - 1);

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddLine(rect.Right, rect.Y + radius, rect.Right, rect.Bottom);
            path.AddLine(rect.Right, rect.Bottom, rect.X, rect.Bottom);
            path.AddLine(rect.X, rect.Bottom, rect.X, rect.Y + radius);
            path.CloseFigure();

            control.Region = new Region(path);
            control.Resize += (_, _) => SetTopRoundedRegion(control, radius);
        }

        private static void SetRoundedRegion(Control control, int radius)
        {
            if (control.Width <= 0 || control.Height <= 0) return;

            var path = new GraphicsPath();
            var diameter = radius * 2;
            var rect = new Rectangle(0, 0, control.Width - 1, control.Height - 1);

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            control.Region = new Region(path);
            control.Resize += (_, _) => SetRoundedRegion(control, radius);
        }

        private void AddConfiguredProductToCart(Product product, SizeOption? selectedSize, string sugar, string ice, List<CartTopping> toppings)
        {
            var sizeLabel = selectedSize?.Label ?? "M";
            var unitPrice = selectedSize?.Price ?? product.BasePrice;

            var existing = _cartItems.FirstOrDefault(x =>
                x.ProductId == product.Id &&
                x.Size == sizeLabel &&
                x.Sugar == sugar &&
                x.Ice == ice &&
                x.Toppings.Select(t => t.ToppingId).OrderBy(id => id).SequenceEqual(toppings.Select(t => t.ToppingId).OrderBy(id => id)));

            if (existing != null)
            {
                existing.Quantity++;
            }
            else
            {
                _cartItems.Add(new CartItem
                {
                    ItemId = Guid.NewGuid(),
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Size = sizeLabel,
                    Sugar = sugar,
                    Ice = ice,
                    UnitPrice = unitPrice,
                    Toppings = toppings,
                    Quantity = 1
                });
            }

            RenderCart();
            UpdateTotals();
        }

        private void RenderCart()
        {
            pnlGiohang.SuspendLayout();
            pnlGiohang.Controls.Clear();

            int yOffset = 8;
            var sortedItems = _cartItems.OrderBy(i => i.ProductName).ToList();
            var rowWidth = pnlGiohang.ClientSize.Width;

            foreach (var item in sortedItems)
            {
                var row = CreateCartRow(item, rowWidth);
                row.Top = yOffset;
                row.Left = 0;
                pnlGiohang.Controls.Add(row);
                yOffset += row.Height + 8;
            }

            pnlGiohang.ResumeLayout();
        }

        private Control CreateCartRow(CartItem item, int rowWidth)
        {
            var row = new Panel
            {
                AutoSize = false,
                Width = rowWidth,
                Height = 110,
                BorderStyle = BorderStyle.None,
                Margin = new Padding(0, 0, 0, 8),
                BackColor = Color.White
            };

            // Left accent bar
            var accentBar = new Panel
            {
                Left = 0,
                Top = 0,
                Width = 5,
                Height = 110,
                BackColor = Color.FromArgb(23, 162, 184)
            };
            row.Controls.Add(accentBar);

            // Product thumbnail
            var thumbSize = 72;
            var pnlThumb = new Panel
            {
                Left = 10,
                Top = 6,
                Width = thumbSize,
                Height = thumbSize,
                BackColor = Color.FromArgb(247, 249, 252),
                BorderStyle = BorderStyle.None
            };
            var thumb = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = GetThumbImageForCart(item)
            };
            pnlThumb.Controls.Add(thumb);
            row.Controls.Add(pnlThumb);

            // Product name - top right of thumbnail
            var lblName = new Label
            {
                Left = 90,
                Top = 6,
                Width = Math.Max(50, rowWidth - 130),
                Height = 20,
                AutoEllipsis = true,
                Text = item.ProductName,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(45, 55, 72)
            };
            row.Controls.Add(lblName);

            // Remove button - top right
            var btnRemove = new Button
            {
                Left = rowWidth - 28,
                Top = 4,
                Width = 24,
                Height = 24,
                Text = "✕",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.FromArgb(180, 190, 200),
                BackColor = Color.Transparent
            };
            btnRemove.FlatAppearance.BorderSize = 0;
            btnRemove.Click += (_, _) => RemoveItem(item.ItemId);
            btnRemove.MouseEnter += (_, _) => { btnRemove.ForeColor = Color.FromArgb(220, 53, 69); };
            btnRemove.MouseLeave += (_, _) => { btnRemove.ForeColor = Color.FromArgb(180, 190, 200); };
            row.Controls.Add(btnRemove);

            // Option tags - below name, right of thumbnail
            var tagsX = 90;
            var tagGap = 5;
            var tagsY = 28;

            // Size tag (larger)
            var pnlSizeTag = CreateTag(item.Size, Color.FromArgb(255, 193, 7), Color.White, 8.5F);
            pnlSizeTag.Left = tagsX;
            pnlSizeTag.Top = tagsY;
            row.Controls.Add(pnlSizeTag);
            tagsX += pnlSizeTag.Width + tagGap;

            // Sugar tag
            var pnlSugarTag = CreateTag($"Đ {item.Sugar}%", Color.FromArgb(72, 187, 120), Color.White, 8.5F);
            pnlSugarTag.Left = tagsX;
            pnlSugarTag.Top = tagsY;
            row.Controls.Add(pnlSugarTag);
            tagsX += pnlSugarTag.Width + tagGap;

            // Ice tag
            var pnlIceTag = CreateTag($"Đá {item.Ice}%", Color.FromArgb(23, 162, 184), Color.White, 8.5F);
            pnlIceTag.Left = tagsX;
            pnlIceTag.Top = tagsY;
            row.Controls.Add(pnlIceTag);
            tagsX += pnlIceTag.Width + tagGap;

            // Toppings panel (FlowLayoutPanel for wrapping)
            int toppingsHeight = 0;
            if (item.Toppings.Count > 0)
            {
                var toppingsWidth = Math.Max(80, rowWidth - 120);
                var flow = new FlowLayoutPanel
                {
                    Left = 90,
                    Top = 50,
                    Width = toppingsWidth,
                    AutoSize = true,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink,
                    FlowDirection = FlowDirection.LeftToRight,
                    WrapContents = true,
                    BackColor = Color.Transparent,
                    Padding = new Padding(0)
                };

                foreach (var topping in item.Toppings)
                {
                    var toppingTag = CreateTag($"+ {topping.Name}", Color.FromArgb(108, 117, 125), Color.White, 7.5F);
                    flow.Controls.Add(toppingTag);
                    var spacer = new Panel { Width = 4, Height = 1, Margin = new Padding(0) };
                    flow.Controls.Add(spacer);
                }

                toppingsHeight = flow.Height;
                row.Controls.Add(flow);
            }

            // Separator line
            var sepTop = 74 + toppingsHeight;
            var sepLine = new Panel
            {
                Left = 6,
                Top = sepTop,
                Width = rowWidth - 12,
                Height = 1,
                BackColor = Color.FromArgb(230, 236, 242)
            };
            row.Controls.Add(sepLine);

            // Note button (📝) - next to remove button
            var btnNote = new Button
            {
                Left = rowWidth - 56,
                Top = 4,
                Width = 24,
                Height = 22,
                Text = "📝",
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.FromArgb(108, 117, 125),
                BackColor = Color.Transparent
            };
            btnNote.FlatAppearance.BorderSize = 0;
            btnNote.MouseEnter += (_, _) => { btnNote.ForeColor = Color.FromArgb(255, 193, 7); };
            btnNote.MouseLeave += (_, _) => { btnNote.ForeColor = Color.FromArgb(108, 117, 125); };

            // Note display label (shows if note exists)
            Label? lblNote = null;
            if (!string.IsNullOrWhiteSpace(item.Note))
            {
                lblNote = new Label
                {
                    Left = 90,
                    Top = sepTop - 20,
                    Width = rowWidth - 120,
                    Height = 16,
                    AutoEllipsis = true,
                    Text = $"📝 {item.Note}",
                    Font = new Font("Segoe UI", 7.5F, FontStyle.Italic),
                    ForeColor = Color.FromArgb(255, 193, 7)
                };
                row.Controls.Add(lblNote);
            }

            btnNote.Click += (_, _) =>
            {
                using var noteDlg = new Form
                {
                    Text = "Ghi chú cho món",
                    StartPosition = FormStartPosition.CenterParent,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    MaximizeBox = false,
                    MinimizeBox = false,
                    Size = new Size(320, 140),
                    AcceptButton = null,
                    CancelButton = null
                };

                var txtNote = new TextBox
                {
                    Left = 16,
                    Top = 14,
                    Width = noteDlg.Width - 32,
                    Height = 24,
                    Text = item.Note,
                    Font = new Font("Segoe UI", 9F),
                    PlaceholderText = "VD: ít đá, thêm đường, không trân châu..."
                };

                var btnOk = new Button
                {
                    Left = noteDlg.Width - 170,
                    Top = 50,
                    Width = 70,
                    Height = 30,
                    Text = "OK",
                    DialogResult = DialogResult.OK,
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.FromArgb(72, 187, 120),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold)
                };
                btnOk.FlatAppearance.BorderSize = 0;

                var btnClear = new Button
                {
                    Left = noteDlg.Width - 90,
                    Top = 50,
                    Width = 70,
                    Height = 30,
                    Text = "Xóa",
                    DialogResult = DialogResult.Abort,
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.FromArgb(220, 53, 69),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold)
                };
                btnClear.FlatAppearance.BorderSize = 0;

                noteDlg.Controls.Add(txtNote);
                noteDlg.Controls.Add(btnOk);
                noteDlg.Controls.Add(btnClear);

                var noteResult = noteDlg.ShowDialog(this);
                if (noteResult == DialogResult.OK)
                {
                    item.Note = txtNote.Text.Trim();
                    RenderCart();
                }
                else if (noteResult == DialogResult.Abort)
                {
                    item.Note = string.Empty;
                    RenderCart();
                }
            };
            row.Controls.Add(btnNote);

            // Quantity controls - below separator (dynamic position)
            var qtyTop = sepTop + 6;
            var btnMinus = new Button
            {
                Left = 12,
                Top = qtyTop,
                Width = 30,
                Height = 28,
                Text = "−",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(240, 243, 248),
                ForeColor = Color.FromArgb(45, 55, 72)
            };

            var lblQty = new Label
            {
                Left = 44,
                Top = qtyTop,
                Width = 30,
                Height = 28,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Text = item.Quantity.ToString(),
                ForeColor = Color.FromArgb(45, 55, 72)
            };

            var btnPlus = new Button
            {
                Left = 76,
                Top = qtyTop,
                Width = 30,
                Height = 28,
                Text = "+",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(240, 243, 248),
                ForeColor = Color.FromArgb(45, 55, 72)
            };

            btnMinus.FlatAppearance.BorderSize = 0;
            btnPlus.FlatAppearance.BorderSize = 0;

            btnMinus.Click += (_, _) => { btnMinus.BackColor = Color.FromArgb(220, 226, 235); };
            btnMinus.MouseUp += (_, _) => { btnMinus.BackColor = Color.FromArgb(240, 243, 248); };
            btnPlus.Click += (_, _) => { btnPlus.BackColor = Color.FromArgb(220, 226, 235); };
            btnPlus.MouseUp += (_, _) => { btnPlus.BackColor = Color.FromArgb(240, 243, 248); };

            btnMinus.Click += (_, _) => ChangeItemQuantity(item.ItemId, -1);
            btnPlus.Click += (_, _) => ChangeItemQuantity(item.ItemId, 1);

            row.Controls.Add(btnMinus);
            row.Controls.Add(lblQty);
            row.Controls.Add(btnPlus);

            // Unit price label
            var lblUnitPrice = new Label
            {
                Left = 112,
                Top = qtyTop + 2,
                Width = 100,
                Height = 24,
                AutoEllipsis = true,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 8F),
                ForeColor = Color.Gray,
                Text = FormatCurrency(item.UnitPrice) + " /sp"
            };
            row.Controls.Add(lblUnitPrice);

            // Line total badge
            var totalTop = qtyTop + 2;
            var pnlTotal = new Panel
            {
                Left = rowWidth - 100,
                Top = totalTop,
                Width = 94,
                Height = 30,
                BackColor = Color.FromArgb(220, 53, 69)
            };
            var lblLineTotal = new Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                Text = FormatCurrency(item.LineTotal)
            };
            pnlTotal.Controls.Add(lblLineTotal);
            row.Controls.Add(pnlTotal);

            // Dynamic row height based on toppings
            var rowHeight = Math.Max(110, sepTop + 66);
            row.Height = rowHeight;
            accentBar.Height = rowHeight;

            // Width sync on resize
            int lastWidth = rowWidth;
            row.Resize += (_, _) =>
            {
                if (row.Width != lastWidth)
                {
                    lastWidth = row.Width;
                    lblName.Width = Math.Max(50, row.Width - 130);
                    pnlTotal.Left = row.Width - 100;
                    btnRemove.Left = row.Width - 28;
                    btnNote.Left = row.Width - 56;
                    sepLine.Width = row.Width - 12;
                    if (lblNote != null) lblNote.Width = row.Width - 120;
                }
            };

            return row;
        }

        private Panel CreateTag(string text, Color bgColor, Color fgColor, float fontSize = 7F)
        {
            using var g = Graphics.FromHwnd(pnlGiohang.Handle);
            var textSize = g.MeasureString(text, new Font("Segoe UI", fontSize, FontStyle.Bold));
            var w = (int)Math.Max(32, textSize.Width + 16);
            var h = (int)(fontSize * 2.4);

            var pnl = new Panel
            {
                Size = new Size(w, h),
                BackColor = bgColor
            };

            var lbl = new Label
            {
                Dock = DockStyle.Fill,
                Text = text,
                Font = new Font("Segoe UI", fontSize, FontStyle.Bold),
                ForeColor = fgColor,
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnl.Controls.Add(lbl);

            return pnl;
        }

        private Bitmap GetThumbImageForCart(CartItem item)
        {
            var prod = _allProducts.FirstOrDefault(p => p.Id == item.ProductId);
            if (prod != null)
            {
                return LoadProductImage(prod);
            }
            return CreatePlaceholderImage();
        }

        private void ChangeItemQuantity(Guid itemId, int delta)
        {
            var item = _cartItems.FirstOrDefault(x => x.ItemId == itemId);
            if (item == null) return;

            item.Quantity += delta;
            if (item.Quantity <= 0)
            {
                _cartItems.Remove(item);
            }

            RenderCart();
            UpdateTotals();
        }

        private void RemoveItem(Guid itemId)
        {
            var item = _cartItems.FirstOrDefault(x => x.ItemId == itemId);
            if (item != null)
            {
                _cartItems.Remove(item);
                RenderCart();
                UpdateTotals();
            }
        }

        private void UpdateTotals()
        {
            var totals = CalculateCurrentTotals();
            _voucherDiscount = totals.VoucherDiscount;

            lblTamtinh.Text = FormatCurrency(totals.Subtotal);
            lblGiamGia.Text = $"-{FormatCurrency(totals.Discount + totals.VoucherDiscount)}";
            lblDiemTichLuy.Text = _selectedMembership?.Points?.ToString("N0") ?? "0";
            lblTongThanhToan.Text = FormatCurrency(totals.Total);
        }

        private (decimal Subtotal, decimal Discount, decimal PointsDiscount, decimal VoucherDiscount, decimal Total) CalculateCurrentTotals()
        {
            var subtotal = _cartItems.Sum(x => x.LineTotal);
            var discount = 0m;
            var pointsDiscount = 0m;
            var voucherDiscount = CalculateVoucherDiscount(subtotal, _appliedVoucher);
            var total = Math.Max(0m, subtotal - discount - voucherDiscount - pointsDiscount);

            return (subtotal, discount, pointsDiscount, voucherDiscount, total);
        }

        private static string FormatCurrency(decimal value) => $"{value:N0}đ";

        private Bitmap LoadProductImage(Product product)
        {
            try
            {
                var categoryKey = GetCategoryKey(product.Category?.Name);
                var files = GetCategoryImageFiles(categoryKey);
                if (files.Count > 0)
                {
                    var index = Math.Abs(product.Id.GetHashCode()) % files.Count;
                    return LoadImageFromFile(files[index]);
                }

                if (!string.IsNullOrWhiteSpace(product.ImageUrl))
                {
                    var normalizedPath = product.ImageUrl.Trim().TrimStart('/', '\\');
                    var projectPath = GetProjectPath();
                    var fullPath = Path.IsPathRooted(normalizedPath)
                        ? normalizedPath
                        : Path.Combine(projectPath, normalizedPath);

                    if (File.Exists(fullPath))
                    {
                        return LoadImageFromFile(fullPath);
                    }
                }
            }
            catch
            {
            }

            _placeholderImage ??= CreatePlaceholderImage();
            return _placeholderImage;
        }

        private Bitmap LoadImageFromFile(string fullPath)
        {
            if (_imageCache.TryGetValue(fullPath, out var cached))
            {
                return cached;
            }

            using var stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
            using var image = Image.FromStream(stream);
            var bmp = new Bitmap(image);
            _imageCache[fullPath] = bmp;
            return bmp;
        }

        private List<string> GetCategoryImageFiles(string categoryKey)
        {
            if (_categoryImageFiles.TryGetValue(categoryKey, out var existing))
            {
                return existing;
            }

            var results = new List<string>();
            var root = Path.Combine(GetProjectPath(), "images", "categories");
            var categoryFolder = Path.Combine(root, categoryKey);

            if (Directory.Exists(categoryFolder))
            {
                results = Directory.GetFiles(categoryFolder)
                    .Where(IsImageFile)
                    .OrderBy(x => x)
                    .ToList();
            }
            else if (Directory.Exists(root))
            {
                results = Directory.GetFiles(root, "*", SearchOption.AllDirectories)
                    .Where(IsImageFile)
                    .Where(x => NormalizeText(Path.GetFileNameWithoutExtension(x)).Contains(categoryKey))
                    .OrderBy(x => x)
                    .ToList();
            }

            _categoryImageFiles[categoryKey] = results;
            return results;
        }

        private static bool IsImageFile(string file)
        {
            var ext = Path.GetExtension(file).ToLowerInvariant();
            return ext is ".jpg" or ".jpeg" or ".png" or ".webp" or ".bmp";
        }

        private static string GetCategoryKey(string? categoryName)
        {
            var normalized = NormalizeText(categoryName ?? string.Empty);

            if (normalized.Contains("ca phe") || normalized.Contains("coffee")) return "coffee";
            if (normalized.Contains("tra sua") || normalized.Contains("milk tea")) return "milktea";
            if (normalized.Contains("tra trai cay") || normalized.Contains("fruit tea")) return "fruittea";
            if (normalized.Contains("sua tuoi") || normalized.Contains("sua chua") || normalized.Contains("yogurt")) return "milk";
            if (normalized.Contains("da xay") || normalized.Contains("frappe")) return "frappe";
            if (normalized.Contains("tra pure") || normalized.Contains("pure tea")) return "puretea";

            return "default";
        }

        private static string GetProjectPath()
        {
            var current = AppDomain.CurrentDomain.BaseDirectory;
            while (!string.IsNullOrEmpty(current) && !File.Exists(Path.Combine(current, "MilkTeaPOS.csproj")))
            {
                current = Directory.GetParent(current)?.FullName;
            }

            return current ?? AppDomain.CurrentDomain.BaseDirectory;
        }

        private static Bitmap CreatePlaceholderImage()
        {
            var bmp = new Bitmap(170, 62);
            using var g = Graphics.FromImage(bmp);
            g.Clear(Color.FromArgb(245, 245, 245));

            using var border = new Pen(Color.FromArgb(220, 220, 220));
            g.DrawRectangle(border, 0, 0, 169, 61);

            using var brush = new SolidBrush(Color.Gray);
            using var font = new Font("Segoe UI", 9F);
            var text = "No Image";
            var size = g.MeasureString(text, font);
            g.DrawString(text, font, brush, (170 - size.Width) / 2, (62 - size.Height) / 2);

            return bmp;
        }

        private static string NormalizeText(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return string.Empty;

            var normalized = value.Trim().ToLowerInvariant().Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();
            foreach (var ch in normalized)
            {
                var category = CharUnicodeInfo.GetUnicodeCategory(ch);
                if (category != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(ch == 'đ' ? 'd' : ch);
                }
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }

        private sealed class CartItem
        {
            public Guid ItemId { get; set; }
            public Guid ProductId { get; set; }
            public string ProductName { get; set; } = string.Empty;
            public string Size { get; set; } = "M";
            public string Sugar { get; set; } = "100";
            public string Ice { get; set; } = "100";
            public List<CartTopping> Toppings { get; set; } = new();
            public decimal UnitPrice { get; set; }
            public int Quantity { get; set; }
            public string Note { get; set; } = string.Empty;
            public decimal ToppingTotal => Toppings.Sum(t => t.Price);
            public decimal LineTotal => (UnitPrice + ToppingTotal) * Quantity;
            public string OptionSummary => $"{Size} - đường {Sugar}% - đá {Ice}%";
        }

        private sealed class CartTopping
        {
            public Guid ToppingId { get; set; }
            public string Name { get; set; } = string.Empty;
            public decimal Price { get; set; }
        }

        private sealed class SizeOption
        {
            public string Label { get; set; } = "M";
            public decimal Price { get; set; }
        }

        private sealed class CheckoutSnapshotItem
        {
            public Guid ProductId { get; set; }
            public string ProductName { get; set; } = string.Empty;
            public string ProductImage { get; set; } = string.Empty;
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal ToppingTotal { get; set; }
            public decimal LineTotal { get; set; }
            public string Size { get; set; } = "M";
            public string Sugar { get; set; } = "100";
            public string Ice { get; set; } = "100";
            public List<CheckoutSnapshotTopping> Toppings { get; set; } = new();
        }

        private sealed class CheckoutSnapshotTopping
        {
            public Guid ToppingId { get; set; }
            public string Name { get; set; } = string.Empty;
            public decimal Price { get; set; }
        }

        private sealed class VoucherSnapshot
        {
            public Guid Id { get; set; }
            public string Code { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
            public string VoucherType { get; set; } = string.Empty;
            public string Status { get; set; } = string.Empty;
            public decimal DiscountValue { get; set; }
            public decimal MinOrderAmount { get; set; }
            public decimal? MaxDiscountAmount { get; set; }
            public int? UsageLimit { get; set; }
            public int UsageCount { get; set; }
            public DateTime? ValidFrom { get; set; }
            public DateTime? ValidUntil { get; set; }
            public HashSet<string> ApplicableTiers { get; set; } = new(StringComparer.OrdinalIgnoreCase);
        }

        private decimal CalculateVoucherDiscount(decimal subtotal, VoucherSnapshot? voucher)
        {
            if (voucher == null || subtotal <= 0)
            {
                System.Diagnostics.Debug.WriteLine($"[Voucher] SKIP: voucher={voucher != null}, subtotal={subtotal}");
                return 0m;
            }

            var nowUtc = DateTime.UtcNow;
            if (!string.Equals(voucher.Status, "active", StringComparison.OrdinalIgnoreCase))
            {
                System.Diagnostics.Debug.WriteLine($"[Voucher] SKIP: status='{voucher.Status}'");
                return 0m;
            }

            // Convert to UTC before comparing (DB timestamps may be local or UTC)
            var validFromUtc = voucher.ValidFrom.HasValue ? voucher.ValidFrom.Value.ToUniversalTime() : (DateTime?)null;
            var validUntilUtc = voucher.ValidUntil.HasValue ? voucher.ValidUntil.Value.ToUniversalTime() : (DateTime?)null;

            if (validFromUtc.HasValue && validFromUtc.Value > nowUtc)
            {
                System.Diagnostics.Debug.WriteLine($"[Voucher] SKIP: valid_from='{validFromUtc}' > now='{nowUtc}'");
                return 0m;
            }
            if (validUntilUtc.HasValue && validUntilUtc.Value <= nowUtc)
            {
                System.Diagnostics.Debug.WriteLine($"[Voucher] SKIP: valid_until='{validUntilUtc}' <= now='{nowUtc}'");
                return 0m;
            }
            if (subtotal < voucher.MinOrderAmount)
            {
                System.Diagnostics.Debug.WriteLine($"[Voucher] SKIP: subtotal={subtotal} < min={voucher.MinOrderAmount}");
                return 0m;
            }
            if (voucher.UsageLimit.HasValue && voucher.UsageCount >= voucher.UsageLimit.Value)
            {
                System.Diagnostics.Debug.WriteLine($"[Voucher] SKIP: usageCount={voucher.UsageCount} >= limit={voucher.UsageLimit}");
                return 0m;
            }

            var tier = ResolveCurrentTier();
            System.Diagnostics.Debug.WriteLine($"[Voucher] tier='{tier}', applicableTiers=[{string.Join(",", voucher.ApplicableTiers)}]");
            if (voucher.ApplicableTiers.Count > 0 && !voucher.ApplicableTiers.Contains(tier))
            {
                System.Diagnostics.Debug.WriteLine($"[Voucher] SKIP: tier '{tier}' not in applicable_tiers");
                return 0m;
            }

            var discount = voucher.VoucherType switch
            {
                "percentage" => Math.Min(subtotal * (voucher.DiscountValue / 100m), voucher.MaxDiscountAmount ?? subtotal),
                "fixed_amount" => Math.Min(voucher.DiscountValue, subtotal),
                "free_item" => CalculateFreeItemDiscount(subtotal),
                "buy_one_get_one" => CalculateBogoDiscount(subtotal),
                _ => 0m
            };
            System.Diagnostics.Debug.WriteLine($"[Voucher] type='{voucher.VoucherType}', discount={discount}");
            return discount;
        }

        private decimal CalculateFreeItemDiscount(decimal subtotal)
        {
            var minToppingPrice = _cartItems
                .SelectMany(i => i.Toppings)
                .Select(t => t.Price)
                .DefaultIfEmpty(0m)
                .Min();

            return Math.Min(minToppingPrice, subtotal);
        }

        private decimal CalculateBogoDiscount(decimal subtotal)
        {
            var discount = _cartItems
                .Where(i => NormalizeText(i.ProductName).Contains("tra sua truyen thong"))
                .Sum(i => Math.Floor(i.Quantity / 2m) * i.UnitPrice);

            return Math.Min(discount, subtotal);
        }

        private string ResolveCurrentTier()
        {
            // Dùng tier thực tế từ DB thay vì tính từ total_spent
            return _selectedMembership?.Tier ?? "none";
        }

        private static HashSet<string> ParseTierArray(string? value)
        {
            var result = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            if (string.IsNullOrWhiteSpace(value)) return result;

            var trimmed = value.Trim().Trim('{', '}');
            if (string.IsNullOrWhiteSpace(trimmed)) return result;

            foreach (var part in trimmed.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
            {
                result.Add(part.Trim('"'));
            }

            return result;
        }

        private static T? GetNullableValue<T>(DbDataReader reader, string name) where T : struct
        {
            var ordinal = reader.GetOrdinal(name);
            if (reader.IsDBNull(ordinal)) return null;
            return reader.GetFieldValue<T>(ordinal);
        }

        private static string GetStringValue(DbDataReader reader, string name)
        {
            var ordinal = reader.GetOrdinal(name);
            if (reader.IsDBNull(ordinal)) return string.Empty;
            return reader.GetString(ordinal);
        }

        /// <summary>
        /// Resolve relative image path from project root for snapshot storage.
        /// </summary>
        private string? GetProductImageForSnapshot(Guid productId)
        {
            try
            {
                using var context = new PostgresContext();
                var imageUrl = context.Products
                    .AsNoTracking()
                    .Where(p => p.Id == productId)
                    .Select(p => p.ImageUrl)
                    .FirstOrDefault();

                if (string.IsNullOrWhiteSpace(imageUrl)) return null;

                // Store relative path as-is (e.g., "images/categories/coffee/xxx.png")
                var normalized = imageUrl.Trim().TrimStart('/', '\\');
                if (normalized.Contains("..") || Path.IsPathRooted(normalized)) return null;
                return normalized;
            }
            catch
            {
                return null;
            }
        }

        private bool TryLoadVoucher(string voucherCode, out VoucherSnapshot? voucher, out string errorMessage)
        {
            voucher = null;
            errorMessage = string.Empty;

            try
            {
                using var context = new PostgresContext();
                var connection = context.Database.GetDbConnection();

                using var command = connection.CreateCommand();
                command.CommandText = @"
SELECT
    id,
    code,
    name,
    voucher_type::text AS voucher_type,
    status::text AS status,
    discount_value,
    min_order_amount,
    max_discount_amount,
    usage_limit,
    usage_count,
    valid_from,
    valid_until,
    applicable_tiers::text AS applicable_tiers
FROM vouchers
WHERE UPPER(code) = @code
LIMIT 1;";

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@code";
                parameter.Value = voucherCode.ToUpperInvariant();
                command.Parameters.Add(parameter);

                connection.Open();
                using var reader = command.ExecuteReader();
                if (!reader.Read())
                {
                    errorMessage = "Không tìm thấy voucher.";
                    return false;
                }

                voucher = new VoucherSnapshot
                {
                    Id = reader.GetFieldValue<Guid>(reader.GetOrdinal("id")),
                    Code = GetStringValue(reader, "code"),
                    Name = GetStringValue(reader, "name"),
                    VoucherType = GetStringValue(reader, "voucher_type"),
                    Status = GetStringValue(reader, "status"),
                    DiscountValue = reader.GetFieldValue<decimal>(reader.GetOrdinal("discount_value")),
                    MinOrderAmount = reader.GetFieldValue<decimal>(reader.GetOrdinal("min_order_amount")),
                    MaxDiscountAmount = GetNullableValue<decimal>(reader, "max_discount_amount"),
                    UsageLimit = GetNullableValue<int>(reader, "usage_limit"),
                    UsageCount = reader.GetFieldValue<int>(reader.GetOrdinal("usage_count")),
                    ValidFrom = GetNullableValue<DateTime>(reader, "valid_from"),
                    ValidUntil = GetNullableValue<DateTime>(reader, "valid_until"),
                    ApplicableTiers = ParseTierArray(GetStringValue(reader, "applicable_tiers"))
                };

                System.Diagnostics.Debug.WriteLine($"[Voucher] Raw applicable_tiers from DB: '{GetStringValue(reader, "applicable_tiers")}'");
                System.Diagnostics.Debug.WriteLine($"[Voucher] Parsed tiers: [{string.Join(",", voucher.ApplicableTiers)}]");

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = $"Không thể tải voucher. {ex.Message}";
                return false;
            }
        }

        private void btnApdungvoucher_Click(object sender, EventArgs e)
        {
            // Khi thử áp mã mới, luôn bỏ mã cũ trước để tránh dính giảm giá trước đó
            _appliedVoucher = null;
            _voucherDiscount = 0m;
            UpdateTotals();

            if (_cartItems.Count == 0)
            {
                MessageBox.Show("Vui lòng thêm sản phẩm trước khi áp dụng voucher.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var code = txtVoucher.Text.Trim();
            if (string.IsNullOrWhiteSpace(code))
            {
                MessageBox.Show("Vui lòng nhập mã voucher.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!TryLoadVoucher(code, out var voucher, out var error))
            {
                MessageBox.Show(error, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var subtotal = _cartItems.Sum(x => x.LineTotal);
            var discount = CalculateVoucherDiscount(subtotal, voucher);
            if (discount <= 0m)
            {
                MessageBox.Show("Voucher không áp dụng được cho đơn hiện tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _appliedVoucher = voucher;
            UpdateTotals();

            MessageBox.Show($"Áp dụng voucher {_appliedVoucher.Code} thành công. Giảm {FormatCurrency(_voucherDiscount)}.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnThemMember_Click(object sender, EventArgs e)
        {
            // Ưu tiên txtSDT, nếu trống thì dùng txtfind
            var phone = txtSDT.Text.Trim();
            if (string.IsNullOrWhiteSpace(phone))
            {
                phone = txtfind.Text.Trim();
            }

            if (string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại.\nNhập vào ô tìm kiếm hoặc ô SĐT góc dưới.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                using var context = new PostgresContext();

                var normalizedPhone = NormalizePhoneNumber(phone);
                if (string.IsNullOrWhiteSpace(normalizedPhone))
                {
                    MessageBox.Show("Số điện thoại không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var customer = context.Customers
                    .FirstOrDefault(c => c.Phone == normalizedPhone);

                var isNewCustomer = false;

                if (customer == null)
                {
                    isNewCustomer = true;
                    customer = new Customer
                    {
                        Id = Guid.NewGuid(),
                        Name = $"KH {normalizedPhone}",
                        Phone = normalizedPhone,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };
                    context.Customers.Add(customer);
                    context.SaveChanges();
                }

                var membership = context.Memberships
                    .FirstOrDefault(m => m.CustomerId == customer.Id);

                if (membership == null)
                {
                    // Dùng raw SQL để tránh lỗi enum type mapping
                    var memId = Guid.NewGuid();
                    context.Database.ExecuteSqlRaw(
                        @"INSERT INTO memberships (id, customer_id, tier, points, total_spent, total_orders, joined_at, updated_at)
                          VALUES ({0}, {1}, 'none'::membership_tier, 0, 0, 0, {2}, {3})",
                        memId, customer.Id, DateTime.UtcNow, DateTime.UtcNow);

                    membership = context.Memberships.Find(memId);
                }

                _selectedCustomer = customer;
                _selectedMembership = membership;
                UpdateTotals();

                // Update customer display
                var tierLabel = membership.Tier ?? "none";
                lblCustomerInfo.Text = $"{customer.Name} ({tierLabel.ToUpperInvariant()})";
                txtVoucher.Enabled = true;
                btnApdungvoucher.Enabled = true;

                MessageBox.Show(isNewCustomer ? "Đã tạo thẻ thành viên mới." : "Đã áp dụng thẻ thành viên.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LogAuditAddMember(customer.Id, customer.Name, isNewCustomer);
            }
            catch (Exception ex)
            {
                var details = ex.InnerException?.Message ?? ex.Message;
                MessageBox.Show($"Không thể thêm thành viên.\n{details}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTimMember_Click(object sender, EventArgs e)
        {
            // Ưu tiên txtSDT, nếu trống thì dùng txtfind
            var phone = txtSDT.Text.Trim();
            if (string.IsNullOrWhiteSpace(phone))
            {
                phone = txtfind.Text.Trim();
            }

            if (string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại để tìm.\nNhập vào ô tìm kiếm hoặc ô SĐT góc dưới.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                using var context = new PostgresContext();
                var normalizedPhone = NormalizePhoneNumber(phone);

                // Use LEFT JOIN so customers without memberships can still be found
                var query = from c in context.Customers
                            join m in context.Memberships on c.Id equals m.CustomerId into mj
                            from m in mj.DefaultIfEmpty()
                            where c.Phone == normalizedPhone
                            select new { Customer = c, Membership = (Membership?)m };

                var result = query.FirstOrDefault();

                if (result == null || result.Customer == null)
                {
                    _selectedCustomer = null;
                    _selectedMembership = null;
                    UpdateTotals();
                    lblCustomerInfo.Text = "Khách lẻ";
                    MessageBox.Show("Không tìm thấy thành viên với số điện thoại này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                _selectedCustomer = result.Customer;
                _selectedMembership = result.Membership;

                // Update customer display
                if (_selectedMembership != null)
                {
                    var tierLabel = _selectedMembership.Tier ?? "none";
                    lblCustomerInfo.Text = $"{_selectedCustomer.Name} ({tierLabel.ToUpperInvariant()})";
                    txtVoucher.Enabled = true;
                    btnApdungvoucher.Enabled = true;
                }
                else
                {
                    lblCustomerInfo.Text = $"{_selectedCustomer.Name} (Chưa có thẻ)";
                    txtVoucher.Enabled = false;
                    btnApdungvoucher.Enabled = false;
                }

                // Sync phone to txtSDT
                txtSDT.Text = normalizedPhone;

                UpdateTotals();

                var membershipInfo = _selectedMembership != null
                    ? $"Hạng: {_selectedMembership.Tier?.ToUpperInvariant() ?? "NONE"}\nĐiểm: {_selectedMembership.Points:N0}\nChi tiêu: {_selectedMembership.TotalSpent:N0}đ"
                    : "Khách hàng chưa có thẻ thành viên.";

                MessageBox.Show($"Thành viên: {_selectedCustomer.Name}\n{membershipInfo}", "Thông tin thành viên", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                var details = ex.InnerException?.Message ?? ex.Message;
                MessageBox.Show($"Không thể tìm thành viên.\n{details}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoaGioHang_Click(object sender, EventArgs e)
        {
            if (_cartItems.Count == 0)
            {
                MessageBox.Show("Giỏ hàng đang trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show("Bạn có chắc muốn xóa toàn bộ giỏ hàng?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                _cartItems.Clear();
                _selectedCustomer = null;
                _selectedMembership = null;
                _appliedVoucher = null;
                _voucherDiscount = 0m;
                txtVoucher.Clear();
                txtSDT.Clear();
                lblCustomerInfo.Text = "Khách lẻ";
                txtVoucher.Enabled = false;
                btnApdungvoucher.Enabled = false;
                lblInvoiceInfo.Text = $"HD-{DateTime.Now:yyMMddHHmm}";
                RenderCart();
                UpdateTotals();

                LogAuditClearCart();
            }
        }

        private static void LogAuditAddMember(Guid customerId, string customerName, bool isNew)
        {
            try
            {
                var userId = PostgresContext.CurrentUserId;
                if (!userId.HasValue) return;

                var escaped = $"{{\"details\": \"{(isNew ? "Created" : "Linked")} member: {customerName}\", \"customerId\": \"{customerId}\"}}";

                using var context = new PostgresContext();
                context.AuditLogs.Add(new AuditLog
                {
                    Id = Guid.NewGuid(),
                    UserId = userId.Value,
                    Action = isNew ? "create_member" : "link_member",
                    TableName = "customers",
                    RecordId = customerId,
                    NewValues = escaped,
                    IpAddress = PostgresContext.CurrentUserIP,
                    CreatedAt = DateTime.UtcNow
                });
                context.SaveChanges();
            }
            catch { }
        }

        private static void LogAuditClearCart()
        {
            try
            {
                var userId = PostgresContext.CurrentUserId;
                if (!userId.HasValue) return;

                using var context = new PostgresContext();
                context.AuditLogs.Add(new AuditLog
                {
                    Id = Guid.NewGuid(),
                    UserId = userId.Value,
                    Action = "clear_cart",
                    TableName = "orders",
                    NewValues = "{\"details\": \"Cart cleared before order\"}",
                    IpAddress = PostgresContext.CurrentUserIP,
                    CreatedAt = DateTime.UtcNow
                });
                context.SaveChanges();
            }
            catch { }
        }

        private async void btnThanhtoan_Click(object sender, EventArgs e)
        {
            if (_cartItems.Count == 0)
            {
                MessageBox.Show("Giỏ hàng đang trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var totals = CalculateCurrentTotals();
            if (totals.Total <= 0m)
            {
                MessageBox.Show("Tổng thanh toán không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var paymentLines = _cartItems.Select(i => new frmPayment.PaymentLineItem
            {
                ProductName = i.ProductName,
                Options = i.OptionSummary,
                Toppings = i.Toppings.Select(t => t.Name).ToList(),
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice + i.ToppingTotal,
                LineTotal = i.LineTotal
            }).ToList();

            using var paymentForm = new frmPayment(
                totals.Total,
                paymentLines,
                _selectedCustomer?.Phone,
                _selectedCustomer?.Name,
                _selectedMembership?.Points ?? 0,
                _appliedVoucher?.Code,
                totals.VoucherDiscount,
                lblInvoiceInfo.Text);

            if (paymentForm.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            if (paymentForm.ReceivedAmount < totals.Total)
            {
                MessageBox.Show("Số tiền nhận chưa đủ để thanh toán.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var cartSnapshot = _cartItems
                .Select(i => new CheckoutSnapshotItem
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    ProductImage = GetProductImageForSnapshot(i.ProductId),
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    ToppingTotal = i.ToppingTotal,
                    LineTotal = i.LineTotal,
                    Size = i.Size,
                    Sugar = i.Sugar,
                    Ice = i.Ice,
                    Toppings = i.Toppings.Select(t => new CheckoutSnapshotTopping
                    {
                        ToppingId = t.ToppingId,
                        Name = t.Name,
                        Price = t.Price
                    }).ToList()
                })
                .ToList();

            try
            {
                var saveResult = await PersistOrderAsync(
                    isHoldOrder: false,
                    cartSnapshot: cartSnapshot,
                    totals: totals,
                    receivedAmount: paymentForm.ReceivedAmount,
                    paymentMethod: paymentForm.SelectedPaymentMethod);

                if (!saveResult.Success)
                {
                    MessageBox.Show($"Thanh toán thất bại.\n{saveResult.ErrorMessage}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var completedOrderNumber = saveResult.OrderNumber ?? string.Empty;
                var changeAmount = saveResult.ChangeAmount;

                if (paymentForm.ShouldPrintReceipt)
                {
                    PrintCashReceipt(completedOrderNumber, cartSnapshot, totals, paymentForm.ReceivedAmount, changeAmount, paymentForm.SelectedPaymentMethod);
                }

                _cartItems.Clear();
                _appliedVoucher = null;
                _voucherDiscount = 0m;
                txtVoucher.Clear();
                if (_selectedCustomer != null)
                {
                    using var reloadContext = new PostgresContext();
                    _selectedMembership = await reloadContext.Memberships
                        .AsNoTracking()
                        .FirstOrDefaultAsync(m => m.CustomerId == _selectedCustomer.Id);
                }
                lblInvoiceInfo.Text = $"HD-{DateTime.Now:yyMMddHHmm}";
                RenderCart();
                UpdateTotals();

                // Refresh table status display (show/hide "Trả bàn" button if table became occupied)
                if (cboBan.Visible && cboBan.SelectedIndex >= 0)
                {
                    cboBan_SelectedIndexChanged(null, null);
                }

                MessageBox.Show($"Thanh toán thành công. Mã đơn: {completedOrderNumber}.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                try
                {
                    await OpenOrderHistoryFormAsync();
                }
                catch (Exception exOpen)
                {
                    MessageBox.Show($"Đã thanh toán thành công nhưng không mở được lịch sử đơn.\n{exOpen.Message}", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                var details = ex.InnerException?.Message ?? ex.Message;
                MessageBox.Show($"Thanh toán thất bại.\n{details}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task<(bool Success, string? OrderNumber, decimal ChangeAmount, string? ErrorMessage)> PersistOrderAsync(
            bool isHoldOrder,
            List<CheckoutSnapshotItem> cartSnapshot,
            (decimal Subtotal, decimal Discount, decimal PointsDiscount, decimal VoucherDiscount, decimal Total) totals,
            decimal receivedAmount,
            string? paymentMethod)
        {
            var session = Guid.NewGuid().ToString("N")[..8];

            try
            {
                using var context = new PostgresContext();
                LogOrderStep(session, "START", "open context");

                var now = DateTime.UtcNow;
                var orderSelection = cbotrangthai.SelectedItem?.ToString();
                var isDelivery = IsDeliverySelection(orderSelection);
                Guid? tableId = null;
                if (orderSelection == "Tại quán" && cboBan.Visible && cboBan.SelectedItem != null)
                {
                    tableId = await ResolveSelectedTableIdAsync(context, cboBan.SelectedItem.ToString());

                    // Block if table is already occupied
                    if (tableId.HasValue)
                    {
                        var conn = context.Database.GetDbConnection();
                        await using var cmd = conn.CreateCommand();
                        cmd.CommandText = @"
                            SELECT status::text FROM tables
                            WHERE id = @tableId
                            LIMIT 1";
                        var p = cmd.CreateParameter();
                        p.ParameterName = "@tableId";
                        p.Value = tableId.Value;
                        cmd.Parameters.Add(p);

                        if (conn.State != System.Data.ConnectionState.Open)
                            await conn.OpenAsync();

                        var tableStatus = await cmd.ExecuteScalarAsync();

                        if (tableStatus != null && tableStatus.ToString() == "occupied")
                        {
                            var tableName = cboBan.SelectedItem.ToString();
                            LogOrderStep(session, "BLOCKED", $"table {tableName} is occupied");
                            return (false, null, 0m, $"Bàn {tableName} đang có khách ngồi. Vui lòng trả bàn trước khi tạo đơn mới.");
                        }
                    }
                }

                LogOrderStep(session, "BEGIN", $"isHold={isHoldOrder}, items={cartSnapshot.Count}, total={totals.Total:N0}");

                var orderId = Guid.NewGuid();
                var orderStatus = isHoldOrder ? "pending" : "served";
                var servedAt = isHoldOrder ? (DateTime?)null : now; // Set served_at for direct checkout

                // Truncate fields to match DB constraints
                var safeCustomerName = _selectedCustomer?.Name?.Substring(0, Math.Min(_selectedCustomer.Name.Length, 100));
                var safeCustomerPhone = _selectedCustomer?.Phone?.Substring(0, Math.Min(_selectedCustomer.Phone.Length, 20));

                context.Database.ExecuteSqlInterpolated($@"
INSERT INTO orders (
    id, status, user_id, table_id, customer_id, customer_name, customer_phone,
    subtotal, discount, voucher_discount, membership_discount, total_amount,
    is_delivery, served_at, notes, created_at, updated_at
)
VALUES (
    {orderId}, {orderStatus}::order_status, {PostgresContext.CurrentUserId}, {tableId}, {_selectedCustomer?.Id}, {safeCustomerName}, {safeCustomerPhone},
    {totals.Subtotal}, {totals.Discount}, {totals.VoucherDiscount}, {totals.PointsDiscount}, {totals.Total},
    {isDelivery}, {servedAt}, { (isHoldOrder ? "Đơn giữ" : null) }, {now}, {now}
);
");
                LogOrderStep(session, "ORDER", $"saved orderId={orderId}");

                // Set table status to occupied when order is created
                if (tableId.HasValue && !isHoldOrder)
                {
                    context.Database.ExecuteSqlInterpolated($@"
UPDATE tables
SET status = 'occupied'::table_status,
    updated_at = {now}
WHERE id = {tableId.Value};
");
                    LogOrderStep(session, "TABLE", $"table {tableId.Value} set to occupied");
                }

                var detailPairs = new List<(Guid DetailId, CheckoutSnapshotItem CartItem)>();
                foreach (var cartItem in cartSnapshot)
                {
                    var detailId = Guid.NewGuid();
                    detailPairs.Add((detailId, cartItem));

                    context.Database.ExecuteSqlInterpolated($@"
INSERT INTO order_details (
    id, order_id, product_id, product_name, product_image, quantity,
    size, sugar, ice,
    unit_price, topping_total, subtotal, created_at, updated_at
)
VALUES (
    {detailId}, {orderId}, {cartItem.ProductId}, {cartItem.ProductName}, {cartItem.ProductImage}, {cartItem.Quantity},
    {cartItem.Size}::size_type, {cartItem.Sugar}::sugar_level, {cartItem.Ice}::ice_level,
    {cartItem.UnitPrice}, {cartItem.ToppingTotal}, {cartItem.LineTotal}, {now}, {now}
);
");
                }

                LogOrderStep(session, "DETAIL", $"saved details={detailPairs.Count}");

                foreach (var pair in detailPairs)
                {
                    foreach (var topping in pair.CartItem.Toppings)
                    {
                        context.Database.ExecuteSqlInterpolated($@"
INSERT INTO order_toppings (
    id, order_detail_id, topping_id, topping_name, quantity, price, created_at
)
VALUES (
    {Guid.NewGuid()}, {pair.DetailId}, {topping.ToppingId}, {topping.Name}, {1}, {topping.Price}, {now}
);
");
                    }
                }

                LogOrderStep(session, "TOPPING", "saved toppings");

                if (_selectedCustomer != null)
                {
                    context.Database.ExecuteSqlInterpolated($@"
INSERT INTO memberships (
    id, customer_id, points, total_spent, total_orders, joined_at, updated_at
)
SELECT {Guid.NewGuid()}, {_selectedCustomer.Id}, 0, 0, 0, {now}, {now}
WHERE NOT EXISTS (
    SELECT 1 FROM memberships WHERE customer_id = {_selectedCustomer.Id}
);
");
                    LogOrderStep(session, "MEMBER", "membership upsert checked");
                }

                var changeAmount = 0m;
                if (!isHoldOrder)
                {
                    var effectivePaymentMethod = string.IsNullOrWhiteSpace(paymentMethod) ? "cash" : paymentMethod;
                    changeAmount = Math.Max(0m, receivedAmount - totals.Total);
                    context.Database.ExecuteSqlInterpolated($@"
INSERT INTO payments (
    id, order_id, method, received_amount, paid_amount, change_amount,
    status, transaction_id, created_at, updated_at, payment_info
)
VALUES (
    {Guid.NewGuid()}, {orderId}, {effectivePaymentMethod}::payment_method, {receivedAmount}, {totals.Total}, {changeAmount},
    {"completed"}::payment_status, {Guid.NewGuid().ToString("N")}, {now}, {now}, {"POS checkout"}
);
");
                    LogOrderStep(session, "PAYMENT", $"saved payment method={paymentMethod}");

                    if (_appliedVoucher != null && totals.VoucherDiscount > 0m)
                    {
                        context.Database.ExecuteSqlInterpolated($@"
UPDATE vouchers
SET usage_count = usage_count + 1,
    updated_at = {now}
WHERE id = {_appliedVoucher.Id};
");
                        LogOrderStep(session, "VOUCHER", $"voucher usage_count incremented, discount={totals.VoucherDiscount}");
                    }

                    // KHÔNG free table sau thanh toán - khách vẫn đang ngồi ăn
                    // Table sẽ được giải phóng thủ công qua nút "Trả bàn"
                    if (tableId.HasValue)
                    {
                        LogOrderStep(session, "TABLE", $"table {tableId.Value} remains occupied (customer still seated)");
                    }

                    // Update order_details status to match order status (served)
                    context.Database.ExecuteSqlInterpolated($@"
UPDATE order_details
SET status = {orderStatus}::order_detail_status,
    updated_at = {now}
WHERE order_id = {orderId};
");
                    LogOrderStep(session, "DETAIL_STATUS", $"updated order_details status to {orderStatus}");

                    // Manually update membership stats (trigger doesn't fire on INSERT with served status)
                    if (_selectedCustomer != null && _selectedMembership != null)
                    {
                        var pointsEarned = (int)Math.Floor(totals.Total / 10000m);
                        context.Database.ExecuteSqlInterpolated($@"
UPDATE memberships
SET
    points = points + {pointsEarned},
    total_spent = total_spent + {totals.Total},
    total_orders = total_orders + 1,
    last_order_at = {now},
    updated_at = {now}
WHERE customer_id = {_selectedCustomer.Id};
");
                        LogOrderStep(session, "MEMBERSHIP", $"points +{pointsEarned}, total_spent += {totals.Total:N0}");
                    }

                    // Table sẽ được giải phóng thủ công qua nút "Trả bàn"
                    if (tableId.HasValue)
                    {
                        LogOrderStep(session, "TABLE", $"table {tableId.Value} remains occupied (customer still seated)");
                    }
                }

                var completedOrderNumber = orderId.ToString()[..8].ToUpperInvariant();
                LogOrderStep(session, "COMMIT", $"success orderNo={completedOrderNumber}");

                // Audit log
                LogAuditOrder(isHoldOrder ? "hold" : "checkout", orderId, $"orderNo={completedOrderNumber}, total={totals.Total:N0}");

                return (true, completedOrderNumber, changeAmount, (string?)null);
            }
            catch (PostgresException pgEx)
            {
                var error = $"[{pgEx.SqlState}] {pgEx.MessageText} | Detail: {pgEx.Detail}";
                LogOrderStep(session, "ERROR-POSTGRES", error);
                return (false, (string?)null, 0m, error);
            }
            catch (Exception ex)
            {
                var error = ex.InnerException?.Message ?? ex.Message;
                LogOrderStep(session, "ERROR", ex.ToString());
                return (false, (string?)null, 0m, error);
            }
        }

        private static void LogOrderStep(string session, string step, string message)
        {
            try
            {
                var logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
                Directory.CreateDirectory(logDir);
                var file = Path.Combine(logDir, "order-save.log");
                var line = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} [{session}] [{step}] {message}{Environment.NewLine}";
                File.AppendAllText(file, line);
            }
            catch
            {
            }
        }

        private async Task OpenOrderHistoryFormAsync()
        {
            using var history = new frmOrderHistory();
            var result = history.ShowDialog(this);
            if (result == DialogResult.OK && history.ResumeOrderId.HasValue)
            {
                await LoadHeldOrderToCurrentCartAsync(history.ResumeOrderId.Value);
            }
        }

        private async Task LoadHeldOrderToCurrentCartAsync(Guid orderId)
        {
            try
            {
                using var context = new PostgresContext();
                var holdOrder = await context.Orders.AsNoTracking().FirstOrDefaultAsync(x => x.Id == orderId);
                if (holdOrder == null)
                {
                    MessageBox.Show("Không tìm thấy đơn giữ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var connection = context.Database.GetDbConnection();
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    await connection.OpenAsync();
                }

                await using (var statusCommand = connection.CreateCommand())
                {
                    statusCommand.CommandText = "SELECT status::text FROM orders WHERE id = @orderId LIMIT 1;";
                    var pStatusOrderId = statusCommand.CreateParameter();
                    pStatusOrderId.ParameterName = "@orderId";
                    pStatusOrderId.Value = orderId;
                    statusCommand.Parameters.Add(pStatusOrderId);

                    var status = (await statusCommand.ExecuteScalarAsync())?.ToString() ?? string.Empty;
                    if (status is not ("pending" or "preparing" or "ready"))
                    {
                        MessageBox.Show("Đơn này không còn ở trạng thái đơn giữ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                var loadedItems = new List<CartItem>();
                var details = new List<(Guid DetailId, Guid ProductId, string ProductName, int Quantity, decimal UnitPrice, string Size, string Sugar, string Ice)>();

                await using (var detailCommand = connection.CreateCommand())
                {
                    detailCommand.CommandText = @"
SELECT
    od.id,
    od.product_id,
    od.product_name,
    COALESCE(od.quantity, 1),
    COALESCE(od.unit_price, 0),
    COALESCE(od.size::text, 'M'),
    COALESCE(od.sugar::text, '100'),
    COALESCE(od.ice::text, '100')
FROM order_details od
WHERE od.order_id = @orderId
ORDER BY od.created_at;";

                    var pOrderId = detailCommand.CreateParameter();
                    pOrderId.ParameterName = "@orderId";
                    pOrderId.Value = orderId;
                    detailCommand.Parameters.Add(pOrderId);

                    await using var detailReader = await detailCommand.ExecuteReaderAsync();
                    while (await detailReader.ReadAsync())
                    {
                        details.Add((
                            detailReader.GetGuid(0),
                            detailReader.GetGuid(1),
                            detailReader.GetString(2),
                            detailReader.GetInt32(3),
                            detailReader.GetDecimal(4),
                            detailReader.GetString(5),
                            detailReader.GetString(6),
                            detailReader.GetString(7)));
                    }
                }

                foreach (var detail in details)
                {
                    var toppings = new List<CartTopping>();
                    await using var toppingCommand = connection.CreateCommand();
                    toppingCommand.CommandText = @"
SELECT topping_id, topping_name, COALESCE(price, 0), COALESCE(quantity, 1)
FROM order_toppings
WHERE order_detail_id = @detailId
ORDER BY created_at;";

                    var pDetailId = toppingCommand.CreateParameter();
                    pDetailId.ParameterName = "@detailId";
                    pDetailId.Value = detail.DetailId;
                    toppingCommand.Parameters.Add(pDetailId);

                    await using var toppingReader = await toppingCommand.ExecuteReaderAsync();
                    while (await toppingReader.ReadAsync())
                    {
                        var toppingId = toppingReader.GetGuid(0);
                        var toppingName = toppingReader.GetString(1);
                        var toppingPrice = toppingReader.GetDecimal(2);
                        var toppingQuantity = toppingReader.GetInt32(3);

                        for (var i = 0; i < Math.Max(1, toppingQuantity); i++)
                        {
                            toppings.Add(new CartTopping
                            {
                                ToppingId = toppingId,
                                Name = toppingName,
                                Price = toppingPrice
                            });
                        }
                    }

                    loadedItems.Add(new CartItem
                    {
                        ItemId = Guid.NewGuid(),
                        ProductId = detail.ProductId,
                        ProductName = detail.ProductName,
                        Size = string.IsNullOrWhiteSpace(detail.Size) ? "M" : detail.Size,
                        Sugar = string.IsNullOrWhiteSpace(detail.Sugar) ? "100" : detail.Sugar,
                        Ice = string.IsNullOrWhiteSpace(detail.Ice) ? "100" : detail.Ice,
                        UnitPrice = detail.UnitPrice,
                        Quantity = detail.Quantity,
                        Toppings = toppings
                    });
                }

                _cartItems.Clear();
                _cartItems.AddRange(loadedItems);

                _selectedCustomer = null;
                _selectedMembership = null;
                if (holdOrder.CustomerId.HasValue)
                {
                    _selectedCustomer = await context.Customers.AsNoTracking().FirstOrDefaultAsync(c => c.Id == holdOrder.CustomerId.Value);
                    _selectedMembership = await context.Memberships.AsNoTracking().FirstOrDefaultAsync(m => m.CustomerId == holdOrder.CustomerId.Value);
                }

                txtSDT.Text = _selectedCustomer?.Phone ?? string.Empty;
                _appliedVoucher = null;
                _voucherDiscount = 0m;
                txtVoucher.Clear();

                if (holdOrder.IsDelivery == true)
                {
                    cbotrangthai.SelectedItem = "Giao hàng";
                }
                else if (holdOrder.TableId.HasValue)
                {
                    // Set table combo to the table from the held order
                    var tableName = await context.Tables
                        .AsNoTracking()
                        .Where(t => t.Id == holdOrder.TableId.Value)
                        .Select(t => t.Name)
                        .FirstOrDefaultAsync();

                    if (!string.IsNullOrWhiteSpace(tableName) && cboBan.Items.Contains(tableName))
                    {
                        cbotrangthai.SelectedItem = "Tại quán";
                        cboBan.SelectedItem = tableName;
                    }
                }
                if (!string.IsNullOrWhiteSpace(holdOrder.OrderNumber))
                {
                    lblInvoiceInfo.Text = holdOrder.OrderNumber;
                }

                await context.Database.ExecuteSqlInterpolatedAsync($@"
DELETE FROM orders
WHERE id = {orderId}
  AND status IN ('pending', 'preparing', 'ready');
");

                // KHÔNG free table ở đây - khách vẫn đang ngồi
                // Table sẽ được free khi thanh toán xong

                RenderCart();
                UpdateTotals();
                MessageBox.Show("Đã nạp đơn giữ để tiếp tục gọi món và xóa đơn giữ cũ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể nạp đơn giữ.\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string NormalizePhoneNumber(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return string.Empty;

            var sb = new StringBuilder();
            foreach (var ch in phone)
            {
                if (char.IsDigit(ch)) sb.Append(ch);
            }

            return sb.ToString();
        }

        private void PrintCashReceipt(string orderNumber, List<CheckoutSnapshotItem> cartSnapshot, (decimal Subtotal, decimal Discount, decimal PointsDiscount, decimal VoucherDiscount, decimal Total) totals, decimal receivedAmount, decimal changeAmount, string? paymentMethod)
        {
            try
            {
                var lines = new List<string>();
                var w = 36; // width in characters for 80mm receipt

                void Center(string text) => lines.Add(text.PadLeft((w + text.Length) / 2).PadRight(w));
                void Left(string text) => lines.Add(text.PadRight(w));
                void Right(string text) => lines.Add(text.PadLeft(w));
                void Line() => lines.Add("═".PadRight(w, '═')[..w]);
                void Dashed() => lines.Add("─".PadRight(w, '─')[..w]);
                void Row(string left, string right)
                {
                    var pad = w - left.Length - right.Length;
                    if (pad < 1) pad = 1;
                    lines.Add(left + new string(' ', pad) + right);
                }

                // Header
                Center("╔══════════════════════════╗");
                Center("║     🧋 MILK TEA POS      ║");
                Center("║  Trà sữa hạnh phúc mỗi ngày║");
                Center("╚══════════════════════════╝");
                lines.Add("");
                Center("HÓA ĐƠN THANH TOÁN");
                Dashed();
                Row("Mã đơn:", orderNumber);
                Row("Ngày giờ:", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                Row("Thu ngân:", "Nhân viên");

                if (_selectedCustomer != null)
                {
                    var tier = _selectedMembership?.Tier?.ToUpperInvariant() ?? "NONE";
                    Row("Khách hàng:", $"{_selectedCustomer.Name} ({tier})");
                    if (_selectedMembership != null)
                        Row("  Điểm tích lũy:", (_selectedMembership.Points ?? 0).ToString("N0"));
                    if (!string.IsNullOrWhiteSpace(_selectedCustomer.Phone))
                        Row("  SĐT:", _selectedCustomer.Phone);
                }
                else
                {
                    Left("Khách hàng: Khách lẻ");
                }

                Dashed();
                lines.Add("");

                // Items header
                Row("Món", "Thành tiền");
                Dashed();

                foreach (var item in cartSnapshot)
                {
                    var qtyStr = $"x{item.Quantity}";
                    var nameLine = $"{item.ProductName} {qtyStr}";

                    // Options (size, sugar, ice)
                    var options = $"{item.Size} | Đ{item.Sugar}% | Đá{item.Ice}%";
                    Left($"  {nameLine}");
                    Left($"    ({options})");

                    // Toppings
                    if (item.Toppings.Count > 0)
                    {
                        foreach (var tp in item.Toppings)
                        {
                            Left($"    + {tp.Name}");
                        }
                    }

                    Row("", FormatCurrency(item.LineTotal));
                    lines.Add("");
                }

                Dashed();

                // Totals
                Row("Tạm tính:", FormatCurrency(totals.Subtotal));

                if (totals.Discount > 0)
                    Row("Giảm giá SP:", $"-{FormatCurrency(totals.Discount)}");

                if (totals.PointsDiscount > 0)
                    Row("Trừ điểm:", $"-{FormatCurrency(totals.PointsDiscount)}");

                if (totals.VoucherDiscount > 0)
                {
                    Row("Voucher:", $"-{FormatCurrency(totals.VoucherDiscount)}");
                    if (_appliedVoucher != null)
                        Left($"  ({_appliedVoucher.Code} - {_appliedVoucher.Name})");
                }

                Dashed();
                Center($"TỔNG THANH TOÁN");
                Center(FormatCurrency(totals.Total));
                Dashed();

                // Payment info
                var methodLabel = paymentMethod == "cash" ? "💵 Tiền mặt" : "📱 QR Code";
                Row("Phương thức:", methodLabel);
                Row("Khách đưa:", FormatCurrency(receivedAmount));
                Row("Tiền thối:", FormatCurrency(changeAmount));

                lines.Add("");
                Center("╭──────────────────────────╮");
                Center("│  Cảm ơn quý khách! 💛    │");
                Center("│  Hẹn gặp lại! 🧋         │");
                Center("╰──────────────────────────╯");
                lines.Add("");
                Center("--- Hết ---");
                lines.Add("");

                var document = new PrintDocument();
                document.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Receipt", 312, 900); // 80mm width
                document.PrintPage += (_, args) =>
                {
                    using var titleFont = new Font("Consolas", 11F, FontStyle.Bold);
                    using var normalFont = new Font("Consolas", 9F);
                    using var headerFont = new Font("Consolas", 10F, FontStyle.Bold);
                    float y = 10;
                    float lineHeight = 16;

                    foreach (var line in lines)
                    {
                        var font = normalFont;
                        if (line.Contains("MILK TEA POS") || line.Contains("HÓA ĐƠN") ||
                            line.Contains("TỔNG THANH TOÁN") || line.Contains("Cảm ơn") ||
                            line.Contains("Hết ---"))
                        {
                            font = headerFont;
                        }

                        if (line.Contains("TỔNG"))
                        {
                            args.Graphics.DrawString(line, titleFont, Brushes.Black, 10, y);
                        }
                        else
                        {
                            args.Graphics.DrawString(line, font, Brushes.Black, 10, y);
                        }
                        y += lineHeight;
                    }
                };

                document.Print();
            }
            catch
            {
                MessageBox.Show("Không thể in hóa đơn. Vui lòng kiểm tra máy in.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private static void LogAuditOrder(string action, Guid orderId, string details)
        {
            try
            {
                var userId = PostgresContext.CurrentUserId;
                if (!userId.HasValue) return;

                var escapedDetails = details.Replace("\"", "\\\"");
                var jsonContent = $"{{\"details\": \"{escapedDetails}\"}}";

                using var context = new PostgresContext();
                context.AuditLogs.Add(new AuditLog
                {
                    Id = Guid.NewGuid(),
                    UserId = userId.Value,
                    Action = action,
                    TableName = "orders",
                    RecordId = orderId,
                    NewValues = jsonContent,
                    IpAddress = PostgresContext.CurrentUserIP,
                    CreatedAt = DateTime.UtcNow
                });
                context.SaveChanges();
            }
            catch { }
        }
    }
}
