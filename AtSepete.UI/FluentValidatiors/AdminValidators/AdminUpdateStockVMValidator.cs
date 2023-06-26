using AtSepete.UI.Areas.Admin.Models.StockVMs;
using FluentValidation;

namespace AtSepete.UI.FluentValidatiors.AdminValidators
{
    public class AdminUpdateStockVMValidator : AbstractValidator<AdminUpdateStockVM>
    {
        public AdminUpdateStockVMValidator()
        {
            RuleFor(x => x.Stock)
           .NotEmpty()
           .WithName("Market Stok Adedi")
           .WithMessage("{PropertyName} alanı boş bırakılamaz")
           .Must(x =>
           {
                if (x == null)
                 return false;

                if (!int.TryParse(x.ToString(), out int result))
                 return false;

               return result > 0;
           })
       .WithMessage("{PropertyName} alanına yalnızca sayısal değer girilmelidir ve sıfırdan büyük  olmalıdır.");
        }
    }
}
