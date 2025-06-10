using MediatR;
using WebsiteOrdering.ViewModels;

namespace WebsiteOrdering.Product.GetAllCategory
{
    public class GetAllCategoriesQuery:IRequest<List<CategoryViewModel>>
    {

    }
}
