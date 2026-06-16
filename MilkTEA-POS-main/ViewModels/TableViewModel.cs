namespace MilkTeaPOS.ViewModels;

public class TableViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Status { get; set; }
    public int Capacity { get; set; }
    public string? Location { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
