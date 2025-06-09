using MediatR;
using Microsoft.EntityFrameworkCore;
using WebsiteOrdering.Models;
using WebsiteOrdering.ViewModels;

namespace WebsiteOrdering.Product.GetAllCategory
{
    public class GetAllCategoriesHandler :IRequestHandler<GetAllCategoriesQuery, List<CategoryViewModel>>
    {
        private readonly AppDbContext _context;
        public GetAllCategoriesHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<CategoryViewModel>> Handle(GetAllCategoriesQuery query,CancellationToken cancellationToken)
        {
            return await _context.Categories.ToListAsync(cancellationToken);
        }
    }
}
