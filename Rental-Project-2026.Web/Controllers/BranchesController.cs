using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Rental_Project_2026.Application.UseCases.Branches.Commands.CreateBranch;
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

        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<BranchListItemDTO> list = await _mediator.Send(new GetBranchesListQuery());
                return View(list);

            }
            catch (Exception ex)
            {
                _notyfService.Error($"Error al cargar las sucursales: {ex.Message}");
                return View(new List<BranchListItemDTO>());
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

    }
}
