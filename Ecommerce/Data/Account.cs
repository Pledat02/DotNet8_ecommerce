using Microsoft.Identity.Client;

namespace Ecommerce.Data
{
    public class Account
    {
        public int id_account { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int type { get; set; } // 0 = User , 1 = Staff
        public Staff Staff { get; set; }
        public User User { get; set; }
        public override string ToString()
        {
            return $"Account ID: {id_account}, Username: {username}, Type: {(type == 0 ? "User" : "Staff")}, User : {User?.ToString()}, Staff : {Staff?.ToString()}";
        }
    }
 
}
