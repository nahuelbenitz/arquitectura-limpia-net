using DientesLimpios.Application.Interfaces.Persistencia;

namespace DientesLimpios.Persistence.UnitOfWork
{
    public class UnitOfWorkEFCore : IUnidadDeTrabajo
    {
        private readonly DientesLimpiosDbContext _context;

        public UnitOfWorkEFCore(DientesLimpiosDbContext context)
        {
            _context = context;
        }

        public Task Cancel()
        {
            return Task.CompletedTask;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
