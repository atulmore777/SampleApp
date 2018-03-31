using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BJK.WEB.Controllers
{
    public class SingleViewController : Controller
    {
        // GET: SingleView
        public ActionResult Index()
        {
            return View();
        }
        // GET: SingleView
        public ActionResult MenuView()
        {
            return View();
        }
    }
}
