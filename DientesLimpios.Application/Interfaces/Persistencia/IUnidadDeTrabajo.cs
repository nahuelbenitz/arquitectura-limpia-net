namespace DientesLimpios.Application.Interfaces.Persistencia
{
    public interface IUnidadDeTrabajo
    {
        Task Save();
        Task Cancel();
    }
}
