using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rental_Project_2026.Api.DTO_s;
using Rental_Project_2026.Application.Contracts.Pagination;
using Rental_Project_2026.Application.Contracts.Security;
using Rental_Project_2026.Application.UseCases.Branches.Commands.ActiveBranch;
using Rental_Project_2026.Application.UseCases.Branches.Commands.CreateBranch;
using Rental_Project_2026.Application.UseCases.Branches.Commands.DeactivateBranch;
using Rental_Project_2026.Application.UseCases.Branches.Commands.DeleteBranch;
using Rental_Project_2026.Application.UseCases.Branches.Commands.UpdateBranch;
using Rental_Project_2026.Application.UseCases.Branches.Queries.GetBranchById;
using Rental_Project_2026.Application.UseCases.Branches.Queries.GetBranchesList;

namespace Rental_Project_2026.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BranchesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
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

               return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al cargar las sucursales: {ex.Message}");
    
            };
        }



        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBranchDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                   return StatusCode(StatusCodes.Status400BadRequest, ModelState);
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
                return StatusCode(StatusCodes.Status201Created, NewBranchId);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Edit([FromRoute] Guid id, [FromBody] EditBranchDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }

                UpdateBranchCommand command = new UpdateBranchCommand
                {
                    Id = id,
                    Name = dto.Name,
                    City = dto.City,
                    Address = dto.Address,
                    Phone = dto.Phone,
                    Status = dto.Status
                };

                await _mediator.Send(command);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

               


        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _mediator.Send(new DeleteBranchCommand { Id = id });
            return StatusCode(StatusCodes.Status204NoContent);
        }

       
    }
}
