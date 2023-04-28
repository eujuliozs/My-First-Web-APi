namespace FirstWebApi.Models
{
    public class Category
    {
        public int Id { get; private set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
