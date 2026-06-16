using Dapper;
using Microsoft.EntityFrameworkCore;
using MilkTeaPOS.Models;
using System.Data;

namespace MilkTeaPOS.Services;

/// <summary>
/// Service quản lý Product Sizes (S/M/L pricing)
/// </summary>
public class ProductSizeService
{
    /// <summary>
    /// Load tất cả sizes của 1 product
    /// Dùng Dapper để tránh lỗi EF Core enum mapping
    /// </summary>
    public async Task<Dictionary<string, decimal>> GetSizesForProductAsync(Guid productId)
    {
        using var context = new PostgresContext();
        var conn = context.Database.GetDbConnection();
        bool wasClosed = conn.State == ConnectionState.Closed;
        if (wasClosed) await conn.OpenAsync();

        var result = new Dictionary<string, decimal>
        {
            { "S", 0m },
            { "M", 0m },
            { "L", 0m }
        };

        try
        {
            var rows = await conn.QueryAsync<(string Size, decimal Price)>(
                "SELECT size::text, price FROM product_sizes WHERE product_id = @productId",
                new { productId });

            foreach (var (size, price) in rows)
            {
                if (result.ContainsKey(size))
                {
                    result[size] = price;
                }
            }
        }
        finally
        {
            if (wasClosed) await conn.CloseAsync();
        }

        return result;
    }

    /// <summary>
    /// Lưu/Update tất cả sizes cho 1 product (UPSERT)
    /// Dùng raw SQL để tránh lỗi so sánh enum với string trong EF Core
    /// </summary>
    public async Task SaveSizesForProductAsync(Guid productId, decimal priceS, decimal priceM, decimal priceL)
    {
        using var context = new PostgresContext();

        var sizeData = new[]
        {
            new { Size = "S", Price = priceS },
            new { Size = "M", Price = priceM },
            new { Size = "L", Price = priceL }
        };

        foreach (var sd in sizeData)
        {
            // UPSERT bằng raw SQL - tránh hoàn toàn vấn đề EF Core enum mapping
            var sql = @"
                INSERT INTO product_sizes (id, product_id, size, price, created_at, updated_at)
                VALUES (@id, @productId, @size::size_type, @price, @createdAt, @updatedAt)
                ON CONFLICT (product_id, size)
                DO UPDATE SET price = @price, updated_at = @updatedAt";

            await context.Database.ExecuteSqlRawAsync(sql,
                new Npgsql.NpgsqlParameter("@id", Guid.NewGuid()),
                new Npgsql.NpgsqlParameter("@productId", productId),
                new Npgsql.NpgsqlParameter("@size", sd.Size),
                new Npgsql.NpgsqlParameter("@price", sd.Price),
                new Npgsql.NpgsqlParameter("@createdAt", DateTime.UtcNow),
                new Npgsql.NpgsqlParameter("@updatedAt", DateTime.UtcNow));
        }
    }

    /// <summary>
    /// Xóa tất cả sizes của 1 product
    /// </summary>
    public async Task DeleteSizesForProductAsync(Guid productId)
    {
        using var context = new PostgresContext();

        var sizes = await context.ProductSizes
            .Where(ps => ps.ProductId == productId)
            .ToListAsync();

        if (sizes.Count > 0)
        {
            context.ProductSizes.RemoveRange(sizes);
            await context.SaveChangesAsync();
        }
    }
}
