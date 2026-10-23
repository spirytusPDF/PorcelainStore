using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PorcelainStore.Models;
using PorcelainStore.Services;

namespace PorcelainStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _service;
        public CustomerController(CustomerService service)
        {
            _service = service;
        }

        [HttpGet("all")]
        public ActionResult<List<Product>> GetAll()
        {
            var customers = _service.GetAll();
            return Ok(customers);
        }

        [HttpGet("{id:int:min(1)}")]
        public ActionResult<Product> GetById(int id)
        {
            var customer = _service.GetById(id);
            if (customer == null) return NotFound("No customer found");
            return Ok(customer);
        }

        [HttpPost("add-customer/{productId:int:min(1)}")]
        public ActionResult<string> AddCustomer([FromBody]Customer customer, int productId)
        {
            var result = _service.AddCustomer(customer, productId);
            if (result.IsSuccess)
                return Ok();
            else return BadRequest(result.Message);
        }
        [HttpPost("add-customer-product/{customerId:int:min(1)}/{productId:int:min(1)}")]
        public ActionResult<string> AddProduct(int customerId, int productId)
        {
            var result = _service.AddProductForCustomer(customerId, productId);
            if (result.IsSuccess)
                return Ok();
            else return BadRequest(result.Message);
        }


        [HttpDelete("delete-customer/{customerId:int:min(1)}")]
        public ActionResult<string> DeleteCustomer(int customerId)
        {
            var result = _service.DeleteCustomer(customerId);
            if (result.IsSuccess)
                return Ok();
            else return BadRequest(result.Message);
        }
        [HttpDelete("delete-customer-product/{customerId:int:min(1)}/{productId:int:min(1)}")]
        public ActionResult<string> DeleteProduct(int customerId, int productId)
        {
            var result = _service.DeleteProductOfCustomer(customerId, productId);
            if (result.IsSuccess)
                return Ok();
            else return BadRequest(result.Message);
        }
    }
}
