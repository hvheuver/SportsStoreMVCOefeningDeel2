using SportsStore.Models.Domain;
using Xunit;

namespace SportsStore.Tests.Models.Domain
{
    public class CategoryTest
    {
        private Category _category;

        public CategoryTest()
        {
            _category = new Category("Soccer");
        }

        [Fact]
        public void Category_StartsEmpty()
        {
            Assert.Equal(0, _category.Products.Count);
        }

        [Fact]
        public void Add_NewProduct_AddsProduct()
        {
            _category.AddProduct("Football", 10, null);
            Assert.Equal(_category.Products.Count, 1);
        }

       [Fact]
        public void Add_ProductInCategory_DoesnotAddProduct()
        {
            _category.AddProduct("Football", 10, null);
            _category.AddProduct("Football", 10, null);
            Assert.Equal(_category.Products.Count, 1);
        }

        [Fact]
        public void Remove_RemovesProduct()
        {
            _category.AddProduct("Football", 10, null);
            _category.RemoveProduct(_category.FindProduct("Football"));
            Assert.Equal(0, _category.Products.Count);
        }

        [Fact]
        public void FindProduct_ProductInCategory_ReturnsProduct()
        {
            _category.AddProduct("Football", 10, null);
            Assert.NotNull(_category.FindProduct("Football"));
        }

        [Fact]
        public void FindProduct_ProductNotInCategory_ReturnsNull()
        {
            Assert.Null(_category.FindProduct("Football"));
        }
    }
}
