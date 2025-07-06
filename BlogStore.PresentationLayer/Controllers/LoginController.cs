using BlogStore.EntityLayer.Entities;
using BlogStore.PresentationLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlogStore.PresentationLayer.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signinManager;

        public LoginController(SignInManager<AppUser> signinManager)
        {
            _signinManager = signinManager;
        }

        public IActionResult UserLogin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UserLogin(UserLoginViewModel userLoginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userLoginViewModel);
            }

            var result = await _signinManager.PasswordSignInAsync(userLoginViewModel.Username, userLoginViewModel.Password, true, false);

            if (result.Succeeded)
            {
                // Giriş başarılı, kullanıcıyı Author/GetProfile sayfasına yönlendir
                return RedirectToAction("GetProfile", "Author");
            }

            ModelState.AddModelError("", "Kullanıcı adı veya parola yanlış.");
            return View(userLoginViewModel);
        }
    }
}
