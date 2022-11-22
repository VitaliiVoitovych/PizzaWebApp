using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PizzaWebApp.Models.Entities
{
    public class Order
    {
        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }

        public int CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        [InverseProperty(nameof(Customer.Orders))]
        public Customer? CustomerNavigation { get; set; }

        [JsonIgnore]
        [InverseProperty(nameof(Payment.OrderNavigation))]
        public IEnumerable<Payment> Payments { get; set; } = new List<Payment>();
    }
}
