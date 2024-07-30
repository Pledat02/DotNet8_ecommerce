using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Data
{
    public class Bill
    {
        [Key, ForeignKey("Order")]
        public int id_order { get; set; }
        public string payment_method { get; set; }
        public DateTime date { get; set; }
        public decimal amount { get; set; }

        public Order Order { get; set; }
    }
}
