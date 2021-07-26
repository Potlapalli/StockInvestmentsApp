using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using StockInvestments.API.Helpers;
using StockInvestments.API.Models;

namespace StockInvestments.API.ValidationAttributes
{
    public class EarningsCallTimeShouldBeAMOrPMAtrribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            var stockEarning = (StockEarningForManipulationDto)validationContext.ObjectInstance;

            if (stockEarning.EarningsCallTime != nameof(EarningsCallTimeEnum.AM) &&
                stockEarning.EarningsCallTime != nameof(EarningsCallTimeEnum.PM))
            {
                return new ValidationResult(ErrorMessage,
                    new[] { nameof(StockEarningForManipulationDto) });
            }

            return ValidationResult.Success;
        }
    }
}
