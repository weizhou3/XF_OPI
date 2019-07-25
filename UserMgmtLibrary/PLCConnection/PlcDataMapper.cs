using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XFOPI_Library.Models;
using XFOPI_Library.Models.SG2000;

namespace XFOPI_Library.PLCConnection
{
    public static class PlcDataMapper
    {
        //UI data exchange layer API
        public static object _RWlistLock = new object();
        public static object _WlistLock = new object();
        public static object _WordsListLock = new object();
        public static Dictionary<string, string> DataNameValue_Wlist = new Dictionary<string, string>();
        //public static Dictionary<string, string> DataNameValue_Rlist = new Dictionary<string, string>();
        public static Dictionary<string, RlistDataModel> DataNameValue_Rlist = new Dictionary<string, RlistDataModel>();

        public static Dictionary<string, string> AlarmCodeDescription = new Dictionary<string, string>();
        public static Dictionary<string, string> WarningCodeDescription = new Dictionary<string, string>();
        //TODO: change dictionary to sortedList

        //Data varibles
        public static List<PlcDataNameAddressModel> DataNameAddresses = new List<PlcDataNameAddressModel>();
        //Data names with AppCode
        public static List<PlcDataNameModel> DataNames = new List<PlcDataNameModel>();


        public static List<TypeBoolModel> TypeBools = new List<TypeBoolModel>();
        public static List<TypeUshortModel> TypeUshorts = new List<TypeUshortModel>();
        public static List<TypeUintModel> TypeUints = new List<TypeUintModel>();
        public static List<AlarmCodeModel> AlarmCodes = new List<AlarmCodeModel>();
        public static List<WarningCodeModel> WarningCodes = new List<WarningCodeModel>();

        //Data mapping
        //public static List<PlcDataAddressRecordModel> PlcDataAddressRecords = new List<PlcDataAddressRecordModel>();
      
        //public static List<PlcDataAddressMappingModel> PlcDataAddressMappings = new List<PlcDataAddressMappingModel>();
        public static List<PlcWordAddressMappingModel> WordMap = new List<PlcWordAddressMappingModel>();
        public static List<PlcBitAddressMappingModel> BitMap = new List<PlcBitAddressMappingModel>();

        //Plc words DM area
        public static List<PlcWordModel> PlcDMWords = new List<PlcWordModel>();
        //PLC words WR,HR area for reading purpose
        public static List<PlcWordModel> PlcWRWords = new List<PlcWordModel>();
        public static List<PlcWordModel> PlcHRWords = new List<PlcWordModel>();
        //Plc bits WR and HR area
        public static List<PlcBitModel> PlcBits = new List<PlcBitModel>();
        public static SortedList<int, int> addrIntervalDM = new SortedList<int, int>();
        public static SortedList<int, int> addrIntervalHR = new SortedList<int, int>();
        public static SortedList<int, int> addrIntervalWR = new SortedList<int, int>();

        public static List<PlcPortSettingModel> PlcPortSettings = new List<PlcPortSettingModel>();

        /// <summary>
        /// Initialize system static data fields, and load default names and values from DB
        /// </summary>
        public static void InitializeData()  
	    {
            //Load the DataNameAddresses table
            DataNameAddresses = GlobalConfig.DBConnection.GetAllDataAddresses();
            //Load all Data Names with AppCode attributes
            DataNames = GlobalConfig.DBConnection.GetAllDataNames();

            generateDataTypes(DataNameAddresses);
            //TypeBools = GlobalConfig.DBConnection.GetTypeBool_All();
            //TypeUints = GlobalConfig.DBConnection.GetTypeUint_All();
            //TypeUshorts = GlobalConfig.DBConnection.GetTypeUshort_All();

            //Load Alarms and Warnings

            AlarmCodes = GlobalConfig.DBConnection.GetAlarmCodes_All();
            WarningCodes = GlobalConfig.DBConnection.GetWarningCodes_All();
            populateAlarmCodeList(AlarmCodes);
            populateWarningCodeList(WarningCodes);


            //Load addresses from DB
            //TODO -- delete old load address method
            //PlcDataAddressRecords = GlobalConfig.DBConnection.GetDataAddressRecord_All();
            

            //Load PLC serial port setting
            PlcPortSettings = GlobalConfig.DBConnection.GetPlcPortSetting_All();

            //Populate the address mappings
            //PlcDataAddressMappings = PopulatePlcDataAddressMappings(PlcDataAddressRecords, PlcDataAddressMappings);
            
            //TODO-- change mapping method to adapt new DB sheets
            //Populate the WordMap and PlcWords -- new
            WordMap = PopulatePlcWordAddressMap(WordMap);
            //Generate the full uint map and Full PlcWords --  new
            WordMap = GenerateFullWordAddressMap(WordMap);
            //Populate the BitMap and PlcBits  -- new
            BitMap = PopulatePlcBitAddressMap(BitMap);

            //Complete the address mappings with full Uint address (2 words)
            //PlcDataAddressMappings = AppendFullUintAddressMappings(TypeUints, PlcDataAddressMappings);


            //Populate default Word list
            //PlcDMWords = GlobalConfig.PopulatePlcWordList_singleType(PlcDMWords,PlcMemArea.DM, OmronFINsClass.StartingId_DM);
            //PlcWRWords = GlobalConfig.PopulatePlcWordList_singleType(PlcWRWords, PlcMemArea.WR, OmronFINsClass.StartingId_WR);
            //PlcHRWords = GlobalConfig.PopulatePlcWordList_singleType(PlcWRWords, PlcMemArea.HR, OmronFINsClass.StartingId_HR);

            //Populate default Bit list
            //PlcBits = GlobalConfig.PopulatePlcBitList_singleType(PlcBits, PlcMemArea.WR, OmronFINsClass.StartingId_WR, 0);
            //PlcBits = GlobalConfig.PopulatePlcBitList_singleType(PlcBits, PlcMemArea.HR, OmronFINsClass.StartingId_HR, 0);

            foreach (var typeBool in TypeBools)
            {
                typeBool.AppCode = DataNames.Find(d => d.Name == typeBool.Name).AppCode;
            }
            foreach (var typeUshort in TypeUshorts)
            {
                typeUshort.AppCode = DataNames.Find(d => d.Name == typeUshort.Name).AppCode;
            }
            foreach (var typeUint in TypeUints)
            {
                typeUint.AppCode = DataNames.Find(d => d.Name == typeUint.Name).AppCode;
            }

            //Populate DataNameValue R,W list
            foreach (var item in TypeBools)
            {
                lock (_RWlistLock)
                {
                    DataNameValue_Rlist.Add(item.Name, new RlistDataModel(item.ValueStr,item.AppCode));                    
                }
            }
            foreach (var item in TypeUshorts)
            {
                lock (_RWlistLock)
                {
                    DataNameValue_Rlist.Add(item.Name, new RlistDataModel(item.ValueStr, item.AppCode));                     
                }
            }
            foreach (var item in TypeUints)
            {
                lock (_RWlistLock)
                {
                    DataNameValue_Rlist.Add(item.Name, new RlistDataModel(item.ValueStr, item.AppCode));                    
                }
            }

            GetAddressInverval(out addrIntervalDM, out addrIntervalHR, out addrIntervalWR);
        }

        



        private static void generateDataTypes(List<PlcDataNameAddressModel> DAlist)
        {
            TypeBools.Clear();
            TypeUshorts.Clear();
            TypeUints.Clear();

            foreach (var item in DAlist)
            {
                if (item.Type.ToLower()=="bool")
                {
                    TypeBools.Add(new TypeBoolModel(item.Name));
                }
                else if (item.Type.ToLower() == "ushort")
                {
                    TypeUshorts.Add(new TypeUshortModel(item.Name));
                }
                else if (item.Type.ToLower() == "uint")
                {
                    TypeUints.Add(new TypeUintModel(item.Name));
                }
            }
        }

        private static void populateAlarmCodeList(List<AlarmCodeModel> AlarmCodes)
        {
            AlarmCodeDescription.Clear();
            foreach (var code in AlarmCodes)
            {
                AlarmCodeDescription.Add(code.AlarmCodeName, code.AlarmDescription);
            }
        }

        private static void populateWarningCodeList(List<WarningCodeModel> WarningCodes)
        {
            WarningCodeDescription.Clear();
            foreach (var code in WarningCodes)
            {
                WarningCodeDescription.Add(code.WarnCodeName, code.WarnDescription);
            }
        }

        /// <summary>
        /// Publist value in words to system data Type Ushort and Uint
        /// </summary>
        public static void DMWordsToData()
        {
            foreach (var typeUshort in TypeUshorts)
            {
                //int wordAddress = WordMap.Find(w => w.DataName == typeUshort.Name).Word.WordAddress;
                typeUshort.SetValueHex(PlcDMWords.Find(w => w.WordAddress == 
                    WordMap.Find(m => m.DataName == typeUshort.Name).Word.WordAddress).ValueStrHex);
            }

            foreach (var typeUint in TypeUints)
            {
                List<PlcWordAddressMappingModel> tempMap = new List<PlcWordAddressMappingModel>();
                tempMap = WordMap.FindAll(m => m.DataName == typeUint.Name);

                string LSB = null;
                string MSB = null;

                if (tempMap[0].Word.WordAddress<tempMap[1].Word.WordAddress)
                {
                    LSB = PlcDMWords.Find(w => w.WordAddress == tempMap[0].Word.WordAddress).ValueStrHex;
                    MSB = PlcDMWords.Find(w => w.WordAddress == tempMap[1].Word.WordAddress).ValueStrHex;
                    //LSB = tempMap[0].Word.ValueStrHex;
                    //MSB = tempMap[1].Word.ValueStrHex;
                }
                else
                {
                    LSB = PlcDMWords.Find(w => w.WordAddress == tempMap[1].Word.WordAddress).ValueStrHex;
                    MSB = PlcDMWords.Find(w => w.WordAddress == tempMap[0].Word.WordAddress).ValueStrHex;
                    //LSB = tempMap[1].Word.ValueStrHex;
                    //MSB = tempMap[0].Word.ValueStrHex;
                }
                typeUint.SetValueHex(LSB, MSB);
            }
        }

        public static void BitsToData()
        {
            foreach (var typeBool in TypeBools)
            {
                //PlcWordModel targetWord = new PlcWordModel();//new
                PlcBitModel targetBit = new PlcBitModel();
                targetBit = BitMap.Find(m => m.DataName == typeBool.Name).Bit;

                typeBool.SetValue(PlcBits.Find(b => b.MemArea == targetBit.MemArea
                && b.WordAddress == targetBit.WordAddress
                && b.BitAddress == targetBit.BitAddress).Value);

                //targetWord = PlcWRWords.FirstOrDefault(w => w.MemoryArea == targetBit.MemArea && w.WordAddress == targetBit.WordAddress);//new
                //if (targetWord!=null)
                //{
                //    typeBool.SetValue(targetWord.plcBits.Find(b => b.BitAddress == targetBit.BitAddress).Value);//new
                //}
                //else
                //{
                //    targetWord=PlcHRWords.FirstOrDefault(w => w.MemoryArea == targetBit.MemArea && w.WordAddress == targetBit.WordAddress);
                //    typeBool.SetValue(targetWord.plcBits.Find(b => b.BitAddress == targetBit.BitAddress).Value);//new
                //}

                
            }
        }

       

        /// <summary>
        /// Transfer the Data value as string and update the RList 
        /// </summary>
        public static void DataToRList()
        {
            foreach (var typeBool in TypeBools)
            {
                //DataNameValue_Rlist[typeBool.Name] = new RlistDataModel(typeBool.ValueStr, typeBool.AppCode);
                DataNameValue_Rlist[typeBool.Name].Value = typeBool.ValueStr;
            }

            foreach (var typeUshort in TypeUshorts)
            {
                //DataNameValue_Rlist[typeUshort.Name] = new RlistDataModel(typeUshort.ValueStr, typeUshort.AppCode);
                DataNameValue_Rlist[typeUshort.Name].Value = typeUshort.ValueStr;
            }

            foreach (var typeUint in TypeUints)
            {
                //DataNameValue_Rlist[typeUint.Name] = new RlistDataModel(typeUint.ValueStr, typeUint.AppCode);
                DataNameValue_Rlist[typeUint.Name].Value = typeUint.ValueStr;
            }
        }

        

        /// <summary>
        /// Generate a copy of words to be sent to PLC -- NEW
        /// </summary>
        /// <param name="retList">Empty word list</param>
        /// <returns>List of Words to be sent to PLC</returns>
        public static List<PlcWordModel> WListGenerateWordList(List<PlcWordModel> retList)
        {
            //List<PlcWordModel> retList = new List<PlcWordModel>();
            foreach (var WlistItem in DataNameValue_Wlist)
            {
                foreach (var wordMapping in WordMap)
                {
                    if (WlistItem.Key==wordMapping.DataName)
                    {
                        retList.Add(new PlcWordModel(wordMapping.Word.MemoryArea, 
                            wordMapping.Word.WordAddress, OmronFINsProcessor.HexConvert10(WlistItem.Value)));
                    }
                }
            }
            return retList;
        }

        /// <summary>
        /// Generate a copy of bits to be sent to PLC --NEW
        /// </summary>
        /// <param name="retList">Empty bit list</param>
        /// <returns>List of Bitss to be sent to PLC</returns>
        public static List<PlcBitModel> WListGenerateBitList(List<PlcBitModel> retList)//new
        {   
            foreach (var WlistItem in DataNameValue_Wlist)
            {
                foreach (var bitMapping in BitMap)
                {
                    if (WlistItem.Key == bitMapping.DataName)
                    {
                        retList.Add(new PlcBitModel(bitMapping.Bit.MemArea, bitMapping.Bit.WordAddress, 
                            bitMapping.Bit.BitAddress, WlistItem.Value));
                    }
                }
            }
            return retList;
        }

        

        /// <summary>
        /// The map between DataName <> Words -- NEW
        /// </summary>
        /// <param name="plcWordAddressMap">Map to be updated</param>
        /// <returns>Updated map</returns>
        public static List<PlcWordAddressMappingModel> PopulatePlcWordAddressMap(List<PlcWordAddressMappingModel> plcWordAddressMap)
        {
            plcWordAddressMap.Clear();
            PlcDMWords.Clear();
            //foreach (var record in PlcDataAddressRecords)
            //{
            //    //get the memory area code string
            //    string MemArea = record.PlcAddress.Substring(0, 1).ToUpper();
            //    if (MemArea=="D")
            //    {
            //        //get the word address as int
            //        int.TryParse(record.PlcAddress.Replace(MemArea, ""), out int WordAddress);

            //        PlcWordAddressMappingModel temp = new PlcWordAddressMappingModel
            //        {
            //            Word = new PlcWordModel(MemArea, WordAddress),
            //            DataName = record.DataName
            //        };

            //        plcWordAddressMap.Add(temp); 
            //    }
            //}

            foreach (var address in DataNameAddresses)
            {
                if (address.PLCAddress.Length>0)
                {
                    string MemArea = address.PLCAddress.Substring(0, 1).ToUpper();
                    if (MemArea == "D")
                    {
                        int.TryParse(address.PLCAddress.Replace(MemArea, ""), out int WordAddress);
                        plcWordAddressMap.Add(new PlcWordAddressMappingModel
                        {
                            Word = new PlcWordModel(MemArea, WordAddress),
                            DataName = address.Name
                        });
                        PlcDMWords.Add(new PlcWordModel(MemArea, WordAddress));
                    }
                }
                
            }
            return plcWordAddressMap;
        }

        /// <summary>
        /// The map between DataName <> Bits -- NEW
        /// </summary>
        /// <param name="plcBitAddressMap">Map to be updated</param>
        /// <returns>Updated map</returns>
        public static List<PlcBitAddressMappingModel> PopulatePlcBitAddressMap(List<PlcBitAddressMappingModel> plcBitAddressMap)
        {
            plcBitAddressMap.Clear();
            PlcBits.Clear();
            //foreach (var record in PlcDataAddressRecords)
            //{
            //    //get the memory area code string
            //    string MemArea = record.PlcAddress.Substring(0, 1).ToUpper();

            //    if (MemArea == "W" || MemArea == "H")
            //    {
            //        //get the word address as int
            //        int.TryParse(record.PlcAddress.Split('.')[0].Replace(MemArea, ""), out int WordAddress);
            //        //get the bit address as int
            //        int.TryParse(record.PlcAddress.Split('.')[1], out int BitAddress);

            //        PlcBitAddressMappingModel temp = new PlcBitAddressMappingModel
            //        {
            //            DataName = record.DataName,
            //            Bit = new PlcBitModel(MemArea, WordAddress, BitAddress)
            //        };

            //        plcBitAddressMap.Add(temp);
            //    }
            //}
            foreach (var address in DataNameAddresses)
            {
                //get the memory area code string
                if (address.PLCAddress.Length>0)
                {
                    string MemArea = address.PLCAddress.Substring(0, 1).ToUpper();

                    if (MemArea == "W" || MemArea == "H")
                    {
                        //get the word address as int
                        int.TryParse(address.PLCAddress.Split('.')[0].Replace(MemArea, ""), out int WordAddress);
                        //get the bit address as int
                        int.TryParse(address.PLCAddress.Split('.')[1], out int BitAddress);

                        PlcBitAddressMappingModel temp = new PlcBitAddressMappingModel
                        {
                            DataName = address.Name,
                            Bit = new PlcBitModel(MemArea, WordAddress, BitAddress)
                        };
                        plcBitAddressMap.Add(temp);
                        PlcBits.Add(new PlcBitModel(MemArea, WordAddress, BitAddress));
                    } 
                }
            }
            PlcBits = PlcBits.OrderBy(x => x.WordAddress).ToList();
            return plcBitAddressMap;
        }

        /// <summary>
        /// Create the full mapping for Uint type which takes two words --NEW
        /// </summary>
        /// <param name="plcWordAddressMappings">Initial mapping</param>
        /// <returns>Complete mapping</returns>
        public static List<PlcWordAddressMappingModel> GenerateFullWordAddressMap
            (List<PlcWordAddressMappingModel> plcWordAddressMappings)
        {
            foreach (var item in TypeUints)
            {
                PlcWordAddressMappingModel temp = plcWordAddressMappings.FirstOrDefault(x => x.DataName == item.Name);

                if (temp!=null)
                {
                    plcWordAddressMappings.Add(new PlcWordAddressMappingModel(item.Name,
                                new PlcWordModel(temp.Word.MemoryArea, temp.Word.WordAddress + 1))); 
                }
                PlcDMWords.Add(new PlcWordModel("D", temp.Word.WordAddress+1));
            }
            PlcDMWords = PlcDMWords.OrderBy(x => x.WordAddress).ToList();
            return plcWordAddressMappings;
        }

        /// <summary>
        /// Get the PLC address interval in SortedList{AddressBeging, AddressEnd}
        /// </summary>
        /// <param name="addrIntervalDM">DM interval</param>
        /// <param name="addrIntervalHR">HR interval</param>
        /// <param name="addrIntervalWR">WR interval</param>
        public static void GetAddressInverval(out SortedList<int,int> addrIntervalDM, 
            out SortedList<int, int> addrIntervalHR, out SortedList<int, int> addrIntervalWR)
        {
            addrIntervalDM = new SortedList<int, int>();
            addrIntervalHR = new SortedList<int, int>();
            addrIntervalWR = new SortedList<int, int>();
            
            for (int i = PlcDMWords[0].WordAddress;
                i <= PlcDMWords[PlcDMWords.Count - 1].WordAddress;
                i++)
            {
                int endIntervalAddr = i;
                if (PlcDMWords.FirstOrDefault(x => x.WordAddress == i) != null)
                {
                    do
                    {
                        endIntervalAddr++;
                        if (PlcDMWords.FirstOrDefault(x => x.WordAddress == endIntervalAddr) == null)
                        {
                            addrIntervalDM.Add(i, endIntervalAddr - 1);
                            i = endIntervalAddr;
                            break;
                        }
                    } while (true);
                }
            }

            List<PlcBitModel> plcBitsHR = PlcBits.Where(x => x.MemArea == "H").ToList();
            for (int i = plcBitsHR[0].WordAddress;
                i <= plcBitsHR[plcBitsHR.Count - 1].WordAddress;
                i++)
            {
                int endIntervalAddr = i;
                if (plcBitsHR.FirstOrDefault(x => x.WordAddress == i) != null)
                {
                    do
                    {
                        endIntervalAddr++;
                        if (plcBitsHR.FirstOrDefault(x => x.WordAddress == endIntervalAddr) == null)
                        {
                            addrIntervalHR.Add(i, endIntervalAddr - 1);
                            i = endIntervalAddr;
                            break;
                        }
                    } while (true);
                }
            }

            List<PlcBitModel> plcBitsWR = PlcBits.Where(x => x.MemArea == "W").ToList();
            for (int i = plcBitsWR[0].WordAddress;
                i <= plcBitsWR[plcBitsWR.Count - 1].WordAddress;
                i++)
            {
                int endIntervalAddr = i;
                if (plcBitsWR.FirstOrDefault(x => x.WordAddress == i) != null)
                {
                    do
                    {
                        endIntervalAddr++;
                        if (plcBitsWR.FirstOrDefault(x => x.WordAddress == endIntervalAddr) == null)
                        {
                            addrIntervalWR.Add(i, endIntervalAddr - 1);
                            i = endIntervalAddr;
                            break;
                        }
                    } while (true);
                }
            }
        }
    }
}
