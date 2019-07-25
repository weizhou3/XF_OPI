using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XFOPI_Library.Models;
using XFOPI_Library.Models.SG2000;

namespace XFOPI_Library.DataConnection
{
    public interface IDataConnection
    {
        UserModel CreateUser(UserModel model, UserAccessGroup uag);
        List<GroupModel> GetGroups_All();
        List<UserModel> GetUsers_All();
        List<ButtonAccessLevelModel> GetButtonsAccessLevel_All();
        List<TypeUintModel> GetTypeUint_All();
        List<TypeBoolModel> GetTypeBool_All();
        List<TypeUshortModel> GetTypeUshort_All();
        List<PlcDataAddressRecordModel> GetDataAddressRecord_All();
        List<PlcDataAddressRecordModel> GetTypeBoolAddressRecord_All();
        List<PlcDataAddressRecordModel> GetTypeUshortAddressRecord_All();
        List<PlcDataAddressRecordModel> GetTypeUintAddressRecord_All();
        List<AlarmCodeModel> GetAlarmCodes_All();
        List<WarningCodeModel> GetWarningCodes_All();
        List<PlcDataNameModel> GetAllDataNames();
        List<PlcDataNameAddressModel> GetAllDataAddresses();
        List<PlcPortSettingModel> GetPlcPortSetting_All();
        bool DeleteUser(UserModel model);
        List<T> LoadData<T>(string sqlStatement, Dictionary<string, object> parameters, string connectionName);
        void SaveData(string sqlStatement, Dictionary<string, object> parameters, string connectionName);
    }
}
