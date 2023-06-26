using AtSepete.UI.Areas.Admin.Models.OrderDetailVMs;
using FluentValidation;

namespace AtSepete.UI.FluentValidatiors.AdminValidators
{
    public class AdminOrderDetailUpdateVMValidator:AbstractValidator<AdminOrderDetailUpdateVM>
    {
        public AdminOrderDetailUpdateVMValidator()
        {
            RuleFor(x => x.Amount)
            .NotEmpty()
            .WithName("Sipariş Ürün Adedi")
            .WithMessage("{PropertyName} alanı boş bırakılamaz.")
            .Must(x => x > 0)
            .WithMessage("{PropertyName} 0 dan büyük olmalıdır");
        }
    }
}
