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

namespace XFOPI_Library.DataConnection
{
    public class SqliteConnector : IDataConnection
    { 
        private const string db = "XFOPI_Sqlite";
        
        //LoadData<UserModel>("Select * from Users", null) = List<UserModel>
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
        /// Create a new user. Update both Users table, GroupMembers table
        /// </summary>
        /// <param name="model">The new user model</param>
        /// <returns></returns>
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

        public List<UserModel> GetUsers_All()
        {
            string sql = "select * from Users order by LastName";
            var userslist = LoadData<UserModel>(sql, new Dictionary<string, object>());
            return userslist;
        }
    }
}
