using Microsoft.EntityFrameworkCore;
using PorcelainStore.Contexts;
using PorcelainStore.Models;

namespace PorcelainStore.Repositories
{
    public class CustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddCustomer(Customer customer, int productId)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == productId);
            if (product == null) throw new Exception("Could't find product to bind");
            customer.Products.Add(product);
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }
        public void AddProductCustomer(int customerId, int productId)
        {
            var customerToUpdate = _context.Customers
                .Include(c => c.Products)
                .FirstOrDefault(c => c.Id == customerId);

            if (customerToUpdate == null) throw new Exception("Customer is not found");
            var product = _context.Products.Find(productId);
            if (product == null) throw new Exception("Could't find the product");

            if(product.Customer != null) throw new Exception("The product is already taken :)");

            customerToUpdate.Products.Add(product);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var customerToRemove = _context.Customers.Include(c=>c.Products).FirstOrDefault(c => c.Id == id);
            if (customerToRemove != null && customerToRemove != new Customer())
            {
                customerToRemove.Products.ForEach(p =>  p.Customer = null);
                _context.Remove(customerToRemove);
                _context.SaveChanges();
            }
            else throw new Exception("Could't find costumer with this id");
        }

        public void DeleteProduct(int customerId, int productId)
        {
            var customerToUpdate = _context.Customers
                .Include(c => c.Products)
                .FirstOrDefault(c => c.Id == customerId);

            if (customerToUpdate == null) throw new Exception("Could't find costumer with this id");

            var productToRemove = customerToUpdate.Products.FirstOrDefault(p => p.Id == productId);
            if (productToRemove == null) throw new Exception("Could't find product in the costumer");

            customerToUpdate.Products.Remove(productToRemove);
            if(customerToUpdate.Products.Count <= 0) Delete(customerId);
            _context.SaveChanges();
        }

        public Customer Get(int id)
        {
            return _context.Customers
                .Include(c => c.Products)
                .FirstOrDefault(c => c.Id == id) ?? new Customer();
        }

        public List<Customer> GetAll()
        {
            return _context.Customers
                .Include(c => c.Products)
                .ToList();
        }
    }
}
