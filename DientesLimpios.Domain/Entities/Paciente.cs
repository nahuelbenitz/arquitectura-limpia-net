using DientesLimpios.Domain.Exceptions;
using DientesLimpios.Domain.ValueObjects;

namespace DientesLimpios.Domain.Entities
{
    public class Paciente
    {
        public Guid Id { get; private set; }
        public string Nombre { get; private set; } = null!;
        public Email Email { get; private set; } = null!;

        public Paciente(string nombre, Email email)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ReglaDeNegocioException($"El {nameof(nombre)} del paciente es obligatorio.");
            }
            if (email is null)
            {
                throw new ReglaDeNegocioException($"El {nameof(email)} del paciente es obligatorio.");
            }
            Id = Guid.CreateVersion7();
            Nombre = nombre;
            Email = email;
        }
    }
}
