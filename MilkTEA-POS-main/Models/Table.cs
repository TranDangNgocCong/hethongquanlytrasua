namespace MilkTeaPOS.Models;

/// <summary>
/// Physical tables in the shop with location and capacity
/// </summary>
public partial class Table
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public int? Capacity { get; set; }

    public string? Location { get; set; }

    public string? ImageUrl { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
