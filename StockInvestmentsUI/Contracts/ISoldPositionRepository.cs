using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockInvestmentsUI.Models;

namespace StockInvestmentsUI.Contracts
{
    public interface ISoldPositionRepository : IRepositoryBase<SoldPosition>
    {
        Task<IList<string>> GetPositionsInProfit(string url);
       
        Task<double> GetSharesRemaining(string url);
    }
}
