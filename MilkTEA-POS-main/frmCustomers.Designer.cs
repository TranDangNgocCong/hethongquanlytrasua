namespace MilkTeaPOS
{
    partial class frmCustomers
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
        private System.Windows.Forms.DataGridView dgvCustomers;

        // Form fields - Left column
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblDateOfBirth;
        private System.Windows.Forms.DateTimePicker dtpDateOfBirth;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.ComboBox cbGender;

        // Form fields - Right column
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Label lblAvatar;
        private System.Windows.Forms.TextBox txtAvatarUrl;
        private System.Windows.Forms.Button btnBrowseAvatar;
        private System.Windows.Forms.PictureBox picAvatar;
        private System.Windows.Forms.Label lblCreatedAt;
        private System.Windows.Forms.DateTimePicker dtpCreatedAt;
        private System.Windows.Forms.Label lblUpdatedAt;
        private System.Windows.Forms.DateTimePicker dtpUpdatedAt;

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
            dgvCustomers = new DataGridView();
            pnlForm = new Panel();
            lblName = new Label();
            txtName = new TextBox();
            lblPhone = new Label();
            txtPhone = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblDateOfBirth = new Label();
            dtpDateOfBirth = new DateTimePicker();
            lblGender = new Label();
            cbGender = new ComboBox();
            lblAddress = new Label();
            txtAddress = new TextBox();
            lblNotes = new Label();
            txtNotes = new TextBox();
            lblAvatar = new Label();
            txtAvatarUrl = new TextBox();
            btnBrowseAvatar = new Button();
            picAvatar = new PictureBox();
            lblCreatedAt = new Label();
            dtpCreatedAt = new DateTimePicker();
            lblUpdatedAt = new Label();
            dtpUpdatedAt = new DateTimePicker();
            pnlHeader.SuspendLayout();
            pnlToolbar.SuspendLayout();
            pnlSearch.SuspendLayout();
            pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).BeginInit();
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
            pnlHeader.Size = new Size(1386, 80);
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
            lblTitle.Text = "👥 Quản lý khách hàng";
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
            pnlToolbar.Size = new Size(1386, 60);
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
            pnlSearch.Size = new Size(1386, 60);
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
            pnlMain.Controls.Add(dgvCustomers);
            pnlMain.Controls.Add(pnlForm);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 200);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new Padding(20);
            pnlMain.Size = new Size(1386, 698);
            pnlMain.TabIndex = 0;
            //
            // dgvCustomers
            //
            dgvCustomers.AllowUserToAddRows = false;
            dgvCustomers.AllowUserToDeleteRows = false;
            dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCustomers.BackgroundColor = Color.White;
            dgvCustomers.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(45, 55, 72);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(45, 55, 72);
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvCustomers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvCustomers.ColumnHeadersHeight = 45;
            dgvCustomers.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvCustomers.GridColor = Color.FromArgb(226, 232, 240);
            dgvCustomers.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 11F);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(45, 55, 72);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(255, 107, 107);
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvCustomers.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(247, 249, 252);
            dgvCustomers.Dock = DockStyle.Fill;
            dgvCustomers.EnableHeadersVisualStyles = false;
            dgvCustomers.Location = new Point(20, 20);
            dgvCustomers.MultiSelect = false;
            dgvCustomers.Name = "dgvCustomers";
            dgvCustomers.RowHeadersVisible = false;
            dgvCustomers.RowTemplate.Height = 50;
            dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCustomers.Size = new Size(1346, 300);
            dgvCustomers.TabIndex = 0;
            dgvCustomers.CellClick += dgvCustomers_CellClick;
            dgvCustomers.CellFormatting += dgvCustomers_CellFormatting;
            // 
            // pnlForm
            // 
            pnlForm.BackColor = Color.White;
            pnlForm.Controls.Add(lblName);
            pnlForm.Controls.Add(txtName);
            pnlForm.Controls.Add(lblPhone);
            pnlForm.Controls.Add(txtPhone);
            pnlForm.Controls.Add(lblEmail);
            pnlForm.Controls.Add(txtEmail);
            pnlForm.Controls.Add(lblDateOfBirth);
            pnlForm.Controls.Add(dtpDateOfBirth);
            pnlForm.Controls.Add(lblGender);
            pnlForm.Controls.Add(cbGender);
            pnlForm.Controls.Add(lblAddress);
            pnlForm.Controls.Add(txtAddress);
            pnlForm.Controls.Add(lblNotes);
            pnlForm.Controls.Add(txtNotes);
            pnlForm.Controls.Add(lblAvatar);
            pnlForm.Controls.Add(txtAvatarUrl);
            pnlForm.Controls.Add(btnBrowseAvatar);
            pnlForm.Controls.Add(picAvatar);
            pnlForm.Controls.Add(lblCreatedAt);
            pnlForm.Controls.Add(dtpCreatedAt);
            pnlForm.Controls.Add(lblUpdatedAt);
            pnlForm.Controls.Add(dtpUpdatedAt);
            pnlForm.Dock = DockStyle.Bottom;
            pnlForm.Location = new Point(20, 398);
            pnlForm.Name = "pnlForm";
            pnlForm.Padding = new Padding(30);
            pnlForm.Size = new Size(1346, 280);
            pnlForm.TabIndex = 1;
            // 
            // lblName
            // 
            lblName.Font = new Font("Segoe UI", 11F);
            lblName.ForeColor = Color.FromArgb(45, 55, 72);
            lblName.Location = new Point(30, 30);
            lblName.Name = "lblName";
            lblName.Size = new Size(120, 30);
            lblName.TabIndex = 0;
            lblName.Text = "Tên khách hàng:";
            lblName.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtName
            // 
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.Font = new Font("Segoe UI", 11F);
            txtName.Location = new Point(160, 30);
            txtName.Name = "txtName";
            txtName.Size = new Size(300, 27);
            txtName.TabIndex = 1;
            txtName.KeyPress += txtName_KeyPress;
            // 
            // lblPhone
            // 
            lblPhone.Font = new Font("Segoe UI", 11F);
            lblPhone.ForeColor = Color.FromArgb(45, 55, 72);
            lblPhone.Location = new Point(30, 70);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(120, 30);
            lblPhone.TabIndex = 2;
            lblPhone.Text = "Số điện thoại:";
            lblPhone.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtPhone
            // 
            txtPhone.BorderStyle = BorderStyle.FixedSingle;
            txtPhone.Font = new Font("Segoe UI", 11F);
            txtPhone.Location = new Point(160, 70);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(300, 27);
            txtPhone.TabIndex = 3;
            txtPhone.KeyPress += txtPhone_KeyPress;
            // 
            // lblEmail
            // 
            lblEmail.Font = new Font("Segoe UI", 11F);
            lblEmail.ForeColor = Color.FromArgb(45, 55, 72);
            lblEmail.Location = new Point(30, 110);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(120, 30);
            lblEmail.TabIndex = 4;
            lblEmail.Text = "Email:";
            lblEmail.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtEmail
            // 
            txtEmail.BorderStyle = BorderStyle.FixedSingle;
            txtEmail.Font = new Font("Segoe UI", 11F);
            txtEmail.Location = new Point(160, 110);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(300, 27);
            txtEmail.TabIndex = 5;
            txtEmail.KeyPress += txtEmail_KeyPress;
            // 
            // lblDateOfBirth
            // 
            lblDateOfBirth.Font = new Font("Segoe UI", 11F);
            lblDateOfBirth.ForeColor = Color.FromArgb(45, 55, 72);
            lblDateOfBirth.Location = new Point(30, 150);
            lblDateOfBirth.Name = "lblDateOfBirth";
            lblDateOfBirth.Size = new Size(120, 30);
            lblDateOfBirth.TabIndex = 6;
            lblDateOfBirth.Text = "Ngày sinh:";
            lblDateOfBirth.TextAlign = ContentAlignment.MiddleRight;
            // 
            // dtpDateOfBirth
            // 
            dtpDateOfBirth.CalendarFont = new Font("Segoe UI", 11F);
            dtpDateOfBirth.CustomFormat = "dd/MM/yyyy";
            dtpDateOfBirth.Format = DateTimePickerFormat.Custom;
            dtpDateOfBirth.Location = new Point(160, 150);
            dtpDateOfBirth.Name = "dtpDateOfBirth";
            dtpDateOfBirth.ShowCheckBox = true;
            dtpDateOfBirth.Size = new Size(300, 23);
            dtpDateOfBirth.TabIndex = 7;
            dtpDateOfBirth.KeyPress += dtpDateOfBirth_KeyPress;
            // 
            // lblGender
            // 
            lblGender.Font = new Font("Segoe UI", 11F);
            lblGender.ForeColor = Color.FromArgb(45, 55, 72);
            lblGender.Location = new Point(503, 30);
            lblGender.Name = "lblGender";
            lblGender.Size = new Size(100, 30);
            lblGender.TabIndex = 8;
            lblGender.Text = "Giới tính:";
            lblGender.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cbGender
            // 
            cbGender.DropDownStyle = ComboBoxStyle.DropDownList;
            cbGender.Font = new Font("Segoe UI", 11F);
            cbGender.FormattingEnabled = true;
            cbGender.Location = new Point(610, 30);
            cbGender.Name = "cbGender";
            cbGender.Size = new Size(200, 28);
            cbGender.TabIndex = 9;
            cbGender.KeyPress += cbGender_KeyPress;
            // 
            // lblAddress
            // 
            lblAddress.Font = new Font("Segoe UI", 11F);
            lblAddress.ForeColor = Color.FromArgb(45, 55, 72);
            lblAddress.Location = new Point(503, 70);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(100, 30);
            lblAddress.TabIndex = 10;
            lblAddress.Text = "Địa chỉ:";
            lblAddress.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtAddress
            // 
            txtAddress.BorderStyle = BorderStyle.FixedSingle;
            txtAddress.Font = new Font("Segoe UI", 11F);
            txtAddress.Location = new Point(610, 70);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(400, 27);
            txtAddress.TabIndex = 11;
            txtAddress.KeyPress += txtAddress_KeyPress;
            // 
            // lblNotes
            // 
            lblNotes.Font = new Font("Segoe UI", 11F);
            lblNotes.ForeColor = Color.FromArgb(45, 55, 72);
            lblNotes.Location = new Point(503, 110);
            lblNotes.Name = "lblNotes";
            lblNotes.Size = new Size(100, 30);
            lblNotes.TabIndex = 12;
            lblNotes.Text = "Ghi chú:";
            lblNotes.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtNotes
            // 
            txtNotes.BorderStyle = BorderStyle.FixedSingle;
            txtNotes.Font = new Font("Segoe UI", 11F);
            txtNotes.Location = new Point(610, 110);
            txtNotes.Name = "txtNotes";
            txtNotes.Size = new Size(400, 27);
            txtNotes.TabIndex = 13;
            txtNotes.KeyPress += txtNotes_KeyPress;
            // 
            // lblAvatar
            // 
            lblAvatar.Font = new Font("Segoe UI", 11F);
            lblAvatar.ForeColor = Color.FromArgb(45, 55, 72);
            lblAvatar.Location = new Point(503, 150);
            lblAvatar.Name = "lblAvatar";
            lblAvatar.Size = new Size(100, 30);
            lblAvatar.TabIndex = 14;
            lblAvatar.Text = "Ảnh đại diện:";
            lblAvatar.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtAvatarUrl
            // 
            txtAvatarUrl.BorderStyle = BorderStyle.FixedSingle;
            txtAvatarUrl.Font = new Font("Segoe UI", 11F);
            txtAvatarUrl.Location = new Point(610, 150);
            txtAvatarUrl.Name = "txtAvatarUrl";
            txtAvatarUrl.Size = new Size(250, 27);
            txtAvatarUrl.TabIndex = 15;
            // 
            // btnBrowseAvatar
            // 
            btnBrowseAvatar.BackColor = Color.FromArgb(108, 117, 125);
            btnBrowseAvatar.Cursor = Cursors.Hand;
            btnBrowseAvatar.FlatAppearance.BorderSize = 0;
            btnBrowseAvatar.FlatStyle = FlatStyle.Flat;
            btnBrowseAvatar.Font = new Font("Segoe UI", 10F);
            btnBrowseAvatar.ForeColor = Color.White;
            btnBrowseAvatar.Location = new Point(875, 149);
            btnBrowseAvatar.Name = "btnBrowseAvatar";
            btnBrowseAvatar.Size = new Size(62, 28);
            btnBrowseAvatar.TabIndex = 16;
            btnBrowseAvatar.Text = "📁 Duyệt";
            btnBrowseAvatar.UseVisualStyleBackColor = false;
            btnBrowseAvatar.Click += btnBrowseAvatar_Click;
            // 
            // picAvatar
            // 
            picAvatar.BackColor = Color.White;
            picAvatar.Location = new Point(1071, 33);
            picAvatar.Name = "picAvatar";
            picAvatar.Size = new Size(186, 186);
            picAvatar.SizeMode = PictureBoxSizeMode.Zoom;
            picAvatar.TabIndex = 17;
            picAvatar.TabStop = false;
            // 
            // lblCreatedAt
            // 
            lblCreatedAt.Font = new Font("Segoe UI", 11F);
            lblCreatedAt.ForeColor = Color.FromArgb(45, 55, 72);
            lblCreatedAt.Location = new Point(503, 150);
            lblCreatedAt.Name = "lblCreatedAt";
            lblCreatedAt.Size = new Size(100, 30);
            lblCreatedAt.TabIndex = 14;
            lblCreatedAt.Text = "Ngày tạo:";
            lblCreatedAt.TextAlign = ContentAlignment.MiddleRight;
            // 
            // dtpCreatedAt
            // 
            dtpCreatedAt.CalendarFont = new Font("Segoe UI", 11F);
            dtpCreatedAt.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            dtpCreatedAt.Format = DateTimePickerFormat.Custom;
            dtpCreatedAt.Location = new Point(610, 150);
            dtpCreatedAt.Name = "dtpCreatedAt";
            dtpCreatedAt.ShowCheckBox = true;
            dtpCreatedAt.Size = new Size(200, 23);
            dtpCreatedAt.TabIndex = 15;
            dtpCreatedAt.KeyPress += dtpCreatedAt_KeyPress;
            // 
            // lblUpdatedAt
            // 
            lblUpdatedAt.Font = new Font("Segoe UI", 11F);
            lblUpdatedAt.ForeColor = Color.FromArgb(45, 55, 72);
            lblUpdatedAt.Location = new Point(484, 191);
            lblUpdatedAt.Name = "lblUpdatedAt";
            lblUpdatedAt.Size = new Size(120, 30);
            lblUpdatedAt.TabIndex = 16;
            lblUpdatedAt.Text = "Ngày cập nhật:";
            lblUpdatedAt.TextAlign = ContentAlignment.MiddleRight;
            // 
            // dtpUpdatedAt
            // 
            dtpUpdatedAt.CalendarFont = new Font("Segoe UI", 11F);
            dtpUpdatedAt.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            dtpUpdatedAt.Format = DateTimePickerFormat.Custom;
            dtpUpdatedAt.Location = new Point(610, 194);
            dtpUpdatedAt.Name = "dtpUpdatedAt";
            dtpUpdatedAt.ShowCheckBox = true;
            dtpUpdatedAt.Size = new Size(200, 23);
            dtpUpdatedAt.TabIndex = 17;
            dtpUpdatedAt.KeyPress += dtpUpdatedAt_KeyPress;
            // 
            // frmCustomers
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(247, 249, 252);
            ClientSize = new Size(1386, 898);
            Controls.Add(pnlMain);
            Controls.Add(pnlSearch);
            Controls.Add(pnlToolbar);
            Controls.Add(pnlHeader);
            Name = "frmCustomers";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "👥 Quản lý khách hàng - MilkTea POS";
            WindowState = FormWindowState.Maximized;
            pnlHeader.ResumeLayout(false);
            pnlToolbar.ResumeLayout(false);
            pnlSearch.ResumeLayout(false);
            pnlSearch.PerformLayout();
            pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).EndInit();
            pnlForm.ResumeLayout(false);
            pnlForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picAvatar).EndInit();
            ResumeLayout(false);
        }
    }
}
