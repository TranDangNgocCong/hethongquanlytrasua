# MilkTeaPOS - Hệ thống Quản lý Quán Trà sữa

Ứng dụng POS (Point of Sale) desktop cho quán trà sữa, xây dựng trên **.NET 10 Windows Forms** với cơ sở dữ liệu **PostgreSQL** (Supabase).

## 📊 Tiến độ dự án: 17/17 forms (100%) 🎉

---

## 🚀 Tính năng chính

### ✅ Đã hoàn thành (17/17 forms)

| Nhóm | Forms | Trạng thái |
|------|-------|------------|
| **Authentication** | Login, ChangePassword | ✅ 10/10 |
| **Main** | MDI Container, Welcome Panel | ✅ 10/10 |
| **Dashboard** | KPIs, Bar Charts, Real-time | ✅ 10/10 |
| **Products** | Categories, Products, Toppings | ✅ 10/10 |
| **Tables** | Quản lý bàn, sơ đồ trực quan | ✅ 10/10 |
| **POS & Orders** | frmOrders, frmOrderHistory, frmPayment | ✅ 10/10 |
| **Customers** | CRM, Membership | ✅ 10/10 |
| **Vouchers** | Promotion, discount codes, tier mapping | ✅ 10/10 |
| **Reports** | Sales Report, Audit Log | ✅ 10/10 |
| **Users** | Staff management, roles | ✅ 10/10 |

---

## 🛠️ Công nghệ sử dụng

- **Framework**: .NET 10.0 Windows Forms
- **Database**: PostgreSQL 15+ (Supabase)
- **ORM**: Entity Framework Core 10.0
- **Password**: BCrypt.Net-Next (work factor 12)
- **UI**: Material Design-inspired, Vietnamese locale

---

## 📁 Cấu trúc dự án

```
MilkTEA-POS/
├── Models/                 # Entity models & DbContext
│   ├── PostgresContext.cs  # EF Core configuration
│   └── *.cs                # Entity classes
├── Services/               # Business logic layer
│   └── *Service.cs         # CRUD operations
├── ViewModels/             # Data transfer objects
├── frm*.cs                 # Windows Forms
├── database.sql            # Database schema
├── fix_order_total_trigger.sql  # Trigger fix for vouchers
└── add_image_url_columns.sql # Migration scripts
```

---

## 🔧 Cài đặt & Chạy

### Yêu cầu
- .NET 10.0 SDK
- PostgreSQL database (Supabase recommended)
- Visual Studio 2022+ (optional)

### Các bước

1. **Clone repository**
   ```bash
   git clone https://github.com/HuynhHao080/MilkTEA-POS.git
   cd MilkTEA-POS
   ```

2. **Cài đặt dependencies**
   ```bash
   dotnet restore
   ```

3. **Cấu hình database**
   - Mở `Models/PostgresContext.cs`
   - Cập nhật connection string trong `OnConfiguring()`
   - Hoặc tạo file `appsettings.json` (được ignore trong git)

4. **Chạy migration**
   ```bash
   # Chạy database.sql trên Supabase SQL Editor
   # Sau đó chạy fix_order_total_trigger.sql
   # Sau đó chạy add_image_url_columns.sql
   ```

5. **Build & Run**
   ```bash
   dotnet build
   dotnet run
   ```

---

## 🔐 Thông tin đăng nhập

| Username | Password     | Role     |
|----------|--------------|----------|
| admin    | admin1234@A  | Admin    |
| tranvi   | tranvi123@A  | Cashier  |
| cong     | cong123@A    | Staff    |

---

## 📋 Danh sách Forms chi tiết

### Các form đã hoàn thành:

1. **frmLogin** - Đăng nhập với BCrypt password
2. **frmChangePassword** - Đổi mật khẩu (audit logged)
3. **frmMain** - MDI Container, menu sidebar, welcome panel với live stats
4. **frmDashboard** - 5 KPI cards, bar charts, real-time data, membership stats
5. **frmUsers** - CRUD users, role assignment, password strength
6. **frmCategories** - CRUD categories, image upload
7. **frmCustomers** - CRM, customer management
8. **frmProducts** - CRUD products, size management (S/M/L)
9. **frmToppings** - CRUD toppings, usage tracking
10. **frmTables** - Table management, status, sơ đồ trực quan
11. **frmMemberships** - Membership tiers, points, ENUM types
12. **frmVouchers** - Voucher management, tier mapping, applicable tiers
13. **frmOrders** - Tạo đơn hàng, customization, voucher áp dụng
14. **frmOrderHistory** - Lịch sử đơn hàng, search, filter
15. **frmPayment** - Thanh toán, multiple payment methods
16. **frmSalesReport** - Revenue reports, CSV export, timezone fix
17. **frmAuditLog** - Audit trail viewer, JSON details

---

## 🛡️ Security Features

- ✅ **BCrypt password hashing** (work factor 12)
- ✅ **Audit logging** cho tất cả CRUD operations
- ✅ **Path traversal protection** cho file uploads
- ✅ **File size limits** (5-10MB tùy form)
- ✅ **Extension whitelist** cho images
- ✅ **SQL injection safe** (EF Core + parameterized queries)
- ✅ **Anti-duplicate audit** (500ms debounce + lock)
- ✅ **No plain-text secrets**
- ✅ **Transaction wrapping** cho order save

---

## 📊 Audit Logging

Tất cả 10 form có CRUD operations đều tích hợp audit logging:

| Form | Actions Logged |
|------|----------------|
| frmTables | INSERT, UPDATE, DELETE |
| frmToppings | INSERT, UPDATE, DELETE, SOFT_DELETE |
| frmCategories | INSERT, UPDATE, DELETE |
| frmCustomers | INSERT, UPDATE, DELETE |
| frmProducts | INSERT, UPDATE, DELETE, SOFT_DELETE |
| frmUsers | INSERT, UPDATE, DELETE |
| frmMemberships | INSERT, UPDATE, DELETE |
| frmVouchers | INSERT, UPDATE, DELETE |
| frmChangePassword | PASSWORD_CHANGE |

Audit logs được lưu vào bảng `audit_logs` với:
- User ID, Action, Table Name, Record ID
- JSON new_values/old_values
- IP address, Timestamp

---

## 📝 Quy ước code

### Đặt tên
- Form: `frm<Tên>.cs`
- Button: `btnAdd`, `btnEdit`, `btnDelete`
- TextBox: `txtName`, `txtSearch`
- Label: `lblTitle`, `lblName`
- DataGridView: `dgvCategories`
- Panel: `pnlHeader`, `pnlMain`
- PictureBox: `picPreview`

### Code style
- `async/await` cho database calls
- Try-catch với friendly messages (Vietnamese + emoji)
- Validation trước khi save
- Dùng `#region` để organize code
- Short-lived DbContext (`using` pattern)
- `AsNoTracking()` cho read-only queries

---

## 🤝 Đóng góp

1. Fork repository
2. Tạo branch mới (`git checkout -b feature/AmazingFeature`)
3. Commit changes (`git commit -m 'Add AmazingFeature'`)
4. Push to branch (`git push origin feature/AmazingFeature`)
5. Mở Pull Request

---

## 📄 License

Dự án nội bộ - Không public.

---

## 📞 Liên hệ

- **Developer**: Huỳnh Nhựt Hào
- **GitHub**: https://github.com/HuynhHao080/MilkTEA-POS

---

*Cập nhật cuối: April 9, 2026 - HOÀN THÀNH 17/17 FORMS 🎉*
