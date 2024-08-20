namespace Ecommerce.Data
{
    public class Voucher_User
    {
        public int id_voucher_User { get; set; }
        public int? id_user { get; set; }
        public int? id_voucher { get; set; }
        public int state { get; set; } // 1 la da su dung , 0 la chua su dung

        public Voucher? Voucher { get; set; }
        public User? User { get; set; }

        public int? id_order { get; set; }
        public Order Order { get; set; }
    }
}
