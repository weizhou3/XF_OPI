using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XFOPI_Library.DataConnection;
using XFOPI_Library.TesterIFConnection;

namespace XFOPI_Library
{
    public static class GlobalConfig
    {
        public static IDataConnection DBConnection { get; private set; }
        public static ITesterIFConnection IFConnection { get; private set; }

        public static void InitializeDBConnections(DatabaseType db)
        {
            switch (db)
            {
                case DatabaseType.SqlServer:
                    SqlConnector sql = new SqlConnector();
                    DBConnection = sql;
                    break;
                case DatabaseType.Sqlite:
                    SqliteConnector sqlite = new SqliteConnector();
                    DBConnection = sqlite;
                    break;
                case DatabaseType.TextFiles:
                    TextConnector text = new TextConnector();
                    DBConnection = text;
                    break;
                default:
                    break;
            }
        }

        internal static string LoadConnString(string id)
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        internal static DynamicParameters ToDynamicParameters(this Dictionary<string, object> p)
        {
            DynamicParameters output = new DynamicParameters();
            p.ToList().ForEach(x => output.Add(x.Key, x.Value));
            return output;
        }

        public static void InitializeIFConnections(TesterIFType IFType)
        {
            switch (IFType)
            {
                case TesterIFType.NIGPIB:
                    NIGpibConnector gpib = new NIGpibConnector();
                    IFConnection = gpib;
                    break;
                case TesterIFType.RS232:
                    RS232Connector rs232 = new RS232Connector();
                    IFConnection = rs232;
                    break;
                default:
                    break;
            }
        }
    }
}
