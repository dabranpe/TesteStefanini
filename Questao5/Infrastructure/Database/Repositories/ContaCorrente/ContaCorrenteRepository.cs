using Dapper;
using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database;

public class ContaCorrenteRepository : IContaCorrenteRepository
{
    private readonly IDataBaseContext _context;

    public ContaCorrenteRepository(IDataBaseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ContaCorrente>> GetAllAsync()
    {
        using (var connection = _context.CreateConnection())
        {
            string query = "SELECT IdContaCorrente, Numero, Nome, Ativo FROM ContaCorrente";
            var contas = await connection.QueryAsync<ContaCorrente>(query);
            return contas;
        }
    }

    public async Task<ContaCorrente> GetByIdAsync(string id)
    {
        using (var connection = _context.CreateConnection())
        {
            string query = $"SELECT IdContaCorrente, Numero, Nome, Ativo FROM ContaCorrente WHERE IdContaCorrente = '{id}'";
            var conta = await connection.QuerySingleOrDefaultAsync<ContaCorrente>(query);
            return conta;
        }
    }
}
