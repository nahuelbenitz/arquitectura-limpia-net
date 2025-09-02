using DientesLimpios.Application.UseCases.Consultorios.Comandos.CrearConsultorio;
using DientesLimpios.Application.UseCases.Consultorios.Consultas.ObtenerDetalleConsultorio;
using DientesLimpios.Application.Utilities.Mediator;
using Microsoft.Extensions.DependencyInjection;

namespace DientesLimpios.Application
{
    public static class ApplicationServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IMediator, MediatorSimple>();
            services.AddScoped<IRequestHandler<CrearConsultorioCommand, Guid>, CrearConsultorioUseCase>();
            services.AddScoped<IRequestHandler<ObtenerDetalleConsultorioQuery, ConsultorioDetalleDTO>, ObtenerDetalleConsultorioUseCase>();

            return services;
        }
    }
}
