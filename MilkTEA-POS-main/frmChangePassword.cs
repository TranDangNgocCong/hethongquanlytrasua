using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using MilkTeaPOS.Models;

namespace MilkTeaPOS
{
    public partial class frmChangePassword : Form
    {
        private readonly User _loggedInUser;
        private readonly Font _fontWeak = new Font("Segoe UI", 9F);
        private readonly Font _fontMedium = new Font("Segoe UI", 9F);
        private readonly Font _fontGood = new Font("Segoe UI", 9F);
        private readonly Font _fontStrong = new Font("Segoe UI", 9F);

        public frmChangePassword(User loggedInUser)
        {
            InitializeComponent();
            _loggedInUser = loggedInUser;
            InitializeForm();
        }

        private void InitializeForm()
        {
            lblUsername.Text = _loggedInUser.Username;
        }

        #region Event Handlers

        private async void btnChangePassword_Click(object sender, EventArgs e)
        {
            await ChangePassword();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCurrentPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtNewPassword.Focus();
            }
        }

        private void txtNewPassword_KeyPress(object sender, KeyPressEventArgs e)
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
                btnChangePassword_Click(sender, e);
            }
        }

        #endregion

        #region Password Change Logic

        private async Task ChangePassword()
        {
            string currentPassword = txtCurrentPassword.Text;
            string newPassword = txtNewPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            // Validate input
            if (string.IsNullOrEmpty(currentPassword))
            {
                MessageBox.Show(
                    "⚠️ Vui lòng nhập mật khẩu hiện tại!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtCurrentPassword.Focus();
                return;
            }

            if (string.IsNullOrEmpty(newPassword))
            {
                MessageBox.Show(
                    "⚠️ Vui lòng nhập mật khẩu mới!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtNewPassword.Focus();
                return;
            }

            if (newPassword.Length < 8)
            {
                MessageBox.Show(
                    "⚠️ Mật khẩu mới phải có ít nhất 8 ký tự!\n\n" +
                    "💡 Khuyến nghị:\n" +
                    "   • Sử dụng ít nhất 12 ký tự để bảo mật tốt hơn\n" +
                    "   • Kết hợp chữ hoa, chữ thường, số và ký tự đặc biệt",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtNewPassword.Focus();
                txtNewPassword.SelectAll();
                return;
            }

            // Password complexity validation (max score = 7)
            int complexityScore = 0;
            if (newPassword.Length >= 8) complexityScore++;
            if (newPassword.Length >= 10) complexityScore++;
            if (newPassword.Length >= 12) complexityScore++;
            if (newPassword.Any(char.IsUpper)) complexityScore++;
            if (newPassword.Any(char.IsLower)) complexityScore++;
            if (newPassword.Any(char.IsDigit)) complexityScore++;
            if (newPassword.Any(c => !char.IsLetterOrDigit(c))) complexityScore++;

            if (complexityScore < 4)
            {
                MessageBox.Show(
                    "⚠️ Mật khẩu mới quá yếu!\n\n" +
                    "💡 Yêu cầu tối thiểu:\n" +
                    "   • Ít nhất 8 ký tự (khuyến nghị 12+)\n" +
                    "   • Có chữ hoa (A-Z)\n" +
                    "   • Có chữ thường (a-z)\n" +
                    "   • Có số (0-9)\n" +
                    "   • Có ký tự đặc biệt (!@#$%^&*)",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtNewPassword.Focus();
                txtNewPassword.SelectAll();
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show(
                    "⚠️ Mật khẩu xác nhận không khớp!\n\nVui lòng nhập lại.",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                txtConfirmPassword.Focus();
                txtConfirmPassword.SelectAll();
                return;
            }

            try
            {
                // Verify current password
                using (var context = new PostgresContext())
                {
                    var user = await context.Users.FindAsync(_loggedInUser.Id);
                    if (user == null)
                    {
                        MessageBox.Show(
                            "❌ Không tìm thấy người dùng!",
                            "Lỗi",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }

                    // Verify current password with BCrypt
                    if (!BCrypt.Net.BCrypt.Verify(currentPassword, user.PasswordHash))
                    {
                        MessageBox.Show(
                            "❌ Mật khẩu hiện tại không đúng!\n\nVui lòng nhập lại.",
                            "Lỗi",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        txtCurrentPassword.Focus();
                        txtCurrentPassword.SelectAll();
                        return;
                    }

                    // Hash new password with BCrypt (work factor = 12)
                    string newHash = BCrypt.Net.BCrypt.HashPassword(newPassword, workFactor: 12);

                    // Update password hash - ONLY store hashed password, never plain text
                    user.PasswordHash = newHash;
                    user.Password = Guid.NewGuid().ToString(); // Placeholder to satisfy NOT NULL constraint
                    user.UpdatedAt = DateTime.UtcNow;

                    await context.SaveChangesAsync();
                }

                // Audit log for password change
                var currentUserId = PostgresContext.CurrentUserId;
                if (currentUserId.HasValue)
                {
                    LogAudit("PASSWORD_CHANGE", currentUserId.Value, $"User changed their password");
                }

                MessageBox.Show(
                    "✅ Đổi mật khẩu thành công!\n\nMật khẩu mới đã được cập nhật và mã hóa.",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Clear form
                txtCurrentPassword.Clear();
                txtNewPassword.Clear();
                txtConfirmPassword.Clear();
                txtCurrentPassword.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"❌ Lỗi khi đổi mật khẩu:\n\n{ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        #endregion

        #region UI Styling

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            StylePasswordTextBox();
            txtNewPassword.TextChanged += txtNewPassword_TextChanged;
        }

        private void StylePasswordTextBox()
        {
            var textboxes = new[] { txtCurrentPassword, txtNewPassword, txtConfirmPassword };
            foreach (var txt in textboxes)
            {
                txt.BorderStyle = BorderStyle.FixedSingle;
                txt.Font = new Font("Segoe UI", 11F);
                txt.PasswordChar = '●';
                txt.UseSystemPasswordChar = false;
            }
        }

        private void txtNewPassword_TextChanged(object sender, EventArgs e)
        {
            UpdatePasswordStrengthIndicator(txtNewPassword.Text);
        }

        private void UpdatePasswordStrengthIndicator(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                lblPasswordStrength.Text = "Độ mạnh mật khẩu: Yếu";
                lblPasswordStrength.ForeColor = Color.FromArgb(108, 117, 125);
                return;
            }

            int score = 0;
            if (password.Length >= 8) score++;
            if (password.Length >= 10) score++;
            if (password.Length >= 12) score++;
            if (password.Any(char.IsUpper)) score++;
            if (password.Any(char.IsLower)) score++;
            if (password.Any(char.IsDigit)) score++;
            if (password.Any(c => !char.IsLetterOrDigit(c))) score++;

            if (score <= 3)
            {
                lblPasswordStrength.Text = "Độ mạnh mật khẩu: 🔴 Yếu";
                lblPasswordStrength.ForeColor = Color.FromArgb(220, 53, 69);
                lblPasswordStrength.Font = _fontWeak;
            }
            else if (score <= 4)
            {
                lblPasswordStrength.Text = "Độ mạnh mật khẩu: 🟡 Trung bình";
                lblPasswordStrength.ForeColor = Color.FromArgb(255, 193, 7);
                lblPasswordStrength.Font = _fontMedium;
            }
            else if (score <= 5)
            {
                lblPasswordStrength.Text = "Độ mạnh mật khẩu: 🟠 Khá tốt";
                lblPasswordStrength.ForeColor = Color.FromArgb(253, 126, 20);
                lblPasswordStrength.Font = _fontGood;
            }
            else
            {
                lblPasswordStrength.Text = "Độ mạnh mật khẩu: 🟢 Mạnh";
                lblPasswordStrength.ForeColor = Color.FromArgb(72, 187, 120);
                lblPasswordStrength.Font = _fontStrong;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _fontWeak?.Dispose();
            _fontMedium?.Dispose();
            _fontGood?.Dispose();
            _fontStrong?.Dispose();
            base.OnFormClosing(e);
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
    }
}
