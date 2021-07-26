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
    public abstract class ClosedPositionForManipulationDto
    {
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Ticker is required.")]
        public string Ticker { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Company { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "FinalValue is required.")]
        public double FinalValue { get; set; }
    }
}
