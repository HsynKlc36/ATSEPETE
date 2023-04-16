using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Dtos.Dto.Users
{
    public class PasswordSignDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; } = false;
        public int AccessFailedCount { get; set; }
    }
}
