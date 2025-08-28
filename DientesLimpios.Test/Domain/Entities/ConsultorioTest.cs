using DientesLimpios.Domain.Entities;
using DientesLimpios.Domain.Exceptions;

namespace DientesLimpios.Test.Domain.Entities
{
    [TestClass]
    public class ConsultorioTest
    {
        [TestClass]
        public class ConsultorioTests
        {
            [TestMethod]
            [ExpectedException(typeof(ReglaDeNegocioException))]
            public void Constructor_NombreNulo_LanzaExcepcion()
            {
                new Consultorio(null!);
            }
        }
    }
}