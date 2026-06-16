namespace MilkTeaPOS
{
    partial class frmVouchers
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            pnlHeader = new Panel();
            lblTitle = new Label();
            pnlMain = new Panel();
            pnlGrid = new Panel();
            dgvVouchers = new DataGridView();
            pnlRight = new Panel();
            pnlFormContainer = new Panel();
            lblFormTitle = new Label();
            pnlFormFields = new Panel();
            cbStatus = new ComboBox();
            lblStatus = new Label();
            dtpValidUntil = new DateTimePicker();
            lblValidUntil = new Label();
            dtpValidFrom = new DateTimePicker();
            lblValidFrom = new Label();
            txtMaxDiscount = new TextBox();
            lblMaxDiscount = new Label();
            txtUsageLimit = new TextBox();
            lblUsageLimit = new Label();
            txtMinOrder = new TextBox();
            lblMinOrder = new Label();
            txtDiscountValue = new TextBox();
            lblDiscountValue = new Label();
            cbVoucherType = new ComboBox();
            lblVoucherType = new Label();
            txtDescription = new TextBox();
            lblDescription = new Label();
            txtName = new TextBox();
            lblName = new Label();
            txtCode = new TextBox();
            lblCode = new Label();
            pnlFormButtons = new Panel();
            btnRefresh = new Button();
            btnEdit = new Button();
            btnAdd = new Button();
            btnDelete = new Button();
            pnlSearchFilter = new Panel();
            btnFilter = new Button();
            txtSearch = new TextBox();
            cbFilterStatus = new ComboBox();
            pnlStats = new Panel();
            statTotal = new Panel();
            lblTotalIcon = new Label();
            lblTotalValue = new Label();
            lblTotalLabel = new Label();
            statActive = new Panel();
            lblActiveIcon = new Label();
            lblActiveValue = new Label();
            lblActiveLabel = new Label();
            statExpired = new Panel();
            lblExpiredIcon = new Label();
            lblExpiredValue = new Label();
            lblExpiredLabel = new Label();
            statUsedUp = new Panel();
            lblUsedUpIcon = new Label();
            lblUsedUpValue = new Label();
            lblUsedUpLabel = new Label();
            pnlHeader.SuspendLayout();
            pnlMain.SuspendLayout();
            pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVouchers).BeginInit();
            pnlRight.SuspendLayout();
            pnlFormContainer.SuspendLayout();
            pnlFormFields.SuspendLayout();
            pnlFormButtons.SuspendLayout();
            pnlSearchFilter.SuspendLayout();
            pnlStats.SuspendLayout();
            statTotal.SuspendLayout();
            statActive.SuspendLayout();
            statExpired.SuspendLayout();
            statUsedUp.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(44, 62, 80);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1600, 75);
            pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(25, 18);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(299, 41);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "QUẢN LÝ VOUCHER";
            // 
            // pnlMain
            // 
            pnlMain.BackColor = Color.FromArgb(246, 248, 250);
            pnlMain.Controls.Add(pnlGrid);
            pnlMain.Controls.Add(pnlRight);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 225);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new Padding(15);
            pnlMain.Size = new Size(1600, 675);
            pnlMain.TabIndex = 3;
            // 
            // pnlGrid
            // 
            pnlGrid.Controls.Add(dgvVouchers);
            pnlGrid.Dock = DockStyle.Fill;
            pnlGrid.Location = new Point(15, 15);
            pnlGrid.Name = "pnlGrid";
            pnlGrid.Size = new Size(1025, 645);
            pnlGrid.TabIndex = 0;
            // 
            // dgvVouchers
            // 
            dgvVouchers.AllowUserToAddRows = false;
            dgvVouchers.AllowUserToDeleteRows = false;
            dgvVouchers.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.White;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(45, 55, 72);
            dataGridViewCellStyle1.Padding = new Padding(5, 8, 5, 8);
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dgvVouchers.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvVouchers.BackgroundColor = Color.FromArgb(246, 248, 250);
            dgvVouchers.BorderStyle = BorderStyle.None;
            dgvVouchers.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvVouchers.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(52, 152, 219);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.Padding = new Padding(5, 10, 5, 10);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(41, 128, 185);
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvVouchers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvVouchers.ColumnHeadersHeight = 45;
            dgvVouchers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(45, 55, 72);
            dataGridViewCellStyle3.Padding = new Padding(5, 8, 5, 8);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dataGridViewCellStyle3.SelectionForeColor = Color.White;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvVouchers.DefaultCellStyle = dataGridViewCellStyle3;
            dgvVouchers.Dock = DockStyle.Fill;
            dgvVouchers.EnableHeadersVisualStyles = false;
            dgvVouchers.GridColor = Color.FromArgb(233, 236, 239);
            dgvVouchers.Location = new Point(0, 0);
            dgvVouchers.MultiSelect = false;
            dgvVouchers.Name = "dgvVouchers";
            dgvVouchers.ReadOnly = true;
            dgvVouchers.RowHeadersVisible = false;
            dgvVouchers.RowHeadersWidth = 50;
            dgvVouchers.RowTemplate.Height = 40;
            dgvVouchers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVouchers.Size = new Size(1025, 645);
            dgvVouchers.StandardTab = true;
            dgvVouchers.TabIndex = 0;
            dgvVouchers.CellClick += dgvVouchers_CellClick;
            dgvVouchers.CellDoubleClick += dgvVouchers_CellDoubleClick;
            dgvVouchers.CellFormatting += dgvVouchers_CellFormatting;
            // 
            // pnlRight
            // 
            pnlRight.Controls.Add(pnlFormContainer);
            pnlRight.Dock = DockStyle.Right;
            pnlRight.Location = new Point(1040, 15);
            pnlRight.Name = "pnlRight";
            pnlRight.Padding = new Padding(0, 0, 0, 10);
            pnlRight.Size = new Size(545, 645);
            pnlRight.TabIndex = 1;
            // 
            // pnlFormContainer
            // 
            pnlFormContainer.BackColor = Color.White;
            pnlFormContainer.Controls.Add(lblFormTitle);
            pnlFormContainer.Controls.Add(pnlFormFields);
            pnlFormContainer.Controls.Add(pnlFormButtons);
            pnlFormContainer.Dock = DockStyle.Fill;
            pnlFormContainer.Location = new Point(0, 0);
            pnlFormContainer.Name = "pnlFormContainer";
            pnlFormContainer.Size = new Size(545, 635);
            pnlFormContainer.TabIndex = 0;
            // 
            // lblFormTitle
            // 
            lblFormTitle.BackColor = Color.FromArgb(52, 152, 219);
            lblFormTitle.Dock = DockStyle.Top;
            lblFormTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblFormTitle.ForeColor = Color.White;
            lblFormTitle.Location = new Point(0, 0);
            lblFormTitle.Name = "lblFormTitle";
            lblFormTitle.Padding = new Padding(20, 12, 20, 12);
            lblFormTitle.Size = new Size(545, 45);
            lblFormTitle.TabIndex = 0;
            lblFormTitle.Text = "THÔNG TIN VOUCHER";
            lblFormTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlFormFields
            // 
            pnlFormFields.AutoScroll = true;
            pnlFormFields.Controls.Add(cbStatus);
            pnlFormFields.Controls.Add(lblStatus);
            pnlFormFields.Controls.Add(dtpValidUntil);
            pnlFormFields.Controls.Add(lblValidUntil);
            pnlFormFields.Controls.Add(dtpValidFrom);
            pnlFormFields.Controls.Add(lblValidFrom);
            pnlFormFields.Controls.Add(txtMaxDiscount);
            pnlFormFields.Controls.Add(lblMaxDiscount);
            pnlFormFields.Controls.Add(txtUsageLimit);
            pnlFormFields.Controls.Add(lblUsageLimit);
            pnlFormFields.Controls.Add(txtMinOrder);
            pnlFormFields.Controls.Add(lblMinOrder);
            pnlFormFields.Controls.Add(txtDiscountValue);
            pnlFormFields.Controls.Add(lblDiscountValue);
            pnlFormFields.Controls.Add(cbVoucherType);
            pnlFormFields.Controls.Add(lblVoucherType);
            pnlFormFields.Controls.Add(txtDescription);
            pnlFormFields.Controls.Add(lblDescription);
            pnlFormFields.Controls.Add(txtName);
            pnlFormFields.Controls.Add(lblName);
            pnlFormFields.Controls.Add(txtCode);
            pnlFormFields.Controls.Add(lblCode);
            pnlFormFields.Dock = DockStyle.Fill;
            pnlFormFields.Location = new Point(0, 0);
            pnlFormFields.Name = "pnlFormFields";
            pnlFormFields.Padding = new Padding(35, 50, 35, 30);
            pnlFormFields.Size = new Size(545, 570);
            pnlFormFields.TabIndex = 1;
            // 
            // cbStatus
            // 
            cbStatus.Dock = DockStyle.Top;
            cbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cbStatus.Font = new Font("Segoe UI", 11F);
            cbStatus.FormattingEnabled = true;
            cbStatus.Location = new Point(35, 941);
            cbStatus.Name = "cbStatus";
            cbStatus.Size = new Size(458, 28);
            cbStatus.TabIndex = 12;
            cbStatus.KeyPress += cbStatus_KeyPress;
            // 
            // lblStatus
            // 
            lblStatus.Dock = DockStyle.Top;
            lblStatus.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblStatus.ForeColor = Color.FromArgb(52, 73, 94);
            lblStatus.Location = new Point(35, 893);
            lblStatus.Name = "lblStatus";
            lblStatus.Padding = new Padding(0, 20, 0, 10);
            lblStatus.Size = new Size(458, 48);
            lblStatus.TabIndex = 22;
            lblStatus.Text = "Trạng thái";
            // 
            // dtpValidUntil
            // 
            dtpValidUntil.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpValidUntil.Dock = DockStyle.Top;
            dtpValidUntil.Font = new Font("Segoe UI", 11F);
            dtpValidUntil.Format = DateTimePickerFormat.Custom;
            dtpValidUntil.Location = new Point(35, 866);
            dtpValidUntil.Name = "dtpValidUntil";
            dtpValidUntil.Size = new Size(458, 27);
            dtpValidUntil.TabIndex = 11;
            dtpValidUntil.KeyPress += dtpValidUntil_KeyPress;
            // 
            // lblValidUntil
            // 
            lblValidUntil.Dock = DockStyle.Top;
            lblValidUntil.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblValidUntil.ForeColor = Color.FromArgb(52, 73, 94);
            lblValidUntil.Location = new Point(35, 817);
            lblValidUntil.Name = "lblValidUntil";
            lblValidUntil.Padding = new Padding(0, 20, 0, 10);
            lblValidUntil.Size = new Size(458, 49);
            lblValidUntil.TabIndex = 20;
            lblValidUntil.Text = "Ngày hết hạn";
            // 
            // dtpValidFrom
            // 
            dtpValidFrom.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpValidFrom.Dock = DockStyle.Top;
            dtpValidFrom.Font = new Font("Segoe UI", 11F);
            dtpValidFrom.Format = DateTimePickerFormat.Custom;
            dtpValidFrom.Location = new Point(35, 790);
            dtpValidFrom.Name = "dtpValidFrom";
            dtpValidFrom.Size = new Size(458, 27);
            dtpValidFrom.TabIndex = 10;
            dtpValidFrom.KeyPress += dtpValidFrom_KeyPress;
            // 
            // lblValidFrom
            // 
            lblValidFrom.Dock = DockStyle.Top;
            lblValidFrom.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblValidFrom.ForeColor = Color.FromArgb(52, 73, 94);
            lblValidFrom.Location = new Point(35, 738);
            lblValidFrom.Name = "lblValidFrom";
            lblValidFrom.Padding = new Padding(0, 20, 0, 10);
            lblValidFrom.Size = new Size(458, 52);
            lblValidFrom.TabIndex = 18;
            lblValidFrom.Text = "Ngày bắt đầu";
            // 
            // txtMaxDiscount
            // 
            txtMaxDiscount.Dock = DockStyle.Top;
            txtMaxDiscount.Font = new Font("Segoe UI", 11F);
            txtMaxDiscount.Location = new Point(35, 711);
            txtMaxDiscount.Name = "txtMaxDiscount";
            txtMaxDiscount.PlaceholderText = "Để trống nếu không giới hạn";
            txtMaxDiscount.Size = new Size(458, 27);
            txtMaxDiscount.TabIndex = 8;
            txtMaxDiscount.KeyPress += txtMaxDiscount_KeyPress;
            // 
            // lblMaxDiscount
            // 
            lblMaxDiscount.Dock = DockStyle.Top;
            lblMaxDiscount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblMaxDiscount.ForeColor = Color.FromArgb(52, 73, 94);
            lblMaxDiscount.Location = new Point(35, 663);
            lblMaxDiscount.Name = "lblMaxDiscount";
            lblMaxDiscount.Padding = new Padding(0, 20, 0, 10);
            lblMaxDiscount.Size = new Size(458, 48);
            lblMaxDiscount.TabIndex = 14;
            lblMaxDiscount.Text = "Giảm tối đa (VNĐ)";
            // 
            // txtUsageLimit
            // 
            txtUsageLimit.Dock = DockStyle.Top;
            txtUsageLimit.Font = new Font("Segoe UI", 11F);
            txtUsageLimit.Location = new Point(35, 636);
            txtUsageLimit.Name = "txtUsageLimit";
            txtUsageLimit.PlaceholderText = "Để trống nếu không giới hạn";
            txtUsageLimit.Size = new Size(458, 27);
            txtUsageLimit.TabIndex = 7;
            txtUsageLimit.KeyPress += txtUsageLimit_KeyPress;
            // 
            // lblUsageLimit
            // 
            lblUsageLimit.Dock = DockStyle.Top;
            lblUsageLimit.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUsageLimit.ForeColor = Color.FromArgb(52, 73, 94);
            lblUsageLimit.Location = new Point(35, 588);
            lblUsageLimit.Name = "lblUsageLimit";
            lblUsageLimit.Padding = new Padding(0, 20, 0, 10);
            lblUsageLimit.Size = new Size(458, 48);
            lblUsageLimit.TabIndex = 12;
            lblUsageLimit.Text = "Giới hạn sử dụng";
            // 
            // txtMinOrder
            // 
            txtMinOrder.Dock = DockStyle.Top;
            txtMinOrder.Font = new Font("Segoe UI", 11F);
            txtMinOrder.Location = new Point(35, 561);
            txtMinOrder.Name = "txtMinOrder";
            txtMinOrder.PlaceholderText = "Để trống nếu không yêu cầu";
            txtMinOrder.Size = new Size(458, 27);
            txtMinOrder.TabIndex = 6;
            txtMinOrder.KeyPress += txtMinOrder_KeyPress;
            // 
            // lblMinOrder
            // 
            lblMinOrder.Dock = DockStyle.Top;
            lblMinOrder.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblMinOrder.ForeColor = Color.FromArgb(52, 73, 94);
            lblMinOrder.Location = new Point(35, 513);
            lblMinOrder.Name = "lblMinOrder";
            lblMinOrder.Padding = new Padding(0, 20, 0, 10);
            lblMinOrder.Size = new Size(458, 48);
            lblMinOrder.TabIndex = 10;
            lblMinOrder.Text = "Đơn tối thiểu (VNĐ)";
            // 
            // txtDiscountValue
            // 
            txtDiscountValue.Dock = DockStyle.Top;
            txtDiscountValue.Font = new Font("Segoe UI", 11F);
            txtDiscountValue.Location = new Point(35, 486);
            txtDiscountValue.Name = "txtDiscountValue";
            txtDiscountValue.PlaceholderText = "VD: 10 (cho %) hoặc 10000 (cho VNĐ)";
            txtDiscountValue.Size = new Size(458, 27);
            txtDiscountValue.TabIndex = 5;
            txtDiscountValue.KeyPress += txtDiscountValue_KeyPress;
            // 
            // lblDiscountValue
            // 
            lblDiscountValue.Dock = DockStyle.Top;
            lblDiscountValue.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDiscountValue.ForeColor = Color.FromArgb(52, 73, 94);
            lblDiscountValue.Location = new Point(35, 435);
            lblDiscountValue.Name = "lblDiscountValue";
            lblDiscountValue.Padding = new Padding(0, 20, 0, 10);
            lblDiscountValue.Size = new Size(458, 51);
            lblDiscountValue.TabIndex = 8;
            lblDiscountValue.Text = "Giá trị giảm";
            // 
            // cbVoucherType
            // 
            cbVoucherType.Dock = DockStyle.Top;
            cbVoucherType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbVoucherType.Font = new Font("Segoe UI", 11F);
            cbVoucherType.FormattingEnabled = true;
            cbVoucherType.Location = new Point(35, 407);
            cbVoucherType.Name = "cbVoucherType";
            cbVoucherType.Size = new Size(458, 28);
            cbVoucherType.TabIndex = 4;
            cbVoucherType.KeyPress += cbVoucherType_KeyPress;
            // 
            // lblVoucherType
            // 
            lblVoucherType.Dock = DockStyle.Top;
            lblVoucherType.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblVoucherType.ForeColor = Color.FromArgb(52, 73, 94);
            lblVoucherType.Location = new Point(35, 359);
            lblVoucherType.Name = "lblVoucherType";
            lblVoucherType.Padding = new Padding(0, 20, 0, 10);
            lblVoucherType.Size = new Size(458, 48);
            lblVoucherType.TabIndex = 6;
            lblVoucherType.Text = "Loại voucher";
            // 
            // txtDescription
            // 
            txtDescription.Dock = DockStyle.Top;
            txtDescription.Font = new Font("Segoe UI", 11F);
            txtDescription.Location = new Point(35, 259);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.PlaceholderText = "VD: Giảm giá cho khách mới đăng ký";
            txtDescription.Size = new Size(458, 100);
            txtDescription.TabIndex = 3;
            txtDescription.KeyPress += txtDescription_KeyPress;
            // 
            // lblDescription
            // 
            lblDescription.Dock = DockStyle.Top;
            lblDescription.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDescription.ForeColor = Color.FromArgb(52, 73, 94);
            lblDescription.Location = new Point(35, 212);
            lblDescription.Name = "lblDescription";
            lblDescription.Padding = new Padding(0, 20, 0, 10);
            lblDescription.Size = new Size(458, 47);
            lblDescription.TabIndex = 4;
            lblDescription.Text = "Mô tả";
            // 
            // txtName
            // 
            txtName.Dock = DockStyle.Top;
            txtName.Font = new Font("Segoe UI", 11F);
            txtName.Location = new Point(35, 185);
            txtName.Name = "txtName";
            txtName.PlaceholderText = "VD: Giảm 20% cho khách mới";
            txtName.Size = new Size(458, 27);
            txtName.TabIndex = 2;
            txtName.KeyPress += txtName_KeyPress;
            // 
            // lblName
            // 
            lblName.Dock = DockStyle.Top;
            lblName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblName.ForeColor = Color.FromArgb(52, 73, 94);
            lblName.Location = new Point(35, 134);
            lblName.Name = "lblName";
            lblName.Padding = new Padding(0, 20, 0, 10);
            lblName.Size = new Size(458, 51);
            lblName.TabIndex = 2;
            lblName.Text = "Tên voucher";
            // 
            // txtCode
            // 
            txtCode.Dock = DockStyle.Top;
            txtCode.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            txtCode.Location = new Point(35, 107);
            txtCode.Name = "txtCode";
            txtCode.PlaceholderText = "VD: WELCOME20, SUMMER_SALE";
            txtCode.Size = new Size(458, 27);
            txtCode.TabIndex = 1;
            txtCode.KeyPress += txtCode_KeyPress;
            // 
            // lblCode
            // 
            lblCode.Dock = DockStyle.Top;
            lblCode.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCode.ForeColor = Color.FromArgb(52, 73, 94);
            lblCode.Location = new Point(35, 50);
            lblCode.Name = "lblCode";
            lblCode.Padding = new Padding(0, 20, 0, 10);
            lblCode.Size = new Size(458, 57);
            lblCode.TabIndex = 0;
            lblCode.Text = "Mã voucher";
            // 
            // pnlFormButtons
            // 
            pnlFormButtons.BackColor = Color.FromArgb(246, 248, 250);
            pnlFormButtons.Controls.Add(btnRefresh);
            pnlFormButtons.Controls.Add(btnEdit);
            pnlFormButtons.Controls.Add(btnAdd);
            pnlFormButtons.Controls.Add(btnDelete);
            pnlFormButtons.Dock = DockStyle.Bottom;
            pnlFormButtons.Location = new Point(0, 570);
            pnlFormButtons.Name = "pnlFormButtons";
            pnlFormButtons.Padding = new Padding(15);
            pnlFormButtons.Size = new Size(545, 65);
            pnlFormButtons.TabIndex = 2;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.FromArgb(149, 165, 166);
            btnRefresh.Cursor = Cursors.Hand;
            btnRefresh.Dock = DockStyle.Left;
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(255, 15);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(120, 35);
            btnRefresh.TabIndex = 15;
            btnRefresh.Text = "Làm mới";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // btnEdit
            // 
            btnEdit.BackColor = Color.FromArgb(243, 156, 18);
            btnEdit.Cursor = Cursors.Hand;
            btnEdit.Dock = DockStyle.Left;
            btnEdit.FlatAppearance.BorderSize = 0;
            btnEdit.FlatStyle = FlatStyle.Flat;
            btnEdit.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnEdit.ForeColor = Color.White;
            btnEdit.Location = new Point(135, 15);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(120, 35);
            btnEdit.TabIndex = 14;
            btnEdit.Text = "Sửa";
            btnEdit.UseVisualStyleBackColor = false;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.FromArgb(46, 204, 113);
            btnAdd.Cursor = Cursors.Hand;
            btnAdd.Dock = DockStyle.Left;
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(15, 15);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(120, 35);
            btnAdd.TabIndex = 13;
            btnAdd.Text = "Thêm";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.FromArgb(231, 76, 60);
            btnDelete.Cursor = Cursors.Hand;
            btnDelete.Dock = DockStyle.Right;
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnDelete.ForeColor = Color.White;
            btnDelete.Location = new Point(410, 15);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(120, 35);
            btnDelete.TabIndex = 16;
            btnDelete.Text = "Xóa";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // pnlSearchFilter
            // 
            pnlSearchFilter.BackColor = Color.White;
            pnlSearchFilter.Controls.Add(btnFilter);
            pnlSearchFilter.Controls.Add(txtSearch);
            pnlSearchFilter.Controls.Add(cbFilterStatus);
            pnlSearchFilter.Dock = DockStyle.Top;
            pnlSearchFilter.Location = new Point(0, 75);
            pnlSearchFilter.Name = "pnlSearchFilter";
            pnlSearchFilter.Padding = new Padding(15, 12, 15, 12);
            pnlSearchFilter.Size = new Size(1600, 60);
            pnlSearchFilter.TabIndex = 1;
            // 
            // btnFilter
            // 
            btnFilter.BackColor = Color.FromArgb(52, 152, 219);
            btnFilter.Cursor = Cursors.Hand;
            btnFilter.FlatAppearance.BorderSize = 0;
            btnFilter.FlatStyle = FlatStyle.Flat;
            btnFilter.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnFilter.ForeColor = Color.White;
            btnFilter.Location = new Point(1190, 13);
            btnFilter.Name = "btnFilter";
            btnFilter.Size = new Size(110, 35);
            btnFilter.TabIndex = 17;
            btnFilter.Text = "Tìm kiếm";
            btnFilter.UseVisualStyleBackColor = false;
            btnFilter.Click += btnFilter_Click;
            // 
            // txtSearch
            // 
            txtSearch.Font = new Font("Segoe UI", 11F);
            txtSearch.Location = new Point(584, 18);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Tìm theo mã, tên, mô tả voucher...";
            txtSearch.Size = new Size(600, 27);
            txtSearch.TabIndex = 14;
            txtSearch.KeyPress += txtSearch_KeyPress;
            // 
            // cbFilterStatus
            // 
            cbFilterStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cbFilterStatus.Font = new Font("Segoe UI", 11F);
            cbFilterStatus.FormattingEnabled = true;
            cbFilterStatus.Location = new Point(1313, 17);
            cbFilterStatus.Name = "cbFilterStatus";
            cbFilterStatus.Size = new Size(220, 28);
            cbFilterStatus.TabIndex = 15;
            cbFilterStatus.SelectedIndexChanged += cbFilterStatus_SelectedIndexChanged;
            // 
            // pnlStats
            // 
            pnlStats.BackColor = Color.White;
            pnlStats.Controls.Add(statTotal);
            pnlStats.Controls.Add(statActive);
            pnlStats.Controls.Add(statExpired);
            pnlStats.Controls.Add(statUsedUp);
            pnlStats.Dock = DockStyle.Top;
            pnlStats.Location = new Point(0, 135);
            pnlStats.Name = "pnlStats";
            pnlStats.Padding = new Padding(15);
            pnlStats.Size = new Size(1600, 90);
            pnlStats.TabIndex = 2;
            // 
            // statTotal
            // 
            statTotal.BackColor = Color.FromArgb(52, 152, 219);
            statTotal.Controls.Add(lblTotalIcon);
            statTotal.Controls.Add(lblTotalValue);
            statTotal.Controls.Add(lblTotalLabel);
            statTotal.Dock = DockStyle.Left;
            statTotal.Location = new Point(675, 15);
            statTotal.Name = "statTotal";
            statTotal.Size = new Size(220, 60);
            statTotal.TabIndex = 0;
            // 
            // lblTotalIcon
            // 
            lblTotalIcon.AutoSize = true;
            lblTotalIcon.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTotalIcon.ForeColor = Color.White;
            lblTotalIcon.Location = new Point(10, 12);
            lblTotalIcon.Name = "lblTotalIcon";
            lblTotalIcon.Size = new Size(34, 25);
            lblTotalIcon.TabIndex = 0;
            lblTotalIcon.Text = "🎫";
            // 
            // lblTotalValue
            // 
            lblTotalValue.AutoSize = true;
            lblTotalValue.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTotalValue.ForeColor = Color.White;
            lblTotalValue.Location = new Point(50, 0);
            lblTotalValue.Name = "lblTotalValue";
            lblTotalValue.Size = new Size(33, 37);
            lblTotalValue.TabIndex = 1;
            lblTotalValue.Text = "0";
            // 
            // lblTotalLabel
            // 
            lblTotalLabel.Font = new Font("Segoe UI", 9F);
            lblTotalLabel.ForeColor = Color.White;
            lblTotalLabel.Location = new Point(15, 40);
            lblTotalLabel.Name = "lblTotalLabel";
            lblTotalLabel.Size = new Size(180, 20);
            lblTotalLabel.TabIndex = 2;
            lblTotalLabel.Text = "Tổng";
            // 
            // statActive
            // 
            statActive.BackColor = Color.FromArgb(46, 204, 113);
            statActive.Controls.Add(lblActiveIcon);
            statActive.Controls.Add(lblActiveValue);
            statActive.Controls.Add(lblActiveLabel);
            statActive.Dock = DockStyle.Left;
            statActive.Location = new Point(455, 15);
            statActive.Name = "statActive";
            statActive.Size = new Size(220, 60);
            statActive.TabIndex = 1;
            // 
            // lblActiveIcon
            // 
            lblActiveIcon.AutoSize = true;
            lblActiveIcon.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblActiveIcon.ForeColor = Color.White;
            lblActiveIcon.Location = new Point(10, 12);
            lblActiveIcon.Name = "lblActiveIcon";
            lblActiveIcon.Size = new Size(34, 25);
            lblActiveIcon.TabIndex = 0;
            lblActiveIcon.Text = "✅";
            // 
            // lblActiveValue
            // 
            lblActiveValue.AutoSize = true;
            lblActiveValue.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblActiveValue.ForeColor = Color.White;
            lblActiveValue.Location = new Point(50, 3);
            lblActiveValue.Name = "lblActiveValue";
            lblActiveValue.Size = new Size(33, 37);
            lblActiveValue.TabIndex = 1;
            lblActiveValue.Text = "0";
            // 
            // lblActiveLabel
            // 
            lblActiveLabel.Font = new Font("Segoe UI", 9F);
            lblActiveLabel.ForeColor = Color.White;
            lblActiveLabel.Location = new Point(15, 40);
            lblActiveLabel.Name = "lblActiveLabel";
            lblActiveLabel.Size = new Size(180, 20);
            lblActiveLabel.TabIndex = 2;
            lblActiveLabel.Text = "Hoạt động";
            // 
            // statExpired
            // 
            statExpired.BackColor = Color.FromArgb(243, 156, 18);
            statExpired.Controls.Add(lblExpiredIcon);
            statExpired.Controls.Add(lblExpiredValue);
            statExpired.Controls.Add(lblExpiredLabel);
            statExpired.Dock = DockStyle.Left;
            statExpired.Location = new Point(235, 15);
            statExpired.Name = "statExpired";
            statExpired.Size = new Size(220, 60);
            statExpired.TabIndex = 2;
            // 
            // lblExpiredIcon
            // 
            lblExpiredIcon.AutoSize = true;
            lblExpiredIcon.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblExpiredIcon.ForeColor = Color.White;
            lblExpiredIcon.Location = new Point(10, 12);
            lblExpiredIcon.Name = "lblExpiredIcon";
            lblExpiredIcon.Size = new Size(34, 25);
            lblExpiredIcon.TabIndex = 0;
            lblExpiredIcon.Text = "⏰";
            // 
            // lblExpiredValue
            // 
            lblExpiredValue.AutoSize = true;
            lblExpiredValue.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblExpiredValue.ForeColor = Color.White;
            lblExpiredValue.Location = new Point(50, 0);
            lblExpiredValue.Name = "lblExpiredValue";
            lblExpiredValue.Size = new Size(33, 37);
            lblExpiredValue.TabIndex = 1;
            lblExpiredValue.Text = "0";
            // 
            // lblExpiredLabel
            // 
            lblExpiredLabel.Font = new Font("Segoe UI", 9F);
            lblExpiredLabel.ForeColor = Color.White;
            lblExpiredLabel.Location = new Point(15, 40);
            lblExpiredLabel.Name = "lblExpiredLabel";
            lblExpiredLabel.Size = new Size(180, 20);
            lblExpiredLabel.TabIndex = 2;
            lblExpiredLabel.Text = "Hết hạn";
            // 
            // statUsedUp
            // 
            statUsedUp.BackColor = Color.FromArgb(149, 165, 166);
            statUsedUp.Controls.Add(lblUsedUpIcon);
            statUsedUp.Controls.Add(lblUsedUpValue);
            statUsedUp.Controls.Add(lblUsedUpLabel);
            statUsedUp.Dock = DockStyle.Left;
            statUsedUp.Location = new Point(15, 15);
            statUsedUp.Name = "statUsedUp";
            statUsedUp.Size = new Size(220, 60);
            statUsedUp.TabIndex = 3;
            // 
            // lblUsedUpIcon
            // 
            lblUsedUpIcon.AutoSize = true;
            lblUsedUpIcon.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblUsedUpIcon.ForeColor = Color.White;
            lblUsedUpIcon.Location = new Point(10, 12);
            lblUsedUpIcon.Name = "lblUsedUpIcon";
            lblUsedUpIcon.Size = new Size(34, 25);
            lblUsedUpIcon.TabIndex = 0;
            lblUsedUpIcon.Text = "✔️";
            // 
            // lblUsedUpValue
            // 
            lblUsedUpValue.AutoSize = true;
            lblUsedUpValue.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblUsedUpValue.ForeColor = Color.White;
            lblUsedUpValue.Location = new Point(50, 0);
            lblUsedUpValue.Name = "lblUsedUpValue";
            lblUsedUpValue.Size = new Size(33, 37);
            lblUsedUpValue.TabIndex = 1;
            lblUsedUpValue.Text = "0";
            // 
            // lblUsedUpLabel
            // 
            lblUsedUpLabel.Font = new Font("Segoe UI", 9F);
            lblUsedUpLabel.ForeColor = Color.White;
            lblUsedUpLabel.Location = new Point(15, 40);
            lblUsedUpLabel.Name = "lblUsedUpLabel";
            lblUsedUpLabel.Size = new Size(180, 20);
            lblUsedUpLabel.TabIndex = 2;
            lblUsedUpLabel.Text = "Đã dùng hết";
            // 
            // frmVouchers
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1600, 900);
            Controls.Add(pnlMain);
            Controls.Add(pnlStats);
            Controls.Add(pnlSearchFilter);
            Controls.Add(pnlHeader);
            Name = "frmVouchers";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý Voucher - MilkTeaPOS";
            WindowState = FormWindowState.Maximized;
            Load += frmVouchers_Load;
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlMain.ResumeLayout(false);
            pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvVouchers).EndInit();
            pnlRight.ResumeLayout(false);
            pnlFormContainer.ResumeLayout(false);
            pnlFormFields.ResumeLayout(false);
            pnlFormFields.PerformLayout();
            pnlFormButtons.ResumeLayout(false);
            pnlSearchFilter.ResumeLayout(false);
            pnlSearchFilter.PerformLayout();
            pnlStats.ResumeLayout(false);
            statTotal.ResumeLayout(false);
            statTotal.PerformLayout();
            statActive.ResumeLayout(false);
            statActive.PerformLayout();
            statExpired.ResumeLayout(false);
            statExpired.PerformLayout();
            statUsedUp.ResumeLayout(false);
            statUsedUp.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private Label lblTitle;
        private Panel pnlSearchFilter;
        private Button btnFilter;
        private TextBox txtSearch;
        private ComboBox cbFilterStatus;
        private Panel pnlStats;
        private Panel statTotal;
        private Label lblTotalIcon;
        private Label lblTotalValue;
        private Label lblTotalLabel;
        private Panel statActive;
        private Label lblActiveIcon;
        private Label lblActiveValue;
        private Label lblActiveLabel;
        private Panel statExpired;
        private Label lblExpiredIcon;
        private Label lblExpiredValue;
        private Label lblExpiredLabel;
        private Panel statUsedUp;
        private Label lblUsedUpIcon;
        private Label lblUsedUpValue;
        private Label lblUsedUpLabel;
        private Panel pnlMain;
        private Panel pnlGrid;
        private DataGridView dgvVouchers;
        private Panel pnlRight;
        private Panel pnlFormContainer;
        private Label lblFormTitle;
        private Panel pnlFormFields;
        private Label lblStatus;
        private ComboBox cbStatus;
        private DateTimePicker dtpValidUntil;
        private Label lblValidUntil;
        private DateTimePicker dtpValidFrom;
        private Label lblValidFrom;
        private Label lblMaxDiscount;
        private TextBox txtMaxDiscount;
        private Label lblUsageLimit;
        private TextBox txtUsageLimit;
        private Label lblMinOrder;
        private TextBox txtMinOrder;
        private Label lblDiscountValue;
        private TextBox txtDiscountValue;
        private Label lblVoucherType;
        private ComboBox cbVoucherType;
        private Label lblDescription;
        private TextBox txtDescription;
        private Label lblName;
        private TextBox txtName;
        private Label lblCode;
        private TextBox txtCode;
        private Panel pnlFormButtons;
        private Button btnRefresh;
        private Button btnEdit;
        private Button btnAdd;
        private Button btnDelete;
    }
}
