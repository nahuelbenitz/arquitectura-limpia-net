using DientesLimpios.Application.Utilities.Mediator;

namespace DientesLimpios.Application.UseCases.Consultorios.Comandos.CrearConsultorio
{
    public class CrearConsultorioCommand : IRequest<Guid>
    {
        public required string Nombre { get; set; }
    }
}
