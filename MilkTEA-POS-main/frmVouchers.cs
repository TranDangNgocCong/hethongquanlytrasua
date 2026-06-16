using Microsoft.EntityFrameworkCore;
using MilkTeaPOS.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MilkTeaPOS
{
    public partial class frmVouchers : Form
    {
        private Voucher _selectedVoucher;
        private CheckedListBox _clbTiers;

        #region Constants

        private static readonly string[] VOUCHER_TYPES = { "percentage", "fixed_amount", "free_item", "buy_one_get_one" };
        private static readonly string[] STATUSES = { "active", "inactive", "expired", "used_up" };
        private static readonly string[] FILTER_STATUSES = { "Tất cả", "Hoạt động", "Hết hạn", "Đã dùng hết", "Không hoạt động" };
        private static readonly string[] TIERS = { "none", "silver", "gold", "platinum", "diamond" };

        #endregion

        #region Constructor & Initialization

        public frmVouchers()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            InitializeComboBoxes();
            InitializeTierCheckList();
            LoadVoucherData();
        }

        private void InitializeTierCheckList()
        {
            _clbTiers = new CheckedListBox
            {
                Name = "clbTiers",
                CheckOnClick = true,
                Font = new Font("Segoe UI", 9F),
                Size = new Size(200, 90),
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };
            foreach (var tier in TIERS)
            {
                _clbTiers.Items.Add(tier.ToUpperInvariant());
            }
            for (int i = 0; i < _clbTiers.Items.Count; i++)
                _clbTiers.SetItemChecked(i, true);

            // Add label
            var lblTiers = new Label
            {
                Text = "Hạng áp dụng:",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(45, 55, 72),
                AutoSize = true,
                Name = "lblTiers"
            };

            // Find pnlFormFields or pnlRight to add controls to
            // pnlFormFields is inside pnlRight, which contains all the form fields
            var formFields = this.Controls.Find("pnlFormFields", true).FirstOrDefault() as Panel;
            if (formFields != null)
            {
                // Position after cbStatus
                var statusLabel = formFields.Controls.Find("lblStatus", true).FirstOrDefault();
                if (statusLabel != null)
                {
                    lblTiers.Location = new Point(statusLabel.Left, statusLabel.Bottom + 35);
                    _clbTiers.Location = new Point(statusLabel.Left, lblTiers.Bottom + 4);
                }
                else
                {
                    lblTiers.Location = new Point(20, 420);
                    _clbTiers.Location = new Point(20, lblTiers.Bottom + 4);
                }
                formFields.Controls.Add(lblTiers);
                formFields.Controls.Add(_clbTiers);
                formFields.Height = Math.Max(formFields.Height, _clbTiers.Bottom + 30);
            }
        }

        private void InitializeComboBoxes()
        {
            cbVoucherType.Items.AddRange(VOUCHER_TYPES);
            cbVoucherType.SelectedIndex = 0;

            cbStatus.Items.AddRange(STATUSES);
            cbStatus.SelectedIndex = 0;

            cbFilterStatus.Items.AddRange(FILTER_STATUSES);
            cbFilterStatus.SelectedIndex = 0;
        }

        #endregion

        #region Data Loading & Display

        private async void LoadVoucherData()
        {
            try
            {
                ShowLoading(true);

                using var context = new PostgresContext();
                var vouchers = await context.Vouchers
                    .AsNoTracking()
                    .OrderByDescending(v => v.CreatedAt)
                    .ToListAsync();

                UpdateVoucherStatistics(vouchers);
                LoadVouchersToGrid(vouchers);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu Voucher:\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ShowLoading(false);
            }
        }

        private void UpdateVoucherStatistics(List<Voucher> vouchers)
        {
            var now = DateTime.UtcNow;

            int total = vouchers.Count;
            int active = vouchers.Count(v => v.status == "active" && (v.ValidUntil == null || v.ValidUntil >= now));
            int expired = vouchers.Count(v => v.status == "expired" || (v.ValidUntil != null && v.ValidUntil < now));
            int usedUp = vouchers.Count(v => v.status == "used_up" || (v.UsageLimit.HasValue && v.UsageCount.HasValue && v.UsageCount >= v.UsageLimit));
            int inactive = vouchers.Count(v => v.status == "inactive");

            lblTotalValue.Text = total.ToString();
            lblActiveValue.Text = active.ToString();
            lblExpiredValue.Text = expired.ToString();
            lblUsedUpValue.Text = usedUp.ToString();
        }

        private void LoadVouchersToGrid(List<Voucher> vouchers = null)
        {
            try
            {
                if (vouchers == null)
                {
                    using var context = new PostgresContext();
                    vouchers = context.Vouchers
                        .AsNoTracking()
                        .OrderByDescending(v => v.CreatedAt)
                        .ToList();
                }

                var searchText = txtSearch.Text.Trim().ToLower();
                var statusFilter = cbFilterStatus.SelectedItem?.ToString();

                if (statusFilter != "Tất cả")
                {
                    var now = DateTime.UtcNow;
                    vouchers = statusFilter switch
                    {
                        "Hoạt động" => vouchers.Where(v => v.status == "active" && (v.ValidUntil == null || v.ValidUntil >= now)).ToList(),
                        "Hết hạn" => vouchers.Where(v => v.status == "expired" || (v.ValidUntil != null && v.ValidUntil < now)).ToList(),
                        "Đã dùng hết" => vouchers.Where(v => v.status == "used_up" || (v.UsageLimit.HasValue && v.UsageCount.HasValue && v.UsageCount >= v.UsageLimit)).ToList(),
                        "Không hoạt động" => vouchers.Where(v => v.status == "inactive").ToList(),
                        _ => vouchers
                    };
                }

                if (!string.IsNullOrEmpty(searchText))
                {
                    vouchers = vouchers.Where(v =>
                        v.Code.ToLower().Contains(searchText) ||
                        v.Name.ToLower().Contains(searchText) ||
                        (v.Description != null && v.Description.ToLower().Contains(searchText))).ToList();
                }

                dgvVouchers.DataSource = null;
                dgvVouchers.Rows.Clear();
                dgvVouchers.Columns.Clear();

                var data = vouchers.Select(v => new
                {
                    v.Id,
                    Code = v.Code,
                    Name = v.Name,
                    Type = GetVoucherTypeDisplay(v.VoucherType),
                    Discount = v.VoucherType == "percentage" ? $"{v.DiscountValue}%" : $"{v.DiscountValue:N0}đ",
                    MinOrder = v.MinOrderAmount.HasValue ? $"{v.MinOrderAmount:N0}đ" : "Không yêu cầu",
                    MaxDiscount = v.MaxDiscountAmount.HasValue ? $"{v.MaxDiscountAmount:N0}đ" : "Không giới hạn",
                    Remaining = v.UsageLimit.HasValue ? $"{v.UsageLimit.Value - (v.UsageCount ?? 0)}" : "∞",
                    ValidFrom = v.ValidFrom?.ToLocalTime().ToString("dd/MM/yyyy") ?? "-",
                    ValidUntil = v.ValidUntil?.ToLocalTime().ToString("dd/MM/yyyy") ?? "∞",
                    Status = GetStatusDisplay(v.status)
                }).ToList();

                dgvVouchers.DataSource = data;
                CustomizeGridColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách Voucher:\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CustomizeGridColumns()
        {
            if (dgvVouchers.Columns.Count == 0) return;

            var columns = dgvVouchers.Columns;

            if (columns["Id"] != null) columns["Id"].Visible = false;
            if (columns["Code"] != null) { columns["Code"].HeaderText = "Mã"; columns["Code"].Width = 120; }
            if (columns["Name"] != null) { columns["Name"].HeaderText = "Tên voucher"; columns["Name"].Width = 180; }
            if (columns["Type"] != null) { columns["Type"].HeaderText = "Loại"; columns["Type"].Width = 120; }
            if (columns["Discount"] != null) { columns["Discount"].HeaderText = "Giá trị giảm"; columns["Discount"].Width = 110; }
            if (columns["MinOrder"] != null) { columns["MinOrder"].HeaderText = "Đơn tối thiểu"; columns["MinOrder"].Width = 120; }
            if (columns["MaxDiscount"] != null) { columns["MaxDiscount"].HeaderText = "Giảm tối đa"; columns["MaxDiscount"].Width = 120; }
            if (columns["Remaining"] != null) { columns["Remaining"].HeaderText = "Còn lại"; columns["Remaining"].Width = 90; }
            if (columns["ValidFrom"] != null) { columns["ValidFrom"].HeaderText = "Bắt đầu"; columns["ValidFrom"].Width = 110; }
            if (columns["ValidUntil"] != null) { columns["ValidUntil"].HeaderText = "Hết hạn"; columns["ValidUntil"].Width = 110; }
            if (columns["Status"] != null) { columns["Status"].HeaderText = "Trạng thái"; columns["Status"].Width = 130; }

            dgvVouchers.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvVouchers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvVouchers.RowTemplate.Height = 38;
        }

        private void dgvVouchers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var statusColIndex = -1;
            if (dgvVouchers.Columns["Status"] != null) statusColIndex = dgvVouchers.Columns["Status"].Index;

            // Color coding cho status column
            if (e.ColumnIndex == statusColIndex && e.Value != null)
            {
                var statusText = e.Value.ToString();
                e.CellStyle.BackColor = statusText switch
                {
                    "Hoạt động" => Color.FromArgb(212, 237, 218),
                    "Không hoạt động" => Color.FromArgb(226, 228, 230),
                    "Hết hạn" => Color.FromArgb(255, 243, 205),
                    "Đã dùng hết" => Color.FromArgb(206, 212, 218),
                    _ => Color.White
                };
                e.CellStyle.ForeColor = statusText switch
                {
                    "Hoạt động" => Color.FromArgb(21, 87, 36),
                    "Không hoạt động" => Color.FromArgb(108, 117, 125),
                    "Hết hạn" => Color.FromArgb(133, 100, 4),
                    "Đã dùng hết" => Color.FromArgb(73, 80, 87),
                    _ => Color.FromArgb(45, 55, 72)
                };
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            // Discount column - red color
            if (dgvVouchers.Columns["Discount"] != null && e.ColumnIndex == dgvVouchers.Columns["Discount"].Index)
            {
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                e.CellStyle.ForeColor = Color.FromArgb(231, 76, 60);
            }

            // MinOrder column
            if (dgvVouchers.Columns["MinOrder"] != null && e.ColumnIndex == dgvVouchers.Columns["MinOrder"].Index)
            {
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            // MaxDiscount column
            if (dgvVouchers.Columns["MaxDiscount"] != null && e.ColumnIndex == dgvVouchers.Columns["MaxDiscount"].Index)
            {
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            // Remaining column - highlight low stock
            if (dgvVouchers.Columns["Remaining"] != null && e.ColumnIndex == dgvVouchers.Columns["Remaining"].Index)
            {
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (e.Value != null && e.Value.ToString() != "∞")
                {
                    if (int.TryParse(e.Value.ToString(), out int remaining) && remaining <= 5 && remaining > 0)
                    {
                        e.CellStyle.ForeColor = Color.FromArgb(231, 76, 60);
                    }
                }
            }

            // Code column - blue color
            if (dgvVouchers.Columns["Code"] != null && e.ColumnIndex == dgvVouchers.Columns["Code"].Index)
            {
                e.CellStyle.ForeColor = Color.FromArgb(52, 152, 219);
            }
        }

        #endregion

        #region Form Data Management

        private void FillFormData()
        {
            if (_selectedVoucher == null) return;

            txtCode.Text = _selectedVoucher.Code;
            txtName.Text = _selectedVoucher.Name;
            txtDescription.Text = _selectedVoucher.Description;
            cbVoucherType.Text = _selectedVoucher.VoucherType;
            txtDiscountValue.Text = _selectedVoucher.DiscountValue.ToString();
            txtMinOrder.Text = _selectedVoucher.MinOrderAmount?.ToString() ?? "";
            txtMaxDiscount.Text = _selectedVoucher.MaxDiscountAmount?.ToString() ?? "";
            txtUsageLimit.Text = _selectedVoucher.UsageLimit?.ToString() ?? "";

            if (_selectedVoucher.ValidFrom.HasValue) dtpValidFrom.Value = _selectedVoucher.ValidFrom.Value.ToLocalTime();
            if (_selectedVoucher.ValidUntil.HasValue) dtpValidUntil.Value = _selectedVoucher.ValidUntil.Value.ToLocalTime();

            var statusToSelect = _selectedVoucher.status?.ToLower() ?? "active";
            cbStatus.SelectedItem = STATUSES.Contains(statusToSelect) ? statusToSelect : "active";

            // Load applicable tiers
            LoadApplicableTiersToCheckList();
        }

        private void LoadApplicableTiersToCheckList()
        {
            if (_clbTiers == null || _selectedVoucher == null) return;

            // Parse applicable_tiers from DB - raw SQL since EF doesn't map it well
            var applicableTiers = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "none", "silver", "gold", "platinum", "diamond" }; // default: all

            try
            {
                using var context = new PostgresContext();
                var conn = context.Database.GetDbConnection();
                if (conn.State != System.Data.ConnectionState.Open) conn.Open();
                using var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT applicable_tiers::text FROM vouchers WHERE id = @id";
                var p = cmd.CreateParameter();
                p.ParameterName = "@id";
                p.Value = _selectedVoucher.Id;
                cmd.Parameters.Add(p);
                var result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    var tiersStr = result.ToString()?.Trim('{', '}') ?? "";
                    if (!string.IsNullOrEmpty(tiersStr))
                    {
                        applicableTiers = new HashSet<string>(tiersStr.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries), StringComparer.OrdinalIgnoreCase);
                    }
                }
            }
            catch { }

            for (int i = 0; i < _clbTiers.Items.Count && i < TIERS.Length; i++)
            {
                _clbTiers.SetItemChecked(i, applicableTiers.Contains(TIERS[i]));
            }
        }

        private void ClearForm()
        {
            txtCode.Clear(); txtName.Clear(); txtDescription.Clear(); txtDiscountValue.Clear();
            txtMinOrder.Clear(); txtMaxDiscount.Clear(); txtUsageLimit.Clear();
            dtpValidFrom.Value = DateTime.Now; dtpValidUntil.Value = DateTime.Now.AddYears(1);
            cbVoucherType.SelectedIndex = 0; cbStatus.SelectedIndex = 0; _selectedVoucher = null;

            // Reset tiers to all checked
            if (_clbTiers != null)
            {
                for (int i = 0; i < _clbTiers.Items.Count; i++)
                    _clbTiers.SetItemChecked(i, true);
            }

            txtCode.Focus();
        }

        #endregion

        #region Event Handlers

        private void dgvVouchers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvVouchers.Rows[e.RowIndex];
            if (row.Cells["Id"].Value == null) return;
            var id = Guid.Parse(row.Cells["Id"].Value.ToString());
            using var context = new PostgresContext();
            _selectedVoucher = context.Vouchers.AsNoTracking().FirstOrDefault(v => v.Id == id);
            if (_selectedVoucher != null) FillFormData();
        }

        private void dgvVouchers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            dgvVouchers_CellClick(sender, e);
        }

        #endregion

        #region Keyboard Navigation

        private void txtCode_KeyPress(object sender, KeyPressEventArgs e) { if (e.KeyChar == (char)Keys.Enter) { e.Handled = true; txtName.Focus(); } }
        private void txtName_KeyPress(object sender, KeyPressEventArgs e) { if (e.KeyChar == (char)Keys.Enter) { e.Handled = true; txtDescription.Focus(); } }
        private void txtDescription_KeyPress(object sender, KeyPressEventArgs e) { if (e.KeyChar == (char)Keys.Enter) { e.Handled = true; cbVoucherType.Focus(); } }
        private void cbVoucherType_KeyPress(object sender, KeyPressEventArgs e) { if (e.KeyChar == (char)Keys.Enter) { e.Handled = true; txtDiscountValue.Focus(); } }
        private void txtDiscountValue_KeyPress(object sender, KeyPressEventArgs e) { if (e.KeyChar == (char)Keys.Enter) { e.Handled = true; txtMinOrder.Focus(); } if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true; }
        private void txtMinOrder_KeyPress(object sender, KeyPressEventArgs e) { if (e.KeyChar == (char)Keys.Enter) { e.Handled = true; txtMaxDiscount.Focus(); } if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true; }
        private void txtMaxDiscount_KeyPress(object sender, KeyPressEventArgs e) { if (e.KeyChar == (char)Keys.Enter) { e.Handled = true; txtUsageLimit.Focus(); } if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true; }
        private void txtUsageLimit_KeyPress(object sender, KeyPressEventArgs e) { if (e.KeyChar == (char)Keys.Enter) { e.Handled = true; dtpValidFrom.Focus(); } if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true; }
        private void dtpValidFrom_KeyPress(object sender, KeyPressEventArgs e) { if (e.KeyChar == (char)Keys.Enter) { e.Handled = true; dtpValidUntil.Focus(); } }
        private void dtpValidUntil_KeyPress(object sender, KeyPressEventArgs e) { if (e.KeyChar == (char)Keys.Enter) { e.Handled = true; cbStatus.Focus(); } }
        private void cbStatus_KeyPress(object sender, KeyPressEventArgs e) { if (e.KeyChar == (char)Keys.Enter) { e.Handled = true; if (_selectedVoucher != null) btnEdit_Click(sender, e); else btnAdd_Click(sender, e); } }

        #endregion

        #region Toolbar Actions

        private async void btnAdd_Click(object sender, EventArgs e) => await SaveVoucher(isEdit: false);
        private async void btnEdit_Click(object sender, EventArgs e) { if (_selectedVoucher == null) { MessageBox.Show("Vui lòng chọn voucher cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; } await SaveVoucher(isEdit: true); }
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedVoucher == null) { MessageBox.Show("Vui lòng chọn voucher cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            var result = MessageBox.Show($"Bạn có chắc muốn xóa Voucher '{_selectedVoucher.Code}'?\n\nHành động này không thể hoàn tác!", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes) return;
            try
            {
                using var context = new PostgresContext();
                var voucher = await context.Vouchers.FindAsync(_selectedVoucher.Id);
                if (voucher != null) { context.Vouchers.Remove(voucher); await context.SaveChangesAsync(); }
                
                // Log Audit
                LogAudit("DELETE", _selectedVoucher.Id, $"Code: {_selectedVoucher.Code}");
                
                MessageBox.Show("Xóa Voucher thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadVoucherData(); ClearForm();
            }
            catch (DbUpdateException dbEx) { MessageBox.Show($"Lỗi khi xóa:\n\n{dbEx.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception ex) { MessageBox.Show($"Lỗi khi xóa:\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void btnRefresh_Click(object sender, EventArgs e) 
        { 
            cbFilterStatus.SelectedIndex = 0; // Reset filter về "Tất cả"
            LoadVoucherData(); 
            ClearForm(); 
        }
        private void btnFilter_Click(object sender, EventArgs e) => LoadVouchersToGrid();
        private void cbFilterStatus_SelectedIndexChanged(object sender, EventArgs e) => LoadVouchersToGrid();

        #endregion

        #region Search Functionality

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e) { if (e.KeyChar == (char)Keys.Enter) { e.Handled = true; LoadVouchersToGrid(); } }

        #endregion

        #region Database Operations

        private DateTime _lastAuditTime = DateTime.MinValue;
        private readonly object _auditLock = new object();

        private void LogAudit(string action, Guid recordId, string details)
        {
            lock (_auditLock)
            {
                if ((DateTime.UtcNow - _lastAuditTime).TotalMilliseconds < 500) return;
                _lastAuditTime = DateTime.UtcNow;
            }

            try
            {
                var userId = PostgresContext.CurrentUserId;
                if (!userId.HasValue) return;

                var escapedDetails = details.Replace("\"", "\\\"");
                var jsonContent = $"{{\"details\": \"{escapedDetails}\"}}";

                using var context = new PostgresContext();
                context.AuditLogs.Add(new AuditLog
                {
                    Id = Guid.NewGuid(),
                    UserId = userId.Value,
                    Action = action,
                    TableName = "vouchers",
                    RecordId = recordId,
                    NewValues = jsonContent,
                    IpAddress = PostgresContext.CurrentUserIP,
                    CreatedAt = DateTime.UtcNow
                });
                context.SaveChanges();
            }
            catch { }
        }

        private async Task SaveVoucher(bool isEdit)
        {
            if (!ValidateInputs(out string errorMessage)) { MessageBox.Show(errorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            var code = txtCode.Text.Trim().ToUpper();
            var name = txtName.Text.Trim();
            var description = string.IsNullOrWhiteSpace(txtDescription.Text) ? null : txtDescription.Text.Trim();
            var voucherType = cbVoucherType.Text;
            var status = cbStatus.Text;

            if (!decimal.TryParse(txtDiscountValue.Text.Trim(), out decimal discountVal) || discountVal < 0) { MessageBox.Show("Giá trị giảm giá không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            decimal? minOrderAmount = decimal.TryParse(txtMinOrder.Text.Trim(), out decimal mo) && mo > 0 ? mo : null;
            decimal? maxDiscount = decimal.TryParse(txtMaxDiscount.Text.Trim(), out decimal md) && md > 0 ? md : null;
            int? usageLimit = int.TryParse(txtUsageLimit.Text.Trim(), out int ul) && ul > 0 ? ul : (int?)null;

            DateTime validFrom = dtpValidFrom.Value.ToUniversalTime();
            DateTime validUntil = dtpValidUntil.Value.ToUniversalTime();

            if (validUntil <= validFrom) { MessageBox.Show("Ngày hết hạn phải sau ngày bắt đầu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            // Build applicable_tiers array from checked items
            var selectedTiers = new List<string>();
            if (_clbTiers != null)
            {
                for (int i = 0; i < _clbTiers.Items.Count && i < TIERS.Length; i++)
                {
                    if (_clbTiers.GetItemChecked(i))
                        selectedTiers.Add(TIERS[i]);
                }
            }
            if (selectedTiers.Count == 0) selectedTiers = TIERS.ToList(); // fallback: all tiers
            var tiersArray = "{" + string.Join(",", selectedTiers) + "}";

            try
            {
                using var context = new PostgresContext();

                if (!isEdit)
                {
                    bool isExist = await context.Vouchers.AsNoTracking().AnyAsync(v => v.Code == code);
                    if (isExist) { MessageBox.Show($"Mã Voucher '{code}' đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                    var id = Guid.NewGuid();
                    var currentUserId = PostgresContext.CurrentUserId;

                    await context.Database.ExecuteSqlInterpolatedAsync($@"
                        INSERT INTO vouchers (id, code, name, description, voucher_type, discount_value,
                            min_order_amount, max_discount_amount, usage_limit, usage_count,
                            status, valid_from, valid_until, applicable_tiers, created_at, updated_at, created_by)
                        VALUES ({id}, {code}, {name}, {description}, {voucherType}::voucher_type, {discountVal},
                            {minOrderAmount}, {maxDiscount}, {usageLimit}, 0,
                            {status}::voucher_status, {validFrom}, {validUntil}, {tiersArray}::membership_tier[], {DateTime.UtcNow}, {DateTime.UtcNow},
                            {currentUserId})");

                    // Log Audit
                    LogAudit("INSERT", id, $"Code: {code}, Name: {name}, Value: {discountVal}");
                    
                    MessageBox.Show("Thêm Voucher thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    var existingVoucher = await context.Vouchers.AsNoTracking().FirstOrDefaultAsync(v => v.Id == _selectedVoucher.Id);
                    if (existingVoucher == null) { MessageBox.Show("Không tìm thấy voucher!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                    
                    bool isExist = await context.Vouchers.AsNoTracking().AnyAsync(v => v.Code == code && v.Id != _selectedVoucher.Id);
                    if (isExist) { MessageBox.Show($"Mã Voucher '{code}' đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                    
                    await context.Database.ExecuteSqlInterpolatedAsync($@"
                        UPDATE vouchers SET
                            code = {code}, name = {name}, description = {description},
                            voucher_type = {voucherType}::voucher_type,
                            discount_value = {discountVal},
                            min_order_amount = {minOrderAmount},
                            max_discount_amount = {maxDiscount},
                            usage_limit = {usageLimit},
                            status = {status}::voucher_status,
                            valid_from = {validFrom},
                            valid_until = {validUntil},
                            applicable_tiers = {tiersArray}::membership_tier[],
                            updated_at = {DateTime.UtcNow}
                        WHERE id = {_selectedVoucher.Id}");
                    
                    // Log Audit
                    LogAudit("UPDATE", _selectedVoucher.Id, $"Code: {code}, Name: {name}, Value: {discountVal}");
                    
                    MessageBox.Show("Sửa Voucher thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                LoadVoucherData(); ClearForm();
            }
            catch (Exception ex) { MessageBox.Show($"Lỗi:\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        #endregion

        #region Validation Helpers

        private bool ValidateInputs(out string errorMessage)
        {
            errorMessage = string.Empty;
            var code = txtCode.Text.Trim(); var name = txtName.Text.Trim();
            if (string.IsNullOrEmpty(code)) { errorMessage = "Vui lòng nhập mã voucher!"; txtCode.Focus(); return false; }
            if (!Regex.IsMatch(code, @"^[A-Z0-9_]+$")) { errorMessage = "Mã voucher chỉ được chứa chữ HOA, số và dấu gạch dưới!"; txtCode.Focus(); return false; }
            if (string.IsNullOrEmpty(name)) { errorMessage = "Vui lòng nhập tên voucher!"; txtName.Focus(); return false; }
            if (string.IsNullOrEmpty(txtDiscountValue.Text.Trim())) { errorMessage = "Vui lòng nhập giá trị giảm giá!"; txtDiscountValue.Focus(); return false; }
            if (!decimal.TryParse(txtDiscountValue.Text.Trim(), out decimal discountVal) || discountVal < 0) { errorMessage = "Giá trị giảm giá không hợp lệ!"; txtDiscountValue.Focus(); return false; }
            if (cbVoucherType.Text == "percentage" && (discountVal < 0 || discountVal > 100)) { errorMessage = "Giá trị giảm giá phải từ 0-100 cho loại phần trăm!"; txtDiscountValue.Focus(); return false; }
            return true;
        }

        #endregion

        #region Display Helpers

        private string GetVoucherTypeDisplay(string voucherType) => voucherType switch { "percentage" => "% Phần trăm", "fixed_amount" => "VNĐ Cố định", "free_item" => "Free item", "buy_one_get_one" => "Mua 1 tặng 1", _ => voucherType };
        private string GetStatusDisplay(string status) => status switch { "active" => "Hoạt động", "inactive" => "Không hoạt động", "expired" => "Hết hạn", "used_up" => "Đã dùng hết", _ => status };
        private string GetVoucherTypeKey(string displayType) => displayType switch { "% Phần trăm" => "percentage", "VNĐ Cố định" => "fixed_amount", "Free item" => "free_item", "Mua 1 tặng 1" => "buy_one_get_one", _ => "percentage" };
        private string GetStatusKey(string displayStatus) => displayStatus switch { "Hoạt động" => "active", "Không hoạt động" => "inactive", "Hết hạn" => "expired", "Đã dùng hết" => "used_up", _ => "active" };

        #endregion

        #region Loading Indicator

        private void ShowLoading(bool show) { if (show) { this.Cursor = Cursors.WaitCursor; pnlMain.Enabled = false; } else { this.Cursor = Cursors.Default; pnlMain.Enabled = true; } }

        #endregion

        #region Form Events

        private void frmVouchers_Load(object sender, EventArgs e) => LoadVoucherData();
        protected override void OnFormClosing(FormClosingEventArgs e) => base.OnFormClosing(e);

        #endregion
    }
}
