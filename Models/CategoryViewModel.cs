namespace DATN.Models;

public class CategoryViewModel
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string ImageUrl { get; set; } = string.Empty;

    public int ProductCount { get; set; }

    public string FilterValue { get; set; } = string.Empty;
}
