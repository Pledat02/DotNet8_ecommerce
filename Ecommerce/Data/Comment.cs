using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Data
{
    public class Comment
    {
        [Key]
        public int id_comment { get; set; }
        public string text { get; set; }
        public DateTime created_date { get; set; }
        public int rating { get; set; }

        public int id_user { get; set; }
        public User User { get; set; }

        public int id_product { get; set; }
        public Product Product { get; set; }

        public override string ToString()
        {
            return $"Comment [ID: {id_comment}, Text: {text}, Created Date: {created_date}, Rating: {rating}, User ID: {id_user}, Product ID: {id_product}]";
        }
    }

}
