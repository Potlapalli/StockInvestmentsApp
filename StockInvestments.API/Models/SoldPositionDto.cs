using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockInvestments.API.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class SoldPositionDto
    {
        /// <summary>
        /// 
        /// </summary>
        public long Number { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double SellingPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double TotalShares { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Ticker { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double TotalAmount { get; set; }
    }
}
