using BlogStore.BusinessLayer.Abstract;
using BlogStore.EntityLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace BlogStore.PresentationLayer.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;
        private readonly UserManager<AppUser> _userManager;

        public AuthorController(IArticleService articleService, ICategoryService categoryService, UserManager<AppUser> userManager)
        {
            _articleService = articleService;
            _categoryService = categoryService;
            _userManager = userManager;
        }

        // --- Mevcut metodlar burada (senin gönderdiğin tüm kodlar) ---

        public async Task<IActionResult> GetProfile()
        {
            var userProfile = await _userManager.FindByNameAsync(User.Identity.Name);
            if (userProfile == null)
            {
                return NotFound();
            }

            var userArticles = _articleService.TGetArticlesByAppUser(userProfile.Id);

            ViewBag.ArticleCount = userArticles.Count;
            return View(userProfile);
        }

        [HttpGet]
        public IActionResult CreateArticle()
        {
            List<SelectListItem> values = (from x in _categoryService.TGetAll()
                                           select new SelectListItem
                                           {
                                               Text = x.CategoryName,
                                               Value = x.CategoryId.ToString()
                                           }).ToList();
            ViewBag.v = values;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var userProfile = await _userManager.FindByNameAsync(User.Identity.Name);
            if (userProfile == null)
            {
                return NotFound();
            }

            return View(userProfile);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(AppUser model)
        {
            try
            {
                var userProfile = await _userManager.FindByNameAsync(User.Identity.Name);
                if (userProfile == null)
                {
                    return NotFound();
                }

                userProfile.Name = model.Name;
                userProfile.Surname = model.Surname;
                userProfile.Email = model.Email;
                userProfile.PhoneNumber = model.PhoneNumber;
                userProfile.Description = model.Description;
                userProfile.ImageUrl = model.ImageUrl;

                var result = await _userManager.UpdateAsync(userProfile);

                if (result.Succeeded)
                {
                    ViewBag.Success = "Profiliniz başarıyla güncellendi!";
                    return View(userProfile);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Bir hata oluştu: " + ex.Message;
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateArticle(Article article)
        {
            var userProfile = await _userManager.FindByNameAsync(User.Identity.Name);

            article.AppUserId = userProfile.Id;
            article.CreatedDate = DateTime.Now;
            _articleService.TInsert(article);
            return RedirectToAction("Index", "Default");
        }

        public async Task<IActionResult> MyArticleList()
        {
            var userProfile = await _userManager.FindByNameAsync(User.Identity.Name);
            var values = _articleService.TGetArticlesByAppUser(userProfile.Id);
            return View(values);
        }

        public IActionResult AuthorList()
        {
            var authors = _articleService.TGetAllAuthorsWithArticles();
            return View(authors);
        }

        public async Task<IActionResult> AuthorDetail(string id)
        {
            var author = await _userManager.FindByIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            var articles = _articleService.TGetArticlesByAppUser(id);
            ViewBag.AuthorName = author.Name + " " + author.Surname;
            ViewBag.AuthorDescription = author.Description;
            ViewBag.AuthorImage = author.ImageUrl;
            ViewBag.ArticleCount = articles.Count;
            return View(articles);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(string oldPassword, string newPassword, string confirmPassword)
        {
            if (string.IsNullOrWhiteSpace(oldPassword) || string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                TempData["Error"] = "Lütfen tüm şifre alanlarını doldurun.";
                return RedirectToAction("EditProfile");
            }

            if (newPassword != confirmPassword)
            {
                TempData["Error"] = "Yeni şifre ile tekrar şifresi eşleşmiyor.";
                return RedirectToAction("EditProfile");
            }

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                TempData["Error"] = "Kullanıcı bulunamadı.";
                return RedirectToAction("EditProfile");
            }

            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            if (result.Succeeded)
            {
                TempData["Success"] = "Şifreniz başarıyla güncellendi.";
            }
            else
            {
                TempData["Error"] = result.Errors.FirstOrDefault()?.Description ?? "Şifre güncellenirken bir hata oluştu.";
            }

            return RedirectToAction("EditProfile");
        }

        // --------------------------------------------
        // Aşağıda yeni eklenen metodlar: 
        // 1) Tüm yazarları 3'lü grid olarak listeleyen sayfa
        public IActionResult AuthorGridList()
        {
            var authors = _articleService.TGetAllAuthorsWithArticles();
            return View(authors);
        }

        // 2) Seçilen yazara ait blogları gösteren sayfa
        public async Task<IActionResult> AuthorBlogList(string id)
        {
            var author = await _userManager.FindByIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            var articles = _articleService.TGetArticlesByAppUser(id);
            ViewBag.AuthorName = author.Name + " " + author.Surname;
            ViewBag.AuthorDescription = author.Description;
            ViewBag.AuthorImage = author.ImageUrl;
            ViewBag.ArticleCount = articles.Count;

            return View(articles);
        }
    }
}
