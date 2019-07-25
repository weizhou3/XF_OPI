using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XFOPI_Library.Models;
using XFOPI_Library.Models.SG2000;

namespace XFOPI_Library.DataConnection
{
    public class SqlConnector : IDataConnection
    {
        /// <summary>
        /// Save a new user to database.
        /// </summary>
        /// <param name="model">The new user's model.</param>
        /// <returns>The new user's model, including the unique indentifier.</returns>
        /// 

        private const string db = "XFOPI_SqlServer";

        public UserModel CreateUser(UserModel model,  UserAccessGroup uag)
        {
            using (IDbConnection connection=new System.Data.SqlClient.SqlConnection(GlobalConfig.LoadConnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@FirstName", model.FirstName);
                p.Add("@LastName", model.LastName);
                p.Add("@EmailAddress", model.EmailAddress);
                p.Add("@PhoneNumber", model.PhoneNumber);
                p.Add("@EmployeeID", model.EmployeeID);
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spUsers_AddNew", p, commandType: CommandType.StoredProcedure);

                model.id = p.Get<int>("@id");

                return model;
            }
        }

        public bool DeleteUser(UserModel model)
        {
            throw new NotImplementedException();
        }

        public List<AlarmCodeModel> GetAlarmCodes_All()
        {
            throw new NotImplementedException();
        }

        public List<PlcDataNameAddressModel> GetAllDataAddresses()
        {
            throw new NotImplementedException();
        }

        public List<PlcDataNameModel> GetAllDataNames()
        {
            throw new NotImplementedException();
        }

        public List<ButtonAccessLevelModel> GetButtonsAccessLevel_All()
        {
            throw new NotImplementedException();
        }

        public List<PlcDataAddressRecordModel> GetDataAddressRecord_All()
        {
            throw new NotImplementedException();
        }

        public List<GroupModel> GetGroups_All()
        {
            throw new NotImplementedException();
        }

        public List<PlcPortSettingModel> GetPlcPortSetting_All()
        {
            throw new NotImplementedException();
        }

        public List<PlcDataAddressRecordModel> GetTypeBoolAddressRecord_All()
        {
            throw new NotImplementedException();
        }

        public List<TypeBoolModel> GetTypeBool_All()
        {
            throw new NotImplementedException();
        }

        public List<PlcDataAddressRecordModel> GetTypeUintAddressRecord_All()
        {
            throw new NotImplementedException();
        }

        public List<TypeUintModel> GetTypeUint_All()
        {
            throw new NotImplementedException();
        }

        public List<PlcDataAddressRecordModel> GetTypeUshortAddressRecord_All()
        {
            throw new NotImplementedException();
        }

        public List<TypeUshortModel> GetTypeUshort_All()
        {
            throw new NotImplementedException();
        }

        public List<UserModel> GetUsers_All()
        {
            List<UserModel> output;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.LoadConnString(db)))
            {
                output = connection.Query<UserModel>("dbo.spUsers_GetAll").ToList();
            }

            return output;
        }

        public List<WarningCodeModel> GetWarningCodes_All()
        {
            throw new NotImplementedException();
        }

        public List<T> LoadData<T>(string sqlStatement, Dictionary<string, object> parameters, string connectionName)
        {
            throw new NotImplementedException();
        }

        public void SaveData(string sqlStatement, Dictionary<string, object> parameters, string connectionName)
        {
            throw new NotImplementedException();
        }
    }
}
