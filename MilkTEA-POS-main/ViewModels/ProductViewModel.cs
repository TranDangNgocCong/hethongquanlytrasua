namespace MilkTeaPOS.ViewModels;

public class ProductViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public decimal BasePrice { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsAvailable { get; set; }
    public bool IsFeatured { get; set; }
    public int? PreparationTime { get; set; }
    public DateTime? CreatedAt { get; set; }
}
