using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Data
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int id_product { get; set; }
        public string name { get; set; }
        public int quantity_in_stock { get; set; }
        public decimal price { get; set; }
        public string description { get; set; }
        public string url_image { get; set; }

        public int? id_category { get; set; }
        public Category Category { get; set; }

        public int? id_supplier { get; set; }
        public Supplier Supplier { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }

        public Product()
        {
            Comments = new List<Comment>();
            OrderDetails = new List<OrderDetail>();
        }

    }
}
