namespace MilkTeaPOS
{
    partial class frmDashboard
    {
        private System.ComponentModel.IContainer components = null;

        // Panels
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlCards;
        private System.Windows.Forms.Panel pnlContent;

        // KPI Cards
        private System.Windows.Forms.Panel pnlCard1;
        private System.Windows.Forms.Panel pnlCard2;
        private System.Windows.Forms.Panel pnlCard3;
        private System.Windows.Forms.Panel pnlCard4;
        private System.Windows.Forms.Panel pnlCard5;
        private System.Windows.Forms.Label lblCard1Emoji;
        private System.Windows.Forms.Label lblCard1Value;
        private System.Windows.Forms.Label lblCard1Trend;
        private System.Windows.Forms.Label lblCard2Emoji;
        private System.Windows.Forms.Label lblCard2Value;
        private System.Windows.Forms.Label lblCard2Trend;
        private System.Windows.Forms.Label lblCard3Emoji;
        private System.Windows.Forms.Label lblCard3Value;
        private System.Windows.Forms.Label lblCard3Trend;
        private System.Windows.Forms.Label lblCard4Emoji;
        private System.Windows.Forms.Label lblCard4Value;
        private System.Windows.Forms.Label lblCard4Trend;
        private System.Windows.Forms.Label lblCard5Emoji;
        private System.Windows.Forms.Label lblCard5Value;
        private System.Windows.Forms.Label lblCard5Trend;

        // Header
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblClock;
        private System.Windows.Forms.Label lblDate;

        // Tab Control
        private System.Windows.Forms.TabControl tcDashboard;
        private System.Windows.Forms.TabPage tabOverview;
        private System.Windows.Forms.TabPage tabCharts;
        private System.Windows.Forms.TabPage tabTables;
        private System.Windows.Forms.TabPage tabStats;

        // Tab: Overview
        private System.Windows.Forms.SplitContainer spOverview;
        private System.Windows.Forms.FlowLayoutPanel pnlLeft;
        private System.Windows.Forms.FlowLayoutPanel pnlRight;
        private System.Windows.Forms.Label lblSectionRecent;
        private System.Windows.Forms.DataGridView dgvRecentOrders;
        private System.Windows.Forms.Label lblSectionTopProducts;
        private System.Windows.Forms.DataGridView dgvTopProducts;
        private System.Windows.Forms.Label lblSectionPayments;
        private System.Windows.Forms.DataGridView dgvPaymentBreakdown;

        // Tab: Charts
        private System.Windows.Forms.Label lblChartInfo;
        private System.Windows.Forms.Label lblHourlyInfo;

        // Tab: Tables
        private System.Windows.Forms.Label lblSectionTables;
        private System.Windows.Forms.DataGridView dgvTables;

        // Cached Fonts
        private System.Drawing.Font _fontCardValue;
        private System.Drawing.Font _fontCardEmoji;
        private System.Drawing.Font _fontCardTrend;
        private System.Drawing.Font _fontSectionTitle;
        private System.Drawing.Font _fontRow;
        private System.Drawing.Font _fontRowBold;
        private System.Drawing.Font _fontClock;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            _fontCardValue?.Dispose();
            _fontCardEmoji?.Dispose();
            _fontCardTrend?.Dispose();
            _fontSectionTitle?.Dispose();
            _fontRow?.Dispose();
            _fontRowBold?.Dispose();
            _fontClock?.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            pnlHeader = new Panel();
            lblTitle = new Label();
            lblClock = new Label();
            lblDate = new Label();
            pnlCards = new Panel();
            pnlCard1 = new Panel();
            lblCard1Emoji = new Label();
            lblCard1Value = new Label();
            lblCard1Trend = new Label();
            pnlCard2 = new Panel();
            lblCard2Emoji = new Label();
            lblCard2Value = new Label();
            lblCard2Trend = new Label();
            pnlCard3 = new Panel();
            lblCard3Emoji = new Label();
            lblCard3Value = new Label();
            lblCard3Trend = new Label();
            pnlCard4 = new Panel();
            lblCard4Emoji = new Label();
            lblCard4Value = new Label();
            lblCard4Trend = new Label();
            pnlCard5 = new Panel();
            lblCard5Emoji = new Label();
            lblCard5Value = new Label();
            lblCard5Trend = new Label();
            pnlContent = new Panel();
            tcDashboard = new TabControl();
            tabOverview = new TabPage();
            spOverview = new SplitContainer();
            pnlLeft = new FlowLayoutPanel();
            lblSectionRecent = new Label();
            dgvRecentOrders = new DataGridView();
            pnlRight = new FlowLayoutPanel();
            lblSectionTopProducts = new Label();
            dgvTopProducts = new DataGridView();
            lblSectionPayments = new Label();
            dgvPaymentBreakdown = new DataGridView();
            tabCharts = new TabPage();
            lblChartInfo = new Label();
            lblHourlyInfo = new Label();
            tabTables = new TabPage();
            dgvTables = new DataGridView();
            lblSectionTables = new Label();
            tabStats = new TabPage();
            lblStatsInfo = new Label();
            pnlHeader.SuspendLayout();
            pnlCards.SuspendLayout();
            pnlCard1.SuspendLayout();
            pnlCard2.SuspendLayout();
            pnlCard3.SuspendLayout();
            pnlCard4.SuspendLayout();
            pnlCard5.SuspendLayout();
            pnlContent.SuspendLayout();
            tcDashboard.SuspendLayout();
            tabOverview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)spOverview).BeginInit();
            spOverview.Panel1.SuspendLayout();
            spOverview.Panel2.SuspendLayout();
            spOverview.SuspendLayout();
            pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRecentOrders).BeginInit();
            pnlRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTopProducts).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvPaymentBreakdown).BeginInit();
            tabCharts.SuspendLayout();
            tabTables.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTables).BeginInit();
            tabStats.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.White;
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(lblClock);
            pnlHeader.Controls.Add(lblDate);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1350, 85);
            pnlHeader.TabIndex = 2;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(45, 55, 72);
            lblTitle.Location = new Point(20, 10);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(344, 32);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "☕ MilkTea POS - Dashboard";
            // 
            // lblClock
            // 
            lblClock.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblClock.ForeColor = Color.FromArgb(72, 187, 120);
            lblClock.Location = new Point(1050, 15);
            lblClock.Name = "lblClock";
            lblClock.Size = new Size(130, 30);
            lblClock.TabIndex = 1;
            lblClock.Text = "00:00:00";
            lblClock.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblDate
            // 
            lblDate.Font = new Font("Segoe UI", 10F);
            lblDate.ForeColor = Color.FromArgb(108, 117, 125);
            lblDate.Location = new Point(1050, 50);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(280, 22);
            lblDate.TabIndex = 2;
            lblDate.TextAlign = ContentAlignment.MiddleRight;
            // 
            // pnlCards
            // 
            pnlCards.BackColor = Color.FromArgb(240, 242, 245);
            pnlCards.Controls.Add(pnlCard1);
            pnlCards.Controls.Add(pnlCard2);
            pnlCards.Controls.Add(pnlCard3);
            pnlCards.Controls.Add(pnlCard4);
            pnlCards.Controls.Add(pnlCard5);
            pnlCards.Dock = DockStyle.Top;
            pnlCards.Location = new Point(0, 85);
            pnlCards.Name = "pnlCards";
            pnlCards.Size = new Size(1350, 133);
            pnlCards.TabIndex = 1;
            // 
            // pnlCard1
            // 
            pnlCard1.BackColor = Color.FromArgb(255, 107, 107);
            pnlCard1.Controls.Add(lblCard1Emoji);
            pnlCard1.Controls.Add(lblCard1Value);
            pnlCard1.Controls.Add(lblCard1Trend);
            pnlCard1.Cursor = Cursors.Hand;
            pnlCard1.Location = new Point(15, 10);
            pnlCard1.Name = "pnlCard1";
            pnlCard1.Size = new Size(240, 117);
            pnlCard1.TabIndex = 0;
            // 
            // lblCard1Emoji
            // 
            lblCard1Emoji.AutoSize = true;
            lblCard1Emoji.Font = new Font("Segoe UI", 18F);
            lblCard1Emoji.ForeColor = Color.White;
            lblCard1Emoji.Location = new Point(12, 10);
            lblCard1Emoji.Name = "lblCard1Emoji";
            lblCard1Emoji.Size = new Size(47, 32);
            lblCard1Emoji.TabIndex = 0;
            lblCard1Emoji.Text = "💰";
            // 
            // lblCard1Value
            // 
            lblCard1Value.AutoSize = true;
            lblCard1Value.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblCard1Value.ForeColor = Color.White;
            lblCard1Value.Location = new Point(12, 40);
            lblCard1Value.Name = "lblCard1Value";
            lblCard1Value.Size = new Size(50, 37);
            lblCard1Value.TabIndex = 1;
            lblCard1Value.Text = "0đ";
            // 
            // lblCard1Trend
            // 
            lblCard1Trend.AutoSize = true;
            lblCard1Trend.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblCard1Trend.ForeColor = Color.FromArgb(255, 255, 200);
            lblCard1Trend.Location = new Point(12, 77);
            lblCard1Trend.Name = "lblCard1Trend";
            lblCard1Trend.Size = new Size(0, 15);
            lblCard1Trend.TabIndex = 2;
            // 
            // pnlCard2
            // 
            pnlCard2.BackColor = Color.FromArgb(72, 187, 120);
            pnlCard2.Controls.Add(lblCard2Emoji);
            pnlCard2.Controls.Add(lblCard2Value);
            pnlCard2.Controls.Add(lblCard2Trend);
            pnlCard2.Cursor = Cursors.Hand;
            pnlCard2.Location = new Point(270, 10);
            pnlCard2.Name = "pnlCard2";
            pnlCard2.Size = new Size(240, 117);
            pnlCard2.TabIndex = 1;
            // 
            // lblCard2Emoji
            // 
            lblCard2Emoji.AutoSize = true;
            lblCard2Emoji.Font = new Font("Segoe UI", 18F);
            lblCard2Emoji.ForeColor = Color.White;
            lblCard2Emoji.Location = new Point(12, 10);
            lblCard2Emoji.Name = "lblCard2Emoji";
            lblCard2Emoji.Size = new Size(47, 32);
            lblCard2Emoji.TabIndex = 0;
            lblCard2Emoji.Text = "📦";
            // 
            // lblCard2Value
            // 
            lblCard2Value.AutoSize = true;
            lblCard2Value.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblCard2Value.ForeColor = Color.White;
            lblCard2Value.Location = new Point(12, 40);
            lblCard2Value.Name = "lblCard2Value";
            lblCard2Value.Size = new Size(33, 37);
            lblCard2Value.TabIndex = 1;
            lblCard2Value.Text = "0";
            // 
            // lblCard2Trend
            // 
            lblCard2Trend.AutoSize = true;
            lblCard2Trend.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblCard2Trend.ForeColor = Color.FromArgb(255, 255, 200);
            lblCard2Trend.Location = new Point(12, 77);
            lblCard2Trend.Name = "lblCard2Trend";
            lblCard2Trend.Size = new Size(0, 15);
            lblCard2Trend.TabIndex = 2;
            // 
            // pnlCard3
            // 
            pnlCard3.BackColor = Color.FromArgb(255, 193, 7);
            pnlCard3.Controls.Add(lblCard3Emoji);
            pnlCard3.Controls.Add(lblCard3Value);
            pnlCard3.Controls.Add(lblCard3Trend);
            pnlCard3.Cursor = Cursors.Hand;
            pnlCard3.Location = new Point(525, 10);
            pnlCard3.Name = "pnlCard3";
            pnlCard3.Size = new Size(240, 117);
            pnlCard3.TabIndex = 2;
            // 
            // lblCard3Emoji
            // 
            lblCard3Emoji.AutoSize = true;
            lblCard3Emoji.Font = new Font("Segoe UI", 18F);
            lblCard3Emoji.ForeColor = Color.White;
            lblCard3Emoji.Location = new Point(12, 10);
            lblCard3Emoji.Name = "lblCard3Emoji";
            lblCard3Emoji.Size = new Size(47, 32);
            lblCard3Emoji.TabIndex = 0;
            lblCard3Emoji.Text = "\U0001fa91";
            // 
            // lblCard3Value
            // 
            lblCard3Value.AutoSize = true;
            lblCard3Value.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblCard3Value.ForeColor = Color.White;
            lblCard3Value.Location = new Point(12, 40);
            lblCard3Value.Name = "lblCard3Value";
            lblCard3Value.Size = new Size(61, 37);
            lblCard3Value.TabIndex = 1;
            lblCard3Value.Text = "0/0";
            // 
            // lblCard3Trend
            // 
            lblCard3Trend.AutoSize = true;
            lblCard3Trend.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblCard3Trend.ForeColor = Color.FromArgb(255, 255, 200);
            lblCard3Trend.Location = new Point(12, 77);
            lblCard3Trend.Name = "lblCard3Trend";
            lblCard3Trend.Size = new Size(0, 15);
            lblCard3Trend.TabIndex = 2;
            // 
            // pnlCard4
            // 
            pnlCard4.BackColor = Color.FromArgb(23, 162, 184);
            pnlCard4.Controls.Add(lblCard4Emoji);
            pnlCard4.Controls.Add(lblCard4Value);
            pnlCard4.Controls.Add(lblCard4Trend);
            pnlCard4.Cursor = Cursors.Hand;
            pnlCard4.Location = new Point(780, 10);
            pnlCard4.Name = "pnlCard4";
            pnlCard4.Size = new Size(240, 117);
            pnlCard4.TabIndex = 3;
            // 
            // lblCard4Emoji
            // 
            lblCard4Emoji.AutoSize = true;
            lblCard4Emoji.Font = new Font("Segoe UI", 18F);
            lblCard4Emoji.ForeColor = Color.White;
            lblCard4Emoji.Location = new Point(12, 10);
            lblCard4Emoji.Name = "lblCard4Emoji";
            lblCard4Emoji.Size = new Size(47, 32);
            lblCard4Emoji.TabIndex = 0;
            lblCard4Emoji.Text = "👥";
            // 
            // lblCard4Value
            // 
            lblCard4Value.AutoSize = true;
            lblCard4Value.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblCard4Value.ForeColor = Color.White;
            lblCard4Value.Location = new Point(12, 40);
            lblCard4Value.Name = "lblCard4Value";
            lblCard4Value.Size = new Size(33, 37);
            lblCard4Value.TabIndex = 1;
            lblCard4Value.Text = "0";
            // 
            // lblCard4Trend
            // 
            lblCard4Trend.AutoSize = true;
            lblCard4Trend.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblCard4Trend.ForeColor = Color.FromArgb(255, 255, 200);
            lblCard4Trend.Location = new Point(12, 77);
            lblCard4Trend.Name = "lblCard4Trend";
            lblCard4Trend.Size = new Size(0, 15);
            lblCard4Trend.TabIndex = 2;
            // 
            // pnlCard5
            // 
            pnlCard5.BackColor = Color.FromArgb(111, 66, 193);
            pnlCard5.Controls.Add(lblCard5Emoji);
            pnlCard5.Controls.Add(lblCard5Value);
            pnlCard5.Controls.Add(lblCard5Trend);
            pnlCard5.Cursor = Cursors.Hand;
            pnlCard5.Location = new Point(1035, 10);
            pnlCard5.Name = "pnlCard5";
            pnlCard5.Size = new Size(240, 117);
            pnlCard5.TabIndex = 4;
            // 
            // lblCard5Emoji
            // 
            lblCard5Emoji.AutoSize = true;
            lblCard5Emoji.Font = new Font("Segoe UI", 18F);
            lblCard5Emoji.ForeColor = Color.White;
            lblCard5Emoji.Location = new Point(12, 10);
            lblCard5Emoji.Name = "lblCard5Emoji";
            lblCard5Emoji.Size = new Size(47, 32);
            lblCard5Emoji.TabIndex = 0;
            lblCard5Emoji.Text = "📈";
            // 
            // lblCard5Value
            // 
            lblCard5Value.AutoSize = true;
            lblCard5Value.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblCard5Value.ForeColor = Color.White;
            lblCard5Value.Location = new Point(12, 40);
            lblCard5Value.Name = "lblCard5Value";
            lblCard5Value.Size = new Size(50, 37);
            lblCard5Value.TabIndex = 1;
            lblCard5Value.Text = "0đ";
            // 
            // lblCard5Trend
            // 
            lblCard5Trend.AutoSize = true;
            lblCard5Trend.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblCard5Trend.ForeColor = Color.FromArgb(255, 255, 200);
            lblCard5Trend.Location = new Point(12, 77);
            lblCard5Trend.Name = "lblCard5Trend";
            lblCard5Trend.Size = new Size(0, 15);
            lblCard5Trend.TabIndex = 2;
            // 
            // pnlContent
            // 
            pnlContent.Controls.Add(tcDashboard);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(0, 218);
            pnlContent.Margin = new Padding(0);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(1350, 843);
            pnlContent.TabIndex = 0;
            // 
            // tcDashboard
            // 
            tcDashboard.Controls.Add(tabOverview);
            tcDashboard.Controls.Add(tabCharts);
            tcDashboard.Controls.Add(tabTables);
            tcDashboard.Controls.Add(tabStats);
            tcDashboard.Dock = DockStyle.Fill;
            tcDashboard.Font = new Font("Segoe UI", 10F);
            tcDashboard.Location = new Point(0, 0);
            tcDashboard.Name = "tcDashboard";
            tcDashboard.Padding = new Point(15, 5);
            tcDashboard.SelectedIndex = 0;
            tcDashboard.Size = new Size(1350, 843);
            tcDashboard.TabIndex = 0;
            // 
            // tabOverview
            // 
            tabOverview.BackColor = Color.FromArgb(247, 249, 252);
            tabOverview.Controls.Add(spOverview);
            tabOverview.Location = new Point(4, 30);
            tabOverview.Name = "tabOverview";
            tabOverview.Size = new Size(1342, 809);
            tabOverview.TabIndex = 0;
            tabOverview.Text = "Tổng quan";
            // 
            // spOverview
            // 
            spOverview.Dock = DockStyle.Fill;
            spOverview.Location = new Point(0, 0);
            spOverview.Name = "spOverview";
            // 
            // spOverview.Panel1
            // 
            spOverview.Panel1.Controls.Add(pnlLeft);
            // 
            // spOverview.Panel2
            // 
            spOverview.Panel2.Controls.Add(pnlRight);
            spOverview.Size = new Size(1342, 809);
            spOverview.SplitterDistance = 850;
            spOverview.TabIndex = 0;
            // 
            // pnlLeft
            // 
            pnlLeft.BackColor = Color.FromArgb(248, 250, 252);
            pnlLeft.Controls.Add(lblSectionRecent);
            pnlLeft.Controls.Add(dgvRecentOrders);
            pnlLeft.Dock = DockStyle.Fill;
            pnlLeft.FlowDirection = FlowDirection.TopDown;
            pnlLeft.Location = new Point(0, 0);
            pnlLeft.Name = "pnlLeft";
            pnlLeft.Padding = new Padding(10);
            pnlLeft.Size = new Size(850, 809);
            pnlLeft.TabIndex = 0;
            pnlLeft.WrapContents = false;
            // 
            // lblSectionRecent
            // 
            lblSectionRecent.AutoSize = true;
            lblSectionRecent.BackColor = Color.Transparent;
            lblSectionRecent.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblSectionRecent.ForeColor = Color.FromArgb(59, 130, 246);
            lblSectionRecent.Location = new Point(13, 10);
            lblSectionRecent.Name = "lblSectionRecent";
            lblSectionRecent.Padding = new Padding(12, 8, 5, 8);
            lblSectionRecent.Size = new Size(194, 37);
            lblSectionRecent.TabIndex = 0;
            lblSectionRecent.Text = "📋 Đơn hàng gần đây";
            // 
            // dgvRecentOrders
            // 
            dgvRecentOrders.AllowUserToAddRows = false;
            dgvRecentOrders.AllowUserToDeleteRows = false;
            dgvRecentOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRecentOrders.BackgroundColor = Color.White;
            dgvRecentOrders.BorderStyle = BorderStyle.None;
            dgvRecentOrders.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvRecentOrders.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(59, 130, 246);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.Padding = new Padding(5, 8, 5, 8);
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(59, 130, 246);
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dgvRecentOrders.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvRecentOrders.ColumnHeadersHeight = 40;
            dgvRecentOrders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvRecentOrders.EnableHeadersVisualStyles = false;
            dgvRecentOrders.GridColor = Color.FromArgb(241, 245, 249);
            dgvRecentOrders.Location = new Point(13, 50);
            dgvRecentOrders.MultiSelect = false;
            dgvRecentOrders.Name = "dgvRecentOrders";
            dgvRecentOrders.ReadOnly = true;
            dgvRecentOrders.RowHeadersVisible = false;
            dgvRecentOrders.RowHeadersWidth = 5;
            dgvRecentOrders.RowTemplate.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvRecentOrders.RowTemplate.DefaultCellStyle.Padding = new Padding(5, 6, 5, 6);
            dgvRecentOrders.RowTemplate.Height = 35;
            dgvRecentOrders.RowTemplate.ReadOnly = true;
            dgvRecentOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRecentOrders.Size = new Size(827, 600);
            dgvRecentOrders.TabIndex = 1;
            // 
            // pnlRight
            // 
            pnlRight.BackColor = Color.FromArgb(248, 250, 252);
            pnlRight.Controls.Add(lblSectionTopProducts);
            pnlRight.Controls.Add(dgvTopProducts);
            pnlRight.Controls.Add(lblSectionPayments);
            pnlRight.Controls.Add(dgvPaymentBreakdown);
            pnlRight.Dock = DockStyle.Fill;
            pnlRight.FlowDirection = FlowDirection.TopDown;
            pnlRight.Location = new Point(0, 0);
            pnlRight.Name = "pnlRight";
            pnlRight.Padding = new Padding(10);
            pnlRight.Size = new Size(488, 809);
            pnlRight.TabIndex = 0;
            pnlRight.WrapContents = false;
            // 
            // lblSectionTopProducts
            // 
            lblSectionTopProducts.AutoSize = true;
            lblSectionTopProducts.BackColor = Color.Transparent;
            lblSectionTopProducts.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblSectionTopProducts.ForeColor = Color.FromArgb(245, 158, 11);
            lblSectionTopProducts.Location = new Point(13, 10);
            lblSectionTopProducts.Name = "lblSectionTopProducts";
            lblSectionTopProducts.Padding = new Padding(12, 8, 5, 8);
            lblSectionTopProducts.Size = new Size(233, 37);
            lblSectionTopProducts.TabIndex = 2;
            lblSectionTopProducts.Text = "🏆 Top sản phẩm bán chạy";
            // 
            // dgvTopProducts
            // 
            dgvTopProducts.AllowUserToAddRows = false;
            dgvTopProducts.AllowUserToDeleteRows = false;
            dgvTopProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTopProducts.BackgroundColor = Color.White;
            dgvTopProducts.BorderStyle = BorderStyle.None;
            dgvTopProducts.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvTopProducts.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(245, 158, 11);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.Padding = new Padding(5, 8, 5, 8);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(245, 158, 11);
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dgvTopProducts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvTopProducts.ColumnHeadersHeight = 40;
            dgvTopProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvTopProducts.EnableHeadersVisualStyles = false;
            dgvTopProducts.GridColor = Color.FromArgb(241, 245, 249);
            dgvTopProducts.Location = new Point(13, 50);
            dgvTopProducts.MultiSelect = false;
            dgvTopProducts.Name = "dgvTopProducts";
            dgvTopProducts.ReadOnly = true;
            dgvTopProducts.RowHeadersVisible = false;
            dgvTopProducts.RowHeadersWidth = 5;
            dgvTopProducts.RowTemplate.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvTopProducts.RowTemplate.DefaultCellStyle.Padding = new Padding(5, 6, 5, 6);
            dgvTopProducts.RowTemplate.Height = 35;
            dgvTopProducts.RowTemplate.ReadOnly = true;
            dgvTopProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTopProducts.Size = new Size(459, 250);
            dgvTopProducts.TabIndex = 3;
            // 
            // lblSectionPayments
            // 
            lblSectionPayments.AutoSize = true;
            lblSectionPayments.BackColor = Color.Transparent;
            lblSectionPayments.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblSectionPayments.ForeColor = Color.FromArgb(16, 185, 129);
            lblSectionPayments.Location = new Point(13, 303);
            lblSectionPayments.Name = "lblSectionPayments";
            lblSectionPayments.Padding = new Padding(12, 8, 5, 8);
            lblSectionPayments.Size = new Size(212, 37);
            lblSectionPayments.TabIndex = 0;
            lblSectionPayments.Text = "💳 Thanh toán hôm nay";
            // 
            // dgvPaymentBreakdown
            // 
            dgvPaymentBreakdown.AllowUserToAddRows = false;
            dgvPaymentBreakdown.AllowUserToDeleteRows = false;
            dgvPaymentBreakdown.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPaymentBreakdown.BackgroundColor = Color.White;
            dgvPaymentBreakdown.BorderStyle = BorderStyle.None;
            dgvPaymentBreakdown.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvPaymentBreakdown.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(16, 185, 129);
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = Color.White;
            dataGridViewCellStyle3.Padding = new Padding(5, 8, 5, 8);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(16, 185, 129);
            dataGridViewCellStyle3.SelectionForeColor = Color.White;
            dgvPaymentBreakdown.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgvPaymentBreakdown.ColumnHeadersHeight = 40;
            dgvPaymentBreakdown.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvPaymentBreakdown.EnableHeadersVisualStyles = false;
            dgvPaymentBreakdown.GridColor = Color.FromArgb(241, 245, 249);
            dgvPaymentBreakdown.Location = new Point(13, 343);
            dgvPaymentBreakdown.MultiSelect = false;
            dgvPaymentBreakdown.Name = "dgvPaymentBreakdown";
            dgvPaymentBreakdown.ReadOnly = true;
            dgvPaymentBreakdown.RowHeadersVisible = false;
            dgvPaymentBreakdown.RowHeadersWidth = 5;
            dgvPaymentBreakdown.RowTemplate.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvPaymentBreakdown.RowTemplate.DefaultCellStyle.Padding = new Padding(5, 6, 5, 6);
            dgvPaymentBreakdown.RowTemplate.Height = 35;
            dgvPaymentBreakdown.RowTemplate.ReadOnly = true;
            dgvPaymentBreakdown.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPaymentBreakdown.Size = new Size(459, 200);
            dgvPaymentBreakdown.TabIndex = 1;
            // 
            // tabCharts
            // 
            tabCharts.BackColor = Color.FromArgb(247, 249, 252);
            tabCharts.Controls.Add(lblChartInfo);
            tabCharts.Controls.Add(lblHourlyInfo);
            tabCharts.Location = new Point(4, 30);
            tabCharts.Name = "tabCharts";
            tabCharts.Padding = new Padding(20);
            tabCharts.Size = new Size(1342, 822);
            tabCharts.TabIndex = 1;
            tabCharts.Text = "Biểu đồ";
            // 
            // lblChartInfo
            // 
            lblChartInfo.AutoSize = true;
            lblChartInfo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblChartInfo.ForeColor = Color.FromArgb(45, 55, 72);
            lblChartInfo.Location = new Point(30, 30);
            lblChartInfo.Name = "lblChartInfo";
            lblChartInfo.Size = new Size(293, 25);
            lblChartInfo.TabIndex = 0;
            lblChartInfo.Text = "📈 Đang tải dữ liệu doanh thu...";
            // 
            // lblHourlyInfo
            // 
            lblHourlyInfo.AutoSize = true;
            lblHourlyInfo.Font = new Font("Segoe UI", 11F);
            lblHourlyInfo.ForeColor = Color.FromArgb(108, 117, 125);
            lblHourlyInfo.Location = new Point(30, 70);
            lblHourlyInfo.Name = "lblHourlyInfo";
            lblHourlyInfo.Size = new Size(242, 20);
            lblHourlyInfo.TabIndex = 1;
            lblHourlyInfo.Text = "⏰ Đang tải dữ liệu giờ cao điểm...";
            // 
            // tabTables
            // 
            tabTables.BackColor = Color.FromArgb(247, 249, 252);
            tabTables.Controls.Add(dgvTables);
            tabTables.Controls.Add(lblSectionTables);
            tabTables.Location = new Point(4, 30);
            tabTables.Name = "tabTables";
            tabTables.Padding = new Padding(20);
            tabTables.Size = new Size(1342, 822);
            tabTables.TabIndex = 2;
            tabTables.Text = "Bàn ăn";
            // 
            // dgvTables
            // 
            dgvTables.AllowUserToAddRows = false;
            dgvTables.AllowUserToDeleteRows = false;
            dgvTables.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTables.BackgroundColor = Color.White;
            dgvTables.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvTables.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dgvTables.ColumnHeadersHeight = 35;
            dgvTables.Dock = DockStyle.Fill;
            dgvTables.EnableHeadersVisualStyles = false;
            dgvTables.GridColor = Color.FromArgb(226, 232, 240);
            dgvTables.Location = new Point(20, 20);
            dgvTables.MultiSelect = false;
            dgvTables.Name = "dgvTables";
            dgvTables.ReadOnly = true;
            dgvTables.RowHeadersVisible = false;
            dgvTables.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTables.Size = new Size(1302, 782);
            dgvTables.TabIndex = 0;
            // 
            // lblSectionTables
            // 
            lblSectionTables.AutoSize = true;
            lblSectionTables.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblSectionTables.ForeColor = Color.FromArgb(45, 55, 72);
            lblSectionTables.Location = new Point(0, 0);
            lblSectionTables.Name = "lblSectionTables";
            lblSectionTables.Padding = new Padding(5, 5, 5, 10);
            lblSectionTables.Size = new Size(348, 40);
            lblSectionTables.TabIndex = 1;
            lblSectionTables.Text = "\U0001fa91 Tình trạng bàn theo thời gian thực";
            // 
            // tabStats
            // 
            tabStats.BackColor = Color.FromArgb(247, 249, 252);
            tabStats.Controls.Add(lblStatsInfo);
            tabStats.Location = new Point(4, 30);
            tabStats.Name = "tabStats";
            tabStats.Padding = new Padding(20);
            tabStats.Size = new Size(1342, 822);
            tabStats.TabIndex = 3;
            tabStats.Text = "Thống kê";
            // 
            // lblStatsInfo
            // 
            lblStatsInfo.Location = new Point(0, 0);
            lblStatsInfo.Name = "lblStatsInfo";
            lblStatsInfo.Size = new Size(100, 23);
            lblStatsInfo.TabIndex = 0;
            // 
            // frmDashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 242, 245);
            ClientSize = new Size(1350, 1061);
            Controls.Add(pnlContent);
            Controls.Add(pnlCards);
            Controls.Add(pnlHeader);
            MinimumSize = new Size(1200, 700);
            Name = "frmDashboard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "📊 Dashboard - MilkTea POS";
            WindowState = FormWindowState.Maximized;
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlCards.ResumeLayout(false);
            pnlCard1.ResumeLayout(false);
            pnlCard1.PerformLayout();
            pnlCard2.ResumeLayout(false);
            pnlCard2.PerformLayout();
            pnlCard3.ResumeLayout(false);
            pnlCard3.PerformLayout();
            pnlCard4.ResumeLayout(false);
            pnlCard4.PerformLayout();
            pnlCard5.ResumeLayout(false);
            pnlCard5.PerformLayout();
            pnlContent.ResumeLayout(false);
            tcDashboard.ResumeLayout(false);
            tabOverview.ResumeLayout(false);
            spOverview.Panel1.ResumeLayout(false);
            spOverview.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)spOverview).EndInit();
            spOverview.ResumeLayout(false);
            pnlLeft.ResumeLayout(false);
            pnlLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRecentOrders).EndInit();
            pnlRight.ResumeLayout(false);
            pnlRight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTopProducts).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvPaymentBreakdown).EndInit();
            tabCharts.ResumeLayout(false);
            tabCharts.PerformLayout();
            tabTables.ResumeLayout(false);
            tabTables.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTables).EndInit();
            tabStats.ResumeLayout(false);
            ResumeLayout(false);
        }

        private void InitializeCustomFonts()
        {
            // Initialize cached fonts
            _fontCardValue = new Font("Segoe UI", 20F, FontStyle.Bold);
            _fontCardEmoji = new Font("Segoe UI", 18F);
            _fontCardTrend = new Font("Segoe UI", 9F, FontStyle.Italic);
            _fontSectionTitle = new Font("Segoe UI", 13F, FontStyle.Bold);
            _fontRow = new Font("Segoe UI", 10F);
            _fontRowBold = new Font("Segoe UI", 10F, FontStyle.Bold);
            _fontClock = new Font("Segoe UI", 16F, FontStyle.Bold);

            // Apply fonts to KPI cards
            lblCard1Value.Font = _fontCardValue;
            lblCard1Trend.Font = _fontCardTrend;
            lblCard2Value.Font = _fontCardValue;
            lblCard2Trend.Font = _fontCardTrend;
            lblCard3Value.Font = _fontCardValue;
            lblCard3Trend.Font = _fontCardTrend;
            lblCard4Value.Font = _fontCardValue;
            lblCard4Trend.Font = _fontCardTrend;
            lblCard5Value.Font = _fontCardValue;
            lblCard5Trend.Font = _fontCardTrend;

            // Apply fonts to header
            lblClock.Font = _fontClock;

            // Apply fonts to section labels
            lblSectionRecent.Font = _fontSectionTitle;
            lblSectionTopProducts.Font = _fontSectionTitle;
            lblSectionPayments.Font = _fontSectionTitle;
            lblSectionTables.Font = _fontSectionTitle;

            // Apply fonts to chart labels
            lblChartInfo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblHourlyInfo.Font = new Font("Segoe UI", 11F);
            lblStatsInfo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);

            // Apply fonts to DataGridViews
            dgvRecentOrders.Font = _fontRow;
            dgvTopProducts.Font = _fontRow;
            dgvPaymentBreakdown.Font = _fontRow;
            dgvTables.Font = _fontRow;
        }

        private Label lblStatsInfo;
    }
}
