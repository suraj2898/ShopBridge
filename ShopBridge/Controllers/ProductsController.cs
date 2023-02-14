using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopBridge.Models;
using ShopBridge.Services.Interfaces;


namespace ShopBridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        } 

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery]PagingModel pagingModel)
        {
            try
            {
                var products= await _productService.GetProducts(pagingModel);
                return Ok(products);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error While Retrieving Products from Database");
            }
        }

        [HttpPost("Add")]
        public async Task<ActionResult<Product>> AddProduct([FromBody]Product product)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest();
                }
                product.ProductId = _productService.GenerateProductID();
                var addedproduct = await _productService.AddProduct(product);
                return Created("~api/Products",addedproduct);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error While Adding Product");
            }
        }

        [HttpPut("Update")]
        public async Task<ActionResult<Product>> UpdateProduct([FromBody] Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var productToUpdate = await _productService.GetProduct(product.ProductId);
                if(productToUpdate==null)
                {
                    return NotFound($"Product With Given ID = {product.ProductId} not found");
                }
                return await _productService.UpdateProduct(product);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error While Updating Product");
            }
        }

        [HttpDelete]
        [Route("Remove/{productid:int}")]
        public async Task<ActionResult<Product>> DeleteProduct(int productid)
        {
            try
            {
                var productToDelete = await _productService.GetProduct(productid);
                if (productToDelete == null)
                {
                    return NotFound($"Product With Given ID = {productid} not found");
                }
                await _productService.DeleteProduct(productid);
                return Ok(productToDelete);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error While Deleting Product");
            }
        }
    }
}
