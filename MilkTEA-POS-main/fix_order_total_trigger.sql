-- Fix: Trigger calculate_order_total() phải bao gồm voucher_discount và membership_discount
-- Vấn đề: Trigger cũ chỉ tính total_amount = subtotal - discount, 
--         nhưng thực tế total_amount = subtotal - discount - voucher_discount - membership_discount

CREATE OR REPLACE FUNCTION calculate_order_total()
RETURNS TRIGGER AS $$
BEGIN
    -- Recalculate subtotal from order_details
    UPDATE orders o
    SET
        subtotal = COALESCE((
            SELECT SUM(od.subtotal)
            FROM order_details od
            WHERE od.order_id = COALESCE(NEW.order_id, OLD.order_id)
        ), 0),
        updated_at = NOW()
    WHERE o.id = COALESCE(NEW.order_id, OLD.order_id);

    -- Update total_amount with ALL discounts (voucher + membership + product-level)
    UPDATE orders o
    SET
        total_amount = GREATEST(0, o.subtotal - COALESCE(o.discount, 0) - COALESCE(o.voucher_discount, 0) - COALESCE(o.membership_discount, 0)),
        updated_at = NOW()
    WHERE o.id = COALESCE(NEW.order_id, OLD.order_id);

    RETURN COALESCE(NEW, OLD);
END;
$$ LANGUAGE plpgsql
SET search_path = public, pg_temp;

-- Same fix for update_order_total_on_detail_change
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

    -- Update total_amount with ALL discounts
    UPDATE orders o
    SET
        total_amount = GREATEST(0, o.subtotal - COALESCE(o.discount, 0) - COALESCE(o.voucher_discount, 0) - COALESCE(o.membership_discount, 0)),
        updated_at = NOW()
    WHERE o.id = COALESCE(NEW.order_id, OLD.order_id);

    RETURN COALESCE(NEW, OLD);
END;
$$ LANGUAGE plpgsql
SET search_path = public, pg_temp;
