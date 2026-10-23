using System.Text.Json.Serialization;

namespace PorcelainStore.Models
{
    public abstract class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }

        [JsonIgnore]
        public int? CustomerID { get; set; }
        [JsonIgnore]
        public Customer? Customer { get; set; }
    }
}
