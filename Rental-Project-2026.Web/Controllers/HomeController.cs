using Microsoft.AspNetCore.Mvc;
using Rental_Project_2026.Web.Models;
using AppExceptionHandlerMiddleware = Rental_Project_2026.Web.Middlewares.ExceptionHandlerMiddleware;

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
            string? message = HttpContext.Session.GetString(AppExceptionHandlerMiddleware.ERROR_MESSAGE_SESSION_KEY);
            HttpContext.Session.Remove(AppExceptionHandlerMiddleware.ERROR_MESSAGE_SESSION_KEY);

            return View(new ErrorViewModel { Message = message });
        }
    }
}
