using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Rental_Project_2026.Application.Utilities.Mediator;


namespace Rental_Project_2026.Application
{
    public static class ApplicationServicesRegistry
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IMediator, SimpleMediator>();

            services.Scan(scan => scan.FromAssembliesOf(typeof(IMediator))
                                      .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<>)))
                                      .AsImplementedInterfaces()
                                      .WithScopedLifetime()

                                      .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<,>)))
                                      .AsImplementedInterfaces()
                                      .WithScopedLifetime()

                                      .AddClasses(classes => classes.AssignableTo(typeof(IValidator<>)))
                                      .AsImplementedInterfaces()
                                      .WithScopedLifetime()
            );

            //// Branches Services UseCases
            //services.AddScoped<IRequestHandler<CreateBranchCommand, Guid>, CreateBranchUseCase>();
            //services.AddScoped<IRequestHandler<GetBranchesListQuery, PaginationResponse<BranchListItemDTO>>, GetBranchesListUseCase>();
            //services.AddScoped<IRequestHandler<UpdateBranchCommand>, UpdateBranchUseCase>();
            //services.AddScoped<IRequestHandler<GetBranchByIdQuery, BranchDetailDTO>, GetBranchByIdUseCase>();
            //services.AddScoped<IRequestHandler<DeleteBranchCommand>, DeleteBranchUseCase>();
            //services.AddScoped<IRequestHandler<ActivateBranchCommand>, ActivateBranchUseCase>();
            //services.AddScoped<IRequestHandler<DeactivateBranchCommand>, DeactivateBranchUseCase>();
            //// Branches Validators
            //services.AddValidatorsFromAssemblyContaining<CreateBranchCommandValidator>();

            ////Users Services UseCases
            //services.AddScoped<IRequestHandler<GetUserByIdQuery, UserDetailDTO>, GetUserByIdUseCase>();
            //services.AddScoped<IRequestHandler<CreateUserCommand, string>, CreateUserUseCase>();
            //services.AddScoped<IRequestHandler<DeleteUserCommand>, DeleteUserUseCase>();
            //services.AddScoped<IRequestHandler<GetUsersListQuery, PaginationResponse<UserListItemDTO>>, GetUsersListUseCase>();
            //services.AddScoped<IRequestHandler<UpdateUserCommand>, UpdateUserUseCase>();
            //services.AddScoped<IRequestHandler<ToggleUserStatusCommand>, ToggleUserStatusUseCase>();

            //services.AddScoped<IRequestHandler<LoginCommand, AccountSignInResult>, LoginUseCase>();
            //services.AddScoped<IRequestHandler<LogoutCommand>, LogoutUseCase>();
            //services.AddValidatorsFromAssemblyContaining<LoginCommandValidator>();

            ////Vehicles Services UseCases
            //services.AddScoped<IRequestHandler<CreateVehicleCommand, Guid>, CreateVehicleUseCase>();
            //services.AddScoped<IRequestHandler<GetVehiclesListQuery, PaginationResponse<VehicleListItemDTO>>, GetVehiclesListUseCase>();
            //services.AddScoped<IRequestHandler<UpdateVehicleCommand>, UpdateVehicleUseCase>();
            //services.AddScoped<IRequestHandler<GetVehicleByIdQuery, VehicleDetailDTO>, GetVehicleByIdUseCase>();
            //services.AddScoped<IRequestHandler<DeleteVehicleCommand>, DeleteVehicleUseCase>();
            //services.AddScoped<IRequestHandler<ChangeStatusVehicleCommand, Guid>, ChangeStatusVehicleUseCase>();
            ////Vehicles Validators
            //services.AddValidatorsFromAssemblyContaining<CreateVehicleCommandValidator>();




            return services;
        }
    }
}
