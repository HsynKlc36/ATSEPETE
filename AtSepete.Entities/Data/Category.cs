using AtSepete.Entities.BaseData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Entities.Data
{
    public class Category :Base
    {
        [Key]
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //navigation property
        public IEnumerable<Product> Products { get; set; }

    }

}
