namespace MilkTeaPOS
{
    partial class frmTables
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
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblStatusFilter;
        private System.Windows.Forms.ComboBox cboStatusFilter;
        private System.Windows.Forms.Label lblLocationFilter;
        private System.Windows.Forms.TextBox txtLocationFilter;

        // DataGridView
        private System.Windows.Forms.DataGridView dgvTables;

        // Form fields
        private System.Windows.Forms.Label lblTableName;
        private System.Windows.Forms.TextBox txtTableName;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Label lblCapacity;
        private System.Windows.Forms.NumericUpDown numCapacity;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Label lblImageUrl;
        private System.Windows.Forms.TextBox txtImageUrl;
        private System.Windows.Forms.Button btnBrowseImage;
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
            txtSearch = new TextBox();
            btnSearch = new Button();
            lblStatusFilter = new Label();
            cboStatusFilter = new ComboBox();
            lblLocationFilter = new Label();
            txtLocationFilter = new TextBox();
            pnlMain = new Panel();
            dgvTables = new DataGridView();
            pnlForm = new Panel();
            lblTableName = new Label();
            txtTableName = new TextBox();
            lblStatus = new Label();
            cboStatus = new ComboBox();
            lblCapacity = new Label();
            numCapacity = new NumericUpDown();
            lblLocation = new Label();
            txtLocation = new TextBox();
            lblImageUrl = new Label();
            txtImageUrl = new TextBox();
            btnBrowseImage = new Button();
            picPreview = new PictureBox();
            pnlHeader.SuspendLayout();
            pnlToolbar.SuspendLayout();
            pnlSearch.SuspendLayout();
            pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTables).BeginInit();
            pnlForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numCapacity).BeginInit();
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
            lblTitle.Text = "\U0001fa91 Quản lý Bàn";
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
            btnRefresh.Size = new Size(120, 40);
            btnRefresh.TabIndex = 3;
            btnRefresh.Text = "🔄 Làm mới";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // pnlSearch
            // 
            pnlSearch.BackColor = Color.White;
            pnlSearch.Controls.Add(txtSearch);
            pnlSearch.Controls.Add(btnSearch);
            pnlSearch.Controls.Add(lblStatusFilter);
            pnlSearch.Controls.Add(cboStatusFilter);
            pnlSearch.Controls.Add(lblLocationFilter);
            pnlSearch.Controls.Add(txtLocationFilter);
            pnlSearch.Dock = DockStyle.Top;
            pnlSearch.Location = new Point(0, 140);
            pnlSearch.Name = "pnlSearch";
            pnlSearch.Size = new Size(1198, 60);
            pnlSearch.TabIndex = 1;
            // 
            // txtSearch
            // 
            txtSearch.BackColor = Color.FromArgb(247, 249, 252);
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.Font = new Font("Segoe UI", 12F);
            txtSearch.Location = new Point(95, 15);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(250, 29);
            txtSearch.TabIndex = 1;
            txtSearch.KeyPress += txtSearch_KeyPress;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.FromArgb(52, 152, 219);
            btnSearch.Cursor = Cursors.Hand;
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnSearch.ForeColor = Color.White;
            btnSearch.Location = new Point(350, 14);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(80, 31);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "Tìm";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;
            // 
            // lblStatusFilter
            // 
            lblStatusFilter.Font = new Font("Segoe UI", 12F);
            lblStatusFilter.ForeColor = Color.FromArgb(45, 55, 72);
            lblStatusFilter.Location = new Point(417, 20);
            lblStatusFilter.Name = "lblStatusFilter";
            lblStatusFilter.Size = new Size(102, 21);
            lblStatusFilter.TabIndex = 3;
            lblStatusFilter.Text = "Trạng thái:";
            lblStatusFilter.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cboStatusFilter
            // 
            cboStatusFilter.BackColor = Color.FromArgb(247, 249, 252);
            cboStatusFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cboStatusFilter.FlatStyle = FlatStyle.Flat;
            cboStatusFilter.Font = new Font("Segoe UI", 12F);
            cboStatusFilter.Location = new Point(529, 15);
            cboStatusFilter.Name = "cboStatusFilter";
            cboStatusFilter.Size = new Size(160, 29);
            cboStatusFilter.TabIndex = 4;
            // 
            // lblLocationFilter
            // 
            lblLocationFilter.Font = new Font("Segoe UI", 12F);
            lblLocationFilter.ForeColor = Color.FromArgb(45, 55, 72);
            lblLocationFilter.Location = new Point(695, 20);
            lblLocationFilter.Name = "lblLocationFilter";
            lblLocationFilter.Size = new Size(70, 21);
            lblLocationFilter.TabIndex = 5;
            lblLocationFilter.Text = "Khu vực:";
            lblLocationFilter.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtLocationFilter
            // 
            txtLocationFilter.BackColor = Color.FromArgb(247, 249, 252);
            txtLocationFilter.BorderStyle = BorderStyle.FixedSingle;
            txtLocationFilter.Font = new Font("Segoe UI", 12F);
            txtLocationFilter.Location = new Point(770, 15);
            txtLocationFilter.Name = "txtLocationFilter";
            txtLocationFilter.Size = new Size(200, 29);
            txtLocationFilter.TabIndex = 6;
            // 
            // pnlMain
            // 
            pnlMain.BackColor = Color.FromArgb(247, 249, 252);
            pnlMain.Controls.Add(dgvTables);
            pnlMain.Controls.Add(pnlForm);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 200);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new Padding(20);
            pnlMain.Size = new Size(1198, 698);
            pnlMain.TabIndex = 0;
            // 
            // dgvTables
            // 
            dgvTables.AllowUserToAddRows = false;
            dgvTables.AllowUserToDeleteRows = false;
            dgvTables.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTables.BackgroundColor = Color.White;
            dgvTables.BorderStyle = BorderStyle.None;
            dgvTables.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvTables.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvTables.ColumnHeadersHeight = 45;
            dgvTables.Dock = DockStyle.Fill;
            dgvTables.EnableHeadersVisualStyles = false;
            dgvTables.GridColor = Color.FromArgb(226, 232, 240);
            dgvTables.Location = new Point(20, 20);
            dgvTables.MultiSelect = false;
            dgvTables.Name = "dgvTables";
            dgvTables.RowHeadersVisible = false;
            dgvTables.RowTemplate.Height = 50;
            dgvTables.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTables.Size = new Size(1158, 378);
            dgvTables.TabIndex = 0;
            dgvTables.CellClick += dgvTables_CellClick;
            dgvTables.CellDoubleClick += DgvTables_CellDoubleClick;
            dgvTables.CellFormatting += dgvTables_CellFormatting;
            dgvTables.DataError += dgvTables_DataError;
            // 
            // pnlForm
            // 
            pnlForm.BackColor = Color.White;
            pnlForm.Controls.Add(lblTableName);
            pnlForm.Controls.Add(txtTableName);
            pnlForm.Controls.Add(lblStatus);
            pnlForm.Controls.Add(cboStatus);
            pnlForm.Controls.Add(lblCapacity);
            pnlForm.Controls.Add(numCapacity);
            pnlForm.Controls.Add(lblLocation);
            pnlForm.Controls.Add(txtLocation);
            pnlForm.Controls.Add(lblImageUrl);
            pnlForm.Controls.Add(txtImageUrl);
            pnlForm.Controls.Add(btnBrowseImage);
            pnlForm.Controls.Add(picPreview);
            pnlForm.Dock = DockStyle.Bottom;
            pnlForm.Location = new Point(20, 398);
            pnlForm.Name = "pnlForm";
            pnlForm.Padding = new Padding(30);
            pnlForm.Size = new Size(1158, 280);
            pnlForm.TabIndex = 1;
            // 
            // lblTableName
            // 
            lblTableName.Font = new Font("Segoe UI", 11F);
            lblTableName.ForeColor = Color.FromArgb(45, 55, 72);
            lblTableName.Location = new Point(30, 30);
            lblTableName.Name = "lblTableName";
            lblTableName.Size = new Size(120, 30);
            lblTableName.TabIndex = 0;
            lblTableName.Text = "Tên bàn:";
            lblTableName.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtTableName
            // 
            txtTableName.BorderStyle = BorderStyle.FixedSingle;
            txtTableName.Font = new Font("Segoe UI", 11F);
            txtTableName.Location = new Point(160, 30);
            txtTableName.Name = "txtTableName";
            txtTableName.Size = new Size(300, 27);
            txtTableName.TabIndex = 1;
            // 
            // lblStatus
            // 
            lblStatus.Font = new Font("Segoe UI", 11F);
            lblStatus.ForeColor = Color.FromArgb(45, 55, 72);
            lblStatus.Location = new Point(30, 80);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(120, 30);
            lblStatus.TabIndex = 2;
            lblStatus.Text = "Trạng thái:";
            lblStatus.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cboStatus
            // 
            cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboStatus.FlatStyle = FlatStyle.Flat;
            cboStatus.Font = new Font("Segoe UI", 11F);
            cboStatus.Location = new Point(160, 80);
            cboStatus.Name = "cboStatus";
            cboStatus.Size = new Size(300, 28);
            cboStatus.TabIndex = 3;
            // 
            // lblCapacity
            // 
            lblCapacity.Font = new Font("Segoe UI", 11F);
            lblCapacity.ForeColor = Color.FromArgb(45, 55, 72);
            lblCapacity.Location = new Point(30, 130);
            lblCapacity.Name = "lblCapacity";
            lblCapacity.Size = new Size(120, 30);
            lblCapacity.TabIndex = 4;
            lblCapacity.Text = "Sức chứa:";
            lblCapacity.TextAlign = ContentAlignment.MiddleRight;
            // 
            // numCapacity
            // 
            numCapacity.Font = new Font("Segoe UI", 11F);
            numCapacity.Location = new Point(160, 130);
            numCapacity.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numCapacity.Name = "numCapacity";
            numCapacity.Size = new Size(300, 27);
            numCapacity.TabIndex = 5;
            numCapacity.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // lblLocation
            // 
            lblLocation.Font = new Font("Segoe UI", 11F);
            lblLocation.ForeColor = Color.FromArgb(45, 55, 72);
            lblLocation.Location = new Point(30, 180);
            lblLocation.Name = "lblLocation";
            lblLocation.Size = new Size(120, 30);
            lblLocation.TabIndex = 6;
            lblLocation.Text = "Khu vực:";
            lblLocation.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtLocation
            // 
            txtLocation.BorderStyle = BorderStyle.FixedSingle;
            txtLocation.Font = new Font("Segoe UI", 11F);
            txtLocation.Location = new Point(160, 180);
            txtLocation.Name = "txtLocation";
            txtLocation.Size = new Size(300, 27);
            txtLocation.TabIndex = 7;
            // 
            // lblImageUrl
            // 
            lblImageUrl.Font = new Font("Segoe UI", 11F);
            lblImageUrl.ForeColor = Color.FromArgb(45, 55, 72);
            lblImageUrl.Location = new Point(30, 230);
            lblImageUrl.Name = "lblImageUrl";
            lblImageUrl.Size = new Size(120, 30);
            lblImageUrl.TabIndex = 8;
            lblImageUrl.Text = "Hình ảnh:";
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
            // picPreview
            // 
            picPreview.BackColor = Color.FromArgb(247, 249, 252);
            picPreview.BorderStyle = BorderStyle.FixedSingle;
            picPreview.Location = new Point(520, 30);
            picPreview.Name = "picPreview";
            picPreview.Size = new Size(200, 160);
            picPreview.SizeMode = PictureBoxSizeMode.Zoom;
            picPreview.TabIndex = 11;
            picPreview.TabStop = false;
            // 
            // frmTables
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(247, 249, 252);
            ClientSize = new Size(1198, 898);
            Controls.Add(pnlMain);
            Controls.Add(pnlSearch);
            Controls.Add(pnlToolbar);
            Controls.Add(pnlHeader);
            Name = "frmTables";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "\U0001fa91 Quản lý Bàn - MilkTea POS";
            WindowState = FormWindowState.Maximized;
            pnlHeader.ResumeLayout(false);
            pnlToolbar.ResumeLayout(false);
            pnlSearch.ResumeLayout(false);
            pnlSearch.PerformLayout();
            pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvTables).EndInit();
            pnlForm.ResumeLayout(false);
            pnlForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numCapacity).EndInit();
            ((System.ComponentModel.ISupportInitialize)picPreview).EndInit();
            ResumeLayout(false);
        }
    }
}
