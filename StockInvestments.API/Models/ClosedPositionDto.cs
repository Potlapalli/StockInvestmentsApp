using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockInvestments.API.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ClosedPositionDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string Ticker { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double FinalValue { get; set; }
    }
}
