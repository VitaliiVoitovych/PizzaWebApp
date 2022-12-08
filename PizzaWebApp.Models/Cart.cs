using PizzaWebApp.Models.Entities;
using System.Collections;

namespace PizzaWebApp.Models
{
    public class Cart : IEnumerable
    {
        private Dictionary<Pizza, int> pizzas;

        public Cart()
        {
            pizzas = new Dictionary<Pizza, int>();
        }

        public void AddItem(Pizza pizza)
        {
            if (pizzas.TryGetValue(pizza, out int count))
            {
                pizzas[pizza] = ++count;
            }
            else
            {
                pizzas.TryAdd(pizza, 1);
            }
        }

        public IEnumerator GetEnumerator()
        {
            foreach (var item in pizzas)
            {
                for (int i = 0; i < item.Value; i++)
                {
                    yield return item.Key;
                }
            }
        }
    }
}
