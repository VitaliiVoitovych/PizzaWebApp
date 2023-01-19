using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PizzaWebApp.Models.Entities
{
    public class Person
    {
        public int PersonId { get; set; }

        [Required, StringLength(50)]
        public string FirstName { get; set; } = "New";

        [Required, StringLength(50)]
        public string LastName { get; set; } = "Person";

        [Required, StringLength(50)]
        public string Email { get; set; } = "Email";

        [Required, StringLength(50)]
        public string Password { get; set; } = "Password";

        [Required, StringLength(15)]
        public string Role { get; set; } = "User";

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string? FullName { get; set; }

        [JsonIgnore]
        [InverseProperty(nameof(Order.PersonNavigation))]
        public IEnumerable<Order> Orders { get; set; } = new List<Order>();
    }
}
