using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PizzaWebApp.Models.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required, StringLength(50)]
        public string FirstName { get; set; } = "New";

        [Required, StringLength(50)]
        public string LastName { get; set; } = "Customer";

        [Required, StringLength(50)]
        public string Email { get; set; } = "Email";

        [Required, StringLength(50)]
        public string Password { get; set; } = "Password";

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string? FullName { get; set; }

        [JsonIgnore]
        [InverseProperty(nameof(Order.CustomerNavigation))]
        public IEnumerable<Order> Orders { get; set; } = new List<Order>();
    }
}
