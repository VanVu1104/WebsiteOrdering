using MediatR;
using WebsiteOrdering.ViewModels;

namespace WebsiteOrdering.Product.GetAllProducts
{
    public class GetAllProductQuery :IRequest<List<ProductsViewModel>>
    {
    }
  
}
