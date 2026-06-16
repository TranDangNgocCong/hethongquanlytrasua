using System.ComponentModel.DataAnnotations.Schema;

namespace MilkTeaPOS.Models;

public partial class Customer
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public string? Address { get; set; }

    public string? Notes { get; set; }

    [Column("avatar_url")]
    public string? AvatarUrl { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Membership? Membership { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
