using BlogStore.BusinessLayer.Abstract;
using BlogStore.DataAccessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BlogStore.PresentationLayer.ViewComponents
{
    public class _ArticleDetailCategoryListComponentPartial : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public _ArticleDetailCategoryListComponentPartial(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IViewComponentResult Invoke()
        {
            var value = _categoryService.TGetCategoriesWithArticlesCount();
            return View(value);
        }
    }
}
