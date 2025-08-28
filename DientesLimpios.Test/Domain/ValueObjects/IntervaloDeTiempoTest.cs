using DientesLimpios.Domain.Exceptions;
using DientesLimpios.Domain.ValueObjects;

namespace DientesLimpios.Test.Domain.ValueObjects
{
    [TestClass]
    public class IntervaloDeTiempoTest
    {
        [TestMethod]
        [ExpectedException(typeof(ReglaDeNegocioException))]
        public void Constructor_FechaInicioPosteriorALaFechaFin_LanzaExcepcion()
        {
            new IntervaloDeTiempo(DateTime.UtcNow, DateTime.UtcNow.AddDays(-1));
        }

        [TestMethod]
        public void Constructor_ParametrosCorrectos_NoLanzaExcepcion()
        {
            new IntervaloDeTiempo(DateTime.UtcNow, DateTime.UtcNow.AddMinutes(30));
        }
    }
}
