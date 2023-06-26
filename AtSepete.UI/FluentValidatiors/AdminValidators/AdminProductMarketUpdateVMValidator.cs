﻿using AtSepete.UI.Areas.Admin.Models.ProductMarketVMs;
using FluentValidation;

namespace AtSepete.UI.FluentValidatiors.AdminValidators
{
    public class AdminProductMarketUpdateVMValidator:AbstractValidator<AdminProductMarketUpdateVM>
    {
        public AdminProductMarketUpdateVMValidator()
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

              if (!int.TryParse(x.ToString(), out int result))
                  return false;

              return result > 0;
          })
            .WithMessage("{PropertyName} alanına yalnızca sayısal değer girilmelidir ve sıfırdan büyük  olmalıdır.");
        }
    }
    
}
