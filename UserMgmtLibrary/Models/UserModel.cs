using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFOPI_Library.Models
{
    public class UserModel
    {
        /// <summary>
        /// Unique identifier of the User table.
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// User's first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// User's last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// User's email address.
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// User's phone number.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// User's employee ID.
        /// </summary>
        public string EmployeeID { get; set; }

        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        public UserModel()
        {

        }
        public UserModel(string FN, string LN, string Email, string Phone, string employeeID)
        {
            FirstName = FN;
            LastName = LN;
            EmailAddress = Email;
            PhoneNumber = Phone;
            EmployeeID = employeeID;
        }
    }


}
