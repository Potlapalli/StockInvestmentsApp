using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockInvestmentsUI.Contracts
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<T> Get(string url, string id);
        Task<IList<T>> Get(string url);
        Task<bool> Create(string url, T obj);
        Task<bool> Update(string url, string id, T obj);
        Task<bool> Delete(string url, string id);
    }
}
