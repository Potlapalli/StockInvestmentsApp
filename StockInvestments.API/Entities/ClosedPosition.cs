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
    public class ClosedPosition
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
