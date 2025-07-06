using BlogStore.BusinessLayer.Abstract;
using BlogStore.EntityLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogStore.PresentationLayer.Controllers
{
    public class AdminController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;
        private readonly ICommentService _commentService;
        private readonly UserManager<AppUser> _userManager;

        public AdminController(IArticleService articleService, ICategoryService categoryService,
                              ICommentService commentService, UserManager<AppUser> userManager)
        {
            _articleService = articleService;
            _categoryService = categoryService;
            _commentService = commentService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Dashboard()
        {
          
            var categoryData = _categoryService.TGetCategoriesWithArticlesCount();
            ViewBag.CategoryChartData = categoryData.Select(x => new {
                name = x.CategoryName,
                value = x.ArticleCount
            }).ToList();

            
            var allComments = _commentService.TGetCommentsWithUser();
            var topCommenters = allComments
                .Where(c => c.AppUser != null)
                .GroupBy(c => c.AppUserId)
                .Select(g => new {
                    User = g.First().AppUser,
                    CommentCount = g.Count()
                })
                .OrderByDescending(x => x.CommentCount)
                .Take(4)
                .ToList();
            ViewBag.TopCommenters = topCommenters;
           
            var allAuthors = _articleService.TGetAllAuthorsWithArticles();
            var topAuthors = allAuthors
                .Select(author => new {
                    User = author,
                    ArticleCount = author.Articles?.Count ?? 0
                })
                .Where(x => x.ArticleCount > 0)
                .OrderByDescending(x => x.ArticleCount)
                .Take(5)
                .ToList();
            ViewBag.TopAuthors = topAuthors;

            // 💬 LATEST COMMENTS - En son 5 yorum
            var latestComments = _commentService.TGetLatestComments(5);
            ViewBag.LatestComments = latestComments;

            return View();
        }
    }
}