using AtSepete.UI.Areas.Admin.Models.ProductVMs;
using FluentValidation;
using System.Text.RegularExpressions;

namespace AtSepete.UI.FluentValidatiors.AdminValidators
{
    public class AdminProductUpdateVMValidator:AbstractValidator<AdminProductUpdateVM>
    {
        public AdminProductUpdateVMValidator()
        {


            RuleFor(x => x.ProductName)
         .NotEmpty()
         .WithName("Ürün Adı")
         .WithMessage("{PropertyName} alanı boş bırakılamaz.")
         .Length(2, 50)
         .WithName("Ürün Adı")
         .WithMessage("{PropertyName}  {MinLength} - {MaxLength} aralığında olmak zorundadır.")
         .Matches(new Regex(@"^[a-zA-ZıİğĞüÜşŞöÖçÇ\s\d\W]+$"))
         .WithMessage("lütfen Sadece harf,rakam ve özel karakter giriniz");

            RuleFor(x => x.Description)
       .NotEmpty()
       .WithName("Ürün Açıklaması")
       .WithMessage("{PropertyName} alanı boş bırakılamaz.")
       .Length(2, 150)
       .WithName("Ürün Açıklaması")
       .WithMessage("{PropertyName}  {MinLength} - {MaxLength} karakter aralığında olmak zorundadır.")
       .Matches(new Regex(@"^[a-zA-ZıİğĞüÜşŞöÖçÇ\s\d\W]+$"))
       .WithMessage("lütfen Sadece harf,rakam ve özel karakter giriniz");

            RuleFor(x => x.Barcode)
                .NotEmpty()
                .WithName("Barkod")
                .WithMessage("{PropertyName} alanı boş bırakılamaz.")
                 .Matches(new Regex(@"^[0-9]{13}$"))
                 .WithMessage("Lütfen sadece 13 haneli rakam giriniz.");

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithName("Ürün Markası")
                .WithMessage("{PropertyName} alanı boş bırakılamaz.")
               .Matches(new Regex(@"^[a-zA-ZıİğĞüÜşŞöÖçÇ\s\d\W]+$"))
               .WithMessage("lütfen Sadece harf,rakam ve özel karakter giriniz");

            RuleFor(x => x.Unit)
               .NotEmpty()
               .WithName("Ürün Birimi")
               .WithMessage("{PropertyName} alanı boş bırakılamaz.")
              .Matches(new Regex(@"^[a-zA-Z]+$"))
              .WithMessage("lütfen Sadece harf giriniz");

            RuleFor(x => x.Quantity)
               .NotEmpty()
               .WithName("Ürün Miktarı")
               .WithMessage("{PropertyName} alanı boş bırakılamaz.")
               .Matches(new Regex(@"^[0-9\.,]+$"))
               .WithMessage("lütfen Sadece sayı giriniz");

            RuleFor(x => x.CategoryId)
              .NotEmpty()
              .WithName("Kategori Adı")
              .WithMessage("{PropertyName} alanı boş bırakılamaz.");
        }
    }
}
