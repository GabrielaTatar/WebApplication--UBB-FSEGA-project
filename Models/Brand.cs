namespace WebApplication_DRUGSTORE.Models
{
    public class Brand
    {
        public int ID { get; set; }
        public string BrandName { get; set; }
        public ICollection<Product>? Products { get; set; }

    }
}
