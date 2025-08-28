using DientesLimpios.Domain.Entities;
using DientesLimpios.Domain.Exceptions;
using DientesLimpios.Domain.ValueObjects;

namespace DientesLimpios.Test.Domain.Entities
{
    [TestClass]
    public class PacienteTest
    {
        [TestMethod]
        [ExpectedException(typeof(ReglaDeNegocioException))]
        public void Constructor_NombreNulo_LanzaExcepcion()
        {
            var email = new Email("felipe@ejemplo.com");
            new Paciente(null!, email);
        }

        [TestMethod]
        [ExpectedException(typeof(ReglaDeNegocioException))]
        public void Constructor_EmailNulo_LanzaExcepcion()
        {
            Email email = null!;
            new Paciente("Felipe", email);
        }
    }
}
