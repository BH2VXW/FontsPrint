using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Biz126.WebUI.Controllers
{
    public class FontsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}