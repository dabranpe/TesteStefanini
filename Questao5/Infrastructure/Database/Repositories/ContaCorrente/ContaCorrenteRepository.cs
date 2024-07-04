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
            var users = await connection.QueryAsync<ContaCorrente>(query);
            return users;
        }
    }
}
