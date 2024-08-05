namespace Ecommerce.Models
{
    public class StaffVM
    {
        public int IdStaff { get; set; }
        public string NameStaff { get; set; }
        public string TypeStaff { get; set; }
        public string Position { get; set; }
        public string Sex { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public List<string> Roles { get; set; }
    }
}
