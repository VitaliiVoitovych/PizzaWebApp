using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaWebApp.Models.Entities
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int PizzaId { get; set; }
        public int OrderId { get; set; }

        public DateTime PaymentDate { get; set; }

        [ForeignKey(nameof(PizzaId))]
        [InverseProperty(nameof(Pizza.Payments))]
        public Pizza? PizzaNavigation { get; set; }

        [ForeignKey(nameof(OrderId))]
        [InverseProperty(nameof(Order.Payments))]
        public Order? OrderNavigation { get; set; }
    }
}
