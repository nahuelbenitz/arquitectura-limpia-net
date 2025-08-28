using FluentValidation;

namespace DientesLimpios.Application.UseCases.Consultorios.Comandos.CrearConsultorio
{
    public class CrearConsultorioCommandValidation : AbstractValidator<CrearConsultorioCommand>
    {
        public CrearConsultorioCommandValidation()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El campo {PropertyName} es obligatorio.");
        }
    }
}
