using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportsStore.Models.Domain;
using SportsStore.Models.ViewModels;
using SportsStore.Helpers;


namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {

        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index(int categoryId = 0)
        {
            IEnumerable<Product> products;
            if (categoryId == 0)
                products = _productRepository.GetAll().OrderBy(b => b.Name);
            else
            {
                Category category = _categoryRepository.GetById(categoryId);
                products = category.Products.OrderBy(b => b.Name);
            }
            ViewData["Categories"] = GetCategoriesSelectList(categoryId);
            ViewData["CategoryId"] = categoryId;
            return View(products);
        }

        public IActionResult Edit(int id)
        {
            Product product = _productRepository.GetById(id);
            ViewData["Categories"] = GetCategoriesSelectList(product.Category.CategoryId);
            ViewData["Availabilities"] = product.Availability.ToSelectList();
            return View(new ProductEditViewModel(product));
        }

        [HttpPost]
        public IActionResult Edit(int id, ProductEditViewModel productEditViewModel)
        {
            Product product = _productRepository.GetById(id);
            MapToProduct(productEditViewModel, product);
            _productRepository.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            Product product = new Product();
            ViewData["Categories"] = GetCategoriesSelectList();
            ViewData["Availabilities"] = product.Availability.ToSelectList();
            return View(nameof(Edit), new ProductEditViewModel(product));
        }

        [HttpPost]
        public IActionResult Create(ProductEditViewModel productEditViewModel)
        {
            Product product = new Product();
            _productRepository.Add(product);
            MapToProduct(productEditViewModel, product);
            _productRepository.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            ViewData["ProductName"] = _productRepository.GetById(id).Name;
            return View();
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Product product = _productRepository.GetById(id);
            _productRepository.Delete(product);
            _productRepository.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private SelectList GetCategoriesSelectList(int selectedValue = 0)
        {
            return new SelectList(_categoryRepository.GetAll().OrderBy(g => g.Name),
                nameof(Category.CategoryId), nameof(Category.Name), selectedValue);
        }

        private void MapToProduct(ProductEditViewModel productEditViewModel, Product product)
        {
            product.Name = productEditViewModel.Name;
            product.Description = productEditViewModel.Description;
            product.Price = productEditViewModel.Price;
            product.InStock = productEditViewModel.InStock;
            product.Availability = productEditViewModel.Availability;
            product.Category = _categoryRepository.GetById(productEditViewModel.CategoryId);
        }
    }
}