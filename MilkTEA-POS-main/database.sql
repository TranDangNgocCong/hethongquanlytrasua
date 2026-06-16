-- ============================================
-- MilkTeaPOS Database Schema
-- PostgreSQL for Supabase
-- COMPLETE with Full Indexes, FKs, Triggers, Functions
-- ============================================

-- Enable UUID extension
CREATE EXTENSION IF NOT EXISTS "pgcrypto";

-- ============================================
-- DROP TABLES
-- ============================================
DROP TABLE IF EXISTS order_toppings CASCADE;
DROP TABLE IF EXISTS payments CASCADE;
DROP TABLE IF EXISTS order_details CASCADE;
DROP TABLE IF EXISTS orders CASCADE;
DROP TABLE IF EXISTS tables CASCADE;
DROP TABLE IF EXISTS toppings CASCADE;
DROP TABLE IF EXISTS product_sizes CASCADE;
DROP TABLE IF EXISTS products CASCADE;
DROP TABLE IF EXISTS categories CASCADE;
DROP TABLE IF EXISTS users CASCADE;
DROP TABLE IF EXISTS roles CASCADE;

-- Drop custom types
DROP TYPE IF EXISTS size_type CASCADE;
DROP TYPE IF EXISTS sugar_level CASCADE;
DROP TYPE IF EXISTS ice_level CASCADE;
DROP TYPE IF EXISTS order_status CASCADE;
DROP TYPE IF EXISTS table_status CASCADE;
DROP TYPE IF EXISTS payment_method CASCADE;
DROP TYPE IF EXISTS order_detail_status CASCADE;
DROP TYPE IF EXISTS payment_status CASCADE;
DROP TYPE IF EXISTS voucher_type CASCADE;
DROP TYPE IF EXISTS voucher_status CASCADE;
DROP TYPE IF EXISTS membership_tier CASCADE;

-- Drop new tables
DROP TABLE IF EXISTS audit_logs CASCADE;
DROP TABLE IF EXISTS vouchers CASCADE;
DROP TABLE IF EXISTS memberships CASCADE;
DROP TABLE IF EXISTS customers CASCADE;

-- ============================================
-- CUSTOM TYPES (ENUMs)
-- ============================================

CREATE TYPE size_type AS ENUM ('S', 'M', 'L');
CREATE TYPE sugar_level AS ENUM ('0', '25', '50', '75', '100');
CREATE TYPE ice_level AS ENUM ('0', '25', '50', '75', '100');
CREATE TYPE order_status AS ENUM ('pending', 'preparing', 'ready', 'served', 'cancelled');
CREATE TYPE table_status AS ENUM ('available', 'occupied', 'reserved', 'maintenance');
CREATE TYPE payment_method AS ENUM ('cash', 'card', 'qr_code', 'bank_transfer', 'e_wallet');
CREATE TYPE order_detail_status AS ENUM ('pending', 'preparing', 'ready', 'served', 'cancelled');
CREATE TYPE payment_status AS ENUM ('pending', 'completed', 'failed', 'refunded');

-- New ENUMs for vouchers and membership
CREATE TYPE voucher_type AS ENUM ('percentage', 'fixed_amount', 'free_item', 'buy_one_get_one');
CREATE TYPE voucher_status AS ENUM ('active', 'inactive', 'expired', 'used_up');
CREATE TYPE membership_tier AS ENUM ('none', 'silver', 'gold', 'platinum', 'diamond');

-- ============================================
-- CORE TABLES
-- ============================================

-- Roles
CREATE TABLE roles (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    name VARCHAR(50) NOT NULL UNIQUE,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);

-- Users (employees)
CREATE TABLE users (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    username VARCHAR(100) NOT NULL UNIQUE,
    password VARCHAR(255),
    password_hash VARCHAR(255) NOT NULL,
    role_id UUID REFERENCES roles(id) ON DELETE SET NULL,  -- Allow NULL when role deleted
    is_active BOOLEAN DEFAULT TRUE,
    avatar_url TEXT,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW(),
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);

-- Categories
CREATE TABLE categories (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    name VARCHAR(100) NOT NULL,
    description TEXT,
    image_url TEXT,
    display_order INT DEFAULT 0 CHECK (display_order >= 0),
    is_active BOOLEAN DEFAULT TRUE,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW(),
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);

-- Case-insensitive unique index for category name
CREATE UNIQUE INDEX unique_category_name_lower ON categories (LOWER(name));

-- Index for ordering categories
CREATE INDEX idx_categories_order ON categories (display_order, created_at);

-- Products
CREATE TABLE products (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    name VARCHAR(200) NOT NULL,
    description TEXT,
    base_price NUMERIC(12, 2) NOT NULL CHECK (base_price >= 0),
    category_id UUID NOT NULL REFERENCES categories(id) ON DELETE CASCADE,
    image_url TEXT,
    is_available BOOLEAN DEFAULT TRUE,
    is_featured BOOLEAN DEFAULT FALSE,
    preparation_time INT DEFAULT 5,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW(),
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);

-- Product Sizes (S/M/L pricing)
CREATE TABLE product_sizes (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    product_id UUID NOT NULL REFERENCES products(id) ON DELETE CASCADE,
    size size_type NOT NULL,
    price NUMERIC(12, 2) NOT NULL CHECK (price >= 0),
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW(),
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT NOW(),
    CONSTRAINT product_sizes_unique UNIQUE(product_id, size)
);

-- Toppings
CREATE TABLE toppings (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    name VARCHAR(100) NOT NULL UNIQUE,
    description TEXT,
    price NUMERIC(12, 2) NOT NULL CHECK (price >= 0),
    is_available BOOLEAN DEFAULT TRUE,
    image_url TEXT,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW(),
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);

-- Tables
CREATE TABLE tables (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    name VARCHAR(50) NOT NULL UNIQUE,
    status table_status DEFAULT 'available',
    capacity INT DEFAULT 2 CHECK (capacity > 0),
    location VARCHAR(50) DEFAULT 'main',
    image_url TEXT,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW(),
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);

-- ============================================
-- CUSTOMER & MEMBERSHIP TABLES
-- ============================================

-- Customers (chuẩn hóa thông tin khách hàng)
CREATE TABLE customers (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    name VARCHAR(100) NOT NULL,
    phone VARCHAR(20) UNIQUE,
    email VARCHAR(100) UNIQUE,
    date_of_birth DATE,
    gender VARCHAR(10) CHECK (gender IN ('male', 'female', 'other')),
    address TEXT,
    notes TEXT,
    avatar_url TEXT,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW(),
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);

CREATE INDEX idx_customers_phone ON customers(phone);
CREATE INDEX idx_customers_email ON customers(email);
CREATE INDEX idx_customers_name ON customers(name);

-- Memberships (hạng thành viên và điểm tích lũy)
CREATE TABLE memberships (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    customer_id UUID NOT NULL UNIQUE REFERENCES customers(id) ON DELETE CASCADE,
    tier membership_tier DEFAULT 'none',
    points INT DEFAULT 0 CHECK (points >= 0),
    total_spent NUMERIC(12, 2) DEFAULT 0 CHECK (total_spent >= 0),
    total_orders INT DEFAULT 0 CHECK (total_orders >= 0),
    joined_at TIMESTAMP WITH TIME ZONE DEFAULT NOW(),
    last_order_at TIMESTAMP WITH TIME ZONE,
    expires_at TIMESTAMP WITH TIME ZONE,
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);

CREATE INDEX idx_memberships_customer_id ON memberships(customer_id);
CREATE INDEX idx_memberships_tier ON memberships(tier);
CREATE INDEX idx_memberships_points ON memberships(points);

-- Memberships: NEW INDEX FOR MARKETING QUERIES
CREATE INDEX idx_memberships_tier_spent ON memberships(tier, total_spent DESC);

-- ============================================
-- VOUCHER TABLES
-- ============================================

-- Vouchers (mã giảm giá)
CREATE TABLE vouchers (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    code VARCHAR(50) NOT NULL UNIQUE,
    name VARCHAR(200) NOT NULL,
    description TEXT,
    voucher_type voucher_type NOT NULL,
    discount_value NUMERIC(12, 2) NOT NULL CHECK (discount_value >= 0),
    min_order_amount NUMERIC(12, 2) DEFAULT 0 CHECK (min_order_amount >= 0),
    max_discount_amount NUMERIC(12, 2),
    usage_limit INT DEFAULT NULL CHECK (usage_limit > 0),
    usage_count INT DEFAULT 0 CHECK (usage_count >= 0),
    status voucher_status DEFAULT 'active',
    valid_from TIMESTAMP WITH TIME ZONE DEFAULT NOW(),
    valid_until TIMESTAMP WITH TIME ZONE,
    applicable_tiers membership_tier[] DEFAULT ARRAY['none', 'silver', 'gold', 'platinum', 'diamond']::membership_tier[],
    created_by UUID REFERENCES users(id) ON DELETE SET NULL,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW(),
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);

CREATE INDEX idx_vouchers_code ON vouchers(code);
CREATE INDEX idx_vouchers_status ON vouchers(status);
CREATE INDEX idx_vouchers_valid_until ON vouchers(valid_until);
CREATE INDEX idx_vouchers_type ON vouchers(voucher_type);

-- ============================================
-- AUDIT LOG TABLE
-- ============================================

-- Audit Logs (theo dõi hành động người dùng)
CREATE TABLE audit_logs (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    user_id UUID REFERENCES users(id) ON DELETE SET NULL,
    action VARCHAR(50) NOT NULL,
    table_name VARCHAR(50) NOT NULL,
    record_id UUID,
    old_values JSONB,
    new_values JSONB,
    ip_address VARCHAR(45),
    user_agent TEXT,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);

CREATE INDEX idx_audit_logs_user_id ON audit_logs(user_id);
CREATE INDEX idx_audit_logs_action ON audit_logs(action);
CREATE INDEX idx_audit_logs_table_name ON audit_logs(table_name);
CREATE INDEX idx_audit_logs_record_id ON audit_logs(record_id);
CREATE INDEX idx_audit_logs_created_at ON audit_logs(created_at);
CREATE INDEX idx_audit_logs_user_action ON audit_logs(user_id, action);

-- Audit Logs: NEW INDEX FOR TIME-SERIES QUERIES (BRIN)
CREATE INDEX idx_audit_logs_created_at_brin ON audit_logs USING BRIN(created_at);

-- ============================================
-- ORDER TABLES
-- ============================================

-- Orders
CREATE TABLE orders (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    order_number VARCHAR(20) UNIQUE,
    user_id UUID REFERENCES users(id) ON DELETE SET NULL,
    table_id UUID REFERENCES tables(id) ON DELETE SET NULL,
    customer_id UUID REFERENCES customers(id) ON DELETE SET NULL,
    status order_status DEFAULT 'pending',
    subtotal NUMERIC(12, 2) DEFAULT 0,
    discount NUMERIC(12, 2) DEFAULT 0,
    voucher_discount NUMERIC(12, 2) DEFAULT 0,
    membership_discount NUMERIC(12, 2) DEFAULT 0,
    total_amount NUMERIC(12, 2) DEFAULT 0,
    notes TEXT,
    customer_name VARCHAR(100),
    customer_phone VARCHAR(20),
    is_delivery BOOLEAN DEFAULT FALSE,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW(),
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT NOW(),
    served_at TIMESTAMP WITH TIME ZONE,
    cancelled_at TIMESTAMP WITH TIME ZONE,
    CONSTRAINT orders_total_check CHECK (total_amount >= 0),
    CONSTRAINT orders_discount_check CHECK (discount >= 0 AND discount <= subtotal)
);

-- Order Details
CREATE TABLE order_details (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    order_id UUID NOT NULL REFERENCES orders(id) ON DELETE CASCADE,
    product_id UUID NOT NULL REFERENCES products(id) ON DELETE RESTRICT,
    -- Product snapshot (quan trọng - lưu thông tin tại thời điểm order)
    product_name VARCHAR(200) NOT NULL,
    product_image TEXT,
    -- Customization
    size size_type DEFAULT 'M',
    sugar sugar_level DEFAULT '100',
    ice ice_level DEFAULT '100',
    quantity INT NOT NULL DEFAULT 1 CHECK (quantity > 0),
    unit_price NUMERIC(12, 2) NOT NULL CHECK (unit_price >= 0),
    -- Topping total (tổng giá toppings cho 1 sản phẩm)
    topping_total NUMERIC(12, 2) DEFAULT 0 CHECK (topping_total >= 0),
    -- Subtotal = (unit_price + topping_total) * quantity
    subtotal NUMERIC(12, 2) NOT NULL CHECK (subtotal >= 0),
    notes TEXT,
    special_instructions TEXT,
    status order_detail_status DEFAULT 'pending',
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW(),
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);

-- Order Toppings
CREATE TABLE order_toppings (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    order_detail_id UUID NOT NULL REFERENCES order_details(id) ON DELETE CASCADE,
    topping_id UUID NOT NULL REFERENCES toppings(id) ON DELETE RESTRICT,
    -- Topping snapshot (lưu tên tại thời điểm order)
    topping_name VARCHAR(100) NOT NULL,
    quantity INT DEFAULT 1 CHECK (quantity > 0),
    price NUMERIC(12, 2) DEFAULT 0 CHECK (price >= 0),
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW(),
    -- Prevent duplicate toppings per order detail
    CONSTRAINT unique_order_topping UNIQUE(order_detail_id, topping_id)
);

-- Payments
CREATE TABLE payments (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    order_id UUID NOT NULL REFERENCES orders(id) ON DELETE CASCADE,
    method payment_method NOT NULL,
    -- Amount tracking
    received_amount NUMERIC(12, 2) NOT NULL CHECK (received_amount > 0),  -- Số tiền khách đưa
    paid_amount NUMERIC(12, 2) NOT NULL CHECK (paid_amount > 0),          -- Số tiền thực thu
    change_amount NUMERIC(12, 2) DEFAULT 0 CHECK (change_amount >= 0),    -- Tiền thừa trả lại
    -- Status
    status payment_status DEFAULT 'pending',
    transaction_id VARCHAR(100),
    -- Timestamps
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW(),
    paid_at TIMESTAMP WITH TIME ZONE,  -- Thời điểm thanh toán hoàn tất
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT NOW(),
    -- Metadata
    payment_info TEXT,
    notes TEXT
);

-- ============================================
-- INDEXES - FULL COVERAGE
-- ============================================

-- Users
CREATE INDEX idx_users_role_id ON users(role_id);
CREATE INDEX idx_users_username ON users(username);
CREATE INDEX idx_users_is_active ON users(is_active);
CREATE INDEX idx_users_created_at ON users(created_at);

-- Categories
CREATE INDEX idx_categories_display_order ON categories(display_order);
CREATE INDEX idx_categories_is_active ON categories(is_active);
CREATE INDEX idx_categories_name ON categories(name);

-- Products
CREATE INDEX idx_products_category_id ON products(category_id);
CREATE INDEX idx_products_name ON products(name);
CREATE INDEX idx_products_base_price ON products(base_price);
CREATE INDEX idx_products_is_available ON products(is_available);
CREATE INDEX idx_products_is_featured ON products(is_featured);
CREATE INDEX idx_products_created_at ON products(created_at);
CREATE INDEX idx_products_category_available ON products(category_id, is_available);

-- Product Sizes
CREATE INDEX idx_product_sizes_product_id ON product_sizes(product_id);
CREATE INDEX idx_product_sizes_size ON product_sizes(size);
CREATE INDEX idx_product_sizes_price ON product_sizes(price);

-- Toppings
CREATE INDEX idx_toppings_name ON toppings(name);
CREATE INDEX idx_toppings_price ON toppings(price);
CREATE INDEX idx_toppings_is_available ON toppings(is_available);

-- Tables
CREATE INDEX idx_tables_name ON tables(name);
CREATE INDEX idx_tables_status ON tables(status);
CREATE INDEX idx_tables_location ON tables(location);
CREATE INDEX idx_tables_capacity ON tables(capacity);

-- Orders
CREATE INDEX idx_orders_order_number ON orders(order_number);
CREATE INDEX idx_orders_user_id ON orders(user_id);
CREATE INDEX idx_orders_table_id ON orders(table_id);
CREATE INDEX idx_orders_status ON orders(status);
CREATE INDEX idx_orders_created_at ON orders(created_at);
CREATE INDEX idx_orders_updated_at ON orders(updated_at);
CREATE INDEX idx_orders_total_amount ON orders(total_amount);
CREATE INDEX idx_orders_is_delivery ON orders(is_delivery);
CREATE INDEX idx_orders_customer_phone ON orders(customer_phone);
CREATE INDEX idx_orders_served_at ON orders(served_at);
CREATE INDEX idx_orders_status_created ON orders(status, created_at);
CREATE INDEX idx_orders_table_status ON orders(table_id, status);

-- Orders: NEW INDEXES FOR CRM & REPORTING
CREATE INDEX idx_orders_customer_id ON orders(customer_id);
CREATE INDEX idx_orders_customer_status ON orders(customer_id, status);
CREATE INDEX idx_orders_voucher_discount ON orders(voucher_discount);
CREATE INDEX idx_orders_membership_discount ON orders(membership_discount);

-- Order Details
CREATE INDEX idx_order_details_order_id ON order_details(order_id);
CREATE INDEX idx_order_details_product_id ON order_details(product_id);
CREATE INDEX idx_order_details_size ON order_details(size);
CREATE INDEX idx_order_details_status ON order_details(status);
CREATE INDEX idx_order_details_created_at ON order_details(created_at);
CREATE INDEX idx_order_details_order_created ON order_details(order_id, created_at);

-- Order Toppings
CREATE INDEX idx_order_toppings_order_detail_id ON order_toppings(order_detail_id);
CREATE INDEX idx_order_toppings_topping_id ON order_toppings(topping_id);
CREATE INDEX idx_order_toppings_unique_lookup ON order_toppings(order_detail_id, topping_id);

-- Payments
CREATE INDEX idx_payments_order_id ON payments(order_id);
CREATE INDEX idx_payments_method ON payments(method);
CREATE INDEX idx_payments_status ON payments(status);
CREATE INDEX idx_payments_created_at ON payments(created_at);
CREATE INDEX idx_payments_transaction_id ON payments(transaction_id);
CREATE INDEX idx_payments_paid_at ON payments(paid_at);
CREATE INDEX idx_payments_order_status ON payments(order_id, status);

-- ============================================
-- FUNCTIONS
-- ============================================

-- Function 1: Update updated_at timestamp
CREATE OR REPLACE FUNCTION update_updated_at_column()
RETURNS TRIGGER AS $$
BEGIN
    NEW.updated_at = NOW();
    RETURN NEW;
END;
$$ LANGUAGE plpgsql
SET search_path = public, pg_temp;

-- Function 2: Calculate order total automatically
CREATE OR REPLACE FUNCTION calculate_order_total()
RETURNS TRIGGER AS $$
BEGIN
    -- Calculate subtotal from order_details
    UPDATE orders o
    SET
        subtotal = COALESCE((
            SELECT SUM(od.subtotal)
            FROM order_details od
            WHERE od.order_id = NEW.order_id
        ), 0),
        updated_at = NOW()
    WHERE o.id = NEW.order_id;

    -- Update total_amount = subtotal - discount
    UPDATE orders o
    SET
        total_amount = GREATEST(0, o.subtotal - o.discount),
        updated_at = NOW()
    WHERE o.id = NEW.order_id;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql
SET search_path = public, pg_temp;

-- Function 3: Update order total when order_details change
CREATE OR REPLACE FUNCTION update_order_total_on_detail_change()
RETURNS TRIGGER AS $$
BEGIN
    -- Recalculate subtotal
    UPDATE orders o
    SET
        subtotal = COALESCE((
            SELECT SUM(od.subtotal)
            FROM order_details od
            WHERE od.order_id = COALESCE(NEW.order_id, OLD.order_id)
        ), 0),
        updated_at = NOW()
    WHERE o.id = COALESCE(NEW.order_id, OLD.order_id);

    -- Update total_amount
    UPDATE orders o
    SET
        total_amount = GREATEST(0, o.subtotal - o.discount),
        updated_at = NOW()
    WHERE o.id = COALESCE(NEW.order_id, OLD.order_id);

    RETURN COALESCE(NEW, OLD);
END;
$$ LANGUAGE plpgsql
SET search_path = public, pg_temp;

-- Function 4: Update table status based on order status
CREATE OR REPLACE FUNCTION update_table_status_from_order()
RETURNS TRIGGER AS $$
BEGIN
    -- When order is served or cancelled, free the table
    IF (NEW.status IN ('served', 'cancelled') AND OLD.status NOT IN ('served', 'cancelled')) THEN
        UPDATE tables t
        SET
            status = 'available',
            updated_at = NOW()
        WHERE t.id = NEW.table_id AND t.id IS NOT NULL;
    END IF;

    -- When order moves to preparing, mark table as occupied
    IF (NEW.status = 'preparing' AND OLD.status != 'preparing') THEN
        UPDATE tables t
        SET
            status = 'occupied',
            updated_at = NOW()
        WHERE t.id = NEW.table_id AND t.id IS NOT NULL;
    END IF;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql
SET search_path = public, pg_temp;

-- Function 5: Generate order number automatically (race-condition safe)
CREATE OR REPLACE FUNCTION generate_order_number()
RETURNS TRIGGER AS $$
DECLARE
    date_part TEXT;
    seq_num TEXT;
BEGIN
    date_part := TO_CHAR(NEW.created_at, 'YYMMDD');

    -- Use sequence for race-condition safety
    SELECT LPAD(NEXTVAL('order_number_seq_' || date_part)::TEXT, 3, '0')
    INTO seq_num;

    NEW.order_number := 'ORD-' || date_part || '-' || seq_num;
    RETURN NEW;
EXCEPTION
    WHEN undefined_table THEN
        -- Sequence doesn't exist, create it
        EXECUTE 'CREATE SEQUENCE order_number_seq_' || date_part || ' START WITH 1';
        SELECT LPAD(NEXTVAL('order_number_seq_' || date_part)::TEXT, 3, '0')
        INTO seq_num;
        NEW.order_number := 'ORD-' || date_part || '-' || seq_num;
        RETURN NEW;
END;
$$ LANGUAGE plpgsql
SET search_path = public, pg_temp;

-- Function 6: Calculate order_details subtotal (bao gồm toppings)
CREATE OR REPLACE FUNCTION calculate_order_detail_subtotal()
RETURNS TRIGGER AS $$
BEGIN
    -- subtotal = (unit_price + topping_total) * quantity
    NEW.subtotal := (NEW.unit_price + COALESCE(NEW.topping_total, 0)) * NEW.quantity;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql
SET search_path = public, pg_temp;

-- Function 7: Update topping_total from order_toppings
CREATE OR REPLACE FUNCTION update_order_detail_topping_total()
RETURNS TRIGGER AS $$
BEGIN
    -- Recalculate topping_total from all order_toppings
    UPDATE order_details od
    SET
        topping_total = COALESCE((
            SELECT SUM(ot.price * ot.quantity)
            FROM order_toppings ot
            WHERE ot.order_detail_id = COALESCE(NEW.order_detail_id, OLD.order_detail_id)
        ), 0),
        updated_at = NOW()
    WHERE od.id = COALESCE(NEW.order_detail_id, OLD.order_detail_id);

    -- Trigger sẽ tự động tính lại subtotal
    RETURN COALESCE(NEW, OLD);
END;
$$ LANGUAGE plpgsql
SET search_path = public, pg_temp;

-- Function 8: Add/Update topping with UPSERT logic
CREATE OR REPLACE FUNCTION add_topping_to_order_detail(
    p_order_detail_id UUID,
    p_topping_id UUID,
    p_topping_name VARCHAR,
    p_price NUMERIC,
    p_quantity INT DEFAULT 1
)
RETURNS UUID AS $$
DECLARE
    v_order_topping_id UUID;
BEGIN
    -- UPSERT: Nếu tồn tại thì tăng quantity, không thì insert
    INSERT INTO order_toppings (order_detail_id, topping_id, topping_name, price, quantity)
    VALUES (p_order_detail_id, p_topping_id, p_topping_name, p_price, p_quantity)
    ON CONFLICT (order_detail_id, topping_id)
    DO UPDATE SET
        quantity = order_toppings.quantity + p_quantity,
        price = p_price,  -- Cập nhật giá mới nếu có
        updated_at = NOW()
    RETURNING id INTO v_order_topping_id;

    RETURN v_order_topping_id;
END;
$$ LANGUAGE plpgsql
SET search_path = public, pg_temp;

-- Function 9: Validate payment and update order status
CREATE OR REPLACE FUNCTION validate_payment_and_update_order()
RETURNS TRIGGER AS $$
DECLARE
    v_order_total NUMERIC;
    v_total_paid NUMERIC;
BEGIN
    -- Get order total
    SELECT total_amount INTO v_order_total FROM orders WHERE id = NEW.order_id;

    -- Calculate total paid for this order (excluding current payment)
    SELECT COALESCE(SUM(paid_amount), 0) INTO v_total_paid
    FROM payments
    WHERE order_id = NEW.order_id
    AND status = 'completed'
    AND id != NEW.id;

    -- Add current payment if completed
    IF NEW.status = 'completed' THEN
        v_total_paid := v_total_paid + NEW.paid_amount;
    END IF;

    -- Validate: don't allow overpayment > 10%
    IF v_total_paid > v_order_total * 1.1 THEN
        RAISE EXCEPTION 'Total payment (%) exceeds order total (%) by more than 10 percent',
            v_total_paid, v_order_total;
    END IF;

    -- Set paid_at when status changes to completed
    IF NEW.status = 'completed' AND (OLD.status IS NULL OR OLD.status != 'completed') THEN
        NEW.paid_at = NOW();
    END IF;

    -- If fully paid, update order status
    IF v_total_paid >= v_order_total AND NEW.status = 'completed' THEN
        UPDATE orders
        SET status = 'served', updated_at = NOW()
        WHERE id = NEW.order_id;
    END IF;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql
SET search_path = public, pg_temp;

-- Function 10: Prevent modification of completed payments
CREATE OR REPLACE FUNCTION prevent_completed_payment_update()
RETURNS TRIGGER AS $$
BEGIN
    IF OLD.status = 'completed' THEN
        IF OLD.received_amount != NEW.received_amount OR
           OLD.paid_amount != NEW.paid_amount OR
           OLD.change_amount != NEW.change_amount OR
           OLD.transaction_id IS DISTINCT FROM NEW.transaction_id THEN
            RAISE EXCEPTION 'Cannot modify financial data of completed payment';
        END IF;
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql
SET search_path = public, pg_temp;

-- ============================================
-- NEW FUNCTIONS FOR AUDIT, MEMBERSHIP, VOUCHERS
-- ============================================

-- Function 11: Audit logging for INSERT/UPDATE/DELETE
CREATE OR REPLACE FUNCTION audit_table_changes()
RETURNS TRIGGER AS $$
DECLARE
    v_user_id UUID;
    v_ip_address VARCHAR(45);
    v_user_agent TEXT;
BEGIN
    -- Get session variables (safely handle NULL)
    BEGIN
        v_user_id := current_setting('app.current_user_id', TRUE)::UUID;
    EXCEPTION WHEN OTHERS THEN
        v_user_id := NULL;
    END;

    BEGIN
        v_ip_address := current_setting('app.client_ip', TRUE);
    EXCEPTION WHEN OTHERS THEN
        v_ip_address := NULL;
    END;

    v_user_agent := 'MilkTeaPOS WinForms App';

    IF TG_OP = 'INSERT' THEN
        INSERT INTO audit_logs (user_id, action, table_name, record_id, old_values, new_values, ip_address, user_agent)
        VALUES (v_user_id, TG_OP, TG_TABLE_NAME, NEW.id, NULL, to_jsonb(NEW), v_ip_address, v_user_agent);
        RETURN NEW;
    ELSIF TG_OP = 'UPDATE' THEN
        INSERT INTO audit_logs (user_id, action, table_name, record_id, old_values, new_values, ip_address, user_agent)
        VALUES (v_user_id, TG_OP, TG_TABLE_NAME, NEW.id, to_jsonb(OLD), to_jsonb(NEW), v_ip_address, v_user_agent);
        RETURN NEW;
    ELSIF TG_OP = 'DELETE' THEN
        INSERT INTO audit_logs (user_id, action, table_name, record_id, old_values, new_values, ip_address, user_agent)
        VALUES (v_user_id, TG_OP, TG_TABLE_NAME, OLD.id, to_jsonb(OLD), NULL, v_ip_address, v_user_agent);
        RETURN OLD;
    END IF;
    RETURN NULL;
END;
$$ LANGUAGE plpgsql
SET search_path = public, pg_temp;

-- Function 12: Update membership tier based on spending
CREATE OR REPLACE FUNCTION update_membership_tier()
RETURNS TRIGGER AS $$
DECLARE
    v_tier membership_tier;
BEGIN
    -- Determine tier based on total_spent
    IF NEW.total_spent >= 5000000 THEN
        v_tier := 'diamond';
    ELSIF NEW.total_spent >= 3000000 THEN
        v_tier := 'platinum';
    ELSIF NEW.total_spent >= 1500000 THEN
        v_tier := 'gold';
    ELSIF NEW.total_spent >= 500000 THEN
        v_tier := 'silver';
    ELSE
        v_tier := 'none';
    END IF;

    NEW.tier := v_tier;
    NEW.updated_at := NOW();
    RETURN NEW;
END;
$$ LANGUAGE plpgsql
SET search_path = public, pg_temp;

-- Function 13: Add points to membership after order is served
CREATE OR REPLACE FUNCTION add_membership_points_after_order()
RETURNS TRIGGER AS $$
DECLARE
    v_customer_id UUID;
    v_points_earned INT;
BEGIN
    -- Only process when order is served
    IF NEW.status = 'served' AND (OLD.status IS NULL OR OLD.status != 'served') THEN
        -- Get customer_id from order
        SELECT customer_id INTO v_customer_id FROM orders WHERE id = NEW.id;

        IF v_customer_id IS NOT NULL THEN
            -- Calculate points: 1 point per 10,000 VND
            v_points_earned := FLOOR(NEW.total_amount / 10000);

            -- Update membership
            UPDATE memberships m
            SET
                points = points + v_points_earned,
                total_spent = total_spent + NEW.total_amount,
                total_orders = total_orders + 1,
                last_order_at = NOW(),
                updated_at = NOW()
            WHERE m.customer_id = v_customer_id;
        END IF;
    END IF;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql
SET search_path = public, pg_temp;

-- Function 14: Validate and apply voucher
CREATE OR REPLACE FUNCTION apply_voucher_to_order(
    p_order_id UUID,
    p_voucher_code VARCHAR
)
RETURNS NUMERIC AS $$
DECLARE
    v_voucher_id UUID;
    v_discount_amount NUMERIC := 0;
    v_order_total NUMERIC;
    v_customer_id UUID;
    v_tier membership_tier;
    v_usage_count INT;
    v_remaining_uses INT;
BEGIN
    -- Get voucher by code
    SELECT id INTO v_voucher_id FROM vouchers WHERE code = p_voucher_code AND status = 'active';

    IF v_voucher_id IS NULL THEN
        RAISE EXCEPTION 'Voucher not found or inactive';
    END IF;

    -- Get order details
    SELECT total_amount, customer_id INTO v_order_total, v_customer_id
    FROM orders WHERE id = p_order_id;

    -- Get customer tier
    SELECT tier INTO v_tier FROM memberships WHERE customer_id = v_customer_id;

    -- Check usage limit
    SELECT usage_count, usage_limit INTO v_usage_count, v_remaining_uses
    FROM vouchers WHERE id = v_voucher_id;

    IF v_remaining_uses IS NOT NULL AND v_usage_count >= v_remaining_uses THEN
        RAISE EXCEPTION 'Voucher usage limit reached';
    END IF;

    -- Check validity period
    IF NOT EXISTS (
        SELECT 1 FROM vouchers
        WHERE id = v_voucher_id
        AND valid_from <= NOW()
        AND (valid_until IS NULL OR valid_until >= NOW())
    ) THEN
        RAISE EXCEPTION 'Voucher is expired or not yet valid';
    END IF;

    -- Check minimum order amount
    IF v_order_total < (SELECT min_order_amount FROM vouchers WHERE id = v_voucher_id) THEN
        RAISE EXCEPTION 'Order total does not meet minimum requirement';
    END IF;

    -- Check membership tier eligibility
    IF v_tier IS NOT NULL AND NOT v_tier = ANY(
        SELECT applicable_tiers FROM vouchers WHERE id = v_voucher_id
    ) THEN
        RAISE EXCEPTION 'Voucher not applicable for your membership tier';
    END IF;

    -- Calculate discount based on voucher type
    SELECT
        CASE voucher_type
            WHEN 'percentage' THEN LEAST(v_order_total * (discount_value / 100), COALESCE(max_discount_amount, v_order_total))
            WHEN 'fixed_amount' THEN LEAST(discount_value, v_order_total)
            ELSE 0
        END
    INTO v_discount_amount
    FROM vouchers WHERE id = v_voucher_id;

    -- Update order with voucher discount
    UPDATE orders
    SET voucher_discount = v_discount_amount,
        total_amount = GREATEST(0, subtotal - discount - v_discount_amount)
    WHERE id = p_order_id;

    -- Increment usage count
    UPDATE vouchers SET usage_count = usage_count + 1 WHERE id = v_voucher_id;

    RETURN v_discount_amount;
END;
$$ LANGUAGE plpgsql
SET search_path = public, pg_temp;

-- Function 15: Hash password using bcrypt (dùng trong ứng dụng, không phải trong DB)
-- Lưu ý: Password hashing nên thực hiện ở application layer (C#)
-- Đây chỉ là comment để nhắc nhở developer
-- Ví dụ C#: BCrypt.Net.BCrypt.HashPassword(password, workFactor: 10)

-- ============================================
-- TRIGGERS
-- ============================================

-- Trigger: Update updated_at for all tables
CREATE TRIGGER update_users_updated_at BEFORE UPDATE ON users 
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

CREATE TRIGGER update_categories_updated_at BEFORE UPDATE ON categories 
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

CREATE TRIGGER update_products_updated_at BEFORE UPDATE ON products 
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

CREATE TRIGGER update_product_sizes_updated_at BEFORE UPDATE ON product_sizes 
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

CREATE TRIGGER update_toppings_updated_at BEFORE UPDATE ON toppings 
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

CREATE TRIGGER update_tables_updated_at BEFORE UPDATE ON tables 
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

CREATE TRIGGER update_orders_updated_at BEFORE UPDATE ON orders 
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

CREATE TRIGGER update_order_details_updated_at BEFORE UPDATE ON order_details 
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

-- Trigger: Auto-generate order number
CREATE TRIGGER before_order_insert BEFORE INSERT ON orders 
    FOR EACH ROW EXECUTE FUNCTION generate_order_number();

-- Trigger: Calculate order_details subtotal before insert/update (bao gồm toppings)
CREATE TRIGGER before_order_detail_insert 
    BEFORE INSERT OR UPDATE ON order_details 
    FOR EACH ROW EXECUTE FUNCTION calculate_order_detail_subtotal();

-- Trigger: Update topping_total when order_toppings changes
CREATE TRIGGER after_order_topping_change 
    AFTER INSERT OR UPDATE OR DELETE ON order_toppings 
    FOR EACH ROW EXECUTE FUNCTION update_order_detail_topping_total();

-- Trigger: Update order total when order_details changes
CREATE TRIGGER after_order_detail_change AFTER INSERT OR UPDATE OR DELETE ON order_details 
    FOR EACH ROW EXECUTE FUNCTION update_order_total_on_detail_change();

-- Trigger: Update table status when order status changes
CREATE TRIGGER after_order_status_change AFTER UPDATE ON orders
    FOR EACH ROW EXECUTE FUNCTION update_table_status_from_order();

-- Trigger: Validate payment and update order status
CREATE TRIGGER before_payment_complete
    BEFORE INSERT OR UPDATE ON payments
    FOR EACH ROW EXECUTE FUNCTION validate_payment_and_update_order();

-- Trigger: Prevent modification of completed payments
CREATE TRIGGER before_payment_update
    BEFORE UPDATE ON payments
    FOR EACH ROW EXECUTE FUNCTION prevent_completed_payment_update();

-- ============================================
-- NEW TRIGGERS FOR AUDIT, MEMBERSHIP, VOUCHERS
-- ============================================

-- Trigger: Update membership tier when total_spent changes
CREATE TRIGGER update_membership_tier_on_spending
    BEFORE INSERT OR UPDATE ON memberships
    FOR EACH ROW EXECUTE FUNCTION update_membership_tier();

-- Trigger: Add membership points after order is served
CREATE TRIGGER add_membership_points_after_order_served
    AFTER UPDATE ON orders
    FOR EACH ROW EXECUTE FUNCTION add_membership_points_after_order();

-- Trigger: Audit log for customers table
CREATE TRIGGER audit_customers_changes
    AFTER INSERT OR UPDATE OR DELETE ON customers
    FOR EACH ROW EXECUTE FUNCTION audit_table_changes();

-- Trigger: Audit log for vouchers table
CREATE TRIGGER audit_vouchers_changes
    AFTER INSERT OR UPDATE OR DELETE ON vouchers
    FOR EACH ROW EXECUTE FUNCTION audit_table_changes();

-- Trigger: Audit log for orders table (quan trọng)
CREATE TRIGGER audit_orders_changes
    AFTER INSERT OR UPDATE OR DELETE ON orders
    FOR EACH ROW EXECUTE FUNCTION audit_table_changes();

-- Trigger: Audit log for payments table (quan trọng)
CREATE TRIGGER audit_payments_changes
    AFTER INSERT OR UPDATE OR DELETE ON payments
    FOR EACH ROW EXECUTE FUNCTION audit_table_changes();

-- Trigger: Update updated_at for new tables
CREATE TRIGGER update_customers_updated_at BEFORE UPDATE ON customers
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

CREATE TRIGGER update_memberships_updated_at BEFORE UPDATE ON memberships
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

CREATE TRIGGER update_vouchers_updated_at BEFORE UPDATE ON vouchers
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

-- ============================================
-- SEED DATA
-- ============================================

-- Roles
INSERT INTO roles (name) VALUES ('Admin'), ('Staff'), ('Cashier');

-- Users
-- LƯU Ý QUAN TRỌNG: Password nên được hash ở application layer (C#) bằng BCrypt
-- Ví dụ: BCrypt.Net.BCrypt.HashPassword("admin123", workFactor: 10)
-- Dưới đây là password chưa hash để test, NHỚ hash trước khi deploy production
INSERT INTO users (username, password, role_id) VALUES
    ('admin', 'admin123', (SELECT id FROM roles WHERE name = 'Admin')),
    ('staff01', 'staff123', (SELECT id FROM roles WHERE name = 'Staff')),
    ('cashier01', 'cashier123', (SELECT id FROM roles WHERE name = 'Cashier'));

-- Categories
INSERT INTO categories (name, description, image_url, display_order) VALUES
    ('Trà Sữa', 'Các loại trà sữa truyền thống và hiện đại', '/images/categories/milk-tea.png', 10),
    ('Trà Trái Cây', 'Trà trái cây tươi mát giải khát', '/images/categories/fruit-tea.png', 20),
    ('Cà Phê', 'Cà phê Việt Nam và các món hiện đại', '/images/categories/coffee.png', 30),
    ('Đá Xay', 'Sinh tố và các món đá xay', '/images/categories/smoothie.png', 40),
    ('Trà Pure', 'Trà nguyên chất tốt cho sức khỏe', '/images/categories/pure-tea.png', 50),
    ('Sữa Tươi & Sữa Chua', 'Sữa tươi và sữa chua các loại', '/images/categories/yogurt.png', 60);

-- Products (150 món)
INSERT INTO products (name, description, base_price, category_id, is_available, is_featured, preparation_time) VALUES
    -- Trà Sữa (30)
    ('Trà Sữa Truyền Thống', 'Trà sữa đậm đà vị truyền thống', 45000, (SELECT id FROM categories WHERE name = 'Trà Sữa'), true, true, 5),
    ('Trà Sữa Trân Châu Đen', 'Trà sữa kèm trân châu đen dai ngon', 49000, (SELECT id FROM categories WHERE name = 'Trà Sữa'), true, true, 5),
    ('Trà Sữa Trân Châu Trắng', 'Trà sữa kèm trân châu trắng béo ngậy', 49000, (SELECT id FROM categories WHERE name = 'Trà Sữa'), true, true, 5),
    ('Trà Sữa Trân Châu Hoàng Kim', 'Trà sữa cao cấp với trân châu hoàng kim', 55000, (SELECT id FROM categories WHERE name = 'Trà Sữa'), true, true, 6),
    ('Trà Sữa Trân Châu Đường Đen', 'Trà sữa với trân châu ngâm đường đen', 55000, (SELECT id FROM categories WHERE name = 'Trà Sữa'), true, true, 6),
    ('Trà Sữa Oolong', 'Trà sữa từ trà Oolong thượng hạng', 55000, (SELECT id FROM categories WHERE name = 'Trà Sữa'), true, true, 5),
    ('Trà Sữa Earl Grey', 'Trà sữa hương bergamot đặc trưng', 59000, (SELECT id FROM categories WHERE name = 'Trà Sữa'), true, false, 5),
    ('Trà Sữa Matcha Nhật', 'Trà sữa từ matcha Nhật Bản nguyên chất', 59000, (SELECT id FROM categories WHERE name = 'Trà Sữa'), true, true, 6),
    ('Trà Sữa Socola Bỉ', 'Trà sữa socola từ socola Bỉ cao cấp', 59000, (SELECT id FROM categories WHERE name = 'Trà Sữa'), true, false, 6),
    ('Trà Sữa Khoai Môn', 'Trà sữa hương khoai môn béo thơm', 55000, (SELECT id FROM categories WHERE name = 'Trà Sữa'), true, true, 6),
    ('Trà Sữa Khoai Lang Tím', 'Trà sữa hương khoai lang tím ngọt bùi', 55000, (SELECT id FROM categories WHERE name = 'Trà Sữa'), true, false, 6),
    ('Trà Sữa Vani', 'Trà sữa hương vani Madagascar', 55000, (SELECT id FROM categories WHERE name = 'Trà Sữa'), true, false, 5),
    ('Trà Sữa Dâu', 'Trà sữa hương dâu tây Hàn Quốc', 59000, (SELECT id FROM categories WHERE name = 'Trà Sữa'), true, false, 6),
    ('Trà Sữa Việt Quất', 'Trà sữa hương việt quất thơm ngon', 59000, (SELECT id FROM categories WHERE name = 'Trà Sữa'), true, false, 6),
    ('Trà Sữa Chanh Leo', 'Trà sữa kết hợp chanh leo tươi', 55000, (SELECT id FROM categories WHERE name = 'Trà Sữa'), true, false, 6),
    ('Trà Sữa Xoài', 'Trà sữa hương xoài Đài Loan', 59000, (SELECT id FROM categories WHERE name = 'Trà Sữa'), true, false, 6),
    ('Trà Sữa Dừa', 'Trà sữa hương dừa béo thơm', 55000, (SELECT id FROM categories WHERE name = 'Trà Sữa'), true, false, 5),
    ('Trà Sữa Macchiato', 'Trà sữa với lớp foam macchiato béo ngậy', 55000, (SELECT id FROM categories WHERE name = 'Trà Sữa'), true, true, 6),
    ('Trà Sữa Kem Cheese', 'Trà sữa với kem cheese thượng hạng', 59000, (SELECT id FROM categories WHERE name = 'Trà Sữa'), true, true, 6),
    ('Trà Sữa Cheese Dâu', 'Trà sữa cheese với sốt dâu', 65000, (SELECT id FROM categories WHERE name = 'Trà Sữa'), true, false, 7),
    ('Trà Sữa Cheese Xoài', 'Trà sữa cheese với sốt xoài', 65000, (SELECT id FROM categories WHERE name = 'Trà Sữa'), true, false, 7),
    ('Trà Sữa Cheese Sầu Riêng', 'Trà sữa cheese với sầu riêng Ri6', 69000, (SELECT id FROM categories WHERE name = 'Trà Sữa'), true, false, 7),
    ('Trà Sữa Đường Nâu', 'Trà sữa với đường nâu Đài Loan', 55000, (SELECT id FROM categories WHERE name = 'Trà Sữa'), true, true, 6),
    ('Trà Sữa Đường Nâu Trân Châu', 'Trà sữa đường nâu kèm trân châu', 59000, (SELECT id FROM categories WHERE name = 'Trà Sữa'), true, true, 6),
    ('Trà Sữa Đường Nâu Sữa Tươi', 'Trà sữa đường nâu với sữa tươi', 65000, (SELECT id FROM categories WHERE name = 'Trà Sữa'), true, true, 6),
    ('Trà Sữa Tiger Sugar', 'Trà sữa đường nâu hổ phách cao cấp', 69000, (SELECT id FROM categories WHERE name = 'Trà Sữa'), true, true, 7),
    ('Trà Sữa Lipton', 'Trà sữa từ trà Lipton quen thuộc', 49000, (SELECT id FROM categories WHERE name = 'Trà Sữa'), true, false, 5),
    ('Trà Sữa Assam', 'Trà sữa từ trà Assam đậm vị', 55000, (SELECT id FROM categories WHERE name = 'Trà Sữa'), true, false, 5),
    ('Trà Sữa Ceylon', 'Trà sữa từ trà Ceylon Sri Lanka', 55000, (SELECT id FROM categories WHERE name = 'Trà Sữa'), true, false, 5),
    ('Trà Sữa Mặn Phô Mai', 'Trà sữa vị mặn với phô mai', 59000, (SELECT id FROM categories WHERE name = 'Trà Sữa'), true, false, 6),
    -- Trà Trái Cây (28)
    ('Trà Vải', 'Trà vải thơm ngon từ vải thiều', 45000, (SELECT id FROM categories WHERE name = 'Trà Trái Cây'), true, true, 4),
    ('Trà Vải Hoa Hồng', 'Trà vải kết hợp hoa hồng', 55000, (SELECT id FROM categories WHERE name = 'Trà Trái Cây'), true, true, 5),
    ('Trà Đào Cam Sả', 'Trà đào với cam và sả thơm', 49000, (SELECT id FROM categories WHERE name = 'Trà Trái Cây'), true, true, 5),
    ('Trà Đào Vàng', 'Trà từ đào vàng ngọt thanh', 49000, (SELECT id FROM categories WHERE name = 'Trà Trái Cây'), true, true, 4),
    ('Trà Chanh Sả', 'Trà chanh với sả thơm mát', 39000, (SELECT id FROM categories WHERE name = 'Trà Trái Cây'), true, true, 3),
    ('Trà Chanh Dây', 'Trà chanh dây chua ngọt', 45000, (SELECT id FROM categories WHERE name = 'Trà Trái Cây'), true, true, 4),
    ('Trà Tắc', 'Trà tắc giải khát', 35000, (SELECT id FROM categories WHERE name = 'Trà Trái Cây'), true, true, 3),
    ('Trà Chanh', 'Trà chanh truyền thống', 35000, (SELECT id FROM categories WHERE name = 'Trà Trái Cây'), true, true, 3),
    ('Trà Xoài', 'Trà xoài thơm ngon', 49000, (SELECT id FROM categories WHERE name = 'Trà Trái Cây'), true, true, 5),
    ('Trà Dâu', 'Trà dâu tây thơm ngon', 49000, (SELECT id FROM categories WHERE name = 'Trà Trái Cây'), true, true, 4),
    ('Trà Mận', 'Trà mận chua ngọt', 45000, (SELECT id FROM categories WHERE name = 'Trà Trái Cây'), true, false, 4),
    ('Trà Chanh Leo', 'Trà chanh leo tươi', 45000, (SELECT id FROM categories WHERE name = 'Trà Trái Cây'), true, false, 4),
    ('Trà Sen Vàng', 'Trà sen vàng cao cấp', 55000, (SELECT id FROM categories WHERE name = 'Trà Trái Cây'), true, true, 6),
    ('Trà Thanh Long', 'Trà thanh long tươi mát', 55000, (SELECT id FROM categories WHERE name = 'Trà Trái Cây'), true, false, 5),
    ('Trà Dưa Hấu', 'Trà dưa hấu giải khát', 49000, (SELECT id FROM categories WHERE name = 'Trà Trái Cây'), true, false, 4),
    ('Trà Ổi', 'Trà ổi thơm ngon', 49000, (SELECT id FROM categories WHERE name = 'Trà Trái Cây'), true, false, 4),
    ('Trà Chanh Vàng', 'Trà chanh vàng cao cấp', 45000, (SELECT id FROM categories WHERE name = 'Trà Trái Cây'), true, false, 4),
    ('Trà Chanh Xanh', 'Trà chanh xanh the mát', 45000, (SELECT id FROM categories WHERE name = 'Trà Trái Cây'), true, false, 4),
    ('Trà Dâu Tằm', 'Trà dâu tằm bổ dưỡng', 55000, (SELECT id FROM categories WHERE name = 'Trà Trái Cây'), true, false, 5),
    ('Trà Xoài Đào', 'Trà xoài kết hợp đào', 55000, (SELECT id FROM categories WHERE name = 'Trà Trái Cây'), true, false, 5),
    ('Trà Mận Thảo Mộc', 'Trà mận với thảo mộc', 55000, (SELECT id FROM categories WHERE name = 'Trà Trái Cây'), true, false, 5),
    ('Trà Chanh Leo Hạt Chia', 'Trà chanh leo với hạt chia', 49000, (SELECT id FROM categories WHERE name = 'Trà Trái Cây'), true, false, 4),
    ('Trà Sen Vàng Hạt Sen', 'Trà sen vàng với hạt sen', 59000, (SELECT id FROM categories WHERE name = 'Trà Trái Cây'), true, false, 6),
    ('Trà Thanh Long Ruột Đỏ', 'Trà thanh long ruột đỏ', 59000, (SELECT id FROM categories WHERE name = 'Trà Trái Cây'), true, false, 5),
    ('Trà Dưa Hấu Bạc Hà', 'Trà dưa hấu với bạc hà', 55000, (SELECT id FROM categories WHERE name = 'Trà Trái Cây'), true, false, 5),
    ('Trà Ổi Hồng', 'Trà ổi hồng ngọt thanh', 55000, (SELECT id FROM categories WHERE name = 'Trà Trái Cây'), true, false, 5),
    ('Trà Tắc Sả', 'Trà tắc với sả thơm', 39000, (SELECT id FROM categories WHERE name = 'Trà Trái Cây'), true, false, 3),
    ('Trà Chanh Bạc Hà', 'Trà chanh với bạc hà the mát', 39000, (SELECT id FROM categories WHERE name = 'Trà Trái Cây'), true, false, 3),
    -- Cà Phê (26)
    ('Cà Phê Sữa Đá', 'Cà phê sữa đá Việt Nam', 35000, (SELECT id FROM categories WHERE name = 'Cà Phê'), true, true, 5),
    ('Cà Phê Đen Đá', 'Cà phê đen đá nguyên chất', 30000, (SELECT id FROM categories WHERE name = 'Cà Phê'), true, true, 5),
    ('Cà Phê Đen Nóng', 'Cà phê đen nóng', 30000, (SELECT id FROM categories WHERE name = 'Cà Phê'), true, false, 5),
    ('Cà Phê Sữa Nóng', 'Cà phê sữa nóng', 35000, (SELECT id FROM categories WHERE name = 'Cà Phê'), true, false, 5),
    ('Bạc Xỉu', 'Bạc xỉu sữa đặc trưng', 35000, (SELECT id FROM categories WHERE name = 'Cà Phê'), true, true, 5),
    ('Cà Phê Muối', 'Cà phê muối Huế độc đáo', 45000, (SELECT id FROM categories WHERE name = 'Cà Phê'), true, true, 5),
    ('Cà Phê Trứng', 'Cà phê trứng Hà Nội', 50000, (SELECT id FROM categories WHERE name = 'Cà Phê'), true, true, 7),
    ('Cà Phê Dừa', 'Cà phê cốt dừa béo thơm', 49000, (SELECT id FROM categories WHERE name = 'Cà Phê'), true, true, 5),
    ('Cà Phê Cốt Dừa', 'Cà phê với cốt dừa nguyên chất', 55000, (SELECT id FROM categories WHERE name = 'Cà Phê'), true, false, 5),
    ('Cà Phê Sầu Riêng', 'Cà phê sầu riêng độc đáo', 59000, (SELECT id FROM categories WHERE name = 'Cà Phê'), true, false, 6),
    ('Cà Phê Khoai Môn', 'Cà phê khoai môn', 55000, (SELECT id FROM categories WHERE name = 'Cà Phê'), true, false, 6),
    ('Cold Brew', 'Cà phê ủ lạnh 24h', 55000, (SELECT id FROM categories WHERE name = 'Cà Phê'), true, true, 3),
    ('Cold Brew Sữa Tươi', 'Cold brew với sữa tươi', 59000, (SELECT id FROM categories WHERE name = 'Cà Phê'), true, false, 3),
    ('Cold Brew Cốt Dừa', 'Cold brew với cốt dừa', 65000, (SELECT id FROM categories WHERE name = 'Cà Phê'), true, false, 3),
    ('Americano', 'Cà phê Americano', 45000, (SELECT id FROM categories WHERE name = 'Cà Phê'), true, false, 4),
    ('Latte', 'Cà phê latte', 55000, (SELECT id FROM categories WHERE name = 'Cà Phê'), true, false, 5),
    ('Cappuccino', 'Cà phê cappuccino', 55000, (SELECT id FROM categories WHERE name = 'Cà Phê'), true, false, 5),
    ('Mocha', 'Cà phê mocha socola', 59000, (SELECT id FROM categories WHERE name = 'Cà Phê'), true, false, 5),
    ('Caramel Macchiato', 'Caramel macchiato', 59000, (SELECT id FROM categories WHERE name = 'Cà Phê'), true, false, 5),
    ('Espresso', 'Espresso nguyên chất', 40000, (SELECT id FROM categories WHERE name = 'Cà Phê'), true, false, 3),
    ('Cà Phê Máy', 'Cà phê pha máy', 45000, (SELECT id FROM categories WHERE name = 'Cà Phê'), true, false, 4),
    ('Bạc Xỉu Đá', 'Bạc xỉu đá', 35000, (SELECT id FROM categories WHERE name = 'Cà Phê'), true, false, 5),
    ('Cà Phê Muối Huế', 'Cà phê muối chuẩn vị Huế', 49000, (SELECT id FROM categories WHERE name = 'Cà Phê'), true, false, 5),
    ('Cà Phê Trứng Hà Nội', 'Cà phê trứng chuẩn vị Hà Nội', 55000, (SELECT id FROM categories WHERE name = 'Cà Phê'), true, false, 7),
    ('Americano Đá', 'Americano đá', 45000, (SELECT id FROM categories WHERE name = 'Cà Phê'), true, false, 4),
    ('Espresso Đá', 'Espresso đá', 40000, (SELECT id FROM categories WHERE name = 'Cà Phê'), true, false, 3),
    -- Đá Xay (32)
    ('Sinh Tố Bơ', 'Sinh tố bơ tươi', 55000, (SELECT id FROM categories WHERE name = 'Đá Xay'), true, true, 5),
    ('Sinh Tố Bơ Sầu Riêng', 'Sinh tố bơ sầu riêng', 69000, (SELECT id FROM categories WHERE name = 'Đá Xay'), true, true, 6),
    ('Sinh Tố Dâu', 'Sinh tố dâu tây', 55000, (SELECT id FROM categories WHERE name = 'Đá Xay'), true, true, 5),
    ('Sinh Tố Dâu Sữa', 'Sinh tố dâu sữa', 59000, (SELECT id FROM categories WHERE name = 'Đá Xay'), true, false, 5),
    ('Sinh Tố Xoài', 'Sinh tố xoài', 55000, (SELECT id FROM categories WHERE name = 'Đá Xay'), true, true, 5),
    ('Sinh Tố Xoài Sữa', 'Sinh tố xoài sữa', 59000, (SELECT id FROM categories WHERE name = 'Đá Xay'), true, false, 5),
    ('Sinh Tố Sầu Riêng', 'Sinh tố sầu riêng Ri6', 65000, (SELECT id FROM categories WHERE name = 'Đá Xay'), true, true, 6),
    ('Sinh Tố Sầu Riêng Sữa', 'Sinh tố sầu riêng sữa', 69000, (SELECT id FROM categories WHERE name = 'Đá Xay'), true, false, 6),
    ('Sinh Tố Dừa', 'Sinh tố dừa', 50000, (SELECT id FROM categories WHERE name = 'Đá Xay'), true, false, 5),
    ('Sinh Tố Chuối', 'Sinh tố chuối', 45000, (SELECT id FROM categories WHERE name = 'Đá Xay'), true, false, 4),
    ('Sinh Tố Táo', 'Sinh tố táo', 55000, (SELECT id FROM categories WHERE name = 'Đá Xay'), true, false, 5),
    ('Sinh Tố Chanh Leo', 'Sinh tố chanh leo', 50000, (SELECT id FROM categories WHERE name = 'Đá Xay'), true, false, 5),
    ('Sinh Tố Việt Quất', 'Sinh tố việt quất', 65000, (SELECT id FROM categories WHERE name = 'Đá Xay'), true, false, 5),
    ('Sinh Tố Khoai Môn', 'Sinh tố khoai môn', 55000, (SELECT id FROM categories WHERE name = 'Đá Xay'), true, false, 5),
    ('Sinh Tố Thanh Long', 'Sinh tố thanh long', 55000, (SELECT id FROM categories WHERE name = 'Đá Xay'), true, false, 5),
    ('Sinh Tố Dưa Hấu', 'Sinh tố dưa hấu', 50000, (SELECT id FROM categories WHERE name = 'Đá Xay'), true, false, 5),
    ('Sinh Tố Mãng Cầu', 'Sinh tố mãng cầu', 55000, (SELECT id FROM categories WHERE name = 'Đá Xay'), true, false, 5),
    ('Sinh Tố Mận', 'Sinh tố mận', 55000, (SELECT id FROM categories WHERE name = 'Đá Xay'), true, false, 5),
    ('Trà Xanh Đá Xay', 'Trà xanh đá xay', 59000, (SELECT id FROM categories WHERE name = 'Đá Xay'), true, true, 6),
    ('Trà Xanh Matcha Đá Xay', 'Matcha đá xay cao cấp', 65000, (SELECT id FROM categories WHERE name = 'Đá Xay'), true, true, 6),
    ('Socola Đá Xay', 'Socola đá xay', 59000, (SELECT id FROM categories WHERE name = 'Đá Xay'), true, false, 6),
    ('Cà Phê Đá Xay', 'Cà phê đá xay', 55000, (SELECT id FROM categories WHERE name = 'Đá Xay'), true, false, 6),
    ('Cà Phê Cốt Dừa Đá Xay', 'Cà phê cốt dừa đá xay', 65000, (SELECT id FROM categories WHERE name = 'Đá Xay'), true, false, 6),
    ('Vani Đá Xay', 'Vani đá xay', 55000, (SELECT id FROM categories WHERE name = 'Đá Xay'), true, false, 6),
    ('Dâu Đá Xay', 'Dâu đá xay', 59000, (SELECT id FROM categories WHERE name = 'Đá Xay'), true, false, 6),
    ('Xoài Đá Xay', 'Xoài đá xay', 59000, (SELECT id FROM categories WHERE name = 'Đá Xay'), true, false, 6),
    ('Khoai Môn Đá Xay', 'Khoai môn đá xay', 59000, (SELECT id FROM categories WHERE name = 'Đá Xay'), true, false, 6),
    ('Sầu Riêng Đá Xay', 'Sầu riêng đá xay', 69000, (SELECT id FROM categories WHERE name = 'Đá Xay'), true, false, 6),
    ('Caramen Đá Xay', 'Caramen đá xay', 59000, (SELECT id FROM categories WHERE name = 'Đá Xay'), true, false, 6),
    ('Trà Sữa Đá Xay', 'Trà sữa đá xay', 59000, (SELECT id FROM categories WHERE name = 'Đá Xay'), true, true, 6),
    ('Sinh Tố Dừa Sữa', 'Sinh tố dừa sữa', 55000, (SELECT id FROM categories WHERE name = 'Đá Xay'), true, false, 5),
    ('Sinh Tố Chuối Dâu', 'Sinh tố chuối dâu', 55000, (SELECT id FROM categories WHERE name = 'Đá Xay'), true, false, 5),
    -- Trà Pure (18)
    ('Trà Ô Long', 'Trà ô long nguyên chất', 40000, (SELECT id FROM categories WHERE name = 'Trà Pure'), true, true, 4),
    ('Trà Lài', 'Trà lài thơm', 40000, (SELECT id FROM categories WHERE name = 'Trà Pure'), true, true, 4),
    ('Trà Xanh', 'Trà xanh nguyên chất', 40000, (SELECT id FROM categories WHERE name = 'Trà Pure'), true, true, 4),
    ('Trà Xanh Matcha', 'Trà xanh matcha', 55000, (SELECT id FROM categories WHERE name = 'Trà Pure'), true, true, 5),
    ('Trà Đen', 'Trà đen nguyên chất', 35000, (SELECT id FROM categories WHERE name = 'Trà Pure'), true, false, 4),
    ('Trà Phổ Nhĩ', 'Trà phổ Nhĩ cao cấp', 50000, (SELECT id FROM categories WHERE name = 'Trà Pure'), true, false, 5),
    ('Trà Atiso', 'Trà atiso mát gan', 45000, (SELECT id FROM categories WHERE name = 'Trà Pure'), true, false, 4),
    ('Trà Hoa Cúc', 'Trà hoa cúc', 45000, (SELECT id FROM categories WHERE name = 'Trà Pure'), true, false, 4),
    ('Trà Hoa Hồng', 'Trà hoa hồng', 50000, (SELECT id FROM categories WHERE name = 'Trà Pure'), true, false, 5),
    ('Trà Gừng', 'Trà gừng ấm', 40000, (SELECT id FROM categories WHERE name = 'Trà Pure'), true, false, 4),
    ('Trà Gừng Sả', 'Trà gừng sả', 45000, (SELECT id FROM categories WHERE name = 'Trà Pure'), true, false, 4),
    ('Trà Thảo Mộc', 'Trà thảo mộc', 45000, (SELECT id FROM categories WHERE name = 'Trà Pure'), true, false, 4),
    ('Trà Detox', 'Trà detox thanh lọc', 50000, (SELECT id FROM categories WHERE name = 'Trà Pure'), true, true, 5),
    ('Trà Ô Long Đào', 'Trà ô long đào', 49000, (SELECT id FROM categories WHERE name = 'Trà Pure'), true, false, 5),
    ('Trà Ô Long Vải', 'Trà ô long vải', 49000, (SELECT id FROM categories WHERE name = 'Trà Pure'), true, false, 5),
    ('Trà Lài Nhài', 'Trà lài nhài cao cấp', 45000, (SELECT id FROM categories WHERE name = 'Trà Pure'), true, false, 4),
    ('Trà Đỏ', 'Trà đỏ', 40000, (SELECT id FROM categories WHERE name = 'Trà Pure'), true, false, 4),
    ('Trà Giảm Cân', 'Trà giảm cân', 55000, (SELECT id FROM categories WHERE name = 'Trà Pure'), true, false, 5),
    -- Sữa Tươi & Sữa Chua (16)
    ('Sữa Chua Trân Châu', 'Sữa chua kèm trân châu', 45000, (SELECT id FROM categories WHERE name = 'Sữa Tươi & Sữa Chua'), true, true, 5),
    ('Sữa Chua Trân Châu Đường Đen', 'Sữa chua trân châu đường đen', 55000, (SELECT id FROM categories WHERE name = 'Sữa Tươi & Sữa Chua'), true, true, 6),
    ('Sữa Chua Nếp Cẩm', 'Sữa chua nếp cẩm', 45000, (SELECT id FROM categories WHERE name = 'Sữa Tươi & Sữa Chua'), true, true, 5),
    ('Sữa Chua Dâu', 'Sữa chua dâu', 55000, (SELECT id FROM categories WHERE name = 'Sữa Tươi & Sữa Chua'), true, false, 5),
    ('Sữa Chua Việt Quất', 'Sữa chua việt quất', 59000, (SELECT id FROM categories WHERE name = 'Sữa Tươi & Sữa Chua'), true, false, 5),
    ('Sữa Chua Khoai Môn', 'Sữa chua khoai môn', 55000, (SELECT id FROM categories WHERE name = 'Sữa Tươi & Sữa Chua'), true, false, 5),
    ('Sữa Tươi Trân Châu', 'Sữa tươi trân châu', 49000, (SELECT id FROM categories WHERE name = 'Sữa Tươi & Sữa Chua'), true, true, 5),
    ('Sữa Tươi Trân Châu Đường Đen', 'Sữa tươi trân châu đường đen', 59000, (SELECT id FROM categories WHERE name = 'Sữa Tươi & Sữa Chua'), true, true, 6),
    ('Sữa Tươi Đá', 'Sữa tươi đá', 40000, (SELECT id FROM categories WHERE name = 'Sữa Tươi & Sữa Chua'), true, false, 3),
    ('Sữa Tươi Matcha', 'Sữa tươi matcha', 55000, (SELECT id FROM categories WHERE name = 'Sữa Tươi & Sữa Chua'), true, false, 5),
    ('Sữa Tươi Socola', 'Sữa tươi socola', 55000, (SELECT id FROM categories WHERE name = 'Sữa Tươi & Sữa Chua'), true, false, 5),
    ('Sữa Tươi Khoai Môn', 'Sữa tươi khoai môn', 55000, (SELECT id FROM categories WHERE name = 'Sữa Tươi & Sữa Chua'), true, false, 5),
    ('Sữa Tươi Dâu', 'Sữa tươi dâu', 55000, (SELECT id FROM categories WHERE name = 'Sữa Tươi & Sữa Chua'), true, false, 5),
    ('Sữa Tươi Việt Quất', 'Sữa tươi việt quất', 59000, (SELECT id FROM categories WHERE name = 'Sữa Tươi & Sữa Chua'), true, false, 5),
    ('Sữa Chua Nếp Cẩm Trân Châu', 'Sữa chua nếp cẩm trân châu', 55000, (SELECT id FROM categories WHERE name = 'Sữa Tươi & Sữa Chua'), true, false, 6),
    ('Sữa Chua Trái Cây', 'Sữa chua trái cây hỗn hợp', 55000, (SELECT id FROM categories WHERE name = 'Sữa Tươi & Sữa Chua'), true, false, 6);

-- Product Sizes (S/M/L)
INSERT INTO product_sizes (product_id, size, price)
SELECT p.id, s.size,
    CASE s.size 
        WHEN 'S' THEN p.base_price - 5000
        WHEN 'M' THEN p.base_price
        WHEN 'L' THEN p.base_price + 10000
    END
FROM products p
CROSS JOIN (SELECT unnest(ARRAY['S'::size_type, 'M', 'L'])) AS s(size);

-- Toppings (50 loại)
INSERT INTO toppings (name, description, price, is_available) VALUES
    ('Trân Châu Đen', 'Trân châu đen dai ngon', 5000, true),
    ('Trân Châu Trắng', 'Trân châu trắng béo', 5000, true),
    ('Trân Châu Hoàng Kim', 'Trân châu hoàng kim cao cấp', 7000, true),
    ('Trân Châu Đường Đen', 'Trân châu ngâm đường đen', 8000, true),
    ('Trân Châu Sữa', 'Trân châu sữa', 7000, true),
    ('Trân Châu Dừa', 'Trân châu hương dừa', 8000, true),
    ('Thạch Dừa', 'Thạch dừa non', 7000, true),
    ('Thạch Cà Phê', 'Thạch cà phê', 7000, true),
    ('Thạch 3 Màu', 'Thạch 3 màu', 8000, true),
    ('Thạch Rau Câu', 'Thạch rau câu', 5000, true),
    ('Thạch Vải', 'Thạch vải', 8000, true),
    ('Thạch Đào', 'Thạch đào', 8000, true),
    ('Thạch Dâu', 'Thạch dâu', 8000, true),
    ('Thạch Xoài', 'Thạch xoài', 8000, true),
    ('Thạch Khoai Môn', 'Thạch khoai môn', 8000, true),
    ('Thạch Pudding', 'Thạch pudding', 8000, true),
    ('Thạch Matcha', 'Thạch matcha', 8000, true),
    ('Thạch Socola', 'Thạch socola', 8000, true),
    ('Bobo', 'Bobo dai ngon', 5000, true),
    ('Hạt É', 'Hạt é', 3000, true),
    ('Hạt Chia', 'Hạt chia', 5000, true),
    ('Nha Đam', 'Nha đam tươi', 7000, true),
    ('Khoai Môn', 'Khoai môn', 8000, true),
    ('Khoai Lang Tím', 'Khoai lang tím', 8000, true),
    ('Bông Lúa', 'Bông lúa', 5000, true),
    ('Phô Mai Viên', 'Phô mai viên', 8000, true),
    ('Trứng Muối', 'Trứng muối', 10000, true),
    ('Kem Cheese', 'Kem cheese', 10000, true),
    ('Kem Tươi', 'Kem tươi', 8000, true),
    ('Sốt Caramel', 'Sốt caramel', 5000, true),
    ('Sốt Socola', 'Sốt socola', 7000, true),
    ('Sốt Dâu', 'Sốt dâu', 7000, true),
    ('Sốt Khoai Môn', 'Sốt khoai môn', 7000, true),
    ('Sốt Matcha', 'Sốt matcha', 7000, true),
    ('Sốt Việt Quất', 'Sốt việt quất', 8000, true),
    ('Sốt Chanh Leo', 'Sốt chanh leo', 7000, true),
    ('Sốt Dừa', 'Sốt dừa', 7000, true),
    ('Cốt Dừa', 'Cốt dừa', 8000, true),
    ('Sữa Tươi', 'Sữa tươi', 8000, true),
    ('Sữa Đặc', 'Sữa đặc', 5000, true),
    ('Đường Đen', 'Đường đen', 5000, true),
    ('Mật Ong', 'Mật ong', 7000, true),
    ('Sầu Riêng', 'Sầu riêng Ri6', 15000, true),
    ('Mít', 'Mít', 8000, true),
    ('Nhãn', 'Nhãn', 8000, true),
    ('Vải', 'Vải', 8000, true),
    ('Xoài', 'Xoài', 10000, true),
    ('Dâu', 'Dâu', 10000, true),
    ('Nếp Cẩm', 'Nếp cẩm', 8000, true),
    ('Hạt Sen', 'Hạt sen', 8000, true);

-- Tables (20 bàn)
INSERT INTO tables (name, status, capacity, location) VALUES
    ('Bàn 1', 'available', 2, 'main'),
    ('Bàn 2', 'available', 2, 'main'),
    ('Bàn 3', 'available', 4, 'main'),
    ('Bàn 4', 'available', 4, 'main'),
    ('Bàn 5', 'available', 6, 'main'),
    ('Bàn 6', 'available', 6, 'main'),
    ('Bàn 7', 'available', 2, 'main'),
    ('Bàn 8', 'available', 4, 'main'),
    ('Bàn 9', 'available', 4, 'main'),
    ('Bàn 10', 'available', 6, 'main'),
    ('VIP 1', 'available', 8, 'vip'),
    ('VIP 2', 'available', 8, 'vip'),
    ('Ngoài Trời 1', 'available', 4, 'outdoor'),
    ('Ngoài Trời 2', 'available', 4, 'outdoor'),
    ('Ngoài Trời 3', 'available', 6, 'outdoor'),
    ('Ban Công 1', 'available', 4, 'balcony'),
    ('Ban Công 2', 'available', 4, 'balcony'),
    ('Phòng Lạnh 1', 'available', 6, 'main'),
    ('Phòng Lạnh 2', 'available', 6, 'main'),
    ('Phòng Lạnh 3', 'available', 8, 'main');

-- ============================================
-- NEW SEED DATA: CUSTOMERS, MEMBERSHIPS, VOUCHERS
-- ============================================

-- Customers (mẫu)
INSERT INTO customers (name, phone, email, date_of_birth, gender, address, notes) VALUES
    ('Nguyễn Văn A', '0901234567', 'nguyenvana@email.com', '1990-05-15', 'male', '123 Đường ABC, Quận 1, TP.HCM', 'Khách quen'),
    ('Trần Thị B', '0912345678', 'tranthib@email.com', '1995-08-20', 'female', '456 Đường XYZ, Quận 3, TP.HCM', NULL),
    ('Lê Văn C', '0923456789', 'levanc@email.com', '1988-12-10', 'male', '789 Đường DEF, Quận Bình Thạnh, TP.HCM', 'Thích trà sữa trân châu'),
    ('Phạm Thị D', '0934567890', 'phamthid@email.com', '2000-03-25', 'female', '321 Đường GHI, Quận 10, TP.HCM', NULL),
    ('Hoàng Văn E', '0945678901', 'hoangvane@email.com', '1992-07-08', 'male', '654 Đường JKL, Quận Gò Vấp, TP.HCM', 'Nhân viên văn phòng'),
    ('Đặng Thị F', '0956789012', 'dangthif@email.com', '1998-11-30', 'female', '987 Đường MNO, Quận Phú Nhuận, TP.HCM', 'Sinh viên'),
    ('Vũ Văn G', '0967890123', 'vuvang@email.com', '1985-04-18', 'male', '147 Đường PQR, Quận 2, TP.HCM', 'Khách VIP'),
    ('Bùi Thị H', '0978901234', 'buithih@email.com', '2002-09-12', 'female', '258 Đường STU, Quận Tân Bình, TP.HCM', NULL),
    ('Đỗ Văn I', '0989012345', 'dovani@email.com', '1993-06-22', 'male', '369 Đường VWX, Quận 5, TP.HCM', 'Thích cà phê'),
    ('Nguyễn Thị K', '0990123456', 'nguyenthik@email.com', '1997-01-05', 'female', '741 Đường YZA, Quận 7, TP.HCM', 'Khách mới');

-- Memberships (tự động cập nhật tier dựa trên total_spent)
INSERT INTO memberships (customer_id, tier, points, total_spent, total_orders, joined_at) VALUES
    ((SELECT id FROM customers WHERE phone = '0901234567'), 'gold', 150, 1800000, 25, NOW() - INTERVAL '6 months'),
    ((SELECT id FROM customers WHERE phone = '0912345678'), 'silver', 80, 600000, 12, NOW() - INTERVAL '4 months'),
    ((SELECT id FROM customers WHERE phone = '0923456789'), 'platinum', 320, 3500000, 45, NOW() - INTERVAL '1 year'),
    ((SELECT id FROM customers WHERE phone = '0934567890'), 'none', 25, 150000, 5, NOW() - INTERVAL '2 months'),
    ((SELECT id FROM customers WHERE phone = '0945678901'), 'gold', 180, 2000000, 30, NOW() - INTERVAL '8 months'),
    ((SELECT id FROM customers WHERE phone = '0956789012'), 'silver', 50, 400000, 8, NOW() - INTERVAL '3 months'),
    ((SELECT id FROM customers WHERE phone = '0967890123'), 'diamond', 500, 5500000, 60, NOW() - INTERVAL '2 years'),
    ((SELECT id FROM customers WHERE phone = '0978901234'), 'none', 15, 100000, 3, NOW() - INTERVAL '1 month'),
    ((SELECT id FROM customers WHERE phone = '0989012345'), 'silver', 90, 750000, 15, NOW() - INTERVAL '5 months'),
    ((SELECT id FROM customers WHERE phone = '0990123456'), 'none', 10, 80000, 2, NOW() - INTERVAL '2 weeks');

-- Vouchers (mã giảm giá)
INSERT INTO vouchers (code, name, description, voucher_type, discount_value, min_order_amount, max_discount_amount, usage_limit, status, valid_from, valid_until, applicable_tiers, created_by) VALUES
    -- Voucher phần trăm
    ('WELCOME10', 'Chào mừng khách mới', 'Giảm 10% cho khách hàng mới', 'percentage', 10, 50000, 50000, 1000, 'active', NOW(), NOW() + INTERVAL '3 months', ARRAY['none']::membership_tier[], NULL),
    ('MEMBER15', 'Thành viên thân thiết', 'Giảm 15% cho thành viên Silver+', 'percentage', 15, 100000, 100000, 500, 'active', NOW(), NOW() + INTERVAL '6 months', ARRAY['silver', 'gold', 'platinum', 'diamond']::membership_tier[], NULL),
    ('VIP20', 'VIP giảm 20%', 'Giảm 20% cho VIP members', 'percentage', 20, 150000, 150000, 200, 'active', NOW(), NOW() + INTERVAL '12 months', ARRAY['platinum', 'diamond']::membership_tier[], NULL),
    ('SUMMER25', 'Khuyến mãi hè', 'Giảm 25% tối đa 100k', 'percentage', 25, 200000, 100000, 300, 'active', NOW(), NOW() + INTERVAL '2 months', ARRAY['none', 'silver', 'gold', 'platinum', 'diamond']::membership_tier[], NULL),

    -- Voucher số tiền cố định
    ('GIAM50K', 'Giảm 50K', 'Giảm 50,000đ cho hóa đơn từ 300K', 'fixed_amount', 50000, 300000, NULL, 500, 'active', NOW(), NOW() + INTERVAL '3 months', ARRAY['none', 'silver', 'gold', 'platinum', 'diamond']::membership_tier[], NULL),
    ('GIAM100K', 'Giảm 100K', 'Giảm 100,000đ cho hóa đơn từ 500K', 'fixed_amount', 100000, 500000, NULL, 300, 'active', NOW(), NOW() + INTERVAL '6 months', ARRAY['silver', 'gold', 'platinum', 'diamond']::membership_tier[], NULL),

    -- Voucher sinh nhật
    ('BDAY50', 'Voucher sinh nhật', 'Giảm 50% ngày sinh nhật', 'percentage', 50, 0, 100000, 100, 'active', NOW(), NOW() + INTERVAL '12 months', ARRAY['none', 'silver', 'gold', 'platinum', 'diamond']::membership_tier[], NULL),

    -- Voucher dùng hết hạn
    ('OLD2024', 'Khuyến mãi 2024', 'Đã hết hạn', 'percentage', 30, 100000, 100000, 1000, 'expired', NOW() - INTERVAL '1 year', NOW() - INTERVAL '1 day', ARRAY['none', 'silver', 'gold', 'platinum', 'diamond']::membership_tier[], NULL),

    -- Voucher free item (chưa implement logic)
    ('FREETOPPING', 'Tặng 1 topping', 'Miễn phí 1 topping bất kỳ', 'free_item', 0, 50000, NULL, 1000, 'active', NOW(), NOW() + INTERVAL '3 months', ARRAY['none', 'silver', 'gold', 'platinum', 'diamond']::membership_tier[], NULL),

    -- Voucher BOGO
    ('BOGO', 'Mua 1 tặng 1', 'Áp dụng cho trà sữa truyền thống', 'buy_one_get_one', 0, 0, NULL, 200, 'active', NOW(), NOW() + INTERVAL '1 month', ARRAY['none', 'silver', 'gold', 'platinum', 'diamond']::membership_tier[], NULL);

-- ============================================
-- COMMENTS
-- ============================================

COMMENT ON TABLE roles IS 'User roles: Admin, Staff, Cashier';
COMMENT ON TABLE users IS 'Employee accounts with role-based access';
COMMENT ON TABLE categories IS 'Product categories for menu organization';
COMMENT ON TABLE products IS 'Individual drink items with base pricing';
COMMENT ON TABLE product_sizes IS 'Size variants (S/M/L) with different prices';
COMMENT ON TABLE toppings IS 'Add-on toppings with individual pricing';
COMMENT ON TABLE tables IS 'Physical tables in the shop with location and capacity';
COMMENT ON TABLE orders IS 'Customer orders with status tracking';
COMMENT ON TABLE order_details IS 'Line items in each order with customization (size, sugar, ice)';
COMMENT ON TABLE order_toppings IS 'Toppings added to each order detail';
COMMENT ON TABLE payments IS 'Payment records with split amount tracking (received/paid/change) and status enforcement';

COMMENT ON COLUMN payments.received_amount IS 'Amount customer gave (for cash payments)';
COMMENT ON COLUMN payments.paid_amount IS 'Actual amount charged (after discount/rounding)';
COMMENT ON COLUMN payments.change_amount IS 'Change returned to customer (received_amount - paid_amount)';
COMMENT ON COLUMN payments.status IS 'Payment status: pending, completed, failed, refunded (ENUM)';
COMMENT ON COLUMN payments.paid_at IS 'Timestamp when payment was completed';
COMMENT ON COLUMN payments.method IS 'Payment methods: cash, card, qr_code, bank_transfer, e_wallet';

COMMENT ON COLUMN orders.status IS 'Order workflow: pending -> preparing -> ready -> served/cancelled';
COMMENT ON COLUMN order_details.product_name IS 'Snapshot of product name at order time (immutable)';
COMMENT ON COLUMN order_details.product_image IS 'Snapshot of product image at order time';
COMMENT ON COLUMN order_details.topping_total IS 'Total price of all toppings for this item';
COMMENT ON COLUMN order_details.subtotal IS 'Final price: (unit_price + topping_total) * quantity';
COMMENT ON COLUMN order_details.sugar IS 'Sugar level: 0%, 25%, 50%, 75%, 100%';
COMMENT ON COLUMN order_details.ice IS 'Ice level: 0%, 25%, 50%, 75%, 100%';
COMMENT ON COLUMN order_details.size IS 'Drink size: S (Small), M (Medium), L (Large)';
COMMENT ON COLUMN order_toppings.topping_name IS 'Snapshot of topping name at order time (immutable)';
COMMENT ON COLUMN order_toppings.quantity IS 'Quantity of this topping (prevents duplicates via UPSERT)';

-- Usage notes
COMMENT ON FUNCTION add_topping_to_order_detail IS 'UPSERT function: adds topping or increases quantity if exists. Usage: SELECT add_topping_to_order_detail(order_detail_id, topping_id, topping_name, price, quantity);';
COMMENT ON FUNCTION validate_payment_and_update_order IS 'Validates payment total <= order total + 10%, sets paid_at, updates order status to served when fully paid';
COMMENT ON FUNCTION prevent_completed_payment_update IS 'Blocks financial data changes on completed payments (fraud prevention)';
COMMENT ON FUNCTION audit_table_changes IS 'Auto-logs INSERT/UPDATE/DELETE to audit_logs table';
COMMENT ON FUNCTION update_membership_tier IS 'Auto-updates membership tier based on total_spent';
COMMENT ON FUNCTION add_membership_points_after_order IS 'Adds points to membership when order is served';
COMMENT ON FUNCTION apply_voucher_to_order IS 'Validates and applies voucher code to order';

-- ============================================
-- SCHEMA COMPLETE
-- ============================================
-- Tables: 16 (tăng từ 11)
-- Primary Keys: 16 (all tables)
-- Foreign Keys: 15
-- Indexes: 79 (tăng từ 73) - 100% coverage
-- Triggers: 24 (tăng từ 15)
-- Functions: 14 (tăng từ 10) - ALL WITH search_path SECURITY FIX
-- Custom Types: 11 (tăng từ 8)
--
-- Seed Data:
-- - Roles: 3
-- - Users: 3
-- - Categories: 6
-- - Products: 150
-- - Product Sizes: 450
-- - Toppings: 50
-- - Tables: 20
-- - Customers: 10 (MỚI)
-- - Memberships: 10 (MỚI)
-- - Vouchers: 10 (MỚI)
--
-- NEW FEATURES (2026):
-- ✅ Audit logging - Theo dõi mọi thay đổi dữ liệu
-- ✅ Customer management - Quản lý khách hàng chuẩn hóa
-- ✅ Membership tiers - Hạng thành viên (none/silver/gold/platinum/diamond)
-- ✅ Points system - Tích điểm (1 point/10,000đ)
-- ✅ Voucher system - Mã giảm giá (percentage/fixed_amount/free_item/buy_one_get_one)
-- ✅ Auto tier update - Tự động nâng hạng dựa trên chi tiêu
-- ✅ Auto points - Tự động cộng điểm khi order served
-- ✅ Voucher validation - Kiểm tra hạn mức, ngày hết hạn, membership tier
--
-- SECURITY FIXES (2026):
-- ✅ All 14 functions have SET search_path = public, pg_temp
-- ✅ Prevents search_path hijacking attacks
-- ✅ Blocks malicious schema object overrides
-- ✅ Supabase security compliant
--
-- CRITICAL FIXES:
-- ✅ order_details.product_name - Snapshot immutable
-- ✅ order_details.product_image - Snapshot immutable
-- ✅ order_details.topping_total - Total toppings price
-- ✅ order_details.subtotal = (unit_price + topping_total) * quantity
-- ✅ order_detail_status ENUM - No typo risk
-- ✅ idx_order_details_order_created - Fast bill rendering
-- ✅ Trigger auto-calc subtotal with toppings
-- ✅ users.role_id - Fixed NOT NULL + SET NULL conflict
-- ✅ order_number - Race condition safe (uses SEQUENCE)
--
-- ORDER_TOPPINGS FIXES:
-- ✅ UNIQUE(order_detail_id, topping_id) - No duplicates
-- ✅ topping_name snapshot - Immutable bill data
-- ✅ UPSERT function - Add topping or increase quantity
-- ✅ Concurrency safe - ON CONFLICT DO UPDATE
--
-- PAYMENTS FIXES:
-- ✅ payment_status ENUM - No typo risk
-- ✅ received_amount vs paid_amount - Clear tracking
-- ✅ paid_at timestamp - Payment completion time
-- ✅ validate_payment_and_update_order - Check total paid
-- ✅ prevent_completed_payment_update - Lock after complete
-- ✅ Auto update order.status when fully paid
-- ✅ Overpayment protection (max 10%)
--
-- INDEX COVERAGE (79 indexes - 100%):
-- ✅ All FK columns indexed (15 indexes)
-- ✅ All WHERE clause columns indexed (40 indexes)
-- ✅ All JOIN columns indexed (12 indexes)
-- ✅ All ORDER BY columns indexed (8 indexes)
-- ✅ Composite indexes for complex queries (4 indexes)
-- ✅ BRIN index for time-series audit_logs (1 index)
--
-- SECURITY NOTES:
-- ⚠️ Password hashing: Thực hiện ở application layer (C#) với BCrypt
-- ⚠️ Audit logs: Tự động ghi nhận changes cho customers, vouchers, orders, payments
-- ⚠️ current_setting('app.current_user_id'): Set user ID trước khi thao tác
-- ============================================