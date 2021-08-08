using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StockInvestments.API.Contracts;
using StockInvestments.API.Entities;
using StockInvestments.API.Helpers;
using StockInvestments.API.Models;

namespace StockInvestments.API.Controllers
{
    /// <summary>
    /// CurrentPositionsController
    /// </summary>
    [Route("api/currentPositions")]
    [ApiController]
    [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 120)]
    [HttpCacheValidation(MustRevalidate = true)]
    //[ResponseCache(CacheProfileName = "240SecondsCacheProfile")]
    public class CurrentPositionsController : ControllerBase
    {
        private readonly ICurrentPositionsRepository _currentPositionsRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CurrentPositionsController> _logger;
        private readonly IPropertyCheckerService _propertyCheckerService;
        private readonly ILoggerRepo _loggerRepo;

       /// <summary>
       /// 
       /// </summary>
       /// <param name="currentPositionsRepository"></param>
       /// <param name="mapper"></param>
       /// <param name="logger"></param>
       /// <param name="loggerRepo"></param>
       /// <param name="propertyCheckerService"></param>
        public CurrentPositionsController(ICurrentPositionsRepository currentPositionsRepository, IMapper mapper, 
            ILogger<CurrentPositionsController> logger , ILoggerRepo loggerRepo, IPropertyCheckerService propertyCheckerService)
        {
            _currentPositionsRepository = currentPositionsRepository ??
                                          throw new ArgumentNullException(nameof(currentPositionsRepository));
            _mapper = mapper ?? 
                      throw new ArgumentNullException(nameof(mapper));

            _logger =  logger ??
                       throw new ArgumentNullException(nameof(logger));

            _loggerRepo = loggerRepo ??
                          throw new ArgumentNullException(nameof(loggerRepo));

            _propertyCheckerService = propertyCheckerService ??
                                      throw new ArgumentNullException(nameof(propertyCheckerService)); ;
        }

        /// <summary>
        /// Get Current Positions
        /// </summary>
        /// <returns>All CurrentPositions</returns>
        /// <response code="200">Returns CurrentPositions list</response>
        /// 
        //Get api/currentPositions
        [HttpGet( Name = "GetCurrentPositions")]
        // [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<CurrentPositionDto>> GetCurrentPositions()
        {
           var currentPositionsFromRepo =  _currentPositionsRepository.GetCurrentPositions();
           return Ok(_mapper.Map<IEnumerable<CurrentPositionDto>>(currentPositionsFromRepo));
        }

        /// <summary>
        /// GetCurrentPositionsFilteredByTotalAmount
        /// </summary>
        /// <param name="totalAmountGreaterThan"></param>
        /// <returns>CurrentPositionsFilteredByTotalAmount</returns>
        /// <response code="200">Returns list of CurrentPositions filtered by total amount</response>
        //Get api/currentPositions/filterByTotalAmount?totalAmountGreaterThan=100
        [Route("filterByTotalAmount")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<CurrentPositionDto>> GetCurrentPositionsFilteredByTotalAmount([FromQuery] double totalAmountGreaterThan)
        {
            var currentPositionsFromRepo = _currentPositionsRepository.GetCurrentPositionsFilteredByTotalAmount(totalAmountGreaterThan);
            return Ok(_mapper.Map<IEnumerable<CurrentPositionDto>>(currentPositionsFromRepo));
        }

        /// <summary>
        /// GetCurrentPosition
        /// </summary>
        /// <param name="ticker"></param>
        /// <param name="fields"></param>
        /// <returns>CurrentPosition</returns>
        /// <response code="200">Returns the specific current position</response>
        /// <response code="400">If the ticker is invalid</response>
        /// <response code="404">If the Current Position couldn't be found</response>
        //Get api/currentPositions/xxx
        //Get api/currentPositions/xxx?fields=ticker,company
        [HttpGet("{ticker}", Name = "GetCurrentPosition")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ResponseCache(Duration = 120)]
        public ActionResult<CurrentPositionDto> /*IActionResult */ GetCurrentPosition(string ticker, string fields)
        {
            if (string.IsNullOrEmpty(ticker))
                return BadRequest("Invalid ticker");

            if (!_propertyCheckerService.TypeHasProperties<CurrentPositionDto>(fields))
            {
                return BadRequest();
            }

            var currentPositionFromRepo = _currentPositionsRepository.GetCurrentPosition(ticker);
            if (currentPositionFromRepo == null)
                return NotFound("Current Position couldn't be found.");

            var links = CreateLinksForCurrentPosition(ticker, fields);

            var linkedResourceToReturn = _mapper.Map<CurrentPositionDto>(currentPositionFromRepo).ShapeData(fields)
                as IDictionary<string, object>;

             linkedResourceToReturn.Add("links", links);

             _loggerRepo.LogDebug("Get is Successful");

            return Ok(linkedResourceToReturn); 
        }

        /// <summary>
        /// GetCurrentPositionCollection
        /// </summary>
        /// <param name="tickers"></param>
        /// <returns>CurrentPositionCollection based on the tickers specified</returns>
        /// <response code="200">Returns the CurrentPositionCollection</response>
        /// <response code="400">If the ticker are invalid</response>
        /// <response code="404">If the Current Positions are not found</response>
        //Get api/currentPositions/xxx,yyy
        [HttpGet("({tickers})", Name = "GetCurrentPositionCollection")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<CurrentPositionDto>> GetCurrentPositionCollection([FromRoute]
            [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<string> tickers)
        {
            if (tickers == null)
                return BadRequest();

            var currentPositionsFromRepo = _currentPositionsRepository.GetCurrentPositions(tickers.ToList());
            if (tickers.Count() != currentPositionsFromRepo.Count())
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<CurrentPositionDto>>(currentPositionsFromRepo));
        }


        /// <summary>
        /// Creates a CurrentPosition
        /// </summary>
        /// <param name="currentPosition"></param>
        /// <returns>A newly created CurrentPosition</returns>
        /// <response code="201">Returns the newly created CurrentPosition</response>
        /// <response code="400">If the CurrentPosition is null</response>
        //Post api/currentPositions
        [HttpPost(Name = "CreateCurrentPosition")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CurrentPositionDto> CreateCurrentPosition(CurrentPositionForCreationDto currentPosition)
        {
            var currentPositionEntity = _mapper.Map<CurrentPosition>(currentPosition);
            _currentPositionsRepository.Add(currentPositionEntity);
            _currentPositionsRepository.Save();

            var currentPositionToReturn = _mapper.Map<CurrentPositionDto>(currentPositionEntity);

            var links = CreateLinksForCurrentPosition(currentPositionToReturn.Ticker, null);

            var linkedResourceToReturn = currentPositionToReturn.ShapeData(null)
                as IDictionary<string, object>;

             linkedResourceToReturn.Add("links", links);

            return CreatedAtRoute("GetCurrentPosition", new {ticker = linkedResourceToReturn["Ticker"]},
                linkedResourceToReturn);
        }

        /// <summary>
        /// CreateCurrentPositionCollection
        /// </summary>
        /// <param name="currentPositionCollection"></param>
        /// <returns>CurrentPositionCollection</returns>
        /// <response code="201">Returns the newly created CurrentPositions</response>
        /// <response code="400">If the CurrentPosition is null</response>
        //Post api/currentPositionsCollection
        [Route("/api/currentPositionsCollection")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<CurrentPositionDto>> CreateCurrentPositionCollection(IEnumerable<CurrentPositionForCreationDto> currentPositionCollection)
        {
            var currentPositionsEntity = _mapper.Map<IEnumerable<CurrentPosition>>(currentPositionCollection);
            foreach (var currentPositionEntity in currentPositionsEntity)
            {
                _currentPositionsRepository.Add(currentPositionEntity);
                _currentPositionsRepository.Save();
            }
            
            var currentPositionsToReturn = _mapper.Map<IEnumerable<CurrentPositionDto>>(currentPositionsEntity);
            var tickers = string.Join(",", currentPositionsToReturn.Select(cp => cp.Ticker));
            return CreatedAtRoute("GetCurrentPositionCollection", new { tickers = tickers },
                currentPositionsToReturn);
        }

        /// <summary>
        /// GetCurrentPositionsOptions
        /// </summary>
        /// <returns>CurrentPositionsOptions</returns>
        /// <response code="200">Http verbs supported</response>
        //Options api/currentPositions
        [HttpOptions]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetCurrentPositionsOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS,POST,PUT,DELETE");
            return Ok();
        }

        /// <summary>
        /// UpdateCurrentPosition
        /// </summary>
        /// <param name="ticker"></param>
        /// <param name="currentPosition"></param>
        /// <returns></returns>
        /// <response code="204">If the current position is updated</response>
        /// <response code="400">If the ticker are invalid</response>
        /// <response code="404">If the Current Positions are not found</response>
        //Put api/currentPositions/xxx
        [HttpPut("{ticker}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateCurrentPosition(string ticker, CurrentPositionForUpdateDto currentPosition)
        {
            if (string.IsNullOrEmpty(ticker))
                return BadRequest("Invalid ticker");

            var currentPositionFromRepo = _currentPositionsRepository.GetCurrentPosition(ticker);
            if (currentPositionFromRepo == null)
                return NotFound("Current Position couldn't be found.");

            _mapper.Map(currentPosition, currentPositionFromRepo);

            _currentPositionsRepository.Update(currentPositionFromRepo);
            _currentPositionsRepository.Save();

            return NoContent();
        }

        /// <summary>
        /// PartiallyUpdateCurrentPosition
        /// </summary>
        /// <param name="ticker"></param>
        /// <param name="patchDocument"></param>
        /// <returns></returns>
        /// <response code="204">If the current position is updated</response>
        /// <response code="400">If the ticker are invalid</response>
        /// <response code="404">If the Current Positions are not found</response>
        //Patch api/currentPositions/xxx
        [HttpPatch("{ticker}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PartiallyUpdateCurrentPosition(string ticker, JsonPatchDocument<CurrentPositionForUpdateDto> patchDocument)
        {
            if (string.IsNullOrEmpty(ticker))
                return BadRequest("Invalid ticker");

            var currentPositionFromRepo = _currentPositionsRepository.GetCurrentPosition(ticker);
            if (currentPositionFromRepo == null)
                return NotFound("Current Position couldn't be found.");

            var currentPositionToPatch = _mapper.Map<CurrentPositionForUpdateDto>(currentPositionFromRepo);
            patchDocument.ApplyTo(currentPositionToPatch , ModelState);

            if (!TryValidateModel(currentPositionToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(currentPositionToPatch, currentPositionFromRepo);

            _currentPositionsRepository.Update(currentPositionFromRepo);
            _currentPositionsRepository.Save();

            return NoContent();
        }


        /// <summary>
        /// DeleteCurrentPosition
        /// </summary>
        /// <param name="ticker"></param>
        /// <returns></returns>
        /// <response code="204">If the current position is deleted</response>
        /// <response code="400">If the ticker are invalid</response>
        /// <response code="404">If the Current Positions are not found</response>
        //Delete api/currentPositions/xxx
        [HttpDelete("{ticker}", Name = "DeleteCurrentPosition")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult DeleteCurrentPosition(string ticker)
        {
            if (string.IsNullOrEmpty(ticker))
                return BadRequest("Invalid ticker");

            var currentPositionFromRepo = _currentPositionsRepository.GetCurrentPosition(ticker);
            if (currentPositionFromRepo == null)
                return NotFound("Current Position couldn't be found.");

            _currentPositionsRepository.Delete(currentPositionFromRepo);
            _currentPositionsRepository.Save();

            return NoContent();
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

        private IEnumerable<LinkDto> CreateLinksForCurrentPosition(string ticker, string fields)
        {
            var links = new List<LinkDto>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                links.Add( 
                    new LinkDto(Url.Link("GetCurrentPosition", new {ticker}),
                        "self",
                        "GET"));
            }
            else
            {
                links.Add(
                    new LinkDto(Url.Link("GetCurrentPosition", new { ticker, fields }),
                        "self",
                        "GET"));
            }


            links.Add(
                new LinkDto(Url.Link("DeleteCurrentPosition", new { ticker }),
                    "delete_currentPosition",
                    "DELETE"));

            links.Add(
                new LinkDto(Url.Link("CreateSoldPosition", new { ticker }),
                    "create_currentPosition",
                    "POST"));

            links.Add(
                new LinkDto(Url.Link("GetSoldPositionsForCurrentPosition", new { ticker }),
                    "soldPositions",
                    "GET"));


            return links;
        }
    }
}
