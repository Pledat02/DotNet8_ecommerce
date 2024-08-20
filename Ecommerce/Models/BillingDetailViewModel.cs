namespace Ecommerce.Models
{
    public class BillingDetailViewModel
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string Phone { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string email { get; set; }
        public string shipping {  get; set; }
        public decimal amount { get; set; }
        public string  payment_method { get; set; }

        
    }
}
