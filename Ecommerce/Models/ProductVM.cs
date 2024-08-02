using Ecommerce.Data;

namespace Ecommerce.Models
{
    public class ProductVM
    {
        public int IdProduct { get; set; }
        public string Name { get; set; }
        public int QuantityInStock { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string UrlImage { get; set; }
        public CategoryVM? Category { get; set; }
        public SupplierVM? Supplier { get; set; }
    }
}
