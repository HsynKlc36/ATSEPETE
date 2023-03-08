using AtSepete.Entities.BaseData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Entities.Data
{
    public class Category :Base
    {
        [Key]
        [Column(Order = 0)]
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //navigation property
        virtual public IEnumerable<Product>? Products { get; set; }

    }

}
