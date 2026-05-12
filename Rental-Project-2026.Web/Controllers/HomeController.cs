using Microsoft.AspNetCore.Mvc;
using Rental_Project_2026.Web.Models;
using Rental_Project_2026.Web.Middlewares;

namespace Rental_Project_2026.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            string? message = HttpContext.Session.GetString(ExceptionHandlerMiddleware.ERROR_MESSAGE_SESSION_KEY);
            HttpContext.Session.Remove(ExceptionHandlerMiddleware.ERROR_MESSAGE_SESSION_KEY);

            return View(new ErrorViewModel { Message = message });
        }
    }
}
