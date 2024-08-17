namespace API_StockMarket.Services.Helper
{
    public class TradingOptions
    {
        public string deFaultStockSymbol { get; set; }

        TradingOptions() 
        {
            deFaultStockSymbol = "MSFT";
        }
    }
}
