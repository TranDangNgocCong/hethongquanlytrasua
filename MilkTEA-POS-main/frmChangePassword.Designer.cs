namespace MilkTeaPOS
{
    partial class frmChangePassword
    {
        private System.ComponentModel.IContainer components = null;

        // Panels
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlForm;

        // Header
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblUserLabel;

        // Form fields
        private System.Windows.Forms.Label lblCurrentPassword;
        private System.Windows.Forms.TextBox txtCurrentPassword;
        private System.Windows.Forms.Label lblNewPassword;
        private System.Windows.Forms.TextBox txtNewPassword;
        private System.Windows.Forms.Label lblPasswordStrength;
        private System.Windows.Forms.Label lblConfirmPassword;
        private System.Windows.Forms.TextBox txtConfirmPassword;

        // Buttons
        private System.Windows.Forms.Button btnChangePassword;
        private System.Windows.Forms.Button btnClose;

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
            pnlHeader = new Panel();
            lblTitle = new Label();
            lblUserLabel = new Label();
            lblUsername = new Label();
            pnlMain = new Panel();
            pnlForm = new Panel();
            lblCurrentPassword = new Label();
            txtCurrentPassword = new TextBox();
            lblNewPassword = new Label();
            txtNewPassword = new TextBox();
            lblPasswordStrength = new Label();
            lblConfirmPassword = new Label();
            txtConfirmPassword = new TextBox();
            btnChangePassword = new Button();
            btnClose = new Button();
            pnlHeader.SuspendLayout();
            pnlMain.SuspendLayout();
            pnlForm.SuspendLayout();
            SuspendLayout();
            //
            // pnlHeader
            //
            pnlHeader.BackColor = Color.White;
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(lblUserLabel);
            pnlHeader.Controls.Add(lblUsername);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(500, 100);
            pnlHeader.TabIndex = 0;
            //
            // lblTitle
            //
            lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(45, 55, 72);
            lblTitle.Location = new Point(20, 15);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(460, 40);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "🔐 Đổi mật khẩu";
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            //
            // lblUserLabel
            //
            lblUserLabel.Font = new Font("Segoe UI", 11F);
            lblUserLabel.ForeColor = Color.FromArgb(108, 117, 125);
            lblUserLabel.Location = new Point(20, 60);
            lblUserLabel.Name = "lblUserLabel";
            lblUserLabel.Size = new Size(100, 25);
            lblUserLabel.TabIndex = 1;
            lblUserLabel.Text = "Người dùng:";
            lblUserLabel.TextAlign = ContentAlignment.MiddleRight;
            //
            // lblUsername
            //
            lblUsername.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblUsername.ForeColor = Color.FromArgb(45, 55, 72);
            lblUsername.Location = new Point(125, 60);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(200, 25);
            lblUsername.TabIndex = 2;
            lblUsername.Text = "admin";
            lblUsername.TextAlign = ContentAlignment.MiddleLeft;
            //
            // pnlMain
            //
            pnlMain.BackColor = Color.FromArgb(247, 249, 252);
            pnlMain.Controls.Add(pnlForm);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 100);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new Padding(20);
            pnlMain.Size = new Size(500, 400);
            pnlMain.TabIndex = 1;
            //
            // pnlForm
            //
            pnlForm.BackColor = Color.White;
            pnlForm.Controls.Add(lblCurrentPassword);
            pnlForm.Controls.Add(txtCurrentPassword);
            pnlForm.Controls.Add(lblNewPassword);
            pnlForm.Controls.Add(txtNewPassword);
            pnlForm.Controls.Add(lblPasswordStrength);
            pnlForm.Controls.Add(lblConfirmPassword);
            pnlForm.Controls.Add(txtConfirmPassword);
            pnlForm.Controls.Add(btnChangePassword);
            pnlForm.Controls.Add(btnClose);
            pnlForm.Dock = DockStyle.Fill;
            pnlForm.Location = new Point(20, 20);
            pnlForm.Name = "pnlForm";
            pnlForm.Padding = new Padding(30);
            pnlForm.Size = new Size(460, 360);
            pnlForm.TabIndex = 0;
            //
            // lblCurrentPassword
            //
            lblCurrentPassword.Font = new Font("Segoe UI", 11F);
            lblCurrentPassword.ForeColor = Color.FromArgb(45, 55, 72);
            lblCurrentPassword.Location = new Point(30, 40);
            lblCurrentPassword.Name = "lblCurrentPassword";
            lblCurrentPassword.Size = new Size(150, 30);
            lblCurrentPassword.TabIndex = 0;
            lblCurrentPassword.Text = "Mật khẩu hiện tại:";
            lblCurrentPassword.TextAlign = ContentAlignment.MiddleRight;
            //
            // txtCurrentPassword
            //
            txtCurrentPassword.BorderStyle = BorderStyle.FixedSingle;
            txtCurrentPassword.Font = new Font("Segoe UI", 11F);
            txtCurrentPassword.Location = new Point(190, 40);
            txtCurrentPassword.Name = "txtCurrentPassword";
            txtCurrentPassword.Size = new Size(240, 27);
            txtCurrentPassword.TabIndex = 1;
            txtCurrentPassword.KeyPress += txtCurrentPassword_KeyPress;
            //
            // lblNewPassword
            //
            lblNewPassword.Font = new Font("Segoe UI", 11F);
            lblNewPassword.ForeColor = Color.FromArgb(45, 55, 72);
            lblNewPassword.Location = new Point(30, 85);
            lblNewPassword.Name = "lblNewPassword";
            lblNewPassword.Size = new Size(150, 30);
            lblNewPassword.TabIndex = 2;
            lblNewPassword.Text = "Mật khẩu mới:";
            lblNewPassword.TextAlign = ContentAlignment.MiddleRight;
            //
            // txtNewPassword
            //
            txtNewPassword.BorderStyle = BorderStyle.FixedSingle;
            txtNewPassword.Font = new Font("Segoe UI", 11F);
            txtNewPassword.Location = new Point(190, 85);
            txtNewPassword.Name = "txtNewPassword";
            txtNewPassword.Size = new Size(240, 27);
            txtNewPassword.TabIndex = 3;
            txtNewPassword.KeyPress += txtNewPassword_KeyPress;
            //
            // lblPasswordStrength
            //
            lblPasswordStrength.Font = new Font("Segoe UI", 9F);
            lblPasswordStrength.ForeColor = Color.FromArgb(108, 117, 125);
            lblPasswordStrength.Location = new Point(190, 115);
            lblPasswordStrength.Name = "lblPasswordStrength";
            lblPasswordStrength.Size = new Size(240, 20);
            lblPasswordStrength.TabIndex = 4;
            lblPasswordStrength.Text = "Độ mạnh mật khẩu: Yếu";
            //
            // lblConfirmPassword
            //
            lblConfirmPassword.Font = new Font("Segoe UI", 11F);
            lblConfirmPassword.ForeColor = Color.FromArgb(45, 55, 72);
            lblConfirmPassword.Location = new Point(30, 145);
            lblConfirmPassword.Name = "lblConfirmPassword";
            lblConfirmPassword.Size = new Size(150, 30);
            lblConfirmPassword.TabIndex = 5;
            lblConfirmPassword.Text = "Xác nhận mật khẩu:";
            lblConfirmPassword.TextAlign = ContentAlignment.MiddleRight;
            //
            // txtConfirmPassword
            //
            txtConfirmPassword.BorderStyle = BorderStyle.FixedSingle;
            txtConfirmPassword.Font = new Font("Segoe UI", 11F);
            txtConfirmPassword.Location = new Point(190, 145);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.Size = new Size(240, 27);
            txtConfirmPassword.TabIndex = 6;
            txtConfirmPassword.KeyPress += txtConfirmPassword_KeyPress;
            //
            // btnChangePassword
            //
            btnChangePassword.BackColor = Color.FromArgb(72, 187, 120);
            btnChangePassword.Cursor = Cursors.Hand;
            btnChangePassword.FlatAppearance.BorderSize = 0;
            btnChangePassword.FlatStyle = FlatStyle.Flat;
            btnChangePassword.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnChangePassword.ForeColor = Color.White;
            btnChangePassword.Location = new Point(30, 205);
            btnChangePassword.Name = "btnChangePassword";
            btnChangePassword.Size = new Size(190, 45);
            btnChangePassword.TabIndex = 7;
            btnChangePassword.Text = "✅ Đổi mật khẩu";
            btnChangePassword.UseVisualStyleBackColor = false;
            btnChangePassword.Click += btnChangePassword_Click;
            //
            // btnClose
            //
            btnClose.BackColor = Color.FromArgb(108, 117, 125);
            btnClose.Cursor = Cursors.Hand;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(240, 205);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(190, 45);
            btnClose.TabIndex = 8;
            btnClose.Text = "❌ Đóng";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            //
            // frmChangePassword
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(247, 249, 252);
            ClientSize = new Size(500, 500);
            Controls.Add(pnlMain);
            Controls.Add(pnlHeader);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmChangePassword";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "🔐 Đổi mật khẩu - MilkTea POS";
            Load += frmChangePassword_Load;
            pnlHeader.ResumeLayout(false);
            pnlMain.ResumeLayout(false);
            pnlForm.ResumeLayout(false);
            pnlForm.PerformLayout();
            ResumeLayout(false);
        }
    }
}
