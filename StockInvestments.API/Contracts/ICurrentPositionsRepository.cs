using System.Collections.Generic;
using StockInvestments.API.Entities;

namespace StockInvestments.API.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICurrentPositionsRepository : IRepositoryBase<CurrentPosition>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<CurrentPosition> GetCurrentPositions();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tickers"></param>
        /// <returns></returns>
        IEnumerable<CurrentPosition> GetCurrentPositions(List<string> tickers);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        IEnumerable<CurrentPosition> GetCurrentPositionsFilteredByTotalAmount(double amount);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ticker"></param>
        /// <returns></returns>
        CurrentPosition GetCurrentPosition(string ticker);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ticker"></param>
        /// <returns></returns>
        bool CurrentPositionExists(string ticker);
    }
}
