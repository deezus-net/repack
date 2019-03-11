using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using repack.Entities;
using repack.Models;
using repack.ViewModels;

namespace repack.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserModel _userModel;
        public HomeController(Db db, AppSetting appSetting)
        {
            _userModel = new UserModel(db, appSetting.Salt);
        }
        public IActionResult Index()
        {
            var vModel = new LoginViewModel();
            return View(vModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel vModel)
        {
            if (!ModelState.IsValid) return View(vModel);
            var user = await _userModel.Login(vModel.Id, vModel.Password);
            if (user != null)
            {
                return Redirect("~/stack/");
            }
            return View(vModel);
        }
        
    }
}