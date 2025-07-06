using BlogStore.BusinessLayer.Abstract;
using BlogStore.EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BlogStore.PresentationLayer.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IArticleService _articleService;

        // IArticleService eklendi blogları çekebilmek için
        public CategoryController(ICategoryService categoryService, IArticleService articleService)
        {
            _categoryService = categoryService;
            _articleService = articleService;
        }

        public IActionResult CategoryList()
        {
            var values = _categoryService.TGetAll();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            _categoryService.TInsert(category);
            return RedirectToAction("CategoryList");
        }

        public IActionResult DeleteCategory(int id)
        {
            _categoryService.TDelete(id);
            return RedirectToAction("CategoryList");
        }

        [HttpGet]
        public IActionResult UpdateCategory(int id)
        {
            var value = _categoryService.TGetById(id);
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateCategory(Category category)
        {
            _categoryService.TUpdate(category);
            return RedirectToAction("CategoryList");
        }

        // --------- Yeni eklenen methodlar ---------

        // Kategorileri 3’lü grid olarak listeleyen action
        public IActionResult CategoryGridList()
        {
            var categories = _categoryService.TGetAll().ToList();
            return View(categories);
        }

        // Kategoriye ait blogları listeleyen action
        public IActionResult BlogListByCategory(int categoryId)
        {
            var blogs = _articleService.TGetAll()
                .Where(a => a.CategoryId == categoryId)
                .ToList();

            var category = _categoryService.TGetById(categoryId);
            ViewBag.CategoryName = category?.CategoryName ?? "Kategori bulunamadı";

            return View(blogs);
        }
    }
}
