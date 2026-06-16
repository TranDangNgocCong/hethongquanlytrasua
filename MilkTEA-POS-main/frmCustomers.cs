using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using MilkTeaPOS.Models;

namespace MilkTeaPOS
{
    public partial class frmCustomers : Form
    {
        private Customer? _selectedCustomer;

        #region Constants

        private const long MAX_FILE_SIZE = 10 * 1024 * 1024; // 10MB
        private static readonly string[] ALLOWED_EXTENSIONS = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
        
        // Cached fonts to avoid GDI leaks
        private readonly Font _fontGenderMale = new Font("Segoe UI", 10F, FontStyle.Bold);
        private readonly Font _fontGenderFemale = new Font("Segoe UI", 10F, FontStyle.Bold);

        #endregion

        #region Constructor & Initialization

        public frmCustomers()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            LoadCustomers();
            InitializeGenderCombo();
        }

        private void InitializeGenderCombo()
        {
            cbGender.Items.Add("Nam");
            cbGender.Items.Add("Nữ");
            cbGender.Items.Add("Khác");
            cbGender.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        #endregion

        #region Data Loading & Display

        private async void LoadCustomers()
        {
            try
            {
                ShowLoading(true);

                using (var context = new PostgresContext())
                {
                    var customers = await context.Customers
                        .OrderBy(c => c.Name)
                        .ToListAsync();

                    dgvCustomers.DataSource = customers.Select(c => new
                    {
                        c.Id,
                        c.Name,
                        c.Phone,
                        c.Email,
                        c.DateOfBirth,
                        Gender = c.Gender == "male" ? "Nam" : c.Gender == "female" ? "Nữ" : c.Gender == "other" ? "Khác" : "",
                        c.Address,
                        c.Notes,
                        c.AvatarUrl,
                        c.CreatedAt,
                        c.UpdatedAt
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
            if (dgvCustomers.Columns.Count == 0) return;

            var columns = dgvCustomers.Columns;

            if (columns["Id"] != null)
            {
                columns["Id"].HeaderText = "ID";
                columns["Id"].Width = 250;
            }

            if (columns["Name"] != null)
            {
                columns["Name"].HeaderText = "Tên khách hàng";
                columns["Name"].Width = 180;
            }

            if (columns["Phone"] != null)
            {
                columns["Phone"].HeaderText = "Số điện thoại";
                columns["Phone"].Width = 120;
            }

            if (columns["Email"] != null)
            {
                columns["Email"].HeaderText = "Email";
                columns["Email"].Width = 200;
            }

            if (columns["DateOfBirth"] != null)
            {
                columns["DateOfBirth"].HeaderText = "Ngày sinh";
                columns["DateOfBirth"].Width = 120;
                columns["DateOfBirth"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }

            if (columns["Gender"] != null)
            {
                columns["Gender"].HeaderText = "Giới tính";
                columns["Gender"].Width = 100;
                columns["Gender"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (columns["Address"] != null)
            {
                columns["Address"].HeaderText = "Địa chỉ";
                columns["Address"].Width = 200;
            }

            if (columns["Notes"] != null)
            {
                columns["Notes"].HeaderText = "Ghi chú";
                columns["Notes"].Width = 200;
            }

            if (columns["AvatarUrl"] != null)
            {
                columns["AvatarUrl"].Visible = false;
            }

            if (columns["CreatedAt"] != null)
            {
                columns["CreatedAt"].HeaderText = "Ngày tạo";
                columns["CreatedAt"].Width = 150;
                columns["CreatedAt"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
            }

            if (columns["UpdatedAt"] != null)
            {
                columns["UpdatedAt"].HeaderText = "Ngày cập nhật";
                columns["UpdatedAt"].Width = 150;
                columns["UpdatedAt"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
            }
        }

        private void dgvCustomers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvCustomers.Columns["Gender"] == null || e.ColumnIndex != dgvCustomers.Columns["Gender"].Index || e.Value == null)
                return;

            var genderText = e.Value.ToString();
            if (genderText == "Nam")
            {
                e.CellStyle.ForeColor = Color.FromArgb(23, 162, 184);
                e.CellStyle.Font = _fontGenderMale;
            }
            else if (genderText == "Nữ")
            {
                e.CellStyle.ForeColor = Color.FromArgb(220, 53, 69);
                e.CellStyle.Font = _fontGenderFemale;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _fontGenderMale?.Dispose();
            _fontGenderFemale?.Dispose();
            base.OnFormClosing(e);
        }

        #endregion

        #region Event Handlers - DataGridView

        private void dgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvCustomers.Rows[e.RowIndex];
            if (row.Cells["Id"].Value == null) return;

            if (!Guid.TryParse(row.Cells["Id"].Value.ToString(), out var customerId)) return;

            _selectedCustomer = new Customer
            {
                Id = customerId,
                Name = row.Cells["Name"].Value?.ToString() ?? string.Empty,
                Phone = row.Cells["Phone"].Value?.ToString(),
                Email = row.Cells["Email"].Value?.ToString(),
                DateOfBirth = row.Cells["DateOfBirth"].Value is DateOnly dob ? dob : null,
                Gender = row.Cells["Gender"].Value?.ToString() switch
                {
                    "Nam" => "male",
                    "Nữ" => "female",
                    "Khác" => "other",
                    _ => null
                },
                Address = row.Cells["Address"].Value?.ToString(),
                Notes = row.Cells["Notes"].Value?.ToString(),
                AvatarUrl = row.Cells["AvatarUrl"].Value?.ToString(),
                CreatedAt = row.Cells["CreatedAt"].Value != null ? Convert.ToDateTime(row.Cells["CreatedAt"].Value) : DateTime.UtcNow,
                UpdatedAt = row.Cells["UpdatedAt"].Value != null ? Convert.ToDateTime(row.Cells["UpdatedAt"].Value) : DateTime.UtcNow
            };

            FillFormData();
        }

        #endregion

        #region Keyboard Navigation

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtPhone.Focus();
            }
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtEmail.Focus();
            }
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                dtpDateOfBirth.Focus();
            }
        }

        private void dtpDateOfBirth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                cbGender.Focus();
            }
        }

        private void cbGender_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtAddress.Focus();
            }
        }

        private void txtAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtNotes.Focus();
            }
        }

        private void txtNotes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                dtpCreatedAt.Focus();
            }
        }

        private void dtpCreatedAt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                dtpUpdatedAt.Focus();
            }
        }

        private void dtpUpdatedAt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                if (_selectedCustomer != null)
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
                string imagesFolder = Path.Combine(GetProjectImagesPath(), "Customers");

                if (!Directory.Exists(imagesFolder))
                {
                    Directory.CreateDirectory(imagesFolder);
                }

                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff");
                string newFileName = $"{Path.GetFileNameWithoutExtension(fileName)}_{timestamp}{extension}";
                string destPath = Path.Combine(imagesFolder, newFileName);

                File.Copy(ofd.FileName, destPath, true);

                string relativePath = Path.Combine("Images", "Customers", newFileName);
                txtAvatarUrl.Text = relativePath;
                LoadImagePreview(destPath);

                if (_selectedCustomer != null)
                {
                    await UpdateCustomerAvatarOnly(relativePath);
                }
            }
        }

        private async Task UpdateCustomerAvatarOnly(string newAvatarUrl)
        {
            if (_selectedCustomer == null) return;

            try
            {
                using (var context = new PostgresContext())
                {
                    var customer = await context.Customers.FindAsync(_selectedCustomer.Id);
                    if (customer != null)
                    {
                        string oldAvatarUrl = customer.AvatarUrl;
                        if (!string.IsNullOrEmpty(oldAvatarUrl) && oldAvatarUrl != newAvatarUrl)
                        {
                            DeleteOldAvatar(oldAvatarUrl);
                        }

                        customer.AvatarUrl = newAvatarUrl;
                        customer.UpdatedAt = DateTime.UtcNow;
                        await context.SaveChangesAsync();

                        _selectedCustomer.AvatarUrl = newAvatarUrl;
                        LoadCustomers();
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
            if (_selectedCustomer == null || dgvCustomers.Rows.Count == 0) return;

            foreach (DataGridViewRow row in dgvCustomers.Rows)
            {
                if (row.Cells["Id"].Value?.ToString() == _selectedCustomer.Id.ToString())
                {
                    dgvCustomers.ClearSelection();
                    row.Selected = true;
                    dgvCustomers.CurrentCell = row.Cells[0];
                    break;
                }
            }
        }

        #endregion

        #region Form Data Management

        private void FillFormData()
        {
            if (_selectedCustomer == null) return;

            txtName.Text = _selectedCustomer.Name;
            txtPhone.Text = _selectedCustomer.Phone;
            txtEmail.Text = _selectedCustomer.Email;
            
            if (_selectedCustomer.DateOfBirth.HasValue)
            {
                dtpDateOfBirth.Value = _selectedCustomer.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue);
            }
            else
            {
                dtpDateOfBirth.Checked = false;
            }

            var genderDisplay = _selectedCustomer.Gender switch
            {
                "male" => "Nam",
                "female" => "Nữ",
                "other" => "Khác",
                _ => ""
            };
            cbGender.Text = genderDisplay;

            txtAddress.Text = _selectedCustomer.Address;
            txtNotes.Text = _selectedCustomer.Notes;
            txtAvatarUrl.Text = _selectedCustomer.AvatarUrl ?? string.Empty;
            
            if (!string.IsNullOrEmpty(_selectedCustomer.AvatarUrl))
            {
                string fullPath = GetFullImagePath(_selectedCustomer.AvatarUrl);
                LoadImagePreview(fullPath);
            }
            else
            {
                picAvatar.Image = null;
            }
            
            dtpCreatedAt.Value = _selectedCustomer.CreatedAt ?? DateTime.UtcNow;
            dtpUpdatedAt.Value = _selectedCustomer.UpdatedAt ?? DateTime.UtcNow;
        }

        private void ClearForm()
        {
            txtName.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            dtpDateOfBirth.Value = DateTime.Now;
            dtpDateOfBirth.Checked = true;
            cbGender.SelectedIndex = -1;
            txtAddress.Clear();
            txtNotes.Clear();
            txtAvatarUrl.Clear();
            picAvatar.Image = null;
            dtpCreatedAt.Value = DateTime.UtcNow;
            dtpUpdatedAt.Value = DateTime.UtcNow;
            _selectedCustomer = null;
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
                    var customers = await context.Customers
                        .AsNoTracking()
                        .Where(c => string.IsNullOrEmpty(searchText) ||
                                   c.Name.ToLower().Contains(searchText) ||
                                   (c.Phone != null && c.Phone.Contains(searchText)) ||
                                   (c.Email != null && c.Email.ToLower().Contains(searchText)))
                        .OrderBy(c => c.Name)
                        .ToListAsync();

                    dgvCustomers.DataSource = customers.Select(c => new
                    {
                        c.Id,
                        c.Name,
                        c.Phone,
                        c.Email,
                        c.DateOfBirth,
                        Gender = c.Gender == "male" ? "Nam" : c.Gender == "female" ? "Nữ" : c.Gender == "other" ? "Khác" : "",
                        c.Address,
                        c.Notes,
                        c.AvatarUrl,
                        c.CreatedAt,
                        c.UpdatedAt
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
            await SaveCustomer();
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            if (_selectedCustomer == null)
            {
                MessageBox.Show("⚠️ Vui lòng chọn khách hàng cần sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            await UpdateCustomer();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedCustomer == null)
            {
                MessageBox.Show("⚠️ Vui lòng chọn khách hàng cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show(
                $"🗑️ Bạn có chắc muốn xóa khách hàng '{_selectedCustomer.Name}'?\n\n⚠️ Hành động này không thể hoàn tác!",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes) return;

            try
            {
                using (var context = new PostgresContext())
                {
                    var customer = await context.Customers.FindAsync(_selectedCustomer.Id);
                    if (customer != null)
                    {
                        context.Customers.Remove(customer);
                        await context.SaveChangesAsync();
                    }
                }

                LogAudit("DELETE", _selectedCustomer.Id, $"Name: {_selectedCustomer.Name}");

                MessageBox.Show("✅ Xóa khách hàng thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadCustomers();
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
            LoadCustomers();
            ClearForm();
        }

        #endregion

        #region Database Operations

        private async Task SaveCustomer()
        {
            var name = txtName.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("⚠️ Vui lòng nhập tên khách hàng!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            // Validate tên khách hàng không quá 100 ký tự (theo DB constraint)
            if (name.Length > 100)
            {
                MessageBox.Show($"⚠️ Tên khách hàng quá dài!\n\nTối đa 100 ký tự, hiện tại: {name.Length} ký tự.",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                txtName.Select(0, 100);
                return;
            }

            // Check duplicate phone
            var phone = txtPhone.Text.Trim();
            if (!string.IsNullOrEmpty(phone))
            {
                // Remove common formatting characters before validation
                phone = phone.Replace(" ", "").Replace("-", "").Replace(".", "");
                txtPhone.Text = phone; // Update UI with cleaned version

                // Validate phone format (Vietnamese: 9-11 digits)
                if (!IsValidPhone(phone))
                {
                    MessageBox.Show("⚠️ Số điện thoại không hợp lệ!\n\nChỉ chấp nhận 9-11 chữ số, bắt đầu bằng số 0 (VD: 0901234567).",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPhone.Focus();
                    txtPhone.SelectAll();
                    return;
                }

                bool phoneExists;
                using (var context = new PostgresContext())
                {
                    phoneExists = await context.Customers
                        .AsNoTracking()
                        .AnyAsync(c => c.Phone == phone);
                }

                if (phoneExists)
                {
                    MessageBox.Show($"⚠️ Số điện thoại '{phone}' đã tồn tại!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPhone.Focus();
                    txtPhone.SelectAll();
                    return;
                }
            }

            // Check duplicate email
            var email = txtEmail.Text.Trim();
            if (!string.IsNullOrEmpty(email))
            {
                // Validate email format with stricter regex
                if (!IsValidEmail(email))
                {
                    MessageBox.Show("⚠️ Email không hợp lệ!\n\nVui lòng nhập đúng định dạng email (VD: example@domain.com).", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    txtEmail.SelectAll();
                    return;
                }

                bool emailExists;
                using (var context = new PostgresContext())
                {
                    emailExists = await context.Customers
                        .AsNoTracking()
                        .AnyAsync(c => c.Email != null && c.Email.ToLower() == email.ToLower());
                }

                if (emailExists)
                {
                    MessageBox.Show($"⚠️ Email '{email}' đã tồn tại!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    txtEmail.SelectAll();
                    return;
                }
            }

            // Validate DateOfBirth if provided
            DateOnly? dob = null;
            if (dtpDateOfBirth.Checked)
            {
                dob = DateOnly.FromDateTime(dtpDateOfBirth.Value);

                // Check if date of birth is reasonable (not in future, not too old)
                if (dob > DateOnly.FromDateTime(DateTime.Now))
                {
                    MessageBox.Show("⚠️ Ngày sinh không thể ở tương lai!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpDateOfBirth.Focus();
                    return;
                }

                if (dob < new DateOnly(1900, 1, 1))
                {
                    MessageBox.Show("⚠️ Ngày sinh không hợp lệ!\n\nNăm sinh phải từ 1900 trở về sau.", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpDateOfBirth.Focus();
                    return;
                }
            }

            // Validate Address length
            var address = txtAddress.Text.Trim();
            if (address.Length > 500)
            {
                MessageBox.Show($"⚠️ Địa chỉ quá dài!\n\nTối đa 500 ký tự, hiện tại: {address.Length} ký tự.",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAddress.Focus();
                txtAddress.Select(0, 500);
                return;
            }

            // Validate Notes length
            var notes = txtNotes.Text.Trim();
            if (notes.Length > 1000)
            {
                MessageBox.Show($"⚠️ Ghi chú quá dài!\n\nTối đa 1000 ký tự, hiện tại: {notes.Length} ký tự.",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNotes.Focus();
                txtNotes.Select(0, 1000);
                return;
            }

            try
            {
                var genderDb = cbGender.Text switch
                {
                    "Nam" => "male",
                    "Nữ" => "female",
                    "Khác" => "other",
                    _ => null
                };

                using (var context = new PostgresContext())
                {
                    var customer = new Customer
                    {
                        Id = Guid.NewGuid(),
                        Name = name,
                        Phone = phone,
                        Email = email,
                        DateOfBirth = dob,
                        Gender = genderDb,
                        Address = txtAddress.Text,
                        Notes = txtNotes.Text,
                        AvatarUrl = txtAvatarUrl.Text,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };

                    context.Customers.Add(customer);
                    await context.SaveChangesAsync();

                    LogAudit("INSERT", customer.Id, $"Name: {customer.Name}, Phone: {customer.Phone ?? "N/A"}");
                }

                MessageBox.Show("✅ Thêm khách hàng thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadCustomers();
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

        private async Task UpdateCustomer()
        {
            var name = txtName.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("⚠️ Vui lòng nhập tên khách hàng!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            // Validate tên khách hàng không quá 100 ký tự
            if (name.Length > 100)
            {
                MessageBox.Show($"⚠️ Tên khách hàng quá dài!\n\nTối đa 100 ký tự, hiện tại: {name.Length} ký tự.",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                txtName.Select(0, 100);
                return;
            }

            // Check duplicate phone (excluding current)
            var phone = txtPhone.Text.Trim();
            if (!string.IsNullOrEmpty(phone))
            {
                // Remove common formatting characters before validation
                phone = phone.Replace(" ", "").Replace("-", "").Replace(".", "");
                txtPhone.Text = phone;

                // Validate phone format (Vietnamese: 9-11 digits)
                if (!IsValidPhone(phone))
                {
                    MessageBox.Show("⚠️ Số điện thoại không hợp lệ!\n\nChỉ chấp nhận 9-11 chữ số, bắt đầu bằng số 0 (VD: 0901234567).",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPhone.Focus();
                    txtPhone.SelectAll();
                    return;
                }

                bool phoneExists;
                using (var context = new PostgresContext())
                {
                    phoneExists = await context.Customers
                        .AsNoTracking()
                        .AnyAsync(c => c.Phone == phone && c.Id != _selectedCustomer!.Id);
                }

                if (phoneExists)
                {
                    MessageBox.Show($"⚠️ Số điện thoại '{phone}' đã tồn tại!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPhone.Focus();
                    txtPhone.SelectAll();
                    return;
                }
            }

            // Check duplicate email (excluding current)
            var email = txtEmail.Text.Trim();
            if (!string.IsNullOrEmpty(email))
            {
                // Validate email format with stricter regex
                if (!IsValidEmail(email))
                {
                    MessageBox.Show("⚠️ Email không hợp lệ!\n\nVui lòng nhập đúng định dạng email (VD: example@domain.com).", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    txtEmail.SelectAll();
                    return;
                }

                bool emailExists;
                using (var context = new PostgresContext())
                {
                    emailExists = await context.Customers
                        .AsNoTracking()
                        .AnyAsync(c => c.Email != null && c.Email.ToLower() == email.ToLower() && c.Id != _selectedCustomer!.Id);
                }

                if (emailExists)
                {
                    MessageBox.Show($"⚠️ Email '{email}' đã tồn tại!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    txtEmail.SelectAll();
                    return;
                }
            }

            // Validate DateOfBirth if provided
            DateOnly? dob = null;
            if (dtpDateOfBirth.Checked)
            {
                dob = DateOnly.FromDateTime(dtpDateOfBirth.Value);

                // Check if date of birth is reasonable
                if (dob > DateOnly.FromDateTime(DateTime.Now))
                {
                    MessageBox.Show("⚠️ Ngày sinh không thể ở tương lai!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpDateOfBirth.Focus();
                    return;
                }

                if (dob < new DateOnly(1900, 1, 1))
                {
                    MessageBox.Show("⚠️ Ngày sinh không hợp lệ!\n\nNăm sinh phải từ 1900 trở về sau.", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpDateOfBirth.Focus();
                    return;
                }
            }

            // Validate Address length
            var address = txtAddress.Text.Trim();
            if (address.Length > 500)
            {
                MessageBox.Show($"⚠️ Địa chỉ quá dài!\n\nTối đa 500 ký tự, hiện tại: {address.Length} ký tự.",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAddress.Focus();
                txtAddress.Select(0, 500);
                return;
            }

            // Validate Notes length
            var notes = txtNotes.Text.Trim();
            if (notes.Length > 1000)
            {
                MessageBox.Show($"⚠️ Ghi chú quá dài!\n\nTối đa 1000 ký tự, hiện tại: {notes.Length} ký tự.",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNotes.Focus();
                txtNotes.Select(0, 1000);
                return;
            }

            try
            {
                var genderDb = cbGender.Text switch
                {
                    "Nam" => "male",
                    "Nữ" => "female",
                    "Khác" => "other",
                    _ => null
                };

                using (var context = new PostgresContext())
                {
                    var customer = await context.Customers.FindAsync(_selectedCustomer!.Id);
                    if (customer != null)
                    {
                        string oldAvatarUrl = customer.AvatarUrl;
                        if (!string.IsNullOrEmpty(oldAvatarUrl) && oldAvatarUrl != txtAvatarUrl.Text)
                        {
                            DeleteOldAvatar(oldAvatarUrl);
                        }

                        customer.Name = name;
                        customer.Phone = phone;
                        customer.Email = email;
                        customer.DateOfBirth = dob;
                        customer.Gender = genderDb;
                        customer.Address = txtAddress.Text;
                        customer.Notes = txtNotes.Text;
                        customer.AvatarUrl = txtAvatarUrl.Text;
                        customer.UpdatedAt = DateTime.UtcNow;

                        await context.SaveChangesAsync();
                    }
                }

                LogAudit("UPDATE", _selectedCustomer.Id, $"Name: {_selectedCustomer.Name}, Phone: {_selectedCustomer.Phone ?? "N/A"}");

                MessageBox.Show("✅ Cập nhật khách hàng thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadCustomers();
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

        #region Helper Methods

        /// <summary>
        /// Validate email format with stricter rules
        /// </summary>
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            // Check length first (practical limit)
            if (email.Length > 254)
                return false;

            // Stricter email regex:
            // - Local part: 1-64 chars, alphanumeric + ._%+-
            // - @ symbol
            // - Domain: 1-255 chars, alphanumeric + hyphens
            // - Must have valid TLD (at least 2 chars)
            var emailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            
            if (!emailRegex.IsMatch(email))
                return false;

            // Additional checks
            if (email.StartsWith(".") || email.EndsWith("."))
                return false;

            if (email.Contains(".."))
                return false;

            if (email.Contains("@@") || email.StartsWith("@") || email.EndsWith("@"))
                return false;

            return true;
        }

        /// <summary>
        /// Validate phone number format (Vietnamese)
        /// </summary>
        private bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            // Remove common formatting
            phone = phone.Replace(" ", "").Replace("-", "").Replace(".", "");

            // Vietnamese phone: 9-11 digits, starting with 0
            return Regex.IsMatch(phone, @"^0[0-9]{8,10}$");
        }

        private void ShowLoading(bool isLoading)
        {
            this.Cursor = isLoading ? Cursors.WaitCursor : Cursors.Default;
            pnlMain.Enabled = !isLoading;
            pnlToolbar.Enabled = !isLoading;
            pnlSearch.Enabled = !isLoading;

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
                    TableName = "customers",
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
