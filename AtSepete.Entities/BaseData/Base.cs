using AtSepete.Entities.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Entities.BaseData
{
    public abstract class Base
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }
        public DateTime? CreatedDate { get; set; } 
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool  IsActive { get; set; }=true; //dto'larda değil burada veriyoruz kalanları dto'larda map'layarak veritabanına ekliyoruz
        public virtual string GetFullName()
        {
            return string.Join("", "");
        }
    }
}
