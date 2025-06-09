using MediatR;
using Microsoft.EntityFrameworkCore;
using WebsiteOrdering.Models;
using WebsiteOrdering.ViewModels;

namespace WebsiteOrdering.Product.GetAllCategoryById
{
    public class GetProductsByCategoriesHandler :IRequestHandler<GetProductsByCategoiesQuery,List<ProductsViewModel>>
    {
        private readonly AppDbContext _appDbContext;

        public GetProductsByCategoriesHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<ProductsViewModel>> Handle(GetProductsByCategoiesQuery request, CancellationToken cancellationToken)
        {
            var products = await _appDbContext.SanPhams
                .Where(p => p.IDLoaiMonAn == request.CategoryId && p.TRANGTHAI == "Còn" && p.IDMONAN2 == "1")
                .Select(p => new ProductsViewModel
                {
                    IDMONAN = p.IDMONAN,
                    IDMONAN2 = p.IDMONAN2,
                    TENMONAN = p.TENMONAN,
                    MOTAMONAN = p.MOTAMONAN,
                    GIACOBAN = p.GIACOBAN,
                    TRANGTHAI = p.TRANGTHAI,
                    ANHMONAN = p.ANHMONAN,
                    IDLoaiMonAn = p.IDLoaiMonAn
                })
                .ToListAsync(cancellationToken);
            
            return products;
        }
    }
}
