using AtSepete.UI.Models;
using FluentValidation;
using System.Text.RegularExpressions;

namespace AtSepete.UI.FluentValidatiors.LoginValidatiors
{
    public class ForgetPasswordVMValidator : AbstractValidator<ForgetPasswordVM>
    {
        public ForgetPasswordVMValidator()
        {
            RuleFor(x => x.Email)
           .NotEmpty().WithName("Email")
           .WithMessage("{PropertyName} alanı boş bırakılamaz.")
           .Matches(new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
           .WithMessage("{PropertyName} formatında giriş yapınız.");
        }
    }
}
