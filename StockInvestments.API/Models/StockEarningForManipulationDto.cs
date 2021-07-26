using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using StockInvestments.API.ValidationAttributes;

namespace StockInvestments.API.Models
{
    /// <summary>
    /// 
    /// </summary>
    [EarningsCallTimeShouldBeAMOrPMAtrribute(ErrorMessage = "Invalid Earnings call time provided. The value should be AM or PM")]
    public abstract class StockEarningForManipulationDto
    {      
        /// <summary>
        /// 
        /// </summary>
        public virtual string Company { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Earnings date is required.")]
        public DateTimeOffset EarningsDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(2)]
        public virtual string EarningsCallTime { get; set; }
    }
}
