using System.ComponentModel.DataAnnotations.Schema;

namespace MilkTeaPOS.Models;

/// <summary>
/// Size variants (S/M/L) with different prices
/// </summary>
public partial class ProductSize
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }

    /// <summary>
    /// Size type: S, M, L (matches PostgreSQL ENUM size_type)
    /// </summary>
    [Column("size", TypeName = "size_type")]
    public string Size { get; set; } = "M";

    [Column("price")]
    public decimal Price { get; set; }

    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    public virtual Product Product { get; set; } = null!;
}
