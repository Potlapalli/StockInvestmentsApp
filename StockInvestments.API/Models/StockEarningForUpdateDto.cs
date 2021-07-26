using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockInvestments.API.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class StockEarningForUpdateDto : StockEarningForManipulationDto
    {
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Company name is required")]
        public override string Company { get => base.Company; set => base.Company = value; }
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "EarningsCallTime is required")]
        [MaxLength(2)]
        public override string EarningsCallTime { get => base.EarningsCallTime; set => base.EarningsCallTime = value; }
    }
}
