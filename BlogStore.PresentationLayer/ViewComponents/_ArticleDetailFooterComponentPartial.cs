﻿using Microsoft.AspNetCore.Mvc;

namespace BlogStore.PresentationLayer.ViewComponents
{
    public class _ArticleDetailFooterComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
