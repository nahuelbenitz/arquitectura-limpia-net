using DientesLimpios.API.DTO_s.Consultorios;
using DientesLimpios.Application.UseCases.Consultorios.Comandos.CrearConsultorio;
using DientesLimpios.Application.UseCases.Consultorios.Consultas.ObtenerDetalleConsultorio;
using DientesLimpios.Application.Utilities.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace DientesLimpios.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultoriosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConsultoriosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConsultorioDetalleDTO>> Get(Guid id)
        {
            var query = new ObtenerDetalleConsultorioQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CrearConsultorioDTO request)
        {
            var command = new CrearConsultorioCommand { Nombre = request.Nombre };
            await _mediator.Send(command);
            return Ok();
        }
    }
}
