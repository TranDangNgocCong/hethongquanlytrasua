-- ============================================
-- ADD MISSING COLUMNS TO vouchers TABLE
-- Run this in Supabase SQL Editor
-- ============================================

-- Add applicable_tiers column if not exists
ALTER TABLE vouchers
ADD COLUMN IF NOT EXISTS applicable_tiers membership_tier[]
    DEFAULT ARRAY['none', 'silver', 'gold', 'platinum', 'diamond']::membership_tier[];

-- Verify the column was added
SELECT column_name, data_type, column_default
FROM information_schema.columns
WHERE table_name = 'vouchers'
  AND column_name = 'applicable_tiers';
