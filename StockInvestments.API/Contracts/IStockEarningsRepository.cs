using System;
using System.Collections.Generic;
using StockInvestments.API.Entities;

namespace StockInvestments.API.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    public interface IStockEarningsRepository : IRepositoryBase<StockEarning>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<StockEarning> GetStockEarnings();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        IEnumerable<StockEarning> GetStockEarningsFilteredByDate(DateTimeOffset date);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ticker"></param>
        /// <returns></returns>
        StockEarning GetStockEarning(string ticker);
    }
}
