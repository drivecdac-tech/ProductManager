using MySql.Data.EntityFramework;
using MySql.Data.MySqlClient;
using System.Data.Entity;

namespace ProductManager.Data
{
    public class MySqlEFConfig : DbConfiguration
    {
        public MySqlEFConfig()
        {
            SetProviderServices(
                "MySql.Data.MySqlClient",
                new MySqlProviderServices());

            SetDefaultConnectionFactory(
                new MySqlConnectionFactory());
        }
    }
}
