using MilkTeaPOS.Models;
using MilkTeaPOS.Services;
using MilkTeaPOS.ViewModels;

namespace MilkTeaPOS
{
    public partial class frmTables : Form
    {
        private readonly TableService _tableService = new();
        private TableViewModel? _selectedTable;
        private bool _isLoading;
        private System.Windows.Forms.Timer? _refreshTimer;

        // Cache font để tránh GDI leak
        private static readonly Font _cellBoldFont = new Font("Segoe UI", 11F, FontStyle.Bold);

        public frmTables()
        {
            DesignTimeHelper.EnsureConfigured();
            InitializeComponent();
            if (DesignTimeHelper.IsDesignMode) return;
            SetupDataGridView();
            SetupComboBoxes();
            SetupAutoRefresh();
            _ = InitializeDataAsync();
        }

        private void SetupAutoRefresh()
        {
            _refreshTimer = new System.Windows.Forms.Timer();
            _refreshTimer.Interval = 30000; // 30s
            _refreshTimer.Tick += async (s, e) => await LoadTablesAsync();
            _refreshTimer.Start();
        }

        private async Task InitializeDataAsync()
        {
            _isLoading = true;
            try
            {
                await LoadTablesAsync();
            }
            finally
            {
                _isLoading = false;
            }
        }

        #region Setup

        private void SetupDataGridView()
        {
            dgvTables.BackgroundColor = Color.White;
            dgvTables.BorderStyle = BorderStyle.None;
            dgvTables.RowHeadersVisible = false;
            dgvTables.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTables.MultiSelect = false;
            dgvTables.AllowUserToAddRows = false;
            dgvTables.AllowUserToDeleteRows = false;
            dgvTables.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTables.RowTemplate.Height = 50;
            dgvTables.ReadOnly = true;

            dgvTables.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            dgvTables.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(45, 55, 72);
            dgvTables.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvTables.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvTables.ColumnHeadersHeight = 45;

            dgvTables.DefaultCellStyle.Font = new Font("Segoe UI", 11F);
            dgvTables.DefaultCellStyle.BackColor = Color.White;
            dgvTables.DefaultCellStyle.ForeColor = Color.FromArgb(45, 55, 72);
            dgvTables.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(247, 249, 252);

            dgvTables.EnableHeadersVisualStyles = false;
            dgvTables.GridColor = Color.FromArgb(226, 232, 240);
            dgvTables.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvTables.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 107, 107);
            dgvTables.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvTables.AutoGenerateColumns = false;
            dgvTables.Columns.AddRange(
                new DataGridViewTextBoxColumn { Name = "Id", DataPropertyName = "Id", Visible = false },
                new DataGridViewTextBoxColumn { Name = "Name", DataPropertyName = "Name", HeaderText = "Tên bàn", FillWeight = 20 },
                new DataGridViewTextBoxColumn
                {
                    Name = "Status",
                    DataPropertyName = "Status",
                    HeaderText = "Trạng thái",
                    FillWeight = 14,
                    DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "Capacity",
                    DataPropertyName = "Capacity",
                    HeaderText = "Sức chứa",
                    FillWeight = 10,
                    DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
                },
                new DataGridViewTextBoxColumn { Name = "Location", DataPropertyName = "Location", HeaderText = "Khu vực", FillWeight = 14 },
                new DataGridViewTextBoxColumn
                {
                    Name = "CreatedAt",
                    DataPropertyName = "CreatedAt",
                    HeaderText = "Ngày tạo",
                    FillWeight = 14,
                    DefaultCellStyle = { Format = "dd/MM/yyyy HH:mm" }
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "UpdatedAt",
                    DataPropertyName = "UpdatedAt",
                    HeaderText = "Ngày cập nhật",
                    FillWeight = 14,
                    DefaultCellStyle = { Format = "dd/MM/yyyy HH:mm" }
                }
            );

            dgvTables.CellFormatting += dgvTables_CellFormatting;
            dgvTables.DataError += dgvTables_DataError;
            dgvTables.CellDoubleClick += DgvTables_CellDoubleClick;
        }

        private void DgvTables_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvTables.Rows[e.RowIndex].DataBoundItem is not TableViewModel table) return;

            ShowActiveOrders(table);
        }

        private void SetupComboBoxes()
        {
            // Status combo for form input
            cboStatus.Items.Clear();
            cboStatus.Items.Add("available");
            cboStatus.Items.Add("occupied");
            cboStatus.Items.Add("reserved");
            cboStatus.Items.Add("maintenance");
            cboStatus.SelectedIndex = 0;

            // Status filter combo for search
            cboStatusFilter.Items.Clear();
            cboStatusFilter.Items.Add("-- Tất cả --");
            cboStatusFilter.Items.Add("available");
            cboStatusFilter.Items.Add("occupied");
            cboStatusFilter.Items.Add("reserved");
            cboStatusFilter.Items.Add("maintenance");
            cboStatusFilter.SelectedIndex = 0;
            cboStatusFilter.SelectedIndexChanged += (s, e) => _ = PerformSearchAsync();

            // Location filter
            txtLocationFilter.KeyPress += (s, e) => { if (e.KeyChar == (char)Keys.Enter) { e.Handled = true; _ = PerformSearchAsync(); } };
        }

        #endregion

        #region Load Data

        private async Task LoadTablesAsync()
        {
            try
            {
                var tables = await _tableService.GetAllAsync();
                BindDataGrid(tables);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi tải dữ liệu bàn:\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindDataGrid(List<TableViewModel> tables)
        {
            dgvTables.DataSource = tables;
        }

        private void dgvTables_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null) return;

            if (dgvTables.Columns["Status"] != null && e.ColumnIndex == dgvTables.Columns["Status"].Index)
            {
                var status = e.Value?.ToString();
                switch (status)
                {
                    case "available":
                        e.CellStyle.ForeColor = Color.FromArgb(72, 187, 120);
                        break;
                    case "occupied":
                        e.CellStyle.ForeColor = Color.FromArgb(220, 53, 69);
                        break;
                    case "reserved":
                        e.CellStyle.ForeColor = Color.FromArgb(255, 153, 0);
                        break;
                    case "maintenance":
                        e.CellStyle.ForeColor = Color.FromArgb(108, 117, 125);
                        break;
                }
                e.CellStyle.Font = _cellBoldFont;
            }
        }

        private void dgvTables_DataError(object? sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        #endregion

        #region DataGridView Events

        private void dgvTables_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvTables.Rows[e.RowIndex].DataBoundItem is not TableViewModel table) return;

            _selectedTable = new TableViewModel
            {
                Id = table.Id,
                Name = table.Name,
                Status = table.Status,
                Capacity = table.Capacity,
                Location = table.Location,
                ImageUrl = table.ImageUrl,
                CreatedAt = table.CreatedAt,
                UpdatedAt = table.UpdatedAt
            };

            FillFormData();
        }

        private void FillFormData()
        {
            if (_selectedTable == null) return;

            txtTableName.Text = _selectedTable.Name;

            for (int i = 0; i < cboStatus.Items.Count; i++)
            {
                if (cboStatus.Items[i].ToString() == _selectedTable.Status)
                {
                    cboStatus.SelectedIndex = i;
                    break;
                }
            }

            numCapacity.Value = Math.Max(numCapacity.Minimum, Math.Min(numCapacity.Maximum, _selectedTable.Capacity));
            txtLocation.Text = _selectedTable.Location ?? string.Empty;
            txtImageUrl.Text = _selectedTable.ImageUrl ?? string.Empty;

            if (!string.IsNullOrEmpty(_selectedTable.ImageUrl))
            {
                string fullPath = GetFullImagePath(_selectedTable.ImageUrl);
                LoadImagePreview(fullPath);
            }
            else
            {
                picPreview.Image = null;
            }
        }

        #endregion

        #region Search & Filter

        private void lblSearch_Click(object? sender, EventArgs e)
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
                var status = cboStatusFilter.SelectedIndex > 0
                    ? cboStatusFilter.SelectedItem?.ToString()
                    : null;
                var location = txtLocationFilter.Text.Trim();

                var tables = await _tableService.SearchAsync(
                    string.IsNullOrWhiteSpace(keyword) ? null : keyword,
                    status,
                    string.IsNullOrWhiteSpace(location) ? null : location);

                BindDataGrid(tables);
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
            if (string.IsNullOrWhiteSpace(txtTableName.Text))
            {
                MessageBox.Show("⚠️ Vui lòng nhập tên bàn!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTableName.Focus();
                return false;
            }

            if (txtTableName.Text.Trim().Length > 50)
            {
                MessageBox.Show("⚠️ Tên bàn không được vượt quá 50 ký tự!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTableName.Focus();
                return false;
            }

            if (numCapacity.Value < 1)
            {
                MessageBox.Show("⚠️ Sức chứa phải >= 1!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numCapacity.Focus();
                return false;
            }

            if (numCapacity.Value > 50)
            {
                MessageBox.Show("⚠️ Sức chứa không được vượt quá 50!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numCapacity.Focus();
                return false;
            }

            if (cboStatus.SelectedIndex < 0)
            {
                MessageBox.Show("⚠️ Vui lòng chọn trạng thái!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboStatus.Focus();
                return false;
            }

            return true;
        }

        private async void btnAdd_Click(object? sender, EventArgs e)
        {
            if (!ValidateForm()) return;

            try
            {
                var name = txtTableName.Text.Trim();

                // Check duplicate name
                var exists = await _tableService.IsNameExistsAsync(name);
                if (exists)
                {
                    MessageBox.Show($"⚠️ Tên bàn \"{name}\" đã tồn tại!", "Trùng tên",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTableName.Focus();
                    return;
                }

                var statusStr = cboStatus.SelectedItem?.ToString() ?? "available";

                var table = new Table
                {
                    Name = name,
                    Status = statusStr,
                    Capacity = (int)numCapacity.Value,
                    Location = txtLocation.Text.Trim(),
                    ImageUrl = txtImageUrl.Text.Trim()
                };

                await _tableService.AddAsync(table);

                LogAudit("INSERT", table.Id, $"Name: {table.Name}, Status: {table.Status}, Capacity: {table.Capacity}");

                MessageBox.Show("✅ Thêm bàn thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                await LoadTablesAsync();
                ClearForm();
            }
            catch (Exception ex)
            {
                string errorMsg = $"❌ Lỗi khi thêm bàn:\n{ex.Message}";
                if (ex.InnerException != null)
                    errorMsg += $"\n\n📋 Chi tiết:\n{ex.InnerException.Message}";
                MessageBox.Show(errorMsg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnEdit_Click(object? sender, EventArgs e)
        {
            if (_selectedTable == null)
            {
                MessageBox.Show("⚠️ Vui lòng chọn bàn cần sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateForm()) return;

            try
            {
                var name = txtTableName.Text.Trim();

                // Check duplicate name (excluding current)
                var exists = await _tableService.IsNameExistsAsync(name, _selectedTable.Id);
                if (exists)
                {
                    MessageBox.Show($"⚠️ Tên bàn \"{name}\" đã tồn tại!", "Trùng tên",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTableName.Focus();
                    return;
                }

                var statusStr = cboStatus.SelectedItem?.ToString() ?? "available";
                int capacity = (int)numCapacity.Value;
                string location = txtLocation.Text.Trim();
                string imageUrl = txtImageUrl.Text.Trim();

                bool updated = await _tableService.UpdateAsync(_selectedTable.Id, t =>
                {
                    t.Name = name;
                    t.Status = statusStr;
                    t.Capacity = capacity;
                    t.Location = location;
                    t.ImageUrl = imageUrl;
                });

                if (!updated)
                {
                    MessageBox.Show("❌ Không tìm thấy bàn trong database!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show("✅ Cập nhật bàn thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LogAudit("UPDATE", _selectedTable.Id, $"Name: {name}, Status: {statusStr}, Capacity: {capacity}");

                await LoadTablesAsync();
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
            if (_selectedTable == null)
            {
                MessageBox.Show("⚠️ Vui lòng chọn bàn cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var (hasOrders, found) = await _tableService.CheckBeforeDeleteAsync(_selectedTable.Id);

                if (!found)
                {
                    MessageBox.Show("❌ Không tìm thấy bàn trong database!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (hasOrders)
                {
                    MessageBox.Show(
                        $"⚠️ Bàn \"{_selectedTable.Name}\" đã phát sinh đơn hàng!\n\n" +
                        "🚫 Không thể xóa bàn này vì đã có dữ liệu đơn hàng liên quan.\n\n" +
                        "💡 Hãy xóa hoặc chuyển các đơn hàng trước khi xóa bàn.",
                        "Không thể xóa",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                var confirmResult = MessageBox.Show(
                    $"🗑️ Bạn có chắc muốn xóa bàn \"{_selectedTable.Name}\"?\n\n" +
                    "⚠️ Hành động này không thể hoàn tác!",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmResult != DialogResult.Yes) return;

                // Delete image file before deleting table
                if (!string.IsNullOrEmpty(_selectedTable.ImageUrl))
                {
                    DeleteOldImage(_selectedTable.ImageUrl);
                }

                await _tableService.HardDeleteAsync(_selectedTable.Id);

                LogAudit("DELETE", _selectedTable.Id, $"Name: {_selectedTable.Name}");

                MessageBox.Show("✅ Xóa bàn thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                await LoadTablesAsync();
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
                txtLocationFilter.Clear();
                cboStatusFilter.SelectedIndex = 0;
                await LoadTablesAsync();
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
            ofd.Title = "Chọn hình ảnh bàn";
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
                    string imagesFolder = Path.Combine(GetProjectImagesPath(), "Tables");

                    if (!Directory.Exists(imagesFolder))
                    {
                        Directory.CreateDirectory(imagesFolder);
                    }

                    string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff");
                    string extension = Path.GetExtension(fileName);
                    string newFileName = $"{Path.GetFileNameWithoutExtension(fileName)}_{timestamp}{extension}";
                    string destPath = Path.Combine(imagesFolder, newFileName);

                    File.Copy(ofd.FileName, destPath, true);

                    string relativePath = Path.Combine("Images", "Tables", newFileName);
                    txtImageUrl.Text = relativePath;

                    LoadImagePreview(destPath);

                    // Auto-save image URL if editing existing table
                    if (_selectedTable != null && !string.IsNullOrEmpty(relativePath))
                    {
                        // Delete old image before updating
                        string oldImageUrl = _selectedTable.ImageUrl;
                        if (!string.IsNullOrEmpty(oldImageUrl) && oldImageUrl != relativePath)
                        {
                            DeleteOldImage(oldImageUrl);
                        }

                        await _tableService.UpdateAsync(_selectedTable.Id, t =>
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
            txtTableName.Clear();
            cboStatus.SelectedIndex = 0;
            numCapacity.Value = 2;
            txtLocation.Clear();
            txtImageUrl.Clear();
            picPreview.Image = null;
            _selectedTable = null;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _refreshTimer?.Stop();
            _refreshTimer?.Dispose();
            base.OnFormClosing(e);
        }

        #endregion

        #region Active Orders

        private async void ShowActiveOrders(TableViewModel table)
        {
            if (table.Status != "occupied" && table.Status != "reserved")
                return;

            try
            {
                var orders = await _tableService.GetActiveOrdersAsync(table.Id);

                if (orders.Count == 0)
                {
                    MessageBox.Show(
                        $"📋 Bàn \"{table.Name}\" không có đơn hàng nào đang hoạt động.\n\n" +
                        $"⚠️ Trạng thái hiện tại: {table.Status}",
                        "📋 Thông tin bàn",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                var message = $"📋 Đơn hàng tại bàn \"{table.Name}\":\n\n";
                foreach (var (orderNum, customer, itemCount, total) in orders)
                {
                    message += $"🧾 {orderNum}\n";
                    message += $"   👤 {customer}\n";
                    message += $"   🍹 {itemCount} món\n";
                    message += $"   💰 {FormatCurrency(total)}\n\n";
                }

                MessageBox.Show(message.Trim(), "📋 Đơn hàng đang hoạt động",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi tải đơn hàng:\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string FormatCurrency(decimal amount) => amount.ToString("#,##0") + "đ";

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

                // Escape quotes for JSON
                var escapedDetails = details.Replace("\"", "\\\"");
                var jsonContent = $"{{\"details\": \"{escapedDetails}\"}}";

                using var context = new PostgresContext();
                var log = new AuditLog
                {
                    Id = Guid.NewGuid(),
                    UserId = userId.Value,
                    Action = action,
                    TableName = "tables",
                    RecordId = recordId,
                    NewValues = jsonContent,
                    IpAddress = PostgresContext.CurrentUserIP,
                    CreatedAt = DateTime.UtcNow
                };
                context.AuditLogs.Add(log);
                context.SaveChanges();
            }
            catch { }
        }

        #endregion
    }
}
