using Microsoft.AspNetCore.Mvc;

namespace BlogStore.PresentationLayer.ViewComponents.AdminViewComponent
{
    public class _AdminHeadPartialComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();


        }
    }
}
