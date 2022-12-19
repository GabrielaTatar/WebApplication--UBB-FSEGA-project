using System.ComponentModel.DataAnnotations;

namespace WebApplication_DRUGSTORE.Models
{
    public class Purchase
    {
        public int ID { get; set; }
        public int? MemberID { get; set; }
        public Member? Member { get; set; }
        public int? ProductID { get; set; }

        public Product? Product { get; set; }

        [DataType(DataType.Date)]
        public DateTime PurchaseDate { get; set; }
    }
}
