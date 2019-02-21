using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XFOPI_Library.DataConnection;
using XFOPI_Library.Models;

namespace XFOPI_Library
{
    public static class Utility
    {
        //public static void CreateDataModels_All()
        //{

        //}

        /// <summary>
        /// Create an empty array of T with fixed indexes
        /// </summary>
        /// <typeparam name="T">data of type T</typeparam>
        /// <param name="count">lengths of the array</param>
        /// <returns>Output array</returns>
        //public static T[] CreateArray<T>(int count) where T : new()
        //{
        //    var array = new T[count];
        //    for (var i = 0; i < count; i++)
        //    {
        //        array[i] = new T();
        //    }
        //    return array;
        //}

        /// <summary>
        /// Load the DataName and PlcAddress relationship into the mapping list, extract Word/Bit address info
        /// </summary>
        /// <param name="plcDataAddressRecords">DataName and PlcAddress records list, raw from DB</param>
        /// <param name="plcDataAddressMappings">Empty mapping list</param>
        /// <returns>Filled mapping list, with Word/Bit address info</returns>
        //public static List<PlcDataAddressMappingModel> LoadPlcDataAddressMappings(List<PlcDataAddressRecordModel> plcDataAddressRecords,
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
        //        int BitAddress;
        //        int.TryParse(Record.PlcAddress.Split('.')[1], out BitAddress);

        //        PlcDataAddressMappingModel temp = new PlcDataAddressMappingModel();
        //        temp.PlcDataAddressRecord = Record;
        //        temp.PlcAddress.MemoryArea = MemArea;
        //        temp.PlcAddress.WordAddress = WordAddress;
        //        temp.PlcAddress.BitAddress = BitAddress;

        //        plcDataAddressMappings.Add(temp);

        //    }
        //    return plcDataAddressMappings;
        //}


    
        //public static 



        //public static List<TypeBool> InstantiateTypeBoolValues()
        //{
        //    //TODO: load TypeBool names from DB, then instantiate instance fo TypeBool
        //}



    }
}
