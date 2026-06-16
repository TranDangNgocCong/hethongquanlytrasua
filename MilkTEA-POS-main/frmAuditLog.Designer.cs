namespace MilkTeaPOS
{
    partial class frmAuditLog
    {
        private System.ComponentModel.IContainer components = null;

        // Panels
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.Panel pnlFilters;
        private System.Windows.Forms.Panel pnlMain;

        // Header
        private System.Windows.Forms.Label lblTitle;

        // Toolbar
        private System.Windows.Forms.Button btnToday;
        private System.Windows.Forms.Button btnWeek;
        private System.Windows.Forms.Button btnMonth;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnClearLogs;

        // Filters
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label lblAction;
        private System.Windows.Forms.ComboBox cbAction;
        private System.Windows.Forms.Label lblTable;
        private System.Windows.Forms.ComboBox cbTable;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.ComboBox cbUser;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;

        // DataGridView
        private System.Windows.Forms.DataGridView dgvAuditLogs;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAction;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRecordId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetail;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIp;

        // Stats
        private System.Windows.Forms.Label lblTotalLogs;
        private System.Windows.Forms.Label lblLoading;

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
            pnlHeader = new Panel();
            lblTitle = new Label();
            pnlToolbar = new Panel();
            btnToday = new Button();
            btnWeek = new Button();
            btnMonth = new Button();
            btnFilter = new Button();
            btnRefresh = new Button();
            btnExport = new Button();
            btnClearLogs = new Button();
            pnlFilters = new Panel();
            lblStartDate = new Label();
            dtpStartDate = new DateTimePicker();
            lblEndDate = new Label();
            dtpEndDate = new DateTimePicker();
            lblAction = new Label();
            cbAction = new ComboBox();
            lblTable = new Label();
            cbTable = new ComboBox();
            lblUser = new Label();
            cbUser = new ComboBox();
            lblSearch = new Label();
            txtSearch = new TextBox();
            pnlMain = new Panel();
            lblTotalLogs = new Label();
            dgvAuditLogs = new DataGridView();
            lblLoading = new Label();
            pnlHeader.SuspendLayout();
            pnlToolbar.SuspendLayout();
            pnlFilters.SuspendLayout();
            pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAuditLogs).BeginInit();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.White;
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1920, 80);
            pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(45, 55, 72);
            lblTitle.Location = new Point(20, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(500, 40);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "🔒 Audit Log - Lịch sử hành động";
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlToolbar
            // 
            pnlToolbar.BackColor = Color.White;
            pnlToolbar.Controls.Add(btnToday);
            pnlToolbar.Controls.Add(btnWeek);
            pnlToolbar.Controls.Add(btnMonth);
            pnlToolbar.Controls.Add(btnFilter);
            pnlToolbar.Controls.Add(btnRefresh);
            pnlToolbar.Controls.Add(btnExport);
            pnlToolbar.Controls.Add(btnClearLogs);
            pnlToolbar.Dock = DockStyle.Top;
            pnlToolbar.Location = new Point(0, 80);
            pnlToolbar.Name = "pnlToolbar";
            pnlToolbar.Size = new Size(1920, 60);
            pnlToolbar.TabIndex = 1;
            // 
            // btnToday
            // 
            btnToday.BackColor = Color.FromArgb(72, 187, 120);
            btnToday.Cursor = Cursors.Hand;
            btnToday.FlatAppearance.BorderSize = 0;
            btnToday.FlatStyle = FlatStyle.Flat;
            btnToday.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnToday.ForeColor = Color.White;
            btnToday.Location = new Point(20, 10);
            btnToday.Name = "btnToday";
            btnToday.Size = new Size(136, 40);
            btnToday.TabIndex = 0;
            btnToday.Text = "📅 Hôm nay";
            btnToday.UseVisualStyleBackColor = false;
            btnToday.Click += btnToday_Click;
            // 
            // btnWeek
            // 
            btnWeek.BackColor = Color.FromArgb(44, 62, 80);
            btnWeek.Cursor = Cursors.Hand;
            btnWeek.FlatAppearance.BorderSize = 0;
            btnWeek.FlatStyle = FlatStyle.Flat;
            btnWeek.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnWeek.ForeColor = Color.White;
            btnWeek.Location = new Point(162, 10);
            btnWeek.Name = "btnWeek";
            btnWeek.Size = new Size(110, 40);
            btnWeek.TabIndex = 1;
            btnWeek.Text = "📆 7 ngày";
            btnWeek.UseVisualStyleBackColor = false;
            btnWeek.Click += btnWeek_Click;
            // 
            // btnMonth
            // 
            btnMonth.BackColor = Color.FromArgb(52, 152, 219);
            btnMonth.Cursor = Cursors.Hand;
            btnMonth.FlatAppearance.BorderSize = 0;
            btnMonth.FlatStyle = FlatStyle.Flat;
            btnMonth.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnMonth.ForeColor = Color.White;
            btnMonth.Location = new Point(278, 10);
            btnMonth.Name = "btnMonth";
            btnMonth.Size = new Size(110, 40);
            btnMonth.TabIndex = 2;
            btnMonth.Text = "🗓️ 30 ngày";
            btnMonth.UseVisualStyleBackColor = false;
            btnMonth.Click += btnMonth_Click;
            // 
            // btnFilter
            // 
            btnFilter.BackColor = Color.FromArgb(155, 89, 182);
            btnFilter.Cursor = Cursors.Hand;
            btnFilter.FlatAppearance.BorderSize = 0;
            btnFilter.FlatStyle = FlatStyle.Flat;
            btnFilter.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnFilter.ForeColor = Color.White;
            btnFilter.Location = new Point(750, 10);
            btnFilter.Name = "btnFilter";
            btnFilter.Size = new Size(110, 40);
            btnFilter.TabIndex = 3;
            btnFilter.Text = "🔍 Lọc";
            btnFilter.UseVisualStyleBackColor = false;
            btnFilter.Click += btnFilter_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.FromArgb(149, 165, 166);
            btnRefresh.Cursor = Cursors.Hand;
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(870, 10);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(110, 40);
            btnRefresh.TabIndex = 4;
            btnRefresh.Text = "🔄 Làm mới";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // btnExport
            // 
            btnExport.BackColor = Color.FromArgb(230, 126, 34);
            btnExport.Cursor = Cursors.Hand;
            btnExport.FlatAppearance.BorderSize = 0;
            btnExport.FlatStyle = FlatStyle.Flat;
            btnExport.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnExport.ForeColor = Color.White;
            btnExport.Location = new Point(990, 10);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(120, 40);
            btnExport.TabIndex = 5;
            btnExport.Text = "📥 Xuất CSV";
            btnExport.UseVisualStyleBackColor = false;
            btnExport.Click += btnExport_Click;
            // 
            // btnClearLogs
            // 
            btnClearLogs.BackColor = Color.FromArgb(220, 53, 69);
            btnClearLogs.Cursor = Cursors.Hand;
            btnClearLogs.FlatAppearance.BorderSize = 0;
            btnClearLogs.FlatStyle = FlatStyle.Flat;
            btnClearLogs.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnClearLogs.ForeColor = Color.White;
            btnClearLogs.Location = new Point(1120, 10);
            btnClearLogs.Name = "btnClearLogs";
            btnClearLogs.Size = new Size(140, 40);
            btnClearLogs.TabIndex = 6;
            btnClearLogs.Text = "🗑️ Xóa log cũ";
            btnClearLogs.UseVisualStyleBackColor = false;
            btnClearLogs.Click += btnClearLogs_Click;
            // 
            // pnlFilters
            // 
            pnlFilters.BackColor = Color.White;
            pnlFilters.Controls.Add(lblStartDate);
            pnlFilters.Controls.Add(dtpStartDate);
            pnlFilters.Controls.Add(lblEndDate);
            pnlFilters.Controls.Add(dtpEndDate);
            pnlFilters.Controls.Add(lblAction);
            pnlFilters.Controls.Add(cbAction);
            pnlFilters.Controls.Add(lblTable);
            pnlFilters.Controls.Add(cbTable);
            pnlFilters.Controls.Add(lblUser);
            pnlFilters.Controls.Add(cbUser);
            pnlFilters.Controls.Add(lblSearch);
            pnlFilters.Controls.Add(txtSearch);
            pnlFilters.Dock = DockStyle.Top;
            pnlFilters.Location = new Point(0, 140);
            pnlFilters.Name = "pnlFilters";
            pnlFilters.Size = new Size(1920, 50);
            pnlFilters.TabIndex = 2;
            // 
            // lblStartDate
            // 
            lblStartDate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblStartDate.Location = new Point(20, 10);
            lblStartDate.Name = "lblStartDate";
            lblStartDate.Size = new Size(80, 30);
            lblStartDate.TabIndex = 0;
            lblStartDate.Text = "Từ ngày:";
            lblStartDate.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dtpStartDate
            // 
            dtpStartDate.CustomFormat = "dd/MM/yyyy";
            dtpStartDate.Font = new Font("Segoe UI", 9F);
            dtpStartDate.Format = DateTimePickerFormat.Custom;
            dtpStartDate.Location = new Point(100, 12);
            dtpStartDate.Name = "dtpStartDate";
            dtpStartDate.Size = new Size(120, 23);
            dtpStartDate.TabIndex = 1;
            dtpStartDate.KeyPress += dtpStartDate_KeyPress;
            // 
            // lblEndDate
            // 
            lblEndDate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblEndDate.Location = new Point(230, 10);
            lblEndDate.Name = "lblEndDate";
            lblEndDate.Size = new Size(80, 30);
            lblEndDate.TabIndex = 2;
            lblEndDate.Text = "Đến ngày:";
            lblEndDate.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dtpEndDate
            // 
            dtpEndDate.CustomFormat = "dd/MM/yyyy";
            dtpEndDate.Font = new Font("Segoe UI", 9F);
            dtpEndDate.Format = DateTimePickerFormat.Custom;
            dtpEndDate.Location = new Point(310, 12);
            dtpEndDate.Name = "dtpEndDate";
            dtpEndDate.Size = new Size(120, 23);
            dtpEndDate.TabIndex = 3;
            dtpEndDate.KeyPress += dtpEndDate_KeyPress;
            // 
            // lblAction
            // 
            lblAction.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblAction.Location = new Point(440, 10);
            lblAction.Name = "lblAction";
            lblAction.Size = new Size(70, 30);
            lblAction.TabIndex = 4;
            lblAction.Text = "Hành động:";
            lblAction.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cbAction
            // 
            cbAction.DropDownStyle = ComboBoxStyle.DropDownList;
            cbAction.Font = new Font("Segoe UI", 9F);
            cbAction.Location = new Point(510, 12);
            cbAction.Name = "cbAction";
            cbAction.Size = new Size(120, 23);
            cbAction.TabIndex = 5;
            // 
            // lblTable
            // 
            lblTable.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTable.Location = new Point(640, 10);
            lblTable.Name = "lblTable";
            lblTable.Size = new Size(50, 30);
            lblTable.TabIndex = 6;
            lblTable.Text = "Bảng:";
            lblTable.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cbTable
            // 
            cbTable.DropDownStyle = ComboBoxStyle.DropDownList;
            cbTable.Font = new Font("Segoe UI", 9F);
            cbTable.Location = new Point(690, 12);
            cbTable.Name = "cbTable";
            cbTable.Size = new Size(130, 23);
            cbTable.TabIndex = 7;
            // 
            // lblUser
            // 
            lblUser.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblUser.Location = new Point(830, 10);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(70, 30);
            lblUser.TabIndex = 8;
            lblUser.Text = "Người dùng:";
            lblUser.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cbUser
            // 
            cbUser.DropDownStyle = ComboBoxStyle.DropDownList;
            cbUser.Font = new Font("Segoe UI", 9F);
            cbUser.Location = new Point(900, 12);
            cbUser.Name = "cbUser";
            cbUser.Size = new Size(140, 23);
            cbUser.TabIndex = 9;
            // 
            // lblSearch
            // 
            lblSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSearch.Location = new Point(1050, 10);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(60, 30);
            lblSearch.TabIndex = 10;
            lblSearch.Text = "🔍 Tìm:";
            lblSearch.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtSearch
            // 
            txtSearch.Font = new Font("Segoe UI", 9F);
            txtSearch.Location = new Point(1110, 12);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Tìm theo user, hành động, bảng, IP...";
            txtSearch.Size = new Size(250, 23);
            txtSearch.TabIndex = 11;
            txtSearch.KeyPress += txtSearch_KeyPress;
            // 
            // pnlMain
            // 
            pnlMain.Controls.Add(lblTotalLogs);
            pnlMain.Controls.Add(dgvAuditLogs);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 190);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new Padding(10);
            pnlMain.Size = new Size(1920, 871);
            pnlMain.TabIndex = 3;
            // 
            // lblTotalLogs
            // 
            lblTotalLogs.Dock = DockStyle.Bottom;
            lblTotalLogs.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTotalLogs.ForeColor = Color.FromArgb(44, 62, 80);
            lblTotalLogs.Location = new Point(10, 831);
            lblTotalLogs.Name = "lblTotalLogs";
            lblTotalLogs.Padding = new Padding(10, 0, 0, 0);
            lblTotalLogs.Size = new Size(1900, 30);
            lblTotalLogs.TabIndex = 1;
            lblTotalLogs.Text = "📊 Tổng: 0 bản ghi";
            lblTotalLogs.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dgvAuditLogs
            // 
            dgvAuditLogs.AllowUserToAddRows = false;
            dgvAuditLogs.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(240, 242, 245);
            dgvAuditLogs.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvAuditLogs.BackgroundColor = Color.FromArgb(247, 249, 252);
            dgvAuditLogs.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(44, 62, 80);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvAuditLogs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvAuditLogs.ColumnHeadersHeight = 40;
            dgvAuditLogs.Cursor = Cursors.Hand;
            dgvAuditLogs.Dock = DockStyle.Fill;
            dgvAuditLogs.EnableHeadersVisualStyles = false;
            dgvAuditLogs.GridColor = Color.FromArgb(236, 240, 241);
            dgvAuditLogs.Location = new Point(10, 10);
            dgvAuditLogs.MultiSelect = false;
            dgvAuditLogs.Name = "dgvAuditLogs";
            dgvAuditLogs.ReadOnly = true;
            dgvAuditLogs.RowHeadersVisible = false;
            dgvAuditLogs.RowHeadersWidth = 50;
            dgvAuditLogs.RowTemplate.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgvAuditLogs.RowTemplate.DefaultCellStyle.Padding = new Padding(5);
            dgvAuditLogs.RowTemplate.Height = 35;
            dgvAuditLogs.RowTemplate.Resizable = DataGridViewTriState.True;
            dgvAuditLogs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAuditLogs.Size = new Size(1900, 851);
            dgvAuditLogs.TabIndex = 0;
            dgvAuditLogs.CellDoubleClick += dgvAuditLogs_CellDoubleClick;
            dgvAuditLogs.CellFormatting += dgvAuditLogs_CellFormatting;
            //
            // Columns - created inline to ensure they exist before LoadAuditLogs
            //
            colTime = new DataGridViewTextBoxColumn();
            colUser = new DataGridViewTextBoxColumn();
            colAction = new DataGridViewTextBoxColumn();
            colTable = new DataGridViewTextBoxColumn();
            colRecordId = new DataGridViewTextBoxColumn();
            colDetail = new DataGridViewTextBoxColumn();
            colIp = new DataGridViewTextBoxColumn();
            //
            colTime.Name = "Time";
            colTime.HeaderText = "⏰ Thời gian";
            colTime.Width = 180;
            colTime.DefaultCellStyle = new DataGridViewCellStyle { Font = new Font("Segoe UI", 10F), Padding = new Padding(8, 5, 8, 5), Alignment = DataGridViewContentAlignment.MiddleCenter };
            //
            colUser.Name = "User";
            colUser.HeaderText = "👤 Người dùng";
            colUser.Width = 160;
            colUser.DefaultCellStyle = new DataGridViewCellStyle { Font = new Font("Segoe UI", 10F), Padding = new Padding(8, 5, 8, 5) };
            //
            colAction.Name = "Action";
            colAction.HeaderText = "🎯 Hành động";
            colAction.Width = 140;
            colAction.DefaultCellStyle = new DataGridViewCellStyle { Font = new Font("Segoe UI", 10F), Padding = new Padding(8, 5, 8, 5), Alignment = DataGridViewContentAlignment.MiddleCenter };
            //
            colTable.Name = "Table";
            colTable.HeaderText = "📊 Bảng";
            colTable.Width = 150;
            colTable.DefaultCellStyle = new DataGridViewCellStyle { Font = new Font("Segoe UI", 10F), Padding = new Padding(8, 5, 8, 5) };
            //
            colRecordId.Name = "RecordId";
            colRecordId.HeaderText = "🔑 Record ID";
            colRecordId.Width = 320;
            colRecordId.DefaultCellStyle = new DataGridViewCellStyle { Font = new Font("Consolas", 10F), Padding = new Padding(8, 5, 8, 5) };
            //
            colDetail.Name = "ActionDetail";
            colDetail.HeaderText = "📝 Chi tiết";
            colDetail.Width = 250;
            colDetail.DefaultCellStyle = new DataGridViewCellStyle { Font = new Font("Segoe UI", 10F), Padding = new Padding(8, 5, 8, 5) };
            //
            colIp.Name = "IpAddress";
            colIp.HeaderText = "🌐 IP Address";
            colIp.Width = 160;
            colIp.DefaultCellStyle = new DataGridViewCellStyle { Font = new Font("Segoe UI", 10F), Padding = new Padding(8, 5, 8, 5), Alignment = DataGridViewContentAlignment.MiddleCenter };
            //
            dgvAuditLogs.Columns.AddRange(new DataGridViewColumn[] { colTime, colUser, colAction, colTable, colRecordId, colDetail, colIp });
            // 
            // lblLoading
            // 
            lblLoading.AutoSize = true;
            lblLoading.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblLoading.ForeColor = Color.FromArgb(52, 152, 219);
            lblLoading.Location = new Point(700, 400);
            lblLoading.Name = "lblLoading";
            lblLoading.Size = new Size(172, 21);
            lblLoading.TabIndex = 99;
            lblLoading.Text = "⏳ Đang tải dữ liệu...";
            lblLoading.Visible = false;
            // 
            // frmAuditLog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(247, 249, 252);
            ClientSize = new Size(1920, 1061);
            Controls.Add(pnlMain);
            Controls.Add(pnlFilters);
            Controls.Add(pnlToolbar);
            Controls.Add(pnlHeader);
            Controls.Add(lblLoading);
            Name = "frmAuditLog";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "🔒 Audit Log - Lịch sử hành động - MilkTeaPOS";
            WindowState = FormWindowState.Maximized;
            pnlHeader.ResumeLayout(false);
            pnlToolbar.ResumeLayout(false);
            pnlFilters.ResumeLayout(false);
            pnlFilters.PerformLayout();
            pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvAuditLogs).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
