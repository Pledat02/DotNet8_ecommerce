namespace Ecommerce.Models
{
     public class CommentVM
    {
        public int IdComment { get; set; }


        public DateTime CreatedDate { get; set; }

        public string Text { get; set; }

        public int Rating { get; set; }

        public UserVM? User { get; set; }
        public ProductVM? Product { get; set; }
    }
    
}
