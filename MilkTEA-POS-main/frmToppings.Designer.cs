namespace MilkTeaPOS
{
    partial class frmToppings
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
        private System.Windows.Forms.Label lblFilterStatus;
        private System.Windows.Forms.ComboBox cboFilterStatus;

        // DataGridView
        private System.Windows.Forms.DataGridView dgvToppings;

        // Form fields
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.NumericUpDown numPrice;
        private System.Windows.Forms.Label lblIsAvailable;
        private System.Windows.Forms.CheckBox chkIsAvailable;
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
            lblFilterStatus = new Label();
            cboFilterStatus = new ComboBox();
            pnlMain = new Panel();
            dgvToppings = new DataGridView();
            pnlForm = new Panel();
            lblName = new Label();
            txtName = new TextBox();
            lblDescription = new Label();
            txtDescription = new TextBox();
            lblPrice = new Label();
            numPrice = new NumericUpDown();
            lblIsAvailable = new Label();
            chkIsAvailable = new CheckBox();
            lblImageUrl = new Label();
            txtImageUrl = new TextBox();
            btnBrowseImage = new Button();
            picPreview = new PictureBox();
            pnlHeader.SuspendLayout();
            pnlToolbar.SuspendLayout();
            pnlSearch.SuspendLayout();
            pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvToppings).BeginInit();
            pnlForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numPrice).BeginInit();
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
            lblTitle.Text = "🍮 Quản lý Topping";
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
            pnlSearch.Controls.Add(lblFilterStatus);
            pnlSearch.Controls.Add(cboFilterStatus);
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
            // lblFilterStatus
            // 
            lblFilterStatus.Font = new Font("Segoe UI", 12F);
            lblFilterStatus.ForeColor = Color.FromArgb(45, 55, 72);
            lblFilterStatus.Location = new Point(440, 20);
            lblFilterStatus.Name = "lblFilterStatus";
            lblFilterStatus.Size = new Size(80, 21);
            lblFilterStatus.TabIndex = 3;
            lblFilterStatus.Text = "Trạng thái:";
            lblFilterStatus.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cboFilterStatus
            // 
            cboFilterStatus.BackColor = Color.FromArgb(247, 249, 252);
            cboFilterStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFilterStatus.FlatStyle = FlatStyle.Flat;
            cboFilterStatus.Font = new Font("Segoe UI", 12F);
            cboFilterStatus.Location = new Point(525, 15);
            cboFilterStatus.Name = "cboFilterStatus";
            cboFilterStatus.Size = new Size(180, 29);
            cboFilterStatus.TabIndex = 4;
            // 
            // pnlMain
            // 
            pnlMain.BackColor = Color.FromArgb(247, 249, 252);
            pnlMain.Controls.Add(dgvToppings);
            pnlMain.Controls.Add(pnlForm);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 200);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new Padding(20);
            pnlMain.Size = new Size(1198, 698);
            pnlMain.TabIndex = 0;
            // 
            // dgvToppings
            // 
            dgvToppings.AllowUserToAddRows = false;
            dgvToppings.AllowUserToDeleteRows = false;
            dgvToppings.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvToppings.BackgroundColor = Color.White;
            dgvToppings.BorderStyle = BorderStyle.None;
            dgvToppings.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvToppings.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvToppings.ColumnHeadersHeight = 45;
            dgvToppings.Dock = DockStyle.Fill;
            dgvToppings.EnableHeadersVisualStyles = false;
            dgvToppings.GridColor = Color.FromArgb(226, 232, 240);
            dgvToppings.Location = new Point(20, 20);
            dgvToppings.MultiSelect = false;
            dgvToppings.Name = "dgvToppings";
            dgvToppings.RowHeadersVisible = false;
            dgvToppings.RowTemplate.Height = 50;
            dgvToppings.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvToppings.Size = new Size(1158, 378);
            dgvToppings.TabIndex = 0;
            dgvToppings.CellClick += dgvToppings_CellClick;
            dgvToppings.CellFormatting += dgvToppings_CellFormatting;
            dgvToppings.DataError += dgvToppings_DataError;
            // 
            // pnlForm
            // 
            pnlForm.BackColor = Color.White;
            pnlForm.Controls.Add(lblName);
            pnlForm.Controls.Add(txtName);
            pnlForm.Controls.Add(lblDescription);
            pnlForm.Controls.Add(txtDescription);
            pnlForm.Controls.Add(lblPrice);
            pnlForm.Controls.Add(numPrice);
            pnlForm.Controls.Add(lblIsAvailable);
            pnlForm.Controls.Add(chkIsAvailable);
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
            // lblName
            // 
            lblName.Font = new Font("Segoe UI", 11F);
            lblName.ForeColor = Color.FromArgb(45, 55, 72);
            lblName.Location = new Point(30, 30);
            lblName.Name = "lblName";
            lblName.Size = new Size(120, 30);
            lblName.TabIndex = 0;
            lblName.Text = "Tên topping:";
            lblName.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtName
            // 
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.Font = new Font("Segoe UI", 11F);
            txtName.Location = new Point(160, 30);
            txtName.Name = "txtName";
            txtName.Size = new Size(300, 27);
            txtName.TabIndex = 1;
            // 
            // lblDescription
            // 
            lblDescription.Font = new Font("Segoe UI", 11F);
            lblDescription.ForeColor = Color.FromArgb(45, 55, 72);
            lblDescription.Location = new Point(30, 80);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(120, 30);
            lblDescription.TabIndex = 2;
            lblDescription.Text = "Mô tả:";
            lblDescription.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtDescription
            // 
            txtDescription.BorderStyle = BorderStyle.FixedSingle;
            txtDescription.Font = new Font("Segoe UI", 11F);
            txtDescription.Location = new Point(160, 80);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(300, 27);
            txtDescription.TabIndex = 3;
            // 
            // lblPrice
            // 
            lblPrice.Font = new Font("Segoe UI", 11F);
            lblPrice.ForeColor = Color.FromArgb(45, 55, 72);
            lblPrice.Location = new Point(30, 130);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(120, 30);
            lblPrice.TabIndex = 4;
            lblPrice.Text = "Giá (VNĐ):";
            lblPrice.TextAlign = ContentAlignment.MiddleRight;
            // 
            // numPrice
            // 
            numPrice.Font = new Font("Segoe UI", 11F);
            numPrice.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
            numPrice.Location = new Point(160, 130);
            numPrice.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            numPrice.Name = "numPrice";
            numPrice.Size = new Size(300, 27);
            numPrice.TabIndex = 5;
            numPrice.ThousandsSeparator = true;
            // 
            // lblIsAvailable
            // 
            lblIsAvailable.Font = new Font("Segoe UI", 11F);
            lblIsAvailable.ForeColor = Color.FromArgb(45, 55, 72);
            lblIsAvailable.Location = new Point(520, 30);
            lblIsAvailable.Name = "lblIsAvailable";
            lblIsAvailable.Size = new Size(91, 30);
            lblIsAvailable.TabIndex = 6;
            lblIsAvailable.Text = "Trạng thái:";
            lblIsAvailable.TextAlign = ContentAlignment.MiddleRight;
            // 
            // chkIsAvailable
            // 
            chkIsAvailable.Checked = true;
            chkIsAvailable.CheckState = CheckState.Checked;
            chkIsAvailable.Font = new Font("Segoe UI", 11F);
            chkIsAvailable.ForeColor = Color.FromArgb(45, 55, 72);
            chkIsAvailable.Location = new Point(620, 35);
            chkIsAvailable.Name = "chkIsAvailable";
            chkIsAvailable.Size = new Size(100, 25);
            chkIsAvailable.TabIndex = 7;
            chkIsAvailable.Text = "Đang bán";
            // 
            // lblImageUrl
            // 
            lblImageUrl.Font = new Font("Segoe UI", 11F);
            lblImageUrl.ForeColor = Color.FromArgb(45, 55, 72);
            lblImageUrl.Location = new Point(30, 180);
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
            txtImageUrl.Location = new Point(160, 180);
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
            btnBrowseImage.Location = new Point(390, 180);
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
            picPreview.Location = new Point(538, 66);
            picPreview.Name = "picPreview";
            picPreview.Size = new Size(200, 160);
            picPreview.SizeMode = PictureBoxSizeMode.Zoom;
            picPreview.TabIndex = 11;
            picPreview.TabStop = false;
            // 
            // frmToppings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(247, 249, 252);
            ClientSize = new Size(1198, 898);
            Controls.Add(pnlMain);
            Controls.Add(pnlSearch);
            Controls.Add(pnlToolbar);
            Controls.Add(pnlHeader);
            Name = "frmToppings";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "🍮 Quản lý Topping - MilkTea POS";
            WindowState = FormWindowState.Maximized;
            pnlHeader.ResumeLayout(false);
            pnlToolbar.ResumeLayout(false);
            pnlSearch.ResumeLayout(false);
            pnlSearch.PerformLayout();
            pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvToppings).EndInit();
            pnlForm.ResumeLayout(false);
            pnlForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numPrice).EndInit();
            ((System.ComponentModel.ISupportInitialize)picPreview).EndInit();
            ResumeLayout(false);
        }
    }
}
