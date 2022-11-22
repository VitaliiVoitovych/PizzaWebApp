using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PizzaWebApp.Models.Entities
{
    public class Pizza
    {
        public int PizzaId { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; } = "New Pizza";

        public PizzaSize Size { get; set; }

        public double Weight { get; set; }

        public decimal Price { get; set; }

        [JsonIgnore]
        [InverseProperty(nameof(Payment.PizzaNavigation))]
        public IEnumerable<Payment> Payments { get; set; } = new List<Payment>();
    }
}
