using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using MilkTeaPOS.Models;

namespace MilkTeaPOS
{
    public partial class frmLogin : Form
    {
        private User? _loggedInUser;

        public frmLogin()
        {
            InitializeComponent();
            setupDefaults();
        }

        private void setupDefaults()
        {
            // Default username for convenience (password must be entered manually for security)
            txtUsername.Text = "admin";
            txtPassword.Text = string.Empty;
            txtPassword.PlaceholderText = "Mật khẩu";
            txtUsername.Focus();
        }

        private void txtPassword_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (this.IsDisposed) return;

            if (e.KeyChar == (char)Keys.Enter)
            {
                btnLogin_Click(this, EventArgs.Empty);
            }
        }

        private async void btnLogin_Click(object? sender, EventArgs e)
        {
            if (this.IsDisposed) return;

            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            // Validate input
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show(
                    "Vui lòng nhập tên đăng nhập để tiếp tục.\n\n" +
                    "📝 Gợi ý:\n" +
                    "• Tên đăng nhập: admin\n" +
                    "• Mật khẩu: admin123",
                    "⚠️ Thiếu thông tin",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show(
                    "Vui lòng nhập mật khẩu để đăng nhập.\n\n" +
                    "🔒 Nếu bạn quên mật khẩu, vui lòng liên hệ quản trị viên.",
                    "⚠️ Thiếu mật khẩu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            // Disable button during login
            btnLogin.Enabled = false;
            btnLogin.Text = "Đang xác thực...";

            try
            {
                User? user;

                // BEST PRACTICE: Create new DbContext per operation
                using (var context = new PostgresContext())
                {
                    user = await context.Users
                        .Include(u => u.Role)
                        .FirstOrDefaultAsync(u => u.Username == username);

                    // Validate user exists
                    if (user == null)
                    {
                        MessageBox.Show(
                            $"❌ Không tìm thấy tài khoản với tên đăng nhập: \"{username}\"\n\n" +
                            "📋 Vui lòng kiểm tra lại:\n" +
                            "• Tên đăng nhập có đúng chính tả?\n" +
                            "• Tài khoản đã được tạo chưa?\n\n" +
                            "💡 Tài khoản demo: admin / admin123",
                            "❌ Tên đăng nhập không hợp lệ",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        resetLoginButton();
                        return;
                    }

                    // Validate password with BCrypt
                    bool passwordValid = false;
                    
                    // Check if password_hash exists and is a BCrypt hash
                    if (!string.IsNullOrEmpty(user.PasswordHash) && 
                        (user.PasswordHash.StartsWith("$2a$") || user.PasswordHash.StartsWith("$2b$")))
                    {
                        // BCrypt hash detected - use it
                        try
                        {
                            passwordValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
                        }
                        catch (Exception ex)
                        {
                            passwordValid = false;
                        }
                    }
                    // Fallback to plain text password (old data)
                    else if (!string.IsNullOrEmpty(user.Password))
                    {
                        if (user.Password == password)
                        {
                            passwordValid = true;
                            
                            // Auto-migrate to BCrypt
                            string newHash = BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
                            user.PasswordHash = newHash;
                            await context.SaveChangesAsync();
                            
                            MessageBox.Show(
                                "🔐 Mật khẩu đã được nâng cấp lên chuẩn mã hóa BCrypt.\n\n" +
                                "✅ Đăng nhập thành công!",
                                "Nâng cấp bảo mật",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        }
                    }
                    
                    if (!passwordValid)
                    {
                        MessageBox.Show(
                            "🔐 Mật khẩu không đúng!\n\n" +
                            $"👤 Tài khoản: {username}\n" +
                            "⚠️ Bạn đã nhập sai mật khẩu.\n\n" +
                            "💡 Thử lại hoặc nhấn Cancel để thoát.",
                            "🔒 Sai mật khẩu",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        resetLoginButton();
                        return;
                    }

                    // Validate user is active
                    if (user.IsActive == false)
                    {
                        MessageBox.Show(
                            $"🚫 Tài khoản \"{username}\" đã bị khóa!\n\n" +
                            "📞 Liên hệ quản trị viên để được hỗ trợ:\n" +
                            "• Email: support@milkteapos.com\n" +
                            "• Hotline: 1900 xxxx\n\n" +
                            "⏰ Thời gian hỗ trợ: 8:00 - 17:00 (Thứ 2 - Thứ 6)",
                            "🚫 Tài khoản bị khóa",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Stop);
                        resetLoginButton();
                        return;
                    }

                    _loggedInUser = user;

                    // Set global current user for audit logging
                    Models.PostgresContext.CurrentUserId = user.Id;
                }

                // Login successful
                string welcomeMessage = $"✅ Đăng nhập thành công!\n\n";
                welcomeMessage += $"👤 Thông tin tài khoản:\n";
                welcomeMessage += $"   • Tên đăng nhập: {user.Username}\n";
                welcomeMessage += $"   • Vai trò: {user.Role?.Name ?? "Người dùng"}\n";
                welcomeMessage += $"   • Cấp quyền: {(user.Role?.Name ?? "N/A")}\n\n";
                welcomeMessage += $"🎉 Chúc bạn một ngày làm việc hiệu quả!";

                MessageBox.Show(
                    welcomeMessage,
                    "🎉 Đăng nhập thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Open main form
                var frmMain = new frmMain(_loggedInUser);
                frmMain.Show();
                this.Hide();

                // Handle main form closed
                frmMain.FormClosed += (s, args) =>
                {
                    if (!this.IsDisposed && this.IsHandleCreated)
                    {
                        this.Invoke(resetLoginForm);
                    }
                };
            }
            catch (Exception ex)
            {
                // Log error internally for debugging
                System.Diagnostics.Debug.WriteLine($"[LOGIN ERROR] {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"[LOGIN STACK] {ex.StackTrace}");

                if (!this.IsDisposed && this.IsHandleCreated)
                {
                    this.Invoke(() =>
                    {
                        // Show generic error message to user (don't leak DB info)
                        string errorMessage = $"❌ Không thể kết nối cơ sở dữ liệu!\n\n";
                        errorMessage += $"📋 Vui lòng kiểm tra:\n";
                        errorMessage += $"   • Kết nối Internet ổn định\n";
                        errorMessage += $"   • Ứng dụng đang chạy bình thường\n\n";
                        errorMessage += $"💡 Liên hệ bộ phận kỹ thuật nếu lỗi tiếp diễn.\n";
                        errorMessage += $"   • Email: support@milkteapos.com\n";
                        errorMessage += $"   • Hotline: 1900 xxxx";

                        MessageBox.Show(
                            errorMessage,
                            "❌ Lỗi kết nối",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        resetLoginButton();
                    });
                }
            }
        }

        private void btnCancel_Click(object? sender, EventArgs e)
        {
            if (this.IsDisposed) return;

            var result = MessageBox.Show(
                "👋 Bạn có chắc muốn thoát ứng dụng?\n\n" +
                "⏹️ Tất cả các phiên làm việc chưa lưu sẽ bị mất.\n\n" +
                "💡 Nhấn Yes để thoát hoặc No để tiếp tục.",
                "❓ Xác nhận thoát",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void resetLoginButton()
        {
            btnLogin.Enabled = true;
            btnLogin.Text = "Đăng nhập";
        }

        private void resetLoginForm()
        {
            this.Show();
            txtPassword.Clear();
            txtUsername.Focus();
            resetLoginButton();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
        }
    }
}
