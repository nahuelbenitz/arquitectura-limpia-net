using DientesLimpios.Application.Interfaces;
using DientesLimpios.Domain.Entities;

namespace DientesLimpios.Persistence.Repositories
{
    public class ConsultorioRepository : Repository<Consultorio>, IConsultorioRepository
    {
        public ConsultorioRepository(DientesLimpiosDbContext context) : base(context)
        {
        }
    }
}
