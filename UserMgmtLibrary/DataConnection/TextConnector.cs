using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMgmtLibrary.Models;
using UserMgmtLibrary.DataConnection.TextHelpers;

namespace UserMgmtLibrary.DataConnection
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
        public UserModel CreateUser(UserModel model)
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

        public List<UserModel> GetUsers_All()
        {
            throw new NotImplementedException();
        }
    }
}
