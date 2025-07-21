using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebsiteOrdering.Models;
using WebsiteOrdering.Product.GetAllCategory;
using WebsiteOrdering.Product.GetAllCategoryById;
using WebsiteOrdering.Product.GetAllProducts;
using WebsiteOrdering.Product.GetProductById;
using WebsiteOrdering.Services;
using WebsiteOrdering.Helper;
using WebsiteOrdering.ViewModels;
using WebsiteOrdering.Helper;
using WebsiteOrdering.ViewModels; // Để dùng session extension (Get<T>)

namespace WebsiteOrdering.Controllers
{
    [Route("Products")]
    public class ProductsController : Controller
    {
        public readonly IMediator _mediator;
        private readonly AppDbContext _appDbContext;
        private readonly LuceneProductIndexer _luceneIndexer;

        public ProductsController(IMediator mediator, AppDbContext appDbContext,
            LuceneProductIndexer luceneIndexer)

        public ProductsController(IMediator mediator, AppDbContext appDbContext)
        {
            _mediator = mediator;
            _appDbContext = appDbContext;
            _luceneIndexer = luceneIndexer;
        }


        //[HttpGet("")]
        //public async Task<IActionResult> Index(int page = 1, string categoryId = null, string searchTerm = "")
        //{
        //    var categories = await _mediator.Send(new GetAllCategoriesQuery());
        //    ViewBag.Categories = categories;
        //    ViewBag.SelectedCategory = categoryId;
        //    ViewBag.SearchTerm = searchTerm;

        //    List<Monan> products;

        //    // Có tìm kiếm
        //    if (!string.IsNullOrEmpty(searchTerm))
        //    {
        //        var exactMatchProducts = await _mediator.Send(new GetProductsByExactNameQuery(searchTerm));

        //        if (exactMatchProducts != null && exactMatchProducts.Any())
        //        {
        //            products = exactMatchProducts.ToList();
        //        }
        //        else
        //        {
        //            var luceneResults = _luceneIndexer.SearchWithScore(searchTerm, 100);
        //            var matchedIds = luceneResults.Select(r => r.Id).ToList();
        //            products = await _mediator.Send(new GetAllProductQuery());
        //            products = products.Where(p => matchedIds.Contains(p.Idmonan)).ToList();
        //        }
        //    }
        //    else if (!string.IsNullOrEmpty(categoryId))
        //    {
        //        var allChildrenIds = GetAllChildCategoryIds(categories, categoryId);
        //        allChildrenIds.Add(categoryId);

        //        products = new List<Monan>();
        //        foreach (var catId in allChildrenIds)
        //        {
        //            var prods = await _mediator.Send(new GetProductsByCategoiesQuery(catId));
        //            if (prods != null) products.AddRange(prods);
        //        }
        //        if (products.Count == 0)
        //        {
        //            TempData["Message"] = "Không tìm thấy sản phẩm thuộc loại đã chọn.";
        //        }
        //    }
        //    // Không tìm kiếm, không lọc
        //    else
        //    {
        //        products = await _mediator.Send(new GetAllProductQuery());
        //    }

        //    // Phân trang
        //    int pageSize = 9;
        //    var totalProducts = products.Count();
        //    var totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
        //    var paginatedProducts = products
        //        .Skip((page - 1) * pageSize)
        //        .Take(pageSize)
        //        .ToList();

        //    ViewBag.CurrentPage = page;
        //    ViewBag.TotalPages = totalPages;
        //    return View(paginatedProducts);
        //}

        // Hàm đệ quy lấy tất cả category con của một category cha
        private List<string> GetAllChildCategoryIds(List<Loaimonan> categories, string parentId)
        {
            var result = new List<string>();
            if (categories == null || parentId == null)
                return result;

            var children = categories.Where(c => c.IdloaimanCha == parentId).ToList();
        //[HttpGet("")]
        //public async Task<IActionResult> Index(int page = 1, string categoryId = null)
        //{

        //    var categories = await _mediator.Send(new GetAllCategoriesQuery());
        //    ViewBag.Categories = categories;

        //    List<Monan> products;

        //    if (!string.IsNullOrEmpty(categoryId))
        //    {
        //        var allChildrenIds = GetAllChildCategoryIds(categories, categoryId);
        //        allChildrenIds.Add(categoryId); // Thêm chính nó

        //        products = new List<Monan>();

            foreach (var child in children)
            {
                result.Add(child.Idloaimonan);
                // Đệ quy lấy con của con
                result.AddRange(GetAllChildCategoryIds(categories, child.Idloaimonan));
            }

            return result;
        }

        //        foreach (var catId in allChildrenIds)
        //        {
        //            var prods = await _mediator.Send(new GetProductsByCategoiesQuery(catId));
        //            if (prods != null && prods.Count > 0)
        //            {
        //                products.AddRange(prods);
        //            }
        //        }

        //        ViewBag.SelectedCategory = categoryId;

        //        if (products.Count == 0)
        //        {
        //            TempData["Message"] = "Không tìm thấy sản phẩm thuộc loại đã chọn.";
        //        }
        //    }
        //    else
        //    {
        //        products = await _mediator.Send(new GetAllProductQuery());
        //        ViewBag.SelectedCategory = null;
        //    }

        //    // ✅ KHÔNG PHÂN TRANG – hiện tất cả
        //    ViewBag.CurrentPage = 1;
        //    ViewBag.TotalPages = 1;

        //    // ✅ LẤY GIỎ HÀNG TỪ SESSION
        //    var cart = HttpContext.Session.Get<List<CartItem>>("Cart") ?? new List<CartItem>();
        //    ViewBag.CartItems = cart;

        //    return View(products);
        //}

        [HttpGet("")]
        public async Task<IActionResult> Index(int page = 1, string categoryId = null, string categoryIds = null)
        {

            var categories = await _mediator.Send(new GetAllCategoriesQuery());
            ViewBag.Categories = categories;

            List<Monan> products;

            if (!string.IsNullOrEmpty(categoryId))
            {
            // ✅ Nếu có categoryIds (danh sách nhiều loại)
            if (!string.IsNullOrEmpty(categoryIds))
            {
                var categoryIdList = categoryIds.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
                products = new List<Monan>();

                foreach (var catId in categoryIdList)
                {
                    // ✅ Bổ sung lấy mục con:
                    var allChildrenIds = GetAllChildCategoryIds(categories, catId);
                    allChildrenIds.Add(catId);  // Thêm chính nó vào danh sách

                    foreach (var childId in allChildrenIds)
                    {
                        var prods = await _mediator.Send(new GetProductsByCategoiesQuery(childId));
                        if (prods != null && prods.Count > 0)
                        {
                            products.AddRange(prods);
                        }
                    }
                }

                ViewBag.SelectedCategory = categoryIds;

                if (products.Count == 0)
                {
                    TempData["Message"] = "Không tìm thấy sản phẩm thuộc các loại đã chọn.";
                }
            }

            // ✅ Logic cũ khi lọc theo categoryId đơn lẻ
            else if (!string.IsNullOrEmpty(categoryId))
            {
                var allChildrenIds = GetAllChildCategoryIds(categories, categoryId);
                allChildrenIds.Add(categoryId); // Thêm chính nó
                allChildrenIds.Add(categoryId);

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

            // ✅ KHÔNG PHÂN TRANG – hiện tất cả
            ViewBag.CurrentPage = 1;
            ViewBag.TotalPages = 1;
            ViewBag.CurrentPage = 1;
            ViewBag.TotalPages = 1;

            var cart = HttpContext.Session.Get<List<CartItem>>("Cart") ?? new List<CartItem>();
            ViewBag.CartItems = cart;

            return View(products);
        }


        // ✅ Hàm đệ quy lấy tất cả category con của một category cha
        private List<string> GetAllChildCategoryIds(List<Loaimonan> categories, string parentId)
        {
            var result = new List<string>();
            if (categories == null || parentId == null)
                return result;

            // ✅ LẤY GIỎ HÀNG TỪ SESSION
            var cart = HttpContext.Session.Get<List<CartItem>>("Cart") ?? new List<CartItem>();
            ViewBag.CartItems = cart;

            return View(products);
        }
            foreach (var child in children)
            {
                result.Add(child.Idloaimonan);
                result.AddRange(GetAllChildCategoryIds(categories, child.Idloaimonan)); // đệ quy
            }

        //Hiển thị chi tiết sản phẩm
            return result;
        }

        // ✅ Hiển thị chi tiết sản phẩm
        [HttpGet("Detail/{id}")]
        public async Task<IActionResult> Detail(string id)
        {
            var product = await _mediator.Send(new GetProductsByIdQuery(id));
            if (product == null)
                return NotFound();

            // Thêm field phụ để gán cha
            if (product.Idloaimonan == "LMA08" || product.Idloaimonan == "LMA09")
            {
                product.Idloaimonan = "LMA07"; // như bạn đã làm
            }
            else if (product.Idloaimonan == "LMA10" || product.Idloaimonan == "LMA11" || product.Idloaimonan == "LMA12" || product.Idloaimonan == "LMA13" || product.Idloaimonan == "LMA14")
            {
                product.Idloaimonan = "LMA08"; // gán cha
            }
            else if (product.Idloaimonan == "LMA15" || product.Idloaimonan == "LMA16")
            {
                product.Idloaimonan = "LMA09"; // gán cha
            }

            return View(product);
        }
    }
}