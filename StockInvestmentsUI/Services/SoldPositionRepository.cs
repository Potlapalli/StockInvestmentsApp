using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
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

        public async Task<IList<string>> GetPositionsInProfit(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var client = _client.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IList<string>>(content);
            }

            return null;
        }

        public async Task<double> GetSharesRemaining(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var client = _client.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<double>(content);
            }

            return 0;
        }
    }
}
