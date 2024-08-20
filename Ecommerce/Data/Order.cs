using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Data
{
    public class Order
    {
        [Key]
        public int id_order { get; set; }
        public DateTime order_time { get; set; }
        public string status { get; set; }

        public int? id_voucher_user { get; set; }
        public Voucher_User? Voucher_User { get; set; }

        public Bill Bill { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }

        public Order()
        {
            OrderDetails = new List<OrderDetail>();
        }
    }
}
