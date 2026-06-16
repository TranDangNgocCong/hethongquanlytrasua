using MilkTeaPOS.Models;
using MilkTeaPOS.Services;
using MilkTeaPOS.ViewModels;

namespace MilkTeaPOS
{
    public partial class frmToppings : Form
    {
        private readonly ToppingService _toppingService = new();
        private ToppingViewModel? _selectedTopping;
        private bool _isLoading;

        // Cache fonts để tránh GDI leak
        private static readonly Font _cellCheckFont = new Font("Segoe UI", 11F, FontStyle.Bold);
        private static readonly Font _usageCountFont = new Font("Segoe UI", 10F, FontStyle.Bold | FontStyle.Italic);

        public frmToppings()
        {
            DesignTimeHelper.EnsureConfigured();
            InitializeComponent();
            if (DesignTimeHelper.IsDesignMode) return;
            SetupDataGridView();
            SetupFilterControls();
            _ = InitializeDataAsync();
        }

        private void SetupFilterControls()
        {
            // Initialize status filter
            cboFilterStatus.Items.Clear();
            cboFilterStatus.Items.Add("🔘 Tất cả");
            cboFilterStatus.Items.Add("✅ Đang bán");
            cboFilterStatus.Items.Add("⛔ Ngừng bán");
            cboFilterStatus.SelectedIndex = 0;
        }

        private async Task InitializeDataAsync()
        {
            _isLoading = true;
            try
            {
                await LoadToppingsAsync();
            }
            finally
            {
                _isLoading = false;
            }
        }

        #region Setup

        private void SetupDataGridView()
        {
            dgvToppings.BackgroundColor = Color.White;
            dgvToppings.BorderStyle = BorderStyle.None;
            dgvToppings.RowHeadersVisible = false;
            dgvToppings.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvToppings.MultiSelect = false;
            dgvToppings.AllowUserToAddRows = false;
            dgvToppings.AllowUserToDeleteRows = false;
            dgvToppings.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvToppings.RowTemplate.Height = 50;
            dgvToppings.ReadOnly = true;

            dgvToppings.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            dgvToppings.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(45, 55, 72);
            dgvToppings.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvToppings.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvToppings.ColumnHeadersHeight = 45;

            dgvToppings.DefaultCellStyle.Font = new Font("Segoe UI", 11F);
            dgvToppings.DefaultCellStyle.BackColor = Color.White;
            dgvToppings.DefaultCellStyle.ForeColor = Color.FromArgb(45, 55, 72);
            dgvToppings.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(247, 249, 252);

            dgvToppings.EnableHeadersVisualStyles = false;
            dgvToppings.GridColor = Color.FromArgb(226, 232, 240);
            dgvToppings.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvToppings.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 107, 107);
            dgvToppings.DefaultCellStyle.SelectionForeColor = Color.White;

            // Use manual columns so bool fields render as text (not checkboxes)
            dgvToppings.AutoGenerateColumns = false;
            dgvToppings.Columns.AddRange(
                new DataGridViewTextBoxColumn { Name = "Id", DataPropertyName = "Id", Visible = false },
                new DataGridViewTextBoxColumn { Name = "Name", DataPropertyName = "Name", HeaderText = "Tên topping", FillWeight = 22 },
                new DataGridViewTextBoxColumn { Name = "Description", DataPropertyName = "Description", HeaderText = "Mô tả", FillWeight = 25 },
                new DataGridViewTextBoxColumn
                {
                    Name = "Price",
                    DataPropertyName = "Price",
                    HeaderText = "Giá (VNĐ)",
                    FillWeight = 11,
                    DefaultCellStyle = { Format = "#,##0", Alignment = DataGridViewContentAlignment.MiddleRight }
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "UsageCount",
                    DataPropertyName = "UsageCount",
                    HeaderText = "Lần dùng",
                    FillWeight = 8,
                    DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "IsAvailable",
                    DataPropertyName = "IsAvailable",
                    HeaderText = "Đang bán",
                    FillWeight = 8,
                    DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "CreatedAt",
                    DataPropertyName = "CreatedAt",
                    HeaderText = "Ngày tạo",
                    FillWeight = 12,
                    DefaultCellStyle = { Format = "dd/MM/yyyy HH:mm" }
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "UpdatedAt",
                    DataPropertyName = "UpdatedAt",
                    HeaderText = "Ngày cập nhật",
                    FillWeight = 12,
                    DefaultCellStyle = { Format = "dd/MM/yyyy HH:mm" }
                }
            );

            dgvToppings.CellFormatting += dgvToppings_CellFormatting;
            dgvToppings.DataError += dgvToppings_DataError;
        }

        #endregion

        #region Load Data

        private async Task LoadToppingsAsync()
        {
            try
            {
                var toppings = await _toppingService.GetAllAsync();
                var usageCounts = await _toppingService.GetUsageCountsAsync();

                // Merge usage counts
                foreach (var t in toppings)
                {
                    t.UsageCount = usageCounts.GetValueOrDefault(t.Id, 0);
                }

                BindDataGrid(toppings);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi tải dữ liệu topping:\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindDataGrid(List<ToppingViewModel> toppings)
        {
            dgvToppings.DataSource = toppings;
        }

        private void dgvToppings_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            var columns = dgvToppings.Columns;
            if (e.Value == null) return;

            if (columns["IsAvailable"] != null && e.ColumnIndex == columns["IsAvailable"].Index && e.Value is bool val)
            {
                if (val)
                {
                    e.Value = "✓";
                    e.CellStyle.ForeColor = Color.FromArgb(72, 187, 120);
                    e.CellStyle.Font = _cellCheckFont;
                }
                else
                {
                    e.Value = "✗";
                    e.CellStyle.ForeColor = Color.FromArgb(220, 53, 69);
                    e.CellStyle.Font = _cellCheckFont;
                }
                e.FormattingApplied = true;
            }
        }

        private void dgvToppings_DataError(object? sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        #endregion

        #region DataGridView Events

        private void dgvToppings_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvToppings.Rows[e.RowIndex].DataBoundItem is not ToppingViewModel topping) return;

            _selectedTopping = new ToppingViewModel
            {
                Id = topping.Id,
                Name = topping.Name,
                Description = topping.Description,
                Price = topping.Price,
                IsAvailable = topping.IsAvailable,
                ImageUrl = topping.ImageUrl,
                UsageCount = topping.UsageCount,
                CreatedAt = topping.CreatedAt,
                UpdatedAt = topping.UpdatedAt
            };

            FillFormData();
        }

        private void FillFormData()
        {
            if (_selectedTopping == null) return;

            txtName.Text = _selectedTopping.Name;
            txtDescription.Text = _selectedTopping.Description ?? string.Empty;
            numPrice.Value = _selectedTopping.Price;
            txtImageUrl.Text = _selectedTopping.ImageUrl ?? string.Empty;
            chkIsAvailable.Checked = _selectedTopping.IsAvailable;

            if (!string.IsNullOrEmpty(_selectedTopping.ImageUrl))
            {
                string fullPath = GetFullImagePath(_selectedTopping.ImageUrl);
                LoadImagePreview(fullPath);
            }
            else
            {
                picPreview.Image = null;
            }
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

        private void btnSearch_Click(object? sender, EventArgs e)
        {
            _ = PerformSearchAsync();
        }

        private async Task PerformSearchAsync()
        {
            try
            {
                var keyword = txtSearch.Text.Trim();
                bool? isAvailable = null;

                // Status filter: 0 = Tất cả, 1 = Đang bán, 2 = Ngừng bán
                if (cboFilterStatus.SelectedIndex == 1) isAvailable = true;
                else if (cboFilterStatus.SelectedIndex == 2) isAvailable = false;

                var toppings = await _toppingService.SearchAsync(keyword, isAvailable);
                var usageCounts = await _toppingService.GetUsageCountsAsync();

                // Merge usage counts
                foreach (var t in toppings)
                {
                    t.UsageCount = usageCounts.GetValueOrDefault(t.Id, 0);
                }

                BindDataGrid(toppings);
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
                MessageBox.Show("⚠️ Vui lòng nhập tên topping!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            if (txtName.Text.Trim().Length > 100)
            {
                MessageBox.Show("⚠️ Tên topping không được vượt quá 100 ký tự!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            if (numPrice.Value < 0)
            {
                MessageBox.Show("⚠️ Giá topping phải >= 0!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numPrice.Focus();
                return false;
            }

            if (numPrice.Value > 1000000)
            {
                MessageBox.Show("⚠️ Giá topping không được vượt quá 1,000,000đ!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numPrice.Focus();
                return false;
            }

            return true;
        }

        private async void btnAdd_Click(object? sender, EventArgs e)
        {
            if (!ValidateForm()) return;

            try
            {
                var name = txtName.Text.Trim();

                // Check duplicate name
                var exists = await _toppingService.IsNameExistsAsync(name);
                if (exists)
                {
                    MessageBox.Show($"⚠️ Topping \"{name}\" đã tồn tại!", "Trùng tên",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtName.Focus();
                    return;
                }

                var topping = new Topping
                {
                    Name = name,
                    Description = txtDescription.Text.Trim(),
                    Price = numPrice.Value,
                    ImageUrl = txtImageUrl.Text.Trim(),
                    IsAvailable = chkIsAvailable.Checked
                };

                await _toppingService.AddAsync(topping);

                LogAudit("INSERT", topping.Id, $"Name: {topping.Name}, Price: {topping.Price}");

                MessageBox.Show("✅ Thêm topping thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                await LoadToppingsAsync();
                ClearForm();
            }
            catch (Exception ex)
            {
                string errorMsg = $"❌ Lỗi khi thêm topping:\n{ex.Message}";
                if (ex.InnerException != null)
                    errorMsg += $"\n\n📋 Chi tiết:\n{ex.InnerException.Message}";
                MessageBox.Show(errorMsg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnEdit_Click(object? sender, EventArgs e)
        {
            if (_selectedTopping == null)
            {
                MessageBox.Show("⚠️ Vui lòng chọn topping cần sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateForm()) return;

            try
            {
                string name = txtName.Text.Trim();

                // Check duplicate name (excluding current)
                var exists = await _toppingService.IsNameExistsAsync(name, _selectedTopping.Id);
                if (exists)
                {
                    MessageBox.Show($"⚠️ Topping \"{name}\" đã tồn tại!", "Trùng tên",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtName.Focus();
                    return;
                }

                string description = txtDescription.Text.Trim();
                decimal price = numPrice.Value;
                string imageUrl = txtImageUrl.Text.Trim();
                bool isAvailable = chkIsAvailable.Checked;

                bool updated = await _toppingService.UpdateAsync(_selectedTopping.Id, t =>
                {
                    t.Name = name;
                    t.Description = description;
                    t.Price = price;
                    t.ImageUrl = imageUrl;
                    t.IsAvailable = isAvailable;
                });

                if (!updated)
                {
                    MessageBox.Show("❌ Không tìm thấy topping trong database!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show("✅ Cập nhật topping thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LogAudit("UPDATE", _selectedTopping.Id, $"Name: {name}, Price: {price}");

                await LoadToppingsAsync();
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
            if (_selectedTopping == null)
            {
                MessageBox.Show("⚠️ Vui lòng chọn topping cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var (hasOrders, found) = await _toppingService.CheckBeforeDeleteAsync(_selectedTopping.Id);

                if (!found)
                {
                    MessageBox.Show("❌ Không tìm thấy topping trong database!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (hasOrders)
                {
                    var result = MessageBox.Show(
                        $"⚠️ Topping \"{_selectedTopping.Name}\" đã có trong đơn hàng!\n\n" +
                        "🚫 Không thể xóa cứng topping này.\n\n" +
                        "💡 Bạn có muốn đánh dấu \"Ngừng bán\" (is_available = false) thay vì xóa?",
                        "Không thể xóa",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        await _toppingService.SoftDeleteAsync(_selectedTopping.Id);

                        LogAudit("SOFT_DELETE", _selectedTopping.Id, $"Name: {_selectedTopping.Name} - Ngừng bán");

                        MessageBox.Show("✅ Đã đánh dấu topping ngừng bán!", "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        await LoadToppingsAsync();
                        ClearForm();
                    }
                    return;
                }

                var confirmResult = MessageBox.Show(
                    $"🗑️ Bạn có chắc muốn xóa topping \"{_selectedTopping.Name}\"?\n\n" +
                    "⚠️ Hành động này không thể hoàn tác!",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmResult != DialogResult.Yes) return;

                // Delete image file before deleting topping
                if (!string.IsNullOrEmpty(_selectedTopping.ImageUrl))
                {
                    DeleteOldImage(_selectedTopping.ImageUrl);
                }

                await _toppingService.HardDeleteAsync(_selectedTopping.Id);

                LogAudit("DELETE", _selectedTopping.Id, $"Name: {_selectedTopping.Name}");

                MessageBox.Show("✅ Xóa topping thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                await LoadToppingsAsync();
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
                await LoadToppingsAsync();
                ClearForm();
            }
            finally
            {
                _isLoading = false;
            }
        }

        #endregion

        #region Image Handling

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

        private async void btnBrowseImage_Click(object? sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.webp|All Files|*.*";
            ofd.Title = "Chọn hình ảnh topping";
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var fileInfo = new FileInfo(ofd.FileName);
                    if (fileInfo.Length > 5 * 1024 * 1024)
                    {
                        MessageBox.Show("⚠️ File hình vượt quá 5MB!\n\nVui lòng chọn file nhỏ hơn.",
                            "File quá lớn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string fileName = Path.GetFileName(ofd.FileName);
                    string imagesFolder = Path.Combine(GetProjectImagesPath(), "Toppings");

                    if (!Directory.Exists(imagesFolder))
                    {
                        Directory.CreateDirectory(imagesFolder);
                    }

                    string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff");
                    string extension = Path.GetExtension(fileName);
                    string newFileName = $"{Path.GetFileNameWithoutExtension(fileName)}_{timestamp}{extension}";
                    string destPath = Path.Combine(imagesFolder, newFileName);

                    File.Copy(ofd.FileName, destPath, true);

                    string relativePath = Path.Combine("Images", "Toppings", newFileName);
                    txtImageUrl.Text = relativePath;

                    LoadImagePreview(destPath);

                    // Auto-save image URL if editing existing topping
                    if (_selectedTopping != null && !string.IsNullOrEmpty(relativePath))
                    {
                        // Delete old image before updating
                        string oldImageUrl = _selectedTopping.ImageUrl;
                        if (!string.IsNullOrEmpty(oldImageUrl) && oldImageUrl != relativePath)
                        {
                            DeleteOldImage(oldImageUrl);
                        }

                        await _toppingService.UpdateAsync(_selectedTopping.Id, t =>
                        {
                            t.ImageUrl = relativePath;
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

        #endregion

        #region Helpers

        private void ClearForm()
        {
            txtName.Clear();
            txtDescription.Clear();
            txtImageUrl.Clear();
            numPrice.Value = 0;
            chkIsAvailable.Checked = true;
            picPreview.Image = null;
            _selectedTopping = null;
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
                    TableName = "toppings",
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
