-- ============================================
-- Xóa SẠCH trigger audit trong database
-- Chuyển hoàn toàn sang C# audit logging
-- Chạy script này trên Supabase SQL Editor
-- ============================================

-- 1. Xóa trigger audit cho TẤT CẢ các bảng
DROP TRIGGER IF EXISTS audit_customers_changes ON customers;
DROP TRIGGER IF EXISTS audit_vouchers_changes ON vouchers;
DROP TRIGGER IF EXISTS audit_orders_changes ON orders;
DROP TRIGGER IF EXISTS audit_payments_changes ON payments;

-- 2. Xóa function audit (cascade để xóa luôn các trigger còn lại nếu có)
DROP FUNCTION IF EXISTS audit_table_changes CASCADE;

-- 3. Kiểm tra lại - phải trống kết quả
SELECT trigger_name, event_object_table 
FROM information_schema.triggers 
WHERE trigger_name LIKE 'audit%'
ORDER BY trigger_name;

-- 4. Kiểm tra function audit đã xóa chưa
SELECT routine_name 
FROM information_schema.routines 
WHERE routine_name = 'audit_table_changes';

-- ✅ Sau khi chạy script này, chỉ còn C# code ghi audit_logs
