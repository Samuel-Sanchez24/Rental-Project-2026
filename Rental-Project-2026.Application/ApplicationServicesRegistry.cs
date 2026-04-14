using Microsoft.Extensions.DependencyInjection;
using Rental_Project_2026.Application.UseCases.Branches.Commands.ActiveBranch;
using Rental_Project_2026.Application.UseCases.Branches.Commands.CreateBranch;
using Rental_Project_2026.Application.UseCases.Branches.Commands.DeactivateBranch;
using Rental_Project_2026.Application.UseCases.Branches.Commands.DeleteBranch;
using Rental_Project_2026.Application.UseCases.Branches.Commands.UpdateBranch;
using Rental_Project_2026.Application.UseCases.Branches.Queries.GetBranchById;
using Rental_Project_2026.Application.UseCases.Branches.Queries.GetBranchesList;
using Rental_Project_2026.Application.Utilities.Mediator;

namespace Rental_Project_2026.Application
{
    public static class ApplicationServicesRegistry
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IMediator, SimpleMediator>();

            services.AddScoped<IRequestHandler<CreateBranchCommand, Guid>, CreateBranchUseCase>();
            services.AddScoped<IRequestHandler<GetBranchesListQuery, IEnumerable<BranchListItemDTO>>, GetBranchesListUseCase>();
            services.AddScoped<IRequestHandler<UpdateBranchCommand>, UpdateBranchUseCase>();
            services.AddScoped<IRequestHandler<GetBranchByIdQuery, BranchDetailDTO>, GetBranchByIdUseCase>();
            services.AddScoped<IRequestHandler<DeleteBranchCommand>, DeleteBranchUseCase>();
            services.AddScoped<IRequestHandler<ActivateBranchCommand>, ActivateBranchUseCase>();
            services.AddScoped<IRequestHandler<DeactivateBranchCommand>, DeactivateBranchUseCase>();

            return services;
        }
    }
}
