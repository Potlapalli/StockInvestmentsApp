using System;
using System.Collections.Generic;
using System.Linq;
using StockInvestments.API.Contracts;
using StockInvestments.API.DbContexts;
using StockInvestments.API.Entities;

namespace StockInvestments.API.Services
{
    public class StockEarningsRepository : RepositoryBase<StockEarning> , IStockEarningsRepository
    {
        public StockEarningsRepository(StockInvestmentsContext context) : base(context)
        {
        }

        public IEnumerable<StockEarning> GetStockEarnings()
        {
            return _stockInvestmentsContext.StockEarnings.ToList();
        }

        public IEnumerable<StockEarning> GetStockEarningsFilteredByDate(DateTimeOffset date)
        {
            return _stockInvestmentsContext.StockEarnings.Where(se => se.EarningsDate == date).ToList();
        }

        public StockEarning GetStockEarning(string ticker)
        {
            return _stockInvestmentsContext.StockEarnings.FirstOrDefault(se => se.Ticker == ticker);
        }

    }
}
