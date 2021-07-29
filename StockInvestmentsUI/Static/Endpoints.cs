using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockInvestmentsUI.Static
{
    public static class Endpoints
    {
        public static string BaseUrl = "http://localhost:51044/";
        public static string CurrentPositionsEndpoint = $"{BaseUrl}api/currentPositions/";
        //public static string SoldPositionsEndpoint = $"{BaseUrl}api/currentPositions/";
    }
}
