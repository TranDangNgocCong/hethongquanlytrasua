using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using MilkTeaPOS.Models;

namespace MilkTeaPOS
{
    public partial class frmMemberships : Form
    {
        private Membership? _selectedMembership;

        #region Constants

        private static readonly string[] TIERS = { "none", "silver", "gold", "platinum", "diamond" };

        // Cached fonts to avoid GDI leaks
        private readonly Font _fontTierNone = new Font("Segoe UI", 10F);
        private readonly Font _fontTierSilver = new Font("Segoe UI", 10F, FontStyle.Bold);
        private readonly Font _fontTierGold = new Font("Segoe UI", 10F, FontStyle.Bold);
        private readonly Font _fontTierPlatinum = new Font("Segoe UI", 10F, FontStyle.Bold);
        private readonly Font _fontTierDiamond = new Font("Segoe UI", 10F, FontStyle.Bold);

        #endregion

        #region Constructor & Initialization

        public frmMemberships()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            InitializeTierCombo();
            InitializeTierFilterCombo();
            LoadCustomers();
            LoadMemberships();
        }

        private void InitializeTierCombo()
        {
            cbTier.Items.Clear();
            cbTier.Items.AddRange(TIERS);
            cbTier.SelectedIndex = 0;
            cbTier.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void InitializeTierFilterCombo()
        {
            cbTierFilter.Items.Clear();
            cbTierFilter.Items.Add("Tất cả");
            cbTierFilter.Items.AddRange(TIERS);
            cbTierFilter.SelectedIndex = 0;
            cbTierFilter.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        #endregion

        #region Data Loading & Display

        private async void LoadMemberships()
        {
            try
            {
                ShowLoading(true);

                using var context = new PostgresContext();

                var memberships = await context.Memberships
                    .AsNoTracking()
                    .Include(m => m.Customer)
                    .OrderByDescending(m => m.TotalSpent)
                    .ToListAsync();

                dgvMemberships.DataSource = memberships.Select(m => new
                {
                    m.Id,
                    CustomerName = m.Customer?.Name ?? "N/A",
                    CustomerPhone = m.Customer?.Phone ?? "-",
                    Tier = m.Tier.ToString(),
                    m.Points,
                    m.TotalSpent,
                    m.TotalOrders,
                    m.JoinedAt,
                    ExpiresAt = m.ExpiresAt ?? (m.JoinedAt.HasValue ? m.JoinedAt.Value.AddMonths(6) : (DateTime?)null),
                    m.LastOrderAt,
                    m.UpdatedAt
                }).ToList();

                CustomizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi tải dữ liệu:\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ShowLoading(false);
            }
        }

        private void CustomizeColumns()
        {
            if (dgvMemberships.Columns.Count == 0) return;

            var columns = dgvMemberships.Columns;

            if (columns["Id"] != null) columns["Id"].Visible = false;
            if (columns["CustomerName"] != null) { columns["CustomerName"].HeaderText = "Khách hàng"; columns["CustomerName"].Width = 180; }
            if (columns["CustomerPhone"] != null) { columns["CustomerPhone"].HeaderText = "SĐT"; columns["CustomerPhone"].Width = 120; }
            if (columns["Tier"] != null) { columns["Tier"].HeaderText = "Hạng"; columns["Tier"].Width = 120; columns["Tier"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; }
            if (columns["Points"] != null) { columns["Points"].HeaderText = "Điểm"; columns["Points"].Width = 80; columns["Points"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; }
            if (columns["TotalSpent"] != null) { columns["TotalSpent"].HeaderText = "Tổng chi tiêu"; columns["TotalSpent"].Width = 140; columns["TotalSpent"].DefaultCellStyle.Format = "N0"; columns["TotalSpent"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; }
            if (columns["TotalOrders"] != null) { columns["TotalOrders"].HeaderText = "Tổng đơn"; columns["TotalOrders"].Width = 90; columns["TotalOrders"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; }
            if (columns["JoinedAt"] != null) { columns["JoinedAt"].HeaderText = "Ngày tham gia"; columns["JoinedAt"].Width = 130; columns["JoinedAt"].DefaultCellStyle.Format = "dd/MM/yyyy"; }
            if (columns["ExpiresAt"] != null) { columns["ExpiresAt"].HeaderText = "Ngày hết hạn"; columns["ExpiresAt"].Width = 130; columns["ExpiresAt"].DefaultCellStyle.Format = "dd/MM/yyyy"; }
            if (columns["LastOrderAt"] != null) { columns["LastOrderAt"].HeaderText = "Đơn cuối"; columns["LastOrderAt"].Width = 130; columns["LastOrderAt"].DefaultCellStyle.Format = "dd/MM/yyyy"; }
            if (columns["UpdatedAt"] != null) columns["UpdatedAt"].Visible = false;
        }

        private void dgvMemberships_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvMemberships.Columns["Tier"] == null || e.ColumnIndex != dgvMemberships.Columns["Tier"].Index || e.Value == null)
                return;

            var tier = e.Value.ToString().ToLower();
            e.CellStyle.ForeColor = tier switch
            {
                "none" => Color.Gray,
                "silver" => Color.FromArgb(192, 192, 192),
                "gold" => Color.FromArgb(255, 193, 7),
                "platinum" => Color.FromArgb(108, 117, 125),
                "diamond" => Color.FromArgb(255, 107, 107),
                _ => Color.Gray
            };

            e.CellStyle.Font = tier switch
            {
                "none" => _fontTierNone,
                "silver" => _fontTierSilver,
                "gold" => _fontTierGold,
                "platinum" => _fontTierPlatinum,
                "diamond" => _fontTierDiamond,
                _ => _fontTierNone
            };
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _fontTierNone?.Dispose();
            _fontTierSilver?.Dispose();
            _fontTierGold?.Dispose();
            _fontTierPlatinum?.Dispose();
            _fontTierDiamond?.Dispose();
            base.OnFormClosing(e);
        }

        #endregion

        #region Event Handlers - DataGridView

        private void dgvMemberships_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvMemberships.Rows[e.RowIndex];
            if (row.Cells["Id"].Value == null) return;

            if (!Guid.TryParse(row.Cells["Id"].Value.ToString(), out var membershipId)) return;

            // Load membership from DB to get correct CustomerId & Tier
            using var context = new PostgresContext();
            _selectedMembership = context.Memberships
                .AsNoTracking()
                .FirstOrDefault(m => m.Id == membershipId);

            if (_selectedMembership != null)
                FillFormData();
        }

        #endregion

        #region Form Data Management

        private void FillFormData()
        {
            if (_selectedMembership == null) return;

            // Load customer info
            using var context = new PostgresContext();
            var customer = context.Customers.Find(_selectedMembership.CustomerId);
            if (customer != null)
            {
                txtPhone.Text = customer.Phone;
                cbCustomer.SelectedValue = customer.Id;
            }

            cbTier.Text = _selectedMembership.Tier ?? "none";
            txtPoints.Text = _selectedMembership.Points.ToString();
            txtTotalSpent.Text = _selectedMembership.TotalSpent.HasValue ? _selectedMembership.TotalSpent.Value.ToString("#,##0") : "0";
            txtTotalOrders.Text = _selectedMembership.TotalOrders.ToString();

            if (_selectedMembership.JoinedAt.HasValue)
                dtpJoinedAt.Value = _selectedMembership.JoinedAt.Value.ToLocalTime();

            if (_selectedMembership.ExpiresAt.HasValue)
                dtpExpiresAt.Value = _selectedMembership.ExpiresAt.Value.ToLocalTime();
            else
                dtpExpiresAt.Checked = false;

            if (_selectedMembership.LastOrderAt.HasValue)
            {
                dtpLastOrder.Checked = true;
                dtpLastOrder.Value = _selectedMembership.LastOrderAt.Value.ToLocalTime();
            }
            else
            {
                dtpLastOrder.Checked = false;
            }
        }

        private void ClearForm()
        {
            _selectedMembership = null;
            txtPhone.Clear();
            cbCustomer.SelectedIndex = -1;
            cbTier.SelectedIndex = 0;
            txtPoints.Clear();
            txtTotalSpent.Clear();
            txtTotalOrders.Clear();
            dtpJoinedAt.Value = DateTime.Now;
            dtpExpiresAt.Value = DateTime.Now.AddMonths(6);
            dtpLastOrder.Checked = false;
            cbTierFilter.SelectedIndex = 0; // Reset filter về "Tất cả"
        }

        #endregion

        #region Customer Loading

        private async void LoadCustomers()
        {
            try
            {
                using var context = new PostgresContext();

                var customers = await context.Customers
                    .AsNoTracking()
                    .Where(c => c.Phone != null) // Chỉ lấy khách có SĐT
                    .OrderBy(c => c.Name)
                    .ToListAsync();

                cbCustomer.DataSource = null;
                cbCustomer.Items.Clear();
                cbCustomer.DataSource = customers;
                cbCustomer.DisplayMember = "Name";
                cbCustomer.ValueMember = "Id";
                cbCustomer.SelectedIndex = -1;
            }
            catch
            {
                // Silently fail
            }
        }

        private Guid? GetCustomerIdFromPhone(string phone)
        {
            if (string.IsNullOrEmpty(phone)) return null;

            using var context = new PostgresContext();
            var customer = context.Customers
                .AsNoTracking()
                .FirstOrDefault(c => c.Phone == phone);

            return customer?.Id;
        }

        private void cbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Tự động điền SĐT khi chọn khách hàng
            if (cbCustomer.SelectedItem is Customer selectedCustomer)
            {
                txtPhone.Text = selectedCustomer.Phone ?? string.Empty;
            }
        }

        #endregion

        #region Search Functionality

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                PerformSearch();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private void cbTierFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private async void PerformSearch()
        {
            var searchText = txtSearch.Text.Trim().ToLower();
            var tierFilter = cbTierFilter.SelectedItem?.ToString();

            try
            {
                ShowLoading(true);

                using var context = new PostgresContext();

                // 1. Load data từ DB (không filter tier ở đây để tránh lỗi PG enum)
                var query = context.Memberships
                    .AsNoTracking()
                    .Include(m => m.Customer);

                var memberships = await query.ToListAsync();

                // 2. Filter Tier ở bộ nhớ (C# string vs string - an toàn)
                if (tierFilter != "Tất cả")
                {
                    var tier = tierFilter?.ToLower();
                    memberships = memberships.Where(m => m.Tier?.ToLower() == tier).ToList();
                }

                // 3. Filter Search Text
                if (!string.IsNullOrEmpty(searchText))
                {
                    memberships = memberships.Where(m =>
                        (m.Customer?.Name?.ToLower().Contains(searchText) ?? false) ||
                        (m.Customer?.Phone?.Contains(searchText) ?? false) ||
                        (m.Tier?.ToLower().Contains(searchText) ?? false)).ToList();
                }

                // 4. Sắp xếp
                memberships = memberships.OrderByDescending(m => m.TotalSpent).ToList();

                dgvMemberships.DataSource = memberships.Select(m => new
                {
                    m.Id,
                    CustomerName = m.Customer?.Name ?? "N/A",
                    CustomerPhone = m.Customer?.Phone ?? "-",
                    Tier = m.Tier ?? "none",
                    m.Points,
                    m.TotalSpent,
                    m.TotalOrders,
                    m.JoinedAt,
                    ExpiresAt = m.ExpiresAt ?? (m.JoinedAt.HasValue ? m.JoinedAt.Value.AddMonths(6) : (DateTime?)null),
                    m.LastOrderAt,
                    m.UpdatedAt
                }).ToList();

                CustomizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi tìm kiếm:\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ShowLoading(false);
            }
        }

        #endregion

        #region Toolbar Actions

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            await SaveMembership();
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            if (_selectedMembership == null)
            {
                MessageBox.Show("⚠️ Vui lòng chọn hội viên cần sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            await UpdateMembership();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedMembership == null)
            {
                MessageBox.Show("⚠️ Vui lòng chọn hội viên cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var customerName = cbCustomer.Text;
            var result = MessageBox.Show(
                $"🗑️ Bạn có chắc muốn xóa thẻ hội viên của '{customerName}'?\n\n⚠️ Hành động này không thể hoàn tác!",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes) return;

            try
            {
                using var context = new PostgresContext();
                var membership = await context.Memberships.FindAsync(_selectedMembership.Id);
                if (membership != null)
                {
                    context.Memberships.Remove(membership);
                    await context.SaveChangesAsync();

                    LogAudit("DELETE", _selectedMembership.Id, $"CustomerId: {_selectedMembership.CustomerId}");
                }

                MessageBox.Show("✅ Xóa thẻ hội viên thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadMemberships();
                ClearForm();
            }
            catch (DbUpdateException dbEx)
            {
                string errorMsg = $"❌ Lỗi khi xóa (ràng buộc dữ liệu):\n\n{dbEx.Message}";
                if (dbEx.InnerException != null)
                {
                    errorMsg += $"\n\n📋 Chi tiết lỗi:\n{dbEx.InnerException.Message}";
                }
                MessageBox.Show(errorMsg, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                string errorMsg = $"❌ Lỗi khi xóa:\n{ex.Message}";
                if (ex.InnerException != null)
                {
                    errorMsg += $"\n\n📋 Chi tiết lỗi:\n{ex.InnerException.Message}";
                }
                MessageBox.Show(errorMsg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadMemberships();
            ClearForm();
        }

        #endregion

        #region Database Operations

        private async Task SaveMembership()
        {
            var phone = txtPhone.Text.Trim();
            var tier = cbTier.Text.Trim().ToLower();

            // Validate phone
            if (string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("⚠️ Vui lòng nhập số điện thoại khách hàng!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return;
            }

            // Validate phone format
            if (!Regex.IsMatch(phone, @"^[0-9]{9,11}$"))
            {
                MessageBox.Show("⚠️ Số điện thoại không hợp lệ!\n\nChỉ chấp nhận 9-11 chữ số.", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                txtPhone.SelectAll();
                return;
            }

            // Check if customer exists
            var customerId = GetCustomerIdFromPhone(phone);
            if (!customerId.HasValue)
            {
                MessageBox.Show($"⚠️ Không tìm thấy khách hàng với SĐT '{phone}'!\n\nVui lòng tạo khách hàng trước.", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                txtPhone.SelectAll();
                return;
            }

            // Check if membership already exists
            using (var context = new PostgresContext())
            {
                bool exists = await context.Memberships
                    .AsNoTracking()
                    .AnyAsync(m => m.CustomerId == customerId.Value);

                if (exists)
                {
                    MessageBox.Show($"⚠️ Khách hàng này đã có thẻ hội viên!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Validate numeric fields
            if (!int.TryParse(txtPoints.Text.Trim(), out int points) || points < 0)
            {
                MessageBox.Show("⚠️ Điểm phải là số dương!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPoints.Focus();
                txtPoints.SelectAll();
                return;
            }

            if (!decimal.TryParse(txtTotalSpent.Text.Trim(), out decimal totalSpent) || totalSpent < 0)
            {
                MessageBox.Show("⚠️ Tổng chi tiêu phải là số dương!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTotalSpent.Focus();
                txtTotalSpent.SelectAll();
                return;
            }

            if (!int.TryParse(txtTotalOrders.Text.Trim(), out int totalOrders) || totalOrders < 0)
            {
                MessageBox.Show("⚠️ Số đơn phải là số dương!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTotalOrders.Focus();
                txtTotalOrders.SelectAll();
                return;
            }

            // Validate dates
            var joinedAt = dtpJoinedAt.Value.ToUniversalTime();
            var expiresAt = dtpExpiresAt.Checked ? dtpExpiresAt.Value.ToUniversalTime() : (DateTime?)null;

            if (expiresAt.HasValue && expiresAt.Value <= joinedAt)
            {
                MessageBox.Show("⚠️ Ngày hết hạn phải sau ngày tham gia!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpExpiresAt.Focus();
                return;
            }

            try
            {
                using var context = new PostgresContext();

                var id = Guid.NewGuid();
                var currentUserId = PostgresContext.CurrentUserId;

                await context.Database.ExecuteSqlInterpolatedAsync($@"
                    INSERT INTO memberships (id, customer_id, tier, points, total_spent, total_orders,
                         joined_at, expires_at, last_order_at, updated_at)
                    VALUES ({id}, {customerId.Value}, {tier}::membership_tier, {points}, {totalSpent}, {totalOrders},
                         {joinedAt}, {expiresAt}, {(dtpLastOrder.Checked ? dtpLastOrder.Value.ToUniversalTime() : (DateTime?)null)}, {DateTime.UtcNow})");

                LogAudit("INSERT", id, $"CustomerId: {customerId.Value}, Tier: {tier}, Points: {points}");

                MessageBox.Show("✅ Thêm thẻ hội viên thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadMemberships();
                ClearForm();
            }
            catch (DbUpdateException dbEx)
            {
                string errorMsg = $"❌ Lỗi khi lưu vào database:\n\n{dbEx.Message}";
                if (dbEx.InnerException != null)
                {
                    errorMsg += $"\n\n📋 Chi tiết lỗi:\n{dbEx.InnerException.Message}";
                }
                MessageBox.Show(errorMsg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                string errorMsg = $"❌ Lỗi khi lưu:\n{ex.Message}";
                if (ex.InnerException != null)
                {
                    errorMsg += $"\n\n📋 Chi tiết lỗi:\n{ex.InnerException.Message}";
                }
                MessageBox.Show(errorMsg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task UpdateMembership()
        {
            if (_selectedMembership == null) return;

            var tier = cbTier.Text.Trim().ToLower();

            // Validate numeric fields
            if (!int.TryParse(txtPoints.Text.Trim(), out int points) || points < 0)
            {
                MessageBox.Show("⚠️ Điểm phải là số dương!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPoints.Focus();
                txtPoints.SelectAll();
                return;
            }

            if (!decimal.TryParse(txtTotalSpent.Text.Trim(), out decimal totalSpent) || totalSpent < 0)
            {
                MessageBox.Show("⚠️ Tổng chi tiêu phải là số dương!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTotalSpent.Focus();
                txtTotalSpent.SelectAll();
                return;
            }

            if (!int.TryParse(txtTotalOrders.Text.Trim(), out int totalOrders) || totalOrders < 0)
            {
                MessageBox.Show("⚠️ Số đơn phải là số dương!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTotalOrders.Focus();
                txtTotalOrders.SelectAll();
                return;
            }

            // Validate dates
            var joinedAt = dtpJoinedAt.Value.ToUniversalTime();
            var expiresAt = dtpExpiresAt.Checked ? dtpExpiresAt.Value.ToUniversalTime() : (DateTime?)null;

            if (expiresAt.HasValue && expiresAt.Value <= joinedAt)
            {
                MessageBox.Show("⚠️ Ngày hết hạn phải sau ngày tham gia!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpExpiresAt.Focus();
                return;
            }

            try
            {
                using var context = new PostgresContext();

                await context.Database.ExecuteSqlInterpolatedAsync($@"
                    UPDATE memberships SET
                         tier = {tier}::membership_tier,
                         points = {points},
                         total_spent = {totalSpent},
                         total_orders = {totalOrders},
                         joined_at = {joinedAt},
                         expires_at = {expiresAt},
                         last_order_at = {(dtpLastOrder.Checked ? dtpLastOrder.Value.ToUniversalTime() : (DateTime?)null)},
                         updated_at = {DateTime.UtcNow}
                    WHERE id = {_selectedMembership.Id}");

                LogAudit("UPDATE", _selectedMembership.Id, $"CustomerId: {_selectedMembership.CustomerId}, Tier: {tier}, Points: {points}");

                MessageBox.Show("✅ Cập nhật thẻ hội viên thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadMemberships();
                ClearForm();
            }
            catch (DbUpdateException dbEx)
            {
                string errorMsg = $"❌ Lỗi khi cập nhật database:\n\n{dbEx.Message}";
                if (dbEx.InnerException != null)
                {
                    errorMsg += $"\n\n📋 Chi tiết lỗi:\n{dbEx.InnerException.Message}";
                }
                MessageBox.Show(errorMsg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                string errorMsg = $"❌ Lỗi khi cập nhật:\n{ex.Message}";
                if (ex.InnerException != null)
                {
                    errorMsg += $"\n\n📋 Chi tiết lỗi:\n{ex.InnerException.Message}";
                }
                MessageBox.Show(errorMsg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Phone Search Helper

        private async void txtPhone_TextChanged(object sender, EventArgs e)
        {
            var phone = txtPhone.Text.Trim();

            if (string.IsNullOrEmpty(phone) || phone.Length < 9)
            {
                cbCustomer.SelectedIndex = -1;
                return;
            }

            // Auto-find customer by phone
            try
            {
                using var context = new PostgresContext();
                var customer = await context.Customers
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Phone == phone);

                if (customer != null)
                {
                    cbCustomer.SelectedValue = customer.Id;
                }
            }
            catch
            {
                // Silently fail
            }
        }

        #endregion

        #region Helper Methods

        private void ShowLoading(bool isLoading)
        {
            this.Cursor = isLoading ? Cursors.WaitCursor : Cursors.Default;
        }

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
                    TableName = "memberships",
                    RecordId = recordId,
                    NewValues = jsonContent,
                    IpAddress = PostgresContext.CurrentUserIP,
                    CreatedAt = DateTime.UtcNow
                });
                context.SaveChanges();
            }
            catch { }
        }

        #endregion
    }
}
