# 📘 Hệ thống Phân quyền - MilkTeaPOS

## 📋 Tổng quan

Hệ thống phân quyền đã được tích hợp vào ứng dụng MilkTeaPOS để kiểm soát truy cập dựa trên vai trò người dùng (Role-based Access Control - RBAC).

---

## 👥 Vai trò Người dùng (User Roles)

### 1. 👑 Admin (Quản trị viên)
- **Mô tả:** Người quản lý toàn bộ hệ thống
- **Quyền hạn:** Toàn quyền truy cập tất cả tính năng
- **Người dùng mẫu:** `admin` / `admin123`

### 2. 👨‍💼 Staff (Nhân viên)
- **Mô tả:** Nhân viên phục vụ, có thể thao tác chính nhưng không được quản lý người dùng/thanh toán
- **Quyền hạn:** Truy cập vừa phải, có thể xem và chỉnh sửa một số tính năng
- **Người dùng mẫu:** `staff01` / `staff123`

### 3. 💰 Cashier (Thu ngân)
- **Mô tả:** Chỉ phục vụ việc order và thanh toán
- **Quyền hạn:** Hạn chế, chỉ truy cập POS và xem danh mục
- **Người dùng mẫu:** `cashier01` / `cashier123`

---

## 📊 Ma trận Phân quyền Chi tiết

| **Module** | **Chức năng** | **Admin** | **Staff** | **Cashier** |
|------------|---------------|-----------|-----------|-------------|
| **Dashboard** | Xem thống kê | ✅ | ✅ | ✅ |
| **Danh mục** | Xem danh sách | ✅ | ✅ (Chỉ đọc) | ✅ (Chỉ đọc) |
| | Thêm/Sửa/Xóa | ✅ | ❌ | ❌ |
| **Sản phẩm** | Xem sản phẩm | ✅ | ✅ (Chỉ đọc) | ✅ (Chỉ đọc) |
| | Thêm | ✅ | ❌ | ❌ |
| | Sửa | ✅ | ✅ | ❌ |
| | Xóa | ✅ | ❌ | ❌ |
| **Toppings** | Xem toppings | ✅ | ✅ (Chỉ đọc) | ✅ (Chỉ đọc) |
| | Thêm/Sửa/Xóa | ✅ | ❌ | ❌ |
| **Bàn** | Xem bàn | ✅ | ✅ | ✅ (Chỉ đọc) |
| | Thêm/Sửa/Xóa | ✅ | ❌ | ❌ |
| | Chuyển trạng thái | ✅ | ✅ | ❌ |
| | Trả bàn | ✅ | ✅ | ✅ |
| **POS (Order)** | Tạo đơn hàng | ✅ | ✅ | ✅ |
| | Tùy chỉnh món | ✅ | ✅ | ✅ |
| | Áp dụng voucher | ✅ | ✅ | ✅ |
| | Tìm thành viên | ✅ | ✅ | ✅ |
| | Giữ/Resume đơn | ✅ | ✅ | ✅ |
| | Hủy đơn | ✅ | ✅ | ❌ |
| **Lịch sử đơn** | Xem tất cả đơn | ✅ | ✅ | ✅ |
| | Xem chi tiết | ✅ | ✅ | ✅ |
| | In hóa đơn | ✅ | ✅ | ✅ |
| | Resume đơn | ✅ | ✅ | ✅ |
| **Thanh toán** | Xử lý thanh toán | ✅ | ✅ | ✅ |
| | Áp dụng giảm giá | ✅ | ✅ | ❌ |
| | Hủy thanh toán | ✅ | ❌ | ❌ |
| | Hoàn tiền | ✅ | ❌ | ❌ |
| **Khách hàng** | Xem danh sách | ✅ | ✅ | ✅ (Chỉ đọc) |
| | Thêm/Sửa | ✅ | ✅ | ❌ |
| | Xóa | ✅ | ❌ | ❌ |
| | Xem lịch sử mua | ✅ | ✅ | ❌ |
| **Thành viên** | Xem thành viên | ✅ | ✅ (Chỉ đọc) | ❌ |
| | Thêm/Sửa hạng | ✅ | ❌ | ❌ |
| | Điều chỉnh điểm | ✅ | ❌ | ❌ |
| | Xóa thành viên | ✅ | ❌ | ❌ |
| **Voucher** | Xem voucher | ✅ | ✅ (Chỉ đọc) | ❌ |
| | Thêm/Sửa/Xóa | ✅ | ❌ | ❌ |
| | Xem thống kê | ✅ | ✅ | ❌ |
| **Báo cáo** | Báo cáo doanh thu | ✅ | ✅ (Giới hạn) | ❌ |
| | Hiệu suất sản phẩm | ✅ | ✅ (Giới hạn) | ❌ |
| | Thống kê thanh toán | ✅ | ❌ | ❌ |
| | Xuất CSV | ✅ | ✅ (Giới hạn) | ❌ |
| | Phân tích khách hàng | ✅ | ❌ | ❌ |
| **Người dùng** | Xem người dùng | ✅ | ❌ | ❌ |
| | Thêm người dùng | ✅ | ❌ | ❌ |
| | Sửa người dùng | ✅ | ❌ | ❌ |
| | Xóa người dùng | ✅ | ❌ | ❌ |
| | Đặt lại mật khẩu | ✅ | ❌ | ❌ |
| **Audit Log** | Xem nhật ký | ✅ | ❌ | ❌ |
| | Lọc/Tìm kiếm | ✅ | ❌ | ❌ |
| | Xuất logs | ✅ | ❌ | ❌ |
| | Xóa logs cũ | ✅ | ❌ | ❌ |
| **Cài đặt** | Đổi mật khẩu | ✅ | ✅ | ✅ |
| | Cấu hình hệ thống | ✅ | ❌ | ❌ |

---

## 🏗️ Kiến trúc Kỹ thuật

### Files đã tạo

#### 1. `Helpers/RolePermissions.cs`
- **Mục đích:** Định nghĩa ma trận phân quyền tĩnh
- **Chức năng:**
  - Enum `PermissionLevel`: None, ReadOnly, Edit, Full
  - Dictionary ánh xạ Form → Role → PermissionLevel
  - Helper methods: `HasAccess()`, `GetPermissionLevel()`, `CanCreate()`, `CanUpdate()`, `CanDelete()`, `CanView()`

#### 2. `Helpers/PermissionChecker.cs`
- **Mục đích:** Kiểm tra quyền runtime từ bất kỳ form nào
- **Chức năng:**
  - `GetCurrentRoleName()`: Lấy role name từ DB sử dụng `PostgresContext.CurrentUserId`
  - `HasAccessToForm(formName)`: Kiểm tra quyền truy cập form
  - `CanCreate/Update/Delete(formName)`: Kiểm tra quyền thao tác
  - `ApplyPermissionsToButtons()`: Tự động ẩn/hiện buttons

### Files đã sửa

#### 1. `frmMain.cs`
**Thay đổi:**
- ✅ Thêm `using MilkTeaPOS.Helpers;`
- ✅ Thêm method `setupRoleBasedAccess()`: Ẩn/hiện sidebar buttons dựa trên role
- ✅ Cập nhật `btnMenu_Click()`: Kiểm tra quyền trước khi mở form
- ✅ Cập nhật `setupButtonHoverEffects()`: Chỉ attach events cho buttons visible

**Logic hoạt động:**
```csharp
// Khi form khởi động
initializeForm()
  → setupRoleBasedAccess()         // Ẩn buttons không có quyền
  → setupButtonHoverEffects()      // Chỉ attach events cho buttons visible

// Khi user click menu button
btnMenu_Click()
  → Kiểm tra RolePermissions.HasAccess()
  → Nếu không có quyền: Hiện message box từ chối
  → Nếu có quyền: Mở form
```

#### 2. `frmProducts.cs`
**Thay đổi:**
- ✅ Thêm `using MilkTeaPOS.Helpers;`
- ✅ `btnAdd_Click()`: Check `PermissionChecker.CanCreate("frmProducts")`
- ✅ `btnEdit_Click()`: Check `PermissionChecker.CanUpdate("frmProducts")`
- ✅ `btnDelete_Click()`: Check `PermissionChecker.CanDelete("frmProducts")`

#### 3. `frmCategories.cs`
**Thay đổi:**
- ✅ Thêm `using MilkTeaPOS.Helpers;`
- ✅ `btnAdd_Click()`: Check `PermissionChecker.CanCreate("frmCategories")`
- ✅ `btnEdit_Click()`: Check `PermissionChecker.CanUpdate("frmCategories")`
- ✅ `btnDelete_Click()`: Check `PermissionChecker.CanDelete("frmCategories")`

#### 4. `frmUsers.cs`
**Thay đổi:**
- ✅ Thêm `using MilkTeaPOS.Helpers;`
- ✅ `btnAdd_Click()`: Check `PermissionChecker.CanCreate("frmUsers")`
- ✅ `btnEdit_Click()`: Check `PermissionChecker.CanUpdate("frmUsers")`
- ✅ `btnDelete_Click()`: Check `PermissionChecker.CanDelete("frmUsers")`

---

## 🔧 Cách sử dụng trong Forms khác

Để thêm kiểm tra quyền vào một form mới, làm theo các bước sau:

### Bước 1: Thêm using directive
```csharp
using MilkTeaPOS.Helpers;
```

### Bước 2: Thêm kiểm tra quyền vào button handlers
```csharp
private async void btnAdd_Click(object sender, EventArgs e)
{
    if (!PermissionChecker.CanCreate("frmYourFormName"))
    {
        MessageBox.Show("❌ Bạn không có quyền thêm mới!\n\n" +
            $"👤 Role hiện tại: {PermissionChecker.GetCurrentRoleName()}\n" +
            $"🔒 Vui lòng liên hệ quản trị viên.",
            "🚫 Truy cập bị từ chối",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);
        return;
    }
    
    // ... code xử lý thêm mới
}

private async void btnEdit_Click(object sender, EventArgs e)
{
    if (!PermissionChecker.CanUpdate("frmYourFormName"))
    {
        MessageBox.Show("❌ Bạn không có quyền sửa!\n\n" +
            $"👤 Role hiện tại: {PermissionChecker.GetCurrentRoleName()}\n" +
            $"🔒 Vui lòng liên hệ quản trị viên.",
            "🚫 Truy cập bị từ chối",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);
        return;
    }
    
    // ... code xử lý sửa
}

private async void btnDelete_Click(object sender, EventArgs e)
{
    if (!PermissionChecker.CanDelete("frmYourFormName"))
    {
        MessageBox.Show("❌ Bạn không có quyền xóa!\n\n" +
            $"👤 Role hiện tại: {PermissionChecker.GetCurrentRoleName()}\n" +
            $"🔒 Vui lòng liên hệ quản trị viên.",
            "🚫 Truy cập bị từ chối",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);
        return;
    }
    
    // ... code xử lý xóa
}
```

### Bước 3: Cập nhật RolePermissions.cs (nếu cần)
Nếu form mới chưa có trong danh sách, thêm vào dictionary `_permissions`:

```csharp
{ "frmYourFormName", new Dictionary<string, PermissionLevel>
    {
        { "Admin", PermissionLevel.Full },
        { "Staff", PermissionLevel.ReadOnly },  // Hoặc Edit, None tùy ý
        { "Cashier", PermissionLevel.None }
    }
},
```

---

## 🎨 UI/UX Behavior

### Khi user không có quyền:

1. **Sidebar Button:** Sẽ bị ẩn hoàn toàn (`Visible = false`)
2. **Nếu cố tình mở form (qua shortcut, v.v.):** 
   - Hiện message box cảnh báo với nội dung:
     ```
     ❌ Bạn không có quyền truy cập chức năng này!
     
     👤 Role hiện tại: Cashier
     🔒 Vui lòng liên hệ quản trị viên nếu cần truy cập.
     ```
3. **Trong form (CRUD buttons):**
   - Hiện message box tương tự khi nhấn nút
   - Không thực hiện thao tác

### Khi user có quyền ReadOnly:
- Xem được danh sách/thông tin
- Không thấy buttons Add/Edit/Delete (hoặc bị ẩn trong form)

---

## 🔐 Bảo mật

### Cơ chế hoạt động:

1. **Login:** 
   - User đăng nhập qua `frmLogin`
   - `PostgresContext.CurrentUserId` được set với User ID
   - User object (với Role navigation property) được eager-loaded

2. **Permission Check:**
   - `PermissionChecker.GetCurrentRoleName()` query DB để lấy role name
   - Sử dụng `PostgresContext.CurrentUserId` làm key
   - Include Role navigation property để lấy `Role.Name`

3. **Authorization:**
   - `RolePermissions.HasAccess(formName, roleName)` kiểm tra dictionary
   - Return `true` nếu permission level != None
   - Return `false` nếu không có quyền

### Lưu ý bảo mật:

⚠️ **Hiện tại:** Permission checks chỉ ở UI layer (forms)
- User với knowledge về code có thể bypass qua database trực tiếp
- Điều này là đủ cho mục đích UI/UX và ngăn chặn người dùng bình thường

🔒 **Để tăng cường bảo mật (tương lai):**
- Thêm middleware ở API layer (nếu có REST API)
- Thêm trigger trong database để kiểm tra role trước khi INSERT/UPDATE/DELETE
- Lưu ý: Database đã có triggers audit_log để theo dõi mọi thay đổi

---

## 📝 Ví dụ Test Cases

### Test Case 1: Cashier đăng nhập
```
1. Đăng nhập: cashier01 / cashier123
2. Kết quả mong đợi:
   ✅ Chỉ thấy buttons: Dashboard, POS, Order History, Categories (readonly), 
                        Products (readonly), Toppings (readonly), Tables (readonly),
                        Customers (readonly), Change Password
   ❌ KHÔNG thấy: Memberships, Vouchers, Reports, Users, Audit Log
3. Thử mở frmProducts qua form khác:
   → Message box: "Bạn không có quyền truy cập chức năng này!"
```

### Test Case 2: Staff đăng nhập
```
1. Đăng nhập: staff01 / staff123
2. Kết quả mong đợi:
   ✅ Thấy tất cả buttons TRỪ: Users, Audit Log
   ✅ Có thể edit Products (nhưng không delete được)
   ✅ Có thể xem Memberships, Vouchers (readonly)
   ❌ KHÔNG thấy: Users, Audit Log
```

### Test Case 3: Admin đăng nhập
```
1. Đăng nhập: admin / admin123
2. Kết quả mong đợi:
   ✅ Thấy TẤT CẢ buttons
   ✅ Có thể thực hiện MỌI thao tác CRUD
   ✅ Truy cập được Users management và Audit Log
```

---

## 🚀 Mở rộng trong Tương lai

### 1. Dynamic Permission Configuration
- Lưu permissions trong database thay vì hardcode
- Admin có thể tùy chỉnh quyền qua UI
- Ví dụ: Bảng `role_permissions(role_id, form_name, permission_level)`

### 2. Fine-grained Permissions
- Thay vì chỉ Full/Edit/ReadOnly/None, có thể chia nhỏ hơn:
  - `CanViewPrice`: Xem giá sản phẩm
  - `CanApplyDiscount`: Áp dụng giảm giá
  - `CanExportReport`: Xuất báo cáo
  - V.v.

### 3. Permission Caching
- Cache role name và permissions để tránh query DB mỗi lần check
- Invalidate cache khi user thay đổi role

### 4. Audit Permission Changes
- Log mọi thay đổi về quyền vào audit_logs
- Theo dõi ai đã thay đổi quyền của ai

---

## 🐛 Troubleshooting

### Vấn đề: Buttons không ẩn khi đăng nhập
**Nguyên nhân:** 
- Role của user không được load đúng
- `RolePermissions` dictionary không đúng form name

**Giải pháp:**
```csharp
// Debug trong frmMain.setupRoleBasedAccess()
var roleName = _currentUser?.Role?.Name ?? "Cashier";
Console.WriteLine($"Current user role: {roleName}");
Console.WriteLine($"btnProducts visible: {btnProducts.Visible}");
```

### Vấn đề: Permission check không hoạt động trong form
**Nguyên nhân:**
- Form name không khớp với key trong `_permissions` dictionary
- Thiếu `using MilkTeaPOS.Helpers;`

**Giải pháp:**
```csharp
// Debug trong form
var roleName = PermissionChecker.GetCurrentRoleName();
var canCreate = PermissionChecker.CanCreate("frmProducts");
Console.WriteLine($"Role: {roleName}, CanCreate: {canCreate}");
```

### Vấn đề: Database query chậm khi check permission
**Nguyên nhân:** 
- `GetCurrentRoleName()` query DB mỗi lần gọi

**Giải pháp:** 
- Cache role name trong static variable
- Hoặc pass User object vào form constructor (cách tốt nhất)

---

## 📞 Support

Nếu bạn gặp vấn đề với hệ thống phân quyền:
1. Kiểm tra logs trong Audit Log (Admin only)
2. Xác nhận role của user trong database:
   ```sql
   SELECT u.username, r.name 
   FROM users u 
   LEFT JOIN roles r ON u.role_id = r.id 
   WHERE u.username = 'cashier01';
   ```
3. Kiểm tra `_permissions` dictionary trong `RolePermissions.cs`

---

## ✅ Checklist Triển khai

- [x] Tạo `Helpers/RolePermissions.cs`
- [x] Tạo `Helpers/PermissionChecker.cs`
- [x] Cập nhật `frmMain.cs` với `setupRoleBasedAccess()`
- [x] Thêm permission checks vào `frmProducts.cs`
- [x] Thêm permission checks vào `frmCategories.cs`
- [x] Thêm permission checks vào `frmUsers.cs`
- [x] Build thành công (không có lỗi)
- [ ] Test với user `admin`
- [ ] Test với user `staff01`
- [ ] Test với user `cashier01`
- [ ] Thêm permission checks vào các forms còn lại (Toppings, Tables, Customers, v.v.)
- [ ] Viết unit tests cho `RolePermissions` helper
- [ ] Cập nhật documentation cho người dùng cuối

---

**Version:** 1.0  
**Last Updated:** April 9, 2026  
**Author:** Qwen Code AI Assistant
