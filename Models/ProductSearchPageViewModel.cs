namespace DATN.Models;

public class ProductSearchPageViewModel
{
    public string Query { get; set; } = string.Empty;

    public List<ProductViewModel> Results { get; set; } = new();
}
