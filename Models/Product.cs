using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;
using System.Xml.Linq;


namespace WebApplication_DRUGSTORE.Models
{
    public class Product
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Completati obligatoriu!")]
        [StringLength(150, MinimumLength = 3)]

        [Display(Name = "Name")]
        public string Title { get; set; }

        [Column(TypeName = "decimal(6, 2)")]
        [Range(0.01, 500)]
        public decimal Price { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        public int? BrandID { get; set; }
        public Brand? Brand { get; set; }

        public int?ReviewID { get; set; }
        public Review? Review { get; set; }

        public ICollection<ProductCategory>? ProductCategories { get; set; }

    }
}
