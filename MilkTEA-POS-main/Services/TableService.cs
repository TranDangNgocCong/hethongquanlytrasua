using Microsoft.EntityFrameworkCore;
using MilkTeaPOS.Models;
using MilkTeaPOS.ViewModels;

namespace MilkTeaPOS.Services;

public class TableService
{
    /// <summary>
    /// Load all tables (read-only).
    /// Each call creates its own DbContext to avoid concurrency issues.
    /// </summary>
    public async Task<List<TableViewModel>> GetAllAsync()
    {
        using var context = new PostgresContext();

        var tables = await context.Tables
            .AsNoTracking()
            .OrderBy(t => t.Name)
            .ToListAsync();

        return tables.Select(t => new TableViewModel
        {
            Id = t.Id,
            Name = t.Name,
            Status = t.Status?.ToString(),
            Capacity = t.Capacity ?? 0,
            Location = t.Location,
            ImageUrl = t.ImageUrl,
            CreatedAt = t.CreatedAt,
            UpdatedAt = t.UpdatedAt
        }).ToList();
    }

    /// <summary>
    /// Search tables by keyword, status, and location (read-only).
    /// </summary>
    public async Task<List<TableViewModel>> SearchAsync(string? keyword, string? status, string? location)
    {
        using var context = new PostgresContext();

        var query = context.Tables
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            var lower = keyword.ToLower();
            query = query.Where(t => t.Name.ToLower().Contains(lower));
        }

        if (!string.IsNullOrWhiteSpace(location))
        {
            var lower = location.ToLower();
            query = query.Where(t => t.Location != null && t.Location.ToLower().Contains(lower));
        }

        var tables = await query
            .OrderBy(t => t.Name)
            .ToListAsync();

        // Filter by status client-side (ENUM can't be compared server-side)
        if (!string.IsNullOrWhiteSpace(status))
        {
            tables = tables.Where(t => status.Equals(t.Status, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        return tables.Select(t => new TableViewModel
        {
            Id = t.Id,
            Name = t.Name,
            Status = t.Status?.ToString(),
            Capacity = t.Capacity ?? 0,
            Location = t.Location,
            ImageUrl = t.ImageUrl,
            CreatedAt = t.CreatedAt,
            UpdatedAt = t.UpdatedAt
        }).ToList();
    }

    /// <summary>
    /// Add a new table.
    /// </summary>
    public async Task AddAsync(Table table)
    {
        using var context = new PostgresContext();

        table.Id = Guid.NewGuid();
        table.CreatedAt = DateTime.UtcNow;
        table.UpdatedAt = DateTime.UtcNow;

        // Use raw SQL to cast status to table_status enum
        await context.Database.ExecuteSqlRawAsync(
            "INSERT INTO tables (id, name, status, capacity, location, image_url, created_at, updated_at) VALUES ({0}, {1}, {2}::table_status, {3}, {4}, {5}, {6}, {7})",
            table.Id,
            table.Name,
            table.Status ?? "available",
            table.Capacity,
            (object?)table.Location ?? DBNull.Value,
            (object?)table.ImageUrl ?? DBNull.Value,
            table.CreatedAt,
            table.UpdatedAt
        );
    }

    /// <summary>
    /// Update an existing table by Id.
    /// </summary>
    public async Task<bool> UpdateAsync(Guid id, Action<Table> applyChanges)
    {
        using var context = new PostgresContext();

        var table = await context.Tables.FirstOrDefaultAsync(t => t.Id == id);
        if (table == null) return false;

        applyChanges(table);
        table.UpdatedAt = DateTime.UtcNow;

        // Use raw SQL to cast status to table_status enum
        await context.Database.ExecuteSqlRawAsync(
            "UPDATE tables SET name = {0}, status = {1}::table_status, capacity = {2}, location = {3}, image_url = {4}, updated_at = {5} WHERE id = {6}",
            table.Name,
            table.Status ?? "available",
            table.Capacity,
            (object?)table.Location ?? DBNull.Value,
            (object?)table.ImageUrl ?? DBNull.Value,
            table.UpdatedAt,
            table.Id
        );

        return true;
    }

    /// <summary>
    /// Check if a table has order references before deleting.
    /// Returns: (hasOrders, found)
    /// </summary>
    public async Task<(bool hasOrders, bool found)> CheckBeforeDeleteAsync(Guid id)
    {
        using var context = new PostgresContext();

        var exists = await context.Tables
            .AsNoTracking()
            .AnyAsync(t => t.Id == id);

        if (!exists) return (false, false);

        var hasOrders = await context.Orders
            .AsNoTracking()
            .AnyAsync(o => o.TableId == id);

        return (hasOrders, true);
    }

    /// <summary>
    /// Hard-delete a table (only when no order references).
    /// </summary>
    public async Task<bool> HardDeleteAsync(Guid id)
    {
        using var context = new PostgresContext();

        var table = await context.Tables.FirstOrDefaultAsync(t => t.Id == id);
        if (table == null) return false;

        context.Tables.Remove(table);
        await context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Check if a table name already exists (for duplicate validation).
    /// </summary>
    public async Task<bool> IsNameExistsAsync(string name, Guid? excludeId = null)
    {
        using var context = new PostgresContext();

        var lower = name.ToLower();
        var query = context.Tables
            .AsNoTracking()
            .Where(t => t.Name.ToLower() == lower);

        if (excludeId.HasValue)
        {
            query = query.Where(t => t.Id != excludeId.Value);
        }

        return await query.AnyAsync();
    }

    /// <summary>
    /// Get active orders (pending/preparing/ready) for a table.
    /// </summary>
    public async Task<List<(string OrderNumber, string Customer, int ItemCount, decimal Total)>> GetActiveOrdersAsync(Guid tableId)
    {
        using var context = new PostgresContext();

        var orders = await context.Orders
            .AsNoTracking()
            .Where(o => o.TableId == tableId &&
                        (o.Status == "pending" || o.Status == "preparing" || o.Status == "ready"))
            .Include(o => o.Customer)
            .ToListAsync();

        var result = new List<(string, string, int, decimal)>();

        foreach (var order in orders)
        {
            var itemCount = await context.OrderDetails
                .AsNoTracking()
                .CountAsync(od => od.OrderId == order.Id);

            result.Add((
                order.OrderNumber ?? "N/A",
                order.Customer?.Name ?? order.CustomerName ?? "Khách lẻ",
                itemCount,
                order.TotalAmount ?? 0
            ));
        }

        return result;
    }
}
