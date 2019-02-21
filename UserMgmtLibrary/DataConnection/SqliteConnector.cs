using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XFOPI_Library.Models;
using XFOPI_Library.Models.SG2000;

namespace XFOPI_Library.DataConnection
{
    public class SqliteConnector : IDataConnection
    { 
        private const string db = "XFOPI_Sqlite";
        
        /// <summary>
        /// Generic load data from Sqlite DB
        /// </summary>
        /// <typeparam name="T">Generic data type</typeparam>
        /// <param name="sqlStatement">SQL command script in Sqlite</param>
        /// <param name="parameters">Parameters to pass in</param>
        /// <param name="connectionName">Sqlite DB name</param>
        /// <returns></returns>
        public List<T> LoadData<T>(string sqlStatement, Dictionary<string, object> parameters, string connectionName=db)
        {
            //connectionName = db;
            DynamicParameters p = parameters.ToDynamicParameters();

            using (IDbConnection cnn = new SQLiteConnection(GlobalConfig.LoadConnString(connectionName)))
            {
                var rows = cnn.Query<T>(sqlStatement, p);
                return rows.ToList();
            }
        }
        /// <summary>
        /// Generic save data to Sqlite DB
        /// </summary>
        /// <param name="sqlStatement">SQL command script in Sqlite</param>
        /// <param name="parameters">Parameters to pass in</param>
        /// <param name="connectionName">Sqlite DB name</param>
        public void SaveData(string sqlStatement, Dictionary<string, object> parameters, string connectionName=db)
        {
            //connectionName = db;
            DynamicParameters p = parameters.ToDynamicParameters();

            using (IDbConnection cnn = new SQLiteConnection(GlobalConfig.LoadConnString(connectionName)))
            {
                cnn.Execute(sqlStatement, p);
            }
        }

        /// <summary>
        /// Create a new user under specific access group. Update both Users table, GroupMembers table
        /// </summary>
        /// <param name="model">The new user model</param>
        /// <param name="uag">The user access group</param>
        /// <returns>The complete User Model</returns>
        public UserModel CreateUser(UserModel model, UserAccessGroup uag)
        {
            //1.prep model data for save
            string groupName;
            switch (uag)
            {
                case UserAccessGroup.Admin:
                    groupName = "'Admin'";
                    break;
                case UserAccessGroup.Maint:
                    groupName = "'Maint'";
                    break;
                case UserAccessGroup.Operator:
                    groupName = "'Operator'";
                    break;
                default:
                    groupName = "'Operator'";
                    break;
            }

            int groupId;
            string sql1 = "insert into Users (FirstName, LastName, EmailAddress, PhoneNumber, EmployeeID) " +
                         "values (@FirstName, @LastName, @EmailAddress, @PhoneNumber, @EmployeeID)";
            string sql2 = "SELECT max(id) from Users";//find out the lastest inserted row id in the This Connection instance
            string sql3 = $"SELECT id from Groups where GroupName = {groupName}";
            string sql4 = "insert into GroupMembers (GroupId, UserId) " + "values (@GroupId, @UserId)";

            Dictionary<string, object> parameters1 = new Dictionary<string, object>
            {
                { "@FirstName", model.FirstName },
                { "@LastName", model.LastName},
                { "@EmailAddress", model.EmailAddress},
                { "@PhoneNumber", model.PhoneNumber},
                { "@EmployeeID", model.EmployeeID}
            };
            
            //2. call save data to Users table
            SaveData(sql1, parameters1);
            
            //3. get lastest user index
            model.id = LoadData<int>(sql2, new Dictionary<string, object>()).FirstOrDefault();

            //4. find out the index of Group
            groupId= LoadData<int>(sql3, new Dictionary<string, object>()).FirstOrDefault();

            //5. save data(indexes) to GroupMembers table
            Dictionary<string, object> parameters4 = new Dictionary<string, object>
            {
                { "@GroupId", groupId},
                { "@UserId", model.id}
            };
            SaveData(sql4, parameters4);
            

            return model;
        }

        /// <summary>
        /// Return list of all users with group information, order first by Group then by Last name 
        /// </summary>
        /// <returns></returns>
        public List<UserModel> GetUsers_All()
        {
            //1. Get all groups in Gourp table
            var grouplist = GetGroups_All();
            //List<int> groupidList = grouplist.Select(x => x.id).ToList();

            //2. Generate Users list with group info
            List<UserModel> userslist = new List<UserModel>();
            foreach (GroupModel group in grouplist)
            {
                //get users ID in group
                string sql2 = $"select UserId from GroupMembers where UserId is not null and GroupId = {group.id}";
                List<int> usersId = LoadData<int>(sql2, new Dictionary<string, object>());
                //usersId.RemoveAll(x => x == null);
                
                //get UserModels using users ID
                string sql3 = $"select * from Users where id in ({string.Join(",", usersId.Select(n => n.ToString()).ToArray())}) order by LastName";
                userslist.AddRange(LoadData<UserModel>(sql3, new Dictionary<string, object>()));

                //append group name to UserModel
                foreach (UserModel user in userslist)
                {
                    if(usersId.Contains(user.id))
                        user.Group = group.GroupName;
                }
            }

            //3. return list of UserModel with group info
            return userslist;
        }

        public bool DeleteUser(UserModel model)
        {
            string sql1 = $"delete from GroupMembers where UserId in ({model.id})";
            string sql2 = $"delete from Users where id in ({model.id})";
            SaveData(sql1, new Dictionary<string, object>());
            SaveData(sql2, new Dictionary<string, object>());
            return true;
        }

        public List<GroupModel> GetGroups_All()
        {
            string sql = "select * from Groups order by GroupName";
            var grouplist = LoadData<GroupModel>(sql, new Dictionary<string, object>());
            return grouplist;
        }

        /// <summary>
        /// Import all TypeBool Values from DB
        /// </summary>
        /// <returns>List of TypeBoolModels</returns>
        public List<TypeBoolModel> GetTypeBool_All()
        {
            string sql = "select * from TypeBool order by Name";
            var TypeBoollist = LoadData<TypeBoolModel>(sql, new Dictionary<string, object>());
            return TypeBoollist;
        }

        public List<TypeUintModel> GetTypeUint_All()
        {
            string sql = "select * from TypeUint order by Name";
            var TypeUintlist = LoadData<TypeUintModel>(sql, new Dictionary<string, object>());
            return TypeUintlist;
        }

        public List<TypeUshortModel> GetTypeUshort_All()
        {
            string sql = "select * from TypeUshort order by Name";
            var TypeUshortlist = LoadData<TypeUshortModel>(sql, new Dictionary<string, object>());
            return TypeUshortlist;
        }

        public List<AlarmCodeModel> GetAlarmCodes_All()
        {
            string sql = "select * from AlarmCode order by AlarmCodeName";
            var TypeAlarmCodelist = LoadData<AlarmCodeModel>(sql, new Dictionary<string, object>());
            return TypeAlarmCodelist;
        }

        public List<PlcDataAddressRecordModel> GetTypeBoolAddressRecord_All()
        {
            string sql = "select * from TypeBoolAddress order by id";
            return LoadData<PlcDataAddressRecordModel>(sql, new Dictionary<string, object>());            
        }

        public List<PlcDataAddressRecordModel> GetTypeUshortAddressRecord_All()
        {
            string sql = "select * from TypeUshortAddress order by DataName";
            return LoadData<PlcDataAddressRecordModel>(sql, new Dictionary<string, object>());
        }

        public List<PlcDataAddressRecordModel> GetTypeUintAddressRecord_All()
        {
            string sql = "select * from TypeUintAddress order by DataName";
            return LoadData<PlcDataAddressRecordModel>(sql, new Dictionary<string, object>());
        }
    }
}
