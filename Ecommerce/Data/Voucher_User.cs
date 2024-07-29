namespace Ecommerce.Data
{
    public class Voucher_User
    {
        public int id_voucher { get; set; }
        public int id_user { get; set; }

        public Voucher Voucher { get; set; }
        public User User { get; set; }
    }
}
