namespace MilkTeaPOS
{
    partial class frmSalesReport
    {
        private System.ComponentModel.IContainer components = null;

        // Panels
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.Panel pnlFilters;
        private System.Windows.Forms.Panel pnlSummary;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel pnlRight;

        // Header
        private System.Windows.Forms.Label lblTitle;

        // Toolbar
        private System.Windows.Forms.Button btnToday;
        private System.Windows.Forms.Button btnWeek;
        private System.Windows.Forms.Button btnMonth;
        private System.Windows.Forms.Button btnAllTime;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnExport;

        // Filters
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;

        // Summary KPIs
        private System.Windows.Forms.Label lblTotalRevenueTitle;
        private System.Windows.Forms.Label lblTotalRevenue;
        private System.Windows.Forms.Label lblTotalOrdersTitle;
        private System.Windows.Forms.Label lblTotalOrders;
        private System.Windows.Forms.Label lblAvgOrderValueTitle;
        private System.Windows.Forms.Label lblAvgOrderValue;
        private System.Windows.Forms.Label lblTotalDiscountTitle;
        private System.Windows.Forms.Label lblTotalDiscount;
        private System.Windows.Forms.Label lblPendingOrdersTitle;
        private System.Windows.Forms.Label lblPendingOrders;
        private System.Windows.Forms.Label lblCancelledOrdersTitle;
        private System.Windows.Forms.Label lblCancelledOrders;
        private System.Windows.Forms.Label lblUniqueCustomersTitle;
        private System.Windows.Forms.Label lblUniqueCustomers;
        private System.Windows.Forms.Label lblAvgSpentPerCustomerTitle;
        private System.Windows.Forms.Label lblAvgSpentPerCustomer;

        // DataGridViews
        private System.Windows.Forms.DataGridView dgvDailyRevenue;
        private System.Windows.Forms.DataGridView dgvPaymentBreakdown;
        private System.Windows.Forms.DataGridView dgvProductPerformance;
        private System.Windows.Forms.DataGridView dgvOrderStats;
        private System.Windows.Forms.DataGridView dgvHourlyDistribution;
        private System.Windows.Forms.DataGridView dgvTopCustomers;

        // Loading
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
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle10 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle11 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle12 = new DataGridViewCellStyle();
            pnlHeader = new Panel();
            lblTitle = new Label();
            pnlToolbar = new Panel();
            btnToday = new Button();
            btnWeek = new Button();
            btnMonth = new Button();
            btnAllTime = new Button();
            btnFilter = new Button();
            btnRefresh = new Button();
            btnExport = new Button();
            pnlFilters = new Panel();
            lblStartDate = new Label();
            dtpStartDate = new DateTimePicker();
            lblEndDate = new Label();
            dtpEndDate = new DateTimePicker();
            pnlSummary = new Panel();
            lblTotalRevenueTitle = new Label();
            lblTotalRevenue = new Label();
            lblTotalOrdersTitle = new Label();
            lblTotalOrders = new Label();
            lblAvgOrderValueTitle = new Label();
            lblAvgOrderValue = new Label();
            lblTotalDiscountTitle = new Label();
            lblTotalDiscount = new Label();
            lblPendingOrdersTitle = new Label();
            lblPendingOrders = new Label();
            lblCancelledOrdersTitle = new Label();
            lblCancelledOrders = new Label();
            lblUniqueCustomersTitle = new Label();
            lblUniqueCustomers = new Label();
            lblAvgSpentPerCustomerTitle = new Label();
            lblAvgSpentPerCustomer = new Label();
            pnlMain = new Panel();
            pnlLeft = new Panel();
            dgvDailyRevenue = new DataGridView();
            dgvPaymentBreakdown = new DataGridView();
            dgvProductPerformance = new DataGridView();
            pnlRight = new Panel();
            dgvOrderStats = new DataGridView();
            dgvHourlyDistribution = new DataGridView();
            dgvTopCustomers = new DataGridView();
            lblLoading = new Label();
            pnlHeader.SuspendLayout();
            pnlToolbar.SuspendLayout();
            pnlFilters.SuspendLayout();
            pnlSummary.SuspendLayout();
            pnlMain.SuspendLayout();
            pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDailyRevenue).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvPaymentBreakdown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvProductPerformance).BeginInit();
            pnlRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOrderStats).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvHourlyDistribution).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvTopCustomers).BeginInit();
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
            lblTitle.Location = new Point(25, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(500, 40);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "📊 Báo cáo doanh thu";
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlToolbar
            // 
            pnlToolbar.BackColor = Color.White;
            pnlToolbar.Controls.Add(btnToday);
            pnlToolbar.Controls.Add(btnWeek);
            pnlToolbar.Controls.Add(btnMonth);
            pnlToolbar.Controls.Add(btnAllTime);
            pnlToolbar.Controls.Add(btnFilter);
            pnlToolbar.Controls.Add(btnRefresh);
            pnlToolbar.Controls.Add(btnExport);
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
            btnToday.Location = new Point(25, 10);
            btnToday.Name = "btnToday";
            btnToday.Size = new Size(110, 40);
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
            btnWeek.Location = new Point(145, 10);
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
            btnMonth.Location = new Point(265, 10);
            btnMonth.Name = "btnMonth";
            btnMonth.Size = new Size(110, 40);
            btnMonth.TabIndex = 2;
            btnMonth.Text = "🗓️ 30 ngày";
            btnMonth.UseVisualStyleBackColor = false;
            btnMonth.Click += btnMonth_Click;
            // 
            // btnAllTime
            // 
            btnAllTime.BackColor = Color.FromArgb(46, 204, 113);
            btnAllTime.Cursor = Cursors.Hand;
            btnAllTime.FlatAppearance.BorderSize = 0;
            btnAllTime.FlatStyle = FlatStyle.Flat;
            btnAllTime.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAllTime.ForeColor = Color.White;
            btnAllTime.Location = new Point(385, 10);
            btnAllTime.Name = "btnAllTime";
            btnAllTime.Size = new Size(110, 40);
            btnAllTime.TabIndex = 6;
            btnAllTime.Text = "📅 Tất cả";
            btnAllTime.UseVisualStyleBackColor = false;
            btnAllTime.Click += btnAllTime_Click;
            // 
            // btnFilter
            // 
            btnFilter.BackColor = Color.FromArgb(155, 89, 182);
            btnFilter.Cursor = Cursors.Hand;
            btnFilter.FlatAppearance.BorderSize = 0;
            btnFilter.FlatStyle = FlatStyle.Flat;
            btnFilter.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnFilter.ForeColor = Color.White;
            btnFilter.Location = new Point(510, 10);
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
            btnRefresh.Location = new Point(630, 10);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(120, 40);
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
            btnExport.Location = new Point(760, 10);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(130, 40);
            btnExport.TabIndex = 5;
            btnExport.Text = "📥 Xuất Excel";
            btnExport.UseVisualStyleBackColor = false;
            btnExport.Click += btnExport_Click;
            // 
            // pnlFilters
            // 
            pnlFilters.BackColor = Color.White;
            pnlFilters.Controls.Add(lblStartDate);
            pnlFilters.Controls.Add(dtpStartDate);
            pnlFilters.Controls.Add(lblEndDate);
            pnlFilters.Controls.Add(dtpEndDate);
            pnlFilters.Dock = DockStyle.Top;
            pnlFilters.Location = new Point(0, 140);
            pnlFilters.Name = "pnlFilters";
            pnlFilters.Size = new Size(1920, 50);
            pnlFilters.TabIndex = 2;
            // 
            // lblStartDate
            // 
            lblStartDate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblStartDate.Location = new Point(25, 10);
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
            dtpStartDate.Location = new Point(105, 12);
            dtpStartDate.Name = "dtpStartDate";
            dtpStartDate.Size = new Size(130, 23);
            dtpStartDate.TabIndex = 1;
            dtpStartDate.KeyPress += dtpStartDate_KeyPress;
            // 
            // lblEndDate
            // 
            lblEndDate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblEndDate.Location = new Point(245, 10);
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
            dtpEndDate.Location = new Point(325, 12);
            dtpEndDate.Name = "dtpEndDate";
            dtpEndDate.Size = new Size(130, 23);
            dtpEndDate.TabIndex = 3;
            dtpEndDate.KeyPress += dtpEndDate_KeyPress;
            // 
            // pnlSummary
            // 
            pnlSummary.BackColor = Color.White;
            pnlSummary.Controls.Add(lblTotalRevenueTitle);
            pnlSummary.Controls.Add(lblTotalRevenue);
            pnlSummary.Controls.Add(lblTotalOrdersTitle);
            pnlSummary.Controls.Add(lblTotalOrders);
            pnlSummary.Controls.Add(lblAvgOrderValueTitle);
            pnlSummary.Controls.Add(lblAvgOrderValue);
            pnlSummary.Controls.Add(lblTotalDiscountTitle);
            pnlSummary.Controls.Add(lblTotalDiscount);
            pnlSummary.Controls.Add(lblPendingOrdersTitle);
            pnlSummary.Controls.Add(lblPendingOrders);
            pnlSummary.Controls.Add(lblCancelledOrdersTitle);
            pnlSummary.Controls.Add(lblCancelledOrders);
            pnlSummary.Controls.Add(lblUniqueCustomersTitle);
            pnlSummary.Controls.Add(lblUniqueCustomers);
            pnlSummary.Controls.Add(lblAvgSpentPerCustomerTitle);
            pnlSummary.Controls.Add(lblAvgSpentPerCustomer);
            pnlSummary.Dock = DockStyle.Top;
            pnlSummary.Location = new Point(0, 190);
            pnlSummary.Name = "pnlSummary";
            pnlSummary.Size = new Size(1920, 136);
            pnlSummary.TabIndex = 3;
            // 
            // lblTotalRevenueTitle
            // 
            lblTotalRevenueTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTotalRevenueTitle.ForeColor = Color.FromArgb(107, 114, 128);
            lblTotalRevenueTitle.Location = new Point(25, 15);
            lblTotalRevenueTitle.Name = "lblTotalRevenueTitle";
            lblTotalRevenueTitle.Size = new Size(180, 20);
            lblTotalRevenueTitle.TabIndex = 0;
            lblTotalRevenueTitle.Text = "💰 Tổng doanh thu";
            lblTotalRevenueTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTotalRevenue
            // 
            lblTotalRevenue.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTotalRevenue.ForeColor = Color.FromArgb(72, 187, 120);
            lblTotalRevenue.Location = new Point(25, 35);
            lblTotalRevenue.Name = "lblTotalRevenue";
            lblTotalRevenue.Size = new Size(180, 40);
            lblTotalRevenue.TabIndex = 1;
            lblTotalRevenue.Text = "0đ";
            lblTotalRevenue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTotalOrdersTitle
            // 
            lblTotalOrdersTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTotalOrdersTitle.ForeColor = Color.FromArgb(107, 114, 128);
            lblTotalOrdersTitle.Location = new Point(250, 15);
            lblTotalOrdersTitle.Name = "lblTotalOrdersTitle";
            lblTotalOrdersTitle.Size = new Size(150, 20);
            lblTotalOrdersTitle.TabIndex = 2;
            lblTotalOrdersTitle.Text = "📦 Tổng đơn hàng";
            lblTotalOrdersTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTotalOrders
            // 
            lblTotalOrders.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTotalOrders.ForeColor = Color.FromArgb(52, 152, 219);
            lblTotalOrders.Location = new Point(250, 35);
            lblTotalOrders.Name = "lblTotalOrders";
            lblTotalOrders.Size = new Size(150, 40);
            lblTotalOrders.TabIndex = 3;
            lblTotalOrders.Text = "0";
            lblTotalOrders.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblAvgOrderValueTitle
            // 
            lblAvgOrderValueTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblAvgOrderValueTitle.ForeColor = Color.FromArgb(107, 114, 128);
            lblAvgOrderValueTitle.Location = new Point(410, 15);
            lblAvgOrderValueTitle.Name = "lblAvgOrderValueTitle";
            lblAvgOrderValueTitle.Size = new Size(150, 20);
            lblAvgOrderValueTitle.TabIndex = 4;
            lblAvgOrderValueTitle.Text = "📊 TB/đơn";
            lblAvgOrderValueTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblAvgOrderValue
            // 
            lblAvgOrderValue.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblAvgOrderValue.ForeColor = Color.FromArgb(155, 89, 182);
            lblAvgOrderValue.Location = new Point(410, 35);
            lblAvgOrderValue.Name = "lblAvgOrderValue";
            lblAvgOrderValue.Size = new Size(200, 40);
            lblAvgOrderValue.TabIndex = 5;
            lblAvgOrderValue.Text = "0đ";
            lblAvgOrderValue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTotalDiscountTitle
            // 
            lblTotalDiscountTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTotalDiscountTitle.ForeColor = Color.FromArgb(107, 114, 128);
            lblTotalDiscountTitle.Location = new Point(620, 15);
            lblTotalDiscountTitle.Name = "lblTotalDiscountTitle";
            lblTotalDiscountTitle.Size = new Size(150, 20);
            lblTotalDiscountTitle.TabIndex = 6;
            lblTotalDiscountTitle.Text = "🎫 Giảm giá";
            lblTotalDiscountTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTotalDiscount
            // 
            lblTotalDiscount.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTotalDiscount.ForeColor = Color.FromArgb(220, 53, 69);
            lblTotalDiscount.Location = new Point(620, 35);
            lblTotalDiscount.Name = "lblTotalDiscount";
            lblTotalDiscount.Size = new Size(200, 40);
            lblTotalDiscount.TabIndex = 7;
            lblTotalDiscount.Text = "0đ";
            lblTotalDiscount.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblPendingOrdersTitle
            // 
            lblPendingOrdersTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPendingOrdersTitle.ForeColor = Color.FromArgb(107, 114, 128);
            lblPendingOrdersTitle.Location = new Point(25, 75);
            lblPendingOrdersTitle.Name = "lblPendingOrdersTitle";
            lblPendingOrdersTitle.Size = new Size(104, 20);
            lblPendingOrdersTitle.TabIndex = 8;
            lblPendingOrdersTitle.Text = "⏳ Chờ xử lý";
            lblPendingOrdersTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblPendingOrders
            // 
            lblPendingOrders.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblPendingOrders.ForeColor = Color.FromArgb(255, 193, 7);
            lblPendingOrders.Location = new Point(25, 95);
            lblPendingOrders.Name = "lblPendingOrders";
            lblPendingOrders.Size = new Size(100, 30);
            lblPendingOrders.TabIndex = 9;
            lblPendingOrders.Text = "0";
            lblPendingOrders.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblCancelledOrdersTitle
            // 
            lblCancelledOrdersTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCancelledOrdersTitle.ForeColor = Color.FromArgb(107, 114, 128);
            lblCancelledOrdersTitle.Location = new Point(135, 75);
            lblCancelledOrdersTitle.Name = "lblCancelledOrdersTitle";
            lblCancelledOrdersTitle.Size = new Size(100, 20);
            lblCancelledOrdersTitle.TabIndex = 10;
            lblCancelledOrdersTitle.Text = "❌ Đã hủy";
            lblCancelledOrdersTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblCancelledOrders
            // 
            lblCancelledOrders.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblCancelledOrders.ForeColor = Color.FromArgb(220, 53, 69);
            lblCancelledOrders.Location = new Point(135, 95);
            lblCancelledOrders.Name = "lblCancelledOrders";
            lblCancelledOrders.Size = new Size(100, 30);
            lblCancelledOrders.TabIndex = 11;
            lblCancelledOrders.Text = "0";
            lblCancelledOrders.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblUniqueCustomersTitle
            // 
            lblUniqueCustomersTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblUniqueCustomersTitle.ForeColor = Color.FromArgb(107, 114, 128);
            lblUniqueCustomersTitle.Location = new Point(250, 75);
            lblUniqueCustomersTitle.Name = "lblUniqueCustomersTitle";
            lblUniqueCustomersTitle.Size = new Size(125, 20);
            lblUniqueCustomersTitle.TabIndex = 12;
            lblUniqueCustomersTitle.Text = "👥 Khách hàng";
            lblUniqueCustomersTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblUniqueCustomers
            // 
            lblUniqueCustomers.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblUniqueCustomers.ForeColor = Color.FromArgb(44, 62, 80);
            lblUniqueCustomers.Location = new Point(250, 95);
            lblUniqueCustomers.Name = "lblUniqueCustomers";
            lblUniqueCustomers.Size = new Size(100, 30);
            lblUniqueCustomers.TabIndex = 13;
            lblUniqueCustomers.Text = "0";
            lblUniqueCustomers.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblAvgSpentPerCustomerTitle
            // 
            lblAvgSpentPerCustomerTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblAvgSpentPerCustomerTitle.ForeColor = Color.FromArgb(107, 114, 128);
            lblAvgSpentPerCustomerTitle.Location = new Point(360, 75);
            lblAvgSpentPerCustomerTitle.Name = "lblAvgSpentPerCustomerTitle";
            lblAvgSpentPerCustomerTitle.Size = new Size(120, 20);
            lblAvgSpentPerCustomerTitle.TabIndex = 14;
            lblAvgSpentPerCustomerTitle.Text = "💵 TB/KH";
            lblAvgSpentPerCustomerTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblAvgSpentPerCustomer
            // 
            lblAvgSpentPerCustomer.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblAvgSpentPerCustomer.ForeColor = Color.FromArgb(44, 62, 80);
            lblAvgSpentPerCustomer.Location = new Point(360, 95);
            lblAvgSpentPerCustomer.Name = "lblAvgSpentPerCustomer";
            lblAvgSpentPerCustomer.Size = new Size(180, 30);
            lblAvgSpentPerCustomer.TabIndex = 15;
            lblAvgSpentPerCustomer.Text = "0đ";
            lblAvgSpentPerCustomer.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlMain
            // 
            pnlMain.Controls.Add(pnlLeft);
            pnlMain.Controls.Add(pnlRight);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 326);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new Padding(10);
            pnlMain.Size = new Size(1920, 734);
            pnlMain.TabIndex = 4;
            // 
            // pnlLeft
            // 
            pnlLeft.Controls.Add(dgvDailyRevenue);
            pnlLeft.Controls.Add(dgvPaymentBreakdown);
            pnlLeft.Controls.Add(dgvProductPerformance);
            pnlLeft.Dock = DockStyle.Fill;
            pnlLeft.Location = new Point(10, 10);
            pnlLeft.Name = "pnlLeft";
            pnlLeft.Padding = new Padding(0, 0, 5, 0);
            pnlLeft.Size = new Size(1060, 714);
            pnlLeft.TabIndex = 0;
            // 
            // dgvDailyRevenue
            // 
            dgvDailyRevenue.AllowUserToAddRows = false;
            dgvDailyRevenue.AllowUserToDeleteRows = false;
            dataGridViewCellStyle7.BackColor = Color.FromArgb(240, 242, 245);
            dgvDailyRevenue.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            dgvDailyRevenue.BackgroundColor = Color.FromArgb(247, 249, 252);
            dgvDailyRevenue.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = Color.FromArgb(44, 62, 80);
            dataGridViewCellStyle8.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle8.ForeColor = Color.White;
            dataGridViewCellStyle8.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dataGridViewCellStyle8.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.True;
            dgvDailyRevenue.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            dgvDailyRevenue.ColumnHeadersHeight = 40;
            dgvDailyRevenue.Dock = DockStyle.Top;
            dgvDailyRevenue.EnableHeadersVisualStyles = false;
            dgvDailyRevenue.Location = new Point(0, 180);
            dgvDailyRevenue.Name = "dgvDailyRevenue";
            dgvDailyRevenue.ReadOnly = true;
            dgvDailyRevenue.RowHeadersVisible = false;
            dgvDailyRevenue.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDailyRevenue.Size = new Size(1055, 220);
            dgvDailyRevenue.TabIndex = 0;
            // 
            // dgvPaymentBreakdown
            // 
            dgvPaymentBreakdown.AllowUserToAddRows = false;
            dgvPaymentBreakdown.AllowUserToDeleteRows = false;
            dataGridViewCellStyle9.BackColor = Color.FromArgb(240, 242, 245);
            dgvPaymentBreakdown.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle9;
            dgvPaymentBreakdown.BackgroundColor = Color.FromArgb(247, 249, 252);
            dgvPaymentBreakdown.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle10.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = Color.FromArgb(52, 152, 219);
            dataGridViewCellStyle10.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle10.ForeColor = Color.White;
            dataGridViewCellStyle10.SelectionBackColor = Color.FromArgb(41, 128, 185);
            dataGridViewCellStyle10.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = DataGridViewTriState.True;
            dgvPaymentBreakdown.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            dgvPaymentBreakdown.ColumnHeadersHeight = 40;
            dgvPaymentBreakdown.Dock = DockStyle.Top;
            dgvPaymentBreakdown.EnableHeadersVisualStyles = false;
            dgvPaymentBreakdown.Location = new Point(0, 0);
            dgvPaymentBreakdown.Name = "dgvPaymentBreakdown";
            dgvPaymentBreakdown.ReadOnly = true;
            dgvPaymentBreakdown.RowHeadersVisible = false;
            dgvPaymentBreakdown.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPaymentBreakdown.Size = new Size(1055, 180);
            dgvPaymentBreakdown.TabIndex = 1;
            // 
            // dgvProductPerformance
            // 
            dgvProductPerformance.AllowUserToAddRows = false;
            dgvProductPerformance.AllowUserToDeleteRows = false;
            dataGridViewCellStyle11.BackColor = Color.FromArgb(240, 242, 245);
            dgvProductPerformance.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle11;
            dgvProductPerformance.BackgroundColor = Color.FromArgb(247, 249, 252);
            dgvProductPerformance.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle12.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = Color.FromArgb(230, 126, 34);
            dataGridViewCellStyle12.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle12.ForeColor = Color.White;
            dataGridViewCellStyle12.SelectionBackColor = Color.FromArgb(211, 84, 0);
            dataGridViewCellStyle12.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = DataGridViewTriState.True;
            dgvProductPerformance.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle12;
            dgvProductPerformance.ColumnHeadersHeight = 40;
            dgvProductPerformance.Dock = DockStyle.Fill;
            dgvProductPerformance.EnableHeadersVisualStyles = false;
            dgvProductPerformance.Location = new Point(0, 0);
            dgvProductPerformance.Name = "dgvProductPerformance";
            dgvProductPerformance.ReadOnly = true;
            dgvProductPerformance.RowHeadersVisible = false;
            dgvProductPerformance.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductPerformance.Size = new Size(1055, 714);
            dgvProductPerformance.TabIndex = 2;
            // 
            // pnlRight
            // 
            pnlRight.Controls.Add(dgvOrderStats);
            pnlRight.Controls.Add(dgvHourlyDistribution);
            pnlRight.Controls.Add(dgvTopCustomers);
            pnlRight.Dock = DockStyle.Right;
            pnlRight.Location = new Point(1070, 10);
            pnlRight.Name = "pnlRight";
            pnlRight.Padding = new Padding(0, 0, 10, 0);
            pnlRight.Size = new Size(840, 714);
            pnlRight.TabIndex = 1;
            // 
            // dgvOrderStats
            // 
            dgvOrderStats.AllowUserToAddRows = false;
            dgvOrderStats.AllowUserToDeleteRows = false;
            dgvOrderStats.BackgroundColor = Color.FromArgb(247, 249, 252);
            dgvOrderStats.BorderStyle = BorderStyle.None;
            dgvOrderStats.ColumnHeadersHeight = 40;
            dgvOrderStats.Dock = DockStyle.Top;
            dgvOrderStats.EnableHeadersVisualStyles = false;
            dgvOrderStats.Location = new Point(0, 250);
            dgvOrderStats.Name = "dgvOrderStats";
            dgvOrderStats.ReadOnly = true;
            dgvOrderStats.RowHeadersVisible = false;
            dgvOrderStats.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrderStats.Size = new Size(830, 220);
            dgvOrderStats.TabIndex = 0;
            // 
            // dgvHourlyDistribution
            // 
            dgvHourlyDistribution.AllowUserToAddRows = false;
            dgvHourlyDistribution.AllowUserToDeleteRows = false;
            dgvHourlyDistribution.BackgroundColor = Color.FromArgb(247, 249, 252);
            dgvHourlyDistribution.BorderStyle = BorderStyle.None;
            dgvHourlyDistribution.ColumnHeadersHeight = 40;
            dgvHourlyDistribution.Dock = DockStyle.Top;
            dgvHourlyDistribution.EnableHeadersVisualStyles = false;
            dgvHourlyDistribution.Location = new Point(0, 0);
            dgvHourlyDistribution.Name = "dgvHourlyDistribution";
            dgvHourlyDistribution.ReadOnly = true;
            dgvHourlyDistribution.RowHeadersVisible = false;
            dgvHourlyDistribution.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHourlyDistribution.Size = new Size(830, 250);
            dgvHourlyDistribution.TabIndex = 1;
            // 
            // dgvTopCustomers
            // 
            dgvTopCustomers.AllowUserToAddRows = false;
            dgvTopCustomers.AllowUserToDeleteRows = false;
            dgvTopCustomers.BackgroundColor = Color.FromArgb(247, 249, 252);
            dgvTopCustomers.BorderStyle = BorderStyle.None;
            dgvTopCustomers.ColumnHeadersHeight = 40;
            dgvTopCustomers.Dock = DockStyle.Fill;
            dgvTopCustomers.EnableHeadersVisualStyles = false;
            dgvTopCustomers.Location = new Point(0, 0);
            dgvTopCustomers.Name = "dgvTopCustomers";
            dgvTopCustomers.ReadOnly = true;
            dgvTopCustomers.RowHeadersVisible = false;
            dgvTopCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTopCustomers.Size = new Size(830, 714);
            dgvTopCustomers.TabIndex = 2;
            // 
            // lblLoading
            // 
            lblLoading.AutoSize = true;
            lblLoading.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblLoading.ForeColor = Color.FromArgb(52, 152, 219);
            lblLoading.Location = new Point(850, 500);
            lblLoading.Name = "lblLoading";
            lblLoading.Size = new Size(172, 21);
            lblLoading.TabIndex = 99;
            lblLoading.Text = "⏳ Đang tải dữ liệu...";
            lblLoading.Visible = false;
            // 
            // frmSalesReport
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(247, 249, 252);
            ClientSize = new Size(1920, 1060);
            Controls.Add(pnlMain);
            Controls.Add(pnlSummary);
            Controls.Add(pnlFilters);
            Controls.Add(pnlToolbar);
            Controls.Add(pnlHeader);
            Controls.Add(lblLoading);
            Name = "frmSalesReport";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "📊 Báo cáo doanh thu - MilkTeaPOS";
            WindowState = FormWindowState.Maximized;
            pnlHeader.ResumeLayout(false);
            pnlToolbar.ResumeLayout(false);
            pnlFilters.ResumeLayout(false);
            pnlSummary.ResumeLayout(false);
            pnlMain.ResumeLayout(false);
            pnlLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDailyRevenue).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvPaymentBreakdown).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvProductPerformance).EndInit();
            pnlRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvOrderStats).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvHourlyDistribution).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvTopCustomers).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void InitializeDataGridViewColumns()
        {
            // dgvDailyRevenue columns
            dgvDailyRevenue.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
            {
                new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Date", HeaderText = "📅 Ngày", Width = 130, DefaultCellStyle = new DataGridViewCellStyle { Font = new System.Drawing.Font("Segoe UI", 10F), Padding = new System.Windows.Forms.Padding(8, 5, 8, 5) } },
                new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "DayOfWeek", HeaderText = "Thứ", Width = 70, DefaultCellStyle = new DataGridViewCellStyle { Font = new System.Drawing.Font("Segoe UI", 10F), Padding = new System.Windows.Forms.Padding(8, 5, 8, 5), Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter } },
                new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Orders", HeaderText = "Đơn hàng", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Font = new System.Drawing.Font("Segoe UI", 10F), Padding = new System.Windows.Forms.Padding(8, 5, 8, 5), Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter } },
                new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Revenue", HeaderText = "💰 Doanh thu", Width = 180, DefaultCellStyle = new DataGridViewCellStyle { Font = new System.Drawing.Font("Segoe UI", 10F), Padding = new System.Windows.Forms.Padding(8, 5, 8, 5) } }
            });

            // dgvPaymentBreakdown columns
            dgvPaymentBreakdown.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
            {
                new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Method", HeaderText = "💳 Phương thức", Width = 200, DefaultCellStyle = new DataGridViewCellStyle { Font = new System.Drawing.Font("Segoe UI", 10F), Padding = new System.Windows.Forms.Padding(8, 5, 8, 5) } },
                new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Count", HeaderText = "Số GD", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Font = new System.Drawing.Font("Segoe UI", 10F), Padding = new System.Windows.Forms.Padding(8, 5, 8, 5), Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter } },
                new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Total", HeaderText = "💰 Tổng tiền", Width = 180, DefaultCellStyle = new DataGridViewCellStyle { Font = new System.Drawing.Font("Segoe UI", 10F), Padding = new System.Windows.Forms.Padding(8, 5, 8, 5) } },
                new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "AvgAmount", HeaderText = "📊 Trung bình", Width = 140, DefaultCellStyle = new DataGridViewCellStyle { Font = new System.Drawing.Font("Segoe UI", 10F), Padding = new System.Windows.Forms.Padding(8, 5, 8, 5) } },
                new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Percentage", HeaderText = "Tỷ lệ", Width = 90, DefaultCellStyle = new DataGridViewCellStyle { Font = new System.Drawing.Font("Segoe UI", 10F), Padding = new System.Windows.Forms.Padding(8, 5, 8, 5), Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter } }
            });

            // dgvProductPerformance columns
            dgvProductPerformance.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
            {
                new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Rank", HeaderText = "XH", Width = 60, DefaultCellStyle = new DataGridViewCellStyle { Font = new System.Drawing.Font("Segoe UI", 10F), Padding = new System.Windows.Forms.Padding(8, 5, 8, 5), Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter } },
                new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "ProductName", HeaderText = "🧋 Sản phẩm", Width = 300, DefaultCellStyle = new DataGridViewCellStyle { Font = new System.Drawing.Font("Segoe UI", 10F), Padding = new System.Windows.Forms.Padding(8, 5, 8, 5) } },
                new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Quantity", HeaderText = "SL", Width = 80, DefaultCellStyle = new DataGridViewCellStyle { Font = new System.Drawing.Font("Segoe UI", 10F), Padding = new System.Windows.Forms.Padding(8, 5, 8, 5), Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter } },
                new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Revenue", HeaderText = "💰 Doanh thu", Width = 180, DefaultCellStyle = new DataGridViewCellStyle { Font = new System.Drawing.Font("Segoe UI", 10F), Padding = new System.Windows.Forms.Padding(8, 5, 8, 5) } },
                new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "AvgPrice", HeaderText = "Giá TB", Width = 120, DefaultCellStyle = new DataGridViewCellStyle { Font = new System.Drawing.Font("Segoe UI", 10F), Padding = new System.Windows.Forms.Padding(8, 5, 8, 5) } }
            });

            // dgvOrderStats columns
            dgvOrderStats.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
            {
                new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "OrderStatLabel", HeaderText = "Trạng thái", Width = 250, DefaultCellStyle = new DataGridViewCellStyle { Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold), Padding = new System.Windows.Forms.Padding(8, 5, 8, 5) } },
                new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "OrderStatValue", HeaderText = "Số lượng", Width = 120, DefaultCellStyle = new DataGridViewCellStyle { Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold), Padding = new System.Windows.Forms.Padding(8, 5, 8, 5), Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter } }
            });

            // dgvHourlyDistribution columns
            dgvHourlyDistribution.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
            {
                new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Hour", HeaderText = "⏰ Giờ", Width = 80, DefaultCellStyle = new DataGridViewCellStyle { Font = new System.Drawing.Font("Segoe UI", 10F), Padding = new System.Windows.Forms.Padding(8, 5, 8, 5) } },
                new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Orders", HeaderText = "Đơn", Width = 80, DefaultCellStyle = new DataGridViewCellStyle { Font = new System.Drawing.Font("Segoe UI", 10F), Padding = new System.Windows.Forms.Padding(8, 5, 8, 5), Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter } },
                new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Revenue", HeaderText = "💰 Doanh thu", Width = 160, DefaultCellStyle = new DataGridViewCellStyle { Font = new System.Drawing.Font("Segoe UI", 10F), Padding = new System.Windows.Forms.Padding(8, 5, 8, 5) } },
                new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Bar", HeaderText = "📊 Biểu đồ", Width = 400, DefaultCellStyle = new DataGridViewCellStyle { Font = new System.Drawing.Font("Consolas", 10F), Padding = new System.Windows.Forms.Padding(8, 5, 8, 5) } }
            });

            // dgvTopCustomers columns
            dgvTopCustomers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
            {
                new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Rank", HeaderText = "XH", Width = 60, DefaultCellStyle = new DataGridViewCellStyle { Font = new System.Drawing.Font("Segoe UI", 10F), Padding = new System.Windows.Forms.Padding(8, 5, 8, 5), Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter } },
                new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "CustomerName", HeaderText = "👤 Khách hàng", Width = 300, DefaultCellStyle = new DataGridViewCellStyle { Font = new System.Drawing.Font("Segoe UI", 10F), Padding = new System.Windows.Forms.Padding(8, 5, 8, 5) } },
                new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "OrderCount", HeaderText = "Số đơn", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Font = new System.Drawing.Font("Segoe UI", 10F), Padding = new System.Windows.Forms.Padding(8, 5, 8, 5), Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter } },
                new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "TotalSpent", HeaderText = "💰 Tổng chi", Width = 180, DefaultCellStyle = new DataGridViewCellStyle { Font = new System.Drawing.Font("Segoe UI", 10F), Padding = new System.Windows.Forms.Padding(8, 5, 8, 5) } }
            });

            // Set AutoSizeColumnsMode
            dgvDailyRevenue.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvPaymentBreakdown.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvProductPerformance.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvOrderStats.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvHourlyDistribution.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvTopCustomers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            // Set RowTemplate heights
            dgvDailyRevenue.RowTemplate.Height = 35;
            dgvPaymentBreakdown.RowTemplate.Height = 35;
            dgvProductPerformance.RowTemplate.Height = 35;
            dgvOrderStats.RowTemplate.Height = 35;
            dgvHourlyDistribution.RowTemplate.Height = 35;
            dgvTopCustomers.RowTemplate.Height = 35;
        }
    }
}
