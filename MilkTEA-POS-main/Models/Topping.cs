namespace MilkTeaPOS.Models;

/// <summary>
/// Add-on toppings with individual pricing
/// </summary>
public partial class Topping
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public string? ImageUrl { get; set; }

    public bool? IsAvailable { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<OrderTopping> OrderToppings { get; set; } = new List<OrderTopping>();
}
