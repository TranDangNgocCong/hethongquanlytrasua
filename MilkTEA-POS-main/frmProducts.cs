using MilkTeaPOS.Models;
using MilkTeaPOS.Services;
using MilkTeaPOS.ViewModels;
using MilkTeaPOS.Helpers;
using ZXing;
using ZXing.Common;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.IO;

namespace MilkTeaPOS
{
    public partial class frmProducts : Form
    {
        private readonly ProductService _productService = new();
        private readonly ProductSizeService _sizeService = new();
        private ProductViewModel? _selectedProduct;
        private bool _isLoading;
        private Guid? _loadingProductId; // Guard chống race condition

        // Cache fonts để tránh GDI leak
        private static readonly Font _cellBoldFont = new Font("Segoe UI", 11F, FontStyle.Bold);
        private static readonly Font _cellCheckFont = new Font("Segoe UI", 11F, FontStyle.Bold);

        public frmProducts()
        {
            DesignTimeHelper.EnsureConfigured();
            InitializeComponent();
            if (DesignTimeHelper.IsDesignMode) return;
            SetupKeyboardShortcuts();
            numBasePrice.ValueChanged += NumBasePrice_ValueChanged;
            cboFilterCategory.SelectedIndexChanged += cboFilterCategory_SelectedIndexChanged;
            cboFilterStatus.SelectedIndexChanged += cboFilterStatus_SelectedIndexChanged;
            _ = InitializeDataAsync();
        }

        private void SetupKeyboardShortcuts()
        {
            this.KeyPreview = true;
            this.KeyDown += FrmProducts_KeyDown;
        }

        private void FrmProducts_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.N)
            {
                e.SuppressKeyPress = true;
                txtName.Focus();
            }
            else if (e.Control && e.KeyCode == Keys.S)
            {
                e.SuppressKeyPress = true;
                if (_selectedProduct != null)
                    btnEdit.PerformClick();
                else
                    btnAdd.PerformClick();
            }
            else if (e.Control && e.KeyCode == Keys.Delete)
            {
                e.SuppressKeyPress = true;
                btnDelete.PerformClick();
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                e.SuppressKeyPress = true;
                btnRefresh.PerformClick();
            }
        }

        /// <summary>
        /// Loads categories first (awaited), then products.
        /// The _isLoading flag prevents cboFilterCategory_SelectedIndexChanged
        /// from triggering a concurrent search while categories are being set.
        /// </summary>
        private async Task InitializeDataAsync()
        {
            _isLoading = true;
            try
            {
                await LoadCategoriesAsync();
                await LoadProductsAsync();
            }
            finally
            {
                _isLoading = false;
            }
        }

        #region Setup

        /// <summary>
        /// Auto-fill: S = base - 4k, M = base, L = base + 10k
        /// </summary>
        private void NumBasePrice_ValueChanged(object? sender, EventArgs e)
        {
            var basePrice = numBasePrice.Value;
            if (basePrice > 0)
            {
                numSizeS.Value = Math.Max(0, basePrice - 4000);
                numSizeM.Value = basePrice;
                numSizeL.Value = basePrice + 10000;
            }
        }

        private async Task LoadCategoriesAsync()
        {
            try
            {
                var categories = await _productService.GetCategoriesAsync();

                cboCategory.DataSource = new List<CategoryViewModel>(categories);
                cboCategory.DisplayMember = "Name";
                cboCategory.ValueMember = "Id";
                cboCategory.SelectedIndex = -1;

                var filterList = new List<CategoryViewModel>
                {
                    new() { Id = Guid.Empty, Name = "-- Tất cả --" }
                };
                filterList.AddRange(categories);

                cboFilterCategory.DataSource = filterList;
                cboFilterCategory.DisplayMember = "Name";
                cboFilterCategory.ValueMember = "Id";
                cboFilterCategory.SelectedIndex = 0;

                // Status filter
                cboFilterStatus.Items.Clear();
                cboFilterStatus.Items.Add("🔘 Tất cả");
                cboFilterStatus.Items.Add("✅ Đang bán");
                cboFilterStatus.Items.Add("⛔ Ngừng bán");
                cboFilterStatus.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi tải danh mục:\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Load Data

        private async Task LoadProductsAsync()
        {
            try
            {
                var products = await _productService.GetAllAsync();
                BindDataGrid(products);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi tải dữ liệu sản phẩm:\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindDataGrid(List<ProductViewModel> products)
        {
            dgvProducts.DataSource = products;
        }

        #endregion

        #region DataGridView Events

        private void dgvProducts_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvProducts.Rows[e.RowIndex].DataBoundItem is not ProductViewModel product) return;

            _selectedProduct = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                CategoryId = product.CategoryId,
                CategoryName = product.CategoryName,
                BasePrice = product.BasePrice,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                IsAvailable = product.IsAvailable,
                IsFeatured = product.IsFeatured,
                PreparationTime = product.PreparationTime
            };

            FillFormData();
        }

        private void dgvProducts_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            var columns = dgvProducts.Columns;
            if (e.Value == null) return;

            bool isBoolColumn = (columns["IsAvailable"] != null && e.ColumnIndex == columns["IsAvailable"].Index)
                             || (columns["IsFeatured"] != null && e.ColumnIndex == columns["IsFeatured"].Index);

            if (isBoolColumn && e.Value is bool val)
            {
                if (val)
                {
                    e.Value = "✓ Có";
                    e.CellStyle.ForeColor = Color.FromArgb(72, 187, 120);
                    e.CellStyle.Font = _cellCheckFont;
                }
                else
                {
                    e.Value = "✗ Không";
                    e.CellStyle.ForeColor = Color.FromArgb(220, 53, 69);
                    e.CellStyle.Font = _cellCheckFont;
                }
                e.FormattingApplied = true;
            }
        }

        private void dgvProducts_DataError(object? sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        #endregion

        #region Load Data

        private async void FillFormData()
        {
            if (_selectedProduct == null) return;

            // Guard: nếu đang load product khác, bỏ qua
            if (_loadingProductId.HasValue && _loadingProductId.Value != _selectedProduct.Id)
                return;

            _loadingProductId = _selectedProduct.Id;

            txtName.Text = _selectedProduct.Name;
            cboCategory.SelectedValue = _selectedProduct.CategoryId;
            numBasePrice.Value = _selectedProduct.BasePrice;
            txtDescription.Text = _selectedProduct.Description ?? string.Empty;
            txtImageUrl.Text = _selectedProduct.ImageUrl ?? string.Empty;
            chkIsAvailable.Checked = _selectedProduct.IsAvailable;
            chkIsFeatured.Checked = _selectedProduct.IsFeatured;
            numPreparationTime.Value = _selectedProduct.PreparationTime ?? 5;

            // Load product sizes
            await LoadProductSizesAsync();

            // Reset guard nếu vẫn là product này
            if (_loadingProductId == _selectedProduct.Id)
                _loadingProductId = null;

            // Generate barcode preview
            string barcodeToUse = $"PRD{_selectedProduct.Id.ToString("N").Substring(0, 8).ToUpper()}";
            GenerateBarcodePreview(barcodeToUse);

            if (!string.IsNullOrEmpty(_selectedProduct.ImageUrl))
            {
                string fullPath = GetFullImagePath(_selectedProduct.ImageUrl);
                LoadImagePreview(fullPath);
            }
            else
            {
                picPreview.Image = null;
            }
        }

        private void LoadImagePreview(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
            {
                picPreview.Image = null;
                return;
            }

            try
            {
                if (File.Exists(imageUrl))
                {
                    using var fs = new FileStream(imageUrl, FileMode.Open, FileAccess.Read, FileShare.Read);
                    using var ms = new MemoryStream();
                    fs.CopyTo(ms);
                    ms.Position = 0;
                    picPreview.Image = Image.FromStream(ms);
                }
                else
                {
                    picPreview.Image = null;
                }
            }
            catch
            {
                picPreview.Image = null;
            }
        }

        private void DeleteOldImage(string oldImageUrl)
        {
            if (string.IsNullOrEmpty(oldImageUrl)) return;

            try
            {
                string fullPath = GetFullImagePath(oldImageUrl);
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
            }
            catch
            {
                // Ignore delete errors
            }
        }

        private string GetProjectPath()
        {
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return string.Empty;

            try
            {
                string projectPath = AppDomain.CurrentDomain.BaseDirectory;
                while (!string.IsNullOrEmpty(projectPath) &&
                       !File.Exists(Path.Combine(projectPath, "MilkTeaPOS.csproj")))
                {
                    projectPath = Directory.GetParent(projectPath)?.FullName;
                }
                return projectPath ?? string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        private string GetFullImagePath(string relativePath)
        {
            if (string.IsNullOrEmpty(relativePath)) return string.Empty;
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return string.Empty;

            string normalizedPath = relativePath.TrimStart('/', '\\');

            if (relativePath.Contains("..") || normalizedPath.Contains("..") ||
                Path.IsPathRooted(normalizedPath))
            {
                throw new ArgumentException("Invalid path format", nameof(relativePath));
            }

            string projectPath = GetProjectPath();
            if (string.IsNullOrEmpty(projectPath)) return string.Empty;

            string fullPath = Path.Combine(projectPath, normalizedPath);

            if (!fullPath.StartsWith(projectPath, StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("Path traversal detected", nameof(relativePath));
            }

            return fullPath;
        }

        private string GetProjectImagesPath()
        {
            string projectPath = GetProjectPath();
            if (string.IsNullOrEmpty(projectPath)) return string.Empty;
            return Path.Combine(projectPath, "Images");
        }

        #endregion

        #region Search & Filter

        private void lblSearchLabel_Click(object? sender, EventArgs e)
        {
            _ = PerformSearchAsync();
        }

        private void txtSearch_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                _ = PerformSearchAsync();
            }
        }

        private void cboFilterCategory_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_isLoading) return;
            _ = PerformSearchAsync();
        }

        private void cboFilterStatus_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_isLoading) return;
            _ = PerformSearchAsync();
        }

        private async Task PerformSearchAsync()
        {
            var searchText = txtSearch.Text.Trim();
            Guid? filterCategoryId = null;
            bool? isAvailable = null;

            if (cboFilterCategory.SelectedValue is Guid catId && catId != Guid.Empty)
            {
                filterCategoryId = catId;
            }

            // Status filter: 0 = Tất cả, 1 = Đang bán, 2 = Ngừng bán
            if (cboFilterStatus.SelectedIndex == 1) isAvailable = true;
            else if (cboFilterStatus.SelectedIndex == 2) isAvailable = false;

            try
            {
                var products = await _productService.SearchWithStatusAsync(searchText, filterCategoryId, isAvailable);
                BindDataGrid(products);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi tìm kiếm:\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region CRUD

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("⚠️ Vui lòng nhập tên sản phẩm!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            if (txtName.Text.Trim().Length > 200)
            {
                MessageBox.Show("⚠️ Tên sản phẩm không được vượt quá 200 ký tự!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            if (cboCategory.SelectedIndex < 0 || cboCategory.SelectedValue == null)
            {
                MessageBox.Show("⚠️ Vui lòng chọn danh mục!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboCategory.Focus();
                return false;
            }

            if (numBasePrice.Value <= 0)
            {
                MessageBox.Show("⚠️ Giá gốc phải lớn hơn 0!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numBasePrice.Focus();
                return false;
            }

            // Validate at least one size has a price > 0
            bool hasValidSize = numSizeS.Value > 0 || numSizeM.Value > 0 || numSizeL.Value > 0;
            if (!hasValidSize)
            {
                MessageBox.Show("⚠️ Phải có ít nhất 1 giá size (S/M/L) lớn hơn 0!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numSizeM.Focus();
                return false;
            }

            if (numPreparationTime.Value <= 0)
            {
                MessageBox.Show("⚠️ Thời gian chuẩn bị phải lớn hơn 0!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numPreparationTime.Focus();
                return false;
            }

            return true;
        }

        private async void btnAdd_Click(object? sender, EventArgs e)
        {
            // Check permission
            if (!PermissionChecker.CanCreate("frmProducts"))
            {
                MessageBox.Show("❌ Bạn không có quyền thêm sản phẩm mới!\n\n" +
                    $"👤 Role hiện tại: {PermissionChecker.GetCurrentRoleName()}\n" +
                    $"🔒 Vui lòng liên hệ quản trị viên.",
                    "🚫 Truy cập bị từ chối",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateForm()) return;

            try
            {
                var name = txtName.Text.Trim();
                var categoryId = (Guid)cboCategory.SelectedValue!;

                // Check duplicate name within same category
                var exists = await _productService.IsNameExistsAsync(name, categoryId);
                if (exists)
                {
                    MessageBox.Show($"⚠️ Sản phẩm \"{name}\" đã tồn tại trong danh mục này!", "Trùng tên",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtName.Focus();
                    return;
                }

                var product = new Product
                {
                    Name = name,
                    CategoryId = categoryId,
                    BasePrice = numBasePrice.Value,
                    Description = txtDescription.Text.Trim(),
                    ImageUrl = txtImageUrl.Text.Trim(),
                    IsAvailable = chkIsAvailable.Checked,
                    IsFeatured = chkIsFeatured.Checked,
                    PreparationTime = (int)numPreparationTime.Value
                };

                await _productService.AddAsync(product);

                // Save product sizes for new product
                await _sizeService.SaveSizesForProductAsync(
                    product.Id,
                    numSizeS.Value,
                    numSizeM.Value,
                    numSizeL.Value);

                LogAudit("INSERT", product.Id, $"Name: {product.Name}, BasePrice: {product.BasePrice}");

                MessageBox.Show("✅ Thêm sản phẩm thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                await LoadProductsAsync();
                ClearForm();
            }
            catch (Exception ex)
            {
                string errorMsg = $"❌ Lỗi khi thêm sản phẩm:\n{ex.Message}";
                if (ex.InnerException != null)
                    errorMsg += $"\n\n📋 Chi tiết:\n{ex.InnerException.Message}";
                MessageBox.Show(errorMsg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnEdit_Click(object? sender, EventArgs e)
        {
            // Check permission
            if (!PermissionChecker.CanUpdate("frmProducts"))
            {
                MessageBox.Show("❌ Bạn không có quyền sửa sản phẩm!\n\n" +
                    $"👤 Role hiện tại: {PermissionChecker.GetCurrentRoleName()}\n" +
                    $"🔒 Vui lòng liên hệ quản trị viên.",
                    "🚫 Truy cập bị từ chối",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (_selectedProduct == null)
            {
                MessageBox.Show("⚠️ Vui lòng chọn sản phẩm cần sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateForm()) return;

            try
            {
                string name = txtName.Text.Trim();
                Guid categoryId = (Guid)cboCategory.SelectedValue!;

                // Check duplicate name within same category (excluding current product)
                var exists = await _productService.IsNameExistsAsync(name, categoryId, _selectedProduct.Id);
                if (exists)
                {
                    MessageBox.Show($"⚠️ Sản phẩm \"{name}\" đã tồn tại trong danh mục này!", "Trùng tên",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtName.Focus();
                    return;
                }

                decimal basePrice = numBasePrice.Value;
                string description = txtDescription.Text.Trim();
                string imageUrl = txtImageUrl.Text.Trim();
                bool isAvailable = chkIsAvailable.Checked;
                bool isFeatured = chkIsFeatured.Checked;
                int prepTime = (int)numPreparationTime.Value;

                bool updated = await _productService.UpdateAsync(_selectedProduct.Id, p =>
                {
                    p.Name = name;
                    p.CategoryId = categoryId;
                    p.BasePrice = basePrice;
                    p.Description = description;
                    p.ImageUrl = imageUrl;
                    p.IsAvailable = isAvailable;
                    p.IsFeatured = isFeatured;
                    p.PreparationTime = prepTime;
                });

                if (!updated)
                {
                    MessageBox.Show("❌ Không tìm thấy sản phẩm trong database!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Save product sizes
                await _sizeService.SaveSizesForProductAsync(
                    _selectedProduct.Id,
                    numSizeS.Value,
                    numSizeM.Value,
                    numSizeL.Value);

                LogAudit("UPDATE", _selectedProduct.Id, $"Name: {_selectedProduct.Name}, BasePrice: {_selectedProduct.BasePrice}");

                MessageBox.Show("✅ Cập nhật sản phẩm thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                await LoadProductsAsync();
                ClearForm();
            }
            catch (Exception ex)
            {
                string errorMsg = $"❌ Lỗi khi cập nhật:\n{ex.Message}";
                if (ex.InnerException != null)
                    errorMsg += $"\n\n📋 Chi tiết:\n{ex.InnerException.Message}";
                MessageBox.Show(errorMsg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnDelete_Click(object? sender, EventArgs e)
        {
            // Check permission
            if (!PermissionChecker.CanDelete("frmProducts"))
            {
                MessageBox.Show("❌ Bạn không có quyền xóa sản phẩm!\n\n" +
                    $"👤 Role hiện tại: {PermissionChecker.GetCurrentRoleName()}\n" +
                    $"🔒 Vui lòng liên hệ quản trị viên.",
                    "🚫 Truy cập bị từ chối",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (_selectedProduct == null)
            {
                MessageBox.Show("⚠️ Vui lòng chọn sản phẩm cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var (hasOrders, found) = await _productService.CheckBeforeDeleteAsync(_selectedProduct.Id);

                if (!found)
                {
                    MessageBox.Show("❌ Không tìm thấy sản phẩm trong database!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (hasOrders)
                {
                    var result = MessageBox.Show(
                        $"⚠️ Sản phẩm \"{_selectedProduct.Name}\" đã có trong đơn hàng!\n\n" +
                        "🚫 Không thể xóa cứng sản phẩm này.\n\n" +
                        "💡 Bạn có muốn đánh dấu \"Ngừng bán\" (is_available = false) thay vì xóa?",
                        "Không thể xóa",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        await _productService.SoftDeleteAsync(_selectedProduct.Id);

                        LogAudit("SOFT_DELETE", _selectedProduct.Id, $"Name: {_selectedProduct.Name} - Ngừng bán");

                        MessageBox.Show("✅ Đã đánh dấu sản phẩm ngừng bán!", "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        await LoadProductsAsync();
                        ClearForm();
                    }
                    return;
                }

                var confirmResult = MessageBox.Show(
                    $"🗑️ Bạn có chắc muốn xóa sản phẩm \"{_selectedProduct.Name}\"?\n\n" +
                    "⚠️ Hành động này không thể hoàn tác!",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmResult != DialogResult.Yes) return;

                // Delete image file before deleting product
                if (!string.IsNullOrEmpty(_selectedProduct.ImageUrl))
                {
                    DeleteOldImage(_selectedProduct.ImageUrl);
                }

                await _productService.HardDeleteAsync(_selectedProduct.Id);

                LogAudit("DELETE", _selectedProduct.Id, $"Name: {_selectedProduct.Name}");

                MessageBox.Show("✅ Xóa sản phẩm thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                await LoadProductsAsync();
                ClearForm();
            }
            catch (Exception ex)
            {
                string errorMsg = $"❌ Lỗi khi xóa:\n{ex.Message}";
                if (ex.InnerException != null)
                    errorMsg += $"\n\n📋 Chi tiết:\n{ex.InnerException.Message}";
                MessageBox.Show(errorMsg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnRefresh_Click(object? sender, EventArgs e)
        {
            _isLoading = true;
            try
            {
                txtSearch.Clear();
                cboFilterCategory.SelectedIndex = 0;
                await LoadProductsAsync();
                ClearForm();
            }
            finally
            {
                _isLoading = false;
            }
        }

        #endregion

        #region Barcode

        private void GenerateBarcodePreview(string barcodeText)
        {
            if (string.IsNullOrWhiteSpace(barcodeText))
            {
                picBarcode.Image = null;
                return;
            }

            try
            {
                var writer = new BarcodeWriterPixelData
                {
                    Format = BarcodeFormat.CODE_128,
                    Options = new EncodingOptions
                    {
                        Width = 250,
                        Height = 80,
                        Margin = 10
                    }
                };

                var pixelData = writer.Write(barcodeText);
                
                // Convert pixel data to bitmap
                var bitmap = new Bitmap(pixelData.Width, pixelData.Height);
                for (int y = 0; y < pixelData.Height; y++)
                {
                    for (int x = 0; x < pixelData.Width; x++)
                    {
                        var index = (y * pixelData.Width + x) * 4;
                        var r = pixelData.Pixels[index];
                        var g = pixelData.Pixels[index + 1];
                        var b = pixelData.Pixels[index + 2];
                        bitmap.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                }
                
                picBarcode.Image = bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi tạo barcode:\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                picBarcode.Image = null;
            }
        }

        private void btnPrintBarcode_Click(object? sender, EventArgs e)
        {
            if (_selectedProduct == null)
            {
                MessageBox.Show("⚠️ Vui lòng chọn sản phẩm trước khi in barcode!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string barcodeToPrint = $"PRD{_selectedProduct.Id.ToString("N").Substring(0, 8).ToUpper()}";

            if (picBarcode.Image == null)
            {
                GenerateBarcodePreview(barcodeToPrint);
            }

            ShowPrintDialog(barcodeToPrint);
        }

        private void ShowPrintDialog(string barcodeText)
        {
            using var printDialog = new PrintDialog();
            printDialog.Document = CreatePrintDocument(barcodeText);
            printDialog.AllowSomePages = false;
            printDialog.UseEXDialog = true;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    printDialog.Document.Print();
                    MessageBox.Show("✅ In barcode thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"❌ Lỗi in barcode:\n{ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private PrintDocument CreatePrintDocument(string barcodeText)
        {
            var printDoc = new PrintDocument();
            printDoc.PrintPage += (sender, e) =>
            {
                if (picBarcode.Image == null) return;

                // Label size: 50mm x 30mm (common barcode label)
                int labelWidth = 500; // in hundredths of inch
                int labelHeight = 300;

                // Calculate position to center barcode
                int x = (e.PageBounds.Width - labelWidth) / 2;
                int y = (e.PageBounds.Height - labelHeight) / 2;

                // Draw barcode
                e.Graphics.DrawImage(picBarcode.Image, x, y, labelWidth, labelHeight - 40);

                // Draw product name below barcode
                var font = new Font("Arial", 10, FontStyle.Bold);
                var brush = new SolidBrush(Color.Black);
                var productName = _selectedProduct?.Name ?? string.Empty;
                var textSize = e.Graphics.MeasureString(productName, font);
                var textX = x + (labelWidth - textSize.Width) / 2;
                var textY = y + labelHeight - 40;

                e.Graphics.DrawString(productName, font, brush, textX, textY);

                // Draw barcode number
                var smallFont = new Font("Arial", 8, FontStyle.Regular);
                var barcodeTextSize = e.Graphics.MeasureString(barcodeText, smallFont);
                var barcodeTextX = x + (labelWidth - barcodeTextSize.Width) / 2;
                var barcodeTextY = textY + 20;

                e.Graphics.DrawString(barcodeText, smallFont, brush, barcodeTextX, barcodeTextY);
            };

            return printDoc;
        }

        #endregion

        #region Helpers

        private async Task LoadProductSizesAsync()
        {
            if (_selectedProduct == null) return;

            try
            {
                var sizes = await _sizeService.GetSizesForProductAsync(_selectedProduct.Id);
                numSizeS.Value = sizes.GetValueOrDefault("S", 0m);
                numSizeM.Value = sizes.GetValueOrDefault("M", 0m);
                numSizeL.Value = sizes.GetValueOrDefault("L", 0m);
            }
            catch
            {
                // Silently fail - sizes might not exist yet
                numSizeS.Value = 0m;
                numSizeM.Value = 0m;
                numSizeL.Value = 0m;
            }
        }

        private async Task SaveProductSizesAsync()
        {
            if (_selectedProduct == null) return;

            try
            {
                await _sizeService.SaveSizesForProductAsync(
                    _selectedProduct.Id,
                    numSizeS.Value,
                    numSizeM.Value,
                    numSizeL.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"⚠️ Lỗi lưu giá theo size:\n{ex.Message}", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ClearForm()
        {
            txtName.Clear();
            cboCategory.SelectedIndex = -1;
            numBasePrice.Value = 0;
            numSizeS.Value = 0;
            numSizeM.Value = 0;
            numSizeL.Value = 0;
            txtDescription.Clear();
            txtImageUrl.Clear();
            chkIsAvailable.Checked = true;
            chkIsFeatured.Checked = false;
            numPreparationTime.Value = 5;
            picPreview.Image = null;
            picBarcode.Image = null;
            _selectedProduct = null;
        }

        private async void btnBrowseImage_Click(object? sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.webp|All Files|*.*";
            ofd.Title = "Chọn hình ảnh sản phẩm";
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Check file size (5MB max)
                    var fileInfo = new FileInfo(ofd.FileName);
                    if (fileInfo.Length > 5 * 1024 * 1024)
                    {
                        MessageBox.Show("⚠️ File hình vượt quá 5MB!\n\nVui lòng chọn file nhỏ hơn.",
                            "File quá lớn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string fileName = Path.GetFileName(ofd.FileName);
                    string imagesFolder = Path.Combine(GetProjectImagesPath(), "Products");

                    if (!Directory.Exists(imagesFolder))
                    {
                        Directory.CreateDirectory(imagesFolder);
                    }

                    string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff");
                    string extension = Path.GetExtension(fileName);
                    string newFileName = $"{Path.GetFileNameWithoutExtension(fileName)}_{timestamp}{extension}";
                    string destPath = Path.Combine(imagesFolder, newFileName);

                    File.Copy(ofd.FileName, destPath, true);

                    string relativePath = Path.Combine("Images", "Products", newFileName);
                    txtImageUrl.Text = relativePath;

                    LoadImagePreview(destPath);

                    // Auto-save image URL if editing existing product
                    if (_selectedProduct != null && !string.IsNullOrEmpty(relativePath))
                    {
                        // Delete old image before updating
                        string oldImageUrl = _selectedProduct.ImageUrl;
                        if (!string.IsNullOrEmpty(oldImageUrl) && oldImageUrl != relativePath)
                        {
                            DeleteOldImage(oldImageUrl);
                        }

                        await _productService.UpdateAsync(_selectedProduct.Id, p =>
                        {
                            p.ImageUrl = relativePath;
                        });
                    }
                }
                catch (IOException ioEx)
                {
                    MessageBox.Show($"❌ Lỗi sao chép file:\n{ioEx.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (UnauthorizedAccessException uaEx)
                {
                    MessageBox.Show($"❌ Không có quyền truy cập file:\n{uaEx.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private DateTime _lastAuditTime = DateTime.MinValue;
        private readonly object _auditLock = new object();

        private void LogAudit(string action, Guid recordId, string details)
        {
            lock (_auditLock)
            {
                if ((DateTime.UtcNow - _lastAuditTime).TotalMilliseconds < 500) return;
                _lastAuditTime = DateTime.UtcNow;
            }

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
                    TableName = "products",
                    RecordId = recordId,
                    NewValues = jsonContent,
                    IpAddress = PostgresContext.CurrentUserIP,
                    CreatedAt = DateTime.UtcNow
                });
                context.SaveChanges();
            }
            catch { }
        }

        #endregion
    }
}
