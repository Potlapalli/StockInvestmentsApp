using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using StockInvestments.API.Helpers;
using StockInvestments.API.ValidationAttributes;

namespace StockInvestments.API.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class StockEarningForCreationDto :  StockEarningForManipulationDto /*: IValidatableObject*/
    {
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Ticker is required.")]
        public string Ticker { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (
        //        EarningsCallTime != nameof(EarningsCallTimeEnum.AM) &&
        //        EarningsCallTime != nameof(EarningsCallTimeEnum.PM))
        //    {
        //        yield return new ValidationResult("Invalid Earnings call time provided. The value should be AM or PM.", 
        //            new []{ "StockEarningForCreationDto" });
        //    }
        //}
    }
}
