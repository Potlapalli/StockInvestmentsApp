using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StockInvestments.API.Contracts;
using StockInvestments.API.DbContexts;
using StockInvestments.API.Entities;

namespace StockInvestments.API.Services
{
    public class CurrentPositionsRepository : RepositoryBase<CurrentPosition> , ICurrentPositionsRepository
    {
        public CurrentPositionsRepository(StockInvestmentsContext context) : base(context)
        {
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
            return _stockInvestmentsContext.CurrentPositions.
                Include(x => x.SoldPositions).FirstOrDefault(cp => cp.Ticker == ticker);
        }

        public bool CurrentPositionExists(string ticker)
        {
            return _stockInvestmentsContext.CurrentPositions.Any(cp => cp.Ticker == ticker);
        }
    }
}
