using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using StockInvestments.API.DbContexts;
using StockInvestments.API.Entities;
using StockInvestments.API.Services;

namespace StockInvestments.API.UnitTest.InMemoryTests
{
    public class ClosedPositionsInMemoryTests
    {
        [Test]
        public void ClosedPositionIntoDatabase()
        {
            var builder = new DbContextOptionsBuilder<StockInvestmentsContext>();
            builder.UseInMemoryDatabase("InsertClosedPosition");
            using (var context = new StockInvestmentsContext(builder.Options))
            {
                var closedPositionsRepository = new ClosedPositionsRepository(context);
                var closedPosition = new ClosedPosition() { Ticker = "XXX" };
                closedPositionsRepository.Add(closedPosition);
                Assert.AreEqual(EntityState.Added, context.Entry(closedPosition).State);
                Assert.IsTrue(closedPositionsRepository.Save());

                var closedPositions = closedPositionsRepository.GetClosedPositions();
                Assert.IsNotNull(closedPositions);
                Assert.AreEqual(1, closedPositions.Count());
                Assert.AreEqual("XXX", closedPositions.ToList()[0].Ticker);
            }
        }
    }
}
