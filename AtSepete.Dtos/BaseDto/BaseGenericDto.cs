using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Dtos.BaseDto
{
    public class BaseGenericDto
    {
        public DateTime? CreatedDate { get; set; } 
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
