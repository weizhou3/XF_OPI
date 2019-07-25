using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XFOPI_Library.Models;
using XFOPI_Library.DataConnection.TextHelpers;
using XFOPI_Library.Models.SG2000;

namespace XFOPI_Library.DataConnection
{
    public class TextConnector : IDataConnection
    {
        private const string UsersFile = "UserModels.csv";

        /// <summary>
        /// Save a new user to Text file UsersFile.
        /// Load the file to list of models, add the new model, then overwrite file
        /// </summary>
        /// <param name="model">The new user's model</param>
        /// <returns>The new user's model, including unique identifier</returns>
        public UserModel CreateUser(UserModel model, UserAccessGroup uag)
        {
            //load text file and convert text to list model
            List<UserModel> users = UsersFile.FullFilePath().LoadFile().ConvertToUserModels();

            //find max id
            int currentId = 1;

            if (users.Count>0)
            {
                currentId = users.Max(x => x.id) + 1;//int currentId = users.OrderByDescending(p => p.id).First().id + 1;﻿
            }

            model.id = currentId;

            //add new record with new id max+1
            users.Add(model);

            //convert record to list string
            //save list string to text file, overwrite
            users.SaveToUserFile(UsersFile);

            return model;
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
            //TODO - GetUsers_All for text connector
            throw new NotImplementedException();
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
