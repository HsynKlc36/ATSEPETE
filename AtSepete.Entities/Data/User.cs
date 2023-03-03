using AtSepete.Entities.BaseData;
using AtSepete.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Entities.Data
{
    public class User:Base
    {
        [Key]
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public Role Role  { get; set; }
        public string Password { get; set; }
        public Gender Gender { get; set; }
        public string Adress { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        //navigation property
        public IEnumerable<Order> Orders { get; set; }
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
        public string GetFullName()
        {
            return string.Join(" ", FirstName, SecondName, LastName, SecondLastName);
        }
    }
}
