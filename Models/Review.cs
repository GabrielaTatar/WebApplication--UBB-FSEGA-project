namespace WebApplication_DRUGSTORE.Models
{
    public class Review
    {
        public int ID { get; set; }

        public string Stars { get; set; }

        public string Comment { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
