namespace MilkTeaPOS
{
    partial class frmProducts
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
        private System.Windows.Forms.Label lblSearchLabel;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblFilterCategory;
        private System.Windows.Forms.ComboBox cboFilterCategory;
        private System.Windows.Forms.Label lblFilterStatus;
        private System.Windows.Forms.ComboBox cboFilterStatus;

        // DataGridView
        private System.Windows.Forms.DataGridView dgvProducts;

        // Form fields
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.Label lblBasePrice;
        private System.Windows.Forms.NumericUpDown numBasePrice;
        private System.Windows.Forms.Label lblSizes;
        private System.Windows.Forms.Label lblSizeS;
        private System.Windows.Forms.NumericUpDown numSizeS;
        private System.Windows.Forms.Label lblSizeM;
        private System.Windows.Forms.NumericUpDown numSizeM;
        private System.Windows.Forms.Label lblSizeL;
        private System.Windows.Forms.NumericUpDown numSizeL;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblImageUrl;
        private System.Windows.Forms.TextBox txtImageUrl;
        private System.Windows.Forms.Button btnBrowseImage;
        private System.Windows.Forms.Label lblIsAvailable;
        private System.Windows.Forms.CheckBox chkIsAvailable;
        private System.Windows.Forms.Label lblIsFeatured;
        private System.Windows.Forms.CheckBox chkIsFeatured;
        private System.Windows.Forms.Label lblPreparationTime;
        private System.Windows.Forms.NumericUpDown numPreparationTime;
        private System.Windows.Forms.PictureBox picPreview;
        
        // Barcode controls
        private System.Windows.Forms.Button btnPrintBarcode;
        private System.Windows.Forms.PictureBox picBarcode;

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
            lblSearchLabel = new Label();
            txtSearch = new TextBox();
            lblFilterCategory = new Label();
            cboFilterCategory = new ComboBox();
            lblFilterStatus = new Label();
            cboFilterStatus = new ComboBox();
            pnlMain = new Panel();
            dgvProducts = new DataGridView();
            pnlForm = new Panel();
            lblName = new Label();
            txtName = new TextBox();
            lblCategory = new Label();
            cboCategory = new ComboBox();
            lblBasePrice = new Label();
            numBasePrice = new NumericUpDown();
            lblSizes = new Label();
            numSizeS = new NumericUpDown();
            lblSizeM = new Label();
            numSizeM = new NumericUpDown();
            lblSizeL = new Label();
            numSizeL = new NumericUpDown();
            lblDescription = new Label();
            txtDescription = new TextBox();
            lblImageUrl = new Label();
            txtImageUrl = new TextBox();
            btnBrowseImage = new Button();
            lblIsAvailable = new Label();
            chkIsAvailable = new CheckBox();
            lblIsFeatured = new Label();
            chkIsFeatured = new CheckBox();
            lblPreparationTime = new Label();
            numPreparationTime = new NumericUpDown();
            picPreview = new PictureBox();
            btnPrintBarcode = new Button();
            picBarcode = new PictureBox();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn8 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn9 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn10 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn11 = new DataGridViewTextBoxColumn();
            pnlHeader.SuspendLayout();
            pnlToolbar.SuspendLayout();
            pnlSearch.SuspendLayout();
            pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            pnlForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numBasePrice).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSizeS).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSizeM).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSizeL).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numPreparationTime).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picPreview).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picBarcode).BeginInit();
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
            lblTitle.Text = "\U0001f964 Quản lý sản phẩm";
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
            pnlSearch.Controls.Add(lblSearchLabel);
            pnlSearch.Controls.Add(txtSearch);
            pnlSearch.Controls.Add(lblFilterCategory);
            pnlSearch.Controls.Add(cboFilterCategory);
            pnlSearch.Controls.Add(lblFilterStatus);
            pnlSearch.Controls.Add(cboFilterStatus);
            pnlSearch.Dock = DockStyle.Top;
            pnlSearch.Location = new Point(0, 140);
            pnlSearch.Name = "pnlSearch";
            pnlSearch.Size = new Size(1198, 60);
            pnlSearch.TabIndex = 1;
            // 
            // lblSearchLabel
            // 
            lblSearchLabel.Cursor = Cursors.Hand;
            lblSearchLabel.Font = new Font("Segoe UI", 12F);
            lblSearchLabel.ForeColor = Color.FromArgb(45, 55, 72);
            lblSearchLabel.Location = new Point(20, 20);
            lblSearchLabel.Name = "lblSearchLabel";
            lblSearchLabel.Size = new Size(100, 21);
            lblSearchLabel.TabIndex = 0;
            lblSearchLabel.Text = "🔍 Tìm kiếm:";
            lblSearchLabel.Click += lblSearchLabel_Click;
            // 
            // txtSearch
            // 
            txtSearch.BackColor = Color.FromArgb(247, 249, 252);
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.Font = new Font("Segoe UI", 12F);
            txtSearch.Location = new Point(120, 15);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(250, 29);
            txtSearch.TabIndex = 1;
            txtSearch.KeyPress += txtSearch_KeyPress;
            // 
            // lblFilterCategory
            // 
            lblFilterCategory.Font = new Font("Segoe UI", 12F);
            lblFilterCategory.ForeColor = Color.FromArgb(45, 55, 72);
            lblFilterCategory.Location = new Point(380, 20);
            lblFilterCategory.Name = "lblFilterCategory";
            lblFilterCategory.Size = new Size(90, 21);
            lblFilterCategory.TabIndex = 2;
            lblFilterCategory.Text = "Danh mục:";
            lblFilterCategory.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cboFilterCategory
            // 
            cboFilterCategory.BackColor = Color.FromArgb(247, 249, 252);
            cboFilterCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFilterCategory.FlatStyle = FlatStyle.Flat;
            cboFilterCategory.Font = new Font("Segoe UI", 12F);
            cboFilterCategory.Location = new Point(475, 15);
            cboFilterCategory.Name = "cboFilterCategory";
            cboFilterCategory.Size = new Size(200, 29);
            cboFilterCategory.TabIndex = 3;
            // 
            // lblFilterStatus
            // 
            lblFilterStatus.Font = new Font("Segoe UI", 12F);
            lblFilterStatus.ForeColor = Color.FromArgb(45, 55, 72);
            lblFilterStatus.Location = new Point(680, 20);
            lblFilterStatus.Name = "lblFilterStatus";
            lblFilterStatus.Size = new Size(114, 21);
            lblFilterStatus.TabIndex = 4;
            lblFilterStatus.Text = "Trạng thái:";
            lblFilterStatus.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cboFilterStatus
            // 
            cboFilterStatus.BackColor = Color.FromArgb(247, 249, 252);
            cboFilterStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFilterStatus.FlatStyle = FlatStyle.Flat;
            cboFilterStatus.Font = new Font("Segoe UI", 12F);
            cboFilterStatus.Location = new Point(800, 14);
            cboFilterStatus.Name = "cboFilterStatus";
            cboFilterStatus.Size = new Size(180, 29);
            cboFilterStatus.TabIndex = 5;
            // 
            // pnlMain
            // 
            pnlMain.BackColor = Color.FromArgb(247, 249, 252);
            pnlMain.Controls.Add(dgvProducts);
            pnlMain.Controls.Add(pnlForm);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 200);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new Padding(20);
            pnlMain.Size = new Size(1198, 698);
            pnlMain.TabIndex = 0;
            //
            // dgvProducts
            //
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.AllowUserToDeleteRows = false;
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProducts.BackgroundColor = Color.White;
            dgvProducts.BorderStyle = BorderStyle.None;
            dgvProducts.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvProducts.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                BackColor = Color.FromArgb(45, 55, 72),
                ForeColor = Color.White,
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };
            dgvProducts.ColumnHeadersHeight = 45;
            dgvProducts.DefaultCellStyle = new DataGridViewCellStyle
            {
                Font = new Font("Segoe UI", 11F),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(45, 55, 72),
                SelectionBackColor = Color.FromArgb(255, 107, 107),
                SelectionForeColor = Color.White
            };
            dgvProducts.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(247, 249, 252)
            };
            dgvProducts.Dock = DockStyle.Fill;
            dgvProducts.EnableHeadersVisualStyles = false;
            dgvProducts.GridColor = Color.FromArgb(226, 232, 240);
            dgvProducts.Location = new Point(20, 20);
            dgvProducts.MultiSelect = false;
            dgvProducts.Name = "dgvProducts";
            dgvProducts.ReadOnly = true;
            dgvProducts.RowHeadersVisible = false;
            dgvProducts.RowTemplate.Height = 50;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.Size = new Size(1158, 359);
            dgvProducts.TabIndex = 0;
            dgvProducts.CellClick += dgvProducts_CellClick;
            dgvProducts.CellFormatting += dgvProducts_CellFormatting;
            dgvProducts.DataError += dgvProducts_DataError;
            dgvProducts.Columns.AddRange(new DataGridViewTextBoxColumn[]
            {
                new DataGridViewTextBoxColumn { Name = "Id", DataPropertyName = "Id", Visible = false, ReadOnly = true },
                new DataGridViewTextBoxColumn { Name = "CategoryId", DataPropertyName = "CategoryId", Visible = false, ReadOnly = true },
                new DataGridViewTextBoxColumn { Name = "ImageUrl", DataPropertyName = "ImageUrl", Visible = false, ReadOnly = true },
                new DataGridViewTextBoxColumn { Name = "Name", DataPropertyName = "Name", HeaderText = "Tên sản phẩm", FillWeight = 18, ReadOnly = true },
                new DataGridViewTextBoxColumn { Name = "CategoryName", DataPropertyName = "CategoryName", HeaderText = "Danh mục", FillWeight = 13, ReadOnly = true },
                new DataGridViewTextBoxColumn { Name = "BasePrice", DataPropertyName = "BasePrice", HeaderText = "Base", FillWeight = 8, DefaultCellStyle = new DataGridViewCellStyle { Format = "#,##0", Alignment = DataGridViewContentAlignment.MiddleRight }, ReadOnly = true },
                new DataGridViewTextBoxColumn { Name = "Description", DataPropertyName = "Description", HeaderText = "Mô tả", FillWeight = 20, ReadOnly = true },
                new DataGridViewTextBoxColumn { Name = "IsAvailable", DataPropertyName = "IsAvailable", HeaderText = "Đang bán", FillWeight = 8, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }, ReadOnly = true },
                new DataGridViewTextBoxColumn { Name = "IsFeatured", DataPropertyName = "IsFeatured", HeaderText = "Nổi bật", FillWeight = 8, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }, ReadOnly = true },
                new DataGridViewTextBoxColumn { Name = "PreparationTime", DataPropertyName = "PreparationTime", HeaderText = "TG (phút)", FillWeight = 7, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }, ReadOnly = true },
                new DataGridViewTextBoxColumn { Name = "CreatedAt", DataPropertyName = "CreatedAt", HeaderText = "Ngày tạo", FillWeight = 10, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" }, ReadOnly = true }
            });
            // 
            // pnlForm
            // 
            pnlForm.BackColor = Color.White;
            pnlForm.Controls.Add(lblName);
            pnlForm.Controls.Add(txtName);
            pnlForm.Controls.Add(lblCategory);
            pnlForm.Controls.Add(cboCategory);
            pnlForm.Controls.Add(lblBasePrice);
            pnlForm.Controls.Add(numBasePrice);
            pnlForm.Controls.Add(lblSizes);
            pnlForm.Controls.Add(numSizeS);
            pnlForm.Controls.Add(lblSizeM);
            pnlForm.Controls.Add(numSizeM);
            pnlForm.Controls.Add(lblSizeL);
            pnlForm.Controls.Add(numSizeL);
            pnlForm.Controls.Add(lblDescription);
            pnlForm.Controls.Add(txtDescription);
            pnlForm.Controls.Add(lblImageUrl);
            pnlForm.Controls.Add(txtImageUrl);
            pnlForm.Controls.Add(btnBrowseImage);
            pnlForm.Controls.Add(lblIsAvailable);
            pnlForm.Controls.Add(chkIsAvailable);
            pnlForm.Controls.Add(lblIsFeatured);
            pnlForm.Controls.Add(chkIsFeatured);
            pnlForm.Controls.Add(lblPreparationTime);
            pnlForm.Controls.Add(numPreparationTime);
            pnlForm.Controls.Add(picPreview);
            pnlForm.Controls.Add(btnPrintBarcode);
            pnlForm.Controls.Add(picBarcode);
            pnlForm.Dock = DockStyle.Bottom;
            pnlForm.Location = new Point(20, 379);
            pnlForm.Name = "pnlForm";
            pnlForm.Padding = new Padding(30);
            pnlForm.Size = new Size(1158, 299);
            pnlForm.TabIndex = 1;
            // 
            // lblName
            // 
            lblName.Font = new Font("Segoe UI", 11F);
            lblName.ForeColor = Color.FromArgb(45, 55, 72);
            lblName.Location = new Point(30, 20);
            lblName.Name = "lblName";
            lblName.Size = new Size(120, 30);
            lblName.TabIndex = 0;
            lblName.Text = "Tên sản phẩm:";
            lblName.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtName
            // 
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.Font = new Font("Segoe UI", 11F);
            txtName.Location = new Point(160, 20);
            txtName.Name = "txtName";
            txtName.Size = new Size(300, 27);
            txtName.TabIndex = 1;
            // 
            // lblCategory
            // 
            lblCategory.Font = new Font("Segoe UI", 11F);
            lblCategory.ForeColor = Color.FromArgb(45, 55, 72);
            lblCategory.Location = new Point(30, 60);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(120, 30);
            lblCategory.TabIndex = 2;
            lblCategory.Text = "Danh mục:";
            lblCategory.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cboCategory
            // 
            cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCategory.FlatStyle = FlatStyle.Flat;
            cboCategory.Font = new Font("Segoe UI", 11F);
            cboCategory.Location = new Point(160, 60);
            cboCategory.Name = "cboCategory";
            cboCategory.Size = new Size(300, 28);
            cboCategory.TabIndex = 3;
            // 
            // lblBasePrice
            // 
            lblBasePrice.Font = new Font("Segoe UI", 11F);
            lblBasePrice.ForeColor = Color.FromArgb(45, 55, 72);
            lblBasePrice.Location = new Point(30, 100);
            lblBasePrice.Name = "lblBasePrice";
            lblBasePrice.Size = new Size(120, 30);
            lblBasePrice.TabIndex = 4;
            lblBasePrice.Text = "Giá gốc (VNĐ):";
            lblBasePrice.TextAlign = ContentAlignment.MiddleRight;
            // 
            // numBasePrice
            // 
            numBasePrice.Font = new Font("Segoe UI", 11F);
            numBasePrice.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
            numBasePrice.Location = new Point(160, 100);
            numBasePrice.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            numBasePrice.Name = "numBasePrice";
            numBasePrice.Size = new Size(300, 27);
            numBasePrice.TabIndex = 5;
            numBasePrice.ThousandsSeparator = true;
            // 
            // lblSizes
            // 
            lblSizes.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblSizes.ForeColor = Color.FromArgb(52, 152, 219);
            lblSizes.Location = new Point(30, 140);
            lblSizes.Name = "lblSizes";
            lblSizes.Size = new Size(120, 30);
            lblSizes.TabIndex = 18;
            lblSizes.Text = "Giá theo size:";
            lblSizes.TextAlign = ContentAlignment.MiddleRight;
            // 
            // numSizeS
            // 
            numSizeS.Font = new Font("Segoe UI", 10F);
            numSizeS.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
            numSizeS.Location = new Point(195, 140);
            numSizeS.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            numSizeS.Name = "numSizeS";
            numSizeS.Size = new Size(80, 25);
            numSizeS.TabIndex = 20;
            numSizeS.ThousandsSeparator = true;
            numSizeS.Value = new decimal(new int[] { 45000, 0, 0, 0 });
            // 
            // lblSizeM
            // 
            lblSizeM.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSizeM.ForeColor = Color.FromArgb(52, 152, 219);
            lblSizeM.Location = new Point(285, 140);
            lblSizeM.Name = "lblSizeM";
            lblSizeM.Size = new Size(35, 28);
            lblSizeM.TabIndex = 21;
            lblSizeM.Text = "M:";
            lblSizeM.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // numSizeM
            // 
            numSizeM.Font = new Font("Segoe UI", 10F);
            numSizeM.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
            numSizeM.Location = new Point(320, 140);
            numSizeM.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            numSizeM.Name = "numSizeM";
            numSizeM.Size = new Size(80, 25);
            numSizeM.TabIndex = 22;
            numSizeM.ThousandsSeparator = true;
            numSizeM.Value = new decimal(new int[] { 49000, 0, 0, 0 });
            // 
            // lblSizeL
            // 
            lblSizeL.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSizeL.ForeColor = Color.FromArgb(155, 89, 182);
            lblSizeL.Location = new Point(410, 140);
            lblSizeL.Name = "lblSizeL";
            lblSizeL.Size = new Size(35, 28);
            lblSizeL.TabIndex = 23;
            lblSizeL.Text = "L:";
            lblSizeL.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // numSizeL
            // 
            numSizeL.Font = new Font("Segoe UI", 10F);
            numSizeL.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
            numSizeL.Location = new Point(445, 140);
            numSizeL.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            numSizeL.Name = "numSizeL";
            numSizeL.Size = new Size(80, 25);
            numSizeL.TabIndex = 24;
            numSizeL.ThousandsSeparator = true;
            numSizeL.Value = new decimal(new int[] { 55000, 0, 0, 0 });
            // 
            // lblDescription
            // 
            lblDescription.Font = new Font("Segoe UI", 11F);
            lblDescription.ForeColor = Color.FromArgb(45, 55, 72);
            lblDescription.Location = new Point(30, 180);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(120, 30);
            lblDescription.TabIndex = 6;
            lblDescription.Text = "Mô tả:";
            lblDescription.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtDescription
            // 
            txtDescription.BorderStyle = BorderStyle.FixedSingle;
            txtDescription.Font = new Font("Segoe UI", 11F);
            txtDescription.Location = new Point(160, 180);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(300, 27);
            txtDescription.TabIndex = 7;
            // 
            // lblImageUrl
            // 
            lblImageUrl.Font = new Font("Segoe UI", 11F);
            lblImageUrl.ForeColor = Color.FromArgb(45, 55, 72);
            lblImageUrl.Location = new Point(30, 220);
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
            txtImageUrl.Location = new Point(160, 220);
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
            btnBrowseImage.Location = new Point(390, 220);
            btnBrowseImage.Name = "btnBrowseImage";
            btnBrowseImage.Size = new Size(70, 30);
            btnBrowseImage.TabIndex = 10;
            btnBrowseImage.Text = "📁 Duyệt";
            btnBrowseImage.UseVisualStyleBackColor = false;
            btnBrowseImage.Click += btnBrowseImage_Click;
            // 
            // lblIsAvailable
            // 
            lblIsAvailable.Font = new Font("Segoe UI", 11F);
            lblIsAvailable.ForeColor = Color.FromArgb(45, 55, 72);
            lblIsAvailable.Location = new Point(597, 19);
            lblIsAvailable.Name = "lblIsAvailable";
            lblIsAvailable.Size = new Size(91, 30);
            lblIsAvailable.TabIndex = 11;
            lblIsAvailable.Text = "Trạng thái:";
            lblIsAvailable.TextAlign = ContentAlignment.MiddleRight;
            // 
            // chkIsAvailable
            // 
            chkIsAvailable.Checked = true;
            chkIsAvailable.CheckState = CheckState.Checked;
            chkIsAvailable.Font = new Font("Segoe UI", 11F);
            chkIsAvailable.ForeColor = Color.FromArgb(45, 55, 72);
            chkIsAvailable.Location = new Point(697, 24);
            chkIsAvailable.Name = "chkIsAvailable";
            chkIsAvailable.Size = new Size(100, 25);
            chkIsAvailable.TabIndex = 12;
            chkIsAvailable.Text = "Đang bán";
            // 
            // lblIsFeatured
            // 
            lblIsFeatured.Font = new Font("Segoe UI", 11F);
            lblIsFeatured.ForeColor = Color.FromArgb(45, 55, 72);
            lblIsFeatured.Location = new Point(597, 59);
            lblIsFeatured.Name = "lblIsFeatured";
            lblIsFeatured.Size = new Size(91, 30);
            lblIsFeatured.TabIndex = 13;
            lblIsFeatured.Text = "Nổi bật:";
            lblIsFeatured.TextAlign = ContentAlignment.MiddleRight;
            // 
            // chkIsFeatured
            // 
            chkIsFeatured.Font = new Font("Segoe UI", 11F);
            chkIsFeatured.ForeColor = Color.FromArgb(45, 55, 72);
            chkIsFeatured.Location = new Point(697, 64);
            chkIsFeatured.Name = "chkIsFeatured";
            chkIsFeatured.Size = new Size(150, 25);
            chkIsFeatured.TabIndex = 14;
            chkIsFeatured.Text = "Sản phẩm nổi bật";
            // 
            // lblPreparationTime
            // 
            lblPreparationTime.Font = new Font("Segoe UI", 11F);
            lblPreparationTime.ForeColor = Color.FromArgb(45, 55, 72);
            lblPreparationTime.Location = new Point(597, 99);
            lblPreparationTime.Name = "lblPreparationTime";
            lblPreparationTime.Size = new Size(91, 30);
            lblPreparationTime.TabIndex = 15;
            lblPreparationTime.Text = "TG chuẩn bị:";
            lblPreparationTime.TextAlign = ContentAlignment.MiddleRight;
            // 
            // numPreparationTime
            // 
            numPreparationTime.Font = new Font("Segoe UI", 11F);
            numPreparationTime.Location = new Point(697, 99);
            numPreparationTime.Maximum = new decimal(new int[] { 120, 0, 0, 0 });
            numPreparationTime.Name = "numPreparationTime";
            numPreparationTime.Size = new Size(150, 27);
            numPreparationTime.TabIndex = 16;
            numPreparationTime.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // picPreview
            // 
            picPreview.BackColor = Color.FromArgb(247, 249, 252);
            picPreview.Location = new Point(647, 140);
            picPreview.Name = "picPreview";
            picPreview.Size = new Size(200, 140);
            picPreview.SizeMode = PictureBoxSizeMode.Zoom;
            picPreview.TabIndex = 17;
            picPreview.TabStop = false;
            // 
            // btnPrintBarcode
            // 
            btnPrintBarcode.BackColor = Color.FromArgb(46, 204, 113);
            btnPrintBarcode.Cursor = Cursors.Hand;
            btnPrintBarcode.FlatAppearance.BorderSize = 0;
            btnPrintBarcode.FlatStyle = FlatStyle.Flat;
            btnPrintBarcode.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnPrintBarcode.ForeColor = Color.White;
            btnPrintBarcode.Location = new Point(888, 24);
            btnPrintBarcode.Name = "btnPrintBarcode";
            btnPrintBarcode.Size = new Size(250, 35);
            btnPrintBarcode.TabIndex = 25;
            btnPrintBarcode.Text = "🖨️ In mã barcode";
            btnPrintBarcode.UseVisualStyleBackColor = false;
            btnPrintBarcode.Click += btnPrintBarcode_Click;
            // 
            // picBarcode
            // 
            picBarcode.BackColor = Color.White;
            picBarcode.Location = new Point(888, 64);
            picBarcode.Name = "picBarcode";
            picBarcode.Size = new Size(250, 120);
            picBarcode.SizeMode = PictureBoxSizeMode.Zoom;
            picBarcode.TabIndex = 26;
            picBarcode.TabStop = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn8
            // 
            dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            dataGridViewTextBoxColumn8.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn9
            // 
            dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            dataGridViewTextBoxColumn9.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn10
            // 
            dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            dataGridViewTextBoxColumn10.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn11
            // 
            dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            dataGridViewTextBoxColumn11.ReadOnly = true;
            // 
            // frmProducts
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(247, 249, 252);
            ClientSize = new Size(1198, 898);
            Controls.Add(pnlMain);
            Controls.Add(pnlSearch);
            Controls.Add(pnlToolbar);
            Controls.Add(pnlHeader);
            Name = "frmProducts";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "\U0001f964 Quản lý sản phẩm - MilkTea POS";
            WindowState = FormWindowState.Maximized;
            pnlHeader.ResumeLayout(false);
            pnlToolbar.ResumeLayout(false);
            pnlSearch.ResumeLayout(false);
            pnlSearch.PerformLayout();
            pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            pnlForm.ResumeLayout(false);
            pnlForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numBasePrice).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSizeS).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSizeM).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSizeL).EndInit();
            ((System.ComponentModel.ISupportInitialize)numPreparationTime).EndInit();
            ((System.ComponentModel.ISupportInitialize)picPreview).EndInit();
            ((System.ComponentModel.ISupportInitialize)picBarcode).EndInit();
            ResumeLayout(false);
        }

        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
    }
}
