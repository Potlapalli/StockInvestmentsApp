using System;
using System.Collections.Generic;
using System.Linq;
using StockInvestments.API.Contracts;
using StockInvestments.API.DbContexts;
using StockInvestments.API.Entities;

namespace StockInvestments.API.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class SoldPositionsRepository : ISoldPositionsRepository
    {
        private readonly StockInvestmentsContext _stockInvestmentsContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public SoldPositionsRepository(StockInvestmentsContext context)
        {
            _stockInvestmentsContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<SoldPosition> GetSoldPositions()
        {
            return _stockInvestmentsContext.SoldPositions.ToList();
        }

        public IEnumerable<SoldPosition> GetSoldPositions(string ticker)
        {
            return _stockInvestmentsContext.SoldPositions.Where(sp => sp.Ticker == ticker).ToList();
        }

        public IEnumerable<string> GetPositionsInProfit(List<CurrentPosition> currentPositions)
        {
            return (from currentPosition in currentPositions 
                let soldPositions = _stockInvestmentsContext.SoldPositions.
                    Where(sp => sp.Ticker == currentPosition.Ticker).ToList() 
                let totalAmountSum = soldPositions.Sum(soldPosition => soldPosition.TotalAmount) 
                where currentPosition.TotalAmount < totalAmountSum select currentPosition.Ticker).ToList();
        }

        public double GetSharesRemaining(CurrentPosition currentPosition)
        {
            var soldPositions = _stockInvestmentsContext.SoldPositions.Where(sp => sp.Ticker == currentPosition.Ticker).ToList();

            double totalShares = soldPositions.Sum(soldPosition => soldPosition.TotalShares);
            return currentPosition.TotalShares - totalShares;
        }

        public double GetSoldPositionsTotalAmount(string ticker)
        {
            var soldPositions = _stockInvestmentsContext.SoldPositions.Where(sp => sp.Ticker == ticker).ToList();

            double totalAmount = soldPositions.Sum(soldPosition => soldPosition.TotalAmount);
            return totalAmount;
        }


        public IEnumerable<SoldPosition> GetSoldPositionsFilteredByTotalAmount(double amount)
        {
            return _stockInvestmentsContext.SoldPositions.Where(sp => sp.TotalAmount >= amount).ToList();
        }

        public SoldPosition GetSoldPosition(string ticker, long number)
        {
            return _stockInvestmentsContext.SoldPositions.FirstOrDefault(sp => sp.Ticker == ticker && sp.Number == number);
        }

        public void Add(SoldPosition soldPosition)
        {
            throw new NotImplementedException();
        }

        public void Add(string ticker, SoldPosition soldPosition)
        {
            if (soldPosition == null)
            {
                throw new ArgumentNullException(nameof(soldPosition));
            }
            soldPosition.Ticker = ticker;
            _stockInvestmentsContext.SoldPositions.Add(soldPosition);
        }

        public void Update(SoldPosition dbSoldPosition)
        {
          // no code in this implementation
        }

        public void Delete(SoldPosition soldPosition)
        {
            _stockInvestmentsContext.SoldPositions.Remove(soldPosition);
        }

        public bool Save()
        {
            return (_stockInvestmentsContext.SaveChanges() >= 0);
        }
    }
}
