using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rental_Project_2026.Application.UseCases.Account.Commands.Login;
using Rental_Project_2026.Application.UseCases.Account.Commands.Logout;
using Rental_Project_2026.Web.DTOs.Account;

namespace Rental_Project_2026.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;
        private readonly INotyfService _notifyService;

        public AccountController(IMediator mediator, INotyfService notifyService)
        {
            _mediator = mediator;
            _notifyService = notifyService;
        }

        [HttpGet]
        public IActionResult Login([FromQuery] string? returnUrl = null)
        {
            return View(new LoginDTO { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _notifyService.Error("Debe corregir los errores de validación.");
                return View(dto);
            }

            try
            {
                LoginCommand command = new LoginCommand
                {
                    UserName = dto.Email,
                    Password = dto.Password,
                    RememberMe = dto.RememberMe,
                };

                AccountSignInResult result = await _mediator.Send(command);

                if (result.Succeeded)
                {
                    _notifyService.Success("Inicio de sesión exitoso.");

                    if (!string.IsNullOrEmpty(dto.ReturnUrl) && Url.IsLocalUrl(dto.ReturnUrl))
                    {
                        return Redirect(dto.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                if (result.IsLockedOut)
                {
                    _notifyService.Error("Su cuenta ha sido bloqueada temporalmente debido a múltiples intentos fallidos de inicio de sesión. Por favor, inténtelo de nuevo más tarde.");
                    return View(dto);
                }

                _notifyService.Error("Usuario o contraseña incorrectos.");
                return View(dto);
            }
            catch (Exception ex)
            {
                _notifyService.Error(ex.Message);
                return View(dto);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Logout()
        {
            try
            {
                await _mediator.Send(new LogoutCommand());
                _notifyService.Success("Cierre de sesión exitoso.");
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                _notifyService.Error(ex.Message);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View("Forbbiden");
        }
    }
}
