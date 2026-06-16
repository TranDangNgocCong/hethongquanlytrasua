using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using MilkTeaPOS.Models;

namespace MilkTeaPOS
{
    public partial class frmOrderHistory : Form
    {
        private sealed class OrderItem
        {
            public Guid Id { get; set; }
            public string OrderNumber { get; set; } = string.Empty;
            public DateTime CreatedAt { get; set; }
            public string Status { get; set; } = string.Empty;
            public string? CustomerName { get; set; }
            public string? CustomerPhone { get; set; }
            public string? CustomerTier { get; set; }
            public string? TableName { get; set; }
            public decimal Total { get; set; }
            public string PaymentMethod { get; set; } = string.Empty;
            public string PaymentStatus { get; set; } = string.Empty;
            public int DetailCount { get; set; }
            public string? StaffName { get; set; }
            public decimal Subtotal { get; set; }
            public decimal Discount { get; set; }
            public decimal VoucherDiscount { get; set; }
            public decimal MembershipDiscount { get; set; }
            public bool IsDelivery { get; set; }
            public string? Notes { get; set; }
            public DateTime? ServedAt { get; set; }
            public DateTime? CancelledAt { get; set; }
        }

        private sealed class OrderDetailItem
        {
            public Guid Id { get; set; }
            public Guid ProductId { get; set; }
            public string ProductName { get; set; } = string.Empty;
            public string? ProductImage { get; set; }
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal ToppingTotal { get; set; }
            public decimal Subtotal { get; set; }
            public string Size { get; set; } = "M";
            public string Sugar { get; set; } = "100";
            public string Ice { get; set; } = "100";
            public string Status { get; set; } = "pending";
            public List<ToppingDetail> Toppings { get; set; } = new();
            public string? Notes { get; set; }
            public string? SpecialInstructions { get; set; }
        }

        private sealed class ToppingDetail
        {
            public Guid ToppingId { get; set; }
            public string Name { get; set; } = string.Empty;
            public int Quantity { get; set; }
            public decimal Price { get; set; }
        }

        private List<OrderItem> _orders = new();
        private Guid? _selectedOrderId;

        // For resuming held orders
        public Guid? ResumeOrderId { get; private set; }

        public frmOrderHistory()
        {
            InitializeComponent();
        }

        private async void FrmOrderHistory_Load(object? sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("=== FrmOrderHistory_Load START ===");
            // Default date range: last 30 days
            dtpTuNgay.Value = DateTime.Today.AddDays(-30);
            dtpDenNgay.Value = DateTime.Today.AddDays(1).AddTicks(-1);
            System.Diagnostics.Debug.WriteLine($"Date range: {dtpTuNgay.Value} to {dtpDenNgay.Value}");

            cboTrangThai.Items.Clear();
            cboTrangThai.Items.Add("Tất cả");
            cboTrangThai.Items.Add("pending");
            cboTrangThai.Items.Add("preparing");
            cboTrangThai.Items.Add("ready");
            cboTrangThai.Items.Add("served");
            cboTrangThai.Items.Add("cancelled");
            cboTrangThai.SelectedIndex = 0;

            System.Diagnostics.Debug.WriteLine("Calling LoadDashboardAsync...");
            await LoadDashboardAsync();
            System.Diagnostics.Debug.WriteLine("Calling LoadOrdersAsync...");
            await LoadOrdersAsync();
            System.Diagnostics.Debug.WriteLine("=== FrmOrderHistory_Load END ===");
        }

        private async Task LoadDashboardAsync()
        {
            System.Diagnostics.Debug.WriteLine("=== LoadDashboardAsync START ===");
            try
            {
                using var context = new PostgresContext();
                var today = DateTime.Today;
                var tomorrow = today.AddDays(1);
                System.Diagnostics.Debug.WriteLine($"Today: {today}, Tomorrow: {tomorrow}");

                await using var conn = context.Database.GetDbConnection();
                System.Diagnostics.Debug.WriteLine($"Connection state before open: {conn.State}");
                if (conn.State != System.Data.ConnectionState.Open)
                    await conn.OpenAsync();
                System.Diagnostics.Debug.WriteLine($"Connection state after open: {conn.State}");

                await using var cmd = conn.CreateCommand();
                cmd.CommandText = @"
SELECT
    (SELECT COUNT(*) FROM orders WHERE created_at >= @today AND created_at < @tomorrow) AS today_orders,
    (SELECT COALESCE(SUM(total_amount), 0) FROM orders WHERE created_at >= @today AND created_at < @tomorrow AND status != 'cancelled') AS today_revenue,
    (SELECT COUNT(*) FROM orders WHERE status = 'pending') AS pending_orders,
    (SELECT COUNT(*) FROM orders WHERE status = 'cancelled') AS cancelled_orders;";
                System.Diagnostics.Debug.WriteLine($"SQL Command: {cmd.CommandText}");

                var pToday = cmd.CreateParameter();
                pToday.ParameterName = "@today";
                pToday.Value = today;
                cmd.Parameters.Add(pToday);

                var pTomorrow = cmd.CreateParameter();
                pTomorrow.ParameterName = "@tomorrow";
                pTomorrow.Value = tomorrow;
                cmd.Parameters.Add(pTomorrow);

                System.Diagnostics.Debug.WriteLine("Executing reader...");
                await using var reader = await cmd.ExecuteReaderAsync();
                System.Diagnostics.Debug.WriteLine("Reader executed, checking if has rows...");
                if (await reader.ReadAsync())
                {
                    var todayOrders = reader.IsDBNull(0) ? 0L : reader.GetInt64(0);
                    var todayRevenue = reader.IsDBNull(1) ? 0m : reader.GetDecimal(1);
                    var pendingOrders = reader.IsDBNull(2) ? 0L : reader.GetInt64(2);
                    var cancelledOrders = reader.IsDBNull(3) ? 0L : reader.GetInt64(3);

                    System.Diagnostics.Debug.WriteLine($"Dashboard results: orders={todayOrders}, revenue={todayRevenue}, pending={pendingOrders}, cancelled={cancelledOrders}");

                    lblCardOrdersValue.Text = todayOrders.ToString("N0");
                    lblCardRevenueValue.Text = FormatCurrency(todayRevenue);
                    lblCardPendingValue.Text = pendingOrders.ToString("N0");
                    lblCardCancelledValue.Text = cancelledOrders.ToString("N0");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Dashboard query returned no rows!");
                    lblCardOrdersValue.Text = "0";
                    lblCardRevenueValue.Text = "0đ";
                    lblCardPendingValue.Text = "0";
                    lblCardCancelledValue.Text = "0";
                }
            }
            catch (Exception ex)
            {
                var fullError = $"Dashboard Error: {ex.Message}\nInner: {ex.InnerException?.Message}\nStack: {ex.StackTrace}";
                System.Diagnostics.Debug.WriteLine(fullError);
                MessageBox.Show(fullError, "Dashboard Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblCardOrdersValue.Text = "0";
                lblCardRevenueValue.Text = "0đ";
                lblCardPendingValue.Text = "0";
                lblCardCancelledValue.Text = "0";
            }
            System.Diagnostics.Debug.WriteLine("=== LoadDashboardAsync END ===");
        }

        private async Task LoadOrdersAsync()
        {
            System.Diagnostics.Debug.WriteLine("=== LoadOrdersAsync START ===");
            try
            {
                using var context = new PostgresContext();
                var fromDate = dtpTuNgay.Value.Date;
                var toDate = dtpDenNgay.Value.Date.AddDays(1);
                System.Diagnostics.Debug.WriteLine($"From: {fromDate}, To: {toDate}");

                await using var conn = context.Database.GetDbConnection();
                System.Diagnostics.Debug.WriteLine($"Connection state: {conn.State}");
                if (conn.State != System.Data.ConnectionState.Open)
                    await conn.OpenAsync();
                System.Diagnostics.Debug.WriteLine("Connection opened");

                await using var cmd = conn.CreateCommand();
                cmd.CommandText = @"
SELECT
    o.id,
    COALESCE(o.order_number, o.id::text),
    o.created_at,
    COALESCE(o.status::text, 'pending'),
    o.customer_name,
    o.customer_phone,
    COALESCE(m.tier::text, 'none') AS customer_tier,
    t.name AS table_name,
    COALESCE(o.total_amount, 0) AS total_amount,
    COALESCE((SELECT p.method::text FROM payments p WHERE p.order_id = o.id ORDER BY p.created_at DESC LIMIT 1), '') AS payment_method,
    COALESCE((SELECT p.status::text FROM payments p WHERE p.order_id = o.id ORDER BY p.created_at DESC LIMIT 1), '') AS payment_status,
    COALESCE((SELECT COUNT(*) FROM order_details od WHERE od.order_id = o.id), 0) AS detail_count,
    COALESCE(u.username, '') AS staff_name,
    COALESCE(o.subtotal, 0) AS subtotal,
    COALESCE(o.discount, 0) AS discount,
    COALESCE(o.voucher_discount, 0) AS voucher_discount,
    COALESCE(o.membership_discount, 0) AS membership_discount,
    COALESCE(o.is_delivery, false) AS is_delivery,
    o.notes,
    o.served_at,
    o.cancelled_at
FROM orders o
LEFT JOIN memberships m ON m.customer_id = o.customer_id
LEFT JOIN tables t ON t.id = o.table_id
LEFT JOIN users u ON u.id = o.user_id
WHERE o.created_at >= @fromDate AND o.created_at < @toDate
ORDER BY o.created_at DESC;";
                System.Diagnostics.Debug.WriteLine($"SQL Command prepared");

                var pFrom = cmd.CreateParameter();
                pFrom.ParameterName = "@fromDate";
                pFrom.Value = fromDate;
                cmd.Parameters.Add(pFrom);

                var pTo = cmd.CreateParameter();
                pTo.ParameterName = "@toDate";
                pTo.Value = toDate;
                cmd.Parameters.Add(pTo);

                System.Diagnostics.Debug.WriteLine("Executing reader...");
                _orders.Clear();
                int rowCount = 0;
                await using var reader = await cmd.ExecuteReaderAsync();
                System.Diagnostics.Debug.WriteLine("Reader executed, reading rows...");
                while (await reader.ReadAsync())
                {
                    rowCount++;
                    try
                    {
                        var orderId = reader.GetGuid(0);
                        var orderNum = reader.IsDBNull(1) ? "" : reader.GetString(1);
                        var createdAt = reader.GetDateTime(2);
                        var status = reader.IsDBNull(3) ? "pending" : reader.GetString(3);
                        var total = reader.IsDBNull(8) ? 0m : reader.GetDecimal(8);

                        System.Diagnostics.Debug.WriteLine($"Row {rowCount}: id={orderId}, num={orderNum}, status={status}, total={total}");

                        _orders.Add(new OrderItem
                        {
                            Id = orderId,
                            OrderNumber = orderNum,
                            CreatedAt = createdAt,
                            Status = status,
                            CustomerName = reader.IsDBNull(4) ? null : reader.GetString(4),
                            CustomerPhone = reader.IsDBNull(5) ? null : reader.GetString(5),
                            CustomerTier = reader.IsDBNull(6) ? null : reader.GetString(6),
                            TableName = reader.IsDBNull(7) ? null : reader.GetString(7),
                            Total = total,
                            PaymentMethod = reader.IsDBNull(9) ? "" : reader.GetString(9),
                            PaymentStatus = reader.IsDBNull(10) ? "" : reader.GetString(10),
                            DetailCount = reader.IsDBNull(11) ? 0 : reader.GetInt32(11),
                            StaffName = reader.IsDBNull(12) ? null : reader.GetString(12),
                            Subtotal = reader.GetDecimal(13),
                            Discount = reader.GetDecimal(14),
                            VoucherDiscount = reader.GetDecimal(15),
                            MembershipDiscount = reader.GetDecimal(16),
                            IsDelivery = reader.GetBoolean(17),
                            Notes = reader.IsDBNull(18) ? null : reader.GetString(18),
                            ServedAt = reader.IsDBNull(19) ? null : reader.GetDateTime(19),
                            CancelledAt = reader.IsDBNull(20) ? null : reader.GetDateTime(20)
                        });
                    }
                    catch (Exception rowEx)
                    {
                        System.Diagnostics.Debug.WriteLine($"Row {rowCount} error: {rowEx.Message}");
                    }
                }

                System.Diagnostics.Debug.WriteLine($"Total rows read: {rowCount}, _orders count: {_orders.Count}");
                System.Diagnostics.Debug.WriteLine("Calling RenderOrderList...");
                RenderOrderList();
                System.Diagnostics.Debug.WriteLine("RenderOrderList done");

                // Update filter options without triggering event
                cboTrangThai.SelectedIndexChanged -= CboTrangThai_SelectedIndexChanged;
                var statuses = _orders.Select(o => o.Status).Distinct().ToList();
                System.Diagnostics.Debug.WriteLine($"Distinct statuses: {string.Join(", ", statuses)}");
                var currentSelection = cboTrangThai.SelectedItem?.ToString();
                cboTrangThai.Items.Clear();
                cboTrangThai.Items.Add("Tất cả");
                foreach (var s in statuses.OrderBy(s => s))
                    cboTrangThai.Items.Add(s);
                if (currentSelection != null && cboTrangThai.Items.Contains(currentSelection))
                    cboTrangThai.SelectedItem = currentSelection;
                cboTrangThai.SelectedIndexChanged += CboTrangThai_SelectedIndexChanged;
                System.Diagnostics.Debug.WriteLine("Filter options updated");
            }
            catch (Exception ex)
            {
                var fullError = $"LoadOrders Error: {ex.Message}\nInner: {ex.InnerException?.Message}\nStack: {ex.StackTrace}";
                System.Diagnostics.Debug.WriteLine(fullError);
                MessageBox.Show(fullError, "LoadOrders Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            System.Diagnostics.Debug.WriteLine("=== LoadOrdersAsync END ===");
        }

        private List<OrderItem> GetFilteredOrders()
        {
            var status = cboTrangThai.SelectedItem?.ToString();
            if (string.IsNullOrWhiteSpace(status) || status == "Tất cả")
                return _orders;
            return _orders.Where(o => o.Status == status).ToList();
        }

        private void CboTrangThai_SelectedIndexChanged(object? sender, EventArgs e)
        {
            RenderOrderList();
        }

        private async void BtnLoc_Click(object? sender, EventArgs e)
        {
            await LoadDashboardAsync();
            await LoadOrdersAsync();
        }

        private void RenderOrderList()
        {
            var filtered = GetFilteredOrders();
            lblOrderListHeader.Text = $"📋 Danh sách đơn hàng ({filtered.Count})";

            var listPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.FromArgb(247, 249, 252)
            };

            int y = 28; // Shift down 20px to avoid overlap with header
            foreach (var order in filtered)
            {
                var card = CreateOrderCard(order, y);
                listPanel.Controls.Add(card);
                y += 148; // card height 140 + 8 margin
            }

            pnlOrderList.Controls.Clear();
            pnlOrderList.Controls.Add(lblOrderListHeader);
            pnlOrderList.Controls.Add(listPanel);
        }

        private Panel CreateOrderCard(OrderItem order, int top)
        {
            var card = new Panel
            {
                Left = 8,
                Top = top,
                Width = pnlOrderList.ClientSize.Width - 16,
                Height = 140,
                BorderStyle = BorderStyle.None,
                BackColor = order.Id == _selectedOrderId ? Color.FromArgb(232, 240, 254) : Color.White,
                Cursor = Cursors.Hand
            };

            // Left accent bar
            var statusColor = GetStatusColor(order.Status);
            var accent = new Panel { Left = 0, Top = 0, Width = 6, Height = 140, BackColor = statusColor };
            card.Controls.Add(accent);

            // Order number
            var lblOrderNum = new Label
            {
                Left = 16,
                Top = 10,
                Width = card.Width - 140,
                Height = 24,
                Text = order.OrderNumber,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(45, 55, 72)
            };
            card.Controls.Add(lblOrderNum);

            // Date/time
            var lblDate = new Label
            {
                Left = 16,
                Top = 34,
                Width = card.Width - 140,
                Height = 20,
                Text = order.CreatedAt.ToString("dd/MM/yyyy HH:mm:ss"),
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.Gray
            };
            card.Controls.Add(lblDate);

            // Customer info
            var custInfo = order.CustomerName ?? "Khách lẻ";
            if (order.CustomerTier != null && order.CustomerTier != "none")
                custInfo += $" ({order.CustomerTier.ToUpper()})";
            if (!string.IsNullOrWhiteSpace(order.TableName))
                custInfo += $" — Bàn {order.TableName}";
            if (order.IsDelivery)
                custInfo += " 🚚 Giao hàng";
            if (!string.IsNullOrWhiteSpace(order.CustomerPhone))
                custInfo += $" — {order.CustomerPhone}";

            var lblCust = new Label
            {
                Left = 16,
                Top = 56,
                Width = card.Width - 140,
                Height = 20,
                Text = custInfo,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(108, 117, 125)
            };
            card.Controls.Add(lblCust);

            // Staff
            if (!string.IsNullOrWhiteSpace(order.StaffName))
            {
                var lblStaff = new Label
                {
                    Left = 16,
                    Top = 78,
                    Width = card.Width - 140,
                    Height = 18,
                    Text = $"NV: {order.StaffName}",
                    Font = new Font("Segoe UI", 8.5F),
                    ForeColor = Color.Gray
                };
                card.Controls.Add(lblStaff);
            }

            // Status badge (larger)
            var statusText = GetStatusText(order.Status);
            var pnlStatus = new Panel
            {
                Left = 16,
                Top = 102,
                BackColor = statusColor,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };
            var lblStatus = new Label
            {
                Text = statusText,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Padding = new Padding(10, 4, 10, 4)
            };
            pnlStatus.Controls.Add(lblStatus);
            card.Controls.Add(pnlStatus);

            // === RIGHT SIDE ===

            // Total (big, right side)
            var lblTotal = new Label
            {
                Left = card.Width - 140,
                Top = 8,
                Width = 130,
                Height = 30,
                Text = FormatCurrency(order.Total),
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(220, 53, 69),
                TextAlign = ContentAlignment.MiddleRight
            };
            card.Controls.Add(lblTotal);

            // Payment method
            var methodText = order.PaymentMethod == "cash" ? "💵 Tiền mặt" : order.PaymentMethod == "qr_code" ? "📱 QR Code" : order.PaymentMethod;
            var lblMethod = new Label
            {
                Left = card.Width - 140,
                Top = 42,
                Width = 130,
                Height = 20,
                Text = methodText,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.Gray,
                TextAlign = ContentAlignment.MiddleRight
            };
            card.Controls.Add(lblMethod);

            // Detail count
            var lblDetails = new Label
            {
                Left = card.Width - 140,
                Top = 64,
                Width = 130,
                Height = 20,
                Text = $"{order.DetailCount} món",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(45, 55, 72),
                TextAlign = ContentAlignment.MiddleRight
            };
            card.Controls.Add(lblDetails);

            // Notes preview
            if (!string.IsNullOrWhiteSpace(order.Notes))
            {
                var lblNotes = new Label
                {
                    Left = card.Width - 140,
                    Top = 88,
                    Width = 130,
                    Height = 36,
                    Text = $"📝 {order.Notes}",
                    Font = new Font("Segoe UI", 7.5F, FontStyle.Italic),
                    ForeColor = Color.FromArgb(255, 152, 0),
                    TextAlign = ContentAlignment.TopRight,
                    AutoEllipsis = true
                };
                card.Controls.Add(lblNotes);
            }

            // Separator line at bottom
            var sepLine = new Panel
            {
                Left = 6,
                Top = 138,
                Width = card.Width - 12,
                Height = 1,
                BackColor = Color.FromArgb(235, 240, 245)
            };
            card.Controls.Add(sepLine);

            // Click handler
            card.Click += (_, _) => SelectOrder(order.Id);
            lblOrderNum.Click += (_, _) => SelectOrder(order.Id);

            return card;
        }

        private static Color GetStatusColor(string status) => status switch
        {
            "pending" => Color.FromArgb(255, 193, 7),
            "preparing" => Color.FromArgb(23, 162, 184),
            "ready" => Color.FromArgb(72, 187, 120),
            "served" => Color.FromArgb(45, 55, 72),
            "cancelled" => Color.FromArgb(220, 53, 69),
            _ => Color.Gray
        };

        private static string GetStatusText(string status) => status switch
        {
            "pending" => "⏳ Chờ xử lý",
            "preparing" => "🍳 Đang pha",
            "ready" => "✅ Sẵn sàng",
            "served" => "🍽 Đã phục vụ",
            "cancelled" => "❌ Đã hủy",
            _ => status
        };

        private void SelectOrder(Guid orderId)
        {
            _selectedOrderId = orderId;
            RenderOrderList();
            _ = RenderOrderDetailAsync(orderId);
        }

        private void ResumeOrder(Guid orderId)
        {
            ResumeOrderId = orderId;
            DialogResult = DialogResult.OK;
            Close();
        }

        private async Task RenderOrderDetailAsync(Guid orderId)
        {
            try
            {
                var order = _orders.FirstOrDefault(o => o.Id == orderId);
                if (order == null) return;

                var details = await LoadOrderDetailsAsync(orderId);

                lblDetailHeader.Text = $"📄 {order.OrderNumber} - {order.CreatedAt:dd/MM/yyyy HH:mm}";

                var detailPanel = new Panel
                {
                    Dock = DockStyle.Fill,
                    AutoScroll = true,
                    BackColor = Color.FromArgb(247, 249, 252)
                };

                int y = 36; // Shifted down 20px to avoid overlap with header
                var panelWidth = pnlOrderDetail.ClientSize.Width - 16;

                // Order info card (taller)
                var infoCard = new Panel
                {
                    Left = 8,
                    Top = y,
                    Width = pnlOrderDetail.ClientSize.Width - 16,
                    Height = 180,
                    BackColor = Color.White,
                    BorderStyle = BorderStyle.FixedSingle
                };

                var infoItems = new List<(string Label, string Value)>
                {
                    ("Trạng thái:", GetStatusText(order.Status)),
                    ("Nhân viên:", order.StaffName ?? "N/A"),
                    ("Khách hàng:", order.CustomerName ?? "Khách lẻ"),
                    ("Điện thoại:", order.CustomerPhone ?? "N/A"),
                    ("Hạng KH:", order.CustomerTier?.ToUpperInvariant() ?? "NONE"),
                    ("Bàn:", order.TableName ?? (order.IsDelivery ? "🚚 Giao hàng" : "N/A")),
                    // Hidden: Served at, Cancelled at
                };

                int infoY = 10;
                int rowH = 22;
                foreach (var (label, value) in infoItems)
                {
                    var lbl = new Label
                    {
                        Left = 14,
                        Top = infoY,
                        Width = 110,
                        Height = rowH,
                        Text = label,
                        Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                        ForeColor = Color.Gray
                    };
                    infoCard.Controls.Add(lbl);

                    var val = new Label
                    {
                        Left = 130,
                        Top = infoY,
                        Width = infoCard.Width - 144,
                        Height = rowH,
                        Text = value,
                        Font = new Font("Segoe UI", 9.5F)
                    };
                    infoCard.Controls.Add(val);

                    infoY += rowH;
                }

                detailPanel.Controls.Add(infoCard);
                y += infoCard.Height + 8;

                // Items header (taller)
                var itemsHeader = new Panel
                {
                    Left = 8,
                    Top = y,
                    Width = pnlOrderDetail.ClientSize.Width - 16,
                    Height = 38,
                    BackColor = Color.FromArgb(45, 55, 72)
                };
                var lblItemsH = new Label
                {
                    Left = 14,
                    Top = 8,
                    AutoSize = true,
                    Text = "🍹 Chi tiết món",
                    Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                    ForeColor = Color.White
                };
                itemsHeader.Controls.Add(lblItemsH);
                detailPanel.Controls.Add(itemsHeader);
                y += 42;

                // Order detail items
                foreach (var detail in details)
                {
                    var itemCard = CreateDetailItemCard(detail, pnlOrderDetail.ClientSize.Width - 16);
                    itemCard.Top = y;
                    itemCard.Left = 8;
                    detailPanel.Controls.Add(itemCard);
                    y += itemCard.Height + 8;
                }

                // Financial summary (taller)
                int summaryY = y + 8;
                var summaryCard = new Panel
                {
                    Left = 8,
                    Top = summaryY,
                    Width = pnlOrderDetail.ClientSize.Width - 16,
                    Height = 220,
                    BackColor = Color.White,
                    BorderStyle = BorderStyle.FixedSingle
                };

                var summaryLbl = new Label
                {
                    Left = 14,
                    Top = 10,
                    AutoSize = true,
                    Text = "💰 Thanh toán",
                    Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(45, 55, 72)
                };
                summaryCard.Controls.Add(summaryLbl);

                var totals = new List<(string Label, string Value, Color? Color)>
                {
                    ("Tạm tính:", FormatCurrency(order.Subtotal), null),
                    ("Giảm giá SP:", $"-{FormatCurrency(order.Discount)}", order.Discount > 0 ? Color.FromArgb(220, 53, 69) : (Color?)null),
                    ("Voucher:", $"-{FormatCurrency(order.VoucherDiscount)}", order.VoucherDiscount > 0 ? Color.FromArgb(220, 53, 69) : (Color?)null),
                    ("Thành viên:", $"-{FormatCurrency(order.MembershipDiscount)}", order.MembershipDiscount > 0 ? Color.FromArgb(220, 53, 69) : (Color?)null),
                };

                int ty = 40;
                foreach (var (label, value, color) in totals)
                {
                    var l = new Label
                    {
                        Left = 14,
                        Top = ty,
                        Width = 140,
                        Height = 26,
                        Text = label,
                        Font = new Font("Segoe UI", 10F),
                        ForeColor = Color.Gray
                    };
                    summaryCard.Controls.Add(l);

                    var v = new Label
                    {
                        Left = 160,
                        Top = ty,
                        Width = summaryCard.Width - 174,
                        Height = 26,
                        Text = value,
                        Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                        ForeColor = color ?? Color.FromArgb(45, 55, 72),
                        TextAlign = ContentAlignment.MiddleRight
                    };
                    summaryCard.Controls.Add(v);
                    ty += 28;
                }

                // Separator
                var sep = new Panel { Left = 14, Top = ty, Width = summaryCard.Width - 28, Height = 3, BackColor = Color.FromArgb(45, 55, 72) };
                summaryCard.Controls.Add(sep);
                ty += 8;

                // Total (bigger)
                var lblTotal = new Label
                {
                    Left = 14,
                    Top = ty,
                    Width = summaryCard.Width - 28,
                    Height = 32,
                    Text = $"TỔNG THANH TOÁN:  {FormatCurrency(order.Total)}",
                    Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(255, 193, 7),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                summaryCard.Controls.Add(lblTotal);
                ty += 36;

                // Payment method (bigger)
                var methodText = order.PaymentMethod == "cash" ? "💵 Tiền mặt" : order.PaymentMethod == "qr_code" ? "📱 QR Code" : order.PaymentMethod;
                var payStatusText = order.PaymentStatus == "completed" ? "✅ Đã thanh toán" : order.PaymentStatus;
                var lblPayMethod = new Label
                {
                    Left = 14,
                    Top = ty,
                    Width = summaryCard.Width / 2 - 8,
                    Height = 22,
                    Text = $"Phương thức: {methodText}",
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(45, 55, 72)
                };
                summaryCard.Controls.Add(lblPayMethod);

                var lblPayStatus = new Label
                {
                    Left = summaryCard.Width / 2 + 8,
                    Top = ty,
                    Width = summaryCard.Width / 2 - 22,
                    Height = 22,
                    Text = $"Trạng thái: {payStatusText}",
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                    ForeColor = order.PaymentStatus == "completed" ? Color.FromArgb(72, 187, 120) : Color.FromArgb(220, 53, 69),
                    TextAlign = ContentAlignment.MiddleRight
                };
                summaryCard.Controls.Add(lblPayStatus);

                // Notes (bigger)
                if (!string.IsNullOrWhiteSpace(order.Notes))
                {
                    ty += 26;
                    var lblNotes = new Label
                    {
                        Left = 14,
                        Top = ty,
                        Width = summaryCard.Width - 28,
                        Height = 22,
                        Text = $"📝 Ghi chú: {order.Notes}",
                        Font = new Font("Segoe UI", 9.5F, FontStyle.Italic),
                        ForeColor = Color.Gray
                    };
                    summaryCard.Controls.Add(lblNotes);
                }

                detailPanel.Controls.Add(summaryCard);

                // Action buttons for pending/served orders (bigger)
                if (order.Status is "pending" or "preparing" or "ready")
                {
                    int btnY = summaryY + summaryCard.Height + 16;
                    var btnResume = new Button
                    {
                        Left = 14,
                        Top = btnY,
                        Width = 160,
                        Height = 44,
                        Text = "▶️ Tiếp tục đơn",
                        Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                        FlatStyle = FlatStyle.Flat,
                        BackColor = Color.FromArgb(72, 187, 120),
                        ForeColor = Color.White
                    };
                    btnResume.FlatAppearance.BorderSize = 0;
                    btnResume.Click += (_, _) => ResumeOrder(order.Id);

                    var btnCancel = new Button
                    {
                        Left = 184,
                        Top = btnY,
                        Width = 140,
                        Height = 44,
                        Text = "❌ Hủy đơn",
                        Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                        FlatStyle = FlatStyle.Flat,
                        BackColor = Color.FromArgb(220, 53, 69),
                        ForeColor = Color.White
                    };
                    btnCancel.FlatAppearance.BorderSize = 0;
                    btnCancel.Click += async (_, _) => await CancelOrderAsync(order.Id);
                    detailPanel.Controls.Add(btnResume);
                    detailPanel.Controls.Add(btnCancel);
                }

                // Set scroll area to fit all content
                detailPanel.AutoScrollMinSize = new Size(0, y + 100);

                pnlOrderDetail.Controls.Clear();
                pnlOrderDetail.Controls.Add(lblDetailHeader);
                pnlOrderDetail.Controls.Add(detailPanel);
            }
            catch (Exception ex)
            {
                lblDetailHeader.Text = $"❌ Lỗi tải chi tiết: {ex.Message}";
            }
        }

        private Panel CreateDetailItemCard(OrderDetailItem detail, int cardWidth)
        {
            var cardHeight = 100 + (detail.Toppings.Count > 0 ? 28 * detail.Toppings.Count : 0);
            var card = new Panel
            {
                Width = cardWidth,
                Height = cardHeight,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Accent bar
            var accent = new Panel { Left = 0, Top = 0, Width = 6, Height = cardHeight, BackColor = Color.FromArgb(23, 162, 184) };
            card.Controls.Add(accent);

            // Product name (larger)
            var lblName = new Label
            {
                Left = 14,
                Top = 8,
                Width = cardWidth - 160,
                Height = 24,
                Text = detail.ProductName,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(45, 55, 72)
            };
            card.Controls.Add(lblName);

            // Options (larger)
            var options = $"{detail.Size} | Đường {detail.Sugar}% | Đá {detail.Ice}%";
            var lblOpts = new Label
            {
                Left = 14,
                Top = 32,
                Width = card.Width - 160,
                Height = 20,
                Text = options,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.Gray
            };
            card.Controls.Add(lblOpts);

            // Qty x UnitPrice (larger)
            var lblQtyPrice = new Label
            {
                Left = card.Width - 150,
                Top = 8,
                Width = 140,
                Height = 24,
                Text = $"{detail.Quantity} x {FormatCurrency(detail.UnitPrice)}",
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.Gray,
                TextAlign = ContentAlignment.MiddleRight
            };
            card.Controls.Add(lblQtyPrice);

            // Special instructions
            if (!string.IsNullOrWhiteSpace(detail.SpecialInstructions))
            {
                var lblSpecial = new Label
                {
                    Left = 14,
                    Top = 52,
                    Width = card.Width - 160,
                    Height = 20,
                    Text = $"⚠️ {detail.SpecialInstructions}",
                    Font = new Font("Segoe UI", 8.5F, FontStyle.Italic),
                    ForeColor = Color.FromArgb(255, 152, 0)
                };
                card.Controls.Add(lblSpecial);
            }

            // Notes
            if (!string.IsNullOrWhiteSpace(detail.Notes))
            {
                var topNote = string.IsNullOrWhiteSpace(detail.SpecialInstructions) ? 52 : 72;
                var lblNote = new Label
                {
                    Left = 14,
                    Top = topNote,
                    Width = card.Width - 160,
                    Height = 18,
                    Text = $"📝 {detail.Notes}",
                    Font = new Font("Segoe UI", 8F, FontStyle.Italic),
                    ForeColor = Color.Gray
                };
                card.Controls.Add(lblNote);
            }

            // Toppings (larger spacing)
            if (detail.Toppings.Count > 0)
            {
                int topY = 56;
                if (!string.IsNullOrWhiteSpace(detail.SpecialInstructions)) topY += 20;
                if (!string.IsNullOrWhiteSpace(detail.Notes)) topY += 18;

                foreach (var tp in detail.Toppings)
                {
                    var lblTp = new Label
                    {
                        Left = 24,
                        Top = topY,
                        Width = card.Width - 170,
                        Height = 22,
                        Text = $"+ {tp.Name}  x{tp.Quantity}  ({FormatCurrency(tp.Price)})",
                        Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                        ForeColor = Color.FromArgb(108, 117, 125)
                    };
                    card.Controls.Add(lblTp);
                    topY += 26;
                }
            }

            // Total (larger, right side bottom)
            var lblTotal = new Label
            {
                Left = card.Width - 150,
                Top = card.Height - 30,
                Width = 140,
                Height = 26,
                Text = FormatCurrency(detail.Subtotal),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(220, 53, 69),
                TextAlign = ContentAlignment.MiddleRight
            };
            card.Controls.Add(lblTotal);

            // Status badge (larger)
            var detailStatus = detail.Status switch
            {
                "pending" => "⏳ Chờ xử lý",
                "preparing" => "🍳 Đang pha chế",
                "ready" => "✅ Sẵn sàng phục vụ",
                "served" => "🍽 Đã phục vụ",
                "cancelled" => "❌ Đã hủy",
                _ => detail.Status
            };
            var pnlDs = new Panel
            {
                Left = 14,
                Top = card.Height - 28,
                BackColor = GetStatusColor(detail.Status),
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };
            var lblDs = new Label
            {
                Text = detailStatus,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Padding = new Padding(8, 3, 8, 3)
            };
            pnlDs.Controls.Add(lblDs);
            card.Controls.Add(pnlDs);

            return card;
        }

        private async Task<List<OrderDetailItem>> LoadOrderDetailsAsync(Guid orderId)
        {
            var details = new List<OrderDetailItem>();
            var detailIds = new List<Guid>();

            using var context = new PostgresContext();
            await using var conn = context.Database.GetDbConnection();
            if (conn.State != System.Data.ConnectionState.Open)
                await conn.OpenAsync();

            // 1. Load order details
            await using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
SELECT
    od.id,
    od.product_id,
    od.product_name,
    od.product_image,
    od.quantity,
    od.unit_price,
    od.topping_total,
    od.subtotal,
    COALESCE(od.size::text, 'M'),
    COALESCE(od.sugar::text, '100'),
    COALESCE(od.ice::text, '100'),
    COALESCE(od.status::text, 'pending'),
    COALESCE(od.notes, ''),
    COALESCE(od.special_instructions, '')
FROM order_details od
WHERE od.order_id = @orderId
ORDER BY od.created_at;";

                var p = cmd.CreateParameter();
                p.ParameterName = "@orderId";
                p.Value = orderId;
                cmd.Parameters.Add(p);

                await using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var d = new OrderDetailItem
                    {
                        Id = reader.GetGuid(0),
                        ProductId = reader.GetGuid(1),
                        ProductName = reader.GetString(2),
                        ProductImage = reader.IsDBNull(3) ? null : reader.GetString(3),
                        Quantity = reader.GetInt32(4),
                        UnitPrice = reader.GetDecimal(5),
                        ToppingTotal = reader.GetDecimal(6),
                        Subtotal = reader.GetDecimal(7),
                        Size = reader.GetString(8),
                        Sugar = reader.GetString(9),
                        Ice = reader.GetString(10),
                        Status = reader.GetString(11),
                        Notes = reader.IsDBNull(12) ? null : reader.GetString(12),
                        SpecialInstructions = reader.IsDBNull(13) ? null : reader.GetString(13)
                    };
                    details.Add(d);
                    detailIds.Add(d.Id);
                }
            } // reader fully disposed here

            // 2. Load ALL toppings in ONE query (no N+1)
            if (detailIds.Count > 0)
            {
                await using (var topCmd = conn.CreateCommand())
                {
                    var idList = string.Join(",", detailIds.Select((id, i) => $"@id{i}"));
                    topCmd.CommandText = $@"
SELECT order_detail_id, topping_id, topping_name, COALESCE(quantity, 1), COALESCE(price, 0)
FROM order_toppings
WHERE order_detail_id IN ({idList})
ORDER BY order_detail_id, created_at;";

                    for (int i = 0; i < detailIds.Count; i++)
                    {
                        var p = topCmd.CreateParameter();
                        p.ParameterName = $"@id{i}";
                        p.Value = detailIds[i];
                        topCmd.Parameters.Add(p);
                    }

                    await using var topReader = await topCmd.ExecuteReaderAsync();
                    while (await topReader.ReadAsync())
                    {
                        var detailId = topReader.GetGuid(0);
                        var detail = details.FirstOrDefault(d => d.Id == detailId);
                        if (detail != null)
                        {
                            detail.Toppings.Add(new ToppingDetail
                            {
                                ToppingId = topReader.GetGuid(1),
                                Name = topReader.GetString(2),
                                Quantity = topReader.GetInt32(3),
                                Price = topReader.GetDecimal(4)
                            });
                        }
                    }
                }
            }

            return details;
        }

        private async Task CancelOrderAsync(Guid orderId)
        {
            var result = MessageBox.Show("Xác nhận hủy đơn hàng này?\nĐơn đã hủy không thể khôi phục.", "Xác nhận hủy", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes) return;

            try
            {
                using var context = new PostgresContext();
                var affected = await context.Database.ExecuteSqlInterpolatedAsync($@"
UPDATE orders
SET status = 'cancelled'::order_status,
    cancelled_at = NOW(),
    updated_at = NOW()
WHERE id = {orderId}
  AND status IN ('pending', 'preparing', 'ready');
");

                if (affected > 0)
                {
                    // Free associated table
                    var order = await context.Orders.AsNoTracking().FirstOrDefaultAsync(o => o.Id == orderId);
                    if (order != null && order.TableId.HasValue)
                    {
                        await context.Database.ExecuteSqlInterpolatedAsync($@"
UPDATE tables
SET status = 'available'::table_status,
    updated_at = NOW()
WHERE id = {order.TableId.Value};
");
                    }

                    MessageBox.Show("Đã hủy đơn hàng.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadDashboardAsync();
                    await LoadOrdersAsync();
                    if (_selectedOrderId == orderId)
                        _selectedOrderId = null;
                }
                else
                {
                    MessageBox.Show("Đơn hàng đã được xử lý hoặc không tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể hủy đơn.\n{ex.InnerException?.Message ?? ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string FormatCurrency(decimal amount) => $"{amount:N0}đ";
    }
}
