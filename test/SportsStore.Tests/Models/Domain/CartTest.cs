using SportsStore.Models.Domain;
using System.Linq;
using Xunit;

namespace SportsStore.Tests.Models.Domain
{
    public class CartTest
    {
        private Cart _cart;
        private Product _p1;
        private Product _p2;


        public CartTest()
        {
            Category category = new Category("Soccer");
            _p1 = new Product(1, "Product1", 10, category);
            _p2 = new Product(2, "Product2", 5, category);
            _cart = new Cart();
        }

        [Fact]
        public void NewCart_StartsEmpty()
        {
            Assert.Equal(0, _cart.NumberOfItems);
        }

        [Fact]
        public void Add_AddsProductToCart()
        {
            _cart.AddLine(_p1, 1);
            _cart.AddLine(_p2, 10);
            Assert.Equal(_cart.NumberOfItems, 2);
            Assert.Equal(_cart.CartLines.First(l => l.Product.Equals(_p1)).Quantity, 1);
            Assert.Equal(_cart.CartLines.First(l => l.Product.Equals(_p2)).Quantity, 10);
        }

        [Fact]
        public void Add_SameProduct_CartCombinesLinesWithSameProduct()
        {
            _cart.AddLine(_p1, 1);
            _cart.AddLine(_p2, 10);
            _cart.AddLine(_p1, 3);
            Assert.Equal(_cart.NumberOfItems, 2);
            Assert.Equal(_cart.CartLines.First(l => l.Product.Equals(_p1)).Quantity, 4);
            Assert.Equal(_cart.CartLines.First(l => l.Product.Equals(_p2)).Quantity, 10);
        }

        [Fact]
        public void RemoveLine_ProductInCart_RemovesProduct()
        {
            _cart.AddLine(_p1, 1);
            _cart.AddLine(_p2, 10);
            _cart.RemoveLine(_p2);
            Assert.Equal(_cart.NumberOfItems, 1);
            Assert.Equal(_cart.CartLines.First(l => l.Product.Equals(_p1)).Quantity, 1);
        }

        [Fact]
        public void Clear_ProductsInCart_ClearsCart()
        {
            _cart.AddLine(_p1, 1);
            _cart.AddLine(_p2, 10);
            _cart.AddLine(_p1, 3);
            _cart.Clear();
            Assert.Equal(_cart.NumberOfItems, 0);
        }

        [Fact]
        public void TotalValue_IsSumofAllCartLines()
        {
            _cart.AddLine(_p1, 1);
            _cart.AddLine(_p2, 10);
            _cart.AddLine(_p1, 3);
            Assert.Equal(_cart.TotalValue, 90);
        }
    }
}