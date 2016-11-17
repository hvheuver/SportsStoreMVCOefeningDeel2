using System.Collections.Generic;
using SportsStore.Models.Domain;

namespace SportsStore.Models.Domain
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
        Category GetById(int categoryId);
    }
}