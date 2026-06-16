namespace MilkTeaPOS
{
    partial class frmLogin
    {
        private System.ComponentModel.IContainer components = null;

        // Controls
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlLeft = new Panel();
            lblLogo = new Label();
            lblTitle = new Label();
            lblSubtitle = new Label();
            lblVersion = new Label();
            lblWelcome = new Label();
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            btnLogin = new Button();
            btnCancel = new Button();
            pnlLeft.SuspendLayout();
            SuspendLayout();
            // 
            // pnlLeft
            // 
            pnlLeft.BackColor = Color.FromArgb(255, 107, 107);
            pnlLeft.Controls.Add(lblLogo);
            pnlLeft.Controls.Add(lblTitle);
            pnlLeft.Controls.Add(lblSubtitle);
            pnlLeft.Controls.Add(lblVersion);
            pnlLeft.Dock = DockStyle.Left;
            pnlLeft.Location = new Point(0, 0);
            pnlLeft.Name = "pnlLeft";
            pnlLeft.Size = new Size(450, 600);
            pnlLeft.TabIndex = 5;
            // 
            // lblLogo
            // 
            lblLogo.Font = new Font("Segoe UI", 72F);
            lblLogo.ForeColor = Color.White;
            lblLogo.Location = new Point(125, 100);
            lblLogo.Name = "lblLogo";
            lblLogo.Size = new Size(200, 120);
            lblLogo.TabIndex = 0;
            lblLogo.Text = "\U0001f9cb";
            lblLogo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(75, 240);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(300, 50);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "MilkTea POS";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSubtitle
            // 
            lblSubtitle.Font = new Font("Segoe UI", 12F);
            lblSubtitle.ForeColor = Color.FromArgb(200, 240, 240);
            lblSubtitle.Location = new Point(75, 290);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(300, 30);
            lblSubtitle.TabIndex = 2;
            lblSubtitle.Text = "Hệ thống quản lý trà sữa";
            lblSubtitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblVersion
            // 
            lblVersion.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblVersion.ForeColor = Color.FromArgb(180, 220, 220);
            lblVersion.Location = new Point(75, 550);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(300, 20);
            lblVersion.TabIndex = 3;
            lblVersion.Text = "Version 2026.1";
            lblVersion.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblWelcome
            // 
            lblWelcome.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblWelcome.ForeColor = Color.FromArgb(45, 55, 72);
            lblWelcome.Location = new Point(520, 80);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(320, 35);
            lblWelcome.TabIndex = 4;
            lblWelcome.Text = "Chào mừng trở lại!";
            // 
            // txtUsername
            // 
            txtUsername.BackColor = Color.FromArgb(247, 249, 252);
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            txtUsername.Font = new Font("Segoe UI", 14F);
            txtUsername.ForeColor = Color.FromArgb(45, 55, 72);
            txtUsername.Location = new Point(520, 160);
            txtUsername.Name = "txtUsername";
            txtUsername.PlaceholderText = "Tên đăng nhập";
            txtUsername.Size = new Size(320, 32);
            txtUsername.TabIndex = 1;
            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.FromArgb(247, 249, 252);
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.Font = new Font("Segoe UI", 14F);
            txtPassword.ForeColor = Color.FromArgb(45, 55, 72);
            txtPassword.Location = new Point(520, 230);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '●';
            txtPassword.PlaceholderText = "Mật khẩu";
            txtPassword.Size = new Size(320, 32);
            txtPassword.TabIndex = 2;
            txtPassword.KeyPress += txtPassword_KeyPress;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(255, 107, 107);
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(520, 310);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(320, 50);
            btnLogin.TabIndex = 3;
            btnLogin.Text = "Đăng nhập";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.Transparent;
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
            btnCancel.FlatAppearance.BorderSize = 2;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 12F);
            btnCancel.ForeColor = Color.FromArgb(107, 114, 128);
            btnCancel.Location = new Point(520, 379);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(320, 45);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "Thoát";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // frmLogin
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(247, 249, 252);
            ClientSize = new Size(900, 600);
            Controls.Add(btnCancel);
            Controls.Add(btnLogin);
            Controls.Add(txtPassword);
            Controls.Add(txtUsername);
            Controls.Add(lblWelcome);
            Controls.Add(pnlLeft);
            FormBorderStyle = FormBorderStyle.None;
            MinimumSize = new Size(900, 600);
            Name = "frmLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng nhập - MilkTea POS";
            pnlLeft.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
