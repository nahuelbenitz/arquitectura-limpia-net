using DientesLimpios.Application.Exceptions;
using DientesLimpios.Application.Interfaces;
using DientesLimpios.Application.Interfaces.Persistencia;
using DientesLimpios.Application.Utilities.Mediator;
using DientesLimpios.Domain.Entities;
using FluentValidation;

namespace DientesLimpios.Application.UseCases.Consultorios.Comandos.CrearConsultorio
{
    public class CrearConsultorioUseCase : IRequestHandler<CrearConsultorioCommand, Guid>
    {
        private readonly IConsultorioRepository _consultorioRepository;
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private readonly IValidator<CrearConsultorioCommand> _validator;

        public CrearConsultorioUseCase(IConsultorioRepository consultorioRepository, IUnidadDeTrabajo unidadDeTrabajo, IValidator<CrearConsultorioCommand> validator)
        {
            _consultorioRepository = consultorioRepository;
            _unidadDeTrabajo = unidadDeTrabajo;
            _validator = validator;
        }

        public async Task<Guid> Handle(CrearConsultorioCommand command)
        {
            var resultaValidacion = await _validator.ValidateAsync(command);

            if(!resultaValidacion.IsValid)
            {
                throw new ValidacionException(resultaValidacion);
            }

            var consultorio = new Consultorio(command.Nombre);
            try
            {
                var respuesta = await _consultorioRepository.AddAsync(consultorio);
                await _unidadDeTrabajo.Save();
                return respuesta.Id;
            }
            catch (Exception)
            {
                await _unidadDeTrabajo.Cancel();
                throw;
            }

        }
    }
}
