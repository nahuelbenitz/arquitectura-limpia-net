using DientesLimpios.Domain.Exceptions;

namespace DientesLimpios.Domain.Entities
{
    public class Consultorio
    {
        public Guid Id { get; private set; }
        public string Nombre { get; private set; } = null!;

        public Consultorio(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ReglaDeNegocioException($"El {nameof(nombre)} del consultorio es obligatorio.");
            }
            Nombre = nombre;
            Id = Guid.CreateVersion7();
        }
    }
}
