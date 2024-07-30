using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Data
{
    public class Voucher
    {
        [Key]
        public int id_voucher { get; set; }
        public decimal percent_discount { get; set; }
        public DateTime start_date { get; set; }
        public DateTime finish_date { get; set; }

        public ICollection<Voucher_User> Voucher_Users { get; set; }
      
        public Voucher()
        {
            Voucher_Users = new List<Voucher_User>();
        }
    }
}
