using DientesLimpios.Application.Utilities.Mediator;

namespace DientesLimpios.Application.UseCases.Consultorios.Consultas.ObtenerDetalleConsultorio
{
    public class ObtenerDetalleConsultorioQuery : IRequest<ConsultorioDetalleDTO>
    {
        public Guid Id { get; set; }
    }
}
