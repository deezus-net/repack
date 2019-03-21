using System;
using Microsoft.AspNetCore.Mvc;

namespace repack.Controllers
{
    public class TokenController : Controller
    {
        // GET
        public string Index()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}