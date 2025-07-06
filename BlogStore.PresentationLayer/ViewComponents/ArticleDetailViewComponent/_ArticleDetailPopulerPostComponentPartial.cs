using BlogStore.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BlogStore.PresentationLayer.ViewComponents.ArticleDetailViewComponent
{
    public class _ArticleDetailPopulerPostComponentPartial : ViewComponent
    {
        private readonly IArticleService _articleService;

        public _ArticleDetailPopulerPostComponentPartial(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public IViewComponentResult Invoke()
        {
            var values = _articleService.TGetTop3PopularArticles();
            return View(values);
        }
    }
}
