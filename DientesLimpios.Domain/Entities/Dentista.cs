using DientesLimpios.Domain.Exceptions;
using DientesLimpios.Domain.ValueObjects;

namespace DientesLimpios.Domain.Entities
{
    public class Dentista
    {
        public Guid Id { get; private set; }
        public string Nombre { get; private set; } = null!;
        public Email Email { get; private set; } = null!;

        public Dentista(string nombre, Email email)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ReglaDeNegocioException($"El {nameof(nombre)} del dentista es obligatorio.");
            }

            if (email is null)
            {
                throw new ReglaDeNegocioException($"El {nameof(email)} del dentista es obligatorio.");
            }

            Id = Guid.CreateVersion7();
            Nombre = nombre;
            Email = email;
        }

    }
}
