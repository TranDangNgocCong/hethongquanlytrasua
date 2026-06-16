namespace MilkTeaPOS.Models;

/// <summary>
/// Line items in each order with customization (size, sugar, ice)
/// </summary>
public partial class OrderDetail
{
    public Guid Id { get; set; }

    public Guid OrderId { get; set; }

    public Guid ProductId { get; set; }

    /// <summary>
    /// Snapshot of product name at order time (immutable)
    /// </summary>
    public string ProductName { get; set; } = null!;

    /// <summary>
    /// Snapshot of product image at order time
    /// </summary>
    public string? ProductImage { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Total price of all toppings for this item
    /// </summary>
    public decimal? ToppingTotal { get; set; }

    /// <summary>
    /// Final price: (unit_price + topping_total) * quantity
    /// </summary>
    public decimal Subtotal { get; set; }

    public string? Notes { get; set; }

    public string? SpecialInstructions { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual ICollection<OrderTopping> OrderToppings { get; set; } = new List<OrderTopping>();

    public virtual Product Product { get; set; } = null!;
}
