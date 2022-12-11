using PizzaWebApp.Models.Entities;
using System.Collections;

namespace PizzaWebApp.Models
{
    public class Cart : IEnumerable<Pizza>
    {
        private Dictionary<Pizza, int> pizzas;

        public Cart()
        {
            pizzas = new Dictionary<Pizza, int>();
        }

        public decimal Price => pizzas.Sum(p => p.Key.Price * p.Value);

        public void AddItem(Pizza pizza)
        {
            if (pizzas.TryGetValue(pizza, out int count))
            {
                pizzas[pizza] = ++count;
            }
            else
            {
                pizzas.Add(pizza, 1);
            }
        }

        public void RemoveItem(Pizza pizza)
        {
            if (pizzas.TryGetValue(pizza, out int count))
            {
                pizzas[pizza] = --count;
                if (count == 0)
                {
                    pizzas.Remove(pizza);
                }
            }
        }

        public IEnumerator<Pizza> GetEnumerator()
        {
            foreach (var item in pizzas)
            {
                for (int i = 0; i < item.Value; i++)
                {
                    yield return item.Key;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
