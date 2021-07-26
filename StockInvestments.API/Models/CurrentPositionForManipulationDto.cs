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
    public abstract class CurrentPositionForManipulationDto
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual string Company { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "PurchasePrice is required.")]
        public double PurchasePrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "TotalShares is required.")]
        public double TotalShares { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "TotalAmount is missing.")]
        public double TotalAmount { get; set; }
    }
}
