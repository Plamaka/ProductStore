﻿using Microsoft.AspNetCore.Mvc;

namespace ProductStore.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
