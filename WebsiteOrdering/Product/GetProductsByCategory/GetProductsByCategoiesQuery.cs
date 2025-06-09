using MediatR;
using WebsiteOrdering.ViewModels;

namespace WebsiteOrdering.Product.GetAllCategoryById
{
    public class GetProductsByCategoiesQuery :IRequest<List<ProductsViewModel>>
    {
        public string CategoryId { get;  }
        public GetProductsByCategoiesQuery(string categoryId)
        {
            CategoryId = categoryId;
        }
    }
}
