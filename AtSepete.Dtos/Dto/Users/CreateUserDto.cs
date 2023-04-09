using AtSepete.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Dtos.Dto.Users
{
    public class CreateUserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string? SecondName { get; set; }
        public string LastName { get; set; }
        public string? SecondLastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RetypedPassword { get; set; }// olabir ya da çıkarılabilir uı tarafına göre düzenlenecek
        public Role Role { get; set; } = Role.Customer;
        public Gender Gender { get; set; }
        public string Adress { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; } = true;
        public int AccessFailedCount { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
