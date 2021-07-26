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
    public class CurrentPositionForUpdateDto : CurrentPositionForManipulationDto
    {
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Company name is required.")]
        public override string Company { get => base.Company; set => base.Company = value; }

    }
}
