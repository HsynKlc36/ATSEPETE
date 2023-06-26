using AtSepete.UI.Models;
using FluentValidation;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Text.RegularExpressions;

namespace AtSepete.UI.FluentValidatiors.LoginValidatiors
{
    public class RegisterVMValidator:AbstractValidator<RegisterVM>
    {
        public RegisterVMValidator()
        {
            RuleFor(x => x.FirstName)
          .NotEmpty()
          .WithName("Adı")
          .WithMessage("{PropertyName} alanı boş bırakılamaz.")
          .Length(2, 15)
          .WithName("Adı")
          .WithMessage("{PropertyName}  {MinLength} - {MaxLength} aralığında olmak zorundadır.")
          .Matches(new Regex(@"^[a-zA-ZıüöçşğİÜÖÇŞĞ]+$"))
           .WithMessage("lütfen Sadece harf girişi yapınız");

            RuleFor(x => x.SecondName)
                .Length(2, 15)
                .WithName("İkinci Adı")
                .WithMessage("{PropertyName}  {MinLength} - {MaxLength} aralığında olmak zorundadır.")
                .Matches(new Regex(@"^[a-zA-ZıüöçşğİÜÖÇŞĞ]+$"))
                .WithMessage("lütfen Sadece harf girişi yapınız");
            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithName("Soyadı")
                .WithMessage("{PropertyName} alanı boş bırakılamaz.")
                .Length(2, 15)
                .WithName("Soyadı")
                .WithMessage("{PropertyName}  {MinLength} - {MaxLength} aralığında olmak zorundadır.")
                .Matches(new Regex(@"^[a-zA-ZıüöçşğİÜÖÇŞĞ]+$"))
               .WithMessage("lütfen Sadece harf girişi yapınız");

            RuleFor(x => x.SecondLastName)
                .Length(2, 15)
                .WithName("İkinci Soyadı")
                .WithMessage("{PropertyName}  {MinLength} - {MaxLength} aralığında olmak zorundadır.")
                .Matches(new Regex(@"^[a-zA-ZıüöçşğİÜÖÇŞĞ]+$"))
               .WithMessage("lütfen Sadece harf girişi yapınız");

            RuleFor(x => x.Gender)
                .NotEmpty()
               .WithName("Cinsiyet")
               .WithMessage("{PropertyName} alanı boş bırakılamaz.");

            RuleFor(x => x.BirthDate)
             .NotEmpty()
             .WithName("Doğum Tarihi")
             .WithMessage("{PropertyName} alanı boş bırakılamaz.")
             .Custom((date, context) =>
             {
                 DateTime from = new DateTime(1940, 1, 1);
                 DateTime to = DateTime.Now.AddYears(-12);
                 if (date < from)
                     context.AddFailure($"Doğum Tarihi {from.Date} tarihinden sonra olmalı");
                 else if (date > to)
                     context.AddFailure($"Doğum Tarihi {to.Day}.{to.Month}.{to.Year} tarihinden önce olmalı");
             });

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                 .WithName("Telefon Numarası")
                .WithMessage("{PropertyName} boş bırakılamaz.")
                .Matches(new Regex(@"^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$"))
                .WithMessage("Lütfen geçerli bir telefon numarası giriniz.");

            RuleFor(x => x.Password)
            .NotEmpty()
            .WithName("Şifre")
            .WithMessage("Şifre alanı boş olamaz.")
            .Matches(new Regex( @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$"))
            .WithMessage("Şifre en az 8 karakter uzunluğunda olmalı ve en az bir büyük harf, bir küçük harf ve bir rakam içermelidir.");

            RuleFor(x => x.RetypedPassword)
                .NotEmpty()
                .WithName("Şifre Doğrulama")
                .WithMessage("Şifre doğrulama alanı boş olamaz.")
            .Matches(new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$"))
            .WithMessage("Şifre doğrulama en az 8 karakter uzunluğunda olmalı ve en az bir büyük harf, bir küçük harf ve bir rakam içermelidir.")
            .Equal(x => x.Password).WithMessage("Şifreler eşleşmiyor.");

            RuleFor(x => x.Email)
                .NotEmpty().WithName("Email")
               .WithMessage("{PropertyName} alanı boş bırakılamaz.")
               .Matches(new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
            .WithMessage("{PropertyName} formatında giriş yapınız.");

            RuleFor(x=>x.Adress)
            .NotEmpty().WithName("Adres")
            .WithMessage("{PropertyName} alanı boş bırakılamaz.")
             .MaximumLength(150).WithMessage("{PropertyName}, en fazla 150 karakter uzunluğunda olmalıdır.");

            //price gibi sayısal değerler must ile girilir
        }
    }
}
