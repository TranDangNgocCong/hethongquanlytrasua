using System.ComponentModel.DataAnnotations.Schema;

namespace MilkTeaPOS.Models;

/// <summary>
/// Employee accounts with role-based access
/// </summary>
public partial class User
{
    public Guid Id { get; set; }

    public string Username { get; set; } = null!;

    /// <summary>
    /// BCrypt hashed password (NOT plain text)
    /// </summary>
    public string PasswordHash { get; set; } = null!;

    /// <summary>
    /// Legacy plain text password - will be removed after migration
    /// </summary>
    public string Password { get; set; } = null!;

    public Guid? RoleId { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    [Column("avatar_url")]
    public string? AvatarUrl { get; set; }

    public virtual ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<Voucher> Vouchers { get; set; } = new List<Voucher>();
}
