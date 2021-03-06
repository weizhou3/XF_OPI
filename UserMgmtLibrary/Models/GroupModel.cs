﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFOPI_Library.Models
{
    public class GroupModel
    {
        /// <summary>
        /// Unique identifier of the Group table.
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// The users in the group
        /// </summary>
        public List<UserModel> GroupMembers { get; set; } = new List<UserModel>();

        /// <summary>
        /// The name of the User Group
        /// </summary>
        public string GroupName { get; set; }

    }
}
