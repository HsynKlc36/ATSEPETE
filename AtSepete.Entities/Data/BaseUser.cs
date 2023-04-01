using AtSepete.Entities.BaseData;
using AtSepete.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Entities.Data
{
    public abstract class BaseUser:Base
    {
        
        public string FirstName { get; set; }
        public string? SecondName { get; set; }
        public string LastName { get; set; }
        public string? SecondLastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }
        public Role Role { get; set; }
        public string GetFullNameShort()
        {

            string lastNameShort = string.Empty;
            string nameShort = string.Empty;
            if (!string.IsNullOrEmpty(SecondName))
            {
                nameShort = SecondName[0].ToString() + ".";
            }
            if (!string.IsNullOrEmpty(SecondLastName))
            {
                lastNameShort = SecondLastName[0].ToString() + ".";
            }
            return string.Join(" ", FirstName, nameShort, LastName, lastNameShort);
        }
        public override string GetFullName()
        {
            return string.Join(" ", FirstName, SecondName, LastName, SecondLastName);
        }

    }
}
