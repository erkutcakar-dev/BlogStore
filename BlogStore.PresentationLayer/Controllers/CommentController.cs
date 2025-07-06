using BlogStore.BusinessLayer.Abstract;
using BlogStore.EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace BlogStore.PresentationLayer.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly UserManager<AppUser> _userManager;

        public CommentController(ICommentService commentService, UserManager<AppUser> userManager)
        {
            _commentService = commentService;
            _userManager = userManager;
        }

        // Tüm yorumları listele
        public IActionResult CommentList()
        {
            var value = _commentService.TGetAll();

            // TempData’dan varsa uyarıyı View’a gönder
            if (TempData["Warning"] != null)
                ViewBag.Warning = TempData["Warning"];

            return View(value);
        }

        // Yeni yorum formu göster
        [HttpGet]
        public IActionResult CreateComment()
        {
            return View();
        }

        // Yeni yorum ekle
        [HttpPost]
        public IActionResult CreateComment(Comment comment)
        {
            comment.CommentDate = DateTime.Now;
            comment.IsValid = false;

            _commentService.TInsert(comment);

            if (comment.IsToxic == true)
            {
                TempData["Warning"] = "Yorumunuz uygun bulunmadığı için işaretlendi.";
            }

            return RedirectToAction("CommentList");
        }

        // Yorum sil
        public IActionResult DeleteComment(int id)
        {
            _commentService.TDelete(id);
            return RedirectToAction("CommentList");
        }

        // Yorum güncelleme formu
        [HttpGet]
        public IActionResult UpdateComment(int id)
        {
            var value = _commentService.TGetById(id);
            return View(value);
        }

        // Yorum güncelle
        [HttpPost]
        public IActionResult UpdateComment(Comment comment)
        {
            comment.CommentDate = DateTime.Now;
            comment.IsValid = false;

            _commentService.TUpdate(comment);

            if (comment.IsToxic == true)
            {
                TempData["Warning"] = "Güncellenen yorum uygun bulunmadığı için işaretlendi.";
            }

            return RedirectToAction("CommentList");
        }

        // Toxic olarak işaretlenmiş yorumları listele
        public IActionResult ToxicComments()
        {
            var toxicComments = _commentService.TGetAll()
                .Where(c => c.IsToxic == true)
                .ToList();

            return View(toxicComments);
        }

        // ----------- AJAX ile yorum ekleme metodu --------------

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddCommentAjax(int articleId, string commentDetail)
        {
            try
            {
                if (string.IsNullOrEmpty(commentDetail) || commentDetail.Length < 3)
                {
                    return Json(new { success = false, message = "Yorum en az 3 karakter olmalıdır." });
                }

                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (user == null)
                {
                    return Json(new { success = false, message = "Kullanıcı bulunamadı." });
                }

                var comment = new Comment
                {
                    ArticleId = articleId,
                    CommentDetail = commentDetail,
                    CommentDate = DateTime.Now,
                    AppUserId = user.Id,
                    UserNameSurname = user.Name + " " + user.Surname
                };

                _commentService.TInsert(comment);

                var isToxic = comment.IsToxic == true;

                return Json(new
                {
                    success = true,
                    message = isToxic ? "Yorumunuz işaretlendi, lütfen kurallara dikkat edin." : "Yorum başarıyla eklendi.",
                    comment = comment.CommentDetail,
                    userName = comment.UserNameSurname,
                    date = comment.CommentDate.ToString("g"),
                    isToxic = isToxic
                });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Beklenmeyen bir hata oluştu." });
            }
        }
    }
}
