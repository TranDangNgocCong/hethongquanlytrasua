namespace MilkTeaPOS
{
    partial class frmUsers
    {
        private System.ComponentModel.IContainer components = null;

        // Panels
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlForm;

        // Header
        private System.Windows.Forms.Label lblTitle;

        // Toolbar buttons
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnRefresh;

        // Search
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;

        // DataGridView
        private System.Windows.Forms.DataGridView dgvUsers;

        // Form fields
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPasswordStrength;
        private System.Windows.Forms.ProgressBar pbPasswordStrength;
        private System.Windows.Forms.Label lblConfirmPassword;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.ComboBox cbRole;
        private System.Windows.Forms.Label lblIsActive;
        private System.Windows.Forms.CheckBox chkIsActive;
        private System.Windows.Forms.Label lblActiveText;
        private System.Windows.Forms.Label lblAvatar;
        private System.Windows.Forms.TextBox txtAvatarUrl;
        private System.Windows.Forms.Button btnBrowseAvatar;
        private System.Windows.Forms.PictureBox picAvatar;

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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            pnlHeader = new Panel();
            lblTitle = new Label();
            pnlToolbar = new Panel();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            btnRefresh = new Button();
            pnlSearch = new Panel();
            lblSearch = new Label();
            txtSearch = new TextBox();
            pnlMain = new Panel();
            dgvUsers = new DataGridView();
            pnlForm = new Panel();
            lblUsername = new Label();
            txtUsername = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            lblPasswordStrength = new Label();
            pbPasswordStrength = new ProgressBar();
            lblConfirmPassword = new Label();
            txtConfirmPassword = new TextBox();
            lblRole = new Label();
            cbRole = new ComboBox();
            lblIsActive = new Label();
            chkIsActive = new CheckBox();
            lblActiveText = new Label();
            lblAvatar = new Label();
            txtAvatarUrl = new TextBox();
            btnBrowseAvatar = new Button();
            picAvatar = new PictureBox();
            pnlHeader.SuspendLayout();
            pnlToolbar.SuspendLayout();
            pnlSearch.SuspendLayout();
            pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            pnlForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picAvatar).BeginInit();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.White;
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1198, 80);
            pnlHeader.TabIndex = 3;
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(45, 55, 72);
            lblTitle.Location = new Point(20, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(450, 40);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "👨‍💼 Quản lý người dùng";
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlToolbar
            // 
            pnlToolbar.BackColor = Color.White;
            pnlToolbar.Controls.Add(btnAdd);
            pnlToolbar.Controls.Add(btnEdit);
            pnlToolbar.Controls.Add(btnDelete);
            pnlToolbar.Controls.Add(btnRefresh);
            pnlToolbar.Dock = DockStyle.Top;
            pnlToolbar.Location = new Point(0, 80);
            pnlToolbar.Name = "pnlToolbar";
            pnlToolbar.Size = new Size(1198, 60);
            pnlToolbar.TabIndex = 2;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.FromArgb(72, 187, 120);
            btnAdd.Cursor = Cursors.Hand;
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(20, 10);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(110, 40);
            btnAdd.TabIndex = 0;
            btnAdd.Text = "➕ Thêm";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.BackColor = Color.FromArgb(255, 193, 7);
            btnEdit.Cursor = Cursors.Hand;
            btnEdit.FlatAppearance.BorderSize = 0;
            btnEdit.FlatStyle = FlatStyle.Flat;
            btnEdit.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnEdit.ForeColor = Color.White;
            btnEdit.Location = new Point(140, 10);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(110, 40);
            btnEdit.TabIndex = 1;
            btnEdit.Text = "✏️ Sửa";
            btnEdit.UseVisualStyleBackColor = false;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.FromArgb(220, 53, 69);
            btnDelete.Cursor = Cursors.Hand;
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnDelete.ForeColor = Color.White;
            btnDelete.Location = new Point(260, 10);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(110, 40);
            btnDelete.TabIndex = 2;
            btnDelete.Text = "🗑️ Xóa";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.FromArgb(23, 162, 184);
            btnRefresh.Cursor = Cursors.Hand;
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(380, 10);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(110, 40);
            btnRefresh.TabIndex = 3;
            btnRefresh.Text = "🔄 Làm mới";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // pnlSearch
            // 
            pnlSearch.BackColor = Color.White;
            pnlSearch.Controls.Add(lblSearch);
            pnlSearch.Controls.Add(txtSearch);
            pnlSearch.Dock = DockStyle.Top;
            pnlSearch.Location = new Point(0, 140);
            pnlSearch.Name = "pnlSearch";
            pnlSearch.Size = new Size(1198, 60);
            pnlSearch.TabIndex = 1;
            // 
            // lblSearch
            // 
            lblSearch.Cursor = Cursors.Hand;
            lblSearch.Font = new Font("Segoe UI", 12F);
            lblSearch.ForeColor = Color.FromArgb(45, 55, 72);
            lblSearch.Location = new Point(54, 20);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(70, 21);
            lblSearch.TabIndex = 0;
            lblSearch.Text = "🔍 Tìm kiếm:";
            lblSearch.TextAlign = ContentAlignment.MiddleRight;
            lblSearch.Click += lblSearch_Click;
            // 
            // txtSearch
            // 
            txtSearch.BackColor = Color.FromArgb(247, 249, 252);
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.Font = new Font("Segoe UI", 12F);
            txtSearch.Location = new Point(130, 15);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(300, 29);
            txtSearch.TabIndex = 1;
            txtSearch.KeyPress += txtSearch_KeyPress;
            // 
            // pnlMain
            // 
            pnlMain.BackColor = Color.FromArgb(247, 249, 252);
            pnlMain.Controls.Add(dgvUsers);
            pnlMain.Controls.Add(pnlForm);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 200);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new Padding(20);
            pnlMain.Size = new Size(1198, 698);
            pnlMain.TabIndex = 0;
            //
            // dgvUsers
            //
            dgvUsers.AllowUserToAddRows = false;
            dgvUsers.AllowUserToDeleteRows = false;
            dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsers.BackgroundColor = Color.White;
            dgvUsers.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(45, 55, 72);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(45, 55, 72);
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvUsers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvUsers.ColumnHeadersHeight = 45;
            dgvUsers.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvUsers.GridColor = Color.FromArgb(226, 232, 240);
            dgvUsers.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 11F);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(45, 55, 72);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(255, 107, 107);
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvUsers.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(247, 249, 252);
            dgvUsers.Dock = DockStyle.Fill;
            dgvUsers.EnableHeadersVisualStyles = false;
            dgvUsers.Location = new Point(20, 20);
            dgvUsers.MultiSelect = false;
            dgvUsers.Name = "dgvUsers";
            dgvUsers.RowHeadersVisible = false;
            dgvUsers.RowTemplate.Height = 50;
            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.Size = new Size(1158, 300);
            dgvUsers.TabIndex = 0;
            dgvUsers.CellClick += dgvUsers_CellClick;
            dgvUsers.CellFormatting += dgvUsers_CellFormatting;
            // 
            // pnlForm
            // 
            pnlForm.BackColor = Color.White;
            pnlForm.Controls.Add(lblUsername);
            pnlForm.Controls.Add(txtUsername);
            pnlForm.Controls.Add(lblPassword);
            pnlForm.Controls.Add(txtPassword);
            pnlForm.Controls.Add(lblPasswordStrength);
            pnlForm.Controls.Add(pbPasswordStrength);
            pnlForm.Controls.Add(lblConfirmPassword);
            pnlForm.Controls.Add(txtConfirmPassword);
            pnlForm.Controls.Add(lblRole);
            pnlForm.Controls.Add(cbRole);
            pnlForm.Controls.Add(lblIsActive);
            pnlForm.Controls.Add(chkIsActive);
            pnlForm.Controls.Add(lblActiveText);
            pnlForm.Controls.Add(lblAvatar);
            pnlForm.Controls.Add(txtAvatarUrl);
            pnlForm.Controls.Add(btnBrowseAvatar);
            pnlForm.Controls.Add(picAvatar);
            pnlForm.Dock = DockStyle.Bottom;
            pnlForm.Location = new Point(20, 398);
            pnlForm.Name = "pnlForm";
            pnlForm.Padding = new Padding(30);
            pnlForm.Size = new Size(1158, 280);
            pnlForm.TabIndex = 1;
            // 
            // lblUsername
            // 
            lblUsername.Font = new Font("Segoe UI", 11F);
            lblUsername.ForeColor = Color.FromArgb(45, 55, 72);
            lblUsername.Location = new Point(30, 40);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(150, 30);
            lblUsername.TabIndex = 0;
            lblUsername.Text = "Tên đăng nhập:";
            lblUsername.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtUsername
            // 
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            txtUsername.Font = new Font("Segoe UI", 11F);
            txtUsername.Location = new Point(190, 40);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(300, 27);
            txtUsername.TabIndex = 1;
            txtUsername.KeyPress += txtUsername_KeyPress;
            // 
            // lblPassword
            // 
            lblPassword.Font = new Font("Segoe UI", 11F);
            lblPassword.ForeColor = Color.FromArgb(45, 55, 72);
            lblPassword.Location = new Point(30, 85);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(150, 30);
            lblPassword.TabIndex = 2;
            lblPassword.Text = "Mật khẩu:";
            lblPassword.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtPassword
            // 
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.Font = new Font("Segoe UI", 11F);
            txtPassword.Location = new Point(190, 85);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '●';
            txtPassword.Size = new Size(300, 27);
            txtPassword.TabIndex = 3;
            txtPassword.TextChanged += txtPassword_TextChanged;
            txtPassword.KeyPress += txtPassword_KeyPress;
            // 
            // lblPasswordStrength
            // 
            lblPasswordStrength.Font = new Font("Segoe UI", 9F);
            lblPasswordStrength.ForeColor = Color.FromArgb(108, 117, 125);
            lblPasswordStrength.Location = new Point(190, 115);
            lblPasswordStrength.Name = "lblPasswordStrength";
            lblPasswordStrength.Size = new Size(300, 20);
            lblPasswordStrength.TabIndex = 4;
            lblPasswordStrength.Text = "Độ mạnh mật khẩu: Yếu";
            // 
            // pbPasswordStrength
            // 
            pbPasswordStrength.Location = new Point(190, 138);
            pbPasswordStrength.Name = "pbPasswordStrength";
            pbPasswordStrength.Size = new Size(300, 8);
            pbPasswordStrength.Style = ProgressBarStyle.Continuous;
            pbPasswordStrength.TabIndex = 5;
            // 
            // lblConfirmPassword
            // 
            lblConfirmPassword.Font = new Font("Segoe UI", 11F);
            lblConfirmPassword.ForeColor = Color.FromArgb(45, 55, 72);
            lblConfirmPassword.Location = new Point(30, 165);
            lblConfirmPassword.Name = "lblConfirmPassword";
            lblConfirmPassword.Size = new Size(150, 30);
            lblConfirmPassword.TabIndex = 6;
            lblConfirmPassword.Text = "Xác nhận mật khẩu:";
            lblConfirmPassword.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtConfirmPassword
            // 
            txtConfirmPassword.BorderStyle = BorderStyle.FixedSingle;
            txtConfirmPassword.Font = new Font("Segoe UI", 11F);
            txtConfirmPassword.Location = new Point(190, 165);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.PasswordChar = '●';
            txtConfirmPassword.Size = new Size(300, 27);
            txtConfirmPassword.TabIndex = 7;
            txtConfirmPassword.KeyPress += txtConfirmPassword_KeyPress;
            // 
            // lblRole
            // 
            lblRole.Font = new Font("Segoe UI", 11F);
            lblRole.ForeColor = Color.FromArgb(45, 55, 72);
            lblRole.Location = new Point(550, 40);
            lblRole.Name = "lblRole";
            lblRole.Size = new Size(120, 30);
            lblRole.TabIndex = 6;
            lblRole.Text = "Vai trò:";
            lblRole.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cbRole
            // 
            cbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cbRole.Font = new Font("Segoe UI", 11F);
            cbRole.FormattingEnabled = true;
            cbRole.Location = new Point(680, 40);
            cbRole.Name = "cbRole";
            cbRole.Size = new Size(250, 28);
            cbRole.TabIndex = 7;
            cbRole.KeyPress += cbRole_KeyPress;
            // 
            // lblIsActive
            // 
            lblIsActive.Font = new Font("Segoe UI", 11F);
            lblIsActive.ForeColor = Color.FromArgb(45, 55, 72);
            lblIsActive.Location = new Point(550, 85);
            lblIsActive.Name = "lblIsActive";
            lblIsActive.Size = new Size(120, 30);
            lblIsActive.TabIndex = 8;
            lblIsActive.Text = "Trạng thái:";
            lblIsActive.TextAlign = ContentAlignment.MiddleRight;
            // 
            // chkIsActive
            // 
            chkIsActive.Checked = true;
            chkIsActive.CheckState = CheckState.Checked;
            chkIsActive.Location = new Point(680, 90);
            chkIsActive.Name = "chkIsActive";
            chkIsActive.Size = new Size(21, 25);
            chkIsActive.TabIndex = 9;
            chkIsActive.KeyPress += chkIsActive_KeyPress;
            // 
            // lblActiveText
            // 
            lblActiveText.Font = new Font("Segoe UI", 11F);
            lblActiveText.ForeColor = Color.FromArgb(45, 55, 72);
            lblActiveText.Location = new Point(697, 90);
            lblActiveText.Name = "lblActiveText";
            lblActiveText.Size = new Size(200, 25);
            lblActiveText.TabIndex = 10;
            lblActiveText.Text = "Hoạt động";
            // 
            // lblAvatar
            // 
            lblAvatar.Font = new Font("Segoe UI", 11F);
            lblAvatar.ForeColor = Color.FromArgb(45, 55, 72);
            lblAvatar.Location = new Point(30, 206);
            lblAvatar.Name = "lblAvatar";
            lblAvatar.Size = new Size(150, 30);
            lblAvatar.TabIndex = 11;
            lblAvatar.Text = "Ảnh đại diện:";
            lblAvatar.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtAvatarUrl
            // 
            txtAvatarUrl.BorderStyle = BorderStyle.FixedSingle;
            txtAvatarUrl.Font = new Font("Segoe UI", 11F);
            txtAvatarUrl.Location = new Point(190, 206);
            txtAvatarUrl.Name = "txtAvatarUrl";
            txtAvatarUrl.Size = new Size(220, 27);
            txtAvatarUrl.TabIndex = 12;
            // 
            // btnBrowseAvatar
            // 
            btnBrowseAvatar.BackColor = Color.FromArgb(108, 117, 125);
            btnBrowseAvatar.Cursor = Cursors.Hand;
            btnBrowseAvatar.FlatAppearance.BorderSize = 0;
            btnBrowseAvatar.FlatStyle = FlatStyle.Flat;
            btnBrowseAvatar.Font = new Font("Segoe UI", 10F);
            btnBrowseAvatar.ForeColor = Color.White;
            btnBrowseAvatar.Location = new Point(420, 206);
            btnBrowseAvatar.Name = "btnBrowseAvatar";
            btnBrowseAvatar.Size = new Size(70, 27);
            btnBrowseAvatar.TabIndex = 13;
            btnBrowseAvatar.Text = "📁 Duyệt";
            btnBrowseAvatar.UseVisualStyleBackColor = false;
            btnBrowseAvatar.Click += btnBrowseAvatar_Click;
            // 
            // picAvatar
            // 
            picAvatar.BackColor = Color.White;
            picAvatar.Location = new Point(617, 124);
            picAvatar.Name = "picAvatar";
            picAvatar.Size = new Size(113, 112);
            picAvatar.SizeMode = PictureBoxSizeMode.Zoom;
            picAvatar.TabIndex = 14;
            picAvatar.TabStop = false;
            // 
            // frmUsers
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(247, 249, 252);
            ClientSize = new Size(1198, 898);
            Controls.Add(pnlMain);
            Controls.Add(pnlSearch);
            Controls.Add(pnlToolbar);
            Controls.Add(pnlHeader);
            Name = "frmUsers";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "👨‍💼 Quản lý người dùng - MilkTea POS";
            WindowState = FormWindowState.Maximized;
            pnlHeader.ResumeLayout(false);
            pnlToolbar.ResumeLayout(false);
            pnlSearch.ResumeLayout(false);
            pnlSearch.PerformLayout();
            pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            pnlForm.ResumeLayout(false);
            pnlForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picAvatar).EndInit();
            ResumeLayout(false);
        }
    }
}
