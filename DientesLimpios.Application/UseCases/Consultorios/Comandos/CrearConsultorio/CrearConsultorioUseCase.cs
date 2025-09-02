using DientesLimpios.Application.Interfaces;
using DientesLimpios.Application.Interfaces.Persistencia;
using DientesLimpios.Application.Utilities.Mediator;
using DientesLimpios.Domain.Entities;

namespace DientesLimpios.Application.UseCases.Consultorios.Comandos.CrearConsultorio
{
    public class CrearConsultorioUseCase : IRequestHandler<CrearConsultorioCommand, Guid>
    {
        private readonly IConsultorioRepository _consultorioRepository;
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public CrearConsultorioUseCase(IConsultorioRepository consultorioRepository, IUnidadDeTrabajo unidadDeTrabajo)
        {
            _consultorioRepository = consultorioRepository;
            _unidadDeTrabajo = unidadDeTrabajo;
            
        }

        public async Task<Guid> Handle(CrearConsultorioCommand command)
        {


            var consultorio = new Consultorio(command.Nombre);
            try
            {
                var respuesta = await _consultorioRepository.AddAsync(consultorio);
                await _unidadDeTrabajo.Save();
                return respuesta.Id;
            }
            catch (Exception)
            {
                await _unidadDeTrabajo.Cancel();
                throw;
            }

        }
    }
}
