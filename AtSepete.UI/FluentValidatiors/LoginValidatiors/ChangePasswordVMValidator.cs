using AtSepete.UI.Models;
using FluentValidation;
using System.Text.RegularExpressions;

namespace AtSepete.UI.FluentValidatiors.LoginValidatiors
{
    public class ChangePasswordVMValidator:AbstractValidator<ChangePasswordVM>
    {
        public ChangePasswordVMValidator()
        {
            RuleFor(x => x.Email)
      .NotEmpty().WithName("Email")
      .WithMessage("{PropertyName} alanı boş bırakılamaz.")
      .Matches(new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
      .WithMessage("{PropertyName} formatında giriş yapınız.");

            RuleFor(x => x.CurrentPassword)
       .NotEmpty()
       .WithName("Şifre")
       .WithMessage("Şifre alanı boş olamaz.")
       .Matches(new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$"))
       .WithMessage("Şifre en az 8 karakter uzunluğunda olmalı ve en az bir büyük harf, bir küçük harf ve bir rakam içermelidir.");

            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .WithName("Şifre Doğrulama")
                .WithMessage("Şifre doğrulama alanı boş olamaz.")
            .Matches(new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$"))
            .WithMessage("Şifre doğrulama en az 8 karakter uzunluğunda olmalı ve en az bir büyük harf, bir küçük harf ve bir rakam içermelidir.")
            .Must((model, newPassword) => newPassword != model.CurrentPassword)
            .WithMessage("Mevcut şifre ile Yeni şifre aynı olamaz.");
        }
    }
}
