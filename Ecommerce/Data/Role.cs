using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Data
{
    public class Role
    {
        [Key]
        public int id_role { get; set; }
        public string role_name { get; set; }

        public ICollection<StaffRole> StaffRoles { get; set; }

        public Role()
        {
            StaffRoles = new List<StaffRole>();
        }
    }
}
