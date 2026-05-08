using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Rental_Project_2026.Application.Contracts.Pagination;
using Rental_Project_2026.Application.UseCases.Vehicles.Commands.ChangeStatusVehicle;
using Rental_Project_2026.Application.UseCases.Vehicles.Commands.CreateVehicle;
using Rental_Project_2026.Application.UseCases.Vehicles.Commands.DeleteVehicle;
using Rental_Project_2026.Application.UseCases.Vehicles.Commands.UpdateVehicle;
using Rental_Project_2026.Application.UseCases.Vehicles.Queries.GetVehicleById;
using Rental_Project_2026.Application.UseCases.Vehicles.Queries.GetVehicleList;
using Rental_Project_2026.Domain.Enums;
using Rental_Project_2026.Web.DTOs.Vehicles;

namespace Rental_Project_2026.Web.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly INotyfService _notyfService;
        private readonly IMediator _mediator;

        public VehiclesController(INotyfService notyfService, IMediator mediator)
        {
            _notyfService = notyfService;
            _mediator = mediator;
        }

        public async Task<IActionResult> Index(
            int page = 1,
            int pageSize = PaginationRequest.DEFAULT_PAGE_SIZE,
            string? brandFilter = null,
            string? modelFilter = null,
            string? colorFilter = null,
            decimal? dailyPriceFilter = null,
            VehicleStatus? statusFilter = null)
        {
            try
            {
                PaginationRequest pagination = new PaginationRequest(page, pageSize);

                GetVehiclesListQuery query = new GetVehiclesListQuery
                {
                    Pagination = pagination,
                    BrandFilter = brandFilter,
                    ModelFilter = modelFilter,
                    ColorFilter = colorFilter,
                    DailyPriceFilter = dailyPriceFilter,
                    StatusFilter = statusFilter
                };

                PaginationResponse<VehicleListItemDTO> result = await _mediator.Send(query);

                VehicleIndexViewModel viewModel = new VehicleIndexViewModel
                {
                    List = result,
                    FilterBrand = brandFilter ?? string.Empty,
                    FilterModel = modelFilter ?? string.Empty,
                    FilterColor = colorFilter ?? string.Empty,
                    FilterDailyPrice = dailyPriceFilter ?? 0,
                    FilterStatus = statusFilter ?? VehicleStatus.Available
                };
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _notyfService.Error($"Ocurrio un error al cargar los vehiculos: {ex.Message}");

                VehicleIndexViewModel viweModel = new VehicleIndexViewModel
                {
                    List = PaginationResponse<VehicleListItemDTO>.Create
                    (new List<VehicleListItemDTO>(), 0, new PaginationRequest(page, pageSize)),

                    FilterBrand = brandFilter ?? string.Empty,
                    FilterModel = modelFilter ?? string.Empty,
                    FilterColor = colorFilter ?? string.Empty,
                    FilterDailyPrice = dailyPriceFilter ?? 0,
                    FilterStatus = statusFilter ?? VehicleStatus.Available
                };

                return View(viweModel);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateVehicleDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _notyfService.Error("Por favor corrige los errores en el formulario");
                    return View(dto);
                }

                CreateVehicleCommand command = new CreateVehicleCommand
                {
                    Plate = dto.Plate,
                    Brand = dto.Brand,
                    Model = dto.Model,
                    Color = dto.Color,
                    DailyPrice = dto.DailyPrice,
                    Year = dto.Year,
                    Status = dto.Status,
                    BranchId = dto.BranchId,
                };

                Guid NewVehicleId = await _mediator.Send(command);
                _notyfService.Success("Vehiculo creado exitosamente");

            }
            catch (Exception ex)
            {
                _notyfService.Error($"Ocurrio un error al crear el vehiculo: {ex.Message}");
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] Guid Id)
        {
            try
            {
                VehicleDetailDTO dto = await _mediator.Send(new GetVehicleByIdQuery(Id));
                EditVehicleDTO editDto = new EditVehicleDTO
                {
                    Id = dto.Id,
                    Plate = dto.Plate,
                    Brand = dto.Brand,
                    Model = dto.Model,
                    Color = dto.Color,
                    Year = dto.Year,
                    DailyPrice = dto.DailyPrice,
                    Status = dto.Status,
                    BranchId = dto.BranchId
                };
                return View(editDto);
            }
            catch (Exception ex)
            {
                _notyfService.Error($"Ocurrio un error al cargar el vehiculo: {ex.Message}");
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditVehicleDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _notyfService.Error("Por favor corrige los errores en el formulario");
                    return View(dto);
                }

                UpdateVehicleCommand command = new UpdateVehicleCommand
                {
                    Id = dto.Id,
                    Plate = dto.Plate,
                    Brand = dto.Brand,
                    Model = dto.Model,
                    Color = dto.Color,
                    Year = dto.Year,
                    DailyPrice = dto.DailyPrice,
                    Status = dto.Status,
                    BranchId = dto.BranchId
                };

                await _mediator.Send(command);
                _notyfService.Success("Vehiculo actualizado exitosamente");
            }
            catch (Exception ex)
            {
                _notyfService.Error($"Ocurrio un error al actualizar el vehiculo: {ex.Message}");
                return View(dto);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] Guid Id)
        {
            try
            {
                await _mediator.Send(new DeleteVehicleCommand { Id = Id });
                _notyfService.Success("Vehiculo eliminado exitosamente");
            }
            catch (Exception ex)
            {
                _notyfService.Error($"Ocurrio un error al eliminar el vehiculo: {ex.Message}");
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStatus([FromRoute] Guid Id, [FromForm] VehicleStatus NewStatus)
        {
            try
            {
                await _mediator.Send(new ChangeStatusVehicleCommand(Id, NewStatus));
                _notyfService.Success("Estado del vehiculo actualizado exitosamente");
            }
            catch (Exception ex)
            {
                _notyfService.Error($"Ocurrio un error al actualizar el estado del vehiculo: {ex.Message}");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}