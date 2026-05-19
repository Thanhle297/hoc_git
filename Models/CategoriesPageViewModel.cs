namespace DATN.Models;

public class CategoriesPageViewModel
{
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public List<CategoryViewModel> Categories { get; set; } = new();
}
