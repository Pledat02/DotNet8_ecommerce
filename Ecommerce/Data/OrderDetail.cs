using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Data
{
    public class OrderDetail
    {
        [Key]
        public int id_order { get; set; }
        public int id_product { get; set; }

        public decimal total_price { get; set; }
        public int quantity { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
