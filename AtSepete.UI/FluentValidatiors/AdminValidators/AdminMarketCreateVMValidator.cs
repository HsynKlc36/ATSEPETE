using AtSepete.UI.Areas.Admin.Models.MarketVMs;
using FluentValidation;
using System.Text.RegularExpressions;

namespace AtSepete.UI.FluentValidatiors.AdminValidators
{
    public class AdminMarketCreateVMValidator:AbstractValidator<AdminMarketCreateVM>
    {
        public AdminMarketCreateVMValidator()
        {
            RuleFor(x => x.MarketName)
          .NotEmpty()
          .WithName("Market Adı")
          .WithMessage("{PropertyName} alanı boş bırakılamaz.")
          .Length(2, 50)
          .WithName("Market Adı")
          .WithMessage("{PropertyName}  {MinLength} - {MaxLength} aralığında olmak zorundadır.")
          .Matches(new Regex(@"^[a-zA-ZıİğĞüÜşŞöÖçÇ\s\d\W]+$"))
          .WithMessage("lütfen Sadece harf,rakam ve özel karakter giriniz");

            RuleFor(x => x.Description)
         .NotEmpty()
         .WithName("Market Açıklaması")
         .WithMessage("{PropertyName} alanı boş bırakılamaz.")
         .Length(2, 150)
         .WithName("Market Açıklaması")
         .WithMessage("{PropertyName}  {MinLength} - {MaxLength} karakter aralığında olmak zorundadır.")
         .Matches(new Regex(@"^[a-zA-ZıİğĞüÜşŞöÖçÇ\s\d\W]+$"))
         .WithMessage("lütfen Sadece harf,rakam ve özel karakter giriniz");

            RuleFor(x => x.PhoneNumber)
           .NotEmpty()
            .WithName("Telefon Numarası")
           .WithMessage("{PropertyName} boş bırakılamaz.")
           .Matches(new Regex(@"^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$"))
           .WithMessage("Lütfen geçerli bir telefon numarası giriniz.");

            RuleFor(x => x.Adress)
           .NotEmpty().WithName("Adres")
           .WithMessage("{PropertyName} alanı boş bırakılamaz.")
           .MaximumLength(150).WithMessage("{PropertyName}, en fazla 150 karakter uzunluğunda olmalıdır.");



        }
    }
}
