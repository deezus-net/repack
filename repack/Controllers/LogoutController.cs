using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using repack.Entities;
using repack.Models;
using repack.ViewModels;

namespace repack.Controllers
{
    public class LogoutController : Controller
    {
        public LogoutController()
        {
            
        }
        public async Task<IActionResult> Index()
        {
            await HttpContext.SignOutAsync();
            return Redirect("~/");
        }

       
        
    }
}