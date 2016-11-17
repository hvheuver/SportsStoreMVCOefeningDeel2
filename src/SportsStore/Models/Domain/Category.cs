using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Models.Domain
{
    public class Category
    {
        #region Properties
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; private set; }
        #endregion

        #region Constructor and Methods
        protected Category()
        {
            Products = new List<Product>();
        }

        public Category(string name) : this()
        {
            Name = name;
        }


        public void AddProduct(string name, int price, string description)
        {
            if (Products.FirstOrDefault(p => p.Name == name) == null)
                Products.Add(new Product(name, price,this,description));

        }

        public void RemoveProduct(Product product)
        {
           Products.Remove(product);
        }

        public Product FindProduct(string name)
        {
            return Products.FirstOrDefault(p => p.Name == name);
        }
        #endregion
    }

   
}
