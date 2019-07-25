using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFOPI_Library.PLCConnection
{
    class OldDataMapper_not_in_use_
    {
        /// <summary>
        /// Transfer the values in all words to all data -- OLD no need for now
        /// </summary>
        //public static void WordToData()
        //{
        //    foreach (var typeBool in TypeBools)
        //    {
        //        //1.Find DataName address using map
        //        //2.Get the value in WordsList using address
        //        PlcWordAddressModel tempAddress = new PlcWordAddressModel();
        //        tempAddress = PlcDataAddressMappings.Find(x => x.PlcDataAddressRecord.DataName == typeBool.Name).PlcWordAddress;

        //        PlcWordModel tempWord = new PlcWordModel();
        //        tempWord = PlcWords.Find(x => x.MemoryArea == tempAddress.MemoryArea && x.WordAddress == tempAddress.WordAddress);

        //        typeBool.SetValue(tempWord.Bits[(int)tempAddress.BitAddress]);                 
        //    }

        //    foreach (var typeUshort in TypeUshorts)
        //    {
        //        PlcWordAddressModel tempAddress = new PlcWordAddressModel();
        //        tempAddress = PlcDataAddressMappings.Find(x => x.PlcDataAddressRecord.DataName == typeUshort.Name).PlcWordAddress;

        //        PlcWordModel tempWord = new PlcWordModel();
        //        tempWord = PlcWords.Find(x => x.MemoryArea == tempAddress.MemoryArea && x.WordAddress == tempAddress.WordAddress);

        //        typeUshort.SetValueHex(tempWord.ValueStrHex);
        //    }

        //    foreach (var typeUint in TypeUints)
        //    {
        //        List<PlcDataAddressMappingModel> tempMappings = new List<PlcDataAddressMappingModel>();
        //        tempMappings = PlcDataAddressMappings.FindAll(x => x.PlcDataAddressRecord.DataName == typeUint.Name);

        //        List<PlcWordModel> tempWords = new List<PlcWordModel>();
        //        foreach (var mapping in tempMappings)
        //        {
        //            tempWords.Add(PlcWords.Find(x => x.MemoryArea == mapping.PlcWordAddress.MemoryArea 
        //            && x.WordAddress == mapping.PlcWordAddress.WordAddress));
        //        }

        //        if (tempWords[0].WordAddress < tempWords[1].WordAddress)
        //        {
        //            typeUint.SetValueHex(tempWords[0].ValueStrHex, tempWords[1].ValueStrHex);
        //        }
        //        else if (tempWords[1].WordAddress < tempWords[0].WordAddress)
        //        {
        //            typeUint.SetValueHex(tempWords[1].ValueStrHex, tempWords[0].ValueStrHex);
        //        }
        //    }
        //}

        /// <summary>
        /// Transfer the value in all data to all words -- OLD no need for now
        /// </summary>
        //public static void DataToWord()
        //{

        //    foreach (var typeBool in TypeBools)
        //    {
        //        PlcWordAddressModel tempAddress = new PlcWordAddressModel();
        //        tempAddress = PlcDataAddressMappings.Find(x => x.PlcDataAddressRecord.DataName == typeBool.Name).PlcWordAddress;

        //        PlcWordModel tempWord = new PlcWordModel();
        //        tempWord = PlcWords.Find(x => x.MemoryArea == tempAddress.MemoryArea && x.WordAddress == tempAddress.WordAddress);
        //        //TODO: consolidate above lines into a method. Same applies to Type Ushort and Uint

        //        tempWord.SetBitValue((int)tempAddress.BitAddress, typeBool.Value);
        //    }

        //    foreach (var typeUshort in TypeUshorts)
        //    {
        //        PlcWordAddressModel tempAddress = new PlcWordAddressModel();
        //        tempAddress = PlcDataAddressMappings.Find(x => x.PlcDataAddressRecord.DataName == typeUshort.Name).PlcWordAddress;

        //        PlcWordModel tempWord = new PlcWordModel();
        //        tempWord = PlcWords.Find(x => x.MemoryArea == tempAddress.MemoryArea && x.WordAddress == tempAddress.WordAddress);

        //        tempWord.SetValue(typeUshort.ValueHexStr);
        //    }

        //    foreach (var typeUint in TypeUints)
        //    {
        //        List<PlcDataAddressMappingModel> tempMappings = new List<PlcDataAddressMappingModel>();
        //        tempMappings = PlcDataAddressMappings.FindAll(x => x.PlcDataAddressRecord.DataName == typeUint.Name);

        //        List<PlcWordModel> tempWords = new List<PlcWordModel>();
        //        foreach (var mapping in tempMappings)
        //        {
        //            tempWords.Add(PlcWords.Find(x => x.MemoryArea == mapping.PlcWordAddress.MemoryArea
        //            && x.WordAddress == mapping.PlcWordAddress.WordAddress));
        //        }

        //        if (tempWords[0].WordAddress < tempWords[1].WordAddress)
        //        {
        //            tempWords[0].SetValue(typeUint.ValueHexStr.Substring(4, 4));//set LSB to low address word
        //            tempWords[1].SetValue(typeUint.ValueHexStr.Substring(0, 4));//set MSB to high address word
        //            //typeUint.SetValueHex(tempWords[0].ValueStrHex, tempWords[1].ValueStrHex);
        //        }
        //        else if (tempWords[1].WordAddress < tempWords[0].WordAddress)
        //        {
        //            tempWords[1].SetValue(typeUint.ValueHexStr.Substring(4, 4));//set LSB to low address word
        //            tempWords[0].SetValue(typeUint.ValueHexStr.Substring(0, 4));//set MSB to high address word
        //            //typeUint.SetValueHex(tempWords[1].ValueStrHex, tempWords[0].ValueStrHex);
        //        }


        //    }
        //}

        /// <summary>
        /// Transfer the Value string in Wlist to Data --OLD
        /// </summary>
        //public static void WListToData()
        //{
        //    foreach (var WlistKey in DataNameValue_Wlist)
        //    {
        //        foreach (var typeBool in TypeBools)
        //        {
        //            if (WlistKey.Key == typeBool.Name)
        //            {
        //                typeBool.SetValue(DataNameValue_Wlist[typeBool.Name]);
        //            }
        //        }
        //        foreach (var typeUshort in TypeUshorts)
        //        {
        //            if (WlistKey.Key == typeUshort.Name)
        //            {
        //                typeUshort.SetValueHex(DataNameValue_Wlist[typeUshort.Name]);
        //            }
        //        }
        //        foreach (var typeUint in TypeUints)
        //        {
        //            if (WlistKey.Key == typeUint.Name)
        //            {
        //                typeUint.SetValueHex(DataNameValue_Wlist[typeUint.Name]);
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// Polulate the mapping list with DataName and PlcAddress relationship, extract Word/Bit address info and store in PlcWordAddress model -- OLD
        /// </summary>
        /// <param name="plcDataAddressRecords">DataName and PlcAddress records list, raw from DB</param>
        /// <param name="plcDataAddressMappings">Empty mapping list</param>
        /// <returns>Filled mapping list, with Word/Bit address info</returns>
        //public static List<PlcDataAddressMappingModel> PopulatePlcDataAddressMappings(List<PlcDataAddressRecordModel> plcDataAddressRecords,
        //    List<PlcDataAddressMappingModel> plcDataAddressMappings)
        //{
        //    foreach (var Record in plcDataAddressRecords)
        //    {
        //        //get the memory area code string
        //        string MemArea = Record.PlcAddress.Substring(0, 1);

        //        //get the word address as int
        //        int WordAddress;
        //        int.TryParse(Record.PlcAddress.Split('.')[0].Replace(MemArea, ""), out WordAddress);

        //        //get the bit address as int
        //        int? BitAddress = null;

        //        if (Record.PlcAddress.Contains("."))
        //        {
        //            int bitAddress;
        //            //int.TryParse(Record.PlcAddress.Split('.')[1], out bitAddress);
        //            if (int.TryParse(Record.PlcAddress.Split('.')[1], out bitAddress))
        //            {
        //                BitAddress = (int?)bitAddress;
        //            }

        //            //BitAddress = int.TryParse(Record.PlcAddress.Split('.')[1], out bitAddress) ? BitAddress:bitAddress;
        //        }
        //        else
        //        {
        //            BitAddress = null;
        //        }
        //        PlcDataAddressMappingModel temp = new PlcDataAddressMappingModel();
        //        temp.PlcDataAddressRecord = Record;
        //        temp.PlcWordAddress.MemoryArea = MemArea;
        //        temp.PlcWordAddress.WordAddress = WordAddress;
        //        temp.PlcWordAddress.BitAddress = BitAddress;

        //        plcDataAddressMappings.Add(temp);

        //    }
        //    return plcDataAddressMappings;
        //}

        /// <summary>
        /// Create the full mapping for Uint type which takes two words --OLD
        /// </summary>
        /// <param name="AllTypeUint">The Unit list</param>
        /// <param name="plcDataAddressMappings">The mapping list to be updated</param>
        /// <returns>The complete mapping list</returns>
        //public static List<PlcDataAddressMappingModel> AppendFullUintAddressMappings
        //    (List<TypeUintModel> AllTypeUint, List<PlcDataAddressMappingModel> plcDataAddressMappings)
        //{
        //    List<PlcDataAddressMappingModel> tempList = new List<PlcDataAddressMappingModel>();
        //    //PlcDataAddressMappingModel temp = new PlcDataAddressMappingModel();
        //    int i = 1;
        //    int maxId = plcDataAddressMappings.Max(x => x.PlcDataAddressRecord.id);
        //    foreach (var item in AllTypeUint)
        //    {
        //        PlcDataAddressMappingModel temp =
        //            plcDataAddressMappings.Find(x => x.PlcDataAddressRecord.DataName == item.Name).DeepCopy();

        //        temp.PlcDataAddressRecord.id = maxId + i;
        //        temp.PlcWordAddress.WordAddress++;
        //        temp.PlcDataAddressRecord.PlcAddress = temp.PlcWordAddress.MemoryArea + temp.PlcWordAddress.WordAddress.ToString();
        //        tempList.Add(temp);
        //        i++;
        //    }
        //    plcDataAddressMappings.AddRange(tempList);
        //    return plcDataAddressMappings;
        //}

        /// <summary>
        /// OLD
        /// </summary>
        /// <param name="plcWordList"></param>
        /// <returns></returns>
        //public static List<PlcWordModel> PopulatePlcWordList_All(List<PlcWordModel> plcWordList)
        //{
        //   
        //    plcWordList = new List<PlcWordModel>();
        //    List<PlcWordModel> plcWRWordList = new List<PlcWordModel>();
        //    plcWRWordList = PopulatePlcWordList_singleType(plcWRWordList, OmronFINsClass.MemArea_WR, OmronFINsClass.StartingId_WR);

        //    
        //    List<PlcWordModel> plcHRWordList = new List<PlcWordModel>();
        //    plcHRWordList = PopulatePlcWordList_singleType(plcHRWordList, OmronFINsClass.MemArea_HR, OmronFINsClass.StartingId_HR);

        //    //TODO:Make length dynamic per DataAddress from DB
        //    List<PlcWordModel> plcDMWordList = new List<PlcWordModel>();
        //    plcDMWordList = PopulatePlcWordList_singleType(plcDMWordList, OmronFINsClass.MemArea_DM, OmronFINsClass.StartingId_DM);

        //    //List<PlcWordModel> plcWRWordList = CreateList<PlcWordModel>(OmronFINsClass.Size_WR);
        //    //List<PlcWordModel> plcHRWordList = CreateList<PlcWordModel>(OmronFINsClass.Size_HR);
        //    //List<PlcWordModel> plcDMWordList = CreateList<PlcWordModel>(OmronFINsClass.Size_DM);

        //    plcWordList.AddRange(plcWRWordList);
        //    plcWordList.AddRange(plcHRWordList);
        //    plcWordList.AddRange(plcDMWordList);

        //    //foreach (var item in plcWRWordList)
        //    //{
        //    //    item.SetMemoryArea(OmronFINsClass.MemArea_WR);
        //    //    item.SetWordAddress(WRid);
        //    //    WRid++;
        //    //}

        //    //foreach (var item in plcHRWordList)
        //    //{
        //    //    item.SetMemoryArea(OmronFINsClass.MemArea_HR);
        //    //    item.SetWordAddress(WRid);
        //    //    WRid++;
        //    //}

        //    //foreach (var item in plcHRWordList)
        //    //{
        //    //    item.SetMemoryArea(OmronFINsClass.MemArea_HR);
        //    //    item.SetWordAddress(WRid);
        //    //    WRid++;
        //    //}

        //    return plcWordList;
        //}


        /// <summary>
        /// Populate the WR,HR bit model and DM word model 
        /// </summary>
        //public void PopulatePlcMemList_All()
        //{
        //    
        //    //plcWordList = new List<PlcWordModel>();
        //    List<PlcBitModel> plcWRBitList = new List<PlcBitModel>();
        //    plcWRBitList = PopulatePlcWordList_singleType(plcWRWordList, OmronFINsClass.MemArea_WR, OmronFINsClass.StartingId_WR);

        //    
        //    List<PlcWordModel> plcHRWordList = new List<PlcWordModel>();
        //    plcHRWordList = PopulatePlcWordList_singleType(plcHRWordList, OmronFINsClass.MemArea_HR, OmronFINsClass.StartingId_HR);

        //    //TODO:Make length dynamic per DataAddress from DB
        //    List<PlcWordModel> plcDMWordList = new List<PlcWordModel>();
        //    plcDMWordList = PopulatePlcWordList_singleType(plcDMWordList, OmronFINsClass.MemArea_DM, OmronFINsClass.StartingId_DM);

        //    //List<PlcWordModel> plcWRWordList = CreateList<PlcWordModel>(OmronFINsClass.Size_WR);
        //    //List<PlcWordModel> plcHRWordList = CreateList<PlcWordModel>(OmronFINsClass.Size_HR);
        //    //List<PlcWordModel> plcDMWordList = CreateList<PlcWordModel>(OmronFINsClass.Size_DM);

        //    PlcDataMapper.PlcWords.AddRange(plcWRWordList);
        //    PlcDataMapper.PlcWords.AddRange(plcHRWordList);
        //    PlcDataMapper.PlcWords.AddRange(plcDMWordList);

        //    //foreach (var item in plcWRWordList)
        //    //{
        //    //    item.SetMemoryArea(OmronFINsClass.MemArea_WR);
        //    //    item.SetWordAddress(WRid);
        //    //    WRid++;
        //    //}

        //    //foreach (var item in plcHRWordList)
        //    //{
        //    //    item.SetMemoryArea(OmronFINsClass.MemArea_HR);
        //    //    item.SetWordAddress(WRid);
        //    //    WRid++;
        //    //}

        //    //foreach (var item in plcHRWordList)
        //    //{
        //    //    item.SetMemoryArea(OmronFINsClass.MemArea_HR);
        //    //    item.SetWordAddress(WRid);
        //    //    WRid++;
        //    //}


        //}
    }
}
