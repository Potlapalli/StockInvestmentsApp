using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StockInvestments.API.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class SoldPosition
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        [ForeignKey("Ticker")]
        public CurrentPosition CurrentPosition { get; set; }
    }
}
