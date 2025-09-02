using DientesLimpios.Application.Exceptions;
using DientesLimpios.Application.Interfaces;
using DientesLimpios.Application.UseCases.Consultorios.Consultas.ObtenerDetalleConsultorio;
using DientesLimpios.Domain.Entities;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace DientesLimpios.Test.Application.UseCases.Consultorios
{
    [TestClass]
    public class ObtenerDetalleConsultorioUseCaseTest
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private IConsultorioRepository repositorio;
        private ObtenerDetalleConsultorioUseCase casoDeUso;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        [TestInitialize]
        public void Setup()
        {
            repositorio = Substitute.For<IConsultorioRepository>();
            casoDeUso = new ObtenerDetalleConsultorioUseCase(repositorio);
        }


        [TestMethod]
        public async Task Handle_ConsultorioExiste_RetornaDTO()
        {
            // Preparación
            var consultorio = new Consultorio("Consultorio A");
            var id = consultorio.Id;
            var consulta = new ObtenerDetalleConsultorioQuery { Id = id };

            repositorio.GetByIdAsync(id).Returns(consultorio);

            // Prueba
            var resultado = await casoDeUso.Handle(consulta);

            // Verificación
            Assert.IsNotNull(resultado);
            Assert.AreEqual(id, resultado.Id);
            Assert.AreEqual("Consultorio A", resultado.Nombre);
        }

        [TestMethod]
        [ExpectedException(typeof(NoEncontradoException))]
        public async Task Handle_ConsultorioNoExiste_LanzaExcepcionNoEncontrado()
        {
            // Preparación
            var id = Guid.NewGuid();
            var consulta = new ObtenerDetalleConsultorioQuery { Id = id };

            repositorio.GetByIdAsync(id).ReturnsNull();

            // Prueba
            await casoDeUso.Handle(consulta);
        }
    }
}
