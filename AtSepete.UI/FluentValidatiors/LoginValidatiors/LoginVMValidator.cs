using AtSepete.UI.Models;
using FluentValidation;
using System.Text.RegularExpressions;

namespace AtSepete.UI.FluentValidatiors.LoginValidatiors
{
    public class LoginVMValidator : AbstractValidator<LoginVM>
    {
        public LoginVMValidator()
        {
            RuleFor(x => x.Email)
           .NotEmpty().WithName("Email")
           .WithMessage("{PropertyName} alanı boş bırakılamaz.")
           .Matches(new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
           .WithMessage("{PropertyName} formatında giriş yapınız.");

           // RuleFor(x => x.Password)
           //.NotEmpty()
           //.WithName("Şifre")
           //.WithMessage("Şifre alanı boş olamaz.")
           //.Matches(new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$"))
           //.WithMessage("Şifre en az 8 karakter uzunluğunda olmalı ve en az bir büyük harf, bir küçük harf ve bir rakam içermelidir.");
        }
    }
}
