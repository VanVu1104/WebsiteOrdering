using MediatR;
using WebsiteOrdering.Product.GetAllProducts;
using WebsiteOrdering.ViewModels;

namespace WebsiteOrdering.Product.GetProductById
{
    public class GetProductsByIdQuery :IRequest<ProductsViewModel>
    {
        public string Id { get;  }
   
        public GetProductsByIdQuery(string id)
        {
            Id = id;
        
        }
    }
}
