using System;

namespace StockInvestments.API.Helpers
{
    public class HttpResponseException : Exception
    {
        public int Status { get; set; } = 500;
        public object Value { get; set; }

        public HttpResponseException(string value)
        {
            Value = value;
        }
    }
}
