namespace DATN.Models;

public class ProductListPageViewModel
{
    public const int DefaultPageSize = 6;

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string SelectedSort { get; set; } = string.Empty;

    public List<string> Genders { get; set; } = new();

    public List<string> Categories { get; set; } = new();

    public List<string> Sizes { get; set; } = new();

    public List<string> Colors { get; set; } = new();

    public List<string> SelectedGenders { get; set; } = new();

    public List<string> SelectedCategories { get; set; } = new();

    public List<string> SelectedSizes { get; set; } = new();

    public List<string> SelectedColors { get; set; } = new();

    public decimal MinPrice { get; set; }

    public decimal MaxPrice { get; set; }

    public decimal? AppliedMinPrice { get; set; }

    public decimal? AppliedMaxPrice { get; set; }

    public int CurrentPage { get; set; }

    public int TotalPages { get; set; }

    public int TotalResults { get; set; }

    public List<ProductViewModel> Products { get; set; } = new();
}
