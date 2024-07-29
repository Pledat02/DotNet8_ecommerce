using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Data
{
    public class User
    {
        [Key]
        public int id_user { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string address { get; set; }
        public string phone { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Voucher_User> Voucher_Users { get; set; }

        public User()
        {
            Comments = new List<Comment>();
            Orders = new List<Order>();
            Voucher_Users = new List<Voucher_User>();
        }
    }
}
