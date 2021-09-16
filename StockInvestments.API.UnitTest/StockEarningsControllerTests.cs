using System.Collections.Generic;
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
    public class StockEarningsControllerTests
    {
        private StockEarningsController _stockEarningsController;
        private Mock<IStockEarningsRepository> _stockEarningsRepositoryMock;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _stockEarningsRepositoryMock = new Mock<IStockEarningsRepository>();
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new StockEarningsProfile())));
            _stockEarningsController = new StockEarningsController(_stockEarningsRepositoryMock.Object, _mapper);
        }

        [Test]
        public void GetStockEarningsTest()
        {
            //Arrange
            _stockEarningsRepositoryMock.Setup(x => x.GetStockEarnings())
                .Returns(new List<StockEarning>()
                {
                    new(){Ticker = "XXX"},
                    new(){Ticker = "YYY"}
                });

            //Act
            ActionResult<IEnumerable<StockEarningDto>> stockEarnings = _stockEarningsController.GetStockEarnings();
            var result = stockEarnings.Result as OkObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            var value = result.Value as List<StockEarningDto>;
            Assert.AreEqual(2, value?.Count);
        }

        [Test]
        public void GetStockEarningTest_ValidRequest()
        {
            //Arrange
            _stockEarningsRepositoryMock.Setup(x => x.GetStockEarning("XXX"))
                .Returns(new StockEarning {Ticker = "XXX"});

            //Act
            ActionResult<StockEarningDto> stockEarning = _stockEarningsController.GetStockEarning("XXX");
            var result = stockEarning.Result as OkObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            var value = result.Value as StockEarningDto;
            Assert.IsNotNull(value);
            Assert.AreEqual("XXX", value.Ticker);
        }

        [Test]
        public void GetStockEarningTest_BadRequest()
        {
            //Act
            ActionResult<StockEarningDto> stockEarning = _stockEarningsController.GetStockEarning("");
            var result = stockEarning.Result as BadRequestObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]
        public void GetStockEarningTest_NotFound()
        {
            //Act
            ActionResult<StockEarningDto> stockEarning = _stockEarningsController.GetStockEarning("YYY");
            var result = stockEarning.Result as NotFoundObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
        }
    }
}