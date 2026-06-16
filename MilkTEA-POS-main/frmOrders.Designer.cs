namespace MilkTeaPOS
{
    partial class frmOrders
    {
        private System.ComponentModel.IContainer components = null;

        // Header
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblClock;
        private System.Windows.Forms.Label lblInvoiceInfo;

        // Top bar
        private System.Windows.Forms.Panel pnlTopBar;
        private System.Windows.Forms.Label lblOrderType;
        private System.Windows.Forms.ComboBox cbotrangthai;
        private System.Windows.Forms.Label lblBanLabel;
        private System.Windows.Forms.ComboBox cboBan;
        private System.Windows.Forms.Button btnTraBan;
        private System.Windows.Forms.Label lblSearchIcon;
        private System.Windows.Forms.TextBox txtfind;
        private System.Windows.Forms.Button btnTimMember;
        private System.Windows.Forms.Label lblCustomerInfo;

        // Category filter bar (dynamic buttons from DB)
        private System.Windows.Forms.Panel pnlCategories;
        private System.Windows.Forms.FlowLayoutPanel pnlCategoryButtons;

        // Split container for main area
        private System.Windows.Forms.SplitContainer splitContainer;

        // Products panel (left side of split)
        private System.Windows.Forms.Panel pnlProductContainer;
        private System.Windows.Forms.FlowLayoutPanel pnlContent;
        private System.Windows.Forms.Panel pnlStatusBar;
        private System.Windows.Forms.Label lbltrangthai;

        // Cart side (right side of split)
        private System.Windows.Forms.Panel pnlCartHeader;
        private System.Windows.Forms.Label lblCartTitle;
        private System.Windows.Forms.Button btnXoaGioHang;
        private System.Windows.Forms.Panel pnlGiohang;

        // Payment section
        private System.Windows.Forms.Panel pnlPayment;
        private System.Windows.Forms.Label lblVoucherLabel;
        private System.Windows.Forms.TextBox txtVoucher;
        private System.Windows.Forms.Button btnApdungvoucher;
        private System.Windows.Forms.Label lblPhoneLabel;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.Button btnThemMember;

        // Totals
        private System.Windows.Forms.Panel pnlTotals;
        private System.Windows.Forms.Label lblTamtinhLabel;
        private System.Windows.Forms.Label lblTamtinh;
        private System.Windows.Forms.Label lblGiamGiaLabel;
        private System.Windows.Forms.Label lblGiamGia;
        private System.Windows.Forms.Label lblDiemLabel;
        private System.Windows.Forms.Label lblDiemTichLuy;
        private System.Windows.Forms.Panel pnlTotalLine;
        private System.Windows.Forms.Label lblTongLabel;
        private System.Windows.Forms.Label lblTongThanhToan;
        private System.Windows.Forms.Button btnThanhtoan;

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
            pnlHeader = new Panel();
            lblTitle = new Label();
            lblClock = new Label();
            lblInvoiceInfo = new Label();
            pnlTopBar = new Panel();
            lblOrderType = new Label();
            cbotrangthai = new ComboBox();
            lblBanLabel = new Label();
            cboBan = new ComboBox();
            btnTraBan = new Button();
            lblSearchIcon = new Label();
            txtfind = new TextBox();
            btnTimMember = new Button();
            lblCustomerInfo = new Label();
            pnlCategories = new Panel();
            pnlCategoryButtons = new FlowLayoutPanel();
            splitContainer = new SplitContainer();
            pnlProductContainer = new Panel();
            pnlContent = new FlowLayoutPanel();
            pnlStatusBar = new Panel();
            lbltrangthai = new Label();
            pnlGiohang = new Panel();
            pnlCartHeader = new Panel();
            lblCartTitle = new Label();
            btnXoaGioHang = new Button();
            pnlPayment = new Panel();
            lblVoucherLabel = new Label();
            txtVoucher = new TextBox();
            btnApdungvoucher = new Button();
            lblPhoneLabel = new Label();
            txtSDT = new TextBox();
            btnThemMember = new Button();
            pnlTotals = new Panel();
            lblTamtinhLabel = new Label();
            lblTamtinh = new Label();
            lblGiamGiaLabel = new Label();
            lblGiamGia = new Label();
            lblDiemLabel = new Label();
            lblDiemTichLuy = new Label();
            pnlTotalLine = new Panel();
            lblTongLabel = new Label();
            lblTongThanhToan = new Label();
            btnThanhtoan = new Button();
            pnlHeader.SuspendLayout();
            pnlTopBar.SuspendLayout();
            pnlCategories.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            pnlProductContainer.SuspendLayout();
            pnlStatusBar.SuspendLayout();
            pnlCartHeader.SuspendLayout();
            pnlPayment.SuspendLayout();
            pnlTotals.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(45, 55, 72);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(lblClock);
            pnlHeader.Controls.Add(lblInvoiceInfo);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1924, 70);
            pnlHeader.TabIndex = 3;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(20, 18);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(213, 32);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "\U0001f9cb Tạo đơn hàng";
            // 
            // lblClock
            // 
            lblClock.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblClock.ForeColor = Color.FromArgb(255, 193, 7);
            lblClock.Location = new Point(900, 20);
            lblClock.Name = "lblClock";
            lblClock.Size = new Size(150, 30);
            lblClock.TabIndex = 0;
            lblClock.Text = "00:00:00";
            lblClock.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblInvoiceInfo
            // 
            lblInvoiceInfo.Font = new Font("Segoe UI", 11F);
            lblInvoiceInfo.ForeColor = Color.FromArgb(200, 210, 220);
            lblInvoiceInfo.Location = new Point(1060, 22);
            lblInvoiceInfo.Name = "lblInvoiceInfo";
            lblInvoiceInfo.Size = new Size(220, 25);
            lblInvoiceInfo.TabIndex = 1;
            lblInvoiceInfo.Text = "HD-000000";
            lblInvoiceInfo.TextAlign = ContentAlignment.MiddleRight;
            // 
            // pnlTopBar
            // 
            pnlTopBar.BackColor = Color.White;
            pnlTopBar.Controls.Add(lblOrderType);
            pnlTopBar.Controls.Add(cbotrangthai);
            pnlTopBar.Controls.Add(lblBanLabel);
            pnlTopBar.Controls.Add(cboBan);
            pnlTopBar.Controls.Add(btnTraBan);
            pnlTopBar.Controls.Add(lblSearchIcon);
            pnlTopBar.Controls.Add(txtfind);
            pnlTopBar.Controls.Add(btnTimMember);
            pnlTopBar.Controls.Add(lblCustomerInfo);
            pnlTopBar.Dock = DockStyle.Top;
            pnlTopBar.Location = new Point(0, 70);
            pnlTopBar.Name = "pnlTopBar";
            pnlTopBar.Size = new Size(1924, 50);
            pnlTopBar.TabIndex = 2;
            // 
            // lblOrderType
            // 
            lblOrderType.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblOrderType.ForeColor = Color.FromArgb(45, 55, 72);
            lblOrderType.Location = new Point(16, 12);
            lblOrderType.Name = "lblOrderType";
            lblOrderType.Size = new Size(60, 26);
            lblOrderType.TabIndex = 0;
            lblOrderType.Text = "Kiểu:";
            lblOrderType.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cbotrangthai
            // 
            cbotrangthai.DropDownStyle = ComboBoxStyle.DropDownList;
            cbotrangthai.FlatStyle = FlatStyle.Flat;
            cbotrangthai.Font = new Font("Segoe UI", 10F);
            cbotrangthai.FormattingEnabled = true;
            cbotrangthai.Location = new Point(82, 12);
            cbotrangthai.Name = "cbotrangthai";
            cbotrangthai.Size = new Size(180, 25);
            cbotrangthai.TabIndex = 1;
            cbotrangthai.SelectedIndexChanged += cbotrangthai_SelectedIndexChanged;
            // 
            // lblBanLabel
            // 
            lblBanLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblBanLabel.ForeColor = Color.FromArgb(45, 55, 72);
            lblBanLabel.Location = new Point(268, 12);
            lblBanLabel.Name = "lblBanLabel";
            lblBanLabel.Size = new Size(40, 26);
            lblBanLabel.TabIndex = 6;
            lblBanLabel.Text = "Bàn:";
            lblBanLabel.TextAlign = ContentAlignment.MiddleRight;
            lblBanLabel.Visible = false;
            // 
            // cboBan
            // 
            cboBan.DropDownStyle = ComboBoxStyle.DropDownList;
            cboBan.FlatStyle = FlatStyle.Flat;
            cboBan.Font = new Font("Segoe UI", 10F);
            cboBan.FormattingEnabled = true;
            cboBan.Location = new Point(314, 12);
            cboBan.Name = "cboBan";
            cboBan.Size = new Size(150, 25);
            cboBan.TabIndex = 7;
            cboBan.SelectedIndexChanged += cboBan_SelectedIndexChanged;
            cboBan.Visible = false;
            //
            // btnTraBan
            //
            btnTraBan.BackColor = Color.FromArgb(255, 152, 0);
            btnTraBan.Cursor = Cursors.Hand;
            btnTraBan.FlatAppearance.BorderSize = 0;
            btnTraBan.FlatStyle = FlatStyle.Flat;
            btnTraBan.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnTraBan.ForeColor = Color.White;
            btnTraBan.Location = new Point(470, 12);
            btnTraBan.Name = "btnTraBan";
            btnTraBan.Size = new Size(80, 28);
            btnTraBan.TabIndex = 8;
            btnTraBan.Text = "🔓 Trả bàn";
            btnTraBan.UseVisualStyleBackColor = false;
            btnTraBan.Visible = false;
            btnTraBan.Click += btnTraBan_Click;
            //
            // lblSearchIcon
            //
            lblSearchIcon.Font = new Font("Segoe UI", 11F);
            lblSearchIcon.ForeColor = Color.FromArgb(150, 160, 170);
            lblSearchIcon.Location = new Point(556, 14);
            lblSearchIcon.Name = "lblSearchIcon";
            lblSearchIcon.Size = new Size(24, 22);
            lblSearchIcon.TabIndex = 2;
            lblSearchIcon.Text = "🔍";
            lblSearchIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtfind
            // 
            txtfind.BorderStyle = BorderStyle.FixedSingle;
            txtfind.Font = new Font("Segoe UI", 10F);
            txtfind.Location = new Point(580, 12);
            txtfind.Name = "txtfind";
            txtfind.Size = new Size(250, 25);
            txtfind.TabIndex = 3;
            txtfind.TextChanged += txtfind_TextChanged;
            // 
            // btnTimMember
            // 
            btnTimMember.BackColor = Color.FromArgb(23, 162, 184);
            btnTimMember.Cursor = Cursors.Hand;
            btnTimMember.FlatAppearance.BorderSize = 0;
            btnTimMember.FlatStyle = FlatStyle.Flat;
            btnTimMember.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnTimMember.ForeColor = Color.White;
            btnTimMember.Location = new Point(846, 12);
            btnTimMember.Name = "btnTimMember";
            btnTimMember.Size = new Size(110, 28);
            btnTimMember.TabIndex = 4;
            btnTimMember.Text = "👤 Thành viên";
            btnTimMember.UseVisualStyleBackColor = false;
            btnTimMember.Click += btnTimMember_Click;
            // 
            // lblCustomerInfo
            // 
            lblCustomerInfo.Font = new Font("Segoe UI", 10F);
            lblCustomerInfo.ForeColor = Color.FromArgb(45, 55, 72);
            lblCustomerInfo.Location = new Point(962, 16);
            lblCustomerInfo.Name = "lblCustomerInfo";
            lblCustomerInfo.Size = new Size(195, 22);
            lblCustomerInfo.TabIndex = 5;
            lblCustomerInfo.Text = "Khách lẻ";
            // 
            // pnlCategories
            // 
            pnlCategories.BackColor = Color.White;
            pnlCategories.Controls.Add(pnlCategoryButtons);
            pnlCategories.Dock = DockStyle.Top;
            pnlCategories.Location = new Point(0, 120);
            pnlCategories.Name = "pnlCategories";
            pnlCategories.Size = new Size(1924, 48);
            pnlCategories.TabIndex = 1;
            // 
            // pnlCategoryButtons
            // 
            pnlCategoryButtons.AutoScroll = true;
            pnlCategoryButtons.Dock = DockStyle.Fill;
            pnlCategoryButtons.Location = new Point(0, 0);
            pnlCategoryButtons.Name = "pnlCategoryButtons";
            pnlCategoryButtons.Padding = new Padding(12, 6, 12, 6);
            pnlCategoryButtons.Size = new Size(1924, 48);
            pnlCategoryButtons.TabIndex = 0;
            pnlCategoryButtons.WrapContents = false;
            // 
            // splitContainer
            // 
            splitContainer.BackColor = Color.FromArgb(226, 232, 240);
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.Location = new Point(0, 168);
            splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add(pnlProductContainer);
            splitContainer.Panel1MinSize = 300;
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(pnlGiohang);
            splitContainer.Panel2.Controls.Add(pnlCartHeader);
            splitContainer.Panel2.Controls.Add(pnlPayment);
            splitContainer.Panel2.Controls.Add(pnlTotals);
            splitContainer.Panel2.Controls.Add(btnThanhtoan);
            splitContainer.Panel2MinSize = 150;
            splitContainer.Size = new Size(1924, 801);
            splitContainer.SplitterDistance = 1500;
            splitContainer.TabIndex = 0;
            // 
            // pnlProductContainer
            // 
            pnlProductContainer.Controls.Add(pnlContent);
            pnlProductContainer.Controls.Add(pnlStatusBar);
            pnlProductContainer.Dock = DockStyle.Fill;
            pnlProductContainer.Location = new Point(0, 0);
            pnlProductContainer.Name = "pnlProductContainer";
            pnlProductContainer.Size = new Size(1500, 801);
            pnlProductContainer.TabIndex = 0;
            // 
            // pnlContent
            // 
            pnlContent.AutoScroll = true;
            pnlContent.BackColor = Color.FromArgb(247, 249, 252);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(0, 0);
            pnlContent.Name = "pnlContent";
            pnlContent.Padding = new Padding(8);
            pnlContent.Size = new Size(1500, 777);
            pnlContent.TabIndex = 0;
            // 
            // pnlStatusBar
            // 
            pnlStatusBar.BackColor = Color.White;
            pnlStatusBar.Controls.Add(lbltrangthai);
            pnlStatusBar.Dock = DockStyle.Bottom;
            pnlStatusBar.Location = new Point(0, 777);
            pnlStatusBar.Name = "pnlStatusBar";
            pnlStatusBar.Size = new Size(1500, 24);
            pnlStatusBar.TabIndex = 1;
            // 
            // lbltrangthai
            // 
            lbltrangthai.Dock = DockStyle.Fill;
            lbltrangthai.Font = new Font("Segoe UI", 9F);
            lbltrangthai.ForeColor = Color.FromArgb(108, 117, 125);
            lbltrangthai.Location = new Point(0, 0);
            lbltrangthai.Name = "lbltrangthai";
            lbltrangthai.Size = new Size(1500, 24);
            lbltrangthai.TabIndex = 0;
            lbltrangthai.Text = "0 món";
            lbltrangthai.TextAlign = ContentAlignment.MiddleRight;
            // 
            // pnlGiohang
            // 
            pnlGiohang.AutoScroll = true;
            pnlGiohang.BackColor = Color.White;
            pnlGiohang.Dock = DockStyle.Fill;
            pnlGiohang.Location = new Point(0, 48);
            pnlGiohang.Name = "pnlGiohang";
            pnlGiohang.Padding = new Padding(8, 0, 8, 0);
            pnlGiohang.Size = new Size(420, 523);
            pnlGiohang.TabIndex = 0;
            // 
            // pnlCartHeader
            // 
            pnlCartHeader.BackColor = Color.White;
            pnlCartHeader.Controls.Add(lblCartTitle);
            pnlCartHeader.Controls.Add(btnXoaGioHang);
            pnlCartHeader.Dock = DockStyle.Top;
            pnlCartHeader.Location = new Point(0, 0);
            pnlCartHeader.Name = "pnlCartHeader";
            pnlCartHeader.Size = new Size(420, 48);
            pnlCartHeader.TabIndex = 11;
            // 
            // lblCartTitle
            // 
            lblCartTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblCartTitle.ForeColor = Color.FromArgb(45, 55, 72);
            lblCartTitle.Location = new Point(16, 10);
            lblCartTitle.Name = "lblCartTitle";
            lblCartTitle.Size = new Size(139, 28);
            lblCartTitle.TabIndex = 0;
            lblCartTitle.Text = "\U0001f6d2 Giỏ hàng";
            lblCartTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnXoaGioHang
            // 
            btnXoaGioHang.BackColor = Color.FromArgb(220, 53, 69);
            btnXoaGioHang.Cursor = Cursors.Hand;
            btnXoaGioHang.FlatAppearance.BorderSize = 0;
            btnXoaGioHang.FlatStyle = FlatStyle.Flat;
            btnXoaGioHang.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnXoaGioHang.ForeColor = Color.White;
            btnXoaGioHang.Location = new Point(290, 10);
            btnXoaGioHang.Name = "btnXoaGioHang";
            btnXoaGioHang.Size = new Size(110, 28);
            btnXoaGioHang.TabIndex = 1;
            btnXoaGioHang.Text = "🗑 Xóa giỏ";
            btnXoaGioHang.UseVisualStyleBackColor = false;
            btnXoaGioHang.Click += btnXoaGioHang_Click;
            // 
            // pnlPayment
            // 
            pnlPayment.BackColor = Color.White;
            pnlPayment.Controls.Add(lblVoucherLabel);
            pnlPayment.Controls.Add(txtVoucher);
            pnlPayment.Controls.Add(btnApdungvoucher);
            pnlPayment.Controls.Add(lblPhoneLabel);
            pnlPayment.Controls.Add(txtSDT);
            pnlPayment.Controls.Add(btnThemMember);
            pnlPayment.Dock = DockStyle.Bottom;
            pnlPayment.Location = new Point(0, 571);
            pnlPayment.Name = "pnlPayment";
            pnlPayment.Padding = new Padding(12, 8, 12, 4);
            pnlPayment.Size = new Size(420, 40);
            pnlPayment.TabIndex = 10;
            // 
            // lblVoucherLabel
            // 
            lblVoucherLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblVoucherLabel.ForeColor = Color.FromArgb(45, 55, 72);
            lblVoucherLabel.Location = new Point(3, 10);
            lblVoucherLabel.Name = "lblVoucherLabel";
            lblVoucherLabel.Size = new Size(58, 22);
            lblVoucherLabel.TabIndex = 0;
            lblVoucherLabel.Text = "Voucher:";
            lblVoucherLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtVoucher
            // 
            txtVoucher.BorderStyle = BorderStyle.FixedSingle;
            txtVoucher.Font = new Font("Segoe UI", 9F);
            txtVoucher.Location = new Point(61, 9);
            txtVoucher.Name = "txtVoucher";
            txtVoucher.Size = new Size(87, 23);
            txtVoucher.TabIndex = 1;
            // 
            // btnApdungvoucher
            // 
            btnApdungvoucher.BackColor = Color.FromArgb(72, 187, 120);
            btnApdungvoucher.Cursor = Cursors.Hand;
            btnApdungvoucher.FlatAppearance.BorderSize = 0;
            btnApdungvoucher.FlatStyle = FlatStyle.Flat;
            btnApdungvoucher.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            btnApdungvoucher.ForeColor = Color.White;
            btnApdungvoucher.Location = new Point(154, 10);
            btnApdungvoucher.Name = "btnApdungvoucher";
            btnApdungvoucher.Size = new Size(64, 23);
            btnApdungvoucher.TabIndex = 2;
            btnApdungvoucher.Text = "Áp dụng";
            btnApdungvoucher.UseVisualStyleBackColor = false;
            btnApdungvoucher.Click += btnApdungvoucher_Click;
            // 
            // lblPhoneLabel
            // 
            lblPhoneLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPhoneLabel.ForeColor = Color.FromArgb(45, 55, 72);
            lblPhoneLabel.Location = new Point(222, 14);
            lblPhoneLabel.Name = "lblPhoneLabel";
            lblPhoneLabel.Size = new Size(30, 22);
            lblPhoneLabel.TabIndex = 3;
            lblPhoneLabel.Text = "SĐT:";
            lblPhoneLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtSDT
            // 
            txtSDT.BorderStyle = BorderStyle.FixedSingle;
            txtSDT.Font = new Font("Segoe UI", 9F);
            txtSDT.Location = new Point(253, 11);
            txtSDT.Name = "txtSDT";
            txtSDT.Size = new Size(100, 23);
            txtSDT.TabIndex = 4;
            // 
            // btnThemMember
            // 
            btnThemMember.BackColor = Color.FromArgb(23, 162, 184);
            btnThemMember.Cursor = Cursors.Hand;
            btnThemMember.FlatAppearance.BorderSize = 0;
            btnThemMember.FlatStyle = FlatStyle.Flat;
            btnThemMember.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            btnThemMember.ForeColor = Color.White;
            btnThemMember.Location = new Point(359, 11);
            btnThemMember.Name = "btnThemMember";
            btnThemMember.Size = new Size(49, 23);
            btnThemMember.TabIndex = 5;
            btnThemMember.Text = "+TV";
            btnThemMember.UseVisualStyleBackColor = false;
            btnThemMember.Click += btnThemMember_Click;
            // 
            // pnlTotals
            // 
            pnlTotals.BackColor = Color.FromArgb(247, 249, 252);
            pnlTotals.Controls.Add(lblTamtinhLabel);
            pnlTotals.Controls.Add(lblTamtinh);
            pnlTotals.Controls.Add(lblGiamGiaLabel);
            pnlTotals.Controls.Add(lblGiamGia);
            pnlTotals.Controls.Add(lblDiemLabel);
            pnlTotals.Controls.Add(lblDiemTichLuy);
            pnlTotals.Controls.Add(pnlTotalLine);
            pnlTotals.Controls.Add(lblTongLabel);
            pnlTotals.Controls.Add(lblTongThanhToan);
            pnlTotals.Dock = DockStyle.Bottom;
            pnlTotals.Location = new Point(0, 611);
            pnlTotals.Name = "pnlTotals";
            pnlTotals.Padding = new Padding(16, 6, 16, 6);
            pnlTotals.Size = new Size(420, 140);
            pnlTotals.TabIndex = 9;
            // 
            // lblTamtinhLabel
            // 
            lblTamtinhLabel.Font = new Font("Segoe UI", 10F);
            lblTamtinhLabel.ForeColor = Color.FromArgb(45, 55, 72);
            lblTamtinhLabel.Location = new Point(16, 12);
            lblTamtinhLabel.Name = "lblTamtinhLabel";
            lblTamtinhLabel.Size = new Size(132, 22);
            lblTamtinhLabel.TabIndex = 0;
            lblTamtinhLabel.Text = "Tạm tính:";
            lblTamtinhLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTamtinh
            // 
            lblTamtinh.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTamtinh.ForeColor = Color.FromArgb(45, 55, 72);
            lblTamtinh.Location = new Point(118, 12);
            lblTamtinh.Name = "lblTamtinh";
            lblTamtinh.Size = new Size(270, 22);
            lblTamtinh.TabIndex = 1;
            lblTamtinh.Text = "0đ";
            lblTamtinh.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblGiamGiaLabel
            // 
            lblGiamGiaLabel.Font = new Font("Segoe UI", 10F);
            lblGiamGiaLabel.ForeColor = Color.FromArgb(45, 55, 72);
            lblGiamGiaLabel.Location = new Point(16, 36);
            lblGiamGiaLabel.Name = "lblGiamGiaLabel";
            lblGiamGiaLabel.Size = new Size(115, 22);
            lblGiamGiaLabel.TabIndex = 2;
            lblGiamGiaLabel.Text = "Giảm giá:";
            lblGiamGiaLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblGiamGia
            // 
            lblGiamGia.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblGiamGia.ForeColor = Color.FromArgb(220, 53, 69);
            lblGiamGia.Location = new Point(118, 34);
            lblGiamGia.Name = "lblGiamGia";
            lblGiamGia.Size = new Size(270, 22);
            lblGiamGia.TabIndex = 3;
            lblGiamGia.Text = "-0đ";
            lblGiamGia.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblDiemLabel
            // 
            lblDiemLabel.Font = new Font("Segoe UI", 10F);
            lblDiemLabel.ForeColor = Color.FromArgb(45, 55, 72);
            lblDiemLabel.Location = new Point(16, 60);
            lblDiemLabel.Name = "lblDiemLabel";
            lblDiemLabel.Size = new Size(132, 22);
            lblDiemLabel.TabIndex = 4;
            lblDiemLabel.Text = "Điểm tích lũy:";
            lblDiemLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDiemTichLuy
            // 
            lblDiemTichLuy.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDiemTichLuy.ForeColor = Color.FromArgb(23, 162, 184);
            lblDiemTichLuy.Location = new Point(115, 58);
            lblDiemTichLuy.Name = "lblDiemTichLuy";
            lblDiemTichLuy.Size = new Size(270, 22);
            lblDiemTichLuy.TabIndex = 5;
            lblDiemTichLuy.Text = "0";
            lblDiemTichLuy.TextAlign = ContentAlignment.MiddleRight;
            // 
            // pnlTotalLine
            // 
            pnlTotalLine.BackColor = Color.FromArgb(45, 55, 72);
            pnlTotalLine.Location = new Point(16, 86);
            pnlTotalLine.Name = "pnlTotalLine";
            pnlTotalLine.Size = new Size(534, 2);
            pnlTotalLine.TabIndex = 6;
            // 
            // lblTongLabel
            // 
            lblTongLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTongLabel.ForeColor = Color.FromArgb(45, 55, 72);
            lblTongLabel.Location = new Point(16, 96);
            lblTongLabel.Name = "lblTongLabel";
            lblTongLabel.Size = new Size(200, 28);
            lblTongLabel.TabIndex = 6;
            lblTongLabel.Text = "TỔNG THANH TOÁN:";
            lblTongLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTongThanhToan
            // 
            lblTongThanhToan.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTongThanhToan.ForeColor = Color.FromArgb(255, 193, 7);
            lblTongThanhToan.Location = new Point(212, 94);
            lblTongThanhToan.Name = "lblTongThanhToan";
            lblTongThanhToan.Size = new Size(176, 30);
            lblTongThanhToan.TabIndex = 7;
            lblTongThanhToan.Text = "0đ";
            lblTongThanhToan.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnThanhtoan
            // 
            btnThanhtoan.BackColor = Color.FromArgb(72, 187, 120);
            btnThanhtoan.Cursor = Cursors.Hand;
            btnThanhtoan.Dock = DockStyle.Bottom;
            btnThanhtoan.FlatAppearance.BorderSize = 0;
            btnThanhtoan.FlatStyle = FlatStyle.Flat;
            btnThanhtoan.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnThanhtoan.ForeColor = Color.White;
            btnThanhtoan.Location = new Point(0, 751);
            btnThanhtoan.Name = "btnThanhtoan";
            btnThanhtoan.Size = new Size(420, 50);
            btnThanhtoan.TabIndex = 8;
            btnThanhtoan.Text = "💰 THANH TOÁN";
            btnThanhtoan.UseVisualStyleBackColor = false;
            btnThanhtoan.Click += btnThanhtoan_Click;
            // 
            // frmOrders
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(247, 249, 252);
            ClientSize = new Size(1924, 969);
            Controls.Add(splitContainer);
            Controls.Add(pnlCategories);
            Controls.Add(pnlTopBar);
            Controls.Add(pnlHeader);
            MinimumSize = new Size(1100, 600);
            Name = "frmOrders";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "\U0001f9cb Tạo đơn hàng - MilkTea POS";
            WindowState = FormWindowState.Maximized;
            Load += frmOrders_Load;
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlTopBar.ResumeLayout(false);
            pnlTopBar.PerformLayout();
            pnlCategories.ResumeLayout(false);
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            pnlProductContainer.ResumeLayout(false);
            pnlStatusBar.ResumeLayout(false);
            pnlCartHeader.ResumeLayout(false);
            pnlPayment.ResumeLayout(false);
            pnlPayment.PerformLayout();
            pnlTotals.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
