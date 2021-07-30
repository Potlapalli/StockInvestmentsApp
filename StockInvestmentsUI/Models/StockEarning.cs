using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockInvestmentsUI.Models
{
    public class StockEarning
    {
        public string Ticker { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Earnings date is required.")]
        public DateTimeOffset EarningsDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(2)]
        public string EarningsCallTime { get; set; }
    }
}
