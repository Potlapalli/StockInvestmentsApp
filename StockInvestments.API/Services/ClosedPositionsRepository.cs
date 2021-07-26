using System;
using System.Collections.Generic;
using System.Linq;
using StockInvestments.API.Contracts;
using StockInvestments.API.DbContexts;
using StockInvestments.API.Entities;

namespace StockInvestments.API.Services
{
    public class ClosedPositionsRepository : IClosedPositionsRepository
    {
        private readonly StockInvestmentsContext _stockInvestmentsContext;

        public ClosedPositionsRepository(StockInvestmentsContext context)
        {
            _stockInvestmentsContext = context ?? throw new ArgumentNullException(nameof(context));
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

        public void Add(ClosedPosition closedPosition)
        {
            if (closedPosition == null)
            {
                throw new ArgumentNullException(nameof(closedPosition));
            }
            _stockInvestmentsContext.ClosedPositions.Add(closedPosition);
        }

        public void Update(ClosedPosition dbClosedPosition)
        {
            // no code in this implementation

        }

        public void Delete(ClosedPosition closedPosition)
        {
            _stockInvestmentsContext.ClosedPositions.Remove(closedPosition);
        }

        public bool Save()
        {
            return (_stockInvestmentsContext.SaveChanges() >= 0);
        }
    }
}
