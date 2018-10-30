using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XFOPI_Library.Models;

namespace XFOPI_Library.DataConnection
{
    public interface IDataConnection
    {
        UserModel CreateUser(UserModel model, UserAccessGroup uag);
        List<UserModel> GetUsers_All();
        List<T> LoadData<T>(string sqlStatement, Dictionary<string, object> parameters, string connectionName);
        void SaveData(string sqlStatement, Dictionary<string, object> parameters, string connectionName);
    }
}
