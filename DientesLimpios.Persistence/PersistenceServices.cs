using DientesLimpios.Application.Interfaces;
using DientesLimpios.Application.Interfaces.Persistencia;
using DientesLimpios.Persistence.Repositories;
using DientesLimpios.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DientesLimpios.Persistence
{
    public static class PersistenceServices
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<DientesLimpiosDbContext>(options =>
            {
                options.UseSqlServer("name=DientesLimpiosDb");
            });

            services.AddScoped<IConsultorioRepository, ConsultorioRepository>();
            services.AddScoped<IUnidadDeTrabajo, UnitOfWorkEFCore>();
            
            return services;
        }
    }
}
