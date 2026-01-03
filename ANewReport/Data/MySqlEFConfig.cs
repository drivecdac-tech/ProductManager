using MySql.Data.EntityFramework;
using MySql.Data.MySqlClient;
using System.Data.Entity;

namespace ANewReport.Data
{
    public class MySqlEFConfig : DbConfiguration
    {
        MySqlEFConfig()
        {
            SetProviderServices("MySql.Data.MySqlClient", new MySqlProviderServices());//   MySqlClient provider for MySQL Server.
            SetDefaultConnectionFactory(new MySqlConnectionFactory()); //Used for creating connections in Code First
        }
    }
}
