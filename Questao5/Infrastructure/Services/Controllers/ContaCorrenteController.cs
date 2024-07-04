using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly IContaCorrenteRepository _contaCorrenteRepository;
        private readonly IMediator _mediator;
        public ContaCorrenteController(IMediator mediator, IContaCorrenteRepository contaCorrenteRepository)
        {
            _mediator = mediator;
            _contaCorrenteRepository = contaCorrenteRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<ContaCorrente>> Get()
        {
            var contas = await _contaCorrenteRepository.GetAllAsync();

            return contas;
        }

        [HttpPost]
        public IActionResult MovimentarConta([FromBody] MovimentarContaRequest command, [FromHeader(Name = "chaveIdempotencia")] string chaveIdempotencia)
        {
            var response = _mediator.Send(command);
            return Ok(response);
        }
    }
}
