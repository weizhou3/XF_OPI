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

        public static void InitializeIFConnections(TesterIFType IFType, TesterIFProtocol IFProtocol)
        {
            switch (IFType)
            {
                case TesterIFType.NIGPIB:
                    NIGpibConnector gpib = new NIGpibConnector();
                    IFConnection = gpib;
                    switch (IFProtocol)
                    {
                        case TesterIFProtocol.MTGPIB:
                            gpib.Protocol = "MTGPIB";
                            break;
                        case TesterIFProtocol.RSGPIB:
                            gpib.Protocol = "RSGPIB";
                            break;
                        case TesterIFProtocol.RSRS232:
                            gpib.Protocol = "invalid";
                            break;
                        default:
                            break;
                    }
                    break;
                case TesterIFType.RS232:
                    RS232Connector rs232 = new RS232Connector();
                    IFConnection = rs232;
                    switch (IFProtocol)
                    {
                        case TesterIFProtocol.MTGPIB:
                            rs232.Protocol = "invalid";
                            break;
                        case TesterIFProtocol.RSGPIB:
                            rs232.Protocol = "invalid";
                            break;
                        case TesterIFProtocol.RSRS232:
                            rs232.Protocol = "RSRS232";
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }



    }
}
