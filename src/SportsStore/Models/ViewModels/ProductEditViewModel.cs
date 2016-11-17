using SportsStore.Models.Domain;

namespace SportsStore.Models.ViewModels
{
    public class ProductEditViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public bool InStock { get; set; }
        public Availability Availability { get; set; }
        public int CategoryId { get; set; }

        public ProductEditViewModel()
        {
            
        }

        public ProductEditViewModel(Product p)
        {
            ProductId = p.ProductId;
            Name = p.Name;
            Description = p.Description;
            Price = p.Price;
            InStock = p.InStock;
            Availability = p.Availability;
            CategoryId = p.Category.CategoryId;
        }
    }
}