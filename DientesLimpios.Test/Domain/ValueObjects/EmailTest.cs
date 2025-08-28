using DientesLimpios.Domain.Exceptions;
using DientesLimpios.Domain.ValueObjects;

namespace DientesLimpios.Test.Domain.ValueObjects
{
    [TestClass]
    public class EmailTest
    {
        [TestMethod]
        [ExpectedException(typeof(ReglaDeNegocioException))]
        public void Constructor_EmailNulo_LanzaExcepcion()
        {
            new Email(null!);
        }

        [TestMethod]
        [ExpectedException(typeof(ReglaDeNegocioException))]
        public void Constructor_EmailSinArroba_LanzaExcepcion()
        {
            new Email("Nahuel.com");
        }

        [TestMethod]
        public void Constructor_EmailValido_NoLanzaExcepcion()
        {
            new Email("Nahuel@gmail.com");
        }
    }
}
