using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database;

public interface IContaCorrenteRepository
{
    Task<IEnumerable<ContaCorrente>> GetAllAsync();
    Task<ContaCorrente> GetByIdAsync(string id);
    Task<string> AddMovimentoAsync(Movimento movimento);
}
