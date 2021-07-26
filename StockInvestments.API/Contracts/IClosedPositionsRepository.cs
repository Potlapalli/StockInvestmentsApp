using System.Collections.Generic;
using StockInvestments.API.Entities;

namespace StockInvestments.API.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    public interface IClosedPositionsRepository : IRepositoryBase<ClosedPosition>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<ClosedPosition> GetClosedPositions();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        IEnumerable<ClosedPosition> GetClosedPositionsFilteredByFinalValue(double value);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ticker"></param>
        /// <returns></returns>
        ClosedPosition GetClosedPosition(string ticker);
        
    }
}
