using PorcelainShop.Services;
using PorcelainStore.Models;
using PorcelainStore.Repositories;

namespace PorcelainStore.Services
{
    public class ProductService
    {
        private readonly ProductRepository _repository;
        public ProductService(ProductRepository repository)
        {
            _repository = repository;
        }

        public List<Product> GetAllProducts()
        {
            return _repository.GetAll();
        }
         
        public Product GetProductById(int id)
        {
            try
            {
                return _repository.Get(id);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Doll();
            }
        }

        public Result AddProduct(Product product)
        {
            if(string.IsNullOrEmpty(product.Name)) new Result() { IsSuccess = false, Message = "Product name must be at least 1 character long" };
            try
            {
                _repository.AddNew(product);
                return new Result() { IsSuccess=true, Message=$"successfully added {product.Name}"};
            }
            catch (Exception ex)
            {
                return new Result() { IsSuccess = false, Message = $"{product.Name}: {ex.Message}" };
            }
        }

        public Result DeleteProduct(int id)
        {
            try
            {
                var productToDelete = _repository.Get(id);
                _repository.Delete(productToDelete);
                return new Result() { IsSuccess = true, Message = $"successfully deleted {productToDelete.Name}" };
            }
            catch (Exception ex)
            {
                return new Result() { IsSuccess = false, Message = $"Id - {id}: {ex.Message}" };
            }
            

        }

        public List<Product> SearchForProduct(string name="", string type="")
        {
            try
            {
                return _repository.GetSpecific(type, name);
            }
            catch( Exception ex){
                Console.WriteLine(ex.Message);
                return new List<Product> { new Doll() };
            }
            
        }
    }
}
