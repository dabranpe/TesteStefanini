using Dapper;
using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database;

public class IdempotenciaRepository : IIdempotenciaRepository
{
    private readonly IDataBaseContext _context;

    public IdempotenciaRepository(IDataBaseContext context)
    {
        _context = context;
    }

    public async Task<Idempotencia> GetByIdAsync(string id)
    {
        using (var connection = _context.CreateConnection())
        {
            string query = $"SELECT chave_idempotencia, requisicao, resultado FROM Idempotencia  WHERE chave_idempotencia = '{id.Replace("'", "")}'";
            var idempotencia = await connection.QuerySingleOrDefaultAsync<Idempotencia>(query);
            return idempotencia;
        }
    }

    public async Task<string> AddAsync(Idempotencia idempotencia)
    {
        using (var connection = _context.CreateConnection())
        {
            string query = "INSERT INTO Idempotencia (chave_idempotencia, requisicao, resultado) VALUES (@Chave_Idempotencia, @Requisicao, @Resultado); SELECT last_insert_rowid();";
            var id = await connection.ExecuteScalarAsync<string>(query, idempotencia);
            return id;
        }
    }
}
