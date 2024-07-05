using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database;

public interface IIdempotenciaRepository
{
    Task<Idempotencia> GetByIdAsync(string id);
    Task<string> AddAsync(Idempotencia idempotencia);
}
