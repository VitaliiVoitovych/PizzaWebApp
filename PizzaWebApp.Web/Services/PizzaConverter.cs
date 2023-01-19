using PizzaWebApp.Models;
using PizzaWebApp.Models.Entities;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PizzaWebApp.Web.Services
{
    public class PizzaConverter : JsonConverter<Pizza>
    {
        public override Pizza? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var pizzaName = "Undefined";
            double weight = default;
            decimal price = default;
            PizzaSize size = default;

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var propertyName = reader.GetString();
                    reader.Read();
                    switch (propertyName?.ToLower())
                    {
                        case "name" when reader.GetString() != string.Empty:
                            string? name = reader.GetString();
                            pizzaName = name ?? pizzaName; 
                            break;
                        case "weight" when reader.TokenType == JsonTokenType.Number:
                            weight = reader.GetDouble();
                            break;
                        case "price" when reader.TokenType == JsonTokenType.Number:
                            price = reader.GetDecimal();
                            break;
                        case "size":
                            size = (PizzaSize)reader.GetInt32();
                            break;
                        default:
                            return null; // ???
                    }
                }
            }
            return new Pizza() { Name = pizzaName, Weight = weight, Price = price, Size = size };
        }

        public override void Write(Utf8JsonWriter writer, Pizza value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteNumber("id", value.PizzaId);
            writer.WriteString("name", value.Name);
            writer.WriteNumber("weight", value.Weight);
            writer.WriteNumber("size", (int)value.Size);
            writer.WriteNumber("weight", value.Price);

            writer.WriteEndObject();
        }
    }
}
