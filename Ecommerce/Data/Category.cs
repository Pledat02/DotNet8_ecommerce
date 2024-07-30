using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Data
{
    public class Category
    {
        [Key]
       public int id_category { get; set; }
        [Required]
       public string name { get; set; }
       public string description { get; set; }
        public ICollection<Product> Products { get; set; }
        public Category()
        {
            Products = new List<Product>();
        }
    }
}
