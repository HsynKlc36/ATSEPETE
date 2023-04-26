using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AtSepete.Dtos.Dto.Users
{
    public class NewPasswordDto
    {      
        public string Email { get; set; }
        public string Password { get; set; }      
        public string SecondPassword { get; set; }            
        public string Token { get; set; }            
    }
}
