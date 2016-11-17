using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SportsStore.Models.Domain;

namespace SportsStore.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DbSet<Category> _categories;

        public CategoryRepository(ApplicationDbContext context)
        {
            _categories = context.Categories;
        }

        public IEnumerable<Category> GetAll()
        {
            return _categories.OrderBy(c => c.Name).AsNoTracking().ToList();
        }

        public Category GetById(int categoryId)
        {
            return _categories.Include(c=>c.Products).SingleOrDefault(c => c.CategoryId == categoryId);
        }

    }
}