using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockInvestments.API.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class StockEarningDto
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
        public DateTimeOffset EarningsDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string EarningsCallTime { get; set; }
    }
}
