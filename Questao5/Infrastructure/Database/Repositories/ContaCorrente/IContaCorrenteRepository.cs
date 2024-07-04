using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database;

public interface IContaCorrenteRepository
{
    Task<IEnumerable<ContaCorrente>> GetAllAsync();
}
