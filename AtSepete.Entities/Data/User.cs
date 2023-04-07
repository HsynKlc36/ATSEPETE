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
        public DateTimeOffset? LockoutEnd { get; set; }// hesabı ne kadar süreyle kitleyeceğini belirler
        public bool LockoutEnabled { get; set; }// hesap kilitlensin mi bunu belirler
        public int AccessFailedCount { get; set; }// kaç kere hatalı girişte hasabın kilitleneceğini belirler
        //navigation property
        virtual public IEnumerable<Order>? Orders { get; set; }
        
    }
}
