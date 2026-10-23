namespace PorcelainStore.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public string PhoneNumber { get; set; }

        public List<Product> Products { get; set; } = new();
    }
}