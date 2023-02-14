using ShopBridge.Models;

namespace ShopBridge.Services.Interfaces
{
    public interface IProductService
    {
        Task<Product> AddProduct(Product product);
        Task DeleteProduct(long productid);
        long GenerateProductID();
        Task<Product> GetProduct(long productid);
        Task<IEnumerable<Product>> GetProducts(PagingModel pagingModel);
        Task<Product> UpdateProduct(Product product);
    }
}
