using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StockInvestmentsUI.Models
{
    public class SoldPosition
    {
        public long Number { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Ticker is missing.")]
        public string Ticker { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public CurrentPosition CurrentPosition { get; set; }
    }
}
