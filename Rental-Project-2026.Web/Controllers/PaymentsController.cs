using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rental_Project_2026.Application.UseCases.Payments.Commands.CreatePaymentForReservation;
using Rental_Project_2026.Application.UseCases.Payments.Commands.UpdatePaymentStatus;
using Rental_Project_2026.Application.UseCases.Payments.Queries.GetPaymentById;
using Rental_Project_2026.Domain.Enums;
using Rental_Project_2026.Web.DTOs.Payments;
using System.Security.Claims;

namespace Rental_Project_2026.Web.Controllers
{
    [Authorize]
    public class PaymentsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly INotyfService _notyfService;

        public PaymentsController(IMediator mediator, INotyfService notyfService)
        {
            _mediator = mediator;
            _notyfService = notyfService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateForReservation(Guid reservationId)
        {
            try
            {
                string? userId = GetCurrentUserId();

                if (string.IsNullOrWhiteSpace(userId))
                {
                    _notyfService.Error("No se pudo identificar el usuario autenticado.");
                    return RedirectToAction("Login", "Account");
                }

                CreatePaymentForReservationResult result = await _mediator.Send(
                    new CreatePaymentForReservationCommand
                    {
                        ReservationId = reservationId,
                        UserId = userId
                    });

                if (string.IsNullOrWhiteSpace(result.PaymentUrl))
                {
                    _notyfService.Error("No fue posible obtener la URL del pago.");
                    return RedirectToAction("Details", "Reservations", new { id = reservationId });
                }

                _notyfService.Success(result.ReusedExistingPayment
                    ? "Se reutilizó el pago pendiente."
                    : "Pago inicializado correctamente.");

                return Redirect(result.PaymentUrl);
            }
            catch (Exception ex)
            {
                _notyfService.Error($"Ocurrió un error al iniciar el pago: {ex.Message}");
                return RedirectToAction("Details", "Reservations", new { id = reservationId });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Checkout(Guid paymentId)
        {
            try
            {
                PaymentDetailDTO payment = await _mediator.Send(new GetPaymentByIdQuery(paymentId));

                if (!IsCurrentUserOwner(payment.UserId))
                {
                    _notyfService.Error("No tienes acceso a este pago.");
                    return RedirectToAction("Index", "Reservations");
                }

                if (!payment.IsMockProvider || payment.Status != PaymentStatus.Pending)
                    return RedirectToAction(nameof(Result), new { paymentId });

                return View(payment);
            }
            catch (Exception ex)
            {
                _notyfService.Error($"Ocurrió un error al cargar el pago: {ex.Message}");
                return RedirectToAction("Index", "Reservations");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompleteMockPayment(Guid paymentId, PaymentStatus status)
        {
            try
            {
                await _mediator.Send(new UpdatePaymentStatusCommand
                {
                    PaymentId = paymentId,
                    Status = status,
                    CurrentUserId = GetCurrentUserId()
                });

                if (status == PaymentStatus.Failed)
                    return RedirectToAction(nameof(Error), new { paymentId });

                if (status == PaymentStatus.Pending)
                    return RedirectToAction(nameof(Pending), new { paymentId });

                return RedirectToAction(nameof(Result), new { paymentId });
            }
            catch (Exception ex)
            {
                _notyfService.Error($"Ocurrió un error al procesar el pago: {ex.Message}");
                return RedirectToAction(nameof(Checkout), new { paymentId });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Result(Guid paymentId)
        {
            try
            {
                PaymentDetailDTO payment = await _mediator.Send(new GetPaymentByIdQuery(paymentId));

                if (!IsCurrentUserOwner(payment.UserId))
                {
                    _notyfService.Error("No tienes acceso a este pago.");
                    return RedirectToAction("Index", "Reservations");
                }

                return View(payment);
            }
            catch (Exception ex)
            {
                _notyfService.Error($"Ocurrió un error al cargar el resultado del pago: {ex.Message}");
                return RedirectToAction("Index", "Reservations");
            }
        }

        [HttpGet]
        public IActionResult Pending(Guid paymentId)
        {
            return RedirectToAction(nameof(Result), new { paymentId });
        }

        [HttpGet]
        public IActionResult Error(Guid paymentId)
        {
            return RedirectToAction(nameof(Result), new { paymentId });
        }

        [AllowAnonymous]
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Webhook([FromBody] PaymentWebhookDTO dto)
        {
            try
            {
                await _mediator.Send(new UpdatePaymentStatusCommand
                {
                    ProviderReference = dto.ProviderReference,
                    Status = dto.Status,
                    ValidateProviderCallback = true,
                    Signature = dto.Signature,
                    Payload = dto.Payload
                });

                return Ok(new { ok = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ok = false, error = ex.Message });
            }
        }

        private string? GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        private bool IsCurrentUserOwner(string paymentUserId)
        {
            string? currentUserId = GetCurrentUserId();

            return !string.IsNullOrWhiteSpace(currentUserId) &&
                   paymentUserId == currentUserId;
        }
    }
}
