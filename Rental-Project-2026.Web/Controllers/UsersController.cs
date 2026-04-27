using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Rental_Project_2026.Application.UseCases.Users.Commands.CreateUser;
using Rental_Project_2026.Application.UseCases.Users.Queries.GetUsersList;
using Rental_Project_2026.Application.UseCases.Users.Queries.GetUserById;
using Rental_Project_2026.Web.DTOs.Users;
using Rental_Project_2026.Application.UseCases.Users.Commands.Update_User;
using Rental_Project_2026.Application.UseCases.Users.Commands.ToggleUserStatus;

namespace Rental_Project_2026.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly INotyfService _notyfService;
        private readonly IMediator _mediator;

        public UsersController(INotyfService notyfService, IMediator mediator)
        {
            _notyfService = notyfService;
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _mediator.Send(new GetUsersListQuery());
                return View(response.Items);
            }
            catch (Exception ex)
            {
                _notyfService.Error($"Error al cargar los usuarios: {ex.Message}");
                return View(new List<UserListItemDTO>());
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _notyfService.Error("Debe corregir los errores de validacion");
                    return View(dto);
                }
                CreateUserCommand command = new CreateUserCommand
                {
                    Name = dto.Name,
                    Email = dto.Email,
                    PasswordHash = dto.Password,
                    Phone = dto.Phone,
                    Role = dto.Role,
                    Status = dto.Status
                };
                await _mediator.Send(command);
                _notyfService.Success("Usuario creado exitosamente");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyfService.Error($"Error al crear el usuario: {ex.Message}");
                return View(dto);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] Guid id)
        {
            try
            {
                UserDetailDTO user = await _mediator.Send(new GetUserByIdQuery(id));
                EditUserDTO editDto = new EditUserDTO
                {
                    Id = user.id,
                    Name = user.Name,
                    Email = user.Email,
                    Phone = user.Phone,
                    Role = user.Role,
                    Status = user.Status
                };
                return View(editDto);
            }
            catch (Exception ex)
            {
                _notyfService.Error($"Error al cargar el usuario: {ex.Message}");
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _notyfService.Error("Debe corregir los errores de validacion");
                    return View(dto);
                }
                UpdateUserCommand command = new UpdateUserCommand
                {
                    id = dto.Id,
                    Name = dto.Name,
                    Email = dto.Email,
                    Phone = dto.Phone,
                    Role = dto.Role,
                    Status = dto.Status
                };
                await _mediator.Send(command);
                _notyfService.Success("Usuario actualizado exitosamente");

            }
            catch (Exception ex)
            {
                _notyfService.Error($"Error al actualizar el usuario: {ex.Message}");
                return View(dto);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> ToggleStatus(Guid id)
        {
            try
            {
                await _mediator.Send(new ToggleUserStatusCommand { id = id });
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
    