using Microsoft.EntityFrameworkCore;
using MilkTeaPOS.Models;
using MilkTeaPOS.ViewModels;

namespace MilkTeaPOS.Services;

public class ProductService
{
    /// <summary>
    /// Load all products with category info (read-only).
    /// Each call creates its own DbContext to avoid concurrency issues.
    /// </summary>
    public async Task<List<ProductViewModel>> GetAllAsync()
    {
        using var context = new PostgresContext();

        return await context.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .OrderBy(p => p.Category.Name)
            .ThenBy(p => p.Name)
            .Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                CategoryId = p.CategoryId,
                CategoryName = p.Category.Name,
                BasePrice = p.BasePrice,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                IsAvailable = p.IsAvailable ?? true,
                IsFeatured = p.IsFeatured ?? false,
                PreparationTime = p.PreparationTime,
                CreatedAt = p.CreatedAt
            })
            .ToListAsync();
    }

    /// <summary>
    /// Search products by keyword and/or category filter (read-only).
    /// </summary>
    public async Task<List<ProductViewModel>> SearchAsync(string? keyword, Guid? categoryId)
    {
        using var context = new PostgresContext();

        return await SearchInternalAsync(context, keyword, categoryId, null);
    }

    /// <summary>
    /// Search products with optional status filter (available/unavailable).
    /// </summary>
    public async Task<List<ProductViewModel>> SearchWithStatusAsync(string? keyword, Guid? categoryId, bool? isAvailable)
    {
        using var context = new PostgresContext();

        return await SearchInternalAsync(context, keyword, categoryId, isAvailable);
    }

    private static async Task<List<ProductViewModel>> SearchInternalAsync(
        PostgresContext context, string? keyword, Guid? categoryId, bool? isAvailable)
    {
        var query = context.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            var lower = keyword.ToLower();
            query = query.Where(p =>
                p.Name.ToLower().Contains(lower) ||
                (p.Description != null && p.Description.ToLower().Contains(lower)));
        }

        if (categoryId.HasValue)
        {
            query = query.Where(p => p.CategoryId == categoryId.Value);
        }

        if (isAvailable.HasValue)
        {
            query = query.Where(p => p.IsAvailable == isAvailable.Value);
        }

        return await query
            .OrderBy(p => p.Category.Name)
            .ThenBy(p => p.Name)
            .Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                CategoryId = p.CategoryId,
                CategoryName = p.Category.Name,
                BasePrice = p.BasePrice,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                IsAvailable = p.IsAvailable ?? true,
                IsFeatured = p.IsFeatured ?? false,
                PreparationTime = p.PreparationTime,
                CreatedAt = p.CreatedAt
            })
            .ToListAsync();
    }

    /// <summary>
    /// Get active categories for ComboBox binding (read-only).
    /// </summary>
    public async Task<List<CategoryViewModel>> GetCategoriesAsync()
    {
        using var context = new PostgresContext();

        return await context.Categories
            .AsNoTracking()
            .Where(c => c.IsActive == true)
            .OrderBy(c => c.DisplayOrder)
            .ThenBy(c => c.Name)
            .Select(c => new CategoryViewModel
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToListAsync();
    }

    /// <summary>
    /// Add a new product.
    /// </summary>
    public async Task AddAsync(Product product)
    {
        using var context = new PostgresContext();

        product.Id = Guid.NewGuid();
        product.CreatedAt = DateTime.UtcNow;
        product.UpdatedAt = DateTime.UtcNow;

        context.Products.Add(product);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Update an existing product by Id.
    /// </summary>
    public async Task<bool> UpdateAsync(Guid id, Action<Product> applyChanges)
    {
        using var context = new PostgresContext();

        var product = await context.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product == null) return false;

        applyChanges(product);
        product.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Delete or soft-delete a product.
    /// Returns: "deleted" | "soft_deleted" | "not_found" | "cancelled"
    /// If the product has order references, it cannot be hard-deleted.
    /// </summary>
    public async Task<(bool hasOrders, bool found)> CheckBeforeDeleteAsync(Guid id)
    {
        using var context = new PostgresContext();

        var exists = await context.Products
            .AsNoTracking()
            .AnyAsync(p => p.Id == id);

        if (!exists) return (false, false);

        var hasOrders = await context.OrderDetails
            .AsNoTracking()
            .AnyAsync(od => od.ProductId == id);

        return (hasOrders, true);
    }

    /// <summary>
    /// Soft-delete: set IsAvailable = false.
    /// </summary>
    public async Task<bool> SoftDeleteAsync(Guid id)
    {
        using var context = new PostgresContext();

        var product = await context.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product == null) return false;

        product.IsAvailable = false;
        product.UpdatedAt = DateTime.UtcNow;
        await context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Hard-delete a product (only when no order references).
    /// </summary>
    public async Task<bool> HardDeleteAsync(Guid id)
    {
        using var context = new PostgresContext();

        var product = await context.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product == null) return false;

        context.Products.Remove(product);
        await context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Check if a product name already exists within the same category (for duplicate validation).
    /// </summary>
    public async Task<bool> IsNameExistsAsync(string name, Guid? categoryId, Guid? excludeId = null)
    {
        using var context = new PostgresContext();

        var lower = name.ToLower();
        var query = context.Products
            .AsNoTracking()
            .Where(p => p.Name.ToLower() == lower);

        if (categoryId.HasValue)
        {
            query = query.Where(p => p.CategoryId == categoryId.Value);
        }

        if (excludeId.HasValue)
        {
            query = query.Where(p => p.Id != excludeId.Value);
        }

        return await query.AnyAsync();
    }
}
