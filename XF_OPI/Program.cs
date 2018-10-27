using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using XFOPI_Library;

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
            
            GlobalConfig.InitializeDBConnections(DatabaseType.Sqlite);
            //TODO - need to add GPIB/RS232 initialization and move initialization call to START button 
            GlobalConfig.InitializeIFConnections(TesterIFType.NIGPIB);
            Application.Run(new MainForm());
        }
    }
}
