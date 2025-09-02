﻿using DientesLimpios.Application.Exceptions;
using DientesLimpios.Application.Utilities.Mediator;
using FluentValidation;
using NSubstitute;

namespace DientesLimpios.Test.Application.Utilities.Mediator
{
    [TestClass]
    public class MediatorSimpleTest
    {
        public class RequestFalso : IRequest<string>
        {
            public required string Nombre { get; set; }
        }

        public class HandlerFalso : IRequestHandler<RequestFalso, string>
        {
            public Task<string> Handle(RequestFalso request)
            {
                return Task.FromResult("respuesta correcta");
            }
        }

        public class ValidadorRequestFalso : AbstractValidator<RequestFalso>
        {
            public ValidadorRequestFalso()
            {
                RuleFor(x => x.Nombre).NotEmpty();
            }
        }


        [TestMethod]
        public async Task Send_LlamaMetodoHandler()
        {
            var request = new RequestFalso() { Nombre = "Nombre A" };

            var casoDeUsoMock = Substitute.For<IRequestHandler<RequestFalso, string>>();

            var serviceProvider = Substitute.For<IServiceProvider>();
            serviceProvider
            .GetService(typeof(IRequestHandler<RequestFalso, string>))
            .Returns(casoDeUsoMock);

            var mediador = new MediatorSimple(serviceProvider);

            var resultado = await mediador.Send(request);

            await casoDeUsoMock.Received(1).Handle(request);
        }

        [TestMethod]
        [ExpectedException(typeof(MediatorException))]
        public async Task Send_SinHandlerRegistrado_LanzaExcepcion()
        {
            var request = new RequestFalso() { Nombre = "Nombre A" };

            var casoDeUsoMock = Substitute.For<IRequestHandler<RequestFalso, string>>();

            var serviceProvider = Substitute.For<IServiceProvider>();

            var mediador = new MediatorSimple(serviceProvider);

            var resultado = await mediador.Send(request);
        }

        [TestMethod]
        public async Task Send_ComandoNoValido_LanzaExcepcion()
        {
            var request = new RequestFalso { Nombre = "" };
            var serviceProvider = Substitute.For<IServiceProvider>();
            var validador = new ValidadorRequestFalso();

            serviceProvider
                   .GetService(typeof(IValidator<RequestFalso>))
                   .Returns(validador);

            var mediador = new MediatorSimple(serviceProvider);

            await Assert.ThrowsExceptionAsync<ValidacionException>(async () =>
            {
                await mediador.Send(request);
            });

        }

    }
}
