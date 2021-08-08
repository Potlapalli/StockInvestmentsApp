using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StockInvestments.API.Contracts;
using StockInvestments.API.Entities;
using StockInvestments.API.Models;
using StockInvestments.API.Services;

namespace StockInvestments.API.Controllers
{
    /// <summary>
    /// SoldPositionsController
    /// </summary>
    [Route("api/currentPositions/{ticker}/soldPositions")]
    [ApiController]
    //[ResponseCache(CacheProfileName = "240SecondsCacheProfile")]
    public class SoldPositionsController : ControllerBase
    {
        private readonly ICurrentPositionsRepository _currentPositionsRepository;
        private readonly ISoldPositionsRepository _soldPositionsRepository;
        private readonly IClosedPositionsRepository _closedPositionsRepository;
        private readonly IMapper _mapper;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentPositionsRepository"></param>
        /// <param name="soldPositionsRepository"></param>
        /// <param name="closedPositionsRepository"></param>
        /// <param name="mapper"></param>
        public SoldPositionsController(ICurrentPositionsRepository currentPositionsRepository, ISoldPositionsRepository soldPositionsRepository, IClosedPositionsRepository closedPositionsRepository, IMapper mapper)
        {
            _currentPositionsRepository = currentPositionsRepository ??
                                          throw new ArgumentNullException(nameof(currentPositionsRepository));

            _soldPositionsRepository = soldPositionsRepository ??
                                       throw new ArgumentNullException(nameof(soldPositionsRepository));

            _closedPositionsRepository = closedPositionsRepository ??
                                         throw new ArgumentNullException(nameof(closedPositionsRepository));

            _mapper = mapper ??
                      throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// GetSoldPositionsForCurrentPosition
        /// </summary>
        /// <param name="ticker"></param>
        /// <returns>SoldPositionsForCurrentPosition</returns>
        /// <response code="200">Returns SoldPositions list</response>
        /// <response code="404">If the current position couldn't be found</response>
        //Get api/currentPositions/xxx/soldPositions
        [HttpGet(Name = "GetSoldPositionsForCurrentPosition")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<SoldPositionDto>> GetSoldPositionsForCurrentPosition(string ticker)
        {
            if (!_currentPositionsRepository.CurrentPositionExists(ticker))
            {
                return NotFound($"No current position found for the ticker {ticker}.");
            }
            var soldPositionsFromRepo = _soldPositionsRepository.GetSoldPositions(ticker);
            return Ok(_mapper.Map<IEnumerable<SoldPositionDto>>(soldPositionsFromRepo));
        }

        /// <summary>
        /// GetSoldPositions
        /// </summary>
        /// <returns>All SoldPositions</returns>
        /// <response code="200">Returns SoldPositions list</response>
      
        //Get api/soldPositions
        [Route("/api/soldPositions")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<SoldPositionDto>> GetSoldPositions()
        {
            var soldPositionsFromRepo = _soldPositionsRepository.GetSoldPositions();
            return Ok(_mapper.Map<IEnumerable<SoldPositionDto>>(soldPositionsFromRepo));
        }

        /// <summary>
        /// GetSharesRemaining
        /// </summary>
        /// <param name="ticker"></param>
        /// <returns>SharesRemaining</returns>
        /// <response code="200">Returns Shares remaining</response>
        /// <response code="404">If the Current Position couldn't be found</response>
        //Get api/currentPositions/xxx/soldPositions/sharesRemaining
        [Route("sharesRemaining")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<double> GetSharesRemaining(string ticker)
        {
            if (!_currentPositionsRepository.CurrentPositionExists(ticker))
            {
                return NotFound($"No current position found for the ticker {ticker}.");
            }

            var currentPosition = _currentPositionsRepository.GetCurrentPosition(ticker);
            var sharesRemaining = _soldPositionsRepository.GetSharesRemaining(currentPosition);
            return Ok(sharesRemaining);
        }

        /// <summary>
        /// GetPositionsInProfit
        /// </summary>
        /// <returns>PositionsInProfit</returns>
        /// <response code="200">Returns PositionsInProfit</response>      
        //Get api/positionsInProfit
        [Route("/api/positionsInProfit")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<string>> GetPositionsInProfit()
        {
            var currentPositions = _currentPositionsRepository.GetCurrentPositions().ToList();
            var positionsInProfit = _soldPositionsRepository.GetPositionsInProfit(currentPositions);
            return Ok(positionsInProfit);
        }

        /// <summary>
        /// GetSoldPositionsFilteredByTotalAmount
        /// </summary>
        /// <param name="totalAmountGreaterThan"></param>
        /// <returns>SoldPositionsFilteredByTotalAmount</returns>
        /// <response code="200">Returns SoldPositionsFilteredByTotalAmount</response> 
        //Get api/soldPositions/filterByTotalAmount?totalAmountGreaterThan=100
        [Route("/api/soldPositions/filterByTotalAmount")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<SoldPositionDto>> GetSoldPositionsFilteredByTotalAmount([FromQuery] double totalAmountGreaterThan)
        {
            var soldPositionsFromRepo = _soldPositionsRepository.GetSoldPositionsFilteredByTotalAmount(totalAmountGreaterThan);
            return Ok(_mapper.Map<IEnumerable<SoldPositionDto>>(soldPositionsFromRepo));
        }

        /// <summary>
        /// GetSoldPositionForCurrentPosition
        /// </summary>
        /// <param name="ticker"></param>
        /// <param name="number"></param>
        /// <returns>SoldPositionForCurrentPosition</returns>
        /// <response code="200">Returns the specific sold position</response>
        /// <response code="404">If the Current Position or sold position couldn't be found</response>
        //Get api/currentPositions/xxx/soldPositions/1
        [HttpGet("{number}", Name = "GetSoldPositionForCurrentPosition")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<SoldPositionDto> GetSoldPositionForCurrentPosition(string ticker, long number)
        {
            if (!_currentPositionsRepository.CurrentPositionExists(ticker))
            {
                return NotFound($"No current position found for the ticker {ticker}.");
            }

            var soldPositionFromRepo = _soldPositionsRepository.GetSoldPosition(ticker, number);
            if (soldPositionFromRepo == null)
                return NotFound("Sold Position couldn't be found.");

            return Ok(_mapper.Map<SoldPositionDto>(soldPositionFromRepo));
        }

        /// <summary>
        /// CreateSoldPosition
        /// </summary>
        /// <param name="ticker"></param>
        /// <param name="soldPosition"></param>
        /// <returns>Newly cretaed SoldPosition</returns>
        /// <response code="201">Returns the newly added sold position</response>
        /// <response code="404">If the current position is not found</response>
        /// <response code="204">If the Current Position is deleted</response>
        //Post api/currentPositions/xxx/soldPositions
        [HttpPost(Name = "CreateSoldPosition")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult CreateSoldPosition(string ticker, SoldPositionForCreationDto soldPosition)
        {
            if (!_currentPositionsRepository.CurrentPositionExists(ticker))
            {
                return NotFound($"No current position found for the ticker {ticker}.");
            }

            var soldPositionEntity = _mapper.Map<SoldPosition>(soldPosition);

            _soldPositionsRepository.Add(ticker, soldPositionEntity);
            _soldPositionsRepository.Save();

            if (AddClosedPositionBasedOnTotalShares(ticker, soldPositionEntity)) 
                return NoContent();
            
            var soldPositionToReturn = _mapper.Map<SoldPositionDto>(soldPositionEntity);
            return CreatedAtRoute("GetSoldPositionForCurrentPosition",
                new {ticker = ticker, number = soldPositionToReturn.Number},
                soldPositionToReturn);
        }

        /// <summary>
        /// UpdateSoldPositionForCurrentPosition
        /// </summary>
        /// <param name="ticker"></param>
        /// <param name="number"></param>
        /// <param name="soldPosition"></param>
        /// <returns></returns>
        /// <response code="204">If the sold position is updated</response>
        /// <response code="404">If the Current Position or sold position couldn't be found</response>
        /// <response code="201">If the sold position is created</response>
        //Put api/currentPositions/xxx/soldPositions/1
        [HttpPut("{number}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult UpdateSoldPositionForCurrentPosition(string ticker, long number, SoldPositionForUpdateDto soldPosition)
        {
            if (!_currentPositionsRepository.CurrentPositionExists(ticker))
            {
                return NotFound($"No current position found for the ticker {ticker}.");
            }

            var soldPositionFromRepo = _soldPositionsRepository.GetSoldPosition(ticker, number);
            if (soldPositionFromRepo == null)
            {
                var soldPositionEntity = _mapper.Map<SoldPosition>(soldPosition);
                soldPositionEntity.Number = number;

                _soldPositionsRepository.Add(ticker, soldPositionEntity);
                _soldPositionsRepository.Save();

                if (AddClosedPositionBasedOnTotalShares(ticker, soldPositionEntity))
                    return NoContent();

                var soldPositionToReturn = _mapper.Map<SoldPositionDto>(soldPositionEntity);
                return CreatedAtRoute("GetSoldPositionForCurrentPosition",
                    new { ticker = ticker, number = soldPositionToReturn.Number },
                    soldPositionToReturn);
            }

            _mapper.Map(soldPosition, soldPositionFromRepo);

            _soldPositionsRepository.Update(soldPositionFromRepo);
            _soldPositionsRepository.Save();

            if (AddClosedPositionBasedOnTotalShares(ticker, soldPositionFromRepo))
                return NoContent();

            return NoContent();
        }

        /// <summary>
        /// PartiallyUpdateSoldPositionForCurrentPosition
        /// </summary>
        /// <param name="ticker"></param>
        /// <param name="number"></param>
        /// <param name="patchDocument"></param>
        /// <returns></returns>
        /// <response code="204">If the sold position is updated</response>
        /// <response code="404">If the Current Position or sold position couldn't be found</response>
        /// <response code="201">If the sold position is created</response>
        //Patch api/currentPositions/xxx/soldPositions/1
        [HttpPatch("{number}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult PartiallyUpdateSoldPositionForCurrentPosition(string ticker, long number, JsonPatchDocument<SoldPositionForUpdateDto> patchDocument)
        {
            if (!_currentPositionsRepository.CurrentPositionExists(ticker))
            {
                return NotFound($"No current position found for the ticker {ticker}.");
            }

            var soldPositionFromRepo = _soldPositionsRepository.GetSoldPosition(ticker, number);
            if (soldPositionFromRepo == null)
            {
                var soldPositionForUpdateDto = new SoldPositionForUpdateDto();
                patchDocument.ApplyTo(soldPositionForUpdateDto, ModelState);

                if (!TryValidateModel(soldPositionForUpdateDto))
                {
                    return ValidationProblem(ModelState);
                }

                var soldPositionEntity = _mapper.Map<SoldPosition>(soldPositionForUpdateDto);
                soldPositionEntity.Number = number;

                _soldPositionsRepository.Add(ticker, soldPositionEntity);
                _soldPositionsRepository.Save();

                if (AddClosedPositionBasedOnTotalShares(ticker, soldPositionEntity))
                    return NoContent();
              
                var soldPositionToReturn = _mapper.Map<SoldPositionDto>(soldPositionEntity);
                return CreatedAtRoute("GetSoldPositionForCurrentPosition",
                    new { ticker = ticker, number = soldPositionToReturn.Number },
                    soldPositionToReturn);
            }

            var  soldPositionToPatch= _mapper.Map<SoldPositionForUpdateDto>(soldPositionFromRepo);
            patchDocument.ApplyTo(soldPositionToPatch, ModelState);

            if (!TryValidateModel(soldPositionToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(soldPositionToPatch, soldPositionFromRepo);

            _soldPositionsRepository.Update(soldPositionFromRepo);
            _soldPositionsRepository.Save();

            if (AddClosedPositionBasedOnTotalShares(ticker, soldPositionFromRepo))
                return NoContent();

            return NoContent();
        }

        
        /// <summary>
        /// DeleteSoldPositionForCurrentPosition
        /// </summary>
        /// <param name="ticker"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        /// <response code="204">If the sold position is delted</response>
        /// <response code="404">If the Current Position or sold position couldn't be found</response>
        //Delete api/currentPositions/xxx/soldPositions/1
        [HttpDelete("{number}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteSoldPositionForCurrentPosition(string ticker, long number)
        {
            if (!_currentPositionsRepository.CurrentPositionExists(ticker))
            {
                return NotFound($"No current position found for the ticker {ticker}.");
            }

            var soldPositionFromRepo = _soldPositionsRepository.GetSoldPosition(ticker, number);
            if (soldPositionFromRepo == null)
                return NotFound("Sold Position couldn't be found.");

            _soldPositionsRepository.Delete(soldPositionFromRepo);
            _soldPositionsRepository.Save();

            return NoContent();
        }

        /// <summary>
        /// DeleteSoldPositionsForCurrentPosition
        /// </summary>
        /// <param name="ticker"></param>
        /// <returns></returns>
        /// <response code="204">If the sold positions are deleted</response>
        /// <response code="404">If the Current Position or sold position couldn't be found</response>
        //Delete api/currentPositions/xxx/soldPositions
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteSoldPositionsForCurrentPosition(string ticker)
        {
            if (!_currentPositionsRepository.CurrentPositionExists(ticker))
            {
                return NotFound($"No current position found for the ticker {ticker}.");
            }

            var soldPositionsFromRepo = _soldPositionsRepository.GetSoldPositions(ticker);
            if (soldPositionsFromRepo == null || soldPositionsFromRepo.Count() == 0)
                return NotFound("Sold Positions couldn't be found.");

            foreach (var soldPositionFromRepo in soldPositionsFromRepo)
            {
                _soldPositionsRepository.Delete(soldPositionFromRepo);
                _soldPositionsRepository.Save();
            }
            
            return NoContent();
        }

        private bool AddClosedPositionBasedOnTotalShares(string ticker, SoldPosition soldPositionEntity)
        {
            var currentPosition = _currentPositionsRepository.GetCurrentPosition(ticker);

            var sharesRemaining = _soldPositionsRepository.GetSharesRemaining(currentPosition);

            if (sharesRemaining == 0)
            {
                var totalAmount = _soldPositionsRepository.GetSoldPositionsTotalAmount(ticker);
                var closedPosition = new ClosedPosition()
                {
                    Ticker = ticker,
                    Company = currentPosition.Company,
                    FinalValue = totalAmount - currentPosition.TotalAmount
                };
                _closedPositionsRepository.Add(closedPosition);
                _soldPositionsRepository.Save();
                _currentPositionsRepository.Delete(currentPosition);
                _soldPositionsRepository.Save();

                return true;
            }

            //var currentPosition = _currentPositionsRepository.GetCurrentPosition(ticker);

            //var sharesRemaining = _soldPositionsRepository.GetSharesRemaining(currentPosition);

            //if (sharesRemaining == 0)
            //{
            //    var totalAmount = _soldPositionsRepository.GetSoldPositionsTotalAmount(ticker);
            //    var closedPosition = new ClosedPosition()
            //    {
            //        Ticker = ticker,
            //        Company = currentPosition.Company,
            //        FinalValue = (totalAmount) / currentPosition.TotalShares - currentPosition.TotalAmount

            //    };
            //    _closedPositionsRepository.Add(closedPosition);
            //    _soldPositionsRepository.Save();
            //    _currentPositionsRepository.Delete(currentPosition);
            //    _soldPositionsRepository.Save();
            //}
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelStateDictionary"></param>
        /// <returns></returns>
        public override ActionResult ValidationProblem([ActionResultObjectValue] ModelStateDictionary modelStateDictionary)
        {
            var options = HttpContext.RequestServices.GetRequiredService<IOptions<ApiBehaviorOptions>>();
            return (ActionResult)options.Value.InvalidModelStateResponseFactory(ControllerContext);
        }
    }
}
