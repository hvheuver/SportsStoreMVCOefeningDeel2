using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace SportsStore.Models.Domain
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Cart
    {
        #region Properties
        [JsonProperty]
        private IList<CartLine> _lines = new List<CartLine>();

        public IEnumerable<CartLine> CartLines => _lines.AsEnumerable();

        public int NumberOfLines => _lines.Count();

        public int NumberOfProducts => _lines.Sum(l => l.Quantity);

        public int TotalValue => _lines.Sum(l => l.Product.Price * l.Quantity);
        public int NumberOfItems => _lines.Count();

        #endregion

        #region Methods
        public void AddLine(Product product, int quantity)
        {
            CartLine line = _lines.SingleOrDefault(l => l.Product.Equals(product));
            if (line == null)
                _lines.Add(new CartLine { Product = product, Quantity = quantity });
            else line.Quantity += quantity;
        }

        public void RemoveLine(Product product)
        {
            CartLine line = _lines.SingleOrDefault(l => l.Product.Equals(product));
            if (line != null)
                _lines.Remove(line);
        }

        public void Clear()
        {
            _lines.Clear();
        }

        public void IncreaseQuantity(int productId)
        {
            CartLine line = GetCartLine(productId);
            if (line != null)
                line.Quantity++;
        }

        public void DecreaseQuantity(int productId)
        {
            CartLine line = GetCartLine(productId);
            if (line != null)
                line.Quantity--;
            if (line.Quantity <= 0)
                _lines.Remove(line);
        }

        private CartLine GetCartLine(int productId)
        {
            return _lines.SingleOrDefault(l => l.Product.ProductId == productId);
        }

        
        #endregion
    }
}