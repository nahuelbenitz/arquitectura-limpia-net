using DientesLimpios.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Application.Utilities.Mediator
{
    internal class MediatorSimple : IMediator
    {
        private readonly IServiceProvider _serviceProvider;

        public MediatorSimple(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
        {
            var tipoUseCase = typeof(IRequestHandler<,>)
                                .MakeGenericType(request.GetType(), typeof(TResponse));

            var useCase = _serviceProvider.GetService(tipoUseCase);

            if (useCase is null)
            {
                throw new MediatorException($"No se encontro un handler para {request.GetType().Name}");
            }

            var metodoHandle = tipoUseCase.GetMethod("Handle");

            return await (Task<TResponse>)metodoHandle.Invoke(useCase, new object[] { request })!;
        }
    }
}
