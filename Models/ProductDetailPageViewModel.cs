namespace DATN.Models;

public class ProductDetailPageViewModel
{
    public ProductViewModel Product { get; set; } = new();

    public List<ProductViewModel> RelatedProducts { get; set; } = new();

    public List<string> FeatureHighlights { get; set; } = new();
}
