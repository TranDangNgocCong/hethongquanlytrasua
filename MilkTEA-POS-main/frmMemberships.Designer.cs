namespace MilkTeaPOS
{
    partial class frmMemberships
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
            pnlHeader = new System.Windows.Forms.Panel();
            lblTitle = new System.Windows.Forms.Label();
            pnlToolbar = new System.Windows.Forms.Panel();
            btnAdd = new System.Windows.Forms.Button();
            btnEdit = new System.Windows.Forms.Button();
            btnDelete = new System.Windows.Forms.Button();
            btnRefresh = new System.Windows.Forms.Button();
            pnlSearch = new System.Windows.Forms.Panel();
            lblSearch = new System.Windows.Forms.Label();
            txtSearch = new System.Windows.Forms.TextBox();
            lblTierFilter = new System.Windows.Forms.Label();
            cbTierFilter = new System.Windows.Forms.ComboBox();
            pnlMain = new System.Windows.Forms.Panel();
            dgvMemberships = new System.Windows.Forms.DataGridView();
            pnlForm = new System.Windows.Forms.Panel();
            lblPhone = new System.Windows.Forms.Label();
            txtPhone = new System.Windows.Forms.TextBox();
            lblCustomer = new System.Windows.Forms.Label();
            cbCustomer = new System.Windows.Forms.ComboBox();
            lblTier = new System.Windows.Forms.Label();
            cbTier = new System.Windows.Forms.ComboBox();
            lblPoints = new System.Windows.Forms.Label();
            txtPoints = new System.Windows.Forms.TextBox();
            lblTotalSpent = new System.Windows.Forms.Label();
            txtTotalSpent = new System.Windows.Forms.TextBox();
            lblTotalOrders = new System.Windows.Forms.Label();
            txtTotalOrders = new System.Windows.Forms.TextBox();
            lblJoinedAt = new System.Windows.Forms.Label();
            dtpJoinedAt = new System.Windows.Forms.DateTimePicker();
            lblExpiresAt = new System.Windows.Forms.Label();
            dtpExpiresAt = new System.Windows.Forms.DateTimePicker();
            lblLastOrder = new System.Windows.Forms.Label();
            dtpLastOrder = new System.Windows.Forms.DateTimePicker();

            pnlHeader.SuspendLayout();
            pnlToolbar.SuspendLayout();
            pnlSearch.SuspendLayout();
            pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMemberships).BeginInit();
            pnlForm.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = System.Drawing.Color.White;
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            pnlHeader.Location = new System.Drawing.Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new System.Drawing.Size(1386, 80);
            pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            lblTitle.ForeColor = System.Drawing.Color.FromArgb(45, 55, 72);
            lblTitle.Location = new System.Drawing.Point(20, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new System.Drawing.Size(450, 40);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "💳 Quản lý thẻ hội viên";
            lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlToolbar
            // 
            pnlToolbar.BackColor = System.Drawing.Color.White;
            pnlToolbar.Controls.Add(btnAdd);
            pnlToolbar.Controls.Add(btnEdit);
            pnlToolbar.Controls.Add(btnDelete);
            pnlToolbar.Controls.Add(btnRefresh);
            pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            pnlToolbar.Location = new System.Drawing.Point(0, 80);
            pnlToolbar.Name = "pnlToolbar";
            pnlToolbar.Size = new System.Drawing.Size(1386, 60);
            pnlToolbar.TabIndex = 1;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = System.Drawing.Color.FromArgb(72, 187, 120);
            btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnAdd.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            btnAdd.ForeColor = System.Drawing.Color.White;
            btnAdd.Location = new System.Drawing.Point(20, 10);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new System.Drawing.Size(110, 40);
            btnAdd.TabIndex = 0;
            btnAdd.Text = "➕ Thêm";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += new System.EventHandler(btnAdd_Click);
            // 
            // btnEdit
            // 
            btnEdit.BackColor = System.Drawing.Color.FromArgb(255, 193, 7);
            btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            btnEdit.FlatAppearance.BorderSize = 0;
            btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnEdit.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            btnEdit.ForeColor = System.Drawing.Color.White;
            btnEdit.Location = new System.Drawing.Point(140, 10);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new System.Drawing.Size(110, 40);
            btnEdit.TabIndex = 1;
            btnEdit.Text = "✏️ Sửa";
            btnEdit.UseVisualStyleBackColor = false;
            btnEdit.Click += new System.EventHandler(btnEdit_Click);
            // 
            // btnDelete
            // 
            btnDelete.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnDelete.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            btnDelete.ForeColor = System.Drawing.Color.White;
            btnDelete.Location = new System.Drawing.Point(260, 10);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new System.Drawing.Size(110, 40);
            btnDelete.TabIndex = 2;
            btnDelete.Text = "🗑️ Xóa";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += new System.EventHandler(btnDelete_Click);
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = System.Drawing.Color.FromArgb(23, 162, 184);
            btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnRefresh.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            btnRefresh.ForeColor = System.Drawing.Color.White;
            btnRefresh.Location = new System.Drawing.Point(380, 10);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new System.Drawing.Size(110, 40);
            btnRefresh.TabIndex = 3;
            btnRefresh.Text = "🔄 Làm mới";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += new System.EventHandler(btnRefresh_Click);
            // 
            // pnlSearch
            // 
            pnlSearch.BackColor = System.Drawing.Color.White;
            pnlSearch.Controls.Add(lblTierFilter);
            pnlSearch.Controls.Add(cbTierFilter);
            pnlSearch.Controls.Add(lblSearch);
            pnlSearch.Controls.Add(txtSearch);
            pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            pnlSearch.Location = new System.Drawing.Point(0, 140);
            pnlSearch.Name = "pnlSearch";
            pnlSearch.Size = new System.Drawing.Size(1386, 60);
            pnlSearch.TabIndex = 2;
            // 
            // lblSearch
            // 
            lblSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            lblSearch.Font = new System.Drawing.Font("Segoe UI", 12F);
            lblSearch.ForeColor = System.Drawing.Color.FromArgb(45, 55, 72);
            lblSearch.Location = new System.Drawing.Point(54, 20);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new System.Drawing.Size(70, 21);
            lblSearch.TabIndex = 0;
            lblSearch.Text = "🔍 Tìm:";
            lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            lblSearch.Click += new System.EventHandler(btnSearch_Click);
            // 
            // txtSearch
            // 
            txtSearch.BackColor = System.Drawing.Color.FromArgb(247, 249, 252);
            txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtSearch.Font = new System.Drawing.Font("Segoe UI", 12F);
            txtSearch.Location = new System.Drawing.Point(130, 15);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Tìm theo tên, SĐT, hạng...";
            txtSearch.Size = new System.Drawing.Size(300, 29);
            txtSearch.TabIndex = 1;
            txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtSearch_KeyPress);
            // 
            // lblTierFilter
            // 
            lblTierFilter.Cursor = System.Windows.Forms.Cursors.Hand;
            lblTierFilter.Font = new System.Drawing.Font("Segoe UI", 12F);
            lblTierFilter.ForeColor = System.Drawing.Color.FromArgb(45, 55, 72);
            lblTierFilter.Location = new System.Drawing.Point(454, 20);
            lblTierFilter.Name = "lblTierFilter";
            lblTierFilter.Size = new System.Drawing.Size(80, 21);
            lblTierFilter.TabIndex = 2;
            lblTierFilter.Text = "🏷️ Hạng:";
            lblTierFilter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbTierFilter
            // 
            cbTierFilter.BackColor = System.Drawing.Color.FromArgb(247, 249, 252);
            cbTierFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cbTierFilter.Font = new System.Drawing.Font("Segoe UI", 12F);
            cbTierFilter.FormattingEnabled = true;
            cbTierFilter.Location = new System.Drawing.Point(540, 16);
            cbTierFilter.Name = "cbTierFilter";
            cbTierFilter.Size = new System.Drawing.Size(160, 29);
            cbTierFilter.TabIndex = 3;
            cbTierFilter.SelectedIndexChanged += new System.EventHandler(cbTierFilter_SelectedIndexChanged);
            // 
            // pnlMain
            // 
            pnlMain.BackColor = System.Drawing.Color.FromArgb(247, 249, 252);
            pnlMain.Controls.Add(dgvMemberships);
            pnlMain.Controls.Add(pnlForm);
            pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlMain.Location = new System.Drawing.Point(0, 200);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new System.Windows.Forms.Padding(20);
            pnlMain.Size = new System.Drawing.Size(1386, 698);
            pnlMain.TabIndex = 3;
            // 
            // dgvMemberships
            // 
            dgvMemberships.AllowUserToAddRows = false;
            dgvMemberships.AllowUserToDeleteRows = false;
            dgvMemberships.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvMemberships.BackgroundColor = System.Drawing.Color.White;
            dgvMemberships.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(45, 55, 72);
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(45, 55, 72);
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dgvMemberships.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvMemberships.ColumnHeadersHeight = 45;
            dgvMemberships.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dgvMemberships.GridColor = System.Drawing.Color.FromArgb(226, 232, 240);
            dgvMemberships.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 11F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(45, 55, 72);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(255, 107, 107);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dgvMemberships.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(247, 249, 252);
            dgvMemberships.Dock = System.Windows.Forms.DockStyle.Fill;
            dgvMemberships.EnableHeadersVisualStyles = false;
            dgvMemberships.Location = new System.Drawing.Point(20, 20);
            dgvMemberships.MultiSelect = false;
            dgvMemberships.Name = "dgvMemberships";
            dgvMemberships.RowHeadersVisible = false;
            dgvMemberships.RowTemplate.Height = 50;
            dgvMemberships.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvMemberships.Size = new System.Drawing.Size(1346, 300);
            dgvMemberships.TabIndex = 0;
            dgvMemberships.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvMemberships_CellClick);
            dgvMemberships.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(dgvMemberships_CellFormatting);
            // 
            // pnlForm
            // 
            pnlForm.BackColor = System.Drawing.Color.White;
            pnlForm.Controls.Add(lblPhone);
            pnlForm.Controls.Add(txtPhone);
            pnlForm.Controls.Add(lblCustomer);
            pnlForm.Controls.Add(cbCustomer);
            pnlForm.Controls.Add(lblTier);
            pnlForm.Controls.Add(cbTier);
            pnlForm.Controls.Add(lblPoints);
            pnlForm.Controls.Add(txtPoints);
            pnlForm.Controls.Add(lblTotalSpent);
            pnlForm.Controls.Add(txtTotalSpent);
            pnlForm.Controls.Add(lblTotalOrders);
            pnlForm.Controls.Add(txtTotalOrders);
            pnlForm.Controls.Add(lblJoinedAt);
            pnlForm.Controls.Add(dtpJoinedAt);
            pnlForm.Controls.Add(lblExpiresAt);
            pnlForm.Controls.Add(dtpExpiresAt);
            pnlForm.Controls.Add(lblLastOrder);
            pnlForm.Controls.Add(dtpLastOrder);
            pnlForm.Dock = System.Windows.Forms.DockStyle.Bottom;
            pnlForm.Location = new System.Drawing.Point(20, 398);
            pnlForm.Name = "pnlForm";
            pnlForm.Padding = new System.Windows.Forms.Padding(30);
            pnlForm.Size = new System.Drawing.Size(1346, 280);
            pnlForm.TabIndex = 1;
            // 
            // lblPhone
            // 
            lblPhone.Font = new System.Drawing.Font("Segoe UI", 11F);
            lblPhone.ForeColor = System.Drawing.Color.FromArgb(45, 55, 72);
            lblPhone.Location = new System.Drawing.Point(30, 30);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new System.Drawing.Size(120, 30);
            lblPhone.TabIndex = 0;
            lblPhone.Text = "Số điện thoại:";
            lblPhone.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPhone
            // 
            txtPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtPhone.Font = new System.Drawing.Font("Segoe UI", 11F);
            txtPhone.Location = new System.Drawing.Point(160, 30);
            txtPhone.Name = "txtPhone";
            txtPhone.PlaceholderText = "Nhập SĐT để tra cứu";
            txtPhone.Size = new System.Drawing.Size(300, 27);
            txtPhone.TabIndex = 1;
            txtPhone.TextChanged += new System.EventHandler(txtPhone_TextChanged);
            // 
            // lblCustomer
            // 
            lblCustomer.Font = new System.Drawing.Font("Segoe UI", 11F);
            lblCustomer.ForeColor = System.Drawing.Color.FromArgb(45, 55, 72);
            lblCustomer.Location = new System.Drawing.Point(503, 30);
            lblCustomer.Name = "lblCustomer";
            lblCustomer.Size = new System.Drawing.Size(100, 30);
            lblCustomer.TabIndex = 2;
            lblCustomer.Text = "Khách hàng:";
            lblCustomer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbCustomer
            // 
            cbCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cbCustomer.Font = new System.Drawing.Font("Segoe UI", 11F);
            cbCustomer.FormattingEnabled = true;
            cbCustomer.Location = new System.Drawing.Point(610, 30);
            cbCustomer.Name = "cbCustomer";
            cbCustomer.Size = new System.Drawing.Size(300, 28);
            cbCustomer.TabIndex = 3;
            cbCustomer.SelectedIndexChanged += new System.EventHandler(cbCustomer_SelectedIndexChanged);
            // 
            // lblTier
            // 
            lblTier.Font = new System.Drawing.Font("Segoe UI", 11F);
            lblTier.ForeColor = System.Drawing.Color.FromArgb(45, 55, 72);
            lblTier.Location = new System.Drawing.Point(30, 70);
            lblTier.Name = "lblTier";
            lblTier.Size = new System.Drawing.Size(120, 30);
            lblTier.TabIndex = 4;
            lblTier.Text = "Hạng thẻ:";
            lblTier.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbTier
            // 
            cbTier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cbTier.Font = new System.Drawing.Font("Segoe UI", 11F);
            cbTier.FormattingEnabled = true;
            cbTier.Location = new System.Drawing.Point(160, 70);
            cbTier.Name = "cbTier";
            cbTier.Size = new System.Drawing.Size(300, 28);
            cbTier.TabIndex = 5;
            // 
            // lblPoints
            // 
            lblPoints.Font = new System.Drawing.Font("Segoe UI", 11F);
            lblPoints.ForeColor = System.Drawing.Color.FromArgb(45, 55, 72);
            lblPoints.Location = new System.Drawing.Point(503, 70);
            lblPoints.Name = "lblPoints";
            lblPoints.Size = new System.Drawing.Size(100, 30);
            lblPoints.TabIndex = 6;
            lblPoints.Text = "Điểm tích lũy:";
            lblPoints.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPoints
            // 
            txtPoints.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtPoints.Font = new System.Drawing.Font("Segoe UI", 11F);
            txtPoints.Location = new System.Drawing.Point(610, 70);
            txtPoints.Name = "txtPoints";
            txtPoints.Size = new System.Drawing.Size(200, 27);
            txtPoints.TabIndex = 7;
            // 
            // lblTotalSpent
            // 
            lblTotalSpent.Font = new System.Drawing.Font("Segoe UI", 11F);
            lblTotalSpent.ForeColor = System.Drawing.Color.FromArgb(45, 55, 72);
            lblTotalSpent.Location = new System.Drawing.Point(30, 110);
            lblTotalSpent.Name = "lblTotalSpent";
            lblTotalSpent.Size = new System.Drawing.Size(120, 30);
            lblTotalSpent.TabIndex = 8;
            lblTotalSpent.Text = "Tổng chi tiêu:";
            lblTotalSpent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTotalSpent
            // 
            txtTotalSpent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtTotalSpent.Font = new System.Drawing.Font("Segoe UI", 11F);
            txtTotalSpent.Location = new System.Drawing.Point(160, 110);
            txtTotalSpent.Name = "txtTotalSpent";
            txtTotalSpent.Size = new System.Drawing.Size(300, 27);
            txtTotalSpent.TabIndex = 9;
            // 
            // lblTotalOrders
            // 
            lblTotalOrders.Font = new System.Drawing.Font("Segoe UI", 11F);
            lblTotalOrders.ForeColor = System.Drawing.Color.FromArgb(45, 55, 72);
            lblTotalOrders.Location = new System.Drawing.Point(503, 110);
            lblTotalOrders.Name = "lblTotalOrders";
            lblTotalOrders.Size = new System.Drawing.Size(100, 30);
            lblTotalOrders.TabIndex = 10;
            lblTotalOrders.Text = "Tổng đơn hàng:";
            lblTotalOrders.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTotalOrders
            // 
            txtTotalOrders.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtTotalOrders.Font = new System.Drawing.Font("Segoe UI", 11F);
            txtTotalOrders.Location = new System.Drawing.Point(610, 110);
            txtTotalOrders.Name = "txtTotalOrders";
            txtTotalOrders.Size = new System.Drawing.Size(200, 27);
            txtTotalOrders.TabIndex = 11;
            // 
            // lblJoinedAt
            // 
            lblJoinedAt.Font = new System.Drawing.Font("Segoe UI", 11F);
            lblJoinedAt.ForeColor = System.Drawing.Color.FromArgb(45, 55, 72);
            lblJoinedAt.Location = new System.Drawing.Point(30, 150);
            lblJoinedAt.Name = "lblJoinedAt";
            lblJoinedAt.Size = new System.Drawing.Size(120, 30);
            lblJoinedAt.TabIndex = 12;
            lblJoinedAt.Text = "Ngày tham gia:";
            lblJoinedAt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpJoinedAt
            // 
            dtpJoinedAt.CalendarFont = new System.Drawing.Font("Segoe UI", 11F);
            dtpJoinedAt.CustomFormat = "dd/MM/yyyy";
            dtpJoinedAt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            dtpJoinedAt.Location = new System.Drawing.Point(160, 150);
            dtpJoinedAt.Name = "dtpJoinedAt";
            dtpJoinedAt.ShowCheckBox = true;
            dtpJoinedAt.Size = new System.Drawing.Size(300, 23);
            dtpJoinedAt.TabIndex = 13;
            // 
            // lblExpiresAt
            // 
            lblExpiresAt.Font = new System.Drawing.Font("Segoe UI", 11F);
            lblExpiresAt.ForeColor = System.Drawing.Color.FromArgb(45, 55, 72);
            lblExpiresAt.Location = new System.Drawing.Point(503, 150);
            lblExpiresAt.Name = "lblExpiresAt";
            lblExpiresAt.Size = new System.Drawing.Size(100, 30);
            lblExpiresAt.TabIndex = 14;
            lblExpiresAt.Text = "Ngày hết hạn:";
            lblExpiresAt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpExpiresAt
            // 
            dtpExpiresAt.CalendarFont = new System.Drawing.Font("Segoe UI", 11F);
            dtpExpiresAt.CustomFormat = "dd/MM/yyyy";
            dtpExpiresAt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            dtpExpiresAt.Location = new System.Drawing.Point(610, 150);
            dtpExpiresAt.Name = "dtpExpiresAt";
            dtpExpiresAt.ShowCheckBox = true;
            dtpExpiresAt.Size = new System.Drawing.Size(300, 23);
            dtpExpiresAt.TabIndex = 15;
            // 
            // lblLastOrder
            // 
            lblLastOrder.Font = new System.Drawing.Font("Segoe UI", 11F);
            lblLastOrder.ForeColor = System.Drawing.Color.FromArgb(45, 55, 72);
            lblLastOrder.Location = new System.Drawing.Point(30, 190);
            lblLastOrder.Name = "lblLastOrder";
            lblLastOrder.Size = new System.Drawing.Size(120, 30);
            lblLastOrder.TabIndex = 16;
            lblLastOrder.Text = "Đơn cuối:";
            lblLastOrder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpLastOrder
            // 
            dtpLastOrder.CalendarFont = new System.Drawing.Font("Segoe UI", 11F);
            dtpLastOrder.CustomFormat = "dd/MM/yyyy";
            dtpLastOrder.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            dtpLastOrder.Location = new System.Drawing.Point(160, 190);
            dtpLastOrder.Name = "dtpLastOrder";
            dtpLastOrder.ShowCheckBox = true;
            dtpLastOrder.Size = new System.Drawing.Size(300, 23);
            dtpLastOrder.TabIndex = 17;
            // 
            // frmMemberships
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(247, 249, 252);
            ClientSize = new System.Drawing.Size(1386, 898);
            Controls.Add(pnlMain);
            Controls.Add(pnlSearch);
            Controls.Add(pnlToolbar);
            Controls.Add(pnlHeader);
            Name = "frmMemberships";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "💳 Quản lý thẻ hội viên - MilkTea POS";
            WindowState = System.Windows.Forms.FormWindowState.Maximized;
            pnlHeader.ResumeLayout(false);
            pnlToolbar.ResumeLayout(false);
            pnlSearch.ResumeLayout(false);
            pnlSearch.PerformLayout();
            pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvMemberships).EndInit();
            pnlForm.ResumeLayout(false);
            pnlForm.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblTierFilter;
        private System.Windows.Forms.ComboBox cbTierFilter;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.DataGridView dgvMemberships;
        private System.Windows.Forms.Panel pnlForm;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.ComboBox cbCustomer;
        private System.Windows.Forms.Label lblTier;
        private System.Windows.Forms.ComboBox cbTier;
        private System.Windows.Forms.Label lblPoints;
        private System.Windows.Forms.TextBox txtPoints;
        private System.Windows.Forms.Label lblTotalSpent;
        private System.Windows.Forms.TextBox txtTotalSpent;
        private System.Windows.Forms.Label lblTotalOrders;
        private System.Windows.Forms.TextBox txtTotalOrders;
        private System.Windows.Forms.Label lblJoinedAt;
        private System.Windows.Forms.DateTimePicker dtpJoinedAt;
        private System.Windows.Forms.Label lblExpiresAt;
        private System.Windows.Forms.DateTimePicker dtpExpiresAt;
        private System.Windows.Forms.Label lblLastOrder;
        private System.Windows.Forms.DateTimePicker dtpLastOrder;
    }
}
