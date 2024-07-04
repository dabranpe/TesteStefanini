using Microsoft.AspNetCore.Mvc;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly IContaCorrenteRepository _contaCorrenteRepository;
        public ContaCorrenteController(IContaCorrenteRepository contaCorrenteRepository)
        {
                _contaCorrenteRepository = contaCorrenteRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<ContaCorrente>> Get()
        {
            var contas = await _contaCorrenteRepository.GetAllAsync();

            return contas;
        }
    }
}
