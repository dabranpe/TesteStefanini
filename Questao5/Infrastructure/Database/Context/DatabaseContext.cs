using Microsoft.Data.Sqlite;
using Questao5.Infrastructure.Sqlite;
using System.Data;

namespace Questao5.Infrastructure.Database;

public class DatabaseContext : IDataBaseContext
{    
    private readonly DatabaseConfig databaseConfig;

    public DatabaseContext(DatabaseConfig databaseConfig)
    {
        this.databaseConfig = databaseConfig;
    }

    public IDbConnection CreateConnection()
    {
        return new SqliteConnection(databaseConfig.Name);
    }
}
