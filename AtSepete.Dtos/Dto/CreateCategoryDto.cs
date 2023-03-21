using AtSepete.Dtos.BaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Dtos.Dto
{
    public class CreateCategoryDto : BaseGenericDto
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public override DateTime? CreatedDate { get; set; }= DateTime.Now;

    }
}
