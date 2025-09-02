using DientesLimpios.Application.Exceptions;
using FluentValidation;
using FluentValidation.Results;

namespace DientesLimpios.Application.Utilities.Mediator
{
    public class MediatorSimple : IMediator
    {
        private readonly IServiceProvider _serviceProvider;

        public MediatorSimple(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
        {
            var tipoValidator = typeof(IValidator<>).MakeGenericType(request.GetType());

            var validator = _serviceProvider.GetService(tipoValidator);

            if(validator is not null)
            {
                var metodoValidate = tipoValidator.GetMethod("ValidateAsync")!;
                var tareaValidacion = (Task)metodoValidate!.Invoke(validator, new object[] { request, CancellationToken.None })!;

                await tareaValidacion.ConfigureAwait(false);

                var resultado = tareaValidacion.GetType().GetProperty("Result")!;
                var resultadoValidacion = (ValidationResult)resultado.GetValue(tareaValidacion)!;

                if(!resultadoValidacion.IsValid)
                {
                    throw new ValidacionException(resultadoValidacion);
                }
            }


            var tipoUseCase = typeof(IRequestHandler<,>)
                                .MakeGenericType(request.GetType(), typeof(TResponse));

            var useCase = _serviceProvider.GetService(tipoUseCase);

            if (useCase is null)
            {
                throw new MediatorException($"No se encontro un handler para {request.GetType().Name}");
            }

            var metodoHandle = tipoUseCase.GetMethod("Handle")!;

            return await (Task<TResponse>)metodoHandle.Invoke(useCase, new object[] { request })!;
        }
    }
}
