using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using StockInvestmentsUI.Contracts;
using StockInvestmentsUI.Models;

namespace StockInvestmentsUI.Services
{
    public class CurrentPositionRepository : RepositoryBase<CurrentPosition>, ICurrentPositionRepository
    {
        private readonly IHttpClientFactory _client;

        public CurrentPositionRepository(IHttpClientFactory client) : base(client)
        {
            _client = client;
        }
    }
}
