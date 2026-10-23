using PorcelainShop.Services;
using PorcelainStore.Models;
using PorcelainStore.Repositories;

namespace PorcelainStore.Services
{
    public class CustomerService
    {
        private readonly CustomerRepository _repository;
        public CustomerService(CustomerRepository repository)
        {
            _repository = repository;
        }

        public Result AddCustomer(Customer customer, int productId)
        {
            try
            {
                _repository.AddCustomer(customer, productId);
                return new Result(){ IsSuccess = true, Message=$"Added customer {customer.Name}" };
            }
            catch (Exception ex)
            {
                return new Result() { IsSuccess = false, Message = $"{customer.Name}: {ex.Message}" };
            }
        }

        public Result AddProductForCustomer(int costumerId, int productId)
        {
            try
            {
                _repository.AddProductCustomer(costumerId, productId);
                return new Result() { IsSuccess = true, Message = $"Added product with id {productId}" };
            }
            catch (Exception ex)
            {
                return new Result() { IsSuccess = false, Message = $"User Id: {costumerId}: {ex.Message}" };
            }
        }

        public Result DeleteCustomer(int id)
        {
            try
            {
                _repository.Delete(id);
                return new Result() { IsSuccess = true, Message = $"Deleted customer with id {id}" };
            }
            catch (Exception ex)
            {
                return new Result() { IsSuccess = false, Message = $"User Id: {id}: {ex.Message}" };
            }
        }

        public Result DeleteProductOfCustomer(int customerId, int productId)
        {
            try
            {
                _repository.DeleteProduct(customerId, productId);
                return new Result() { IsSuccess = true, Message = $"Deleted customer's product with id {productId}" };
            }
            catch (Exception ex)
            {
                return new Result() { IsSuccess = false, Message = $"User Id: {customerId}: {ex.Message}" };
            }
        }

        public List<Customer> GetAll()
        {
            return _repository.GetAll();
        }

        public Customer GetById(int id)
        {
            return _repository.Get(id);
        }

    }
}
