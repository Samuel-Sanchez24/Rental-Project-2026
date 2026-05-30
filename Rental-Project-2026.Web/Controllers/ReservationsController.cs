using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rental_Project_2026.Application.Contracts.Pagination;
using Rental_Project_2026.Application.Contracts.Security;
using Rental_Project_2026.Application.UseCases.Account.Queries.GetAccountUserInfo;
using Rental_Project_2026.Application.UseCases.Reservations.Commands.CancelledReservation;
using Rental_Project_2026.Application.UseCases.Reservations.Commands.CreateReservation;
using Rental_Project_2026.Application.UseCases.Reservations.Commands.UpdateReservation;
using Rental_Project_2026.Application.UseCases.Reservations.Queries.GetReservationById;
using Rental_Project_2026.Application.UseCases.Reservations.Queries.GetReservationList;
using Rental_Project_2026.Application.UseCases.Vehicles.Queries.GetVehicleById;
using Rental_Project_2026.Domain.Enums;
using Rental_Project_2026.Web.DTOs.Reservations;
using System.Security.Claims;

namespace Rental_Project_2026.Web.Controllers
{
    [Authorize]
    public class ReservationsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly INotyfService _notyfService;

        public ReservationsController(IMediator mediator, INotyfService notyfService)
        {
            _mediator = mediator;
            _notyfService = notyfService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(
            int page = 1,
            int pageSize = PaginationRequest.DEFAULT_PAGE_SIZE,
            Guid? vehicleIdFilter = null,
            Guid? branchIdFilter = null,
            ReservationStatus? statusFilter = null,
            DateTime? rentDateFromFilter = null,
            DateTime? rentDateToFilter = null)
        {
            try
            {
                string? userId = GetCurrentUserId();

                if (string.IsNullOrWhiteSpace(userId))
                {
                    _notyfService.Error("No se pudo identificar el usuario autenticado.");
                    return RedirectToAction("Login", "Account");
                }

                bool isAdmin = await IsCurrentUserAdminAsync();
                PaginationRequest pagination = new PaginationRequest(page, pageSize);

                GetReservationListQuery query = new GetReservationListQuery
                {
                    Pagination = pagination,
                    UserIdFilter = isAdmin ? null : userId,
                    VehicleIdFilter = vehicleIdFilter,
                    BranchIdFilter = branchIdFilter,
                    StatusFilter = statusFilter,
                    RentDateFromFilter = rentDateFromFilter,
                    RentDateToFilter = rentDateToFilter
                };

                PaginationResponse<ReservationListItemDTO> result = await _mediator.Send(query);

                ReservationIndexViewModel viewModel = new ReservationIndexViewModel
                {
                    List = result,
                    UserIdFilter = isAdmin ? null : userId,
                    VehicleIdFilter = vehicleIdFilter,
                    BranchIdFilter = branchIdFilter,
                    StatusFilter = statusFilter,
                    RentDateFromFilter = rentDateFromFilter,
                    RentDateToFilter = rentDateToFilter
                };

                ViewBag.IsAdmin = isAdmin;
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _notyfService.Error($"Ocurrió un error al cargar las reservas: {ex.Message}");

                ReservationIndexViewModel viewModel = new ReservationIndexViewModel
                {
                    List = PaginationResponse<ReservationListItemDTO>.Create(
                        new List<ReservationListItemDTO>(),
                        0,
                        new PaginationRequest(page, pageSize))
                };

                return View(viewModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                ReservationDetailDTO reservation = await _mediator.Send(new GetReservationByIdQuery(id));
                bool isAdmin = await IsCurrentUserAdminAsync();

                if (!isAdmin && !IsCurrentUserOwner(reservation.UserId))
                {
                    _notyfService.Error("No tienes acceso a esta reserva.");
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.IsAdmin = isAdmin;
                return View(reservation);
            }
            catch (Exception ex)
            {
                _notyfService.Error($"Ocurrió un error al cargar el detalle de la reserva: {ex.Message}");
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create(Guid vehicleId)
        {
            try
            {
                if (vehicleId == Guid.Empty)
                {
                    _notyfService.Error("El vehículo es requerido para crear una reserva.");
                    return RedirectToAction("Index", "Vehicles");
                }

                VehicleDetailDTO vehicle = await _mediator.Send(new GetVehicleByIdQuery(vehicleId));

                CreateReservationDTO dto = new CreateReservationDTO
                {
                    VehicleId = vehicle.Id,
                    VehiclePlate = vehicle.Plate,
                    VehicleBrand = vehicle.Brand,
                    VehicleModel = vehicle.Model,
                    VehicleImageUrl = vehicle.ImageUrl,
                    DailyPrice = vehicle.DailyPrice,
                    BranchName = vehicle.BranchName,
                    BranchCity = vehicle.BranchCity,
                    RentDate = DateTime.Now.Date,
                    ReturnDate = DateTime.Now.Date.AddDays(1)
                };

                return View(dto);
            }
            catch (Exception ex)
            {
                _notyfService.Error($"Ocurrió un error al cargar el formulario de reserva: {ex.Message}");
                return RedirectToAction("Index", "Vehicles");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateReservationDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    await ReloadVehicleDataAsync(dto);
                    _notyfService.Error("Por favor corrige los errores en el formulario.");
                    return View(dto);
                }

                string? userId = GetCurrentUserId();

                if (string.IsNullOrWhiteSpace(userId))
                {
                    _notyfService.Error("No se pudo identificar el usuario autenticado.");
                    return RedirectToAction("Login", "Account");
                }

                CreateReservationCommand command = new CreateReservationCommand
                {
                    VehicleId = dto.VehicleId,
                    UserId = userId,

                    RentDate = dto.RentDate,
                    ReturnDate = dto.ReturnDate,

                    CustomerFullName = dto.CustomerFullName,
                    DocumentNumber = dto.DocumentNumber,
                    PhoneNumber = dto.PhoneNumber,
                    Email = dto.Email,
                    BirthDate = dto.BirthDate,

                    DriverLicenseCategories = dto.DriverLicenseCategories,
                    DriverLicenseExpirationDate = dto.DriverLicenseExpirationDate,

                    RequiresSpecialAssistance = dto.RequiresSpecialAssistance,
                    AssistanceNotes = dto.AssistanceNotes
                };

                Guid reservationId = await _mediator.Send(command);

                _notyfService.Success("Reserva creada exitosamente. Queda pendiente de pago.");

                return RedirectToAction(nameof(Details), new { id = reservationId });
            }
            catch (Exception ex)
            {
                await ReloadVehicleDataAsync(dto);

                Console.WriteLine(ex.ToString());

                Exception realException = ex;

                while (realException.InnerException != null)
                {
                    realException = realException.InnerException;
                }

                _notyfService.Error($"Error real: {realException.Message}");

                return View(dto);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                ReservationDetailDTO reservation = await _mediator.Send(new GetReservationByIdQuery(id));
                bool isAdmin = await IsCurrentUserAdminAsync();

                if (!isAdmin && !IsCurrentUserOwner(reservation.UserId))
                {
                    _notyfService.Error("No tienes acceso para editar esta reserva.");
                    return RedirectToAction(nameof(Index));
                }

                if (!isAdmin && reservation.Status != ReservationStatus.Pending)
                {
                    _notyfService.Error("Solo se pueden editar reservas pendientes de pago.");
                    return RedirectToAction(nameof(Details), new { id = reservation.Id });
                }

                EditReservationDTO dto = new EditReservationDTO
                {
                    Id = reservation.Id,
                    VehicleId = reservation.VehicleId,
                    VehiclePlate = reservation.VehiclePlate,
                    VehicleBrand = reservation.VehicleBrand,
                    VehicleModel = reservation.VehicleModel,
                    BranchName = reservation.BranchName,
                    UserFullName = reservation.UserFullName,
                    UserEmail = reservation.UserEmail,
                    CustomerFullName = reservation.CustomerFullName,
                    DocumentNumber = reservation.DocumentNumber,
                    PhoneNumber = reservation.PhoneNumber,
                    Email = reservation.Email,
                    BirthDate = reservation.BirthDate,
                    DriverLicenseCategories = ParseDriverLicenseCategories(reservation.DriverLicenseCategories),
                    DriverLicenseExpirationDate = reservation.DriverLicenseExpirationDate,
                    RequiresSpecialAssistance = reservation.RequiresSpecialAssistance,
                    AssistanceNotes = reservation.AssistanceNotes,
                    RentDate = reservation.RentDate,
                    ReturnDate = reservation.ReturnDate,
                    Days = reservation.Days,
                    DailyPriceAtBooking = reservation.DailyPriceAtBooking,
                    TotalPrice = reservation.TotalPrice,
                    Status = reservation.Status,
                    CanEditAllFields = isAdmin
                };

                return View(dto);
            }
            catch (Exception ex)
            {
                _notyfService.Error($"Ocurrió un error al cargar la reserva: {ex.Message}");
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditReservationDTO dto)
        {
            try
            {
                bool isAdmin = await IsCurrentUserAdminAsync();

                if (!isAdmin)
                    RemoveAdminOnlyValidationEntries();

                if (!ModelState.IsValid)
                {
                    await ReloadReservationDataAsync(dto);
                    _notyfService.Error("Por favor corrige los errores en el formulario.");
                    return View(dto);
                }

                ReservationDetailDTO reservation = await _mediator.Send(new GetReservationByIdQuery(dto.Id));

                if (!isAdmin && !IsCurrentUserOwner(reservation.UserId))
                {
                    _notyfService.Error("No tienes acceso para editar esta reserva.");
                    return RedirectToAction(nameof(Index));
                }

                if (!isAdmin && reservation.Status != ReservationStatus.Pending)
                {
                    _notyfService.Error("Solo se pueden editar reservas pendientes de pago.");
                    return RedirectToAction(nameof(Details), new { id = reservation.Id });
                }

                UpdateReservationCommand command = new UpdateReservationCommand
                {
                    Id = dto.Id,
                    RentDate = dto.RentDate,
                    ReturnDate = dto.ReturnDate,
                    Status = isAdmin ? dto.Status : reservation.Status,
                    IsAdmin = isAdmin,
                    CustomerFullName = isAdmin ? dto.CustomerFullName : reservation.CustomerFullName,
                    DocumentNumber = isAdmin ? dto.DocumentNumber : reservation.DocumentNumber,
                    PhoneNumber = isAdmin ? dto.PhoneNumber : reservation.PhoneNumber,
                    Email = isAdmin ? dto.Email : reservation.Email,
                    BirthDate = isAdmin ? dto.BirthDate : reservation.BirthDate,
                    DriverLicenseCategories = isAdmin
                        ? dto.DriverLicenseCategories
                        : ParseDriverLicenseCategories(reservation.DriverLicenseCategories),
                    DriverLicenseExpirationDate = isAdmin
                        ? dto.DriverLicenseExpirationDate
                        : reservation.DriverLicenseExpirationDate,
                    RequiresSpecialAssistance = isAdmin
                        ? dto.RequiresSpecialAssistance
                        : reservation.RequiresSpecialAssistance,
                    AssistanceNotes = isAdmin ? dto.AssistanceNotes : reservation.AssistanceNotes
                };

                await _mediator.Send(command);

                _notyfService.Success("Reserva actualizada exitosamente.");

                return RedirectToAction(nameof(Details), new { id = dto.Id });
            }
            catch (Exception ex)
            {
                await ReloadReservationDataAsync(dto);
                _notyfService.Error($"Ocurrió un error al actualizar la reserva: {ex.Message}");
                return View(dto);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                ReservationDetailDTO reservation = await _mediator.Send(new GetReservationByIdQuery(id));

                if (!IsCurrentUserOwner(reservation.UserId))
                {
                    _notyfService.Error("No tienes acceso para cancelar esta reserva.");
                    return RedirectToAction(nameof(Index));
                }

                if (reservation.Status == ReservationStatus.Finished)
                {
                    _notyfService.Error("No se puede cancelar una reserva finalizada.");
                    return RedirectToAction(nameof(Details), new { id = reservation.Id });
                }

                if (reservation.Status == ReservationStatus.Cancelled)
                {
                    _notyfService.Error("La reserva ya se encuentra cancelada.");
                    return RedirectToAction(nameof(Details), new { id = reservation.Id });
                }

                await _mediator.Send(new CancelledReservationCommand(id));

                _notyfService.Success("Reserva cancelada exitosamente.");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyfService.Error($"Ocurrió un error al cancelar la reserva: {ex.Message}");
                return RedirectToAction(nameof(Index));
            }
        }

        private string? GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        private bool IsCurrentUserOwner(string reservationUserId)
        {
            string? currentUserId = GetCurrentUserId();

            return !string.IsNullOrWhiteSpace(currentUserId)
                && reservationUserId == currentUserId;
        }

        private async Task<bool> IsCurrentUserAdminAsync()
        {
            string? currentUserId = GetCurrentUserId();

            if (string.IsNullOrWhiteSpace(currentUserId))
                return false;

            UserAccountInfoDTO? user = await _mediator.Send(new GetAccountUserInfoQuery
            {
                UserId = currentUserId
            });

            return string.Equals(user?.RoleName, RolesCatalog.ADMIN, StringComparison.OrdinalIgnoreCase);
        }

        private void RemoveAdminOnlyValidationEntries()
        {
            ModelState.Remove(nameof(EditReservationDTO.CustomerFullName));
            ModelState.Remove(nameof(EditReservationDTO.DocumentNumber));
            ModelState.Remove(nameof(EditReservationDTO.PhoneNumber));
            ModelState.Remove(nameof(EditReservationDTO.Email));
            ModelState.Remove(nameof(EditReservationDTO.BirthDate));
            ModelState.Remove(nameof(EditReservationDTO.DriverLicenseCategories));
            ModelState.Remove(nameof(EditReservationDTO.DriverLicenseExpirationDate));
            ModelState.Remove(nameof(EditReservationDTO.RequiresSpecialAssistance));
            ModelState.Remove(nameof(EditReservationDTO.AssistanceNotes));
            ModelState.Remove(nameof(EditReservationDTO.Status));
        }

        private static List<string> ParseDriverLicenseCategories(string categories)
        {
            return categories
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .ToList();
        }

        private async Task ReloadVehicleDataAsync(CreateReservationDTO dto)
        {
            if (dto.VehicleId == Guid.Empty)
                return;

            VehicleDetailDTO vehicle = await _mediator.Send(new GetVehicleByIdQuery(dto.VehicleId));

            dto.VehiclePlate = vehicle.Plate;
            dto.VehicleBrand = vehicle.Brand;
            dto.VehicleModel = vehicle.Model;
            dto.VehicleImageUrl = vehicle.ImageUrl;
            dto.DailyPrice = vehicle.DailyPrice;
            dto.BranchName = vehicle.BranchName;
            dto.BranchCity = vehicle.BranchCity;
        }

        private async Task ReloadReservationDataAsync(EditReservationDTO dto)
        {
            if (dto.Id == Guid.Empty)
                return;

            ReservationDetailDTO reservation = await _mediator.Send(new GetReservationByIdQuery(dto.Id));

            dto.VehicleId = reservation.VehicleId;
            dto.VehiclePlate = reservation.VehiclePlate;
            dto.VehicleBrand = reservation.VehicleBrand;
            dto.VehicleModel = reservation.VehicleModel;
            dto.BranchName = reservation.BranchName;
            dto.UserFullName = reservation.UserFullName;
            dto.UserEmail = reservation.UserEmail;
            dto.DailyPriceAtBooking = reservation.DailyPriceAtBooking;
            dto.Status = reservation.Status;
            dto.CanEditAllFields = await IsCurrentUserAdminAsync();

            if (dto.ReturnDate.Date > dto.RentDate.Date)
            {
                dto.Days = (dto.ReturnDate.Date - dto.RentDate.Date).Days;
                dto.TotalPrice = dto.Days * dto.DailyPriceAtBooking;
            }
            else
            {
                dto.Days = reservation.Days;
                dto.TotalPrice = reservation.TotalPrice;
            }
        }
    }
}
