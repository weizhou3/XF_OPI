using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XFOPI_Library.DataConnection;
using XFOPI_Library.TesterIFConnection;
using XFOPI_Library.Models;

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

        /// <summary>
        /// Create an empty array of T with fixed indexes
        /// </summary>
        /// <typeparam name="T">data of type T</typeparam>
        /// <param name="count">lengths of the array</param>
        /// <returns>Output array</returns>
        public static T[] CreateArray<T>(int count) where T : new()
        {
            var array = new T[count];
            for (var i = 0; i < count; i++)
            {
                array[i] = new T();
            }
            return array;
        }

        public static List<T> CreateList<T>(int count) where T : new()
        {
            var list = new List<T>();
            for (var i = 0; i < count; i++)
            {
                list.Add(new T());
            }
            return list;
        }

        /// <summary>
        /// Load the DataName and PlcAddress relationship into the mapping list, extract Word/Bit address info
        /// </summary>
        /// <param name="plcDataAddressRecords">DataName and PlcAddress records list, raw from DB</param>
        /// <param name="plcDataAddressMappings">Empty mapping list</param>
        /// <returns>Filled mapping list, with Word/Bit address info</returns>
        public static List<PlcDataAddressMappingModel> LoadPlcDataAddressMappings(List<PlcDataAddressRecordModel> plcDataAddressRecords,
            List<PlcDataAddressMappingModel> plcDataAddressMappings)
        {
            foreach (var Record in plcDataAddressRecords)
            {
                //get the memory area code string
                string MemArea = Record.PlcAddress.Substring(0, 1);

                //get the word address as int
                int WordAddress;
                int.TryParse(Record.PlcAddress.Split('.')[0].Replace(MemArea, ""), out WordAddress);

                //get the bit address as int
                int BitAddress;
                int.TryParse(Record.PlcAddress.Split('.')[1], out BitAddress);

                PlcDataAddressMappingModel temp = new PlcDataAddressMappingModel();
                temp.PlcDataAddressRecord = Record;
                temp.PlcAddress.MemoryArea = MemArea;
                temp.PlcAddress.WordAddress = WordAddress;
                temp.PlcAddress.BitAddress = BitAddress;

                plcDataAddressMappings.Add(temp);

            }
            return plcDataAddressMappings;
        }


        public static string ReplaceCommonEscapeSequences(string s)
        {
            return s.Replace("\\n", "\n").Replace("\\r", "\r");
        }

        public static string InsertCommonEscapeSequences(string s)
        {
            return s.Replace("\n", "\\n").Replace("\r", "\\r");
        }

    }
}
