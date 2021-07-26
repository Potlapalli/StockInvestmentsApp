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
    public class CurrentPositionForCreationDto : CurrentPositionForManipulationDto
    {
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Ticker is required.")]
        public string Ticker { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<SoldPositionForCreationDto> SoldPositions { get; set; }
            = new List<SoldPositionForCreationDto>();
    }
}
