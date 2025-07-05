using WebsiteOrdering.Models;

namespace WebsiteOrdering.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Monan>> GetProductsByExactNameAsync(string productName);

    }
}
