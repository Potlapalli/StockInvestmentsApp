using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockInvestments.API.Contracts;
using StockInvestments.API.Entities;
using StockInvestments.API.Helpers;
using StockInvestments.API.Models;
using StockInvestments.API.Services;

namespace StockInvestments.API.Controllers
{
    /// <summary>
    /// StockEarningsController
    /// </summary>
    [Route("api/stockEarnings")]
    [ApiController]
    public class StockEarningsController : ControllerBase
    {
        private readonly IStockEarningsRepository _stockEarningsRepository;
        private readonly IMapper _mapper;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stockEarningsRepository"></param>
        /// <param name="mapper"></param>
        public StockEarningsController(IStockEarningsRepository stockEarningsRepository, IMapper mapper)
        {
            _stockEarningsRepository = stockEarningsRepository ??
                                       throw new ArgumentNullException(nameof(stockEarningsRepository));
            _mapper = mapper ??
                      throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// GetStockEarnings
        /// </summary>
        /// <returns>StockEarnings</returns>
        /// <response code="200">Returns list of stock earnings</response>
        //Get api/stockEarnings
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<StockEarningDto>> GetStockEarnings()
        {
            var stockEarningsFromRepo = _stockEarningsRepository.GetStockEarnings();
            return Ok(_mapper.Map<IEnumerable<StockEarningDto>>(stockEarningsFromRepo));
        }

        /// <summary>
        /// GetStockEarningsFilteredByDate
        /// </summary>
        /// <param name="date"></param>
        /// <returns>StockEarningsFilteredByDate</returns>
        ///  <response code="200">Returns list of stock earnings filtered by date</response>
        //Get api/stockEarnings/filterByDate?Date=xxx
        [Route("filterByDate")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<StockEarningDto>> GetStockEarningsFilteredByDate([FromQuery] DateTimeOffset date)
        {
            var stockEarningsFromRepo = _stockEarningsRepository.GetStockEarningsFilteredByDate(date);
            return Ok(_mapper.Map<IEnumerable<StockEarningDto>>(stockEarningsFromRepo));
        }

        /// <summary>
        /// GetStockEarning
        /// </summary>
        /// <param name="ticker"></param>
        /// <returns>StockEarning</returns>
        /// <response code="200">Returns the specific stock earning</response>
        /// <response code="400">If the ticker is invalid</response>
        /// <response code="404">If the stock earning couldn't be found</response>
        //Get api/stockEarnings/xxx
        [HttpGet("{ticker}", Name = "GetStockEarning")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<StockEarningDto> GetStockEarning(string ticker)
        {
            if (string.IsNullOrEmpty(ticker))
                return BadRequest("Invalid ticker");

            var stockEarningFromRepo = _stockEarningsRepository.GetStockEarning(ticker);
            if (stockEarningFromRepo == null)
                return NotFound("Stock Earning couldn't be found.");

            return Ok(_mapper.Map<StockEarningDto>(stockEarningFromRepo));
        }

        /// <summary>
        /// CreateStockEarning
        /// </summary>
        /// <param name="stockEarning"></param>
        /// <returns>Newly created StockEarning</returns>
        /// <response code="201">New stock earning created</response>
        /// <response code="400">If the stock earning is null</response>
        //Post api/stockEarnings
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<StockEarningDto> CreateStockEarning(StockEarningForCreationDto stockEarning)
        {
            var stockEarningEntity = _mapper.Map<StockEarning>(stockEarning);
            //if (!string.IsNullOrEmpty(stockEarningEntity.EarningsCallTime) &&
            //    stockEarningEntity.EarningsCallTime != nameof(EarningsCallTime.AM) &&
            //    stockEarningEntity.EarningsCallTime != nameof(EarningsCallTime.PM)) 
            //    return BadRequest("Invalid Earnings call time provided. The value should be AM or PM.");

            _stockEarningsRepository.Add(stockEarningEntity);
            _stockEarningsRepository.Save();

            var stockEarningToReturn = _mapper.Map<StockEarningDto>(stockEarningEntity);
            return CreatedAtRoute("GetStockEarning", new { ticker = stockEarningToReturn.Ticker },
                stockEarningToReturn);
        }

        /// <summary>
        /// UpdateStockEarning
        /// </summary>
        /// <param name="ticker"></param>
        /// <param name="stockEarning"></param>
        /// <returns></returns>
        /// <response code="204">If the Stock earning is updated</response>
        /// <response code="400">If the ticker is invalid</response>
        /// <response code="404">If the Stock earning is not found</response>
        //Put api/stockEarnings/xxx
        [HttpPut("{ticker}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult UpdateStockEarning(string ticker, StockEarningForUpdateDto stockEarning)
        {
            if (string.IsNullOrEmpty(ticker))
                return BadRequest("Invalid ticker");

            var stockEarningFromRepo = _stockEarningsRepository.GetStockEarning(ticker);
            if (stockEarningFromRepo == null)
                return NotFound("Stock earning couldn't be found.");

            _mapper.Map(stockEarning, stockEarningFromRepo);

            _stockEarningsRepository.Update(stockEarningFromRepo);
            _stockEarningsRepository.Save();

            return NoContent();
        }
    }
}
