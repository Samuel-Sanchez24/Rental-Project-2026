using FluentValidation;
using FluentValidation.Results;
using Rental_Project_2026.Application.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Rental_Project_2026.Application.Utilities.Mediator
{
    public class SimpleMediator : IMediator
    {
        private readonly IServiceProvider _serviceProvider;

        public SimpleMediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
        {

            await ValidateRequestAsync(request).ConfigureAwait(false);

            Type useCaseType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));

            var useCase = _serviceProvider.GetService(useCaseType);

            if (useCase is null)
            {
                throw new MediatorException($"No se encontró un handler para {request.GetType().Name}");
            }

            MethodInfo method = useCaseType.GetMethod("Handle")!;

            return await (Task<TResponse>)method.Invoke(useCase, new object[] { request })!;
        }

        public async Task Send(IRequest request)
        {
            Type useCaseType = typeof(IRequestHandler<>).MakeGenericType(request.GetType());

            var useCase = _serviceProvider.GetService(useCaseType);

            if (useCase is null)
            {
                throw new MediatorException($"No se encontró un handler para {request.GetType().Name}");
            }

            MethodInfo? method = useCaseType.GetMethod("Handle")
                             ?? useCaseType.GetMethod("Handler");
            if (method is null)
            {
                throw new MediatorException($"No se encontró un método Handle o Handler para {request.GetType().Name}");
            }

            await (Task)method.Invoke(useCase, new object[] { request })!;
        }

        private async Task ValidateRequestAsync(object request)
        {
            Type RequestType = request.GetType();
            Type ValidatorInterface = typeof(IValidator<>).MakeGenericType(RequestType);
            object? validator = _serviceProvider.GetService(ValidatorInterface);

            if (validator is null)
            {
                return;
            }

            MethodInfo? validateMethod = ValidatorInterface.GetMethod("ValidateAsync", new[] { RequestType, typeof(CancellationToken) });
            if (validateMethod is null)
            {
                return;
            }
            object? validationResult = validateMethod.Invoke(validator, new object[] { request, CancellationToken.None });

            if (validationResult is not Task task)
            {
                return;
            }
            await task.ConfigureAwait(false);

            PropertyInfo? resultProperty = task.GetType().GetProperty("Result");
            if (resultProperty?.GetValue(task) is not FluentValidation.Results.ValidationResult result)
            {
                return;
            }

            if (!result.IsValid)
            {
                throw new CustomValidationException(result);
            }
        }
    }
}
