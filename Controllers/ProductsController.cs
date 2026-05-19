using DATN.Models;
using Microsoft.AspNetCore.Mvc;

namespace DATN.Controllers;

public class ProductsController : Controller
{
    public IActionResult Index(
        string? sort = null,
        string[]? genders = null,
        string[]? categories = null,
        string[]? sizes = null,
        string[]? colors = null,
        decimal? minPrice = null,
        decimal? maxPrice = null,
        int page = 1)
    {
        var products = GetProducts();
        var availableMinPrice = products.Min(product => product.Price);
        var availableMaxPrice = products.Max(product => product.Price);
        var selectedSort = string.IsNullOrWhiteSpace(sort) ? "Mới nhất" : sort;
        var selectedGenders = NormalizeSelections(genders);
        var selectedCategories = NormalizeSelections(categories);
        var selectedSizes = NormalizeSelections(sizes);
        var selectedColors = NormalizeSelections(colors);
        var appliedMinPrice = minPrice.HasValue
            ? Math.Clamp(minPrice.Value, availableMinPrice, availableMaxPrice)
            : availableMinPrice;
        var appliedMaxPrice = maxPrice.HasValue
            ? Math.Clamp(maxPrice.Value, availableMinPrice, availableMaxPrice)
            : availableMaxPrice;

        if (appliedMinPrice > appliedMaxPrice)
        {
            (appliedMinPrice, appliedMaxPrice) = (appliedMaxPrice, appliedMinPrice);
        }

        var filteredProducts = products.AsEnumerable();

        if (selectedGenders.Count > 0)
        {
            filteredProducts = filteredProducts.Where(product => selectedGenders.Contains(product.Gender, StringComparer.OrdinalIgnoreCase));
        }

        if (selectedCategories.Count > 0)
        {
            filteredProducts = filteredProducts.Where(product => selectedCategories.Contains(product.Category, StringComparer.OrdinalIgnoreCase));
        }

        if (selectedSizes.Count > 0)
        {
            filteredProducts = filteredProducts.Where(product => product.Sizes.Any(size => selectedSizes.Contains(size, StringComparer.OrdinalIgnoreCase)));
        }

        if (selectedColors.Count > 0)
        {
            filteredProducts = filteredProducts.Where(product => product.Colors.Any(color => selectedColors.Contains(color, StringComparer.OrdinalIgnoreCase)));
        }

        filteredProducts = filteredProducts.Where(product => product.Price >= appliedMinPrice && product.Price <= appliedMaxPrice);

        var orderedProducts = selectedSort switch
        {
            "Bán chạy" => filteredProducts.OrderByDescending(product => product.IsBestSeller).ThenBy(product => product.Name).ToList(),
            "Giá tăng dần" => filteredProducts.OrderBy(product => product.Price).ToList(),
            "Giá giảm dần" => filteredProducts.OrderByDescending(product => product.Price).ToList(),
            _ => filteredProducts.OrderByDescending(product => product.IsNew).ThenBy(product => product.Name).ToList()
        };

        var totalResults = orderedProducts.Count;
        var totalPages = Math.Max(1, (int)Math.Ceiling(totalResults / (double)ProductListPageViewModel.DefaultPageSize));
        var currentPage = Math.Clamp(page, 1, totalPages);
        var pagedProducts = orderedProducts
            .Skip((currentPage - 1) * ProductListPageViewModel.DefaultPageSize)
            .Take(ProductListPageViewModel.DefaultPageSize)
            .ToList();

        var availableSizes = products.SelectMany(product => product.Sizes).Distinct(StringComparer.OrdinalIgnoreCase).OrderBy(size => size).ToList();
        var availableColors = products.SelectMany(product => product.Colors).Distinct(StringComparer.OrdinalIgnoreCase).OrderBy(color => color).ToList();

        var model = new ProductListPageViewModel
        {
            Title = "Danh sách sản phẩm",
            Description = "Khám phá các thiết kế thời trang và giày dép nổi bật dành cho bản demo mua sắm.",
            SelectedSort = selectedSort,
            Genders = new List<string> { "Nam", "Nữ", "Unisex" },
            Categories = new List<string> { "Áo", "Quần", "Giày", "Phụ kiện" },
            Sizes = availableSizes,
            Colors = availableColors,
            SelectedGenders = selectedGenders,
            SelectedCategories = selectedCategories,
            SelectedSizes = selectedSizes,
            SelectedColors = selectedColors,
            MinPrice = availableMinPrice,
            MaxPrice = availableMaxPrice,
            AppliedMinPrice = appliedMinPrice,
            AppliedMaxPrice = appliedMaxPrice,
            CurrentPage = currentPage,
            TotalPages = totalPages,
            TotalResults = totalResults,
            Products = pagedProducts
        };

        return View(model);
    }

    public IActionResult Detail(int id)
    {
        var products = GetProducts();
        var product = products.FirstOrDefault(item => item.Id == id);

        if (product is null)
        {
            return NotFound();
        }

        var model = new ProductDetailPageViewModel
        {
            Product = product,
            RelatedProducts = products.Where(item => item.Id != id && item.Category == product.Category).Take(3).ToList(),
            FeatureHighlights = new List<string>
            {
                "Chất liệu chọn lọc, phù hợp mặc hằng ngày và chụp lookbook demo.",
                "Form dáng hiện đại, dễ phối với các sản phẩm khác trong bộ sưu tập.",
                "Dữ liệu đang ở chế độ mẫu để phục vụ dựng UI trước khi nối backend thật."
            }
        };

        return View(model);
    }

    public IActionResult Search(string? q)
    {
        var query = q?.Trim() ?? string.Empty;
        var products = GetProducts();
        var results = string.IsNullOrWhiteSpace(query)
            ? new List<ProductViewModel>()
            : products.Where(product =>
                    product.Name.Contains(query, StringComparison.OrdinalIgnoreCase)
                    || product.Category.Contains(query, StringComparison.OrdinalIgnoreCase)
                    || product.Gender.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();

        var model = new ProductSearchPageViewModel
        {
            Query = query,
            Results = results
        };

        return View(model);
    }

    private static List<ProductViewModel> GetProducts()
    {
        return new List<ProductViewModel>
        {
            new()
            {
                Id = 1,
                Name = "Áo khoác bomber Urban Edge",
                Category = "Áo",
                Gender = "Nam",
                Price = 899000,
                OriginalPrice = 1199000,
                ShortDescription = "Thiết kế bomber tối giản, dễ phối cho outfit hằng ngày.",
                Description = "Mẫu bomber chất liệu dày vừa, phù hợp thời tiết se lạnh và phong cách streetwear hiện đại.",
                ImageUrl = "https://images.unsplash.com/photo-1521572163474-6864f9cf17ab?auto=format&fit=crop&w=1200&q=80",
                GalleryImages = new List<string>
                {
                    "https://images.unsplash.com/photo-1521572163474-6864f9cf17ab?auto=format&fit=crop&w=1200&q=80",
                    "https://images.unsplash.com/photo-1515886657613-9f3515b0c78f?auto=format&fit=crop&w=1200&q=80",
                    "https://images.unsplash.com/photo-1506629905607-d9d4b7d74c2a?auto=format&fit=crop&w=1200&q=80"
                },
                Sizes = new List<string> { "M", "L", "XL" },
                Colors = new List<string> { "Đen", "Xanh navy", "Rêu" },
                StockLabel = "Còn 18 sản phẩm",
                IsNew = true,
                IsBestSeller = true,
                Rating = 4.8,
                ReviewCount = 126
            },
            new()
            {
                Id = 2,
                Name = "Đầm midi Soft Bloom",
                Category = "Áo",
                Gender = "Nữ",
                Price = 1250000,
                OriginalPrice = 1499000,
                ShortDescription = "Phom midi nhẹ nhàng, phù hợp đi làm và đi chơi cuối tuần.",
                Description = "Thiết kế đầm midi nhấn eo, chất liệu mềm rũ tạo cảm giác thanh lịch và nữ tính.",
                ImageUrl = "https://images.unsplash.com/photo-1496747611176-843222e1e57c?auto=format&fit=crop&w=1200&q=80",
                GalleryImages = new List<string>
                {
                    "https://images.unsplash.com/photo-1496747611176-843222e1e57c?auto=format&fit=crop&w=1200&q=80",
                    "https://images.unsplash.com/photo-1483985988355-763728e1935b?auto=format&fit=crop&w=1200&q=80",
                    "https://images.unsplash.com/photo-1524504388940-b1c1722653e1?auto=format&fit=crop&w=1200&q=80"
                },
                Sizes = new List<string> { "S", "M", "L" },
                Colors = new List<string> { "Kem", "Hồng phấn" },
                StockLabel = "Còn 12 sản phẩm",
                IsNew = true,
                IsBestSeller = false,
                Rating = 4.7,
                ReviewCount = 84
            },
            new()
            {
                Id = 3,
                Name = "Sneaker Motion Run",
                Category = "Giày",
                Gender = "Unisex",
                Price = 1599000,
                OriginalPrice = 1899000,
                ShortDescription = "Đôi sneaker năng động với đế êm và phối màu trẻ trung.",
                Description = "Thiết kế sneaker phục vụ nhu cầu di chuyển cả ngày, phù hợp phối đồ casual và athleisure.",
                ImageUrl = "https://images.unsplash.com/photo-1542291026-7eec264c27ff?auto=format&fit=crop&w=1200&q=80",
                GalleryImages = new List<string>
                {
                    "https://images.unsplash.com/photo-1542291026-7eec264c27ff?auto=format&fit=crop&w=1200&q=80",
                    "https://images.unsplash.com/photo-1549298916-b41d501d3772?auto=format&fit=crop&w=1200&q=80",
                    "https://images.unsplash.com/photo-1600185365483-26d7a4cc7519?auto=format&fit=crop&w=1200&q=80"
                },
                Sizes = new List<string> { "40", "41", "42" },
                Colors = new List<string> { "Trắng", "Xám", "Cam" },
                StockLabel = "Còn 25 sản phẩm",
                IsNew = false,
                IsBestSeller = true,
                Rating = 4.9,
                ReviewCount = 203
            },
            new()
            {
                Id = 4,
                Name = "Túi đeo chéo City Walk",
                Category = "Phụ kiện",
                Gender = "Unisex",
                Price = 499000,
                OriginalPrice = 699000,
                ShortDescription = "Túi đeo nhỏ gọn cho nhu cầu di chuyển linh hoạt mỗi ngày.",
                Description = "Mẫu túi đeo chéo thiết kế tối giản, nhiều ngăn và phù hợp với phong cách hiện đại.",
                ImageUrl = "https://images.unsplash.com/photo-1548036328-c9fa89d128fa?auto=format&fit=crop&w=1200&q=80",
                GalleryImages = new List<string>
                {
                    "https://images.unsplash.com/photo-1548036328-c9fa89d128fa?auto=format&fit=crop&w=1200&q=80",
                    "https://images.unsplash.com/photo-1590874103328-eac38a683ce7?auto=format&fit=crop&w=1200&q=80",
                    "https://images.unsplash.com/photo-1584917865442-de89df76afd3?auto=format&fit=crop&w=1200&q=80"
                },
                Sizes = new List<string> { "Free size" },
                Colors = new List<string> { "Đen", "Nâu" },
                StockLabel = "Còn 30 sản phẩm",
                IsNew = false,
                IsBestSeller = false,
                Rating = 4.6,
                ReviewCount = 61
            },
            new()
            {
                Id = 5,
                Name = "Quần jean straight fit Retro Blue",
                Category = "Quần",
                Gender = "Nữ",
                Price = 759000,
                OriginalPrice = null,
                ShortDescription = "Quần jean ống đứng, wash xanh cổ điển và dễ phối đồ.",
                Description = "Mẫu quần jean dáng straight fit mang cảm hứng retro, phù hợp phối cùng áo thun hoặc blazer nhẹ.",
                ImageUrl = "https://images.unsplash.com/photo-1541099649105-f69ad21f3246?auto=format&fit=crop&w=1200&q=80",
                GalleryImages = new List<string>
                {
                    "https://images.unsplash.com/photo-1541099649105-f69ad21f3246?auto=format&fit=crop&w=1200&q=80",
                    "https://images.unsplash.com/photo-1475180098004-ca77a66827be?auto=format&fit=crop&w=1200&q=80",
                    "https://images.unsplash.com/photo-1483985988355-763728e1935b?auto=format&fit=crop&w=1200&q=80"
                },
                Sizes = new List<string> { "S", "M", "L" },
                Colors = new List<string> { "Xanh denim" },
                StockLabel = "Còn 9 sản phẩm",
                IsNew = true,
                IsBestSeller = false,
                Rating = 4.5,
                ReviewCount = 47
            },
            new()
            {
                Id = 6,
                Name = "Loafer Heritage Leather",
                Category = "Giày",
                Gender = "Nam",
                Price = 1899000,
                OriginalPrice = 2199000,
                ShortDescription = "Giày loafer da thật cho outfit công sở và smart-casual.",
                Description = "Phom loafer cổ điển với bề mặt da thật, phần lót mềm tạo cảm giác thoải mái khi sử dụng lâu.",
                ImageUrl = "https://images.unsplash.com/photo-1614252369475-531eba835eb1?auto=format&fit=crop&w=1200&q=80",
                GalleryImages = new List<string>
                {
                    "https://images.unsplash.com/photo-1614252369475-531eba835eb1?auto=format&fit=crop&w=1200&q=80",
                    "https://images.unsplash.com/photo-1449824913935-59a10b8d2000?auto=format&fit=crop&w=1200&q=80",
                    "https://images.unsplash.com/photo-1543163521-1bf539c55dd2?auto=format&fit=crop&w=1200&q=80"
                },
                Sizes = new List<string> { "39", "40", "41", "42" },
                Colors = new List<string> { "Nâu", "Đen" },
                StockLabel = "Sắp hết hàng",
                IsNew = false,
                IsBestSeller = true,
                Rating = 4.8,
                ReviewCount = 94
            }
        };
    }

    private static List<string> NormalizeSelections(string[]? values)
    {
        return values?
            .Where(value => !string.IsNullOrWhiteSpace(value))
            .Select(value => value.Trim())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList()
            ?? new List<string>();
    }
}
