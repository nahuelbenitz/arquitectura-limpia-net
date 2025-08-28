using DientesLimpios.Domain.Exceptions;

namespace DientesLimpios.Domain.ValueObjects
{
    public record Email
    {
        public string Valor { get; } = null!;
        public Email(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ReglaDeNegocioException($"El {nameof(email)} no puede estar vacío.");
            }
            if (!email.Contains("@"))
            {
                throw new ReglaDeNegocioException($"El {nameof(email)} no es válido.");
            }
            Valor = email;
        }
    }
}
