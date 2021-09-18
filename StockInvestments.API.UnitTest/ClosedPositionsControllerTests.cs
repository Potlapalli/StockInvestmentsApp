using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using StockInvestments.API.Contracts;
using StockInvestments.API.Controllers;
using StockInvestments.API.Entities;
using StockInvestments.API.Models;
using StockInvestments.API.Profiles;

namespace StockInvestments.API.UnitTest
{
    public class ClosedPositionsControllerTests
    {
        private ClosedPositionsController _closedPositionsController;
        private Mock<IClosedPositionsRepository> _closedPositionsRepositoryMock;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _closedPositionsRepositoryMock = new Mock<IClosedPositionsRepository>();
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new ClosedPositionsProfile())));
            _closedPositionsController = new ClosedPositionsController(_closedPositionsRepositoryMock.Object, _mapper);
        }

        [Test]
        public void GetClosedPositionsTest()
        {
            //Arrange
            _closedPositionsRepositoryMock.Setup(x => x.GetClosedPositions())
                .Returns(new List<ClosedPosition>()
                {
                    new() {Ticker = "XXX"},
                    new() {Ticker = "YYY"}
                });

            //Act
            ActionResult<IEnumerable<ClosedPositionDto>> closedPositions = _closedPositionsController.GetClosedPositions();
            var result = closedPositions.Result as OkObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual((int) HttpStatusCode.OK, result.StatusCode);
            var value = result.Value as List<ClosedPositionDto>;
            Assert.AreEqual(2, value?.Count);
        }

        [Test]
        public void GetClosedPositionTest_ValidRequest()
        {
            //Arrange
            _closedPositionsRepositoryMock.Setup(x => x.GetClosedPosition("XXX"))
                .Returns(new ClosedPosition {Ticker = "XXX"});

            //Act
            ActionResult<ClosedPositionDto> closedPosition = _closedPositionsController.GetClosedPosition("XXX");
            var result = closedPosition.Result as OkObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual((int) HttpStatusCode.OK, result.StatusCode);
            var value = result.Value as ClosedPositionDto;
            Assert.IsNotNull(value);
            Assert.AreEqual("XXX", value.Ticker);
        }

        [Test]
        public void GetClosedPositionTest_BadRequest()
        {
            //Act
            ActionResult<ClosedPositionDto> closedPosition = _closedPositionsController.GetClosedPosition("");
            var result = closedPosition.Result as BadRequestObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual((int) HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Test]
        public void GetClosedPositionTest_NotFound()
        {
            //Act
            ActionResult<ClosedPositionDto> closedPosition = _closedPositionsController.GetClosedPosition("YYY");
            var result = closedPosition.Result as NotFoundObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual((int) HttpStatusCode.NotFound, result.StatusCode);
        }

        [Test]
        public void CreateClosedPositionTest_ValidRequest()
        {

            //Act
            ActionResult<ClosedPositionDto> closedPosition = _closedPositionsController.CreateClosedPosition(
                new ClosedPositionForCreationDto() {Ticker = "XXX"});
            var result = closedPosition.Result as CreatedAtRouteResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual((int) HttpStatusCode.Created, result.StatusCode);
            Assert.AreEqual("GetClosedPosition", result.RouteName);
            Assert.AreEqual("XXX", result.RouteValues["ticker"]);
        }

        [Test]
        public void UpdateClosedPositionTest_ValidRequest()
        {
            //Arrange
            _closedPositionsRepositoryMock.Setup(x => x.GetClosedPosition("XXX"))
                .Returns(new ClosedPosition {Ticker = "XXX"});

            //Act
            ActionResult<ClosedPositionDto> closedPosition = _closedPositionsController.UpdateClosedPosition("XXX",
                new ClosedPositionForUpdateDto() {Company = "XXX"});
            var result = closedPosition.Result as NoContentResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual((int) HttpStatusCode.NoContent, result.StatusCode);
        }
    }
}

