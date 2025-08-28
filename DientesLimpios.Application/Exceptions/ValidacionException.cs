using FluentValidation.Results;

namespace DientesLimpios.Application.Exceptions
{
    public class ValidacionException : Exception
    {
        public List<string> Errors { get; set; } = [];
        public ValidacionException(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Errors.Add(error.ErrorMessage);
            }
        }

    }
}
