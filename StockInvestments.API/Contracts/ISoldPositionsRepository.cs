using System.Collections.Generic;
using StockInvestments.API.Entities;

namespace StockInvestments.API.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISoldPositionsRepository : IRepositoryBase<SoldPosition>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<SoldPosition> GetSoldPositions();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ticker"></param>
        /// <returns></returns>
        IEnumerable<SoldPosition> GetSoldPositions(string ticker);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentPositions"></param>
        /// <returns></returns>
        IEnumerable<string> GetPositionsInProfit(List<CurrentPosition> currentPositions);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentPosition"></param>
        /// <returns></returns>
        double GetSharesRemaining(CurrentPosition currentPosition);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ticker"></param>
        /// <returns></returns>
        double GetSoldPositionsTotalAmount(string ticker);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        IEnumerable<SoldPosition> GetSoldPositionsFilteredByTotalAmount(double amount);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ticker"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        SoldPosition GetSoldPosition(string ticker, long number);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ticker"></param>
        /// <param name="soldPosition"></param>
        void Add(string ticker, SoldPosition soldPosition);
       
    }
}
