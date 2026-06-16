-- ============================================
-- Sync columns between local SQL and Supabase
-- Run this in Supabase SQL Editor
-- ============================================

-- 1. Add avatar_url to customers
ALTER TABLE customers
ADD COLUMN IF NOT EXISTS avatar_url TEXT;

-- 2. Add image_url to tables
ALTER TABLE tables
ADD COLUMN IF NOT EXISTS image_url TEXT;

-- 3. Add image_url to toppings
ALTER TABLE toppings
ADD COLUMN IF NOT EXISTS image_url TEXT;

-- 4. Add avatar_url and password_hash to users
ALTER TABLE users
ADD COLUMN IF NOT EXISTS avatar_url TEXT,
ADD COLUMN IF NOT EXISTS password_hash VARCHAR(255);

-- 5. Make password nullable (was NOT NULL)
ALTER TABLE users
ALTER COLUMN password DROP NOT NULL;

-- 6. Backfill password_hash from password (nếu có data cũ)
-- Copy password sang password_hash cho các user chưa có hash
UPDATE users
SET password_hash = password
WHERE password_hash IS NULL AND password IS NOT NULL;

-- 7. Verify columns
SELECT table_name, column_name, data_type, is_nullable
FROM information_schema.columns
WHERE table_name IN ('customers', 'tables', 'toppings', 'users')
  AND column_name IN ('avatar_url', 'image_url', 'password_hash', 'password')
ORDER BY table_name, column_name;
