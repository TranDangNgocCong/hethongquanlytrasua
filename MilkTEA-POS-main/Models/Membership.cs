namespace MilkTeaPOS.Models;

public partial class Membership
{
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    public int? Points { get; set; }

    public decimal? TotalSpent { get; set; }

    public int? TotalOrders { get; set; }

    public DateTime? JoinedAt { get; set; }

    public DateTime? LastOrderAt { get; set; }

    public string? Tier { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Customer Customer { get; set; } = null!;
}
