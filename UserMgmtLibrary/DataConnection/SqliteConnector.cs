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

        public UserModel CreateUser(UserModel model)
        {
            //1.prep model data for save
            string sql = "insert into Users (FirstName, LastName, EmailAddress, PhoneNumber, EmployeeID) " +
                         "values (@FirstName, @LastName, @EmailAddress, @PhoneNumber, @EmployeeID)";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@FirstName", model.FirstName },
                { "@LastName", model.LastName},
                { "@EmailAddress", model.EmailAddress},
                { "@PhoneNumber", model.PhoneNumber},
                { "@EmployeeID", model.EmployeeID}
            };
            //2. call save data to sqlite
             SaveData(sql, parameters);

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
