using DientesLimpios.Domain.Entities;

namespace DientesLimpios.Application.UseCases.Consultorios.Consultas.ObtenerDetalleConsultorio
{
    public static class MapperExtensions
    {
        public static ConsultorioDetalleDTO ToDTO(this Consultorio consultorio)
        {
            var dto = new ConsultorioDetalleDTO
            {
                Id = consultorio.Id,
                Nombre = consultorio.Nombre
            };

            return dto;
        }
    }
}
