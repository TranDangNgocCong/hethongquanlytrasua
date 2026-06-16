namespace MilkTeaPOS.Models;

/// <summary>
/// Individual drink items with base pricing
/// </summary>
public partial class Product
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal BasePrice { get; set; }

    public Guid CategoryId { get; set; }

    public string? ImageUrl { get; set; }

    public bool? IsAvailable { get; set; }

    public bool? IsFeatured { get; set; }

    public int? PreparationTime { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<ProductSize> ProductSizes { get; set; } = new List<ProductSize>();
}
