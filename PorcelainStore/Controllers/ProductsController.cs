using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PorcelainShop.Services;
using PorcelainStore.Models;
using PorcelainStore.Services;

namespace PorcelainStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("all")]
        public ActionResult<List<Product>> GetAll()
        {
            var products = _productService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{id:int:min(1)}")]
        public ActionResult<Product> GetById(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null) return NotFound("No product found");
            return Ok(product);
        }

        [HttpPost("add-figurine")]
        public ActionResult<string> AddFigurine([FromBody] Figurine figurine)
        {
            var result = _productService.AddProduct(figurine);
            if (result.IsSuccess)
                return Ok();
            else return BadRequest(result.Message);
        }

        [HttpPost("add-doll")]
        public ActionResult<string> AddDoll([FromBody] Doll doll)
        {
            var result = _productService.AddProduct(doll);
            if (result.IsSuccess)
                return Ok();
            else return BadRequest(result.Message);
        }

        [HttpDelete("deleteProduct/{id:int:min(1)}")]
        public ActionResult<string> DeleteProduct(int id)
        {
            var result = _productService.DeleteProduct(id);
            if (result.IsSuccess)
                return Ok();
            else return BadRequest(result.Message);
        }

        [HttpGet("find")]
        public ActionResult<List<Product>> SearchForProduct([FromQuery] string? type, [FromQuery] string? name)
        {
            var products = _productService.SearchForProduct(name, type);
            if (products.Any()) return Ok(products);
            return NotFound("No products found");
        }
    }
}
