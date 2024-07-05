using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Questao5.Application;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly IIdempotenciaRepository _idempotenciaRepository;
        private readonly IMediator _mediator;
        public ContaCorrenteController(IMediator mediator, IIdempotenciaRepository idempotenciaRepository)
        {
            _mediator = mediator;
            _idempotenciaRepository = idempotenciaRepository;
        }

        
        [HttpGet("id")]
        public async Task<IActionResult> Get(string id)
        {
            var response = await _mediator.Send(new ConsultarSaldoContaRequest { IdContaCorrente = id });

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> MovimentarConta([FromBody] MovimentarContaRequest command, [FromHeader(Name = "chaveIdempotencia")] string chaveIdempotencia)
        {
            if (string.IsNullOrEmpty(chaveIdempotencia))
            {
                return BadRequest("Chave Idempotencia é obrigatório.");
            }

            var existingRequest = await _idempotenciaRepository.GetByIdAsync(chaveIdempotencia);
            if (existingRequest != null)
            {
                return Ok(existingRequest.Resultado);
            }
            else
            {
                var response = await _mediator.Send(command);

                await _idempotenciaRepository.AddAsync(new Idempotencia {
                                                                          Chave_Idempotencia = chaveIdempotencia,
                                                                          Requisicao = JsonConvert.SerializeObject(command),
                                                                          Resultado = JsonConvert.SerializeObject(response)
                                                        });

                return Ok(response);
            }            
        }
    }
}
