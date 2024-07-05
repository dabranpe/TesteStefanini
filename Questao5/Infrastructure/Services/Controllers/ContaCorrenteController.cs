using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Questao5.Application;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database;
using Questao5.Infrastructure.Services.Swagger;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;


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


        /// <summary>
        /// Método para obter o saldo da conta corrente.
        /// </summary>
        /// <returns>Retorna Número da conta corrente, Nome do titular da conta corrente, Data e hora da resposta da consulta e Valor do Saldo atual
        ///</returns>
        [HttpGet("id")]
        [SwaggerOperation(Summary = "Método para obter o saldo da conta corrente", Description = "Método para obter o saldo da conta corrente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ConsultarSaldoContaResponse))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ConsultarSaldoContaResponseExample))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(CustomResponse))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(CustomResponseExample))]        
        public async Task<IActionResult> Get([SwaggerParameter(Description ="Identificador da Conta Corrente")] string id)
        {
            var response = await _mediator.Send(new ConsultarSaldoContaRequest { IdContaCorrente = id });

            return Ok(response);
        }

        /// <summary>
        /// Método para movimentar conta corrente.
        /// </summary>
        /// <param name="item">Método para movimentar conta corrente.</param>
        /// <returns>ID do movimento</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Método para movimentar conta corrente", Description = "Método para movimentar conta corrente")]
        [ProducesResponseType(StatusCodes.Status200OK)]        
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(MovimentarContaResponse))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(MovimentarContaResponseExample))]
        [SwaggerRequestExample(typeof(MovimentarContaRequest), typeof(MovimentarContaRequestExample))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(CustomResponse))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(CustomResponseExample))]
        public async Task<IActionResult> MovimentarConta([FromBody, SwaggerRequestBody(Description ="Dados para movimentação da conta")] MovimentarContaRequest command, [FromHeader(Name = "chaveIdempotencia"), SwaggerParameter(Description ="Identificador da Requisição")] string chaveIdempotencia)
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
