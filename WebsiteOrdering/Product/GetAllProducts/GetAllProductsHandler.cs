using MediatR;
using Microsoft.EntityFrameworkCore;
using WebsiteOrdering.Models;
using WebsiteOrdering.ViewModels;

namespace WebsiteOrdering.Product.GetAllProducts
{
    public class GetAllProductsHandler :IRequestHandler<GetAllProductQuery,List<ProductsViewModel>>
    {
        private readonly AppDbContext _appDbContext;
        public GetAllProductsHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<ProductsViewModel>> Handle(GetAllProductQuery query, CancellationToken cancellationToken)
        {
            return await _appDbContext.SanPhams
                .Where(p => p.TRANGTHAI == "Còn" && p.IDMONAN2 == "1")
                .Select(p=>new ProductsViewModel
                {
                    IDMONAN = p.IDMONAN,
                    IDMONAN2 = p.IDMONAN2,
                    TENMONAN = p.TENMONAN,
                    MOTAMONAN = p.MOTAMONAN,
                    GIACOBAN = p.GIACOBAN,
                    ANHMONAN = p.ANHMONAN
                })
                .ToListAsync(cancellationToken);

        }
    }
}
