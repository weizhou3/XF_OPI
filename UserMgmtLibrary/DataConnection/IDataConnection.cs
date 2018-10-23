using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMgmtLibrary.Models;

namespace UserMgmtLibrary.DataConnection
{
    public interface IDataConnection
    {
        UserModel CreateUser(UserModel model);
        List<UserModel> GetUsers_All();
    }
}
