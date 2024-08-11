namespace Ecommerce.Models
{
    public class CartItemViewModel
    {
        public int id_product { get; set; }
        public string url_image {  get; set; }
        public string name {  get; set; }
        public int quantity { get; set; }
        public double price { get; set; }
        public double amount => price *quantity;

    
    }
}
