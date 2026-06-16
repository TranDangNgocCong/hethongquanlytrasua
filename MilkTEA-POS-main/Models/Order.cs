namespace MilkTeaPOS.Models;

/// <summary>
/// Customer orders with status tracking
/// </summary>
public partial class Order
{
    public Guid Id { get; set; }

    public string? OrderNumber { get; set; }

    public Guid? UserId { get; set; }

    public Guid? TableId { get; set; }

    public Guid? CustomerId { get; set; }

    public decimal? Subtotal { get; set; }

    public decimal? Discount { get; set; }

    public decimal? VoucherDiscount { get; set; }

    public decimal? MembershipDiscount { get; set; }

    public decimal? TotalAmount { get; set; }

    public string? Notes { get; set; }

    public string? CustomerName { get; set; }

    public string? CustomerPhone { get; set; }

    public bool? IsDelivery { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? ServedAt { get; set; }

    public DateTime? CancelledAt { get; set; }

    public string? Status { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Table? Table { get; set; }

    public virtual User? User { get; set; }
}
