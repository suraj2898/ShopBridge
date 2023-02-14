using Microsoft.EntityFrameworkCore;
using ShopBridge.Models;
using ShopBridge.Services.Interfaces;

namespace ShopBridge.Services.Implementations
{
    public class ProductService:IProductService
    {
        private readonly ShopBridgeContext _context;
        public ProductService(ShopBridgeContext context)
        {
            _context = context;
        }

        public async Task<Product> GetProduct(long productid)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.ProductId == productid);
        }

        public long GenerateProductID()
        {
            Random rnd = new Random();
            return Convert.ToInt64(DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + rnd.Next(10000, 99999).ToString());
        }

        public async Task<IEnumerable<Product>> GetProducts(PagingModel pagingModel)
        {
            var products = await _context.Products.
                Skip((pagingModel.pageNumber-1)*pagingModel.pageSize).Take(pagingModel.pageSize).ToListAsync();
            return products;
        }

        public async Task<Product> AddProduct(Product product)
        {
            var result=await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            var result=await _context.Products.FirstOrDefaultAsync(p=>p.ProductId==product.ProductId);
            if(result!=null)
            {
                result.Name=product.Name;
                result.Description=product.Description;
                result.Price=product.Price;
                result.Quantity = product.Quantity;
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task DeleteProduct(long productid)
        {
            var result = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == productid);
            if(result!=null)
            {
                _context.Products.Remove(result);
                await _context.SaveChangesAsync();
            }
        }
    }
}
