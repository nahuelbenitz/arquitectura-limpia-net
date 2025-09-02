using DientesLimpios.Application.Exceptions;
using DientesLimpios.Application.Interfaces;
using DientesLimpios.Application.Utilities.Mediator;

namespace DientesLimpios.Application.UseCases.Consultorios.Consultas.ObtenerDetalleConsultorio
{
    public class ObtenerDetalleConsultorioUseCase : IRequestHandler<ObtenerDetalleConsultorioQuery, ConsultorioDetalleDTO>
    {
        private readonly IConsultorioRepository _consultorioRepository;

        public ObtenerDetalleConsultorioUseCase(IConsultorioRepository consultorioRepository)
        {
            _consultorioRepository = consultorioRepository;
        }

        public async Task<ConsultorioDetalleDTO> Handle(ObtenerDetalleConsultorioQuery request)
        {
            var consultorio = await _consultorioRepository.GetByIdAsync(request.Id);

            if(consultorio is null)
            {
                throw new NoEncontradoException();
            }

            return consultorio.ToDTO();
        }
    }
}
