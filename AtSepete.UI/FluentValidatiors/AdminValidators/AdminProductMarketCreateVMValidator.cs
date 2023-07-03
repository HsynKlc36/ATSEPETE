using AtSepete.UI.Areas.Admin.Models.ProductMarketVMs;
using FluentValidation;
using System.Globalization;
using System.Text.RegularExpressions;

namespace AtSepete.UI.FluentValidatiors.AdminValidators
{
    public class AdminProductMarketCreateVMValidator:AbstractValidator<AdminProductMarketCreateVM>
    {
        public AdminProductMarketCreateVMValidator()
        {
            RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithName("Ürün Id")
            .WithMessage("{PropertyName} alanı boş bırakılamaz");

            RuleFor(x => x.MarketId)
           .NotEmpty()
           .WithName("Market Id")
           .WithMessage("{PropertyName} alanı boş bırakılamaz");

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


            RuleFor(x => x.Price)
           .NotEmpty()
          .WithName("Market Ürün Fiyatı")
          .WithMessage("{PropertyName} alanı boş bırakılamaz")
          .Must(x =>
          {
              if (x == null)
                  return false;

              var stringValue = x.ToString().Replace(',', '.');
              if (!decimal.TryParse(stringValue, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal result))
                  return false;

              return result > 0;

          })
            .WithMessage("{PropertyName} alanına yalnızca sayısal değer girilmelidir ve sıfırdan büyük  olmalıdır.");
        }
    }
}
