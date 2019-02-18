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
        List<TypeUintModel> GetTypeUint_All();
        List<TypeBoolModel> GetTypeBool_All();
        List<TypeUshortModel> GetTypeUshort_All();
        List<AlarmCodeModel> GetAlarmCodes_All();
        bool DeleteUser(UserModel model);
        List<T> LoadData<T>(string sqlStatement, Dictionary<string, object> parameters, string connectionName);
        void SaveData(string sqlStatement, Dictionary<string, object> parameters, string connectionName);
    }
}
