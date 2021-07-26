using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockInvestments.API.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class CurrentPosition
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string Ticker { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Company { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        public ICollection<SoldPosition> SoldPositions { get; set; } = new List<SoldPosition>();
    }
}
