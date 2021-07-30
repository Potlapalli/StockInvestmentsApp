using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockInvestmentsUI.Models
{
    public class ClosedPosition
    {

        public long Number { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Ticker is required.")]
        public string Ticker { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "FinalValue is required.")]
        public double FinalValue { get; set; }
    }
}
