using Microsoft.EntityFrameworkCore;
using MilkTeaPOS.Models;
using MilkTeaPOS.ViewModels;

namespace MilkTeaPOS.Services;

public class ToppingService
{
    /// <summary>
    /// Load all toppings (read-only).
    /// Each call creates its own DbContext to avoid concurrency issues.
    /// </summary>
    public async Task<List<ToppingViewModel>> GetAllAsync()
    {
        using var context = new PostgresContext();

        return await context.Toppings
            .AsNoTracking()
            .OrderBy(t => t.Name)
            .Select(t => new ToppingViewModel
            {
                Id = t.Id,
                Name = t.Name,
                Description = t.Description,
                Price = t.Price,
                IsAvailable = t.IsAvailable ?? true,
                ImageUrl = t.ImageUrl,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt
            })
            .ToListAsync();
    }

    /// <summary>
    /// Search toppings by keyword and status (read-only).
    /// </summary>
    public async Task<List<ToppingViewModel>> SearchAsync(string? keyword, bool? isAvailable = null)
    {
        using var context = new PostgresContext();

        var query = context.Toppings
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            var lower = keyword.ToLower();
            query = query.Where(t =>
                t.Name.ToLower().Contains(lower) ||
                (t.Description != null && t.Description.ToLower().Contains(lower)));
        }

        if (isAvailable.HasValue)
        {
            query = query.Where(t => t.IsAvailable == isAvailable.Value);
        }

        return await query
            .OrderBy(t => t.Name)
            .Select(t => new ToppingViewModel
            {
                Id = t.Id,
                Name = t.Name,
                Description = t.Description,
                Price = t.Price,
                IsAvailable = t.IsAvailable ?? true,
                ImageUrl = t.ImageUrl,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt
            })
            .ToListAsync();
    }

    /// <summary>
    /// Get usage count (số lần xuất hiện trong order_toppings) cho tất cả toppings.
    /// </summary>
    public async Task<Dictionary<Guid, int>> GetUsageCountsAsync()
    {
        using var context = new PostgresContext();

        var counts = await context.OrderToppings
            .AsNoTracking()
            .GroupBy(ot => ot.ToppingId)
            .Select(g => new { ToppingId = g.Key, Count = g.Count() })
            .ToListAsync();

        return counts.ToDictionary(x => x.ToppingId, x => x.Count);
    }

    /// <summary>
    /// Add a new topping.
    /// </summary>
    public async Task AddAsync(Topping topping)
    {
        using var context = new PostgresContext();

        topping.Id = Guid.NewGuid();
        topping.CreatedAt = DateTime.UtcNow;
        topping.UpdatedAt = DateTime.UtcNow;

        context.Toppings.Add(topping);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Update an existing topping by Id.
    /// </summary>
    public async Task<bool> UpdateAsync(Guid id, Action<Topping> applyChanges)
    {
        using var context = new PostgresContext();

        var topping = await context.Toppings.FirstOrDefaultAsync(t => t.Id == id);
        if (topping == null) return false;

        applyChanges(topping);
        topping.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Check if a topping has order references before deleting.
    /// Returns: (hasOrders, found)
    /// </summary>
    public async Task<(bool hasOrders, bool found)> CheckBeforeDeleteAsync(Guid id)
    {
        using var context = new PostgresContext();

        var exists = await context.Toppings
            .AsNoTracking()
            .AnyAsync(t => t.Id == id);

        if (!exists) return (false, false);

        var hasOrders = await context.OrderToppings
            .AsNoTracking()
            .AnyAsync(ot => ot.ToppingId == id);

        return (hasOrders, true);
    }

    /// <summary>
    /// Soft-delete: set IsAvailable = false.
    /// </summary>
    public async Task<bool> SoftDeleteAsync(Guid id)
    {
        using var context = new PostgresContext();

        var topping = await context.Toppings.FirstOrDefaultAsync(t => t.Id == id);
        if (topping == null) return false;

        topping.IsAvailable = false;
        topping.UpdatedAt = DateTime.UtcNow;
        await context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Hard-delete a topping (only when no order references).
    /// </summary>
    public async Task<bool> HardDeleteAsync(Guid id)
    {
        using var context = new PostgresContext();

        var topping = await context.Toppings.FirstOrDefaultAsync(t => t.Id == id);
        if (topping == null) return false;

        context.Toppings.Remove(topping);
        await context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Check if a topping name already exists (for duplicate validation).
    /// </summary>
    public async Task<bool> IsNameExistsAsync(string name, Guid? excludeId = null)
    {
        using var context = new PostgresContext();

        var lower = name.ToLower();
        var query = context.Toppings
            .AsNoTracking()
            .Where(t => t.Name.ToLower() == lower);

        if (excludeId.HasValue)
        {
            query = query.Where(t => t.Id != excludeId.Value);
        }

        return await query.AnyAsync();
    }
}
