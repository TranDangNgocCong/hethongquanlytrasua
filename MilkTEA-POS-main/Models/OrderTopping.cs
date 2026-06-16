namespace MilkTeaPOS.Models;

/// <summary>
/// Toppings added to each order detail
/// </summary>
public partial class OrderTopping
{
    public Guid Id { get; set; }

    public Guid OrderDetailId { get; set; }

    public Guid ToppingId { get; set; }

    /// <summary>
    /// Snapshot of topping name at order time (immutable)
    /// </summary>
    public string ToppingName { get; set; } = null!;

    /// <summary>
    /// Quantity of this topping (prevents duplicates via UPSERT)
    /// </summary>
    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual OrderDetail OrderDetail { get; set; } = null!;

    public virtual Topping Topping { get; set; } = null!;
}
