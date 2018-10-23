using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMgmtLibrary.DataConnection;

namespace UserMgmtLibrary
{
    public static class GlobalConfig
    {
        public static IDataConnection Connection { get; private set; } 

        public static void InitializeConnections(DatabaseType db)
        {
            switch (db)
            {
                case DatabaseType.SqlServer:
                    SqlConnector sql = new SqlConnector();
                    Connection = sql;
                    break;
                case DatabaseType.Sqlite:
                    //TODO - add Sqlite connector
                    break;
                case DatabaseType.TextFiles:
                    TextConnector text = new TextConnector();
                    Connection = text;
                    break;
                default:
                    break;
            }



            
        }

        public static string ConnString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
