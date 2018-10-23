using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserMgmtLibrary;

namespace XF_OPI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Initialize database connection
            UserMgmtLibrary.GlobalConfig.InitializeConnections(DatabaseType.SqlServer);
            Application.Run(new MainForm());
        }
    }
}
