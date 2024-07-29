namespace Ecommerce.Data
{
    public class ProductVoucher
    {
        public int id_product { get; set; }
        public int id_voucher { get; set; }

        public Product Product { get; set; }
        public Voucher Voucher { get; set; }
    }
}
