using Newtonsoft.Json;

namespace SportsStore.Models.Domain
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CartLine
    {
        [JsonProperty]
        public Product Product { get; set; }
        [JsonProperty]
        public int Quantity { get; set; }
        public int Total => Product.Price * Quantity;
    }
}