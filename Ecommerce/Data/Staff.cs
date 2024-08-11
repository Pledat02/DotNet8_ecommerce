using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Data
{
    public class Staff
    {
        [Key]
        public int id_staff { get; set; }
        public string name_staff { get; set; }
        public string type_staff { get; set; }
        public string position { get; set; }
        public string sex { get; set; }
        public string email { get; set; }
        public int id_account { get; set; }
        public Account Account { get; set; }


        public ICollection<Order> Orders { get; set; }
        public ICollection<StaffRole> StaffRoles { get; set; }

        public Staff()
        {
            Orders = new List<Order>();
            StaffRoles = new List<StaffRole>();
        }
       
    }
}
