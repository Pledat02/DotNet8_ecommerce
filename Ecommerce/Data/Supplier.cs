using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Data
{
    [Table("Supplier")]
    public class Supplier
    {
        public int id_supplier { get; set; }
        [Required]
        public string name_company { get; set; }
        [Required]
        public string address { get; set; }

        public ICollection<Product> Products { get; set; }
        public Supplier()
        {
            Products = new List<Product>();
        }
    }
}
