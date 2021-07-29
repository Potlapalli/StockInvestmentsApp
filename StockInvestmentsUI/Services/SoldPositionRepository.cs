using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using StockInvestmentsUI.Contracts;
using StockInvestmentsUI.Models;

namespace StockInvestmentsUI.Services
{
    public class SoldPositionRepository : RepositoryBase<SoldPosition>, ISoldPositionRepository
    {
        private readonly IHttpClientFactory _client;

        public SoldPositionRepository(IHttpClientFactory client) : base(client)
        {
            _client = client;
        }
    }
}
