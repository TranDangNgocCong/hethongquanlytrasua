namespace MilkTeaPOS.Models;

/// <summary>
/// Payment records with split amount tracking (received/paid/change) and status enforcement
/// </summary>
public partial class Payment
{
    public Guid Id { get; set; }

    public Guid OrderId { get; set; }

    /// <summary>
    /// Amount customer gave (for cash payments)
    /// </summary>
    public decimal ReceivedAmount { get; set; }

    /// <summary>
    /// Actual amount charged (after discount/rounding)
    /// </summary>
    public decimal PaidAmount { get; set; }

    /// <summary>
    /// Change returned to customer (received_amount - paid_amount)
    /// </summary>
    public decimal? ChangeAmount { get; set; }

    public string? TransactionId { get; set; }

    public DateTime? CreatedAt { get; set; }

    /// <summary>
    /// Timestamp when payment was completed
    /// </summary>
    public DateTime? PaidAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? PaymentInfo { get; set; }

    public string? Notes { get; set; }

    public string? Status { get; set; }

    public string? Method { get; set; }

    public virtual Order Order { get; set; } = null!;
}
