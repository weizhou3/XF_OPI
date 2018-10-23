using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMgmtLibrary.Models;

//*load text file
//*convert text to list model
//find max id
//add new record with new id max+1
//convert record to list string
//save list string to text file, overwrite

namespace UserMgmtLibrary.DataConnection.TextHelpers
{
    public static class TextConnectorProcessor
    {
        public static string FullFilePath(this string fileName)
        {
            return $"{ConfigurationManager.AppSettings["filepath"] }\\{ fileName }";
        }

        public static List<string> LoadFile(this string file)
        {
            if (!File.Exists(file))
            {
                return new List<string>();
            }

            return File.ReadAllLines(file).ToList();
        }

        public static List<UserModel> ConvertToUserModels(this List<string> lines)
        {
            List<UserModel> output = new List<UserModel>();
            foreach (string line in lines)
            {
                string[] cols = line.Split(',');
                UserModel u = new UserModel();
                u.id = int.Parse(cols[0]);//crash if cols is non integer
                u.FirstName = cols[1];
                u.LastName = cols[2];
                u.EmailAddress = cols[3];
                u.PhoneNumber = cols[4];
                u.EmployeeID = cols[5];
                output.Add(u);
            }

            return output;
        }

        public static void SaveToUserFile(this List<UserModel> models,string fileName)
        {
            List<string> lines = new List<string>();

            foreach (UserModel u in models)
            {
                lines.Add($"{u.id},{u.FirstName},{u.LastName},{u.EmailAddress},{u.PhoneNumber},{u.EmployeeID}");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);

        }
    }
}
