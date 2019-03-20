using Microsoft.AspNetCore.Mvc;
using repack.ViewModels;

namespace repack.Controllers
{
    public class HelpController : Controller
    {
        // GET
        public IActionResult Index()
        {
            var vModel = new HelpViewModel();
            return View(vModel);
        }
    }
}