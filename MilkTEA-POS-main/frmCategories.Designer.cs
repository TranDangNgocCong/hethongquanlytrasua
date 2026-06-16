namespace MilkTeaPOS
{
    partial class frmCategories
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
        private System.Windows.Forms.DataGridView dgvCategories;

        // Form fields
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblDisplayOrder;
        private System.Windows.Forms.NumericUpDown numDisplayOrder;
        private System.Windows.Forms.Label lblCreatedAt;
        private System.Windows.Forms.DateTimePicker dtpCreatedAt;
        private System.Windows.Forms.Label lblUpdatedAt;
        private System.Windows.Forms.DateTimePicker dtpUpdatedAt;
        private System.Windows.Forms.Label lblImageUrl;
        private System.Windows.Forms.TextBox txtImageUrl;
        private System.Windows.Forms.Button btnBrowseImage;
        private System.Windows.Forms.Label lblIsActive;
        private System.Windows.Forms.CheckBox chkIsActive;
        private System.Windows.Forms.Label lblActiveText;
        private System.Windows.Forms.PictureBox picPreview;

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
            dgvCategories = new DataGridView();
            pnlForm = new Panel();
            lblName = new Label();
            txtName = new TextBox();
            lblDescription = new Label();
            txtDescription = new TextBox();
            lblDisplayOrder = new Label();
            numDisplayOrder = new NumericUpDown();
            lblCreatedAt = new Label();
            dtpCreatedAt = new DateTimePicker();
            lblUpdatedAt = new Label();
            dtpUpdatedAt = new DateTimePicker();
            lblImageUrl = new Label();
            txtImageUrl = new TextBox();
            btnBrowseImage = new Button();
            lblIsActive = new Label();
            chkIsActive = new CheckBox();
            lblActiveText = new Label();
            picPreview = new PictureBox();
            pnlHeader.SuspendLayout();
            pnlToolbar.SuspendLayout();
            pnlSearch.SuspendLayout();
            pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCategories).BeginInit();
            pnlForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numDisplayOrder).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picPreview).BeginInit();
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
            lblTitle.Text = "📋 Quản lý danh mục sản phẩm";
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
            pnlMain.Controls.Add(dgvCategories);
            pnlMain.Controls.Add(pnlForm);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 200);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new Padding(20);
            pnlMain.Size = new Size(1198, 698);
            pnlMain.TabIndex = 0;
            //
            // dgvCategories
            //
            dgvCategories.AllowUserToAddRows = false;
            dgvCategories.AllowUserToDeleteRows = false;
            dgvCategories.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCategories.BackgroundColor = Color.White;
            dgvCategories.BorderStyle = BorderStyle.None;
            dgvCategories.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                BackColor = Color.FromArgb(45, 55, 72),
                ForeColor = Color.White,
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };
            dgvCategories.ColumnHeadersHeight = 45;
            dgvCategories.DefaultCellStyle = new DataGridViewCellStyle
            {
                Font = new Font("Segoe UI", 11F),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(45, 55, 72),
                SelectionBackColor = Color.FromArgb(255, 107, 107),
                SelectionForeColor = Color.White
            };
            dgvCategories.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(247, 249, 252)
            };
            dgvCategories.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvCategories.Dock = DockStyle.Fill;
            dgvCategories.EnableHeadersVisualStyles = false;
            dgvCategories.GridColor = Color.FromArgb(226, 232, 240);
            dgvCategories.Location = new Point(20, 20);
            dgvCategories.MultiSelect = false;
            dgvCategories.Name = "dgvCategories";
            dgvCategories.RowHeadersVisible = false;
            dgvCategories.RowTemplate.Height = 50;
            dgvCategories.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCategories.Size = new Size(1158, 300);
            dgvCategories.TabIndex = 0;
            dgvCategories.CellClick += dgvCategories_CellClick;
            dgvCategories.CellFormatting += dgvCategories_CellFormatting;
            //
            // pnlForm
            // 
            pnlForm.BackColor = Color.White;
            pnlForm.Controls.Add(lblName);
            pnlForm.Controls.Add(txtName);
            pnlForm.Controls.Add(lblDescription);
            pnlForm.Controls.Add(txtDescription);
            pnlForm.Controls.Add(lblDisplayOrder);
            pnlForm.Controls.Add(numDisplayOrder);
            pnlForm.Controls.Add(lblCreatedAt);
            pnlForm.Controls.Add(dtpCreatedAt);
            pnlForm.Controls.Add(lblUpdatedAt);
            pnlForm.Controls.Add(dtpUpdatedAt);
            pnlForm.Controls.Add(lblImageUrl);
            pnlForm.Controls.Add(txtImageUrl);
            pnlForm.Controls.Add(btnBrowseImage);
            pnlForm.Controls.Add(lblIsActive);
            pnlForm.Controls.Add(chkIsActive);
            pnlForm.Controls.Add(lblActiveText);
            pnlForm.Controls.Add(picPreview);
            pnlForm.Dock = DockStyle.Bottom;
            pnlForm.Location = new Point(20, 398);
            pnlForm.Name = "pnlForm";
            pnlForm.Padding = new Padding(30);
            pnlForm.Size = new Size(1158, 280);
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
            lblName.Text = "Tên danh mục:";
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
            // lblDescription
            // 
            lblDescription.Font = new Font("Segoe UI", 11F);
            lblDescription.ForeColor = Color.FromArgb(45, 55, 72);
            lblDescription.Location = new Point(30, 70);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(120, 30);
            lblDescription.TabIndex = 2;
            lblDescription.Text = "Mô tả:";
            lblDescription.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtDescription
            // 
            txtDescription.BorderStyle = BorderStyle.FixedSingle;
            txtDescription.Font = new Font("Segoe UI", 11F);
            txtDescription.Location = new Point(160, 70);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(300, 27);
            txtDescription.TabIndex = 3;
            txtDescription.KeyPress += txtDescription_KeyPress;
            // 
            // lblDisplayOrder
            // 
            lblDisplayOrder.Font = new Font("Segoe UI", 11F);
            lblDisplayOrder.ForeColor = Color.FromArgb(45, 55, 72);
            lblDisplayOrder.Location = new Point(30, 110);
            lblDisplayOrder.Name = "lblDisplayOrder";
            lblDisplayOrder.Size = new Size(120, 30);
            lblDisplayOrder.TabIndex = 4;
            lblDisplayOrder.Text = "Thứ tự hiển thị:";
            lblDisplayOrder.TextAlign = ContentAlignment.MiddleRight;
            // 
            // numDisplayOrder
            // 
            numDisplayOrder.Font = new Font("Segoe UI", 11F);
            numDisplayOrder.Location = new Point(160, 110);
            numDisplayOrder.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numDisplayOrder.Name = "numDisplayOrder";
            numDisplayOrder.Size = new Size(300, 27);
            numDisplayOrder.TabIndex = 5;
            numDisplayOrder.KeyPress += numDisplayOrder_KeyPress;
            // 
            // lblCreatedAt
            // 
            lblCreatedAt.Font = new Font("Segoe UI", 11F);
            lblCreatedAt.ForeColor = Color.FromArgb(45, 55, 72);
            lblCreatedAt.Location = new Point(30, 150);
            lblCreatedAt.Name = "lblCreatedAt";
            lblCreatedAt.Size = new Size(120, 30);
            lblCreatedAt.TabIndex = 6;
            lblCreatedAt.Text = "Ngày tạo:";
            lblCreatedAt.TextAlign = ContentAlignment.MiddleRight;
            // 
            // dtpCreatedAt
            // 
            dtpCreatedAt.CalendarFont = new Font("Segoe UI", 11F);
            dtpCreatedAt.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            dtpCreatedAt.Format = DateTimePickerFormat.Custom;
            dtpCreatedAt.Location = new Point(160, 150);
            dtpCreatedAt.Name = "dtpCreatedAt";
            dtpCreatedAt.ShowCheckBox = true;
            dtpCreatedAt.Size = new Size(300, 23);
            dtpCreatedAt.TabIndex = 7;
            dtpCreatedAt.KeyPress += dtpCreatedAt_KeyPress;
            // 
            // lblUpdatedAt
            // 
            lblUpdatedAt.Font = new Font("Segoe UI", 11F);
            lblUpdatedAt.ForeColor = Color.FromArgb(45, 55, 72);
            lblUpdatedAt.Location = new Point(30, 190);
            lblUpdatedAt.Name = "lblUpdatedAt";
            lblUpdatedAt.Size = new Size(120, 30);
            lblUpdatedAt.TabIndex = 15;
            lblUpdatedAt.Text = "Ngày cập nhật:";
            lblUpdatedAt.TextAlign = ContentAlignment.MiddleRight;
            // 
            // dtpUpdatedAt
            // 
            dtpUpdatedAt.CalendarFont = new Font("Segoe UI", 11F);
            dtpUpdatedAt.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            dtpUpdatedAt.Format = DateTimePickerFormat.Custom;
            dtpUpdatedAt.Location = new Point(160, 190);
            dtpUpdatedAt.Name = "dtpUpdatedAt";
            dtpUpdatedAt.ShowCheckBox = true;
            dtpUpdatedAt.Size = new Size(300, 23);
            dtpUpdatedAt.TabIndex = 16;
            dtpUpdatedAt.KeyPress += dtpUpdatedAt_KeyPress;
            // 
            // lblImageUrl
            // 
            lblImageUrl.Font = new Font("Segoe UI", 11F);
            lblImageUrl.ForeColor = Color.FromArgb(45, 55, 72);
            lblImageUrl.Location = new Point(30, 230);
            lblImageUrl.Name = "lblImageUrl";
            lblImageUrl.Size = new Size(120, 30);
            lblImageUrl.TabIndex = 8;
            lblImageUrl.Text = "URL hình ảnh:";
            lblImageUrl.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtImageUrl
            // 
            txtImageUrl.BorderStyle = BorderStyle.FixedSingle;
            txtImageUrl.Font = new Font("Segoe UI", 11F);
            txtImageUrl.Location = new Point(160, 230);
            txtImageUrl.Name = "txtImageUrl";
            txtImageUrl.Size = new Size(220, 27);
            txtImageUrl.TabIndex = 9;
            txtImageUrl.KeyPress += txtImageUrl_KeyPress;
            // 
            // btnBrowseImage
            // 
            btnBrowseImage.BackColor = Color.FromArgb(108, 117, 125);
            btnBrowseImage.Cursor = Cursors.Hand;
            btnBrowseImage.FlatAppearance.BorderSize = 0;
            btnBrowseImage.FlatStyle = FlatStyle.Flat;
            btnBrowseImage.Font = new Font("Segoe UI", 10F);
            btnBrowseImage.ForeColor = Color.White;
            btnBrowseImage.Location = new Point(390, 230);
            btnBrowseImage.Name = "btnBrowseImage";
            btnBrowseImage.Size = new Size(70, 30);
            btnBrowseImage.TabIndex = 10;
            btnBrowseImage.Text = "📁 Duyệt";
            btnBrowseImage.UseVisualStyleBackColor = false;
            btnBrowseImage.Click += btnBrowseImage_Click;
            // 
            // lblIsActive
            // 
            lblIsActive.Font = new Font("Segoe UI", 11F);
            lblIsActive.ForeColor = Color.FromArgb(45, 55, 72);
            lblIsActive.Location = new Point(504, 30);
            lblIsActive.Name = "lblIsActive";
            lblIsActive.Size = new Size(91, 30);
            lblIsActive.TabIndex = 11;
            lblIsActive.Text = "Trạng thái:";
            lblIsActive.TextAlign = ContentAlignment.MiddleRight;
            // 
            // chkIsActive
            // 
            chkIsActive.Checked = true;
            chkIsActive.CheckState = CheckState.Checked;
            chkIsActive.Location = new Point(615, 35);
            chkIsActive.Name = "chkIsActive";
            chkIsActive.Size = new Size(20, 25);
            chkIsActive.TabIndex = 12;
            chkIsActive.KeyPress += chkIsActive_KeyPress;
            // 
            // lblActiveText
            // 
            lblActiveText.Font = new Font("Segoe UI", 11F);
            lblActiveText.ForeColor = Color.FromArgb(45, 55, 72);
            lblActiveText.Location = new Point(630, 35);
            lblActiveText.Name = "lblActiveText";
            lblActiveText.Size = new Size(200, 25);
            lblActiveText.TabIndex = 13;
            lblActiveText.Text = "Hoạt động";
            // 
            // picPreview
            // 
            picPreview.BackColor = Color.FromArgb(247, 249, 252);
            picPreview.BorderStyle = BorderStyle.FixedSingle;
            picPreview.Location = new Point(521, 73);
            picPreview.Name = "picPreview";
            picPreview.Size = new Size(187, 187);
            picPreview.SizeMode = PictureBoxSizeMode.Zoom;
            picPreview.TabIndex = 14;
            picPreview.TabStop = false;
            // 
            // frmCategories
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(247, 249, 252);
            ClientSize = new Size(1198, 898);
            Controls.Add(pnlMain);
            Controls.Add(pnlSearch);
            Controls.Add(pnlToolbar);
            Controls.Add(pnlHeader);
            Name = "frmCategories";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "📦 Quản lý danh mục - MilkTea POS";
            WindowState = FormWindowState.Maximized;
            pnlHeader.ResumeLayout(false);
            pnlToolbar.ResumeLayout(false);
            pnlSearch.ResumeLayout(false);
            pnlSearch.PerformLayout();
            pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCategories).EndInit();
            pnlForm.ResumeLayout(false);
            pnlForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numDisplayOrder).EndInit();
            ((System.ComponentModel.ISupportInitialize)picPreview).EndInit();
            ResumeLayout(false);
        }
    }
}
