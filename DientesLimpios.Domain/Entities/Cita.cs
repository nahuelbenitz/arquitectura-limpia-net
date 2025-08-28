using DientesLimpios.Domain.Enums;
using DientesLimpios.Domain.Exceptions;
using DientesLimpios.Domain.ValueObjects;

namespace DientesLimpios.Domain.Entities
{
    public class Cita
    {
        public Guid Id { get; private set; }
        public Guid IdPaciente { get; private set; }
        public Guid IdDentista { get; private set; }
        public Guid IdConsultorio { get; private set; }
        public EstadoCita Estado { get; private set; }
        public IntervaloDeTiempo IntervaloDeTiempo { get; private set; }
        public Paciente? Paciente { get; private set; }
        public Dentista? Dentista { get; private set; }
        public Consultorio? Consultorio { get; private set; }

        public Cita(Guid idPaciente, Guid idDentista, Guid idConsultorio, IntervaloDeTiempo intervaloDeTiempo)
        {
            if (intervaloDeTiempo.Inicio < DateTime.Now)
            {
                throw new ReglaDeNegocioException($"La fecha de inicio no puede ser anterior a la fecha actual");
            }

            Id = Guid.CreateVersion7();
            IdPaciente = idPaciente;
            IdDentista = idDentista;
            IdConsultorio = idConsultorio;
            IntervaloDeTiempo = intervaloDeTiempo;
            Estado = EstadoCita.Programada;
        }

        public void Cancelar()
        {
            if (Estado != EstadoCita.Programada)
            {
                throw new ReglaDeNegocioException("Solo se pueden cancelar citas programadas.");
            }
            Estado = EstadoCita.Cancelada;
        }

        public void Completar()
        {
            if (Estado != EstadoCita.Programada)
            {
                throw new ReglaDeNegocioException("Solo se pueden completar citas programadas.");
            }
            Estado = EstadoCita.Completada;
        }
    }
}
