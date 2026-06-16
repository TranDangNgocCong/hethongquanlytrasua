using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using MilkTeaPOS.Models;
using MilkTeaPOS.Helpers;

namespace MilkTeaPOS
{
    public partial class frmUsers : Form
    {
        private User? _selectedUser;

        #region Constants

        private const long MAX_FILE_SIZE = 10 * 1024 * 1024; // 10MB
        private static readonly string[] ALLOWED_EXTENSIONS = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
        
        // Cached fonts to avoid GDI leaks
        private readonly Font _fontActiveGreen = new Font("Segoe UI", 11F, FontStyle.Bold);
        private readonly Font _fontActiveRed = new Font("Segoe UI", 11F, FontStyle.Bold);

        #endregion

        #region Constructor & Initialization

        public frmUsers()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            // Load Roles first (Sync)
            using (var context = new PostgresContext())
            {
                LoadRoles(context);
            }

            // Load Users (Async)
            LoadUsers();
        }

        private void LoadRoles(PostgresContext context)
        {
            try
            {
                var roles = context.Roles
                    .AsNoTracking()
                    .OrderBy(r => r.Name)
                    .ToList();

                cbRole.DataSource = roles;
                cbRole.DisplayMember = "Name";
                cbRole.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi tải danh sách vai trò:\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Data Loading & Display

        private async void LoadUsers()
        {
            try
            {
                ShowLoading(true);

                using (var context = new PostgresContext())
                {
                    var users = await context.Users
                        .Include(u => u.Role)
                        .OrderBy(u => u.Username)
                        .ToListAsync();

                    dgvUsers.DataSource = users.Select(u => new
                    {
                        u.Id,
                        u.Username,
                        RoleName = u.Role != null ? u.Role.Name : "Chưa gán",
                        u.AvatarUrl,
                        u.IsActive,
                        u.CreatedAt,
                        u.UpdatedAt
                    }).ToList();
                }

                CustomizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi tải dữ liệu:\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ShowLoading(false);
            }
        }

        private void CustomizeColumns()
        {
            if (dgvUsers.Columns.Count == 0) return;

            var columns = dgvUsers.Columns;

            if (columns["Id"] != null)
            {
                columns["Id"].HeaderText = "ID";
                // columns["Id"].Width = 250; // AutoSizeColumnsMode.Fill handles this
            }

            if (columns["Username"] != null)
            {
                columns["Username"].HeaderText = "Tên đăng nhập";
            }

            if (columns["RoleName"] != null)
            {
                columns["RoleName"].HeaderText = "Vai trò";
            }

            if (columns["IsActive"] != null)
            {
                columns["IsActive"].HeaderText = "Hoạt động";
                columns["IsActive"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (columns["CreatedAt"] != null)
            {
                columns["CreatedAt"].HeaderText = "Ngày tạo";
                columns["CreatedAt"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
            }

            if (columns["UpdatedAt"] != null)
            {
                columns["UpdatedAt"].HeaderText = "Ngày cập nhật";
                columns["UpdatedAt"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
            }

            if (columns["AvatarUrl"] != null)
            {
                columns["AvatarUrl"].Visible = false;
            }
        }

        private void dgvUsers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvUsers.Columns["IsActive"] == null || e.ColumnIndex != dgvUsers.Columns["IsActive"].Index || e.Value == null)
                return;

            if (e.Value is bool isActive)
            {
                e.Value = isActive ? "✓ Đúng" : "✗ Sai";
                e.CellStyle.ForeColor = isActive ? Color.FromArgb(72, 187, 120) : Color.FromArgb(220, 53, 69);
                e.CellStyle.Font = isActive ? _fontActiveGreen : _fontActiveRed;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _fontActiveGreen?.Dispose();
            _fontActiveRed?.Dispose();
            base.OnFormClosing(e);
        }

        #endregion

        #region Event Handlers - DataGridView

        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvUsers.Rows[e.RowIndex];
            if (row.Cells["Id"].Value == null) return;

            _selectedUser = new User
            {
                Id = Guid.Parse(row.Cells["Id"].Value.ToString()),
                Username = row.Cells["Username"].Value?.ToString() ?? string.Empty,
                AvatarUrl = row.Cells["AvatarUrl"].Value?.ToString(),
                IsActive = Convert.ToBoolean(row.Cells["IsActive"].Value ?? true)
            };

            FillFormData();
        }

        #endregion

        #region Image Handling

        private void LoadImagePreview(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
            {
                picAvatar.Image = null;
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
                    picAvatar.Image = Image.FromStream(ms);
                }
                else
                {
                    picAvatar.Image = null;
                }
            }
            catch
            {
                picAvatar.Image = null;
            }
        }

        private string GetProjectPath()
        {
            string projectPath = AppDomain.CurrentDomain.BaseDirectory;
            while (!string.IsNullOrEmpty(projectPath) &&
                   !File.Exists(Path.Combine(projectPath, "MilkTeaPOS.csproj")))
            {
                projectPath = Directory.GetParent(projectPath)?.FullName;
            }
            return projectPath ?? string.Empty;
        }

        private string GetFullImagePath(string relativePath)
        {
            if (string.IsNullOrEmpty(relativePath)) return string.Empty;

            string normalizedPath = relativePath.TrimStart('/', '\\');

            if (relativePath.Contains("..") || normalizedPath.Contains("..") ||
                Path.IsPathRooted(normalizedPath))
            {
                throw new ArgumentException("Invalid path format", nameof(relativePath));
            }

            string fullPath = Path.Combine(GetProjectPath(), normalizedPath);

            string projectPath = GetProjectPath();
            if (!fullPath.StartsWith(projectPath, StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("Path traversal detected", nameof(relativePath));
            }

            return fullPath;
        }

        private string GetProjectImagesPath()
        {
            return Path.Combine(GetProjectPath(), "Images");
        }

        private async void btnBrowseAvatar_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.webp|All Files|*.*",
                Title = "Chọn ảnh đại diện",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var fileInfo = new FileInfo(ofd.FileName);
                if (fileInfo.Length > MAX_FILE_SIZE)
                {
                    MessageBox.Show(
                        $"⚠️ File quá lớn!\n\nKích thước: {fileInfo.Length / 1024 / 1024:.0}MB\nTối đa: {MAX_FILE_SIZE / 1024 / 1024}MB",
                        "Cảnh báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                string extension = Path.GetExtension(ofd.FileName).ToLower();
                if (!ALLOWED_EXTENSIONS.Contains(extension))
                {
                    MessageBox.Show(
                        $"⚠️ Định dạng file không hợp lệ!\n\nChỉ chấp nhận: {string.Join(", ", ALLOWED_EXTENSIONS)}",
                        "Cảnh báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                string fileName = Path.GetFileName(ofd.FileName);
                string imagesFolder = Path.Combine(GetProjectImagesPath(), "Users");

                if (!Directory.Exists(imagesFolder))
                {
                    Directory.CreateDirectory(imagesFolder);
                }

                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff");
                string newFileName = $"{Path.GetFileNameWithoutExtension(fileName)}_{timestamp}{extension}";
                string destPath = Path.Combine(imagesFolder, newFileName);

                File.Copy(ofd.FileName, destPath, true);

                string relativePath = Path.Combine("Images", "Users", newFileName);
                txtAvatarUrl.Text = relativePath;
                LoadImagePreview(destPath);

                if (_selectedUser != null)
                {
                    await UpdateUserAvatarOnly(relativePath);
                }
            }
        }

        private async Task UpdateUserAvatarOnly(string newAvatarUrl)
        {
            if (_selectedUser == null) return;

            try
            {
                using (var context = new PostgresContext())
                {
                    var user = await context.Users.FindAsync(_selectedUser.Id);
                    if (user != null)
                    {
                        string oldAvatarUrl = user.AvatarUrl;
                        if (!string.IsNullOrEmpty(oldAvatarUrl) && oldAvatarUrl != newAvatarUrl)
                        {
                            DeleteOldAvatar(oldAvatarUrl);
                        }

                        user.AvatarUrl = newAvatarUrl;
                        user.UpdatedAt = DateTime.UtcNow;
                        await context.SaveChangesAsync();

                        _selectedUser.AvatarUrl = newAvatarUrl;
                        LoadUsers();
                        ReselectCurrentRow();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"⚠️ Lỗi khi cập nhật ảnh:\n{ex.Message}",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void DeleteOldAvatar(string oldAvatarUrl)
        {
            if (string.IsNullOrEmpty(oldAvatarUrl)) return;

            try
            {
                string fullPath = GetFullImagePath(oldAvatarUrl);
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

        private void ReselectCurrentRow()
        {
            if (_selectedUser == null || dgvUsers.Rows.Count == 0) return;

            foreach (DataGridViewRow row in dgvUsers.Rows)
            {
                if (row.Cells["Id"].Value?.ToString() == _selectedUser.Id.ToString())
                {
                    dgvUsers.ClearSelection();
                    row.Selected = true;
                    dgvUsers.CurrentCell = row.Cells[0];
                    break;
                }
            }
        }

        #endregion

        #region Password Strength Indicator

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            UpdatePasswordStrength(txtPassword.Text);
        }

        private void UpdatePasswordStrength(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                lblPasswordStrength.Text = "Độ mạnh mật khẩu: Yếu";
                lblPasswordStrength.ForeColor = Color.FromArgb(108, 117, 125);
                pbPasswordStrength.Value = 0;
                return;
            }

            int score = 0;

            // Length check (aligned with 8 char minimum)
            if (password.Length >= 8) score++;
            if (password.Length >= 10) score++;
            if (password.Length >= 12) score++;

            // Character variety checks
            if (password.Any(char.IsUpper)) score++;
            if (password.Any(char.IsLower)) score++;
            if (password.Any(char.IsDigit)) score++;
            if (password.Any(c => !char.IsLetterOrDigit(c))) score++;

            // Update UI based on score (0-7)
            if (score <= 3)
            {
                lblPasswordStrength.Text = "Độ mạnh mật khẩu: 🔴 Yếu";
                lblPasswordStrength.ForeColor = Color.FromArgb(220, 53, 69);
                pbPasswordStrength.Value = 25;
                pbPasswordStrength.ForeColor = Color.FromArgb(220, 53, 69);
            }
            else if (score <= 4)
            {
                lblPasswordStrength.Text = "Độ mạnh mật khẩu: 🟡 Trung bình";
                lblPasswordStrength.ForeColor = Color.FromArgb(255, 193, 7);
                pbPasswordStrength.Value = 50;
                pbPasswordStrength.ForeColor = Color.FromArgb(255, 193, 7);
            }
            else if (score <= 5)
            {
                lblPasswordStrength.Text = "Độ mạnh mật khẩu: 🟠 Khá tốt";
                lblPasswordStrength.ForeColor = Color.FromArgb(253, 126, 20);
                pbPasswordStrength.Value = 75;
                pbPasswordStrength.ForeColor = Color.FromArgb(253, 126, 20);
            }
            else
            {
                lblPasswordStrength.Text = "Độ mạnh mật khẩu: 🟢 Mạnh";
                lblPasswordStrength.ForeColor = Color.FromArgb(72, 187, 120);
                pbPasswordStrength.Value = 100;
                pbPasswordStrength.ForeColor = Color.FromArgb(72, 187, 120);
            }
        }

        #endregion

        #region Keyboard Navigation

        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtPassword.Focus();
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtConfirmPassword.Focus();
            }
        }

        private void txtConfirmPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                cbRole.Focus();
            }
        }

        private void cbRole_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                chkIsActive.Focus();
            }
        }

        private void chkIsActive_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                // If editing existing user, save; otherwise add new
                if (_selectedUser != null)
                {
                    btnEdit_Click(sender, e);
                }
                else
                {
                    btnAdd_Click(sender, e);
                }
            }
        }

        #endregion

        #region Form Data Management

        private void FillFormData()
        {
            if (_selectedUser == null) return;

            txtUsername.Text = _selectedUser.Username;
            chkIsActive.Checked = _selectedUser.IsActive ?? true;
            txtAvatarUrl.Text = _selectedUser.AvatarUrl ?? string.Empty;

            if (!string.IsNullOrEmpty(_selectedUser.AvatarUrl))
            {
                string fullPath = GetFullImagePath(_selectedUser.AvatarUrl);
                LoadImagePreview(fullPath);
            }
            else
            {
                picAvatar.Image = null;
            }

            // Set role
            if (cbRole.DataSource != null)
            {
                var roles = (System.Collections.Generic.List<Role>)cbRole.DataSource;
                var role = roles.FirstOrDefault(r => r.Id == _selectedUser.RoleId);
                if (role != null)
                {
                    cbRole.SelectedItem = role;
                }
            }

            // Clear password fields
            txtPassword.Clear();
            txtConfirmPassword.Clear();
        }

        private void ClearForm()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtConfirmPassword.Clear();
            chkIsActive.Checked = true;
            cbRole.SelectedIndex = 0;
            txtAvatarUrl.Clear();
            picAvatar.Image = null;
            _selectedUser = null;
        }

        #endregion

        #region Search Functionality

        private void lblSearch_Click(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                PerformSearch();
            }
        }

        private async void PerformSearch()
        {
            var searchText = txtSearch.Text.Trim().ToLower();

            try
            {
                ShowLoading(true);

                using (var context = new PostgresContext())
                {
                    var users = await context.Users
                        .AsNoTracking()
                        .Include(u => u.Role)
                        .Where(u => string.IsNullOrEmpty(searchText) ||
                                   u.Username.ToLower().Contains(searchText))
                        .OrderBy(u => u.Username)
                        .ToListAsync();

                    dgvUsers.DataSource = users.Select(u => new
                    {
                        u.Id,
                        u.Username,
                        RoleName = u.Role != null ? u.Role.Name : "Chưa gán",
                        u.AvatarUrl,
                        u.IsActive,
                        u.CreatedAt,
                        u.UpdatedAt
                    }).ToList();
                }

                CustomizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi tìm kiếm:\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ShowLoading(false);
            }
        }

        #endregion

        #region Toolbar Actions

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (!PermissionChecker.CanCreate("frmUsers"))
            {
                MessageBox.Show("❌ Bạn không có quyền quản lý người dùng!\n\n" +
                    $"👤 Role hiện tại: {PermissionChecker.GetCurrentRoleName()}\n" +
                    $"🔒 Chỉ Admin mới có thể truy cập.",
                    "🚫 Truy cập bị từ chối",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            await SaveUser();
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            if (!PermissionChecker.CanUpdate("frmUsers"))
            {
                MessageBox.Show("❌ Bạn không có quyền sửa người dùng!\n\n" +
                    $"👤 Role hiện tại: {PermissionChecker.GetCurrentRoleName()}\n" +
                    $"🔒 Chỉ Admin mới có thể truy cập.",
                    "🚫 Truy cập bị từ chối",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (_selectedUser == null)
            {
                MessageBox.Show("⚠️ Vui lòng chọn người dùng cần sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            await UpdateUser();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (!PermissionChecker.CanDelete("frmUsers"))
            {
                MessageBox.Show("❌ Bạn không có quyền xóa người dùng!\n\n" +
                    $"👤 Role hiện tại: {PermissionChecker.GetCurrentRoleName()}\n" +
                    $"🔒 Chỉ Admin mới có thể truy cập.",
                    "🚫 Truy cập bị từ chối",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (_selectedUser == null)
            {
                MessageBox.Show("⚠️ Vui lòng chọn người dùng cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show(
                $"🗑️ Bạn có chắc muốn xóa người dùng '{_selectedUser.Username}'?\n\n⚠️ Hành động này không thể hoàn tác!",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes) return;

            try
            {
                using (var context = new PostgresContext())
                {
                    var user = await context.Users.FindAsync(_selectedUser.Id);
                    if (user != null)
                    {
                        context.Users.Remove(user);
                        await context.SaveChangesAsync();

                        LogAudit("DELETE", _selectedUser.Id, $"Username: {_selectedUser.Username}");
                    }
                }

                MessageBox.Show("✅ Xóa người dùng thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadUsers();
                ClearForm();
            }
            catch (DbUpdateException dbEx)
            {
                string errorMsg = $"❌ Lỗi khi xóa:\n\n{dbEx.Message}";
                if (dbEx.InnerException != null)
                {
                    errorMsg += $"\n\n📋 Chi tiết lỗi:\n{dbEx.InnerException.Message}";
                }
                MessageBox.Show(errorMsg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                string errorMsg = $"❌ Lỗi khi xóa:\n{ex.Message}";
                if (ex.InnerException != null)
                {
                    errorMsg += $"\n\n📋 Chi tiết lỗi:\n{ex.InnerException.Message}";
                }
                MessageBox.Show(errorMsg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadUsers();
            ClearForm();
        }

        #endregion

        #region Database Operations

        private async Task SaveUser()
        {
            var username = txtUsername.Text.Trim();
            var password = txtPassword.Text;
            var confirmPassword = txtConfirmPassword.Text;

            // Validate input
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("⚠️ Vui lòng nhập tên đăng nhập!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("⚠️ Vui lòng nhập mật khẩu!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            if (password.Length < 8)
            {
                MessageBox.Show("⚠️ Mật khẩu phải có ít nhất 8 ký tự!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                txtPassword.SelectAll();
                return;
            }

            // Password complexity validation
            if (!password.Any(char.IsUpper))
            {
                MessageBox.Show("⚠️ Mật khẩu phải có ít nhất 1 chữ HOA (A-Z)!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            if (!password.Any(char.IsLower))
            {
                MessageBox.Show("⚠️ Mật khẩu phải có ít nhất 1 chữ THƯỜNG (a-z)!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            if (!password.Any(char.IsDigit))
            {
                MessageBox.Show("⚠️ Mật khẩu phải có ít nhất 1 số (0-9)!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            if (!password.Any(c => !char.IsLetterOrDigit(c)))
            {
                MessageBox.Show("⚠️ Mật khẩu phải có ít nhất 1 ký tự đặc biệt (!@#$%^&*)!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("⚠️ Mật khẩu xác nhận không khớp!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtConfirmPassword.Focus();
                txtConfirmPassword.SelectAll();
                return;
            }

            // Check duplicate username
            bool exists;
            using (var context = new PostgresContext())
            {
                exists = await context.Users
                    .AsNoTracking()
                    .AnyAsync(u => u.Username.ToLower() == username.ToLower());
            }

            if (exists)
            {
                MessageBox.Show($"⚠️ Tên đăng nhập '{username}' đã tồn tại!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                txtUsername.SelectAll();
                return;
            }

            try
            {
                var selectedRole = cbRole.SelectedItem as Role;

                using (var context = new PostgresContext())
                {
                    var user = new User
                    {
                        Id = Guid.NewGuid(),
                        Username = username,
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12),
                        Password = Guid.NewGuid().ToString(), // Placeholder to satisfy NOT NULL constraint (never store plain text!)
                        RoleId = selectedRole?.Id,
                        AvatarUrl = txtAvatarUrl.Text,
                        IsActive = chkIsActive.Checked,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };

                    context.Users.Add(user);
                    await context.SaveChangesAsync();

                    LogAudit("INSERT", user.Id, $"Username: {user.Username}, RoleId: {user.RoleId}");
                }

                MessageBox.Show("✅ Thêm người dùng thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadUsers();
                ClearForm();
            }
            catch (DbUpdateException dbEx)
            {
                ShowDbError("lưu", dbEx);
            }
            catch (Exception ex)
            {
                ShowError("lưu", ex);
            }
        }

        private async Task UpdateUser()
        {
            var username = txtUsername.Text.Trim();
            var password = txtPassword.Text;
            var confirmPassword = txtConfirmPassword.Text;

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("⚠️ Vui lòng nhập tên đăng nhập!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            // Check duplicate username (excluding current user)
            bool exists;
            using (var context = new PostgresContext())
            {
                exists = await context.Users
                    .AsNoTracking()
                    .AnyAsync(u => u.Username.ToLower() == username.ToLower() && u.Id != _selectedUser!.Id);
            }

            if (exists)
            {
                MessageBox.Show($"⚠️ Tên đăng nhập '{username}' đã tồn tại!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                txtUsername.SelectAll();
                return;
            }

            // Validate password if changing
            if (!string.IsNullOrEmpty(password))
            {
                if (password.Length < 8)
                {
                    MessageBox.Show("⚠️ Mật khẩu phải có ít nhất 8 ký tự!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    txtPassword.SelectAll();
                    return;
                }

                // Password complexity validation
                if (!password.Any(char.IsUpper))
                {
                    MessageBox.Show("⚠️ Mật khẩu phải có ít nhất 1 chữ HOA (A-Z)!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return;
                }

                if (!password.Any(char.IsLower))
                {
                    MessageBox.Show("⚠️ Mật khẩu phải có ít nhất 1 chữ THƯỜNG (a-z)!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return;
                }

                if (!password.Any(char.IsDigit))
                {
                    MessageBox.Show("⚠️ Mật khẩu phải có ít nhất 1 số (0-9)!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return;
                }

                if (!password.Any(c => !char.IsLetterOrDigit(c)))
                {
                    MessageBox.Show("⚠️ Mật khẩu phải có ít nhất 1 ký tự đặc biệt (!@#$%^&*)!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return;
                }

                if (password != confirmPassword)
                {
                    MessageBox.Show("⚠️ Mật khẩu xác nhận không khớp!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtConfirmPassword.Focus();
                    txtConfirmPassword.SelectAll();
                    return;
                }
            }

            try
            {
                using (var context = new PostgresContext())
                {
                    var user = await context.Users.FindAsync(_selectedUser!.Id);
                    if (user != null)
                    {
                        string oldAvatarUrl = user.AvatarUrl;
                        if (!string.IsNullOrEmpty(oldAvatarUrl) && oldAvatarUrl != txtAvatarUrl.Text)
                        {
                            DeleteOldAvatar(oldAvatarUrl);
                        }

                        var selectedRole = cbRole.SelectedItem as Role;

                        user.Username = username;
                        user.RoleId = selectedRole?.Id;
                        user.AvatarUrl = txtAvatarUrl.Text;
                        user.IsActive = chkIsActive.Checked;
                        user.UpdatedAt = DateTime.UtcNow;

                        // Update password if provided
                        if (!string.IsNullOrEmpty(password))
                        {
                            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
                            user.Password = Guid.NewGuid().ToString(); // Placeholder (never store plain text!)
                        }

                        await context.SaveChangesAsync();
                    }
                }

                LogAudit("UPDATE", _selectedUser.Id, $"Username: {_selectedUser.Username}, RoleId: {_selectedUser.RoleId}");

                MessageBox.Show("✅ Cập nhật người dùng thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadUsers();
                ClearForm();
            }
            catch (DbUpdateException dbEx)
            {
                ShowDbError("cập nhật", dbEx);
            }
            catch (Exception ex)
            {
                ShowError("cập nhật", ex);
            }
        }

        #endregion

        #region Audit Logging

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
                    TableName = "users",
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

        #region Helper Methods

        private void ShowLoading(bool isLoading)
        {
            this.Cursor = isLoading ? Cursors.WaitCursor : Cursors.Default;
            pnlMain.Enabled = !isLoading;
            pnlToolbar.Enabled = !isLoading;
            pnlSearch.Enabled = !isLoading;
            
            // Force UI update without DoEvents
            if (isLoading)
            {
                pnlMain.Refresh();
                pnlToolbar.Refresh();
                pnlSearch.Refresh();
            }
        }

        private void ShowDbError(string action, DbUpdateException ex)
        {
            string errorMsg = $"❌ Lỗi khi {action} vào database:\n\n{ex.Message}";
            if (ex.InnerException != null)
            {
                errorMsg += $"\n\n📋 Chi tiết lỗi:\n{ex.InnerException.Message}";
            }
            MessageBox.Show(errorMsg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ShowError(string action, Exception ex)
        {
            string errorMsg = $"❌ Lỗi khi {action}:\n{ex.Message}";
            if (ex.InnerException != null)
            {
                errorMsg += $"\n\n📋 Chi tiết lỗi:\n{ex.InnerException.Message}";
            }
            MessageBox.Show(errorMsg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion
    }
}
