using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XFOPI_Library.DataConnection;
using XFOPI_Library.TesterIFConnection;
using XFOPI_Library.Models;
using XFOPI_Library.Models.SG2000;
using XFOPI_Library.PLCConnection;

namespace XFOPI_Library
{
    public static class GlobalConfig
    {
        public static IDataConnection DBConnection { get; private set; }
        public static ITesterIFConnection IFConnection { get; private set; }
        public static IPlcDataConnection DataConnection { get; private set; }

        public static void InitializePlcConnection()
        {
            OmronFINsPlcConnector PlcConn = new OmronFINsPlcConnector();
            DataConnection = PlcConn;
        }

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
        /// Populate the full PCL Word list of a certain memory area
        /// </summary>
        /// <param name="plcWordList">The PLC Word list to be filled</param>
        /// <param name="MemArea">Word memory area</param>
        /// <param name="startingId">Starting Word address</param>
        /// <returns></returns>
        public static List<PlcWordModel> PopulatePlcWordList_singleType(List<PlcWordModel> plcWordList, PlcMemArea memArea ,int startingId)
        {
            int listSize;
            string MemArea = null;
            switch (memArea)
            {
                case PlcMemArea.CIO:
                    MemArea = "C";
                    listSize = 0;
                    break;
                case PlcMemArea.WR:
                    MemArea = "W";
                    listSize = OmronFINsClass.Size_WR;
                    break;
                case PlcMemArea.HR:
                    MemArea = "H";
                    listSize = OmronFINsClass.Size_HR;
                    break;
                case PlcMemArea.AR:
                    MemArea = "A";
                    listSize = 0;
                    break;
                case PlcMemArea.DM:
                    MemArea = "D";
                    listSize = OmronFINsClass.Size_DM;
                    break;
                default:
                    MemArea = "";
                    listSize = 0;
                    break;
            }
            
            
            List<PlcWordModel> retPlcWordList = CreateList<PlcWordModel>(listSize);

            foreach (var item in retPlcWordList)
            {
                item.SetMemoryArea(MemArea);
                item.SetWordAddress(startingId);
                startingId++;
            }
            plcWordList.AddRange(retPlcWordList);
            return plcWordList;
        }

        /// <summary>
        /// Create the complete BitModel list, with Word address/str and Bit address/str 
        /// </summary>
        /// <param name="plcBitList"></param>
        /// <param name="MemArea"></param>
        /// <param name="startingWordId"></param>
        /// <param name="startingBitId"></param>
        /// <returns></returns>
        public static List<PlcBitModel> PopulatePlcBitList_singleType(List<PlcBitModel> plcBitList, PlcMemArea memArea, int startingWordId, int startingBitId)
        {
            int listSize;
            string MemArea = null;
            switch (memArea)
            {
                case PlcMemArea.CIO:
                    MemArea = "C";
                    listSize = 0;
                    break;
                case PlcMemArea.WR:
                    MemArea = "W";
                    listSize = OmronFINsClass.Size_WR * OmronFINsClass.Width_Word;
                    break;
                case PlcMemArea.HR:
                    MemArea = "H";
                    listSize = OmronFINsClass.Size_HR * OmronFINsClass.Width_Word;
                    break;
                case PlcMemArea.AR:
                    MemArea = "A";
                    listSize = 0;
                    break;
                case PlcMemArea.DM:
                    MemArea = "D";
                    listSize = 0;
                    break;
                default:
                    MemArea = "";
                    listSize = 0;
                    break;
            }
           
            List<PlcBitModel> retPlcBitList = CreateList<PlcBitModel>(listSize);

            foreach (var item in retPlcBitList)
            {
                if (startingBitId == OmronFINsClass.Width_Word)
                {
                    startingBitId = 0;
                    startingWordId++;
                }
                item.SetMemArea(MemArea);
                item.SetWordAddress(startingWordId);
                item.SetBitAddress(startingBitId);
                item.SetAddressStr();

                startingBitId++;
            }
            plcBitList.AddRange(retPlcBitList);
            return plcBitList;
        }

        

        

        /// <summary>
        /// Polulate the mapping list with DataName and PlcAddress relationship, extract Word/Bit address info and store in PlcWordAddress model
        /// </summary>
        /// <param name="plcDataAddressRecords">DataName and PlcAddress records list, raw from DB</param>
        /// <param name="plcDataAddressMappings">Empty mapping list</param>
        /// <returns>Filled mapping list, with Word/Bit address info</returns>
        public static List<PlcDataAddressMappingModel> PopulatePlcDataAddressMappings(List<PlcDataAddressRecordModel> plcDataAddressRecords,
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
                int? BitAddress = null;
                
                if (Record.PlcAddress.Contains("."))
                {
                    int bitAddress;
                    //int.TryParse(Record.PlcAddress.Split('.')[1], out bitAddress);
                    if (int.TryParse(Record.PlcAddress.Split('.')[1], out bitAddress))
                    {
                        BitAddress = (int?)bitAddress;
                    }
                    
                    //BitAddress = int.TryParse(Record.PlcAddress.Split('.')[1], out bitAddress) ? BitAddress:bitAddress;
                }
                else
                {
                    BitAddress = null;
                }
                

                PlcDataAddressMappingModel temp = new PlcDataAddressMappingModel();
                temp.PlcDataAddressRecord = Record;
                temp.PlcWordAddress.MemoryArea = MemArea;
                temp.PlcWordAddress.WordAddress = WordAddress;
                temp.PlcWordAddress.BitAddress = BitAddress;

                plcDataAddressMappings.Add(temp);

            }
            return plcDataAddressMappings;
        }

        public static List<PlcDataAddressMappingModel> AppendFullUintAddressMappings
            (List<TypeUintModel> AllTypeUint, List<PlcDataAddressMappingModel> plcDataAddressMappings)
        {
            List<PlcDataAddressMappingModel> tempList = new List<PlcDataAddressMappingModel>();
            //PlcDataAddressMappingModel temp = new PlcDataAddressMappingModel();
            int i = 1;
            int maxId = plcDataAddressMappings.Max(x => x.PlcDataAddressRecord.id);
            foreach (var item in AllTypeUint)
            {
                PlcDataAddressMappingModel  temp = 
                    plcDataAddressMappings.Find(x => x.PlcDataAddressRecord.DataName == item.Name).DeepCopy();
                
                temp.PlcDataAddressRecord.id = maxId + i;
                temp.PlcWordAddress.WordAddress++;
                temp.PlcDataAddressRecord.PlcAddress = temp.PlcWordAddress.MemoryArea + temp.PlcWordAddress.WordAddress.ToString();
                tempList.Add(temp);
                i++;
            }
            plcDataAddressMappings.AddRange(tempList);
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

        //public static async Task<int> testWorkerThreadAsync(IProgress<ProgressReportModel> progress, CancellationToken ctsToken, int startingNumber)
        //{

        //    int result = startingNumber;
        //    int totalNumer = 100;
        //    ProgressReportModel report = new ProgressReportModel();

        //    await Task.Run(() => 
        //    {
        //        for (int i = 0; i < totalNumer; i++)
        //        {
        //            result = ModifyResult(result);
        //            ctsToken.ThrowIfCancellationRequested();

        //            report.CountedNumber = result;
        //            report.PercetageComplete = result * 100 / totalNumer;
        //            progress.Report(report);

        //            Thread.Sleep(500);

        //        }
        //    });

        //    return result;
        //}

        public static bool VerifyButtonAccess(string btnName, string formName, string currentLogin)
        {
            List<ButtonAccessLevelModel> btnLevels = DBConnection.GetButtonsAccessLevel_All();
            ButtonAccessLevelModel btn = btnLevels.FirstOrDefault(x => x.ButtonName == btnName && x.Form == formName);
            if (btn!=null)
            {
                switch (currentLogin)
                {
                    case "Admin":
                        return true;
                    case "Maint":
                        if (btn.Maint=="1")
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    case "Operator":
                        if (btn.Operator=="1")
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    default:
                        return false;
                }
            }
            else
            {
                return false;
            }
        }



        public static int testWorkerThread()
        {
            int startingNumber = 1;
            int result = startingNumber;
            int totalNumer = 100;
            PlcProgressReportModel report = new PlcProgressReportModel();


                for (int i = 0; i < totalNumer; i++)
                {
                    result = ModifyResult(result);
                    //ctsToken.ThrowIfCancellationRequested();

                    report.CountedNumber = result;
                    report.PercetageComplete = result * 100 / totalNumer;
                    //progress.Report(report);

                    Thread.Sleep(500);

                }

            return result;
        }

        private static int ModifyResult(int result)
        {
            return result + 1;
        }

    }
}
