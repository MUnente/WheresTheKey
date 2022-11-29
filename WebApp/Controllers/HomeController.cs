using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
    [Authentication]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.RoleUser = HttpContext.Session.GetInt32("_Role");
            return View();
        }
    }
}

