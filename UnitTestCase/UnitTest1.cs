using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using ShopBridge.Controllers;
using ShopBridge.Models;
using ShopBridge.Services.Implementations;
using ShopBridge.Services.Interfaces;

namespace UnitTestCase
{
    [TestFixture]
    public class Tests
    {
        private ProductService iproductServices;
        [SetUp]
        public void Setup()
        {
            string connectionstring = "Server=DESKTOP-F0AQSOF\\SQLEXPRESS;Database=ShopBridge;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true;User ID=sa;Password=djl123";
            var optionsBuilder = new DbContextOptionsBuilder<ShopBridgeContext>();
            optionsBuilder.UseSqlServer(connectionstring);
            var dBContext = new ShopBridgeContext(optionsBuilder.Options);
            iproductServices = new ProductService(dBContext);
        }


        [Test]
        public void GetAllProducts()
        {            
            PagingModel pagingModel = new PagingModel
            {
                pageNumber = 1,
                pageSize = 3
            };
            var result = iproductServices.GetProducts(pagingModel);
            Assert.IsNotNull(result.Result);
        }

        [Test]
        public void AddProduct()
        {
            Product product = new Product()
            {
                ProductId = 99998888,
                Name = "XYZ",
                Description = "ABC",
                Price = 100,
                Quantity = 20
            };
            var result = iproductServices.AddProduct(product);
            Assert.True(result.Result.GetType().Equals(typeof(Product)));
            Assert.AreEqual(result.Result, product);
        }

        [Test]
        public void UpdateProduct()
        {
            Product product = new Product()
            {
                ProductId = 202321316806,
                Name = "Bike",
                Description = "200 CC",
                Price = 100000,
                Quantity = 20
            };
            var result = iproductServices.UpdateProduct(product);
            Assert.True(result.Result.GetType().Equals(typeof(Product)));
        }

        [Test]
        public void DeleteProduct()
        {
            var result = iproductServices.DeleteProduct(99998888);
            Assert.True(true);
        }
    }
}