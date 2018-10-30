using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFOPI_Library
{
    public enum DatabaseType
    {
        SqlServer,
        Sqlite,
        TextFiles
    }

    public enum TesterIFType
    {
        NIGPIB,
        RS232
    }

    public enum TesterIFProtocol
    {
        MTGPIB,
        RSGPIB,
        RSRS232
    }

    public enum UserAccessGroup
    {
        Admin,
        Maint,
        Operator
    }
}
