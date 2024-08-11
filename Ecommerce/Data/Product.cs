using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Data
{
    public class Product
    {
        [Key]
        public int id_product { get; set; }
        public string name { get; set; }
        public int quantity_in_stock { get; set; }
        public decimal price { get; set; }
        public string description { get; set; }
        public string url_image { get; set; }
        public int average_rate {  get; set; }
        public decimal percent_discount { get; set; } // max = 1;

        public int id_category { get; set; }
        public Category Category { get; set; }

        public int id_supplier { get; set; }
        public Supplier Supplier { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }

        public Product()
        {
            Comments = new List<Comment>();
            OrderDetails = new List<OrderDetail>();
        }
        public int getAvgRate()
        {
            int sum = 0;
            int count = 0;
            foreach (Comment comment in Comments)
            {   
                if(comment.rating > 0)
                {
                    sum += comment.rating;
                    count++;
                }
               
            }
            if(sum ==0 || count ==0) 
                return 0;
            return (int) (sum/count);
        }


    }
}
