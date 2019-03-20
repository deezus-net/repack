using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
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
            _userModel = new UserModel(db, appSetting);
        }
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("~/stack/");
            }
            
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
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Type)
                };
                
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties();
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), authProperties);
                
                return Redirect("~/stack/");
            }
            else
            {
                ModelState.AddModelError("Id", "AuthId");
            }
            return View(vModel);
        }
        
    }
}