using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using MilkTeaPOS.Models;

namespace MilkTeaPOS
{
    public partial class frmDashboard : Form
    {
        private System.Windows.Forms.Timer _clockTimer;
        private System.Windows.Forms.Timer _refreshTimer;

        public frmDashboard()
        {
            InitializeComponent();
            InitializeDataGridViewColumns();
            InitializeTableTab();
            InitializeStatsTab();
            StartClock();
            StartAutoRefresh();
            LoadDashboard();
        }

        private Panel _pnlDailyChart;
        private Panel _pnlHourlyChart;
        private FlowLayoutPanel _flpDailyBars;
        private FlowLayoutPanel _flpHourlyBars;

        private void InitializeDataGridViewColumns()
        {
            // Daily revenue chart panel
            _flpDailyBars = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(250, 251, 252),
                Padding = new Padding(20, 10, 20, 10),
                WrapContents = false,
                AutoScroll = true
            };

            _pnlDailyChart = new Panel
            {
                Dock = DockStyle.Top,
                Height = 320,
                BackColor = Color.White
            };
            var lblDailyTitle = new Label
            {
                Text = "📊 Doanh thu theo ngày",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(45, 55, 72),
                Location = new Point(25, 15),
                AutoSize = true
            };
            _pnlDailyChart.Controls.Add(lblDailyTitle);

            // Bar area below title
            var pnlBarArea = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 280,
                BackColor = Color.FromArgb(250, 251, 252)
            };
            _flpDailyBars.Dock = DockStyle.Fill;
            pnlBarArea.Controls.Add(_flpDailyBars);
            _pnlDailyChart.Controls.Add(pnlBarArea);

            // Hourly chart panel
            _flpHourlyBars = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(250, 251, 252),
                Padding = new Padding(20, 10, 20, 10),
                WrapContents = false,
                AutoScroll = true
            };

            _pnlHourlyChart = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            var lblHourlyTitle = new Label
            {
                Text = "⏰ Phân bố theo giờ",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(45, 55, 72),
                Location = new Point(25, 15),
                AutoSize = true
            };
            _pnlHourlyChart.Controls.Add(lblHourlyTitle);

            var pnlHourlyArea = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 280,
                BackColor = Color.FromArgb(250, 251, 252)
            };
            _flpHourlyBars.Dock = DockStyle.Fill;
            pnlHourlyArea.Controls.Add(_flpHourlyBars);
            _pnlHourlyChart.Controls.Add(pnlHourlyArea);

            // Add to tab
            tabCharts.Controls.Clear();
            tabCharts.Controls.Add(_pnlHourlyChart);
            tabCharts.Controls.Add(_pnlDailyChart);

            // Recent Orders
            dgvRecentOrders.Columns.Clear();
            dgvRecentOrders.Columns.Add("OrderNumber", "Mã đơn");
            dgvRecentOrders.Columns.Add("Customer", "Khách hàng");
            dgvRecentOrders.Columns.Add("Table", "Bàn");
            dgvRecentOrders.Columns.Add("Total", "Tổng tiền");
            dgvRecentOrders.Columns.Add("Status", "Trạng thái");
            dgvRecentOrders.Columns.Add("Time", "Giờ");

            // Top Products
            dgvTopProducts.Columns.Clear();
            dgvTopProducts.Columns.Add("Rank", "");
            dgvTopProducts.Columns.Add("ProductName", "Sản phẩm");
            dgvTopProducts.Columns.Add("Qty", "SL");
            dgvTopProducts.Columns.Add("Revenue", "Doanh thu");

            // Payment Breakdown
            dgvPaymentBreakdown.Columns.Clear();
            dgvPaymentBreakdown.Columns.Add("Method", "Phương thức");
            dgvPaymentBreakdown.Columns.Add("Count", "Số GD");
            dgvPaymentBreakdown.Columns.Add("Total", "Tổng tiền");

            // Tables
            dgvTables.Columns.Clear();
            dgvTables.Columns.Add("TableName", "Tên bàn");
            dgvTables.Columns.Add("Location", "Vị trí");
            dgvTables.Columns.Add("Capacity", "Sức chứa");
            dgvTables.Columns.Add("TableStatus", "Trạng thái");
        }

        private void InitializeColumnHeaders()
        {
            // Columns are now set in InitializeDataGridViewColumns()
            // This method kept for compatibility
        }

        private void StartClock()
        {
            _clockTimer = new System.Windows.Forms.Timer();
            _clockTimer.Interval = 1000;
            _clockTimer.Tick += (s, e) =>
            {
                lblClock.Text = DateTime.Now.ToString("HH:mm:ss");
                lblDate.Text = DateTime.Now.ToString("dddd, dd/MM/yyyy");
            };
            _clockTimer.Start();
        }

        private void StartAutoRefresh()
        {
            _refreshTimer = new System.Windows.Forms.Timer();
            _refreshTimer.Interval = 30000;
            _refreshTimer.Tick += async (s, e) => await LoadDashboard();
            _refreshTimer.Start();
        }

        private async Task LoadDashboard()
        {
            try
            {
                var tasks = new List<Task>
                {
                    LoadKpiCards(),
                    LoadRecentOrders(),
                    LoadTopProducts(),
                    LoadTableStatus(),
                    LoadPaymentBreakdown(),
                    LoadRevenueChart(),
                    LoadHourlyOrders(),
                    LoadMembershipStats()
                };

                await Task.WhenAll(tasks);

                foreach (var task in tasks)
                {
                    if (task.IsFaulted)
                    {
                        System.Diagnostics.Debug.WriteLine($"[Dashboard] Task failed: {task.Exception?.Flatten().Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[Dashboard] Error: {ex.Message}");
            }
        }

        private async Task LoadKpiCards()
        {
            try
            {
                using var context = new PostgresContext();

                // Convert local midnight to UTC for queries (Vietnam is UTC+7)
                var todayLocal = DateTime.Now.Date; // e.g. 2026-04-09 00:00 Vietnam
                var todayStart = todayLocal.ToUniversalTime(); // e.g. 2026-04-08 17:00 UTC
                var todayEnd = todayLocal.AddDays(1).ToUniversalTime(); // e.g. 2026-04-09 17:00 UTC

                System.Diagnostics.Debug.WriteLine($"[Dashboard.KPI] Local today: {todayLocal:yyyy-MM-dd HH:mm}");
                System.Diagnostics.Debug.WriteLine($"[Dashboard.KPI] UTC range: {todayStart:yyyy-MM-dd HH:mm} to {todayEnd:yyyy-MM-dd HH:mm}");

                // Revenue today - filter by served orders
                var revenueToday = await context.Orders
                    .Where(o => o.Status == "served" && o.CreatedAt >= todayStart && o.CreatedAt < todayEnd)
                    .SumAsync(o => (decimal?)o.TotalAmount) ?? 0m;

                // Orders today
                var ordersToday = await context.Orders
                    .CountAsync(o => o.CreatedAt >= todayStart && o.CreatedAt < todayEnd);

                // Orders yesterday for trend
                var yesterdayLocal = todayLocal.AddDays(-1);
                var yesterdayStart = yesterdayLocal.ToUniversalTime();
                var yesterdayEnd = todayStart;
                var ordersYesterday = await context.Orders
                    .CountAsync(o => o.CreatedAt >= yesterdayStart && o.CreatedAt < yesterdayEnd);
                var orderTrend = ordersToday > ordersYesterday ? "▲" : ordersToday < ordersYesterday ? "▼" : "●";
                var orderDiff = ordersToday - ordersYesterday;

                System.Diagnostics.Debug.WriteLine($"[Dashboard.KPI] Revenue today: {revenueToday}, Orders today: {ordersToday}, Yesterday: {ordersYesterday}");

                // Active tables
                var activeTables = await context.Tables.CountAsync(t => t.Status == "occupied");
                var totalTables = await context.Tables.CountAsync();

                // Total customers
                var totalCustomers = await context.Customers.CountAsync();

                // Weekly revenue (from Monday local midnight)
                var weekStartLocal = todayLocal.AddDays(-(int)todayLocal.DayOfWeek);
                var weekStart = weekStartLocal.ToUniversalTime();
                var revenueWeek = await context.Orders
                    .Where(o => o.Status == "served" && o.CreatedAt >= weekStart)
                    .SumAsync(o => (decimal?)o.TotalAmount) ?? 0m;

                // Average order value
                var avgOrderValue = ordersToday > 0 ? revenueToday / ordersToday : 0m;

                // VIP members
                var vipCount = await context.Memberships.CountAsync(m =>
                    m.Tier == "gold" || m.Tier == "platinum" || m.Tier == "diamond");

                // Pending & preparing orders
                var pendingOrders = await context.Orders.CountAsync(o => o.Status == "pending");
                var preparingOrders = await context.Orders.CountAsync(o => o.Status == "preparing");

                System.Diagnostics.Debug.WriteLine($"[Dashboard.KPI] Active tables: {activeTables}/{totalTables}, VIP: {vipCount}, Customers: {totalCustomers}");

                if (InvokeRequired)
                {
                    try
                    {
                        Invoke(() =>
                        {
                            try
                            {
                                lblCard1Value.Text = FormatCurrency(revenueToday);
                                lblCard1Trend.Text = $"{orderTrend} {orderDiff:+0;-0;0} so với hôm qua";

                                lblCard2Value.Text = ordersToday.ToString();
                                lblCard2Trend.Text = $"⏳ Chờ: {pendingOrders} | 🔥 Pha: {preparingOrders}";

                                lblCard3Value.Text = $"{activeTables}/{totalTables}";
                                lblCard3Trend.Text = $"Trống: {totalTables - activeTables} bàn";

                                lblCard4Value.Text = totalCustomers.ToString();
                                lblCard4Trend.Text = $"VIP: {vipCount} | TB: {FormatCurrency(avgOrderValue)}/đơn";

                                lblCard5Value.Text = FormatCurrency(revenueWeek);
                                lblCard5Trend.Text = $"TB ngày: {FormatCurrency(revenueWeek / 7)}";
                                
                                System.Diagnostics.Debug.WriteLine($"[Dashboard.KPI] ✅ UI updated: Revenue={lblCard1Value.Text}, Orders={lblCard2Value.Text}");
                            }
                            catch (Exception innerEx)
                            {
                                System.Diagnostics.Debug.WriteLine($"[Dashboard.KPI] ❌ Invoke inner error: {innerEx.Message}\n{innerEx.StackTrace}");
                            }
                        });
                    }
                    catch (Exception invokeEx)
                    {
                        System.Diagnostics.Debug.WriteLine($"[Dashboard.KPI] ❌ Invoke failed: {invokeEx.Message}\n{invokeEx.StackTrace}");
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"[Dashboard.KPI] ⚠️ InvokeRequired=false, updating directly");
                    lblCard1Value.Text = FormatCurrency(revenueToday);
                    lblCard1Trend.Text = $"{orderTrend} {orderDiff:+0;-0;0} so với hôm qua";
                    lblCard2Value.Text = ordersToday.ToString();
                    lblCard2Trend.Text = $"⏳ Chờ: {pendingOrders} | 🔥 Pha: {preparingOrders}";
                    lblCard3Value.Text = $"{activeTables}/{totalTables}";
                    lblCard3Trend.Text = $"Trống: {totalTables - activeTables} bàn";
                    lblCard4Value.Text = totalCustomers.ToString();
                    lblCard4Trend.Text = $"VIP: {vipCount} | TB: {FormatCurrency(avgOrderValue)}/đơn";
                    lblCard5Value.Text = FormatCurrency(revenueWeek);
                    lblCard5Trend.Text = $"TB ngày: {FormatCurrency(revenueWeek / 7)}";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[Dashboard] LoadKpiCards error: {ex.Message}");
                if (InvokeRequired)
                {
                    Invoke(() =>
                    {
                        lblCard1Value.Text = "0đ"; lblCard1Trend.Text = "";
                        lblCard2Value.Text = "0"; lblCard2Trend.Text = "";
                        lblCard3Value.Text = "0/0"; lblCard3Trend.Text = "";
                        lblCard4Value.Text = "0"; lblCard4Trend.Text = "";
                        lblCard5Value.Text = "0đ"; lblCard5Trend.Text = "";
                    });
                }
            }
        }

        private async Task LoadRecentOrders()
        {
            try
            {
                using var context = new PostgresContext();
                
                // Use raw SQL to avoid Include issues with column mapping
                var orders = await context.Orders
                    .OrderByDescending(o => o.CreatedAt)
                    .Take(10)
                    .ToListAsync();

                dgvRecentOrders.Rows.Clear();
                foreach (var order in orders)
                {
                    var statusText = order.Status switch
                    {
                        "pending" => "⏳ Chờ",
                        "preparing" => "🔥 Đang pha",
                        "ready" => "✅ Sẵn sàng",
                        "served" => "🍹 Đã phục vụ",
                        "cancelled" => "❌ Hủy",
                        _ => order.Status ?? "❓ Không rõ"
                    };

                    var statusColor = order.Status switch
                    {
                        "pending" => Color.FromArgb(255, 193, 7),
                        "preparing" => Color.FromArgb(255, 107, 107),
                        "ready" => Color.FromArgb(72, 187, 120),
                        "served" => Color.FromArgb(23, 162, 184),
                        "cancelled" => Color.FromArgb(220, 53, 69),
                        _ => Color.Gray
                    };

                    var customerName = order.CustomerName ?? "Khách lẻ";
                    var tableName = order.IsDelivery == true ? "Mang đi" : "Tại quán";
                    var createTime = order.CreatedAt?.ToLocalTime().ToString("HH:mm") ?? "--:--";

                    dgvRecentOrders.Rows.Add(
                        order.OrderNumber ?? "",
                        customerName,
                        tableName,
                        FormatCurrency(order.TotalAmount ?? 0),
                        statusText,
                        createTime
                    );

                    var lastRow = dgvRecentOrders.Rows[dgvRecentOrders.Rows.Count - 1];
                    if (dgvRecentOrders.Columns["Status"] != null)
                    {
                        lastRow.Cells["Status"].Style.ForeColor = statusColor;
                        lastRow.Cells["Status"].Style.Font = _fontRowBold;
                    }
                }

                if (orders.Count == 0)
                {
                    dgvRecentOrders.Rows.Add("", "", "", "💤 Chưa có đơn hàng nào", "", "");
                    dgvRecentOrders.Rows[0].DefaultCellStyle.ForeColor = Color.Gray;
                    dgvRecentOrders.Rows[0].DefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Italic);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[Dashboard] LoadRecentOrders error: {ex.Message}");
                if (dgvRecentOrders.Rows.Count == 0)
                {
                    dgvRecentOrders.Rows.Add("Lỗi tải dữ liệu", ex.Message.Substring(0, Math.Min(50, ex.Message.Length)), "", "", "", "");
                    dgvRecentOrders.Rows[0].DefaultCellStyle.ForeColor = Color.Red;
                }
            }
        }

        private async Task LoadTopProducts()
        {
            try
            {
                using var context = new PostgresContext();
                var todayLocal = DateTime.Now.Date;
                var today = todayLocal.ToUniversalTime();
                var todayEnd = todayLocal.AddDays(1).ToUniversalTime();

                var topProducts = await context.OrderDetails
                    .Where(od => od.Order.CreatedAt >= today && od.Order.CreatedAt < todayEnd && od.Order.Status == "served")
                    .GroupBy(od => new { od.ProductId, od.ProductName })
                    .Select(g => new
                    {
                        ProductId = g.Key.ProductId,
                        ProductName = g.Key.ProductName ?? "Unknown",
                        TotalQty = g.Sum(x => x.Quantity),
                        TotalRevenue = g.Sum(x => x.Subtotal)
                    })
                    .OrderByDescending(x => x.TotalQty)
                    .Take(7)
                    .ToListAsync();

                dgvTopProducts.Rows.Clear();
                int rank = 1;
                foreach (var item in topProducts)
                {
                    var medal = rank == 1 ? "🥇" : rank == 2 ? "🥈" : rank == 3 ? "🥉" : $"{rank}.";
                    dgvTopProducts.Rows.Add(
                        medal,
                        item.ProductName,
                        item.TotalQty.ToString(),
                        FormatCurrency(item.TotalRevenue)
                    );

                    if (rank <= 3)
                    {
                        dgvTopProducts.Rows[dgvTopProducts.Rows.Count - 1].DefaultCellStyle.Font = _fontRowBold;
                        dgvTopProducts.Rows[dgvTopProducts.Rows.Count - 1].DefaultCellStyle.ForeColor =
                            rank == 1 ? Color.FromArgb(255, 193, 7) :
                            rank == 2 ? Color.FromArgb(108, 117, 125) :
                            Color.FromArgb(205, 127, 50);
                    }
                    rank++;
                }

                if (topProducts.Count == 0)
                {
                    dgvTopProducts.Rows.Add("", "💤 Chưa có sản phẩm nào hôm nay", "", "");
                    dgvTopProducts.Rows[0].DefaultCellStyle.ForeColor = Color.Gray;
                    dgvTopProducts.Rows[0].DefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Italic);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[Dashboard] LoadTopProducts error: {ex.Message}");
            }
        }

        private FlowLayoutPanel _flpTables;
        private Panel _pnlTableMap;
        private Label _lblTableSummary;

        private FlowLayoutPanel _flpTierStats;
        private FlowLayoutPanel _flpVoucherStats;
        private FlowLayoutPanel _flpCustomerStats;

        private void InitializeTableTab()
        {
            // Title panel at top
            var pnlTitle = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = Color.White
            };
            _lblTableSummary = new Label
            {
                Text = "🪑 Sơ đồ bàn",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(45, 55, 72),
                Location = new Point(25, 10),
                AutoSize = true
            };
            pnlTitle.Controls.Add(_lblTableSummary);

            // Table cards area below title
            _flpTables = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(247, 249, 252),
                Padding = new Padding(20, 10, 20, 20),
                AutoScroll = true
            };

            _pnlTableMap = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            _pnlTableMap.Controls.Add(_flpTables);
            _pnlTableMap.Controls.Add(pnlTitle);

            tabTables.Controls.Clear();
            tabTables.Controls.Add(_pnlTableMap);
        }

        private async Task LoadTableStatus()
        {
            try
            {
                using var context = new PostgresContext();
                var tables = await context.Tables
                    .OrderBy(t => t.Name)
                    .ToListAsync();

                // Get active orders per table
                var ordersByTable = await context.Orders
                    .Where(o => o.TableId.HasValue && o.Status != "served" && o.Status != "cancelled")
                    .GroupBy(o => o.TableId)
                    .Select(g => new { TableId = g.Key.Value, OrderCount = g.Count(), LatestOrder = g.OrderByDescending(o => o.CreatedAt).FirstOrDefault() })
                    .ToListAsync();

                Action updateAction = () =>
                {
                    _flpTables.Controls.Clear();

                    int totalTables = tables.Count;
                    int available = tables.Count(t => t.Status == "available");
                    int occupied = tables.Count(t => t.Status == "occupied");
                    int reserved = tables.Count(t => t.Status == "reserved");
                    int maintenance = tables.Count(t => t.Status == "maintenance");

                    // Update summary
                    _lblTableSummary.Text = $"🪑 Sơ đồ bàn  |  Tổng: {totalTables} bàn  |  🟢 {available} trống  |  🔴 {occupied} đang dùng  |  🟡 {reserved} đặt trước  |  ⚫ {maintenance} bảo trì";

                    foreach (var table in tables)
                    {
                        var card = CreateTableCard(table, ordersByTable.FirstOrDefault(o => o.TableId == table.Id));
                        _flpTables.Controls.Add(card);
                    }
                };

                if (InvokeRequired) Invoke(updateAction); else updateAction();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[Dashboard] LoadTableStatus error: {ex.Message}");
            }
        }

        private Panel CreateTableCard(Models.Table table, dynamic? activeOrder)
        {
            int cardWidth = 180;
            int cardHeight = 160;

            // Colors based on status
            var (bgColor, borderColor, textColor, icon, statusText) = table.Status switch
            {
                "available" => (Color.FromArgb(240, 253, 244), Color.FromArgb(34, 197, 94), Color.FromArgb(21, 128, 61), "🟢", "Trống"),
                "occupied" => (Color.FromArgb(254, 242, 242), Color.FromArgb(239, 68, 68), Color.FromArgb(185, 28, 28), "🔴", "Đang dùng"),
                "reserved" => (Color.FromArgb(254, 252, 232), Color.FromArgb(234, 179, 8), Color.FromArgb(146, 114, 8), "🟡", "Đã đặt"),
                "maintenance" => (Color.FromArgb(243, 244, 246), Color.FromArgb(107, 114, 128), Color.FromArgb(55, 65, 81), "⚫", "Bảo trì"),
                _ => (Color.FromArgb(249, 250, 251), Color.FromArgb(156, 163, 175), Color.Gray, "⚪", "Không rõ")
            };

            var card = new Panel
            {
                Width = cardWidth,
                Height = cardHeight,
                BackColor = bgColor,
                BorderStyle = BorderStyle.None,
                Margin = new Padding(8),
                Cursor = Cursors.Hand
            };

            // Border
            var border = new Panel
            {
                Dock = DockStyle.Top,
                Height = 4,
                BackColor = borderColor
            };
            card.Controls.Add(border);

            // Table name
            var lblName = new Label
            {
                Text = table.Name,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = textColor,
                Location = new Point(15, 15),
                AutoSize = false,
                Width = cardWidth - 30,
                TextAlign = ContentAlignment.MiddleCenter
            };
            card.Controls.Add(lblName);

            // Status
            var lblStatus = new Label
            {
                Text = $"{icon} {statusText}",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = textColor,
                Location = new Point(15, 50),
                AutoSize = false,
                Width = cardWidth - 30,
                TextAlign = ContentAlignment.MiddleCenter
            };
            card.Controls.Add(lblStatus);

            // Capacity
            var lblCapacity = new Label
            {
                Text = $"👥 {table.Capacity} người",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.Gray,
                Location = new Point(15, 85),
                AutoSize = false,
                Width = cardWidth - 30,
                TextAlign = ContentAlignment.MiddleCenter
            };
            card.Controls.Add(lblCapacity);

            // Location
            var lblLocation = new Label
            {
                Text = $"📍 {table.Location ?? "Khu vực chính"}",
                Font = new Font("Segoe UI", 8F),
                ForeColor = Color.Gray,
                Location = new Point(15, 108),
                AutoSize = false,
                Width = cardWidth - 30,
                TextAlign = ContentAlignment.MiddleCenter
            };
            card.Controls.Add(lblLocation);

            // Active order info
            if (activeOrder != null && activeOrder.LatestOrder != null)
            {
                var lblOrder = new Label
                {
                    Text = $"🧾 {activeOrder.LatestOrder.OrderNumber}",
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    ForeColor = textColor,
                    Location = new Point(15, 130),
                    AutoSize = false,
                    Width = cardWidth - 30,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                card.Controls.Add(lblOrder);
            }

            return card;
        }

        private async Task LoadPaymentBreakdown()
        {
            try
            {
                using var context = new PostgresContext();
                var todayLocal = DateTime.Now.Date;
                var today = todayLocal.ToUniversalTime();
                var todayEnd = todayLocal.AddDays(1).ToUniversalTime();

                var paymentStats = await context.Payments
                    .Where(p => p.Status == "completed" && p.CreatedAt >= today && p.CreatedAt < todayEnd)
                    .GroupBy(p => p.Method)
                    .Select(g => new
                    {
                        Method = g.Key,
                        Count = g.Count(),
                        Total = g.Sum(x => (decimal?)x.PaidAmount) ?? 0m
                    })
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

                    dgvPaymentBreakdown.Rows.Add(
                        methodText,
                        stat.Count.ToString(),
                        FormatCurrency(stat.Total)
                    );
                }

                var totalTx = paymentStats.Sum(p => p.Count);
                var totalAmount = paymentStats.Sum(p => p.Total);
                dgvPaymentBreakdown.Rows.Add(
                    "📊 Tổng cộng",
                    totalTx.ToString(),
                    FormatCurrency(totalAmount)
                );
                dgvPaymentBreakdown.Rows[dgvPaymentBreakdown.Rows.Count - 1].DefaultCellStyle.Font = _fontRowBold;
                dgvPaymentBreakdown.Rows[dgvPaymentBreakdown.Rows.Count - 1].DefaultCellStyle.BackColor = Color.FromArgb(240, 242, 245);

                if (paymentStats.Count == 0)
                {
                    dgvPaymentBreakdown.Rows.Insert(0, "💤 Chưa có giao dịch nào hôm nay", "", "");
                    dgvPaymentBreakdown.Rows[0].DefaultCellStyle.ForeColor = Color.Gray;
                    dgvPaymentBreakdown.Rows[0].DefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Italic);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[Dashboard] LoadPaymentBreakdown error: {ex.Message}");
            }
        }

        private async Task LoadRevenueChart()
        {
            try
            {
                using var context = new PostgresContext();
                var todayLocal = DateTime.Now.Date;
                var today = todayLocal.ToUniversalTime();
                var todayEnd = todayLocal.AddDays(1).ToUniversalTime();

                var dailyData = await context.Orders
                    .Where(o => o.Status == "served" && o.CreatedAt >= today && o.CreatedAt < todayEnd)
                    .GroupBy(o => o.CreatedAt!.Value.Date)
                    .Select(g => new { Date = g.Key, Revenue = g.Sum(o => o.TotalAmount ?? 0m), Orders = g.Count() })
                    .OrderBy(x => x.Date)
                    .ToListAsync();

                Action updateAction = () =>
                {
                    _flpDailyBars.Controls.Clear();
                    decimal maxRev = dailyData.Any() ? dailyData.Max(d => d.Revenue) : 1;

                    foreach (var day in dailyData)
                    {
                        var dateStr = day.Date.ToLocalTime().ToString("dd/MM");
                        var barHeight = (int)(day.Revenue / maxRev * 180);
                        var barWidth = 65;
                        
                        // Bar container
                        var pnl = new Panel { Width = barWidth, Height = 250, Margin = new Padding(4, 0, 4, 0) };
                        
                        // Revenue label on top
                        var lblVal = new Label
                        {
                            Text = FormatCurrencyShort(day.Revenue),
                            AutoSize = false,
                            Width = barWidth,
                            TextAlign = ContentAlignment.BottomCenter,
                            Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                            ForeColor = Color.FromArgb(45, 55, 72),
                            Location = new Point(0, 10),
                            Height = 25
                        };
                        pnl.Controls.Add(lblVal);
                        
                        // The bar
                        var bar = new Panel
                        {
                            BackColor = Color.FromArgb(59, 130, 246),
                            Width = barWidth - 10,
                            Height = Math.Max(barHeight, 6),
                            Location = new Point(5, 220 - barHeight),
                            BorderStyle = BorderStyle.None
                        };
                        pnl.Controls.Add(bar);
                        
                        // Date label below
                        var lblDate = new Label
                        {
                            Text = dateStr,
                            AutoSize = false,
                            Width = barWidth,
                            TextAlign = ContentAlignment.TopCenter,
                            Font = new Font("Segoe UI", 9F),
                            ForeColor = Color.Gray,
                            Location = new Point(0, 230),
                            Height = 22
                        };
                        pnl.Controls.Add(lblDate);
                        
                        _flpDailyBars.Controls.Add(pnl);
                    }

                    if (dailyData.Count == 0)
                    {
                        var lbl = new Label
                        {
                            Text = "💤 Chưa có dữ liệu hôm nay",
                            Font = new Font("Segoe UI", 12F, FontStyle.Italic),
                            ForeColor = Color.Gray,
                            AutoSize = true,
                            Location = new Point(50, 80)
                        };
                        _flpDailyBars.Controls.Add(lbl);
                    }
                };

                if (InvokeRequired) Invoke(updateAction); else updateAction();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[Dashboard] LoadRevenueChart error: {ex.Message}");
            }
        }

        private async Task LoadHourlyOrders()
        {
            try
            {
                using var context = new PostgresContext();
                var todayLocal = DateTime.Now.Date;
                var today = todayLocal.ToUniversalTime();
                var hourCounts = new int[24];
                var hourRevenue = new decimal[24];

                var orders = await context.Orders
                    .Where(o => o.Status == "served" && o.CreatedAt >= today)
                    .ToListAsync();

                foreach (var order in orders)
                {
                    var hour = order.CreatedAt?.Hour ?? 0;
                    if (hour >= 0 && hour < 24)
                    {
                        hourCounts[hour]++;
                        hourRevenue[hour] += order.TotalAmount ?? 0m;
                    }
                }

                Action updateAction = () =>
                {
                    _flpHourlyBars.Controls.Clear();
                    int maxCount = hourCounts.Max();

                    for (int hour = 6; hour <= 22; hour++)
                    {
                        var barHeight = maxCount > 0 ? (int)((double)hourCounts[hour] / maxCount * 180) : 0;
                        var barWidth = 50;
                        
                        // Bar container
                        var pnl = new Panel { Width = barWidth, Height = 250, Margin = new Padding(2, 0, 2, 0) };
                        
                        // Orders count on top
                        var lblVal = new Label
                        {
                            Text = hourCounts[hour].ToString(),
                            AutoSize = false,
                            Width = barWidth,
                            TextAlign = ContentAlignment.BottomCenter,
                            Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                            ForeColor = hourCounts[hour] == maxCount ? Color.FromArgb(245, 158, 11) : Color.Gray,
                            Location = new Point(0, 10),
                            Height = 25
                        };
                        pnl.Controls.Add(lblVal);
                        
                        // The bar
                        var isPeak = hourCounts[hour] == maxCount;
                        var bar = new Panel
                        {
                            BackColor = isPeak 
                                ? Color.FromArgb(245, 158, 11)  // Peak hour = amber
                                : Color.FromArgb(59, 130, 246), // Normal = blue
                            Width = barWidth - 10,
                            Height = Math.Max(barHeight, 4),
                            Location = new Point(5, 220 - barHeight)
                        };
                        pnl.Controls.Add(bar);
                        
                        // Hour label below
                        var lblHour = new Label
                        {
                            Text = $"{hour}h:00",
                            AutoSize = false,
                            Width = barWidth,
                            TextAlign = ContentAlignment.TopCenter,
                            Font = new Font("Segoe UI", 9F),
                            ForeColor = Color.Gray,
                            Location = new Point(0, 230),
                            Height = 22
                        };
                        pnl.Controls.Add(lblHour);
                        
                        _flpHourlyBars.Controls.Add(pnl);
                    }
                };

                if (InvokeRequired) Invoke(updateAction); else updateAction();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[Dashboard] LoadHourlyOrders error: {ex.Message}");
            }
        }

        private string GetPeakHour(int[] counts)
        {
            if (counts.Length == 0) return "N/A";
            
            int maxIndex = 0;
            for (int i = 1; i < counts.Length; i++)
            {
                if (counts[i] > counts[maxIndex]) maxIndex = i;
            }
            return $"{maxIndex + 6}h: {counts[maxIndex]} đơn";
        }

        private string FormatCurrency(decimal amount) => amount.ToString("#,##0") + "đ";

        private string FormatCurrencyShort(decimal amount)
        {
            if (amount >= 1000000) return $"{amount / 1000000:F1}Mđ";
            if (amount >= 1000) return $"{amount / 1000:F0}Kđ";
            return amount.ToString("#,##0") + "đ";
        }

        private void InitializeStatsTab()
        {
            // Main container
            var pnlStatsMain = new Panel { Dock = DockStyle.Fill, BackColor = Color.FromArgb(247, 249, 252) };

            // Title
            var lblStatsTitle = new Label
            {
                Text = "📈 Thống kê tổng quan",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = Color.FromArgb(45, 55, 72),
                Location = new Point(25, 15),
                AutoSize = true
            };

            // Membership tier cards
            _flpTierStats = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 140,
                BackColor = Color.White,
                Padding = new Padding(20, 10, 20, 10),
                AutoScroll = true
            };

            // Vouchers summary
            _flpVoucherStats = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 140,
                BackColor = Color.White,
                Padding = new Padding(20, 10, 20, 10),
                AutoScroll = true
            };

            // Customer activity chart
            _flpCustomerStats = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(247, 249, 252),
                Padding = new Padding(20),
                AutoScroll = true
            };

            pnlStatsMain.Controls.Add(_flpCustomerStats);
            pnlStatsMain.Controls.Add(_flpVoucherStats);
            pnlStatsMain.Controls.Add(_flpTierStats);
            pnlStatsMain.Controls.Add(lblStatsTitle);

            tabStats.Controls.Clear();
            tabStats.Controls.Add(pnlStatsMain);
        }

        private async Task LoadMembershipStats()
        {
            try
            {
                using var context = new PostgresContext();

                var tierStats = await context.Memberships
                    .GroupBy(m => m.Tier)
                    .Select(g => new
                    {
                        Tier = g.Key,
                        Count = g.Count(),
                        AvgPoints = g.Average(x => (double?)x.Points) ?? 0,
                        TotalRevenue = g.Sum(x => (decimal?)x.TotalSpent) ?? 0m
                    })
                    .ToListAsync();

                var totalVouchers = await context.Vouchers.CountAsync();
                var activeVouchers = await context.Vouchers.CountAsync(v => v.status == "active");
                var totalCustomers = await context.Customers.CountAsync();
                var membersWithOrders = await context.Orders.CountAsync(o => o.CustomerId.HasValue);

                Action updateAction = () =>
                {
                    // === Tier Stats ===
                    _flpTierStats.Controls.Clear();
                    var tierColors = new Dictionary<string, (Color bg, Color border, Color text, string icon)>
                    {
                        { "none", (Color.FromArgb(249, 250, 251), Color.FromArgb(156, 163, 175), Color.Gray, "⚪") },
                        { "silver", (Color.FromArgb(243, 244, 246), Color.FromArgb(107, 114, 128), Color.FromArgb(55, 65, 81), "🥈") },
                        { "gold", (Color.FromArgb(254, 252, 232), Color.FromArgb(234, 179, 8), Color.FromArgb(146, 114, 8), "🥇") },
                        { "platinum", (Color.FromArgb(219, 234, 254), Color.FromArgb(59, 130, 246), Color.FromArgb(29, 78, 137), "💎") },
                        { "diamond", (Color.FromArgb(237, 233, 254), Color.FromArgb(139, 92, 246), Color.FromArgb(76, 29, 149), "👑") }
                    };

                    foreach (var tier in tierStats.OrderByDescending(t => t.TotalRevenue))
                    {
                        var colors = tierColors.GetValueOrDefault(tier.Tier, tierColors["none"]);
                        var card = CreateStatCard($"{colors.icon} {tier.Tier.ToUpper()}",
                            $"{tier.Count} KH",
                            $"DT: {FormatCurrency(tier.TotalRevenue)}",
                            colors.bg, colors.border, colors.text, 200);
                        _flpTierStats.Controls.Add(card);
                    }

                    // Total customers card
                    var totalCard = CreateStatCard("👥 Tổng KH",
                        $"{totalCustomers} KH",
                        $"Thành viên: {membersWithOrders}",
                        Color.FromArgb(240, 253, 244), Color.FromArgb(34, 197, 94), Color.FromArgb(21, 128, 61), 200);
                    _flpTierStats.Controls.Add(totalCard);

                    // === Voucher Stats ===
                    _flpVoucherStats.Controls.Clear();
                    var voucherCard = CreateStatCard("🎫 Voucher",
                        $"{totalVouchers} mã",
                        $"Hoạt động: {activeVouchers}",
                        Color.FromArgb(254, 242, 242), Color.FromArgb(239, 68, 68), Color.FromArgb(185, 28, 28), 200);
                    _flpVoucherStats.Controls.Add(voucherCard);
                };

                if (InvokeRequired) Invoke(updateAction); else updateAction();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[Dashboard] LoadMembershipStats error: {ex.Message}");
            }
        }

        private Panel CreateStatCard(string title, string value, string subtitle, Color bgColor, Color borderColor, Color textColor, int width)
        {
            var card = new Panel
            {
                Width = width,
                Height = 110,
                BackColor = bgColor,
                BorderStyle = BorderStyle.None,
                Margin = new Padding(8)
            };

            // Top border
            var border = new Panel { Dock = DockStyle.Top, Height = 4, BackColor = borderColor };
            card.Controls.Add(border);

            // Title
            var lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = textColor,
                Location = new Point(10, 15),
                AutoSize = false,
                Width = width - 20,
                TextAlign = ContentAlignment.MiddleCenter
            };
            card.Controls.Add(lblTitle);

            // Value
            var lblValue = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = textColor,
                Location = new Point(10, 42),
                AutoSize = false,
                Width = width - 20,
                TextAlign = ContentAlignment.MiddleCenter
            };
            card.Controls.Add(lblValue);

            // Subtitle
            var lblSub = new Label
            {
                Text = subtitle,
                Font = new Font("Segoe UI", 8F),
                ForeColor = Color.Gray,
                Location = new Point(10, 80),
                AutoSize = false,
                Width = width - 20,
                TextAlign = ContentAlignment.MiddleCenter
            };
            card.Controls.Add(lblSub);

            return card;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _clockTimer?.Stop();
            _refreshTimer?.Stop();
            _clockTimer?.Dispose();
            _refreshTimer?.Dispose();
            base.OnFormClosing(e);
        }
    }
}
