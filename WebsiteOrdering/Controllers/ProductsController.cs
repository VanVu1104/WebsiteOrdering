using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebsiteOrdering.Models;
using WebsiteOrdering.Product.GetAllCategory;
using WebsiteOrdering.Product.GetAllCategoryById;
using WebsiteOrdering.Product.GetAllProducts;
using WebsiteOrdering.Product.GetProductById;
using WebsiteOrdering.ViewModels;

namespace WebsiteOrdering.Controllers
{
    [Route("Products")]
    public class ProductsController : Controller
    {
        public readonly IMediator _mediator;
        private readonly AppDbContext _appDbContext;
        public ProductsController(IMediator mediator, AppDbContext appDbContext)
        {
            _mediator = mediator;
            _appDbContext = appDbContext;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}



        [HttpGet("")]
        public async Task<IActionResult> Index(int page = 1, string categoryId = null)
        {
            var categories = await _mediator.Send(new GetAllCategoriesQuery());
            ViewBag.Categories = categories;
            List<ProductsViewModel> products;

            if (!string.IsNullOrEmpty(categoryId))
            {
                // Kiểm tra xem category này có con không
                var hasChildren = categories.Any(c => c.IDLOAIMONANCHA == categoryId);

                if (hasChildren)
                {
                    // Nếu có category con, lấy tất cả category con đệ quy
                    var allChildrenIds = GetAllChildCategoryIds(categories, categoryId);

                    products = new List<ProductsViewModel>();

                    // Lấy sản phẩm từ tất cả category con (đệ quy)
                    foreach (var childId in allChildrenIds)
                    {
                        var prods = await _mediator.Send(new GetProductsByCategoiesQuery(childId));
                        if (prods != null && prods.Count > 0)
                        {
                            products.AddRange(prods);
                        }
                    }
                }
                else
                {
                    // Nếu không có category con (là category lá), lấy sản phẩm trực tiếp
                    products = await _mediator.Send(new GetProductsByCategoiesQuery(categoryId));
                    if (products == null)
                        products = new List<ProductsViewModel>();
                }

                ViewBag.SelectedCategory = categoryId;
                if (products.Count == 0)
                {
                    TempData["Message"] = "Không tìm thấy sản phẩm thuộc loại đã chọn.";
                }
            }
            else
            {
                products = await _mediator.Send(new GetAllProductQuery());
                ViewBag.SelectedCategory = null;
            }

            int pageSize = 9;
            var totalProducts = products.Count();
            var totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
            var paginatedProducts = products
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            return View(paginatedProducts);
        }

        // Hàm đệ quy lấy tất cả category con của một category cha
        private List<string> GetAllChildCategoryIds(List<CategoryViewModel> categories, string parentId)
        {
            var result = new List<string>();
            var children = categories.Where(c => c.IDLOAIMONANCHA == parentId).ToList();

            foreach (var child in children)
            {
                result.Add(child.IDLOAIMONAN);
                // Đệ quy lấy con của con
                result.AddRange(GetAllChildCategoryIds(categories, child.IDLOAIMONAN));
            }

            return result;
        }

        //Hiển thị chi tiết sản phẩm
        [HttpGet("Detail/{id}")]
        public async Task<IActionResult> Detail(string id)
        {

            var product = await _mediator.Send(new GetProductsByIdQuery(id));
            if (product == null)
                return NotFound();
            return View(product);
        }

    }
}
