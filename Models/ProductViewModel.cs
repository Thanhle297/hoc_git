namespace DATN.Models;

public class ProductViewModel
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public string Gender { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public decimal? OriginalPrice { get; set; }

    public string ShortDescription { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string ImageUrl { get; set; } = string.Empty;

    public List<string> GalleryImages { get; set; } = new();

    public List<string> Sizes { get; set; } = new();

    public List<string> Colors { get; set; } = new();

    public string StockLabel { get; set; } = string.Empty;

    public bool IsNew { get; set; }

    public bool IsBestSeller { get; set; }

    public double Rating { get; set; }

    public int ReviewCount { get; set; }
}
