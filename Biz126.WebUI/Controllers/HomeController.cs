using Microsoft.AspNetCore.Mvc;

namespace Biz126.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult About()
        {
            //ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "没什么联系方式。";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
