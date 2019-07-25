using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFOPI_Library.Models.SG2000
{
    public class WarningCodeModel
    {
        public int id { get; set; }
        public string WarnCodeName { get; }
        public string WarnDescription { get; }

        public WarningCodeModel()
        {

        }
    }
}
