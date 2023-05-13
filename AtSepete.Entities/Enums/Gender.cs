using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AtSepete.Entities.Enums
{
    public enum Gender
    {
        [Display(Name = "Erkek")]
        Male,
        [Display(Name = "Kadın")]
        Female
    }
}
