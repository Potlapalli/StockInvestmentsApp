using System;
using System.Collections.Generic;
using System.Linq;
using StockInvestments.API.Contracts;
using StockInvestments.API.DbContexts;
using StockInvestments.API.Entities;

namespace StockInvestments.API.Services
{
    public class CurrentPositionsRepository : ICurrentPositionsRepository
    {
        private readonly StockInvestmentsContext _stockInvestmentsContext;

        public CurrentPositionsRepository(StockInvestmentsContext context)
        {
            _stockInvestmentsContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<CurrentPosition> GetCurrentPositions()
        {
            return _stockInvestmentsContext.CurrentPositions.ToList();
        }

        public IEnumerable<CurrentPosition> GetCurrentPositions(List<string> tickers)
        {
            return _stockInvestmentsContext.CurrentPositions.Where(cp => tickers.Contains(cp.Ticker)).ToList();
        }

        public IEnumerable<CurrentPosition> GetCurrentPositionsFilteredByTotalAmount(double amount)
        {
            return _stockInvestmentsContext.CurrentPositions.Where(cp => cp.TotalAmount >= amount).ToList();
        }

        public CurrentPosition GetCurrentPosition(string ticker)
        {
            return _stockInvestmentsContext.CurrentPositions.FirstOrDefault(cp => cp.Ticker == ticker);
        }

        public void Add(CurrentPosition currentPosition)
        {
            if (currentPosition == null)
            {
                throw new ArgumentNullException(nameof(currentPosition));
            }
            _stockInvestmentsContext.CurrentPositions.Add(currentPosition);
        }

        public void Update(CurrentPosition dbCurrentPosition)
        {
            // no code in this implementation
        }

        public void Delete(CurrentPosition currentPosition)
        {
            _stockInvestmentsContext.CurrentPositions.Remove(currentPosition);
        }

        public bool CurrentPositionExists(string ticker)
        {
            return _stockInvestmentsContext.CurrentPositions.Any(cp => cp.Ticker == ticker);
        }

        public bool Save()
        {
            return (_stockInvestmentsContext.SaveChanges() >= 0);
        }
    }
}
