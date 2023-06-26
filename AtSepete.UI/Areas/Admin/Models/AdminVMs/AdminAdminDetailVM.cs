using AtSepete.Entities.Enums;
using System.ComponentModel;

namespace AtSepete.UI.Areas.Admin.Models.AdminVMs
{
    public class AdminAdminDetailVM
    {
        [DisplayName("Admin Id")]
        public Guid Id { get; set; }
        [DisplayName("Adı")]
        public string FirstName { get; set; }
        [DisplayName("İkinci Adı")]
        public string? SecondName { get; set; }
        [DisplayName("Soyadı")]
        public string LastName { get; set; }
        [DisplayName("İkinci Soyadı")]
        public string? SecondLastName { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Şifre")]
        public string Password { get; set; }
        [DisplayName("Cinsiyet")]
        public Gender Gender { get; set; }
        [DisplayName("Adres")]
        public string Adress { get; set; }
        [DisplayName("Doğum Tarihi")]
        public DateTime BirthDate { get; set; }
        [DisplayName("Telefon Numarası")]
        public string PhoneNumber { get; set; }
        [DisplayName("Rolü")]
        public Role Role { get; set; }
    }
}
