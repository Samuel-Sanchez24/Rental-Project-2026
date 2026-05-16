using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rental_Project_2026.Application.Contracts.Pagination;
using Rental_Project_2026.Application.UseCases.Branches.Queries.GetBranchesList;
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
        private readonly IWebHostEnvironment _hostEnvironment;

        public VehiclesController(INotyfService notyfService, IMediator mediator, IWebHostEnvironment hostEnvironment)
        {
            _notyfService = notyfService;
            _mediator = mediator;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index(
            int page = 1,
            int pageSize = PaginationRequest.DEFAULT_PAGE_SIZE,
            string? brandFilter = "",
            string? modelFilter = "",
            string? colorFilter = "",
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
                    FilterColor = colorFilter ?? string.Empty,
                    FilterStatus = statusFilter 
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
                    FilterColor = colorFilter ?? string.Empty,                  
                    FilterStatus = statusFilter
                };

                return View(viweModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            CreateVehicleDTO dto = new CreateVehicleDTO
            {
                Branches = await GetBranchesSelectListAsync()
            };

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateVehicleDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    dto.Branches = await GetBranchesSelectListAsync();

                    _notyfService.Error("Por favor corrige los errores en el formulario");
                    return View(dto);
                }
                
                string? imageUrl = await SaveVehicleImageAsync(dto.ImageFile);

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
                    ImageUrl = imageUrl
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
                    BranchId = dto.BranchId,
                    CurrentImageUrl = dto.ImageUrl,
                    Branches = await GetBranchesSelectListAsync()
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditVehicleDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _notyfService.Error("Por favor corrige los errores en el formulario");
                    return View(dto);
                }

                string? imageUrl = dto.CurrentImageUrl;

                if(dto.ImageFile != null && dto.ImageFile.Length > 0)
                {
                    imageUrl = await SaveVehicleImageAsync(dto.ImageFile);
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
                    BranchId = dto.BranchId,
                    ImageUrl = imageUrl
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

        //METODO PARA GUARDAR IMAGENES
        private async Task<string?> SaveVehicleImageAsync(IFormFile? imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return null;
            }

            string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".webp" };
            string extension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(extension))
            {
                throw new InvalidOperationException("El archivo debe ser una imagen válida: jpg, jpeg, png o webp.");
            }

            string uploadsFolder = Path.Combine(
                _hostEnvironment.WebRootPath,
                "uploads",
                "vehicles"
            );

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string fileName = $"{Guid.NewGuid()}{extension}";
            string filePath = Path.Combine(uploadsFolder, fileName);

            using FileStream fileStream = new FileStream(filePath, FileMode.Create);
            await imageFile.CopyToAsync(fileStream);

            return $"/uploads/vehicles/{fileName}";
        }

        private async Task<List<SelectListItem>> GetBranchesSelectListAsync()
        {
            GetBranchesListQuery query = new GetBranchesListQuery
            {
                Pagination = new PaginationRequest(1,25), 
                StatusFilter = BranchStatus.Active
            };

            PaginationResponse<BranchListItemDTO> branches = await _mediator.Send(query);

            return branches.Items
                .Select(branch => new SelectListItem
                {
                    Value = branch.Id.ToString(),
                    Text = $"{branch.Name} - {branch.City}"
                })
                .ToList();
        }
    }
}