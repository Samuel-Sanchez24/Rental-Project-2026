using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Rental_Project_2026.Application.Contracts.Pagination;
using Rental_Project_2026.Application.UseCases.Branches.Commands.ActiveBranch;
using Rental_Project_2026.Application.UseCases.Branches.Commands.CreateBranch;
using Rental_Project_2026.Application.UseCases.Branches.Commands.DeactivateBranch;
using Rental_Project_2026.Application.UseCases.Branches.Commands.DeleteBranch;
using Rental_Project_2026.Application.UseCases.Branches.Commands.UpdateBranch;
using Rental_Project_2026.Application.UseCases.Branches.Queries.GetBranchById;
using Rental_Project_2026.Application.UseCases.Branches.Queries.GetBranchesList;
using Rental_Project_2026.Web.DTOs.Branches;
    
namespace Rental_Project_2026.Web.Controllers
{
    public class BranchesController : Controller
    {
        private readonly INotyfService _notyfService;
        private readonly IMediator _mediator;

        public BranchesController(INotyfService notyfService, IMediator mediator)
        {
            _notyfService = notyfService;
            _mediator = mediator;
        }

        public async Task<IActionResult> Index(
            int page = 1,
            int pageSize = PaginationRequest.DEFAULT_PAGE_SIZE,
            string? nameFilter = null,
            string? cityFilter = null,
            BranchStatus? statusFilter = null)
        {
            try
            {
                PaginationRequest pagination = new PaginationRequest(page, pageSize);
                
                GetBranchesListQuery query = new GetBranchesListQuery
                {
                    Pagination = pagination,
                    NameFilter = nameFilter,
                    CityFilter = cityFilter,
                    StatusFilter = statusFilter
                };

                PaginationResponse<BranchListItemDTO> response = await _mediator.Send(query);

                BranchesIndexViewModel viewModel = new BranchesIndexViewModel
                {
                    List = response,
                    FilterName = nameFilter ?? string.Empty,
                    FilterCity = cityFilter ?? string.Empty,
                    FilterStatus = statusFilter
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _notyfService.Error($"Error al cargar las sucursales: {ex.Message}");

                BranchesIndexViewModel viewModel = new BranchesIndexViewModel
                {
                    List = PaginationResponse<BranchListItemDTO>.Create(
                        new List<BranchListItemDTO>(),
                        0,
                        new PaginationRequest(page, pageSize)),
                    FilterName = nameFilter ?? string.Empty,
                    FilterCity = cityFilter ?? string.Empty,
                    FilterStatus = statusFilter

                };
                return View(viewModel);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBranchDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _notyfService.Error("Debe corregir los errores de validacion");
                    return View(dto);
                }

                CreateBranchCommand command = new CreateBranchCommand
                {
                    Name = dto.Name,
                    City = dto.City,
                    Address = dto.Address,
                    Phone = dto.Phone,
                    Status = dto.Status
                };

                Guid NewBranchId = await _mediator.Send(command);
                _notyfService.Success("Sucurusal creada exitosamente!");
            }
            catch (Exception ex)
            {
                _notyfService.Error($"Error al crear la sucursal: {ex.Message}");
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] Guid id)
        {
            try
            {
                BranchDetailDTO dto = await _mediator.Send(new GetBranchByIdQuery(id));
                EditBranchDTO editDto = new EditBranchDTO
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    City = dto.City,
                    Address = dto.Address,
                    Phone = dto.Phone,
                    Status = dto.Status
                };
                return View(editDto);
            }
            catch (Exception ex)
            {
                _notyfService.Error($"Error al cargar la sucursal: {ex.Message}");
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditBranchDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _notyfService.Error("Debe corregir los errores de validacion");
                    return View(dto);
                }

                UpdateBranchCommand command = new UpdateBranchCommand
                {
                    Id= dto.Id,
                    Name = dto.Name,
                    City = dto.City,
                    Address = dto.Address,
                    Phone = dto.Phone,
                    Status = dto.Status
                };

                await _mediator.Send(command);
                _notyfService.Success("Sucurusal editada exitosamente!");
            }
            catch (Exception ex)
            {
                _notyfService.Error($"Error al editar la sucursal: {ex.Message}");
                return View(dto);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] Guid Id) 
        {
            try
            {
                await _mediator.Send(new DeleteBranchCommand { Id = Id });
                _notyfService.Success("Sucursal eliminada exitosamente");
            }
            catch (Exception ex)
            {

               _notyfService.Error($"Error al eliminar la sucursal: {ex.Message}");
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Activate([FromRoute] Guid Id)
        {
            try
            {
                await _mediator.Send(new ActivateBranchCommand { Id = Id });
                _notyfService.Success("Sucursal activada exitosamente");
            }
            catch (Exception ex)
            {

                _notyfService.Error($"Error al activar la sucursal: {ex.Message}");
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Deactivate([FromRoute] Guid Id)
        {
            try
            {
                await _mediator.Send(new DeactivateBranchCommand { Id = Id });
                _notyfService.Success("Sucursal desactivada exitosamente");
            }
            catch (Exception ex)
            {

                _notyfService.Error($"Error al desactivar la sucursal: {ex.Message}");
            }
            return RedirectToAction(nameof(Index));
        }


    }
}
