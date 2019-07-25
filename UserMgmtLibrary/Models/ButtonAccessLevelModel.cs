using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFOPI_Library.Models
{
    public class ButtonAccessLevelModel
    {
        /// <summary>
        /// Unique line id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Unique ButtonName in this form 
        /// </summary>
        public string ButtonName { get; set; }
        /// <summary>
        /// Button displayed text
        /// </summary>
        public string ButtonText { get; set; }
        /// <summary>
        /// Form this button located
        /// </summary>
        public string Form { get; set; }
        /// <summary>
        /// If Admin can access 1 yes 0 no
        /// </summary>
        public string Admin { get; set; }
        /// <summary>
        /// If Maint can access 1 yes 0 no
        /// </summary>
        public string Maint { get; set; }
        /// <summary>
        /// If Operator can access 1 yes 0 no
        /// </summary>
        public string Operator { get; set; }

    }
}
