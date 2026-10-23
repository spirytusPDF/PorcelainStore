using Microsoft.EntityFrameworkCore;
using PorcelainStore.Contexts;
using PorcelainStore.Models;

namespace PorcelainStore.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddNew(Product product)
        {
            _context.Add(product);
            _context.SaveChanges();
        }

        public void Delete(Product product)
        {
            _context.Remove(product);
            _context.SaveChanges();
        }

        public Product Get(int id)
        {
            return _context.Products.FirstOrDefault(p => p.Id == id)??throw new Exception("No product found");
        }

        public List<Product> GetAll()
        {
           return _context.Products.ToList();
        }

        public List<Product> GetSpecific(string type="", string name = "")
        {
           
            IQueryable<Product> query = _context.Products;

            if (!string.IsNullOrWhiteSpace(type))
            {
                query = query.Where(p => EF.Property<string>(p, "Discriminator").ToLower() == type.ToLower());
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(p => p.Name.ToLower().Contains(name.ToLower()));
            }

           
            var result = query.ToList();

            return result;
        }
    }
}
