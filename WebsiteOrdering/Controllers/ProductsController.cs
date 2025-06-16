using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebsiteOrdering.Models;
using WebsiteOrdering.Product.GetAllCategory;
using WebsiteOrdering.Product.GetAllCategoryById;
using WebsiteOrdering.Product.GetAllProducts;
using WebsiteOrdering.Product.GetProductById;

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


        [HttpGet("")]
        public async Task<IActionResult> Index(int page = 1, string categoryId = null)
        {
            var categories = await _mediator.Send(new GetAllCategoriesQuery());
            ViewBag.Categories = categories;
            List<Monan> products;

   
            if (!string.IsNullOrEmpty(categoryId))
            {
                // Lấy tất cả category con đệ quy
                var allChildrenIds = GetAllChildCategoryIds(categories, categoryId);
                allChildrenIds.Add(categoryId); // Thêm chính nó nữa!

                // Lấy sản phẩm từ tất cả category con (bao gồm chính nó)
                products = new List<Monan>();

                foreach (var catId in allChildrenIds)
                {
                    var prods = await _mediator.Send(new GetProductsByCategoiesQuery(catId));
                    if (prods != null && prods.Count > 0)
                    {
                        products.AddRange(prods);
                    }
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
        private List<string> GetAllChildCategoryIds(List<Loaimonan> categories, string parentId)
        {
            var result = new List<string>();
            if (categories == null || parentId == null)
                return result;

            var children = categories.Where(c => c.IdloaimanCha == parentId).ToList();

            foreach (var child in children)
            {
                result.Add(child.Idloaimonan);
                // Đệ quy lấy con của con
                result.AddRange(GetAllChildCategoryIds(categories, child.Idloaimonan));
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
