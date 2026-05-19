using DATN.Models;
using Microsoft.AspNetCore.Mvc;

namespace DATN.Controllers;

public class CategoriesController : Controller
{
    public IActionResult Index()
    {
        var model = new CategoriesPageViewModel
        {
            Title = "Danh mục",
            Description = "Đi nhanh vào nhóm sản phẩm bạn quan tâm để bắt đầu trải nghiệm mua sắm.",
            Categories = new List<CategoryViewModel>
            {
                new()
                {
                    Name = "Thời trang Nam",
                    Description = "Áo khoác, áo thun, quần và giày dành cho phong cách nam tính hiện đại.",
                    ImageUrl = "https://images.unsplash.com/photo-1515886657613-9f3515b0c78f?auto=format&fit=crop&w=1200&q=80",
                    ProductCount = 128,
                    FilterValue = "Nam"
                },
                new()
                {
                    Name = "Thời trang Nữ",
                    Description = "Các thiết kế mềm mại, thanh lịch và linh hoạt cho nhiều hoàn cảnh sử dụng.",
                    ImageUrl = "https://images.unsplash.com/photo-1483985988355-763728e1935b?auto=format&fit=crop&w=1200&q=80",
                    ProductCount = 164,
                    FilterValue = "Nữ"
                },
                new()
                {
                    Name = "Giày dép",
                    Description = "Từ sneaker năng động đến loafer lịch lãm cho mọi nhu cầu hằng ngày.",
                    ImageUrl = "https://images.unsplash.com/photo-1549298916-b41d501d3772?auto=format&fit=crop&w=1200&q=80",
                    ProductCount = 96,
                    FilterValue = "Giày"
                },
                new()
                {
                    Name = "Phụ kiện",
                    Description = "Túi, ví và các điểm nhấn hoàn thiện outfit theo cách gọn gàng, tinh tế.",
                    ImageUrl = "https://images.unsplash.com/photo-1590874103328-eac38a683ce7?auto=format&fit=crop&w=1200&q=80",
                    ProductCount = 72,
                    FilterValue = "Phụ kiện"
                }
            }
        };

        return View(model);
    }
}
