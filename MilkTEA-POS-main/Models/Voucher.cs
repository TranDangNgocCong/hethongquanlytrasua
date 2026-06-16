using System.ComponentModel.DataAnnotations.Schema;
namespace MilkTeaPOS.Models;

public partial class Voucher
{
    public Guid Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal DiscountValue { get; set; }

    public decimal? MinOrderAmount { get; set; }

    public decimal? MaxDiscountAmount { get; set; }

    public int? UsageLimit { get; set; }

    public int? UsageCount { get; set; }

    public DateTime? ValidFrom { get; set; }

    public DateTime? ValidUntil { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User? CreatedByNavigation { get; set; }


    [Column("voucher_type", TypeName = "voucher_type")]
    public string VoucherType { get; set; } = "percentage";

    // Chỉ định rõ kiểu dữ liệu Enum cho Postgres
    [Column("status", TypeName = "voucher_status")]
    public string status { get; set; } = "active";

}
