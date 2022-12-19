using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WebApplication_DRUGSTORE.Models
{
    public class Review
    {
        public int ID { get; set; }

        public string Stars { get; set; }

        public string Comment { get; set; }

        [Display(Name = "Full Review")]
        public string FullReview
        {
            get
            {
                return Stars + " " + Comment;
            }
        }

        public ICollection<Product>? Products { get; set; }
    }
}
