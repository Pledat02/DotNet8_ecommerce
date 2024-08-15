using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Data
{
    public class Bill
    {
        public int id_order { get; set; }
        public string? payment_method { get; set; }
        public DateTime date { get; set; }
        public decimal amount { get; set; }
        public string? shipping {  get; set; }
        public string? firstname { get; set; }
        public string? lastname { get; set; }
        public string? address { get; set; }
        public string? city { get; set; }
        public string? Phone { get; set; }
        public string? PostalCode { get; set; }
        public string? CountryCode { get; set; }
        public string? email { get; set; }
        public Order? Order { get; set; }
    }
}
