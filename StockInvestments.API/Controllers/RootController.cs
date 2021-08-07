using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using StockInvestments.API.Models;

namespace StockInvestments.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api")]
    [ApiController]
    public class RootController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetRoot")]
        public IActionResult GetRoot()
        {
            // create links for root
            var links = new List<LinkDto>
            {
                new LinkDto(Url.Link("GetRoot", new { }),
                    "self",
                    "GET"),

                new LinkDto(Url.Link("GetCurrentPositions", new { }),
                    "CurrentPositions",
                    "GET"),

                new LinkDto(Url.Link("CreateCurrentPosition", new { }),
                    "create_CurrentPosition",
                    "POST")
            };

            return Ok(links);

        }
    }
}