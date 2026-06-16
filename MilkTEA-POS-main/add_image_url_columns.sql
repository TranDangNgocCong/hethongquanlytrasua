-- Add image_url column to toppings table
ALTER TABLE toppings 
ADD COLUMN IF NOT EXISTS image_url TEXT;

-- Add image_url column to tables table  
ALTER TABLE tables 
ADD COLUMN IF NOT EXISTS image_url TEXT;

-- Update EF Core model configuration required
-- After running this script, you need to add ImageUrl mapping to PostgresContext.cs
-- for both Table and Topping entities
