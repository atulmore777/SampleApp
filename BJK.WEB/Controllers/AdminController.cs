﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BJK.WEB.Controllers
{
    public class AdminController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SingleView()
        {
            return View();
        }

        public ActionResult UserView()
        {
            return View();
        }

        public ActionResult Partials(string id)
        {
            return PartialView("~/Views/Shared/AdminPartials/" + id + ".cshtml");
        }
    }
}
