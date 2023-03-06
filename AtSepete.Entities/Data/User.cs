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
    public class User:BaseUser
    {
     
        public Gender Gender { get; set; }
        public string Adress { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        //navigation property
        public IEnumerable<Order> Orders { get; set; }
        
    }
}
