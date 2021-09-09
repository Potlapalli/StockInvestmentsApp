using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockInvestmentsUI.Static
{
    public static class Endpoints
    {
#if DEBUG
        public static string BaseUrl = "http://localhost:51044/";
#else
        public static string BaseUrl = "https://stockinvestmentsapi20210908221344.azurewebsites.net/";
#endif
        public static string CurrentPositionsEndpoint = $"{BaseUrl}api/currentPositions/";
        public static string CurrentPositionsFilteredByTotalAmountEndpoint 
            = $"{BaseUrl}api/currentPositions/filterByTotalAmount?totalAmountGreaterThan=";
        public static string SoldPositionsFilteredByTotalAmountEndpoint
            = $"{BaseUrl}api/soldPositions/filterByTotalAmount?totalAmountGreaterThan=";

        public static string SoldPositionsEndpoint = $"{BaseUrl}api/soldPositions/";

        public static string ClosedPositionsEndpoint = $"{BaseUrl}api/closedPositions/";

        public static string StockEarningsEndpoint = $"{BaseUrl}api/stockEarnings/";

        public static string PositionsInProfitEndpoint = $"{BaseUrl}api/positionsInProfit/";
    }
}
