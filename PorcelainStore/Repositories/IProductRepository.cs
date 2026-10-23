using PorcelainStore.Models;

namespace PorcelainStore.Repositories
{
    public interface IProductRepository
    {
        public List<Product> GetAll();
        public Product Get(int id);
        public void AddNew(Product product);
        public void Delete(Product product);
        public List<Product> GetSpecific(string type = "", string name = "");
    }
}
