namespace Ecommerce.Models
{
    public class VoucherVM
    {
        public string IdVoucher { get; set; }
      
        public decimal PercentDiscount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }

    }
}
