namespace Ecommerce.Models
{
    public class OrderDetailsVM
    {
        public int IdOrder { get; set; }

        public int IdProduct { get; set; }

        public decimal TotalPrice { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

       // public OrderVM? Order { get; set; }
        public ProductVM? Product { get; set; }
    }
}
