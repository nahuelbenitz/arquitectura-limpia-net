using DientesLimpios.Application.Interfaces;
using DientesLimpios.Application.Interfaces.Persistencia;
using DientesLimpios.Application.UseCases.Consultorios.Comandos.CrearConsultorio;
using DientesLimpios.Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace DientesLimpios.Test.Application.UseCases.Consultorios
{
    [TestClass]
    public class CrearConsultorioUseCaseTest
    {
        //Sirve para que no aparezca el warning de que las variables no son inicializadas en el constructor
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private IConsultorioRepository repositorio;
        private IValidator<CrearConsultorioCommand> validador;
        private IUnidadDeTrabajo unidadDeTrabajo;
        private CrearConsultorioUseCase casoDeUso;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        [TestInitialize]
        public void Setup()
        {
            repositorio = Substitute.For<IConsultorioRepository>();
            validador = Substitute.For<IValidator<CrearConsultorioCommand>>();
            unidadDeTrabajo = Substitute.For<IUnidadDeTrabajo>();
            casoDeUso = new CrearConsultorioUseCase(repositorio, unidadDeTrabajo, validador);
        }

        [TestMethod]
        public async Task Handle_ComandoValido_ObtenemosIdConsultorio()
        {
            var comando = new CrearConsultorioCommand { Nombre = "Consultorio A" };

            validador.ValidateAsync(comando).Returns(new ValidationResult());

            var consultorioCreado = new Consultorio("Consultorio A");
            repositorio.AddAsync(Arg.Any<Consultorio>()).Returns(consultorioCreado);

            var resultado = await casoDeUso.Handle(comando);

            await validador.Received(1).ValidateAsync(comando);
            await repositorio.Received(1).AddAsync(Arg.Any<Consultorio>());
            await unidadDeTrabajo.Received(1).Save();
            Assert.AreNotEqual(Guid.Empty, resultado);
        }

        [TestMethod]
        public async Task Handle_CuandoHayError_HacemosRollback()
        {
            var comando = new CrearConsultorioCommand { Nombre = "Consultorio A" };
            repositorio.AddAsync(Arg.Any<Consultorio>()).Throws<Exception>();
            validador.ValidateAsync(comando).Returns(new ValidationResult());


            await Assert.ThrowsExceptionAsync<Exception>(async () =>
            {
                var resultado = await casoDeUso.Handle(comando);
            });

            await unidadDeTrabajo.Received(1).Cancel();

        }
    }
}
