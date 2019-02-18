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
            switch (UserSettings.Default.DatabaseType)
            {
                case "SqlServer":
                    GlobalConfig.InitializeDBConnections(DatabaseType.SqlServer);
                    break;
                case "Sqlite":
                    GlobalConfig.InitializeDBConnections(DatabaseType.Sqlite);
                    break;
                case "TextFile":
                    GlobalConfig.InitializeDBConnections(DatabaseType.TextFiles);
                    break;
                default:
                    GlobalConfig.InitializeDBConnections(DatabaseType.Sqlite);
                    break;
            }

            //Initialize Tester IF setting
            switch (UserSettings.Default.TesterIFType)
            {
                case "NIGPIB":
                    if (UserSettings.Default.TesterIFProtocol == "MTGPIB")
                        GlobalConfig.InitializeIFConnections(TesterIFType.NIGPIB, TesterIFProtocol.MTGPIB);
                    if (UserSettings.Default.TesterIFProtocol == "RSGPIB")
                        GlobalConfig.InitializeIFConnections(TesterIFType.NIGPIB, TesterIFProtocol.RSGPIB);
                    break;

                case "RS232":
                    GlobalConfig.InitializeIFConnections(TesterIFType.RS232, TesterIFProtocol.RSRS232);
                    break;

                case "TTL":
                    break;

                default:
                    break;
            }

            //TODO - need to add GPIB/RS232 initialization and move initialization call to START button 
            
            Application.Run(new MainForm());
        }
    }
}
