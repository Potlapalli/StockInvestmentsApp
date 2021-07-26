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
    /// ClosedPositionsController
    /// </summary>
    [Route("api/closedPositions")]
    [ApiController]
    public class ClosedPositionsController : ControllerBase
    {
        private readonly IClosedPositionsRepository _closedPositionsRepository;
        private readonly IMapper _mapper;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="closedPositionsRepository"></param>
        /// <param name="mapper"></param>
        public ClosedPositionsController(IClosedPositionsRepository closedPositionsRepository, IMapper mapper)
        {
            _closedPositionsRepository = closedPositionsRepository ??
                                       throw new ArgumentNullException(nameof(closedPositionsRepository));
            _mapper = mapper ??
                      throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// GetClosedPositions
        /// </summary>
        /// <returns>ClosedPositions</returns>
        /// <response code="200">Returns list of closed positions</response>
        //Get api/ClosedPositions
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ClosedPositionDto>> GetClosedPositions()
        {
            var closedPositionsFromRepo = _closedPositionsRepository.GetClosedPositions();
            return Ok(_mapper.Map<IEnumerable<ClosedPositionDto>>(closedPositionsFromRepo));
        }

        /// <summary>
        /// GetClosedPositionsFilteredByFinalValue
        /// </summary>
        /// <param name="value"></param>
        /// <returns>ClosedPositionsFilteredByFinalValue</returns>
        /// <response code="200">Returns list of ClosedPositions filtered by final value</response>
        //Get api/ClosedPositions/filterByFinalValue?Value=100
        [Route("filterByFinalValue")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ClosedPositionDto>> GetClosedPositionsFilteredByFinalValue([FromQuery] double value)
        {
            var closedPositionsFromRepo = _closedPositionsRepository.GetClosedPositionsFilteredByFinalValue(value);
            return Ok(_mapper.Map<IEnumerable<ClosedPositionDto>>(closedPositionsFromRepo));
        }

        /// <summary>
        /// GetClosedPosition
        /// </summary>
        /// <param name="ticker"></param>
        /// <returns>ClosedPosition</returns>
        /// <response code="200">Returns the specific closed position</response>
        /// <response code="400">If the ticker is invalid</response>
        /// <response code="404">If the Closed Position couldn't be found</response>
        //Get api/ClosedPositions/xxx
        [HttpGet("{ticker}", Name = "GetClosedPosition")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ClosedPositionDto> GetClosedPosition(string ticker)
        {
            if (string.IsNullOrEmpty(ticker))
                return BadRequest("Invalid ticker");

            var closedPositionFromRepo = _closedPositionsRepository.GetClosedPosition(ticker);
            if (closedPositionFromRepo == null)
                return NotFound("Closed position couldn't be found.");

            return Ok(_mapper.Map<ClosedPositionDto>(closedPositionFromRepo));
        }

        /// <summary>
        /// CreateClosedPosition
        /// </summary>
        /// <param name="closedPosition"></param>
        /// <returns>Newly created ClosedPosition</returns>
        ///  <response code="201">New closed position created</response>
        /// <response code="400">If the closed position is null</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //Post api/ClosedPositions
        [HttpPost]
        public ActionResult<ClosedPositionDto> CreateClosedPosition(ClosedPositionForCreationDto closedPosition)
        {
            var closedPositionEntity = _mapper.Map<ClosedPosition>(closedPosition);
            _closedPositionsRepository.Add(closedPositionEntity);
            _closedPositionsRepository.Save();

            var closedPositionToReturn = _mapper.Map<ClosedPositionDto>(closedPositionEntity);
            return CreatedAtRoute("GetClosedPosition", new { ticker = closedPositionToReturn.Ticker },
                closedPositionToReturn);
        }

        /// <summary>
        /// UpdateClosedPosition
        /// </summary>
        /// <param name="ticker"></param>
        /// <param name="closedPosition"></param>
        /// <returns></returns>
        /// <response code="204">If the closed position is updated</response>
        /// <response code="400">If the ticker is invalid</response>
        /// <response code="404">If the Closed Position is not found</response>
        //Put api/closedPositions/xxx
        [HttpPut("{ticker}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult UpdateClosedPosition(string ticker, ClosedPositionForUpdateDto closedPosition)
        {
            if (string.IsNullOrEmpty(ticker))
                return BadRequest("Invalid ticker");

            var closedPositionFromRepo = _closedPositionsRepository.GetClosedPosition(ticker);
            if (closedPositionFromRepo == null)
                return NotFound("Closed Position couldn't be found.");

            _mapper.Map(closedPosition, closedPositionFromRepo);

            _closedPositionsRepository.Update(closedPositionFromRepo);
            _closedPositionsRepository.Save();

            return NoContent();
        }
    }
}
