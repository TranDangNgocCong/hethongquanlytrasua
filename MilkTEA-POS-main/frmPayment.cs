using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MilkTeaPOS
{
    public sealed class frmPayment : Form
    {
        private readonly decimal _total;
        private readonly string? _orderNumber;
        private readonly TextBox _txtReceived;
        private readonly ComboBox _cboMethod;
        private readonly CheckBox _chkPrint;
        private readonly Label _lblChange;
        private readonly Label _lblPreviewCount;
        private readonly PictureBox _picQR;
        private readonly Panel _pnlQR;
        private readonly Label _lblQRTip;
        private readonly List<Control> _controlsBelowQR = new();
        private readonly Dictionary<Control, int> _originalTops = new();
        private const string VietQRBankBin = "970422"; // MB Bank
        private const string VietQRAccount = "0326798878";
        private const string VietQRAccountName = "HUYNH NHAT HAO";

        public sealed class PaymentLineItem
        {
            public string ProductName { get; set; } = string.Empty;
            public string Options { get; set; } = string.Empty;
            public List<string> Toppings { get; set; } = new();
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal LineTotal { get; set; }
        }

        public decimal ReceivedAmount { get; private set; }
        public string? SelectedPaymentMethod { get; private set; }
        public bool ShouldPrintReceipt => _chkPrint.Checked;

        public frmPayment(
            decimal total,
            List<PaymentLineItem> lineItems,
            string? customerPhone,
            string? customerName,
            int currentPoints,
            string? voucherCode,
            decimal voucherDiscount = 0m,
            string? orderNumber = null)
        {
            _total = total;
            _orderNumber = orderNumber ?? $"HD-{DateTime.Now:yyMMddHHmm}";

            Text = "💰 Thanh toán";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(480, 500);
            BackColor = Color.FromArgb(247, 249, 252);

            // === HEADER ===
            var pnlHeader = new Panel
            {
                Left = 0,
                Top = 0,
                Width = 480,
                Height = 60,
                BackColor = Color.FromArgb(45, 55, 72)
            };

            var lblTotalHeader = new Label
            {
                Left = 16,
                Top = 6,
                Width = 448,
                Height = 28,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                ForeColor = Color.FromArgb(200, 210, 220),
                Text = "Tổng thanh toán"
            };

            var lblTotalValue = new Label
            {
                Left = 16,
                Top = 28,
                Width = 448,
                Height = 28,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(255, 193, 7),
                Text = $"{total:N0}đ"
            };

            pnlHeader.Controls.Add(lblTotalHeader);
            pnlHeader.Controls.Add(lblTotalValue);
            Controls.Add(pnlHeader);

            // === CUSTOMER INFO ===
            var customerInfo = $"{customerName ?? "Khách lẻ"}";
            if (!string.IsNullOrWhiteSpace(customerPhone))
                customerInfo += $" - {customerPhone}";
            if (currentPoints > 0)
                customerInfo += $" | Điểm: {currentPoints:N0}";

            var lblCustomer = new Label
            {
                Left = 16,
                Top = 68,
                Width = 448,
                Height = 20,
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                ForeColor = Color.FromArgb(108, 117, 125),
                Text = customerInfo
            };
            Controls.Add(lblCustomer);

            int currentTop = 88;

            // === VOUCHER INFO (with discount amount) ===
            if (!string.IsNullOrWhiteSpace(voucherCode) && voucherDiscount > 0)
            {
                var pnlVoucher = new Panel
                {
                    Left = 16,
                    Top = currentTop,
                    Width = 448,
                    Height = 24,
                    BackColor = Color.FromArgb(240, 253, 244)
                };

                var lblVoucherLabel = new Label
                {
                    Left = 8,
                    Top = 2,
                    Width = 200,
                    Height = 20,
                    Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                    ForeColor = Color.FromArgb(22, 163, 74),
                    Text = $"🎫 {voucherCode}"
                };

                var lblVoucherDiscount = new Label
                {
                    Left = 220,
                    Top = 2,
                    Width = 220,
                    Height = 20,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(22, 163, 74),
                    Text = $"Giảm: -{voucherDiscount:N0}đ",
                    TextAlign = ContentAlignment.MiddleRight
                };

                pnlVoucher.Controls.Add(lblVoucherLabel);
                pnlVoucher.Controls.Add(lblVoucherDiscount);
                Controls.Add(pnlVoucher);
                currentTop += 32;
            }
            else if (!string.IsNullOrWhiteSpace(voucherCode))
            {
                var lblVoucher = new Label
                {
                    Left = 16,
                    Top = currentTop,
                    Width = 448,
                    Height = 20,
                    Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                    ForeColor = Color.FromArgb(72, 187, 120),
                    Text = $"Voucher: {voucherCode}"
                };
                Controls.Add(lblVoucher);
                currentTop += 28;
            }

            // === LINE ITEMS PREVIEW ===
            int previewTop = string.IsNullOrWhiteSpace(voucherCode) ? 96 : 116;
            int previewCount = lineItems.Count;
            var lstPreview = new ListBox
            {
                Left = 16,
                Top = previewTop,
                Width = 448,
                Height = 120,
                Font = new Font("Segoe UI", 9F),
                BorderStyle = BorderStyle.None,
                BackColor = Color.White
            };

            var displayCount = Math.Min(previewCount, 10);
            foreach (var item in lineItems.Take(displayCount))
            {
                var toppingInfo = item.Toppings.Count > 0 ? $" (+{string.Join(", ", item.Toppings)})" : "";
                lstPreview.Items.Add($"{item.ProductName}  x{item.Quantity}  {item.LineTotal:N0}đ{toppingInfo}");
            }

            _lblPreviewCount = new Label
            {
                Left = 16,
                Top = previewTop + 122,
                Width = 448,
                Height = 16,
                Font = new Font("Segoe UI", 8F, FontStyle.Italic),
                ForeColor = Color.Gray,
                Visible = previewCount > displayCount
            };
            if (_lblPreviewCount.Visible)
                _lblPreviewCount.Text = $"...và {previewCount - displayCount} món khác";

            Controls.Add(lstPreview);
            Controls.Add(_lblPreviewCount);

            // === PAYMENT METHOD ===
            int methodTop = _lblPreviewCount.Visible ? previewTop + 144 : previewTop + 126;

            var lblMethod = new Label
            {
                Left = 16,
                Top = methodTop,
                Width = 120,
                Height = 24,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(45, 55, 72),
                Text = "Phương thức:"
            };
            Controls.Add(lblMethod);

            _cboMethod = new ComboBox
            {
                Left = 140,
                Top = methodTop - 1,
                Width = 180,
                Height = 26,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9F)
            };
            _cboMethod.Items.AddRange(new object[]
            {
                "💵 Tiền mặt",
                "📱 QR Code"
            });
            _cboMethod.SelectedIndex = 0;
            _cboMethod.SelectedIndexChanged += CboMethod_SelectedIndexChanged;
            Controls.Add(_cboMethod);

            // === QR CODE PANEL ===
            int qrTop = methodTop + 34;

            _pnlQR = new Panel
            {
                Left = 16,
                Top = qrTop,
                Width = 448,
                Height = 220,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Visible = false
            };

            _picQR = new PictureBox
            {
                Left = 124,
                Top = 10,
                Width = 200,
                Height = 200,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.White
            };

            _lblQRTip = new Label
            {
                Left = 10,
                Top = 10,
                Width = 110,
                Height = 200,
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                ForeColor = Color.FromArgb(45, 55, 72),
                Text = "📱 Quét mã QR\n\nNgân hàng: MB\nSTK: 0326798878\nChủ TK: HUYNH NHAT HAO",
                TextAlign = ContentAlignment.MiddleLeft
            };

            var lblQRNote = new Label
            {
                Left = 334,
                Top = 10,
                Width = 104,
                Height = 200,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Italic),
                ForeColor = Color.FromArgb(255, 193, 7),
                Text = "⚠️ Vui lòng quét mã\nvà chờ xác nhận\nchuyển khoản\nthành công trước\nkhi nhấn Xác nhận",
                TextAlign = ContentAlignment.MiddleLeft
            };

            _pnlQR.Controls.Add(_picQR);
            _pnlQR.Controls.Add(_lblQRTip);
            _pnlQR.Controls.Add(lblQRNote);
            Controls.Add(_pnlQR);

            // === RECEIVED AMOUNT (initial position for cash mode) ===
            int baseTop = qrTop + 6; // just below method combo

            var lblReceived = new Label
            {
                Left = 16,
                Top = baseTop,
                Width = 120,
                Height = 24,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(45, 55, 72),
                Text = "Tiền nhận:"
            };
            _controlsBelowQR.Add(lblReceived);
            Controls.Add(lblReceived);

            _txtReceived = new TextBox
            {
                Left = 140,
                Top = baseTop - 1,
                Width = 180,
                Height = 26,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Text = total.ToString("N0"),
                TextAlign = HorizontalAlignment.Right
            };
            _txtReceived.TextChanged += TxtReceived_TextChanged;
            _txtReceived.KeyPress += TxtReceived_KeyPress;
            _controlsBelowQR.Add(_txtReceived);
            Controls.Add(_txtReceived);

            // === CHANGE AMOUNT ===
            var changeTop = baseTop + 32;
            _lblChange = new Label
            {
                Left = 16,
                Top = changeTop,
                Width = 448,
                Height = 22,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(72, 187, 120),
                Text = $"Tiền thừa: 0đ"
            };
            _controlsBelowQR.Add(_lblChange);
            Controls.Add(_lblChange);

            // === PRINT CHECKBOX ===
            var chkTop = changeTop + 28;

            _chkPrint = new CheckBox
            {
                Left = 16,
                Top = chkTop,
                Width = 200,
                Height = 24,
                Font = new Font("Segoe UI", 9F),
                Text = "In hóa đơn",
                Checked = true
            };
            _controlsBelowQR.Add(_chkPrint);
            Controls.Add(_chkPrint);

            // === BUTTONS ===
            int btnTop = chkTop + 34;

            var btnOk = new Button
            {
                Left = 250,
                Top = btnTop,
                Width = 100,
                Height = 36,
                Text = "✓ Xác nhận",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(72, 187, 120),
                ForeColor = Color.White
            };
            btnOk.FlatAppearance.BorderSize = 0;
            btnOk.Click += BtnOk_Click;
            _controlsBelowQR.Add(btnOk);
            Controls.Add(btnOk);

            var btnCancel = new Button
            {
                Left = 356,
                Top = btnTop,
                Width = 100,
                Height = 36,
                Text = "✕ Hủy",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += (_, _) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };
            _controlsBelowQR.Add(btnCancel);
            Controls.Add(btnCancel);

            // === KEYBOARD SHORTCUTS ===
            AcceptButton = btnOk;
            CancelButton = btnCancel;

            // Store original positions for shift calculations
            foreach (var ctrl in _controlsBelowQR)
            {
                _originalTops[ctrl] = ctrl.Top;
            }

            // Initial change calculation
            UpdateChangeDisplay();
        }

        private void CboMethod_SelectedIndexChanged(object? sender, EventArgs e)
        {
            var isQR = _cboMethod.SelectedIndex == 1;

            // Show/hide QR panel
            _pnlQR.Visible = isQR;

            // Shift controls based on QR visibility
            var shift = isQR ? _pnlQR.Height + 10 : 0;
            foreach (var ctrl in _controlsBelowQR)
            {
                if (_originalTops.TryGetValue(ctrl, out var origTop))
                {
                    ctrl.Top = origTop + shift;
                }
            }

            // Resize form height to fit content
            var baseHeight = 500;
            var qrExtra = isQR ? _pnlQR.Height + 10 : 0;
            ClientSize = new Size(480, baseHeight + qrExtra);

            // Cash: enable input; QR: auto-fill and disable
            _txtReceived.Enabled = !isQR;
            _txtReceived.ReadOnly = isQR;

            if (isQR)
            {
                _txtReceived.Text = _total.ToString("N0");
                _txtReceived.BackColor = Color.FromArgb(240, 243, 248);
                _ = LoadVietQRCodeAsync();
            }
            else
            {
                _txtReceived.BackColor = Color.White;
            }

            UpdateChangeDisplay();
        }

        private async Task LoadVietQRCodeAsync()
        {
            try
            {
                var amount = (long)_total;
                var description = _orderNumber ?? $"Thanh toan don hang";
                var qrUrl = $"https://img.vietqr.io/image/{VietQRBankBin}-{VietQRAccount}-compact2.png?amount={amount}&addInfo={Uri.EscapeDataString(description)}&accountName={Uri.EscapeDataString(VietQRAccountName)}";

                using var client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(10);
                var bytes = await client.GetByteArrayAsync(qrUrl);
                using var ms = new System.IO.MemoryStream(bytes);
                var qrImage = Image.FromStream(ms);

                _picQR.Image = qrImage;
                _picQR.Visible = true;
            }
            catch
            {
                // If QR fails, show placeholder
                _picQR.Visible = false;
                _lblQRTip.Text += $"\n\nLỗi tải QR.\nVui lòng chuyển khoản\nthủ công.";
            }
        }

        private void TxtReceived_TextChanged(object? sender, EventArgs e)
        {
            UpdateChangeDisplay();
        }

        private void TxtReceived_KeyPress(object? sender, KeyPressEventArgs e)
        {
            // Allow only digits, decimal separator, backspace, and Ctrl shortcuts
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',')
            {
                e.Handled = true;
            }
        }

        private void UpdateChangeDisplay()
        {
            if (decimal.TryParse(_txtReceived.Text.Replace(",", "").Replace(".", ""), NumberStyles.Number, CultureInfo.InvariantCulture, out var amount))
            {
                var change = amount - _total;
                if (change >= 0)
                {
                    _lblChange.Text = $"Tiền thừa: {change:N0}đ";
                    _lblChange.ForeColor = Color.FromArgb(72, 187, 120);
                }
                else
                {
                    _lblChange.Text = $"Thiếu: {Math.Abs(change):N0}đ";
                    _lblChange.ForeColor = Color.FromArgb(220, 53, 69);
                }
            }
            else
            {
                _lblChange.Text = "Tiền thừa: 0đ";
                _lblChange.ForeColor = Color.Gray;
            }
        }

        private void BtnOk_Click(object? sender, EventArgs e)
        {
            var rawText = _txtReceived.Text.Replace(",", "").Replace(".", "");
            if (!decimal.TryParse(rawText, NumberStyles.Number, CultureInfo.InvariantCulture, out var amount))
            {
                MessageBox.Show("Số tiền nhận không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (amount < _total)
            {
                MessageBox.Show("Số tiền nhận chưa đủ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ReceivedAmount = amount;

            // Map Vietnamese display text to DB enum values
            var methodMap = new[] { "cash", "qr_code" };
            SelectedPaymentMethod = methodMap[_cboMethod.SelectedIndex];

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
