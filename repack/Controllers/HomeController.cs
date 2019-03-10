using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using repack.Entities;
using repack.Models;

namespace repack.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(Db db)
        {
            var s = db.Stacks.ToList();
        }
        public IActionResult Index()
        {
           // var stack = db.Stacks.ToList();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}