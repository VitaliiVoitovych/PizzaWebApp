using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaWebApp.Models.Entities
{
    public class Payment
    {
        public int PizzaId { get; set; }
        public int OrderId { get; set; }

        [ForeignKey(nameof(PizzaId))]
        [InverseProperty(nameof(Pizza.Payments))]
        public Pizza? PizzaNavigation{ get; set; }

        [ForeignKey(nameof(OrderId))]
        [InverseProperty(nameof(Order.Payments))]
        public Order? OrderNavigation { get; set; }
    }
}
