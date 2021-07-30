using StockInvestmentsUI.Contracts;
using StockInvestmentsUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace StockInvestmentsUI.Services
{
    public class ClosedPositionRepository : RepositoryBase<ClosedPosition>, IClosedPositionRepository
    {
        private readonly IHttpClientFactory _client;

        public ClosedPositionRepository(IHttpClientFactory client) : base(client)
        {
            _client = client;
        }
    }
}
