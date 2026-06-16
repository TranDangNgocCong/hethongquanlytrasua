# 📌 Hướng dẫn Nhanh - Hệ thống Phân quyền

## 🎯 Tóm tắt

Hệ thống phân quyền đã được tích hợp thành công vào MilkTeaPOS!

### ✅ Đã hoàn thành:
1. ✅ Tạo hệ thống phân quyền 3 cấp: Admin, Staff, Cashier
2. ✅ Tự động ẩn sidebar buttons không phù hợp với role
3. ✅ Chặn truy cập trái phép vào các forms
4. ✅ Thêm kiểm tra quyền vào các forms quan trọng
5. ✅ Build thành công không có lỗi

---

## 👥 3 Vai trò Chính

### 👑 Admin (`admin` / `admin123`)
- **Toàn quyền** tất cả tính năng
- Quản lý người dùng, audit logs, cấu hình hệ thống
- Xóa/sửa/Thêm mọi thứ

### 👨‍💼 Staff (`staff01` / `staff123`)
- **POS & Order:** Đầy đủ
- **Products/Toppings:** Chỉ xem
- **Customers:** Xem + Sửa (không xóa)
- **Reports:** Xem giới hạn
- **KHÔNG có:** Users management, Audit logs

### 💰 Cashier (`cashier01` / `cashier123`)
- **POS & Order:** Đầy đủ (chức năng chính)
- **Products/Categories/Toppings:** Chỉ xem
- **Tables:** Xem + Trả bàn
- **KHÔNG có:** Reports, Users, Audit, Vouchers, Memberships

---

## 📋 Bảng Permissions Nhanh

| Form/Chức năng | Admin | Staff | Cashier |
|----------------|-------|-------|---------|
| Dashboard | ✅ Full | ✅ View | ✅ View |
| Categories | ✅ CRUD | 👁️ Read | 👁️ Read |
| Products | ✅ CRUD | ✏️ Edit | 👁️ Read |
| Toppings | ✅ CRUD | 👁️ Read | 👁️ Read |
| Tables | ✅ CRUD | ✏️ Edit | 👁️ Read |
| **POS (Orders)** | ✅ Full | ✅ Full | ✅ Full |
| Order History | ✅ View | ✅ View | ✅ View |
| Customers | ✅ CRUD | ✏️ Edit | 👁️ Read |
| Memberships | ✅ CRUD | 👁️ Read | ❌ None |
| Vouchers | ✅ CRUD | 👁️ Read | ❌ None |
| Reports | ✅ Full | 👁️ Limited | ❌ None |
| **Users** | ✅ Full | ❌ None | ❌ None |
| **Audit Log** | ✅ View | ❌ None | ❌ None |

**Legend:** ✅ Full = Toàn quyền | ✏️ Edit = Sửa | 👁️ Read = Chỉ xem | ❌ None = Không có

---

## 🔧 Cách Test

### 1. Test với Admin
```
1. Chạy ứng dụng
2. Đăng nhập: admin / admin123
3. Kiểm tra:
   ✅ Tất cả buttons bên trái đều hiện
   ✅ Mở được frmUsers, frmAuditLog
   ✅ Thêm/Sửa/Xóa được products, categories, v.v.
```

### 2. Test với Staff
```
1. Đăng nhập: staff01 / staff123
2. Kiểm tra:
   ✅ KHÔNG thấy buttons: Users, Audit Log
   ✅ Mở được POS, Orders, Customers
   ✅ Sửa được Products (nhưng form khác sẽ check quyền)
   ❌ Thử xóa Product → Phải bị chặn
```

### 3. Test với Cashier
```
1. Đăng nhập: cashier01 / cashier123
2. Kiểm tra:
   ✅ Chỉ thấy: Dashboard, POS, Order History, Categories, Products, 
                Toppings, Tables, Customers, Change Password
   ❌ KHÔNG thấy: Memberships, Vouchers, Reports, Users, Audit Log
   ❌ Thử mở Products → Chỉ xem, không Add/Edit/Delete được
```

---

## 🛠️ Files Đã Thay Đổi

### Files MỚI:
```
✅ Helpers/RolePermissions.cs      - Định nghĩa permissions
✅ Helpers/PermissionChecker.cs    - Runtime permission checks
✅ HỆ_THỐNG_PHÂN_QUYỀN.md          - Documentation đầy đủ
```

### Files SỬA:
```
✅ frmMain.cs                      - Ẩn/hiện buttons theo role
✅ frmProducts.cs                  - Check quyền CRUD
✅ frmCategories.cs                - Check quyền CRUD
✅ frmUsers.cs                     - Check quyền CRUD (Admin only)
```

---

## 📝 Thêm Permissions cho Form Khác

Nếu muốn thêm quyền cho form mới (ví dụ: `frmToppings`):

### Bước 1: Mở `Helpers/RolePermissions.cs`
Thêm vào dictionary `_permissions`:

```csharp
{ "frmToppings", new Dictionary<string, PermissionLevel>
    {
        { "Admin", PermissionLevel.Full },
        { "Staff", PermissionLevel.ReadOnly },
        { "Cashier", PermissionLevel.ReadOnly }
    }
},
```

### Bước 2: Mở form cần kiểm tra (ví dụ: `frmToppings.cs`)
Thêm using:
```csharp
using MilkTeaPOS.Helpers;
```

Thêm check quyền vào buttons:
```csharp
private async void btnAdd_Click(object sender, EventArgs e)
{
    if (!PermissionChecker.CanCreate("frmToppings"))
    {
        MessageBox.Show("❌ Bạn không có quyền thêm topping!", ...);
        return;
    }
    // ... code thêm topping
}
```

---

## ⚠️ Lưu ý Quan trọng

1. **Password Users:**
   - Admin: `admin123`
   - Staff: `staff123`
   - Cashier: `cashier123`
   
   ⚨ **NÊN ĐỔI PASSWORD** sau khi đăng nhập lần đầu!

2. **Database Connection:**
   - Permissions hoạt động dựa vào DB query để lấy role
   - Nếu DB connection lỗi, default sẽ là "Cashier" (an toàn nhất)

3. **Current User ID:**
   - `PostgresContext.CurrentUserId` được set khi login
   - Nếu không set, permission checks sẽ fail → default = Cashier

---

## 🐛 Troubleshooting

### Buttons không ẩn?
```
→ Kiểm tra user có Role chưa?
→ Query DB: SELECT u.username, r.name FROM users u LEFT JOIN roles r ON u.role_id = r.id WHERE u.username = 'admin';
→ Nếu r.name = NULL → User chưa có role → Gán role trong DB
```

### Permission check không hoạt động?
```
→ Đảm bảo đã thêm: using MilkTeaPOS.Helpers;
→ Kiểm tra form name có khớp trong dictionary không
→ Debug: var role = PermissionChecker.GetCurrentRoleName(); Console.WriteLine(role);
```

### Form mở được nhưng buttons bị ẩn?
```
→ Đây là hành vi đúng! Form mở được nhưng CRUD buttons bị ẩn
→ User vẫn có thể xem danh sách (read-only)
```

---

## 🚀 Bước Tiếp Theo

### Khuyến nghị:
1. ✅ **Test với cả 3 roles** để đảm bảo permissions đúng
2. 📝 **Thêm permission checks** vào các forms còn lại:
   - frmToppings.cs
   - frmTables.cs
   - frmCustomers.cs
   - frmMemberships.cs
   - frmVouchers.cs
   - frmOrders.cs
   - frmSalesReport.cs
   - frmAuditLog.cs
3. 🔒 **Thay đổi passwords** mặc định
4. 📊 **Monitor audit logs** để theo dõi activity

### Tùy chọn nâng cao (sau này):
- [ ] Dynamic permissions từ DB (thay vì hardcode)
- [ ] Permission caching để tăng performance
- [ ] Fine-grained permissions (xem giá, áp dụng giảm giá, v.v.)
- [ ] API middleware protection (nếu có REST API)

---

## 📞 Cần Trợ Giúp?

- 📖 Đọc đầy đủ: `HỆ_THỐNG_PHÂN_QUYỀN.md`
- 🔍 Kiểm tra audit logs (Admin only)
- 💬 Liên hệ developer nếu có bugs

---

**Chúc bạn thành công! 🎉**
