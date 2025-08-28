using DientesLimpios.Domain.Entities;
using DientesLimpios.Domain.Enums;
using DientesLimpios.Domain.Exceptions;
using DientesLimpios.Domain.ValueObjects;

namespace DientesLimpios.Test.Domain.Entities
{
    [TestClass]
    public class CitaTest
    {
        private Guid _pacienteId = Guid.NewGuid();
        private Guid _dentistaId = Guid.NewGuid();
        private Guid _consultorioId = Guid.NewGuid();
        private IntervaloDeTiempo _intervalo = new IntervaloDeTiempo(DateTime.UtcNow.AddDays(1),
                                DateTime.UtcNow.AddDays(2));

        [TestMethod]
        public void Constructor_CitaValida_EstadoEsProgramada()
        {
            var cita = new Cita(_pacienteId, _dentistaId, _consultorioId, _intervalo);

            Assert.AreEqual(_pacienteId, cita.IdPaciente);
            Assert.AreEqual(_dentistaId, cita.IdDentista);
            Assert.AreEqual(_consultorioId, cita.IdConsultorio);
            Assert.AreEqual(_intervalo, cita.IntervaloDeTiempo);
            Assert.AreEqual(EstadoCita.Programada, cita.Estado);
            Assert.AreNotEqual(Guid.Empty, cita.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ReglaDeNegocioException))]
        public void Constructor_FechaInicioEnElPasado_LanzaExcepcion()
        {
            var intervalo = new IntervaloDeTiempo(DateTime.UtcNow.AddDays(-1), DateTime.UtcNow);
            var cita = new Cita(_pacienteId, _dentistaId, _consultorioId, intervalo);
        }

        [TestMethod]
        public void Cancelar_CitaProgramada_CambiaEstadoACancelada()
        {
            var cita = new Cita(_pacienteId, _dentistaId, _consultorioId, _intervalo);
            cita.Cancelar();
            Assert.AreEqual(EstadoCita.Cancelada, cita.Estado);
        }

        [TestMethod]
        [ExpectedException(typeof(ReglaDeNegocioException))]
        public void Cancelar_CitaNoProgramada_LanzaExcepcion()
        {
            var cita = new Cita(_pacienteId, _dentistaId, _consultorioId, _intervalo);
            cita.Cancelar();
            cita.Cancelar();
        }

        [TestMethod]
        public void Completar_CitaProgramada_CambiaEstadoACompletada()
        {
            var cita = new Cita(_pacienteId, _dentistaId, _consultorioId, _intervalo);
            cita.Completar();
            Assert.AreEqual(EstadoCita.Completada, cita.Estado);
        }

        [TestMethod]
        [ExpectedException(typeof(ReglaDeNegocioException))]
        public void Completar_CitaCancelada_LanzaExcepcion()
        {
            var cita = new Cita(_pacienteId, _dentistaId, _consultorioId, _intervalo);
            cita.Cancelar();
            cita.Completar();
        }
    }
}
