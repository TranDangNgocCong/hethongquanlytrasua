using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using MilkTeaPOS.Models;
using Npgsql;

namespace MilkTeaPOS
{
    public partial class frmSalesReport : Form
    {
        #region Constants & Fields

        private DateTime _startDate = DateTime.UtcNow.Date.AddDays(-7);
        private DateTime _endDate = DateTime.UtcNow.Date.AddDays(1);

        // Cached fonts to avoid GDI leaks
        private readonly Font _fontHeader = new Font("Segoe UI", 10F, FontStyle.Bold);
        private readonly Font _fontValue = new Font("Segoe UI", 12F, FontStyle.Bold);
        private readonly Font _fontTotal = new Font("Segoe UI", 11F, FontStyle.Bold);
        private readonly Font _fontItalic = new Font("Segoe UI", 11F, FontStyle.Italic);

        #endregion

        #region Constructor & Initialization

        public frmSalesReport()
        {
            InitializeComponent();
            InitializeDataGridViewColumns();
            InitializeDateRange();
            LoadReport();
        }

        private void InitializeDateRange()
        {
            dtpStartDate.Value = _startDate.ToLocalTime();
            dtpEndDate.Value = _endDate.ToLocalTime();
        }

        #endregion

        #region Event Handlers

        private void btnFilter_Click(object sender, EventArgs e)
        {
            _startDate = dtpStartDate.Value.Date.ToUniversalTime();
            _endDate = dtpEndDate.Value.Date.AddDays(1).ToUniversalTime();
            LoadReport();
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            _startDate = DateTime.UtcNow.Date;
            _endDate = DateTime.UtcNow.Date.AddDays(1);
            dtpStartDate.Value = _startDate.ToLocalTime();
            dtpEndDate.Value = _endDate.ToLocalTime();
            LoadReport();
        }

        private void btnWeek_Click(object sender, EventArgs e)
        {
            _startDate = DateTime.UtcNow.Date.AddDays(-7);
            _endDate = DateTime.UtcNow.Date.AddDays(1);
            dtpStartDate.Value = _startDate.ToLocalTime();
            dtpEndDate.Value = _endDate.ToLocalTime();
            LoadReport();
        }

        private void btnMonth_Click(object sender, EventArgs e)
        {
            _startDate = DateTime.UtcNow.Date.AddDays(-30);
            _endDate = DateTime.UtcNow.Date.AddDays(1);
            dtpStartDate.Value = _startDate.ToLocalTime();
            dtpEndDate.Value = _endDate.ToLocalTime();
            LoadReport();
        }

        private async void btnAllTime_Click(object sender, EventArgs e)
        {
            try
            {
                using var context = new PostgresContext();
                
                // Find earliest and latest order dates
                var earliest = await context.Orders.AsNoTracking().MinAsync(o => o.CreatedAt);
                var latest = await context.Orders.AsNoTracking().MaxAsync(o => o.CreatedAt);
                
                if (earliest.HasValue && latest.HasValue)
                {
                    _startDate = earliest.Value.Date;
                    _endDate = latest.Value.Date.AddDays(1);
                    dtpStartDate.Value = _startDate.ToLocalTime();
                    dtpEndDate.Value = _endDate.ToLocalTime();
                    
                    System.Diagnostics.Debug.WriteLine($"[SalesReport] All Time: {earliest} to {latest}");
                    
                    LoadReport();
                }
                else
                {
                    MessageBox.Show("💤 Không có đơn hàng nào trong database!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi:\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadReport();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }

        private void dtpStartDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                dtpEndDate.Focus();
            }
        }

        private void dtpEndDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnFilter_Click(sender, e);
            }
        }

        #endregion

        #region Report Loading

        private async void LoadReport()
        {
            try
            {
                ShowLoading(true);

                // Debug: Show date range being used
                System.Diagnostics.Debug.WriteLine($"[SalesReport] Loading report with date range:");
                System.Diagnostics.Debug.WriteLine($"[SalesReport]   Start (UTC): {_startDate:yyyy-MM-dd HH:mm:ss}");
                System.Diagnostics.Debug.WriteLine($"[SalesReport]   End (UTC): {_endDate:yyyy-MM-dd HH:mm:ss}");
                System.Diagnostics.Debug.WriteLine($"[SalesReport]   Start (Local): {_startDate.ToLocalTime():yyyy-MM-dd HH:mm:ss}");
                System.Diagnostics.Debug.WriteLine($"[SalesReport]   End (Local): {_endDate.ToLocalTime():yyyy-MM-dd HH:mm:ss}");

                var tasks = new List<Task>
                {
                    LoadRevenueSummary(),
                    LoadDailyRevenue(),
                    LoadPaymentBreakdown(),
                    LoadProductPerformance(),
                    LoadOrderStatistics(),
                    LoadHourlyDistribution(),
                    LoadCustomerAnalytics()
                };

                await Task.WhenAll(tasks);

                // Check for any exceptions from tasks
                foreach (var task in tasks)
                {
                    if (task.IsFaulted)
                    {
                        System.Diagnostics.Debug.WriteLine($"[SalesReport] Task failed: {task.Exception?.Flatten().Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi tải báo cáo:\n{ex.Message}\n\nStack: {ex.StackTrace}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ShowLoading(false);
            }
        }

        private async Task LoadRevenueSummary()
        {
            try
            {
                using var context = new PostgresContext();

                var allOrders = await context.Orders
                    .AsNoTracking()
                    .Where(o => o.CreatedAt >= _startDate && o.CreatedAt < _endDate)
                    .ToListAsync();

                System.Diagnostics.Debug.WriteLine($"[RevenueSummary] Total orders in range: {allOrders.Count}");
                
                var statusGroups = allOrders.GroupBy(x => x.Status ?? "NULL").Select(g => new { Status = g.Key, Count = g.Count() }).ToList();
                foreach (var o in statusGroups)
                {
                    System.Diagnostics.Debug.WriteLine($"[RevenueSummary]   Status '{o.Status}': {o.Count}");
                }

                var servedOnly = allOrders.Where(o => o.Status == "served").ToList();
                System.Diagnostics.Debug.WriteLine($"[RevenueSummary] Served orders: {servedOnly.Count}");

                var totalRevenue = servedOnly.Sum(o => o.TotalAmount ?? 0m);
                var totalOrders = servedOnly.Count;
                var totalDiscount = servedOnly.Sum(o => (o.Discount ?? 0m) + (o.VoucherDiscount ?? 0m) + (o.MembershipDiscount ?? 0m));
                var avgOrderValue = totalOrders > 0 ? totalRevenue / totalOrders : 0m;

                var pendingOrders = allOrders.Count(o => o.Status == "pending");
                var cancelledOrders = allOrders.Count(o => o.Status == "cancelled");

                var ordersWithCustomer = servedOnly.Where(o => o.CustomerId.HasValue).ToList();
                var uniqueCustomers = ordersWithCustomer.Select(o => o.CustomerId).Distinct().Count();
                var avgSpentPerCustomer = uniqueCustomers > 0 ? totalRevenue / uniqueCustomers : 0m;

                System.Diagnostics.Debug.WriteLine($"[RevenueSummary] Revenue={totalRevenue}, Orders={totalOrders}, Discount={totalDiscount}");
                System.Diagnostics.Debug.WriteLine($"[RevenueSummary] Pending={pendingOrders}, Cancelled={cancelledOrders}, Customers={uniqueCustomers}");

                // Update UI - try Invoke directly since form is already shown
                try
                {
                    System.Diagnostics.Debug.WriteLine($"[RevenueSummary] About to Invoke UI update...");
                    this.Invoke(() =>
                    {
                        lblTotalRevenue.Text = FormatCurrency(totalRevenue);
                        lblTotalOrders.Text = totalOrders.ToString();
                        lblAvgOrderValue.Text = FormatCurrency(avgOrderValue);
                        lblTotalDiscount.Text = FormatCurrency(totalDiscount);
                        lblPendingOrders.Text = pendingOrders.ToString();
                        lblCancelledOrders.Text = cancelledOrders.ToString();
                        lblUniqueCustomers.Text = uniqueCustomers.ToString();
                        lblAvgSpentPerCustomer.Text = FormatCurrency(avgSpentPerCustomer);
                        
                        System.Diagnostics.Debug.WriteLine($"[RevenueSummary] ✅ UI labels updated: Revenue={lblTotalRevenue.Text}, Orders={lblTotalOrders.Text}");
                    });
                }
                catch (Exception uiEx)
                {
                    System.Diagnostics.Debug.WriteLine($"[RevenueSummary] ❌ UI update failed: {uiEx.Message}");
                }
            }
            catch (Exception ex)
            {
                ShowQueryError("Revenue Summary", ex);
            }
        }

        private async Task LoadDailyRevenue()
        {
            try
            {
                using var context = new PostgresContext();

                var dailyData = await context.Orders
                    .AsNoTracking()
                    .Where(o => o.Status == "served" && o.CreatedAt >= _startDate && o.CreatedAt < _endDate)
                    .GroupBy(o => o.CreatedAt!.Value.Date)
                    .Select(g => new
                    {
                        Date = g.Key,
                        Revenue = g.Sum(o => o.TotalAmount ?? 0m),
                        Orders = g.Count()
                    })
                    .OrderBy(x => x.Date)
                    .ToListAsync();

                dgvDailyRevenue.Rows.Clear();
                foreach (var day in dailyData)
                {
                    var dateStr = day.Date.ToLocalTime().ToString("dd/MM/yyyy");
                    var dayOfWeek = day.Date.ToLocalTime().DayOfWeek switch
                    {
                        DayOfWeek.Monday => "T2",
                        DayOfWeek.Tuesday => "T3",
                        DayOfWeek.Wednesday => "T4",
                        DayOfWeek.Thursday => "T5",
                        DayOfWeek.Friday => "T6",
                        DayOfWeek.Saturday => "T7",
                        DayOfWeek.Sunday => "CN",
                        _ => ""
                    };

                    dgvDailyRevenue.Rows.Add(
                        dateStr,
                        dayOfWeek,
                        day.Orders.ToString(),
                        FormatCurrency(day.Revenue)
                    );
                }

                // Add totals row
                if (dailyData.Any())
                {
                    var totalRev = dailyData.Sum(d => d.Revenue);
                    var totalOrd = dailyData.Sum(d => d.Orders);
                    dgvDailyRevenue.Rows.Add(
                        "📊 Tổng cộng",
                        "",
                        totalOrd.ToString(),
                        FormatCurrency(totalRev)
                    );
                    dgvDailyRevenue.Rows[dgvDailyRevenue.Rows.Count - 1].DefaultCellStyle.Font = _fontTotal;
                    dgvDailyRevenue.Rows[dgvDailyRevenue.Rows.Count - 1].DefaultCellStyle.BackColor = Color.FromArgb(240, 242, 245);
                }

                if (dailyData.Count == 0)
                {
                    dgvDailyRevenue.Rows.Add("💤 Không có dữ liệu", "", "", "");
                    dgvDailyRevenue.Rows[0].DefaultCellStyle.ForeColor = Color.Gray;
                    dgvDailyRevenue.Rows[0].DefaultCellStyle.Font = _fontItalic;
                }
            }
            catch (Exception ex)
            {
                ShowQueryError("Daily Revenue", ex);
            }
        }

        private async Task LoadPaymentBreakdown()
        {
            try
            {
                using var context = new PostgresContext();

                var paymentStats = await context.Payments
                    .AsNoTracking()
                    .Where(p => p.Status == "completed" && p.CreatedAt >= _startDate && p.CreatedAt < _endDate)
                    .GroupBy(p => p.Method)
                    .Select(g => new
                    {
                        Method = g.Key,
                        Count = g.Count(),
                        Total = g.Sum(x => x.PaidAmount),
                        AvgAmount = g.Average(x => x.PaidAmount)
                    })
                    .OrderByDescending(x => x.Total)
                    .ToListAsync();

                dgvPaymentBreakdown.Rows.Clear();
                foreach (var stat in paymentStats)
                {
                    var methodText = stat.Method switch
                    {
                        "cash" => "💵 Tiền mặt",
                        "card" => "💳 Thẻ",
                        "qr_code" => "📱 QR Code",
                        "bank_transfer" => "🏦 Chuyển khoản",
                        "e_wallet" => "📲 Ví điện tử",
                        _ => stat.Method ?? "❓ Khác"
                    };

                    var percentage = paymentStats.Sum(x => x.Total) > 0
                        ? (stat.Total / paymentStats.Sum(x => x.Total) * 100).ToString("F1") + "%"
                        : "0%";

                    dgvPaymentBreakdown.Rows.Add(
                        methodText,
                        stat.Count.ToString(),
                        FormatCurrency(stat.Total),
                        FormatCurrency(stat.AvgAmount),
                        percentage
                    );
                }

                // Add totals row
                if (paymentStats.Any())
                {
                    var totalTx = paymentStats.Sum(p => p.Count);
                    var totalAmount = paymentStats.Sum(p => p.Total);
                    dgvPaymentBreakdown.Rows.Add(
                        "📊 Tổng cộng",
                        totalTx.ToString(),
                        FormatCurrency(totalAmount),
                        "",
                        "100%"
                    );
                    dgvPaymentBreakdown.Rows[dgvPaymentBreakdown.Rows.Count - 1].DefaultCellStyle.Font = _fontTotal;
                    dgvPaymentBreakdown.Rows[dgvPaymentBreakdown.Rows.Count - 1].DefaultCellStyle.BackColor = Color.FromArgb(240, 242, 245);
                }

                if (paymentStats.Count == 0)
                {
                    dgvPaymentBreakdown.Rows.Add("💤 Không có giao dịch nào", "", "", "", "");
                    dgvPaymentBreakdown.Rows[0].DefaultCellStyle.ForeColor = Color.Gray;
                    dgvPaymentBreakdown.Rows[0].DefaultCellStyle.Font = _fontItalic;
                }
            }
            catch (Exception ex)
            {
                ShowQueryError("Payment Breakdown", ex);
            }
        }

        private async Task LoadProductPerformance()
        {
            try
            {
                using var context = new PostgresContext();

                var topProducts = await context.OrderDetails
                    .AsNoTracking()
                    .Where(od => od.Order.CreatedAt >= _startDate && od.Order.CreatedAt < _endDate &&
                                od.Order.Status == "served")
                    .GroupBy(od => new { od.ProductId, od.ProductName })
                    .Select(g => new
                    {
                        ProductId = g.Key.ProductId,
                        ProductName = g.Key.ProductName ?? "Unknown",
                        TotalQty = g.Sum(x => x.Quantity),
                        TotalRevenue = g.Sum(x => x.Subtotal),
                        AvgPrice = g.Average(x => x.UnitPrice)
                    })
                    .OrderByDescending(x => x.TotalQty)
                    .Take(20)
                    .ToListAsync();

                dgvProductPerformance.Rows.Clear();
                int rank = 1;
                foreach (var item in topProducts)
                {
                    var medal = rank == 1 ? "🥇" : rank == 2 ? "🥈" : rank == 3 ? "🥉" : $"{rank}.";

                    dgvProductPerformance.Rows.Add(
                        medal,
                        item.ProductName,
                        item.TotalQty.ToString(),
                        FormatCurrency(item.TotalRevenue),
                        FormatCurrency(item.AvgPrice)
                    );

                    // Highlight top 3
                    if (rank <= 3)
                    {
                        dgvProductPerformance.Rows[dgvProductPerformance.Rows.Count - 1].DefaultCellStyle.ForeColor =
                            rank == 1 ? Color.FromArgb(255, 193, 7) :
                            rank == 2 ? Color.FromArgb(108, 117, 125) :
                            Color.FromArgb(205, 127, 50);
                    }
                    rank++;
                }

                if (topProducts.Count == 0)
                {
                    dgvProductPerformance.Rows.Add("", "💤 Không có dữ liệu", "", "", "");
                    dgvProductPerformance.Rows[0].DefaultCellStyle.ForeColor = Color.Gray;
                    dgvProductPerformance.Rows[0].DefaultCellStyle.Font = _fontItalic;
                }
            }
            catch (Exception ex)
            {
                ShowQueryError("Product Performance", ex);
            }
        }

        private async Task LoadOrderStatistics()
        {
            try
            {
                using var context = new PostgresContext();

                var orders = await context.Orders
                    .AsNoTracking()
                    .Where(o => o.CreatedAt >= _startDate && o.CreatedAt < _endDate)
                    .ToListAsync();

                var totalOrders = orders.Count;
                var servedCount = orders.Count(o => o.Status == "served");
                var pendingCount = orders.Count(o => o.Status == "pending");
                var preparingCount = orders.Count(o => o.Status == "preparing");
                var readyCount = orders.Count(o => o.Status == "ready");
                var cancelledCount = orders.Count(o => o.Status == "cancelled");

                var dineInCount = orders.Count(o => o.IsDelivery == false || o.IsDelivery == null);
                var deliveryCount = orders.Count(o => o.IsDelivery == true);

                if (InvokeRequired)
                {
                    Invoke(() =>
                    {
                        dgvOrderStats.Rows.Clear();

                        dgvOrderStats.Rows.Add("✅ Đã phục vụ", servedCount.ToString());
                        dgvOrderStats.Rows.Add("⏳ Chờ xử lý", pendingCount.ToString());
                        dgvOrderStats.Rows.Add("🔥 Đang pha", preparingCount.ToString());
                        dgvOrderStats.Rows.Add("✓ Sẵn sàng", readyCount.ToString());
                        dgvOrderStats.Rows.Add("❌ Đã hủy", cancelledCount.ToString());
                        dgvOrderStats.Rows.Add("", "");
                        dgvOrderStats.Rows.Add("🏠 Tại quán", dineInCount.ToString());
                        dgvOrderStats.Rows.Add("🚀 Giao hàng", deliveryCount.ToString());

                        // Color coding
                        foreach (DataGridViewRow row in dgvOrderStats.Rows)
                        {
                            if (row.Cells["OrderStatValue"].Value == null) continue;

                            var value = row.Cells["OrderStatValue"].Value.ToString();
                            if (value == "0")
                            {
                                row.Cells["OrderStatValue"].Style.ForeColor = Color.Gray;
                            }
                            else if (row.Cells["OrderStatLabel"].Value?.ToString() == "❌ Đã hủy")
                            {
                                row.Cells["OrderStatValue"].Style.ForeColor = Color.FromArgb(220, 53, 69);
                            }
                            else
                            {
                                row.Cells["OrderStatValue"].Style.ForeColor = Color.FromArgb(72, 187, 120);
                            }
                            row.Cells["OrderStatValue"].Style.Font = _fontHeader;
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                ShowQueryError("Order Statistics", ex);
            }
        }

        private async Task LoadHourlyDistribution()
        {
            try
            {
                using var context = new PostgresContext();

                var orders = await context.Orders
                    .AsNoTracking()
                    .Where(o => o.CreatedAt >= _startDate && o.CreatedAt < _endDate)
                    .ToListAsync();

                var hourCounts = new int[24];
                var hourRevenue = new decimal[24];

                foreach (var order in orders.Where(o => o.Status == "served"))
                {
                    var hour = order.CreatedAt?.Hour ?? 0;
                    if (hour >= 0 && hour < 24)
                    {
                        hourCounts[hour]++;
                        hourRevenue[hour] += order.TotalAmount ?? 0m;
                    }
                }

                dgvHourlyDistribution.Rows.Clear();
                for (int hour = 6; hour <= 22; hour++)
                {
                    var barLength = hourCounts[hour] > 0 ? Math.Min(hourCounts[hour] * 3, 30) : 0;
                    var bar = new string('█', barLength);

                    dgvHourlyDistribution.Rows.Add(
                        $"{hour}h:00",
                        hourCounts[hour].ToString(),
                        FormatCurrency(hourRevenue[hour]),
                        bar
                    );

                    // Highlight peak hours
                    if (hourCounts[hour] > 0)
                    {
                        var row = dgvHourlyDistribution.Rows[dgvHourlyDistribution.Rows.Count - 1];
                        if (hourCounts[hour] >= hourCounts.Max())
                        {
                            row.DefaultCellStyle.ForeColor = Color.FromArgb(255, 193, 7);
                            row.DefaultCellStyle.Font = _fontHeader;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowQueryError("Hourly Distribution", ex);
            }
        }

        private async Task LoadCustomerAnalytics()
        {
            try
            {
                using var context = new PostgresContext();

                // Top customers by revenue
                var topCustomers = await context.Orders
                    .AsNoTracking()
                    .Where(o => o.Status == "served" && o.CreatedAt >= _startDate && o.CreatedAt < _endDate &&
                               o.CustomerId != null)
                    .GroupBy(o => new { o.CustomerId, o.CustomerName })
                    .Select(g => new
                    {
                        CustomerId = g.Key.CustomerId,
                        CustomerName = g.Key.CustomerName ?? "Khách lẻ",
                        OrderCount = g.Count(),
                        TotalSpent = g.Sum(o => o.TotalAmount ?? 0m)
                    })
                    .OrderByDescending(x => x.TotalSpent)
                    .Take(5)
                    .ToListAsync();

                if (InvokeRequired)
                {
                    Invoke(() =>
                    {
                        dgvTopCustomers.Rows.Clear();
                        int rank = 1;
                        foreach (var customer in topCustomers)
                        {
                            var medal = rank == 1 ? "🥇" : rank == 2 ? "🥈" : rank == 3 ? "🥉" : $"{rank}.";
                            dgvTopCustomers.Rows.Add(
                                medal,
                                customer.CustomerName,
                                customer.OrderCount.ToString(),
                                FormatCurrency(customer.TotalSpent)
                            );
                            rank++;
                        }

                        if (topCustomers.Count == 0)
                        {
                            dgvTopCustomers.Rows.Add("", "💤 Không có dữ liệu", "", "");
                            dgvTopCustomers.Rows[0].DefaultCellStyle.ForeColor = Color.Gray;
                            dgvTopCustomers.Rows[0].DefaultCellStyle.Font = _fontItalic;
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                ShowQueryError("Customer Analytics", ex);
            }
        }

        #endregion

        #region Export Functionality

        private void ExportToExcel()
        {
            try
            {
                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "CSV File|*.csv|All Files|*.*",
                    Title = "Xuất báo cáo",
                    FileName = $"SalesReport_{_startDate:yyyyMMdd}_{_endDate:yyyyMMdd}.csv"
                };

                if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

                using (var writer = new StreamWriter(saveFileDialog.FileName, false, System.Text.Encoding.UTF8))
                {
                    // Header
                    writer.WriteLine("BÁO CÁO DOANH THU - MILKTEA POS");
                    writer.WriteLine($"Thời gian: {_startDate.ToLocalTime():dd/MM/yyyy} - {_endDate.ToLocalTime():dd/MM/yyyy}");
                    writer.WriteLine($"Ngày xuất: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                    writer.WriteLine();

                    // Revenue Summary
                    writer.WriteLine("=== TỔNG QUAN DOANH THU ===");
                    writer.WriteLine($"Tổng doanh thu,{lblTotalRevenue.Text}");
                    writer.WriteLine($"Tổng đơn hàng,{lblTotalOrders.Text}");
                    writer.WriteLine($"Giá trị trung bình/đơn,{lblAvgOrderValue.Text}");
                    writer.WriteLine($"Tổng giảm giá,{lblTotalDiscount.Text}");
                    writer.WriteLine();

                    // Daily Revenue
                    writer.WriteLine("=== DOANH THU THEO NGÀY ===");
                    writer.WriteLine("Ngày,Thứ tự,Tổng đơn hàng,Doanh thu");
                    foreach (DataGridViewRow row in dgvDailyRevenue.Rows)
                    {
                        if (row.Cells[0].Value != null)
                        {
                            writer.WriteLine($"{row.Cells[0].Value},{row.Cells[1].Value},{row.Cells[2].Value},{row.Cells[3].Value}");
                        }
                    }
                    writer.WriteLine();

                    // Payment Breakdown
                    writer.WriteLine("=== THANH TOÁN THEO PHƯƠNG THỨC ===");
                    writer.WriteLine("Phương thức,Số giao dịch,Tổng tiền,Trung bình,Tỷ lệ");
                    foreach (DataGridViewRow row in dgvPaymentBreakdown.Rows)
                    {
                        if (row.Cells[0].Value != null)
                        {
                            writer.WriteLine($"{row.Cells[0].Value},{row.Cells[1].Value},{row.Cells[2].Value},{row.Cells[3].Value},{row.Cells[4].Value}");
                        }
                    }
                    writer.WriteLine();

                    // Product Performance
                    writer.WriteLine("=== HIỆU SUẤT SẢN PHẨM ===");
                    writer.WriteLine("Xếp hạng,Sản phẩm,Số lượng,Doanh thu,Giá TB");
                    foreach (DataGridViewRow row in dgvProductPerformance.Rows)
                    {
                        if (row.Cells[1].Value != null)
                        {
                            writer.WriteLine($"{row.Cells[0].Value},{row.Cells[1].Value},{row.Cells[2].Value},{row.Cells[3].Value},{row.Cells[4].Value}");
                        }
                    }
                }

                MessageBox.Show("✅ Xuất báo cáo thành công!\n\nFile đã được lưu tại:\n" + saveFileDialog.FileName,
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi xuất báo cáo:\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Helper Methods

        private void UpdateSummaryLabels(decimal totalRevenue, int totalOrders, decimal totalDiscount,
            decimal avgOrderValue, int pendingOrders, int cancelledOrders, int uniqueCustomers, decimal avgSpentPerCustomer)
        {
            try
            {
                lblTotalRevenue.Text = FormatCurrency(totalRevenue);
                lblTotalOrders.Text = totalOrders.ToString();
                lblAvgOrderValue.Text = FormatCurrency(avgOrderValue);
                lblTotalDiscount.Text = FormatCurrency(totalDiscount);
                lblPendingOrders.Text = pendingOrders.ToString();
                lblCancelledOrders.Text = cancelledOrders.ToString();
                lblUniqueCustomers.Text = uniqueCustomers.ToString();
                lblAvgSpentPerCustomer.Text = FormatCurrency(avgSpentPerCustomer);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[RevenueSummary] Error updating labels: {ex.Message}");
            }
        }

        private string FormatCurrency(decimal amount) => amount.ToString("#,##0") + "đ";

        private void ShowQueryError(string queryName, Exception ex)
        {
            var errorMsg = $"⚠️ Lỗi tải {queryName}:\n{ex.Message}";
            
            if (ex is PostgresException pgEx)
            {
                errorMsg += $"\n\nPostgreSQL Error:\n";
                errorMsg += $"  SQL State: {pgEx.SqlState}\n";
                errorMsg += $"  Message: {pgEx.MessageText}\n";
                errorMsg += $"  Detail: {pgEx.Detail}\n";
                errorMsg += $"  Hint: {pgEx.Hint}\n";
                errorMsg += $"  Position: {pgEx.Position}";
            }
            
            System.Diagnostics.Debug.WriteLine($"[SalesReport] {queryName} Error: {errorMsg}");
            
            if (InvokeRequired)
            {
                Invoke(() =>
                {
                    MessageBox.Show(errorMsg, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                });
            }
        }

        private void ShowLoading(bool isLoading)
        {
            if (InvokeRequired)
            {
                Invoke(() => ShowLoading(isLoading));
                return;
            }

            if (isLoading)
            {
                this.Cursor = Cursors.WaitCursor;
                lblLoading.Text = "⏳ Đang tải dữ liệu...";
                lblLoading.Visible = true;
            }
            else
            {
                this.Cursor = Cursors.Default;
                lblLoading.Visible = false;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _fontHeader?.Dispose();
            _fontValue?.Dispose();
            _fontTotal?.Dispose();
            _fontItalic?.Dispose();
            base.OnFormClosing(e);
        }

        #endregion
    }
}
