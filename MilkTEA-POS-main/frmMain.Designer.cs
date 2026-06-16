namespace MilkTeaPOS
{
    partial class frmMain
    {
        private System.ComponentModel.IContainer components = null;

        // Panels
        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Panel pnlTopBar;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel pnlWelcome;

        // Sidebar Controls
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Button btnCategories;
        private System.Windows.Forms.Button btnProducts;
        private System.Windows.Forms.Button btnToppings;
        private System.Windows.Forms.Button btnTables;
        private System.Windows.Forms.Button btnPOS;
        private System.Windows.Forms.Button btnOrderHistory;
        private System.Windows.Forms.Button btnCustomers;
        private System.Windows.Forms.Button btnMemberships;
        private System.Windows.Forms.Button btnVouchers;
        private System.Windows.Forms.Button btnReports;
        private System.Windows.Forms.Button btnUsers;
        private System.Windows.Forms.Button btnAuditLog;
        private System.Windows.Forms.Button btnChangePassword;
        private System.Windows.Forms.Button btnLogout;

        // Top Bar Controls
        private System.Windows.Forms.Label lblUserInfo;
        private System.Windows.Forms.Label lblClock;

        // Content Controls
        private System.Windows.Forms.Label lblWelcomeTitle;
        private System.Windows.Forms.Label lblWelcomeSubtitle;
        private System.Windows.Forms.FlowLayoutPanel pnlStatsContainer;
        private System.Windows.Forms.Panel pnlStats1;
        private System.Windows.Forms.Label lblStats1Icon;
        private System.Windows.Forms.Label lblStats1Label;
        private System.Windows.Forms.Label lblStats1Value;
        private System.Windows.Forms.Panel pnlStats2;
        private System.Windows.Forms.Label lblStats2Icon;
        private System.Windows.Forms.Label lblStats2Label;
        private System.Windows.Forms.Label lblStats2Value;
        private System.Windows.Forms.Panel pnlStats3;
        private System.Windows.Forms.Label lblStats3Icon;
        private System.Windows.Forms.Label lblStats3Label;
        private System.Windows.Forms.Label lblStats3Value;
        private System.Windows.Forms.Panel pnlStats4;
        private System.Windows.Forms.Label lblStats4Icon;
        private System.Windows.Forms.Label lblStats4Label;
        private System.Windows.Forms.Label lblStats4Value;

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
            pnlSidebar = new Panel();
            btnLogout = new Button();
            btnChangePassword = new Button();
            btnAuditLog = new Button();
            btnUsers = new Button();
            btnReports = new Button();
            btnVouchers = new Button();
            btnMemberships = new Button();
            btnCustomers = new Button();
            btnOrderHistory = new Button();
            btnPOS = new Button();
            btnTables = new Button();
            btnToppings = new Button();
            btnProducts = new Button();
            btnCategories = new Button();
            btnDashboard = new Button();
            lblLogo = new Label();
            pnlTopBar = new Panel();
            lblUserInfo = new Label();
            lblClock = new Label();
            pnlContent = new Panel();
            pnlWelcome = new Panel();
            lblWelcomeTitle = new Label();
            lblWelcomeSubtitle = new Label();
            pnlStatsContainer = new FlowLayoutPanel();
            pnlStats1 = new Panel();
            lblStats1Icon = new Label();
            lblStats1Label = new Label();
            lblStats1Value = new Label();
            pnlStats2 = new Panel();
            lblStats2Icon = new Label();
            lblStats2Label = new Label();
            lblStats2Value = new Label();
            pnlStats3 = new Panel();
            lblStats3Icon = new Label();
            lblStats3Label = new Label();
            lblStats3Value = new Label();
            pnlStats4 = new Panel();
            lblStats4Icon = new Label();
            lblStats4Label = new Label();
            lblStats4Value = new Label();
            pnlSidebar.SuspendLayout();
            pnlTopBar.SuspendLayout();
            pnlContent.SuspendLayout();
            pnlWelcome.SuspendLayout();
            pnlStatsContainer.SuspendLayout();
            pnlStats1.SuspendLayout();
            pnlStats2.SuspendLayout();
            pnlStats3.SuspendLayout();
            pnlStats4.SuspendLayout();
            SuspendLayout();
            // 
            // pnlSidebar
            // 
            pnlSidebar.BackColor = Color.FromArgb(20, 27, 36);
            pnlSidebar.Controls.Add(btnLogout);
            pnlSidebar.Controls.Add(btnChangePassword);
            pnlSidebar.Controls.Add(btnAuditLog);
            pnlSidebar.Controls.Add(btnUsers);
            pnlSidebar.Controls.Add(btnReports);
            pnlSidebar.Controls.Add(btnVouchers);
            pnlSidebar.Controls.Add(btnMemberships);
            pnlSidebar.Controls.Add(btnCustomers);
            pnlSidebar.Controls.Add(btnOrderHistory);
            pnlSidebar.Controls.Add(btnPOS);
            pnlSidebar.Controls.Add(btnTables);
            pnlSidebar.Controls.Add(btnToppings);
            pnlSidebar.Controls.Add(btnProducts);
            pnlSidebar.Controls.Add(btnCategories);
            pnlSidebar.Controls.Add(btnDashboard);
            pnlSidebar.Controls.Add(lblLogo);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Location = new Point(0, 0);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(280, 900);
            pnlSidebar.TabIndex = 2;
            // 
            // btnLogout
            // 
            btnLogout.BackColor = Color.FromArgb(220, 53, 69);
            btnLogout.Cursor = Cursors.Hand;
            btnLogout.Dock = DockStyle.Bottom;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatAppearance.MouseDownBackColor = Color.FromArgb(150, 30, 45);
            btnLogout.FlatAppearance.MouseOverBackColor = Color.FromArgb(180, 40, 55);
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            btnLogout.ForeColor = Color.White;
            btnLogout.Location = new Point(0, 804);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(280, 48);
            btnLogout.TabIndex = 0;
            btnLogout.Tag = "Logout";
            btnLogout.Text = "🚪  Đăng xuất";
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Click += btnMenu_Click;
            // 
            // btnChangePassword
            // 
            btnChangePassword.BackColor = Color.FromArgb(108, 117, 125);
            btnChangePassword.Cursor = Cursors.Hand;
            btnChangePassword.Dock = DockStyle.Bottom;
            btnChangePassword.FlatAppearance.BorderSize = 0;
            btnChangePassword.FlatAppearance.MouseDownBackColor = Color.FromArgb(80, 90, 100);
            btnChangePassword.FlatAppearance.MouseOverBackColor = Color.FromArgb(120, 130, 140);
            btnChangePassword.FlatStyle = FlatStyle.Flat;
            btnChangePassword.Font = new Font("Segoe UI", 11.5F);
            btnChangePassword.ForeColor = Color.White;
            btnChangePassword.Location = new Point(0, 852);
            btnChangePassword.Name = "btnChangePassword";
            btnChangePassword.Size = new Size(280, 48);
            btnChangePassword.TabIndex = 13;
            btnChangePassword.Text = "🔐  Đổi mật khẩu";
            btnChangePassword.UseVisualStyleBackColor = false;
            btnChangePassword.Click += btnChangePassword_Click;
            // 
            // btnAuditLog
            // 
            btnAuditLog.BackColor = Color.FromArgb(30, 40, 50);
            btnAuditLog.Cursor = Cursors.Hand;
            btnAuditLog.Dock = DockStyle.Top;
            btnAuditLog.FlatAppearance.BorderSize = 0;
            btnAuditLog.FlatAppearance.MouseDownBackColor = Color.FromArgb(52, 152, 219);
            btnAuditLog.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 55, 75);
            btnAuditLog.FlatStyle = FlatStyle.Flat;
            btnAuditLog.Font = new Font("Segoe UI", 11.5F);
            btnAuditLog.ForeColor = Color.FromArgb(165, 175, 185);
            btnAuditLog.Location = new Point(0, 716);
            btnAuditLog.Name = "btnAuditLog";
            btnAuditLog.Size = new Size(280, 48);
            btnAuditLog.TabIndex = 1;
            btnAuditLog.Tag = "frmAuditLog";
            btnAuditLog.Text = "🔒  Audit Log";
            btnAuditLog.UseVisualStyleBackColor = false;
            btnAuditLog.Click += btnMenu_Click;
            // 
            // btnUsers
            // 
            btnUsers.BackColor = Color.FromArgb(30, 40, 50);
            btnUsers.Cursor = Cursors.Hand;
            btnUsers.Dock = DockStyle.Top;
            btnUsers.FlatAppearance.BorderSize = 0;
            btnUsers.FlatAppearance.MouseDownBackColor = Color.FromArgb(52, 152, 219);
            btnUsers.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 55, 75);
            btnUsers.FlatStyle = FlatStyle.Flat;
            btnUsers.Font = new Font("Segoe UI", 11.5F);
            btnUsers.ForeColor = Color.FromArgb(165, 175, 185);
            btnUsers.Location = new Point(0, 668);
            btnUsers.Name = "btnUsers";
            btnUsers.Size = new Size(280, 48);
            btnUsers.TabIndex = 2;
            btnUsers.Tag = "frmUsers";
            btnUsers.Text = "👨‍💼  Nhân viên";
            btnUsers.UseVisualStyleBackColor = false;
            btnUsers.Click += btnMenu_Click;
            // 
            // btnReports
            // 
            btnReports.BackColor = Color.FromArgb(30, 40, 50);
            btnReports.Cursor = Cursors.Hand;
            btnReports.Dock = DockStyle.Top;
            btnReports.FlatAppearance.BorderSize = 0;
            btnReports.FlatAppearance.MouseDownBackColor = Color.FromArgb(52, 152, 219);
            btnReports.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 55, 75);
            btnReports.FlatStyle = FlatStyle.Flat;
            btnReports.Font = new Font("Segoe UI", 11.5F);
            btnReports.ForeColor = Color.FromArgb(165, 175, 185);
            btnReports.Location = new Point(0, 620);
            btnReports.Name = "btnReports";
            btnReports.Size = new Size(280, 48);
            btnReports.TabIndex = 3;
            btnReports.Tag = "frmSalesReport";
            btnReports.Text = "📈  Báo cáo";
            btnReports.UseVisualStyleBackColor = false;
            btnReports.Click += btnMenu_Click;
            // 
            // btnVouchers
            // 
            btnVouchers.BackColor = Color.FromArgb(30, 40, 50);
            btnVouchers.Cursor = Cursors.Hand;
            btnVouchers.Dock = DockStyle.Top;
            btnVouchers.FlatAppearance.BorderSize = 0;
            btnVouchers.FlatAppearance.MouseDownBackColor = Color.FromArgb(52, 152, 219);
            btnVouchers.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 55, 75);
            btnVouchers.FlatStyle = FlatStyle.Flat;
            btnVouchers.Font = new Font("Segoe UI", 11.5F);
            btnVouchers.ForeColor = Color.FromArgb(165, 175, 185);
            btnVouchers.Location = new Point(0, 572);
            btnVouchers.Name = "btnVouchers";
            btnVouchers.Size = new Size(280, 48);
            btnVouchers.TabIndex = 4;
            btnVouchers.Tag = "frmVouchers";
            btnVouchers.Text = "🎫  Vouchers";
            btnVouchers.UseVisualStyleBackColor = false;
            btnVouchers.Click += btnMenu_Click;
            // 
            // btnMemberships
            // 
            btnMemberships.BackColor = Color.FromArgb(30, 40, 50);
            btnMemberships.Cursor = Cursors.Hand;
            btnMemberships.Dock = DockStyle.Top;
            btnMemberships.FlatAppearance.BorderSize = 0;
            btnMemberships.FlatAppearance.MouseDownBackColor = Color.FromArgb(52, 152, 219);
            btnMemberships.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 55, 75);
            btnMemberships.FlatStyle = FlatStyle.Flat;
            btnMemberships.Font = new Font("Segoe UI", 11.5F);
            btnMemberships.ForeColor = Color.FromArgb(165, 175, 185);
            btnMemberships.Location = new Point(0, 524);
            btnMemberships.Name = "btnMemberships";
            btnMemberships.Size = new Size(280, 48);
            btnMemberships.TabIndex = 5;
            btnMemberships.Tag = "frmMemberships";
            btnMemberships.Text = "💳  Membership";
            btnMemberships.UseVisualStyleBackColor = false;
            btnMemberships.Click += btnMenu_Click;
            // 
            // btnCustomers
            // 
            btnCustomers.BackColor = Color.FromArgb(30, 40, 50);
            btnCustomers.Cursor = Cursors.Hand;
            btnCustomers.Dock = DockStyle.Top;
            btnCustomers.FlatAppearance.BorderSize = 0;
            btnCustomers.FlatAppearance.MouseDownBackColor = Color.FromArgb(52, 152, 219);
            btnCustomers.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 55, 75);
            btnCustomers.FlatStyle = FlatStyle.Flat;
            btnCustomers.Font = new Font("Segoe UI", 11.5F);
            btnCustomers.ForeColor = Color.FromArgb(165, 175, 185);
            btnCustomers.Location = new Point(0, 476);
            btnCustomers.Name = "btnCustomers";
            btnCustomers.Size = new Size(280, 48);
            btnCustomers.TabIndex = 6;
            btnCustomers.Tag = "frmCustomers";
            btnCustomers.Text = "👥  Khách hàng";
            btnCustomers.UseVisualStyleBackColor = false;
            btnCustomers.Click += btnMenu_Click;
            // 
            // btnOrderHistory
            // 
            btnOrderHistory.BackColor = Color.FromArgb(30, 40, 50);
            btnOrderHistory.Cursor = Cursors.Hand;
            btnOrderHistory.Dock = DockStyle.Top;
            btnOrderHistory.FlatAppearance.BorderSize = 0;
            btnOrderHistory.FlatAppearance.MouseDownBackColor = Color.FromArgb(52, 152, 219);
            btnOrderHistory.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 55, 75);
            btnOrderHistory.FlatStyle = FlatStyle.Flat;
            btnOrderHistory.Font = new Font("Segoe UI", 11.5F);
            btnOrderHistory.ForeColor = Color.FromArgb(165, 175, 185);
            btnOrderHistory.Location = new Point(0, 428);
            btnOrderHistory.Name = "btnOrderHistory";
            btnOrderHistory.Size = new Size(280, 48);
            btnOrderHistory.TabIndex = 7;
            btnOrderHistory.Tag = "frmOrderHistory";
            btnOrderHistory.Text = "📜  Lịch sử đơn";
            btnOrderHistory.UseVisualStyleBackColor = false;
            btnOrderHistory.Click += btnMenu_Click;
            // 
            // btnPOS
            // 
            btnPOS.BackColor = Color.FromArgb(30, 40, 50);
            btnPOS.Cursor = Cursors.Hand;
            btnPOS.Dock = DockStyle.Top;
            btnPOS.FlatAppearance.BorderSize = 0;
            btnPOS.FlatAppearance.MouseDownBackColor = Color.FromArgb(52, 152, 219);
            btnPOS.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 55, 75);
            btnPOS.FlatStyle = FlatStyle.Flat;
            btnPOS.Font = new Font("Segoe UI", 11.5F);
            btnPOS.ForeColor = Color.FromArgb(165, 175, 185);
            btnPOS.Location = new Point(0, 380);
            btnPOS.Name = "btnPOS";
            btnPOS.Size = new Size(280, 48);
            btnPOS.TabIndex = 8;
            btnPOS.Tag = "frmOrders";
            btnPOS.Text = "\U0001f6d2  Order (POS)";
            btnPOS.UseVisualStyleBackColor = false;
            btnPOS.Click += btnMenu_Click;
            // 
            // btnTables
            // 
            btnTables.BackColor = Color.FromArgb(30, 40, 50);
            btnTables.Cursor = Cursors.Hand;
            btnTables.Dock = DockStyle.Top;
            btnTables.FlatAppearance.BorderSize = 0;
            btnTables.FlatAppearance.MouseDownBackColor = Color.FromArgb(52, 152, 219);
            btnTables.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 55, 75);
            btnTables.FlatStyle = FlatStyle.Flat;
            btnTables.Font = new Font("Segoe UI", 11.5F);
            btnTables.ForeColor = Color.FromArgb(165, 175, 185);
            btnTables.Location = new Point(0, 332);
            btnTables.Name = "btnTables";
            btnTables.Size = new Size(280, 48);
            btnTables.TabIndex = 9;
            btnTables.Tag = "frmTables";
            btnTables.Text = "\U0001fa91  Bàn";
            btnTables.UseVisualStyleBackColor = false;
            btnTables.Click += btnMenu_Click;
            // 
            // btnToppings
            // 
            btnToppings.BackColor = Color.FromArgb(30, 40, 50);
            btnToppings.Cursor = Cursors.Hand;
            btnToppings.Dock = DockStyle.Top;
            btnToppings.FlatAppearance.BorderSize = 0;
            btnToppings.FlatAppearance.MouseDownBackColor = Color.FromArgb(52, 152, 219);
            btnToppings.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 55, 75);
            btnToppings.FlatStyle = FlatStyle.Flat;
            btnToppings.Font = new Font("Segoe UI", 11.5F);
            btnToppings.ForeColor = Color.FromArgb(165, 175, 185);
            btnToppings.Location = new Point(0, 284);
            btnToppings.Name = "btnToppings";
            btnToppings.Size = new Size(280, 48);
            btnToppings.TabIndex = 10;
            btnToppings.Tag = "frmToppings";
            btnToppings.Text = "🍮  Toppings";
            btnToppings.UseVisualStyleBackColor = false;
            btnToppings.Click += btnMenu_Click;
            // 
            // btnProducts
            // 
            btnProducts.BackColor = Color.FromArgb(30, 40, 50);
            btnProducts.Cursor = Cursors.Hand;
            btnProducts.Dock = DockStyle.Top;
            btnProducts.FlatAppearance.BorderSize = 0;
            btnProducts.FlatAppearance.MouseDownBackColor = Color.FromArgb(52, 152, 219);
            btnProducts.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 55, 75);
            btnProducts.FlatStyle = FlatStyle.Flat;
            btnProducts.Font = new Font("Segoe UI", 11.5F);
            btnProducts.ForeColor = Color.FromArgb(165, 175, 185);
            btnProducts.Location = new Point(0, 236);
            btnProducts.Name = "btnProducts";
            btnProducts.Size = new Size(280, 48);
            btnProducts.TabIndex = 11;
            btnProducts.Tag = "frmProducts";
            btnProducts.Text = "\U0001f964  Sản phẩm";
            btnProducts.UseVisualStyleBackColor = false;
            btnProducts.Click += btnMenu_Click;
            // 
            // btnCategories
            // 
            btnCategories.BackColor = Color.FromArgb(30, 40, 50);
            btnCategories.Cursor = Cursors.Hand;
            btnCategories.Dock = DockStyle.Top;
            btnCategories.FlatAppearance.BorderSize = 0;
            btnCategories.FlatAppearance.MouseDownBackColor = Color.FromArgb(52, 152, 219);
            btnCategories.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 55, 75);
            btnCategories.FlatStyle = FlatStyle.Flat;
            btnCategories.Font = new Font("Segoe UI", 11.5F);
            btnCategories.ForeColor = Color.FromArgb(165, 175, 185);
            btnCategories.Location = new Point(0, 188);
            btnCategories.Name = "btnCategories";
            btnCategories.Size = new Size(280, 48);
            btnCategories.TabIndex = 12;
            btnCategories.Tag = "frmCategories";
            btnCategories.Text = "\U0001f9cb  Danh mục";
            btnCategories.UseVisualStyleBackColor = false;
            btnCategories.Click += btnMenu_Click;
            // 
            // btnDashboard
            // 
            btnDashboard.BackColor = Color.FromArgb(30, 40, 50);
            btnDashboard.Cursor = Cursors.Hand;
            btnDashboard.Dock = DockStyle.Top;
            btnDashboard.FlatAppearance.BorderSize = 0;
            btnDashboard.FlatAppearance.MouseDownBackColor = Color.FromArgb(52, 152, 219);
            btnDashboard.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 55, 75);
            btnDashboard.FlatStyle = FlatStyle.Flat;
            btnDashboard.Font = new Font("Segoe UI", 11.5F);
            btnDashboard.ForeColor = Color.FromArgb(165, 175, 185);
            btnDashboard.Location = new Point(0, 140);
            btnDashboard.Name = "btnDashboard";
            btnDashboard.Size = new Size(280, 48);
            btnDashboard.TabIndex = 13;
            btnDashboard.Tag = "frmDashboard";
            btnDashboard.Text = "📊  Dashboard";
            btnDashboard.UseVisualStyleBackColor = false;
            btnDashboard.Click += btnMenu_Click;
            // 
            // lblLogo
            // 
            lblLogo.BackColor = Color.FromArgb(255, 107, 107);
            lblLogo.Dock = DockStyle.Top;
            lblLogo.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblLogo.ForeColor = Color.White;
            lblLogo.Location = new Point(0, 0);
            lblLogo.Name = "lblLogo";
            lblLogo.Size = new Size(280, 140);
            lblLogo.TabIndex = 14;
            lblLogo.Text = "\U0001f9cb MilkTea\nPOS";
            lblLogo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlTopBar
            // 
            pnlTopBar.BackColor = Color.White;
            pnlTopBar.Controls.Add(lblUserInfo);
            pnlTopBar.Controls.Add(lblClock);
            pnlTopBar.Dock = DockStyle.Top;
            pnlTopBar.Location = new Point(280, 0);
            pnlTopBar.Name = "pnlTopBar";
            pnlTopBar.Size = new Size(1120, 80);
            pnlTopBar.TabIndex = 1;
            // 
            // lblUserInfo
            // 
            lblUserInfo.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblUserInfo.ForeColor = Color.FromArgb(52, 58, 64);
            lblUserInfo.Location = new Point(30, 25);
            lblUserInfo.Name = "lblUserInfo";
            lblUserInfo.Size = new Size(320, 30);
            lblUserInfo.TabIndex = 0;
            lblUserInfo.Text = "👤 admin | Quản trị viên";
            lblUserInfo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblClock
            // 
            lblClock.Font = new Font("Segoe UI", 12.5F);
            lblClock.ForeColor = Color.FromArgb(127, 140, 141);
            lblClock.Location = new Point(770, 25);
            lblClock.Name = "lblClock";
            lblClock.Size = new Size(320, 30);
            lblClock.TabIndex = 1;
            lblClock.Text = "🕐 Thứ Hai, 01/04/2026 • 14:30";
            lblClock.TextAlign = ContentAlignment.MiddleRight;
            // 
            // pnlContent
            // 
            pnlContent.BackColor = Color.FromArgb(240, 242, 245);
            pnlContent.Controls.Add(pnlWelcome);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(280, 80);
            pnlContent.Name = "pnlContent";
            pnlContent.Padding = new Padding(20);
            pnlContent.Size = new Size(1120, 820);
            pnlContent.TabIndex = 0;
            // 
            // pnlWelcome
            // 
            pnlWelcome.BackColor = Color.White;
            pnlWelcome.Controls.Add(lblWelcomeTitle);
            pnlWelcome.Controls.Add(lblWelcomeSubtitle);
            pnlWelcome.Controls.Add(pnlStatsContainer);
            pnlWelcome.Location = new Point(20, 20);
            pnlWelcome.Name = "pnlWelcome";
            pnlWelcome.Size = new Size(1000, 389);
            pnlWelcome.TabIndex = 0;
            // 
            // lblWelcomeTitle
            // 
            lblWelcomeTitle.Font = new Font("Segoe UI", 30F, FontStyle.Bold);
            lblWelcomeTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblWelcomeTitle.Location = new Point(25, 25);
            lblWelcomeTitle.Name = "lblWelcomeTitle";
            lblWelcomeTitle.Size = new Size(950, 50);
            lblWelcomeTitle.TabIndex = 0;
            lblWelcomeTitle.Text = "Xin chào! 👋";
            lblWelcomeTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblWelcomeSubtitle
            // 
            lblWelcomeSubtitle.Font = new Font("Segoe UI", 13F);
            lblWelcomeSubtitle.ForeColor = Color.FromArgb(127, 140, 141);
            lblWelcomeSubtitle.Location = new Point(25, 80);
            lblWelcomeSubtitle.Name = "lblWelcomeSubtitle";
            lblWelcomeSubtitle.Size = new Size(950, 35);
            lblWelcomeSubtitle.TabIndex = 1;
            lblWelcomeSubtitle.Text = "Chúc bạn một ngày làm việc hiệu quả! Chọn chức năng ở menu bên trái để bắt đầu.";
            lblWelcomeSubtitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlStatsContainer
            // 
            pnlStatsContainer.BackColor = Color.Transparent;
            pnlStatsContainer.Controls.Add(pnlStats1);
            pnlStatsContainer.Controls.Add(pnlStats2);
            pnlStatsContainer.Controls.Add(pnlStats3);
            pnlStatsContainer.Controls.Add(pnlStats4);
            pnlStatsContainer.Location = new Point(25, 135);
            pnlStatsContainer.Name = "pnlStatsContainer";
            pnlStatsContainer.Size = new Size(972, 208);
            pnlStatsContainer.TabIndex = 2;
            // 
            // pnlStats1
            // 
            pnlStats1.BackColor = Color.FromArgb(255, 107, 107);
            pnlStats1.Controls.Add(lblStats1Icon);
            pnlStats1.Controls.Add(lblStats1Label);
            pnlStats1.Controls.Add(lblStats1Value);
            pnlStats1.Cursor = Cursors.Hand;
            pnlStats1.Location = new Point(0, 0);
            pnlStats1.Margin = new Padding(0, 0, 15, 0);
            pnlStats1.Name = "pnlStats1";
            pnlStats1.Size = new Size(225, 100);
            pnlStats1.TabIndex = 0;
            // 
            // lblStats1Icon
            // 
            lblStats1Icon.Font = new Font("Segoe UI", 30F);
            lblStats1Icon.ForeColor = Color.White;
            lblStats1Icon.Location = new Point(20, 18);
            lblStats1Icon.Name = "lblStats1Icon";
            lblStats1Icon.Size = new Size(50, 50);
            lblStats1Icon.TabIndex = 0;
            lblStats1Icon.Text = "💰";
            lblStats1Icon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblStats1Label
            // 
            lblStats1Label.Font = new Font("Segoe UI", 10F);
            lblStats1Label.ForeColor = Color.FromArgb(255, 220, 220);
            lblStats1Label.Location = new Point(75, 22);
            lblStats1Label.Name = "lblStats1Label";
            lblStats1Label.Size = new Size(135, 25);
            lblStats1Label.TabIndex = 1;
            lblStats1Label.Text = "Doanh thu";
            lblStats1Label.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblStats1Value
            // 
            lblStats1Value.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
            lblStats1Value.ForeColor = Color.White;
            lblStats1Value.Location = new Point(75, 52);
            lblStats1Value.Name = "lblStats1Value";
            lblStats1Value.Size = new Size(135, 38);
            lblStats1Value.TabIndex = 2;
            lblStats1Value.Text = "0đ";
            lblStats1Value.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlStats2
            // 
            pnlStats2.BackColor = Color.FromArgb(26, 188, 156);
            pnlStats2.Controls.Add(lblStats2Icon);
            pnlStats2.Controls.Add(lblStats2Label);
            pnlStats2.Controls.Add(lblStats2Value);
            pnlStats2.Cursor = Cursors.Hand;
            pnlStats2.Location = new Point(240, 0);
            pnlStats2.Margin = new Padding(0, 0, 15, 0);
            pnlStats2.Name = "pnlStats2";
            pnlStats2.Size = new Size(225, 100);
            pnlStats2.TabIndex = 1;
            // 
            // lblStats2Icon
            // 
            lblStats2Icon.Font = new Font("Segoe UI", 30F);
            lblStats2Icon.ForeColor = Color.White;
            lblStats2Icon.Location = new Point(20, 18);
            lblStats2Icon.Name = "lblStats2Icon";
            lblStats2Icon.Size = new Size(50, 50);
            lblStats2Icon.TabIndex = 0;
            lblStats2Icon.Text = "📦";
            lblStats2Icon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblStats2Label
            // 
            lblStats2Label.Font = new Font("Segoe UI", 10F);
            lblStats2Label.ForeColor = Color.FromArgb(220, 250, 245);
            lblStats2Label.Location = new Point(75, 22);
            lblStats2Label.Name = "lblStats2Label";
            lblStats2Label.Size = new Size(135, 25);
            lblStats2Label.TabIndex = 1;
            lblStats2Label.Text = "Đơn hàng";
            lblStats2Label.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblStats2Value
            // 
            lblStats2Value.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
            lblStats2Value.ForeColor = Color.White;
            lblStats2Value.Location = new Point(75, 52);
            lblStats2Value.Name = "lblStats2Value";
            lblStats2Value.Size = new Size(135, 38);
            lblStats2Value.TabIndex = 2;
            lblStats2Value.Text = "0";
            lblStats2Value.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlStats3
            // 
            pnlStats3.BackColor = Color.FromArgb(241, 196, 15);
            pnlStats3.Controls.Add(lblStats3Icon);
            pnlStats3.Controls.Add(lblStats3Label);
            pnlStats3.Controls.Add(lblStats3Value);
            pnlStats3.Cursor = Cursors.Hand;
            pnlStats3.Location = new Point(480, 0);
            pnlStats3.Margin = new Padding(0, 0, 15, 0);
            pnlStats3.Name = "pnlStats3";
            pnlStats3.Size = new Size(225, 100);
            pnlStats3.TabIndex = 2;
            // 
            // lblStats3Icon
            // 
            lblStats3Icon.Font = new Font("Segoe UI", 30F);
            lblStats3Icon.ForeColor = Color.White;
            lblStats3Icon.Location = new Point(20, 18);
            lblStats3Icon.Name = "lblStats3Icon";
            lblStats3Icon.Size = new Size(50, 50);
            lblStats3Icon.TabIndex = 0;
            lblStats3Icon.Text = "\U0001fa91";
            lblStats3Icon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblStats3Label
            // 
            lblStats3Label.Font = new Font("Segoe UI", 10F);
            lblStats3Label.ForeColor = Color.FromArgb(255, 250, 220);
            lblStats3Label.Location = new Point(75, 22);
            lblStats3Label.Name = "lblStats3Label";
            lblStats3Label.Size = new Size(135, 25);
            lblStats3Label.TabIndex = 1;
            lblStats3Label.Text = "Bàn đang dùng";
            lblStats3Label.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblStats3Value
            // 
            lblStats3Value.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
            lblStats3Value.ForeColor = Color.White;
            lblStats3Value.Location = new Point(75, 52);
            lblStats3Value.Name = "lblStats3Value";
            lblStats3Value.Size = new Size(135, 38);
            lblStats3Value.TabIndex = 2;
            lblStats3Value.Text = "0/20";
            lblStats3Value.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlStats4
            // 
            pnlStats4.BackColor = Color.FromArgb(155, 89, 182);
            pnlStats4.Controls.Add(lblStats4Icon);
            pnlStats4.Controls.Add(lblStats4Label);
            pnlStats4.Controls.Add(lblStats4Value);
            pnlStats4.Cursor = Cursors.Hand;
            pnlStats4.Location = new Point(720, 0);
            pnlStats4.Margin = new Padding(0, 0, 15, 0);
            pnlStats4.Name = "pnlStats4";
            pnlStats4.Size = new Size(225, 100);
            pnlStats4.TabIndex = 3;
            // 
            // lblStats4Icon
            // 
            lblStats4Icon.Font = new Font("Segoe UI", 30F);
            lblStats4Icon.ForeColor = Color.White;
            lblStats4Icon.Location = new Point(20, 18);
            lblStats4Icon.Name = "lblStats4Icon";
            lblStats4Icon.Size = new Size(50, 50);
            lblStats4Icon.TabIndex = 0;
            lblStats4Icon.Text = "👥";
            lblStats4Icon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblStats4Label
            // 
            lblStats4Label.Font = new Font("Segoe UI", 10F);
            lblStats4Label.ForeColor = Color.FromArgb(240, 220, 250);
            lblStats4Label.Location = new Point(75, 22);
            lblStats4Label.Name = "lblStats4Label";
            lblStats4Label.Size = new Size(135, 25);
            lblStats4Label.TabIndex = 1;
            lblStats4Label.Text = "Khách hàng";
            lblStats4Label.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblStats4Value
            // 
            lblStats4Value.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
            lblStats4Value.ForeColor = Color.White;
            lblStats4Value.Location = new Point(75, 52);
            lblStats4Value.Name = "lblStats4Value";
            lblStats4Value.Size = new Size(135, 38);
            lblStats4Value.TabIndex = 2;
            lblStats4Value.Text = "10";
            lblStats4Value.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(240, 242, 245);
            ClientSize = new Size(1400, 900);
            Controls.Add(pnlContent);
            Controls.Add(pnlTopBar);
            Controls.Add(pnlSidebar);
            Name = "frmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MilkTea POS - Hệ thống quản lý trà sữa";
            WindowState = FormWindowState.Maximized;
            pnlSidebar.ResumeLayout(false);
            pnlTopBar.ResumeLayout(false);
            pnlContent.ResumeLayout(false);
            pnlWelcome.ResumeLayout(false);
            pnlStatsContainer.ResumeLayout(false);
            pnlStats1.ResumeLayout(false);
            pnlStats2.ResumeLayout(false);
            pnlStats3.ResumeLayout(false);
            pnlStats4.ResumeLayout(false);
            ResumeLayout(false);
        }

        private void btnChangePassword_Click(object? sender, EventArgs e)
        {
            var frmChangePassword = new frmChangePassword(_currentUser);
            frmChangePassword.ShowDialog(this);
        }
    }
}
