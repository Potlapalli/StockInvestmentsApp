using System.Linq;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using StockInvestments.API.DbContexts;
using StockInvestments.API.Entities;
using StockInvestments.API.Services;

namespace StockInvestments.API.UnitTest.InMemoryTests
{
    public class StockEarningsInMemoryTests
    {
        [Test]
        public void StockEarningIntoDatabase()
        {
            var builder = new DbContextOptionsBuilder<StockInvestmentsContext>();
            builder.UseInMemoryDatabase("InsertStockEarning");
            using (var context = new StockInvestmentsContext(builder.Options))
            {
                var stockEarningsRepository = new StockEarningsRepository(context);
                var stockEarning= new StockEarning(){Ticker = "XXX"};
                stockEarningsRepository.Add(stockEarning);
                Assert.AreEqual(EntityState.Added, context.Entry(stockEarning).State);
                Assert.IsTrue(stockEarningsRepository.Save());

                var stockEarnings =  stockEarningsRepository.GetStockEarnings();
                Assert.IsNotNull(stockEarnings);
                Assert.AreEqual(1, stockEarnings.Count());
                Assert.AreEqual("XXX", stockEarnings.ToList()[0].Ticker);
            }
        }
    }
}
