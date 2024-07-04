using System.Data;

namespace Questao5.Infrastructure.Database;

public interface IDataBaseContext
{
    IDbConnection CreateConnection();
}
