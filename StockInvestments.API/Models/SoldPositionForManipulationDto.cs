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
    public abstract class SoldPositionForManipulationDto
    {
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "SellingPrice is required.")]
        public double SellingPrice { get; set; }

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
