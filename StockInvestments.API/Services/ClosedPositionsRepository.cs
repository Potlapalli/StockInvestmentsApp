using System;
using System.Collections.Generic;
using System.Linq;
using StockInvestments.API.Contracts;
using StockInvestments.API.DbContexts;
using StockInvestments.API.Entities;

namespace StockInvestments.API.Services
{
    public class ClosedPositionsRepository : RepositoryBase<ClosedPosition>, IClosedPositionsRepository
    {

        public ClosedPositionsRepository(StockInvestmentsContext context) : base(context)
        {
        }

        public IEnumerable<ClosedPosition> GetClosedPositions()
        {
            return _stockInvestmentsContext.ClosedPositions.ToList();
        }

        public IEnumerable<ClosedPosition> GetClosedPositionsFilteredByFinalValue(double value)
        {
            return _stockInvestmentsContext.ClosedPositions.Where(cp => cp.FinalValue >= value).ToList();
        }

        public ClosedPosition GetClosedPosition(string ticker)
        {
            return _stockInvestmentsContext.ClosedPositions.FirstOrDefault(cp => cp.Ticker == ticker);
        }
    }
}
