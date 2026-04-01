using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Rental_Project_2026.Application.UseCases.Branches.Commands;
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

        public IActionResult Index()
        {
            return View();
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
    }
}
