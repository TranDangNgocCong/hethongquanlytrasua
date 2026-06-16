using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using MilkTeaPOS.Models;

namespace MilkTeaPOS
{
    public partial class frmAuditLog : Form
    {
        #region Constants & Fields

        private DateTime _startDate = DateTime.UtcNow.Date.AddDays(-7);
        private DateTime _endDate = DateTime.UtcNow.Date.AddDays(1);

        // Cached fonts
        private readonly Font _fontHeader = new Font("Segoe UI", 10F, FontStyle.Bold);
        private readonly Font _fontValue = new Font("Segoe UI", 9F);
        private readonly Font _fontActionInsert = new Font("Segoe UI", 9F, FontStyle.Bold);
        private readonly Font _fontActionUpdate = new Font("Segoe UI", 9F, FontStyle.Bold);
        private readonly Font _fontActionDelete = new Font("Segoe UI", 9F, FontStyle.Bold);

        #endregion

        #region Constructor & Initialization

        public frmAuditLog()
        {
            InitializeComponent();
            ConfigureDataGridView();
            InitializeDateRange();
            InitializeFilterCombos();
            LoadAuditLogs();
        }

        private void ConfigureDataGridView()
        {
            dgvAuditLogs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAuditLogs.RowTemplate.Height = 40;
        }

        private void InitializeDateRange()
        {
            dtpStartDate.Value = _startDate.ToLocalTime();
            dtpEndDate.Value = _endDate.ToLocalTime();
        }

        private async void InitializeFilterCombos()
        {
            try
            {
                using var context = new PostgresContext();

                // Load actions
                var actions = await context.AuditLogs
                    .Select(a => a.Action)
                    .Distinct()
                    .OrderBy(a => a)
                    .ToListAsync();

                cbAction.Items.Clear();
                cbAction.Items.Add("Tất cả");
                foreach (var action in actions)
                {
                    cbAction.Items.Add(action);
                }
                cbAction.SelectedIndex = 0;

                // Load tables
                var tables = await context.AuditLogs
                    .Select(a => a.TableName)
                    .Distinct()
                    .OrderBy(t => t)
                    .ToListAsync();

                cbTable.Items.Clear();
                cbTable.Items.Add("Tất cả");
                foreach (var table in tables)
                {
                    cbTable.Items.Add(table);
                }
                cbTable.SelectedIndex = 0;

                // Load users
                var users = await context.Users
                    .Where(u => u.AuditLogs.Any())
                    .OrderBy(u => u.Username)
                    .ToListAsync();

                cbUser.Items.Clear();
                cbUser.Items.Add(new UserComboItem { Id = null, DisplayName = "Tất cả" });
                foreach (var user in users)
                {
                    cbUser.Items.Add(new UserComboItem { Id = user.Id, DisplayName = user.Username ?? "Unknown" });
                }
                cbUser.SelectedIndex = 0;
                cbUser.DisplayMember = "DisplayName";
                cbUser.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                // Log error for debugging
                System.Diagnostics.Debug.WriteLine($"[AUDIT LOG] Failed to load filters: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"[AUDIT LOG] Stack trace: {ex.StackTrace}");
                
                // Show user-friendly error
                MessageBox.Show(
                    "⚠️ Không thể tải bộ lọc!\n\n" +
                    "💡 Ứng dụng sẽ tiếp tục hoạt động bình thường.\n" +
                    "🔄 Nhấn 'Làm mới' để thử lại.",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        #endregion

        #region Event Handlers

        private void btnFilter_Click(object sender, EventArgs e)
        {
            _startDate = dtpStartDate.Value.Date.ToUniversalTime();
            _endDate = dtpEndDate.Value.Date.AddDays(1).ToUniversalTime();
            LoadAuditLogs();
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            _startDate = DateTime.UtcNow.Date;
            _endDate = DateTime.UtcNow.Date.AddDays(1);
            dtpStartDate.Value = _startDate.ToLocalTime();
            dtpEndDate.Value = _endDate.ToLocalTime();
            LoadAuditLogs();
        }

        private void btnWeek_Click(object sender, EventArgs e)
        {
            _startDate = DateTime.UtcNow.Date.AddDays(-7);
            _endDate = DateTime.UtcNow.Date.AddDays(1);
            dtpStartDate.Value = _startDate.ToLocalTime();
            dtpEndDate.Value = _endDate.ToLocalTime();
            LoadAuditLogs();
        }

        private void btnMonth_Click(object sender, EventArgs e)
        {
            _startDate = DateTime.UtcNow.Date.AddDays(-30);
            _endDate = DateTime.UtcNow.Date.AddDays(1);
            dtpStartDate.Value = _startDate.ToLocalTime();
            dtpEndDate.Value = _endDate.ToLocalTime();
            LoadAuditLogs();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadAuditLogs();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnFilter_Click(sender, e);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportToCsv();
        }

        private void btnClearLogs_Click(object sender, EventArgs e)
        {
            ClearOldLogs();
        }

        private void dgvAuditLogs_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                ViewLogDetails(e.RowIndex);
            }
        }

        private void dgvAuditLogs_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvAuditLogs.Columns[e.ColumnIndex].Name == "Action")
            {
                var actionText = e.Value?.ToString() ?? "";
                if (actionText.Contains("INSERT") || actionText.Contains("➕"))
                {
                    e.CellStyle.ForeColor = Color.FromArgb(72, 187, 120);
                    e.CellStyle.Font = _fontActionInsert;
                }
                else if (actionText.Contains("UPDATE") || actionText.Contains("✏️"))
                {
                    e.CellStyle.ForeColor = Color.FromArgb(255, 193, 7);
                    e.CellStyle.Font = _fontActionUpdate;
                }
                else if (actionText.Contains("DELETE") || actionText.Contains("🗑️"))
                {
                    e.CellStyle.ForeColor = Color.FromArgb(220, 53, 69);
                    e.CellStyle.Font = _fontActionDelete;
                }
                else if (actionText.Contains("LOGIN") || actionText.Contains("🔑"))
                {
                    e.CellStyle.ForeColor = Color.FromArgb(52, 152, 219);
                }
                else if (actionText.Contains("LOGOUT") || actionText.Contains("🚪"))
                {
                    e.CellStyle.ForeColor = Color.FromArgb(149, 165, 166);
                }
            }
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

        #region Data Loading

        private async void LoadAuditLogs()
        {
            try
            {
                ShowLoading(true);

                using var context = new PostgresContext();

                var query = context.AuditLogs
                    .Include(a => a.User)
                    .AsNoTracking()
                    .Where(a => a.CreatedAt >= _startDate && a.CreatedAt < _endDate);

                // Apply filters
                if (cbAction.SelectedItem != null && cbAction.SelectedItem.ToString() != "Tất cả")
                {
                    var actionFilter = cbAction.SelectedItem.ToString();
                    query = query.Where(a => a.Action == actionFilter);
                }

                if (cbTable.SelectedItem != null && cbTable.SelectedItem.ToString() != "Tất cả")
                {
                    var tableFilter = cbTable.SelectedItem.ToString();
                    query = query.Where(a => a.TableName == tableFilter);
                }

                if (cbUser.SelectedItem is UserComboItem selectedUser && selectedUser.Id.HasValue)
                {
                    query = query.Where(a => a.UserId == selectedUser.Id);
                }

                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    var searchText = txtSearch.Text.Trim().ToLower();
                    query = query.Where(a =>
                        (a.User != null && a.User.Username != null && a.User.Username.ToLower().Contains(searchText)) ||
                        a.Action.ToLower().Contains(searchText) ||
                        a.TableName.ToLower().Contains(searchText) ||
                        (a.IpAddress != null && a.IpAddress.Contains(searchText)));
                }

                var logs = await query
                    .OrderByDescending(a => a.CreatedAt)
                    .Take(500)
                    .ToListAsync();

                dgvAuditLogs.Rows.Clear();
                foreach (var log in logs)
                {
                    var actionEmoji = log.Action.ToLower() switch
                    {
                        "insert" or "create" or "add" => "➕",
                        "update" or "edit" or "modify" => "✏️",
                        "delete" or "remove" => "🗑️",
                        "login" => "🔑",
                        "logout" => "🚪",
                        _ => "📝"
                    };

                    var actionText = $"{actionEmoji} {log.Action}";

                    var actionColor = log.Action.ToLower() switch
                    {
                        "insert" or "create" or "add" => Color.FromArgb(72, 187, 120),
                        "update" or "edit" or "modify" => Color.FromArgb(255, 193, 7),
                        "delete" or "remove" => Color.FromArgb(220, 53, 69),
                        "login" => Color.FromArgb(52, 152, 219),
                        "logout" => Color.FromArgb(149, 165, 166),
                        _ => Color.Gray
                    };

                    var userName = log.User?.Username ?? (log.UserId.HasValue ? $"User #{log.UserId.Value.ToString().Substring(0, 8)}..." : "🤖 System");
                    var timeStr = log.CreatedAt?.ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss") ?? "--";
                    var recordStr = log.RecordId.HasValue ? log.RecordId.Value.ToString().Substring(0, 8) + "..." : "-";

                    var rowIndex = dgvAuditLogs.Rows.Add(
                        timeStr,
                        userName,
                        log.Action.ToUpper(),
                        log.TableName,
                        recordStr,
                        actionText,
                        log.IpAddress ?? "-"
                    );

                    // Set action color
                    var row = dgvAuditLogs.Rows[rowIndex];
                    row.Cells["Action"].Style.ForeColor = actionColor;
                    row.Cells["Action"].Style.Font = log.Action.ToLower() switch
                    {
                        "delete" or "remove" => _fontActionDelete,
                        "insert" or "create" or "add" => _fontActionInsert,
                        "update" or "edit" or "modify" => _fontActionUpdate,
                        _ => _fontValue
                    };

                    // Store full log for details view
                    row.Tag = log;
                }

                // Update stats
                lblTotalLogs.Text = $"📊 Tổng: {logs.Count} bản ghi";
                lblTotalLogs.Font = _fontHeader;

                if (logs.Count == 0)
                {
                    dgvAuditLogs.Rows.Add("", "", "", "💤 Không có dữ liệu", "", "", "");
                    dgvAuditLogs.Rows[0].DefaultCellStyle.ForeColor = Color.Gray;
                    dgvAuditLogs.Rows[0].DefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Italic);
                }
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

        #endregion

        #region Export & Clear

        private void ExportToCsv()
        {
            try
            {
                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "CSV File|*.csv|All Files|*.*",
                    Title = "Xuất audit log",
                    FileName = $"AuditLog_{_startDate:yyyyMMdd}_{_endDate:yyyyMMdd}.csv"
                };

                if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

                using (var writer = new System.IO.StreamWriter(saveFileDialog.FileName, false, System.Text.Encoding.UTF8))
                {
                    writer.WriteLine("THỜI GIAN,NGƯỜI DÙNG,HÀNH ĐỘNG,BẢNG,RECORD ID,IP ADDRESS");
                    
                    foreach (DataGridViewRow row in dgvAuditLogs.Rows)
                    {
                        if (row.Tag is AuditLog log)
                        {
                            var userName = log.User?.Username ?? "System";
                            writer.WriteLine($"{log.CreatedAt},{userName},{log.Action},{log.TableName},{log.RecordId},{log.IpAddress}");
                        }
                    }
                }

                MessageBox.Show("✅ Xuất audit log thành công!\n\nFile: " + saveFileDialog.FileName,
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi xuất file:\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void ClearOldLogs()
        {
            var result = MessageBox.Show(
                "🗑️ Bạn có chắc muốn xóa các audit log cũ hơn 30 ngày?\n\n" +
                "⚠️ Hành động này không thể hoàn tác!\n\n" +
                "💡 Nhấn Yes để xóa hoặc No để hủy.",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (result != DialogResult.Yes) return;

            try
            {
                ShowLoading(true);

                using var context = new PostgresContext();
                var cutoffDate = DateTime.UtcNow.AddDays(-30);

                var oldLogs = await context.AuditLogs
                    .Where(a => a.CreatedAt < cutoffDate)
                    .ToListAsync();

                context.AuditLogs.RemoveRange(oldLogs);
                await context.SaveChangesAsync();

                MessageBox.Show($"✅ Đã xóa {oldLogs.Count} audit log cũ!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadAuditLogs();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi xóa log:\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ShowLoading(false);
            }
        }

        #endregion

        #region Log Details

        private void ViewLogDetails(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= dgvAuditLogs.Rows.Count) return;
            if (dgvAuditLogs.Rows[rowIndex].Tag is not AuditLog log) return;

            var detailForm = new Form
            {
                Text = $"📋 Chi tiết: {log.Action.ToUpper()} - {log.TableName}",
                Size = new Size(950, 800),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = Color.FromArgb(247, 249, 252)
            };

            // Header panel với gradient effect
            var actionColor = GetActionColor(log.Action);
            var pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 110,
                BackColor = actionColor,
                Padding = new Padding(25, 20, 25, 20)
            };

            // Icon circle
            var pnlIcon = new Panel
            {
                Size = new Size(50, 50),
                BackColor = Color.FromArgb(255, 255, 255, 30),
                BorderStyle = BorderStyle.None
            };
            pnlIcon.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using var brush = new SolidBrush(Color.FromArgb(255, 255, 255, 50));
                e.Graphics.FillEllipse(brush, new Rectangle(0, 0, 50, 50));
            };

            var lblIcon = new Label
            {
                Text = GetActionEmoji(log.Action),
                Font = new Font("Segoe UI Emoji", 24F),
                ForeColor = Color.White,
                Location = new Point(10, 5),
                Size = new Size(30, 40),
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlIcon.Controls.Add(lblIcon);

            var lblHeaderTitle = new Label
            {
                Text = $"{log.Action.ToUpper()} - {log.TableName}",
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(65, 5),
                Size = new Size(800, 40),
                TextAlign = ContentAlignment.MiddleLeft
            };

            var lblHeaderTime = new Label
            {
                Text = $"⏰ {log.CreatedAt?.ToLocalTime():dd/MM/yyyy HH:mm:ss}  •  👤 {log.User?.Username ?? "System"}  •  🌐 {log.IpAddress ?? "-"}",
                Font = new Font("Segoe UI", 11F),
                ForeColor = Color.FromArgb(240, 248, 255),
                Location = new Point(65, 48),
                Size = new Size(850, 25),
                TextAlign = ContentAlignment.MiddleLeft
            };

            pnlHeader.Controls.Add(pnlIcon);
            pnlHeader.Controls.Add(lblHeaderTitle);
            pnlHeader.Controls.Add(lblHeaderTime);

            // Scrollable Flow Layout Panel
            var flowPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Padding = new Padding(25, 20, 25, 25),
                BackColor = Color.FromArgb(247, 249, 252)
            };

            // Add Info Card với shadow
            flowPanel.Controls.Add(CreateDetailCard("📌 Thông tin cơ bản", new[]
            {
                ("⏰ Thời gian", log.CreatedAt?.ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss") ?? "-"),
                ("👤 Người dùng", log.User?.Username ?? $"User #{log.UserId?.ToString().Substring(0, 8)}"),
                ("🎯 Hành động", log.Action.ToUpper()),
                ("📊 Bảng", log.TableName),
                ("🔑 Record ID", log.RecordId?.ToString() ?? "-"),
                ("🌐 IP Address", log.IpAddress ?? "-"),
            }, actionColor));

            // Add Old Values
            var oldValues = FormatJson(log.OldValues);
            if (oldValues != "(Không có dữ liệu)")
            {
                flowPanel.Controls.Add(CreateJsonSection("📝 Giá trị TRƯỚC (OLD)", oldValues, Color.FromArgb(255, 193, 7)));
            }

            // Add New Values
            var newValues = FormatJson(log.NewValues);
            if (newValues != "(Không có dữ liệu)")
            {
                flowPanel.Controls.Add(CreateJsonSection("📝 Giá trị SAU (NEW)", newValues, Color.FromArgb(72, 187, 120)));
            }

            // Footer Panel
            var pnlFooter = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 55,
                BackColor = Color.White
            };

            var btnClose = new Button
            {
                Text = "Đóng",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Size = new Size(140, 38),
                Anchor = AnchorStyles.Right
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => detailForm.Close();

            pnlFooter.Controls.Add(btnClose);
            pnlFooter.Resize += (s, e) => btnClose.Location = new Point(pnlFooter.Width - 160, 9);

            detailForm.Controls.Add(flowPanel);
            detailForm.Controls.Add(pnlFooter);
            detailForm.Controls.Add(pnlHeader);

            detailForm.ShowDialog(this);
        }

        // Helper tạo Card thông tin với shadow effect
        private Panel CreateDetailCard(string title, (string label, string value)[] items, Color accentColor)
        {
            int cardHeight = 50 + items.Length * 40;
            var card = new Panel
            {
                Size = new Size(850, cardHeight),
                BackColor = Color.White,
                Margin = new Padding(0, 0, 0, 18),
                BorderStyle = BorderStyle.None
            };

            // Shadow effect (giả lập bằng border)
            card.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using var shadowPen = new Pen(Color.FromArgb(20, 0, 0, 0), 2);
                var rect = new Rectangle(2, 2, card.Width - 4, card.Height - 4);
                e.Graphics.DrawRectangle(shadowPen, rect);
            };

            // Thanh màu bên trái (accent bar)
            var accentBar = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(5, cardHeight),
                BackColor = accentColor
            };
            card.Controls.Add(accentBar);

            // Icon nền
            var pnlIconBg = new Panel
            {
                Location = new Point(15, 10),
                Size = new Size(35, 35),
                BackColor = Color.FromArgb(255, 255, 255, 20)
            };
            pnlIconBg.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using var brush = new SolidBrush(Color.FromArgb(30, accentColor.R, accentColor.G, accentColor.B));
                e.Graphics.FillEllipse(brush, new Rectangle(0, 0, 35, 35));
            };
            card.Controls.Add(pnlIconBg);

            // Tiêu đề card
            var lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                ForeColor = accentColor,
                Location = new Point(55, 12),
                Size = new Size(780, 28),
                TextAlign = ContentAlignment.MiddleLeft
            };
            card.Controls.Add(lblTitle);

            // Đường kẻ dưới tiêu đề
            var headerSep = new Label
            {
                Location = new Point(20, 45),
                Size = new Size(810, 1),
                BackColor = Color.FromArgb(240, 242, 245),
                Text = ""
            };
            card.Controls.Add(headerSep);

            // Các dòng thông tin
            for (int i = 0; i < items.Length; i++)
            {
                var itemY = 60 + i * 40;

                // Label
                var lbl = new Label
                {
                    Text = items[i].label,
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(107, 114, 128),
                    Location = new Point(20, itemY),
                    Size = new Size(150, 28),
                    TextAlign = ContentAlignment.MiddleRight
                };
                card.Controls.Add(lbl);

                // Dấu :
                var colon = new Label
                {
                    Text = ":",
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(150, 160, 170),
                    Location = new Point(165, itemY),
                    Size = new Size(10, 28),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                card.Controls.Add(colon);

                // Value (dịch xuống 5px so với label)
                var txt = new Label
                {
                    Text = items[i].value,
                    Font = new Font(items[i].label.Contains("Record ID") ? "Consolas" : "Segoe UI", 10.5F),
                    ForeColor = Color.FromArgb(45, 55, 72),
                    Location = new Point(175, itemY + 6),
                    Size = new Size(650, 28),
                    AutoEllipsis = false
                };
                // Highlight cho giá trị
                if (items[i].label.Contains("Hành động"))
                {
                    txt.BackColor = accentColor;
                    txt.ForeColor = Color.White;
                    txt.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                    txt.TextAlign = ContentAlignment.MiddleCenter;
                }
                card.Controls.Add(txt);

                // Đường kẻ ngang giữa các dòng
                if (i < items.Length - 1)
                {
                    var sep = new Label
                    {
                        Location = new Point(20, itemY + 34),
                        Size = new Size(810, 1),
                        BackColor = Color.FromArgb(245, 247, 250),
                        Text = ""
                    };
                    card.Controls.Add(sep);
                }
            }

            return card;
        }

        // Helper tạo vùng hiển thị JSON xịn hơn
        private Panel CreateJsonSection(string title, string json, Color accentColor)
        {
            int sectionHeight = 300;
            var section = new Panel
            {
                Size = new Size(850, sectionHeight),
                BackColor = Color.White,
                Margin = new Padding(0, 0, 0, 18),
                BorderStyle = BorderStyle.None
            };

            // Shadow
            section.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using var shadowPen = new Pen(Color.FromArgb(20, 0, 0, 0), 2);
                var rect = new Rectangle(2, 2, section.Width - 4, section.Height - 4);
                e.Graphics.DrawRectangle(shadowPen, rect);
            };

            // Thanh màu bên trái
            var accentBar = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(5, sectionHeight),
                BackColor = accentColor
            };
            section.Controls.Add(accentBar);

            // Tiêu đề
            var lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                ForeColor = accentColor,
                Location = new Point(20, 12),
                Size = new Size(810, 28),
                TextAlign = ContentAlignment.MiddleLeft
            };
            section.Controls.Add(lblTitle);

            // Header bar cho JSON
            var jsonHeader = new Panel
            {
                Location = new Point(20, 45),
                Size = new Size(810, 28),
                BackColor = Color.FromArgb(40, 40, 50)
            };

            var lblFileIcon = new Label
            {
                Text = "📄",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(166, 226, 46),
                Location = new Point(10, 3),
                Size = new Size(18, 18),
                TextAlign = ContentAlignment.MiddleCenter
            };

            var lblFileName = new Label
            {
                Text = "data.json",
                Font = new Font("Segoe UI", 8.5F),
                ForeColor = Color.FromArgb(180, 190, 200),
                Location = new Point(28, 5),
                Size = new Size(80, 16),
                TextAlign = ContentAlignment.MiddleLeft
            };

            jsonHeader.Controls.Add(lblFileIcon);
            jsonHeader.Controls.Add(lblFileName);
            section.Controls.Add(jsonHeader);

            // TextBox hiển thị JSON (Dark Theme giống VS Code)
            var txtJson = new TextBox
            {
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Location = new Point(20, 75),
                Size = new Size(810, sectionHeight - 90),
                Font = new Font("Consolas", 10.5F),
                BackColor = Color.FromArgb(30, 30, 40),
                ForeColor = Color.FromArgb(166, 226, 46),
                BorderStyle = BorderStyle.None,
                Text = json,
                WordWrap = false // Không wrap để giống code editor
            };
            section.Controls.Add(txtJson);

            return section;
        }

        private Color GetActionColor(string action)
        {
            return action.ToLower() switch
            {
                "insert" or "create" or "add" => Color.FromArgb(40, 167, 69),   // Xanh lá đậm
                "update" or "edit" or "modify" => Color.FromArgb(230, 150, 0),   // Cam đậm
                "delete" or "remove" => Color.FromArgb(200, 35, 51),             // Đỏ đậm
                "login" => Color.FromArgb(40, 120, 200),                         // Xanh dương đậm
                "logout" => Color.FromArgb(120, 130, 140),                       // Xám đậm
                _ => Color.FromArgb(108, 117, 125)
            };
        }

        private string GetActionEmoji(string action)
        {
            return action.ToLower() switch
            {
                "insert" or "create" or "add" => "➕",
                "update" or "edit" or "modify" => "✏️",
                "delete" or "remove" => "🗑️",
                "login" => "🔑",
                "logout" => "🚪",
                _ => "📝"
            };
        }

        private string FormatJson(string? json)
        {
            if (string.IsNullOrEmpty(json)) return "(Không có dữ liệu)";

            try
            {
                using var doc = JsonDocument.Parse(json);
                var options = new JsonSerializerOptions { WriteIndented = true };
                return JsonSerializer.Serialize(doc.RootElement, options);
            }
            catch
            {
                return json;
            }
        }

        #endregion

        #region Helper Methods

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
            _fontActionInsert?.Dispose();
            _fontActionUpdate?.Dispose();
            _fontActionDelete?.Dispose();
            base.OnFormClosing(e);
        }

        #endregion

        #region Helper Classes

        private class UserComboItem
        {
            public Guid? Id { get; set; }
            public string DisplayName { get; set; } = string.Empty;
        }

        #endregion
    }
}
