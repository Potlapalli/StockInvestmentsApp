using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StockInvestmentsUI.Contracts;

namespace StockInvestmentsUI.Services
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly IHttpClientFactory _client;

        public RepositoryBase(IHttpClientFactory client)
        {
            _client = client;
        }
        public async Task<T> Get(string url, string ticker)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url + ticker);

            var client = _client.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }

            return null;
        }

        public async Task<IList<T>> Get(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var client = _client.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IList<T>>(content);
            }

            return null;
        }

        public async Task<bool> Create(string url, T obj)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            if (obj == null)
                return false;

            request.Content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

            var client = _client.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.Created)
                return true;

            return false;
        }

        public async Task<bool> Update(string url, string ticker, T obj)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, url + ticker);
            if (obj == null)
                return false;

            request.Content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

            var client = _client.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.NoContent)
                return true;

            return false;
        }

        public async Task<bool> Delete(string url, string ticker)
        {
            if (string.IsNullOrEmpty(ticker))
                return false;

            var request = new HttpRequestMessage(HttpMethod.Delete, url + ticker);

            var client = _client.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.NoContent)
                return true;

            return false;
        }
    }
}
