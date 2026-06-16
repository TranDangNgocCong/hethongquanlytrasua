namespace MilkTeaPOS
{
    partial class frmOrderHistory
    {
        private System.ComponentModel.IContainer components = null;

        // Filter bar
        private System.Windows.Forms.Panel pnlFilterBar;
        private System.Windows.Forms.Label lblFilterFrom;
        private System.Windows.Forms.Label lblFilterTo;
        private System.Windows.Forms.Label lblFilterStatus;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.Button btnLoc;

        // Dashboard cards
        private System.Windows.Forms.Panel pnlDashboard;
        private System.Windows.Forms.Panel pnlCardOrders;
        private System.Windows.Forms.Panel pnlCardRevenue;
        private System.Windows.Forms.Panel pnlCardPending;
        private System.Windows.Forms.Panel pnlCardCancelled;
        private System.Windows.Forms.Label lblCardOrdersLabel;
        private System.Windows.Forms.Label lblCardOrdersValue;
        private System.Windows.Forms.Label lblCardRevenueLabel;
        private System.Windows.Forms.Label lblCardRevenueValue;
        private System.Windows.Forms.Label lblCardPendingLabel;
        private System.Windows.Forms.Label lblCardPendingValue;
        private System.Windows.Forms.Label lblCardCancelledLabel;
        private System.Windows.Forms.Label lblCardCancelledValue;

        // Split container
        private System.Windows.Forms.SplitContainer splitContainer;

        // Order list panel (left)
        private System.Windows.Forms.Panel pnlOrderList;
        private System.Windows.Forms.Label lblOrderListHeader;

        // Order detail panel (right)
        private System.Windows.Forms.Panel pnlOrderDetail;
        private System.Windows.Forms.Label lblDetailHeader;

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
            pnlFilterBar = new Panel();
            lblFilterFrom = new Label();
            dtpTuNgay = new DateTimePicker();
            lblFilterTo = new Label();
            dtpDenNgay = new DateTimePicker();
            lblFilterStatus = new Label();
            cboTrangThai = new ComboBox();
            btnLoc = new Button();
            pnlDashboard = new Panel();
            pnlCardOrders = new Panel();
            lblCardOrdersLabel = new Label();
            lblCardOrdersValue = new Label();
            pnlCardRevenue = new Panel();
            lblCardRevenueLabel = new Label();
            lblCardRevenueValue = new Label();
            pnlCardPending = new Panel();
            lblCardPendingLabel = new Label();
            lblCardPendingValue = new Label();
            pnlCardCancelled = new Panel();
            lblCardCancelledLabel = new Label();
            lblCardCancelledValue = new Label();
            splitContainer = new SplitContainer();
            pnlOrderList = new Panel();
            lblOrderListHeader = new Label();
            pnlOrderDetail = new Panel();
            lblDetailHeader = new Label();
            pnlFilterBar.SuspendLayout();
            pnlDashboard.SuspendLayout();
            pnlCardOrders.SuspendLayout();
            pnlCardRevenue.SuspendLayout();
            pnlCardPending.SuspendLayout();
            pnlCardCancelled.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            pnlOrderList.SuspendLayout();
            pnlOrderDetail.SuspendLayout();
            SuspendLayout();
            // 
            // pnlFilterBar
            // 
            pnlFilterBar.BackColor = Color.White;
            pnlFilterBar.Controls.Add(lblFilterFrom);
            pnlFilterBar.Controls.Add(dtpTuNgay);
            pnlFilterBar.Controls.Add(lblFilterTo);
            pnlFilterBar.Controls.Add(dtpDenNgay);
            pnlFilterBar.Controls.Add(lblFilterStatus);
            pnlFilterBar.Controls.Add(cboTrangThai);
            pnlFilterBar.Controls.Add(btnLoc);
            pnlFilterBar.Dock = DockStyle.Top;
            pnlFilterBar.Location = new Point(0, 0);
            pnlFilterBar.Name = "pnlFilterBar";
            pnlFilterBar.Size = new Size(1400, 50);
            pnlFilterBar.TabIndex = 0;
            // 
            // lblFilterFrom
            // 
            lblFilterFrom.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblFilterFrom.ForeColor = Color.FromArgb(45, 55, 72);
            lblFilterFrom.Location = new Point(16, 12);
            lblFilterFrom.Name = "lblFilterFrom";
            lblFilterFrom.Size = new Size(40, 24);
            lblFilterFrom.TabIndex = 0;
            lblFilterFrom.Text = "Từ:";
            lblFilterFrom.TextAlign = ContentAlignment.MiddleRight;
            // 
            // dtpTuNgay
            // 
            dtpTuNgay.CustomFormat = "dd/MM/yyyy";
            dtpTuNgay.Font = new Font("Segoe UI", 9F);
            dtpTuNgay.Format = DateTimePickerFormat.Custom;
            dtpTuNgay.Location = new Point(62, 12);
            dtpTuNgay.Name = "dtpTuNgay";
            dtpTuNgay.Size = new Size(120, 23);
            dtpTuNgay.TabIndex = 1;
            // 
            // lblFilterTo
            // 
            lblFilterTo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblFilterTo.ForeColor = Color.FromArgb(45, 55, 72);
            lblFilterTo.Location = new Point(192, 12);
            lblFilterTo.Name = "lblFilterTo";
            lblFilterTo.Size = new Size(40, 24);
            lblFilterTo.TabIndex = 2;
            lblFilterTo.Text = "Đến:";
            lblFilterTo.TextAlign = ContentAlignment.MiddleRight;
            // 
            // dtpDenNgay
            // 
            dtpDenNgay.CustomFormat = "dd/MM/yyyy";
            dtpDenNgay.Font = new Font("Segoe UI", 9F);
            dtpDenNgay.Format = DateTimePickerFormat.Custom;
            dtpDenNgay.Location = new Point(238, 12);
            dtpDenNgay.Name = "dtpDenNgay";
            dtpDenNgay.Size = new Size(120, 23);
            dtpDenNgay.TabIndex = 3;
            // 
            // lblFilterStatus
            // 
            lblFilterStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblFilterStatus.ForeColor = Color.FromArgb(45, 55, 72);
            lblFilterStatus.Location = new Point(364, 13);
            lblFilterStatus.Name = "lblFilterStatus";
            lblFilterStatus.Size = new Size(84, 24);
            lblFilterStatus.TabIndex = 4;
            lblFilterStatus.Text = "Trạng thái:";
            lblFilterStatus.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cboTrangThai
            // 
            cboTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTrangThai.FlatStyle = FlatStyle.Flat;
            cboTrangThai.Font = new Font("Segoe UI", 9F);
            cboTrangThai.FormattingEnabled = true;
            cboTrangThai.Location = new Point(454, 12);
            cboTrangThai.Name = "cboTrangThai";
            cboTrangThai.Size = new Size(160, 23);
            cboTrangThai.TabIndex = 5;
            // 
            // btnLoc
            // 
            btnLoc.BackColor = Color.FromArgb(23, 162, 184);
            btnLoc.Cursor = Cursors.Hand;
            btnLoc.FlatAppearance.BorderSize = 0;
            btnLoc.FlatStyle = FlatStyle.Flat;
            btnLoc.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnLoc.ForeColor = Color.White;
            btnLoc.Location = new Point(636, 9);
            btnLoc.Name = "btnLoc";
            btnLoc.Size = new Size(80, 26);
            btnLoc.TabIndex = 6;
            btnLoc.Text = "🔍 Lọc";
            btnLoc.UseVisualStyleBackColor = false;
            btnLoc.Click += BtnLoc_Click;
            // 
            // pnlDashboard
            // 
            pnlDashboard.BackColor = Color.FromArgb(247, 249, 252);
            pnlDashboard.Controls.Add(pnlCardOrders);
            pnlDashboard.Controls.Add(pnlCardRevenue);
            pnlDashboard.Controls.Add(pnlCardPending);
            pnlDashboard.Controls.Add(pnlCardCancelled);
            pnlDashboard.Dock = DockStyle.Top;
            pnlDashboard.Location = new Point(0, 50);
            pnlDashboard.Name = "pnlDashboard";
            pnlDashboard.Padding = new Padding(12, 8, 12, 8);
            pnlDashboard.Size = new Size(1400, 80);
            pnlDashboard.TabIndex = 1;
            // 
            // pnlCardOrders
            // 
            pnlCardOrders.BackColor = Color.White;
            pnlCardOrders.Controls.Add(lblCardOrdersLabel);
            pnlCardOrders.Controls.Add(lblCardOrdersValue);
            pnlCardOrders.Location = new Point(12, 8);
            pnlCardOrders.Name = "pnlCardOrders";
            pnlCardOrders.Size = new Size(220, 64);
            pnlCardOrders.TabIndex = 0;
            // 
            // lblCardOrdersLabel
            // 
            lblCardOrdersLabel.Font = new Font("Segoe UI", 8F);
            lblCardOrdersLabel.ForeColor = Color.FromArgb(108, 117, 125);
            lblCardOrdersLabel.Location = new Point(12, 6);
            lblCardOrdersLabel.Name = "lblCardOrdersLabel";
            lblCardOrdersLabel.Size = new Size(196, 16);
            lblCardOrdersLabel.TabIndex = 0;
            lblCardOrdersLabel.Text = "Đơn hôm nay";
            lblCardOrdersLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblCardOrdersValue
            // 
            lblCardOrdersValue.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblCardOrdersValue.ForeColor = Color.FromArgb(23, 162, 184);
            lblCardOrdersValue.Location = new Point(12, 24);
            lblCardOrdersValue.Name = "lblCardOrdersValue";
            lblCardOrdersValue.Size = new Size(196, 32);
            lblCardOrdersValue.TabIndex = 1;
            lblCardOrdersValue.Text = "0";
            lblCardOrdersValue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlCardRevenue
            // 
            pnlCardRevenue.BackColor = Color.White;
            pnlCardRevenue.Controls.Add(lblCardRevenueLabel);
            pnlCardRevenue.Controls.Add(lblCardRevenueValue);
            pnlCardRevenue.Location = new Point(244, 8);
            pnlCardRevenue.Name = "pnlCardRevenue";
            pnlCardRevenue.Size = new Size(280, 64);
            pnlCardRevenue.TabIndex = 1;
            // 
            // lblCardRevenueLabel
            // 
            lblCardRevenueLabel.Font = new Font("Segoe UI", 8F);
            lblCardRevenueLabel.ForeColor = Color.FromArgb(108, 117, 125);
            lblCardRevenueLabel.Location = new Point(12, 6);
            lblCardRevenueLabel.Name = "lblCardRevenueLabel";
            lblCardRevenueLabel.Size = new Size(256, 16);
            lblCardRevenueLabel.TabIndex = 0;
            lblCardRevenueLabel.Text = "Doanh thu hôm nay";
            lblCardRevenueLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblCardRevenueValue
            // 
            lblCardRevenueValue.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblCardRevenueValue.ForeColor = Color.FromArgb(72, 187, 120);
            lblCardRevenueValue.Location = new Point(12, 24);
            lblCardRevenueValue.Name = "lblCardRevenueValue";
            lblCardRevenueValue.Size = new Size(256, 32);
            lblCardRevenueValue.TabIndex = 1;
            lblCardRevenueValue.Text = "0đ";
            lblCardRevenueValue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlCardPending
            // 
            pnlCardPending.BackColor = Color.White;
            pnlCardPending.Controls.Add(lblCardPendingLabel);
            pnlCardPending.Controls.Add(lblCardPendingValue);
            pnlCardPending.Location = new Point(536, 8);
            pnlCardPending.Name = "pnlCardPending";
            pnlCardPending.Size = new Size(180, 64);
            pnlCardPending.TabIndex = 2;
            // 
            // lblCardPendingLabel
            // 
            lblCardPendingLabel.Font = new Font("Segoe UI", 8F);
            lblCardPendingLabel.ForeColor = Color.FromArgb(108, 117, 125);
            lblCardPendingLabel.Location = new Point(12, 6);
            lblCardPendingLabel.Name = "lblCardPendingLabel";
            lblCardPendingLabel.Size = new Size(156, 16);
            lblCardPendingLabel.TabIndex = 0;
            lblCardPendingLabel.Text = "Đơn đang giữ";
            lblCardPendingLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblCardPendingValue
            // 
            lblCardPendingValue.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblCardPendingValue.ForeColor = Color.FromArgb(255, 193, 7);
            lblCardPendingValue.Location = new Point(12, 24);
            lblCardPendingValue.Name = "lblCardPendingValue";
            lblCardPendingValue.Size = new Size(156, 32);
            lblCardPendingValue.TabIndex = 1;
            lblCardPendingValue.Text = "0";
            lblCardPendingValue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlCardCancelled
            // 
            pnlCardCancelled.BackColor = Color.White;
            pnlCardCancelled.Controls.Add(lblCardCancelledLabel);
            pnlCardCancelled.Controls.Add(lblCardCancelledValue);
            pnlCardCancelled.Location = new Point(728, 8);
            pnlCardCancelled.Name = "pnlCardCancelled";
            pnlCardCancelled.Size = new Size(180, 64);
            pnlCardCancelled.TabIndex = 3;
            // 
            // lblCardCancelledLabel
            // 
            lblCardCancelledLabel.Font = new Font("Segoe UI", 8F);
            lblCardCancelledLabel.ForeColor = Color.FromArgb(108, 117, 125);
            lblCardCancelledLabel.Location = new Point(12, 6);
            lblCardCancelledLabel.Name = "lblCardCancelledLabel";
            lblCardCancelledLabel.Size = new Size(156, 16);
            lblCardCancelledLabel.TabIndex = 0;
            lblCardCancelledLabel.Text = "Đơn hoàn trả";
            lblCardCancelledLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblCardCancelledValue
            // 
            lblCardCancelledValue.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblCardCancelledValue.ForeColor = Color.FromArgb(220, 53, 69);
            lblCardCancelledValue.Location = new Point(12, 24);
            lblCardCancelledValue.Name = "lblCardCancelledValue";
            lblCardCancelledValue.Size = new Size(156, 32);
            lblCardCancelledValue.TabIndex = 1;
            lblCardCancelledValue.Text = "0";
            lblCardCancelledValue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // splitContainer
            // 
            splitContainer.BackColor = Color.FromArgb(226, 232, 240);
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.Location = new Point(0, 130);
            splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add(pnlOrderList);
            splitContainer.Panel1MinSize = 300;
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(pnlOrderDetail);
            splitContainer.Panel2MinSize = 300;
            splitContainer.Size = new Size(1400, 620);
            splitContainer.SplitterDistance = 480;
            splitContainer.TabIndex = 2;
            // 
            // pnlOrderList
            // 
            pnlOrderList.Controls.Add(lblOrderListHeader);
            pnlOrderList.Dock = DockStyle.Fill;
            pnlOrderList.Location = new Point(0, 0);
            pnlOrderList.Name = "pnlOrderList";
            pnlOrderList.Size = new Size(480, 620);
            pnlOrderList.TabIndex = 0;
            // 
            // lblOrderListHeader
            // 
            lblOrderListHeader.BackColor = Color.White;
            lblOrderListHeader.Dock = DockStyle.Top;
            lblOrderListHeader.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblOrderListHeader.ForeColor = Color.FromArgb(45, 55, 72);
            lblOrderListHeader.Location = new Point(0, 0);
            lblOrderListHeader.Name = "lblOrderListHeader";
            lblOrderListHeader.Padding = new Padding(12, 8, 12, 8);
            lblOrderListHeader.Size = new Size(480, 36);
            lblOrderListHeader.TabIndex = 0;
            lblOrderListHeader.Text = "📋 Danh sách đơn hàng";
            // 
            // pnlOrderDetail
            // 
            pnlOrderDetail.Controls.Add(lblDetailHeader);
            pnlOrderDetail.Dock = DockStyle.Fill;
            pnlOrderDetail.Location = new Point(0, 0);
            pnlOrderDetail.Name = "pnlOrderDetail";
            pnlOrderDetail.Size = new Size(916, 620);
            pnlOrderDetail.TabIndex = 0;
            // 
            // lblDetailHeader
            // 
            lblDetailHeader.BackColor = Color.White;
            lblDetailHeader.Dock = DockStyle.Top;
            lblDetailHeader.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDetailHeader.ForeColor = Color.FromArgb(45, 55, 72);
            lblDetailHeader.Location = new Point(0, 0);
            lblDetailHeader.Name = "lblDetailHeader";
            lblDetailHeader.Padding = new Padding(12, 8, 12, 8);
            lblDetailHeader.Size = new Size(916, 36);
            lblDetailHeader.TabIndex = 0;
            lblDetailHeader.Text = "📄 Chi tiết đơn hàng";
            lblDetailHeader.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // frmOrderHistory
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(247, 249, 252);
            ClientSize = new Size(1400, 750);
            Controls.Add(splitContainer);
            Controls.Add(pnlDashboard);
            Controls.Add(pnlFilterBar);
            MinimumSize = new Size(1100, 600);
            Name = "frmOrderHistory";
            StartPosition = FormStartPosition.CenterParent;
            Text = "📜 Lịch sử đơn hàng - MilkTea POS";
            Load += FrmOrderHistory_Load;
            pnlFilterBar.ResumeLayout(false);
            pnlDashboard.ResumeLayout(false);
            pnlCardOrders.ResumeLayout(false);
            pnlCardRevenue.ResumeLayout(false);
            pnlCardPending.ResumeLayout(false);
            pnlCardCancelled.ResumeLayout(false);
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            pnlOrderList.ResumeLayout(false);
            pnlOrderDetail.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
