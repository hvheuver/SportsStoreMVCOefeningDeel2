using System.Collections.Generic;

namespace SportsStore.Models.Domain
{
    public interface IProductRepository
    {
         IEnumerable<Product> GetAll();
         IEnumerable<Product> GetByAvailability(IEnumerable<Availability> availabilities);
         Product GetById(int productId);
         void Add(Product product);
        void Delete(Product product);
        void SaveChanges();
    }
}
