﻿using BlogStore.BusinessLayer.Abstract;
using BlogStore.EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BlogStore.PresentationLayer.Controllers
{

    public class WriterController : Controller
    {

        public IActionResult Index(string id)
        {
            ViewBag.i = id;
            return View();
        }
    }
}