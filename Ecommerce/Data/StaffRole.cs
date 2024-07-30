namespace Ecommerce.Data
{
    public class StaffRole
    {
        public int id_staff { get; set; }
        public int id_role { get; set; }

        public Staff Staff { get; set; }
        public Role Role { get; set; }
    }
}
