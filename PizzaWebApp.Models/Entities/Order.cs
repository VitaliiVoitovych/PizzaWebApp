using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PizzaWebApp.Models.Entities
{
    public class Order
    {
        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }

        public int PersonId { get; set; }

        [ForeignKey(nameof(PersonId))]
        [InverseProperty(nameof(Person.Orders))]
        public Person? PersonNavigation { get; set; }

        [JsonIgnore]
        [InverseProperty(nameof(Payment.OrderNavigation))]
        public IEnumerable<Payment> Payments { get; set; } = new List<Payment>();
    }
}
