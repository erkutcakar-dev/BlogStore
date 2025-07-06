using Microsoft.AspNetCore.Mvc;

namespace BlogStore.PresentationLayer.Controllers
{
    public class CategoryListController : Controller
    {
        public IActionResult CategoryList(int id)
        {
            ViewBag.i = id;
            return View();
        }
    }


}
