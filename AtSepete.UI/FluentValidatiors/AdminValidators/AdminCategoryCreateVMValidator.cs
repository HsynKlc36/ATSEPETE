using AtSepete.UI.Areas.Admin.Models.CategoryVMs;
using FluentValidation;
using System.Text.RegularExpressions;

namespace AtSepete.UI.FluentValidatiors.AdminValidators
{
    public class AdminCategoryCreateVMValidator:AbstractValidator<AdminCategoryCreateVM>
    {
        public AdminCategoryCreateVMValidator()
        {
            RuleFor(x => x.Name)
           .NotEmpty()
           .WithName("Kategori Adı")
           .WithMessage("{PropertyName} alanı boş bırakılamaz.")
           .Length(2, 50)
           .WithName("Kategori Adı")
           .WithMessage("{PropertyName}  {MinLength} - {MaxLength} aralığında olmak zorundadır.")
           .Matches(new Regex(@"^[a-zA-ZıİğĞüÜşŞöÖçÇ\s\d\W]+$"))
           .WithMessage("lütfen Sadece harf,rakam ve özel karakter giriniz");

            RuleFor(x => x.Description)
         .NotEmpty()
         .WithName("Kategori Açıklaması")
         .WithMessage("{PropertyName} alanı boş bırakılamaz.")
         .Length(2, 150)
         .WithName("Kategori Açıklaması")
         .WithMessage("{PropertyName}  {MinLength} - {MaxLength} karakter aralığında olmak zorundadır.")
         .Matches(new Regex(@"^[a-zA-ZıİğĞüÜşŞöÖçÇ\s\d\W]+$"))
         .WithMessage("lütfen Sadece harf,rakam ve özel karakter giriniz");
        }
    }
}
