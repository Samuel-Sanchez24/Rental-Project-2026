using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rental_Project_2026.Application.Contracts.Pagination;
using Rental_Project_2026.Application.Contracts.Security;
using Rental_Project_2026.Application.UseCases.Users.Commands.CreateUser;
using Rental_Project_2026.Application.UseCases.Users.Commands.DeleteUser;
using Rental_Project_2026.Application.UseCases.Users.Commands.Update_User;
using Rental_Project_2026.Application.UseCases.Users.Commands.UpdateUser;
using Rental_Project_2026.Application.UseCases.Users.Queries.GetRoleOptions;
using Rental_Project_2026.Application.UseCases.Users.Queries.GetUserById;
using Rental_Project_2026.Application.UseCases.Users.Queries.GetUsersList;
using Rental_Project_2026.Domain.Entities;
using Rental_Project_2026.Web.DTOs.Users;
using Rental_Project_2026.Web.Security;

namespace Rental_Project_2026.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly INotyfService _notifyService;
        private readonly IMediator _mediator;

        public UsersController(INotyfService notifyService, IMediator mediator)
        {
            _notifyService = notifyService;
            _mediator = mediator;
        }

        [HttpGet]
        [RequirePermission(PermissionCodesCatalog.SHOW_USERS)]
        public async Task<IActionResult> Index([FromQuery] int page = 1,
                                   [FromQuery] int pageSize = PaginationRequest.DEFAULT_PAGE_SIZE,
                                   [FromQuery] string? nameFilter = null,
                                   [FromQuery] Guid? roleIdFilter = null)
        {
            try
            {
                PaginationRequest paginationRequest = new PaginationRequest(page, pageSize);

                GetUsersListQuery query = new GetUsersListQuery
                {
                    Pagination = paginationRequest,
                    NameFilter = nameFilter,
                    RoleIdFilter = roleIdFilter,
                };

                PaginationResponse<UserListItemDTO> list = await _mediator.Send(query);
                IReadOnlyList<RoleOptionDTO> roles = await _mediator.Send(new GetRoleOptionsQuery());

                UsersIndexViewModel viewModel = new UsersIndexViewModel
                {
                    List = list,
                    FilterName = nameFilter ?? string.Empty,
                    FilterRoleId = roleIdFilter,
                    Roles = roles,
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _notifyService.Error($"Error al cargar los usuarios: {ex.Message}");
                return View(new UsersIndexViewModel
                {
                    List = PaginationResponse<UserListItemDTO>.Create([], 0, new PaginationRequest(1, pageSize)),
                    Roles = [],
                });
            }
        }

        [HttpGet]
        [RequirePermission(PermissionCodesCatalog.CREATE_USERS)]
        public async Task<IActionResult> Create()
        {
            await LoadRolesSelectListAsync();
            return View();
        }

        [HttpPost]
        [RequirePermission(PermissionCodesCatalog.CREATE_USERS)]
        public async Task<IActionResult> Create(CreateUserDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _notifyService.Error("Debe corregir los errores de validación.");
                    await LoadRolesSelectListAsync();
                    return View(dto);
                }

                CreateUserCommand command = new CreateUserCommand
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Email = dto.Email,
                    Password = "1234",
                    PhoneNumber = dto.Phone,
                    RoleId = dto.RoleId,
                };

                await _mediator.Send(command);
                _notifyService.Success("Usuario creado exitosamente.");
            }
            catch (Exception ex)
            {
                _notifyService.Error($"Error al crear el usuario: {ex.Message}");
                await LoadRolesSelectListAsync();
                return View(dto);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [RequirePermission(PermissionCodesCatalog.EDIT_USERS)]
        public async Task<IActionResult> Edit([FromRoute] string id)
        {
            try
            {
                UserDetailDTO user = await _mediator.Send(new GetUserByIdQuery { Id = id });

                EditUserDTO editDto = new EditUserDTO
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Phone = user.Phone,
                    RoleId = user.RoleId,
                };

                await LoadRolesSelectListAsync();
                return View(editDto);
            }
            catch (Exception ex)
            {
                _notifyService.Error($"Error al cargar el usuario: {ex.Message}");
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [RequirePermission(PermissionCodesCatalog.EDIT_USERS)]
        public async Task<IActionResult> Edit(EditUserDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _notifyService.Error("Debe corregir los errores de validación.");
                    await LoadRolesSelectListAsync();
                    return View(dto);
                }

                UpdateUserCommand command = new UpdateUserCommand
                {
                    Id = dto.Id,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Email = dto.Email,
                    Phone = dto.Phone,
                    Role = dto.RoleId,
                };

                await _mediator.Send(command);
                _notifyService.Success("Usuario actualizado exitosamente.");
            }
            catch (Exception ex)
            {
                _notifyService.Error($"Error al actualizar el usuario: {ex.Message}");
                await LoadRolesSelectListAsync();
                return View(dto);
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [RequirePermission(PermissionCodesCatalog.DELETE_USERS)]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            try
            {
                await _mediator.Send(new DeleteUserCommand { Id = id });
                _notifyService.Success("Usuario eliminado exitosamente.");
            }
            catch (Exception ex)
            {
                _notifyService.Error($"Error al eliminar el usuario: {ex.Message}");
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task LoadRolesSelectListAsync()
        {
            IReadOnlyList<RoleOptionDTO> roles = await _mediator.Send(new GetRoleOptionsQuery());
            ViewBag.Roles = new SelectList(roles, nameof(RoleOptionDTO.Id), nameof(RoleOptionDTO.Name));
        }
    }
}
