namespace API_StockMarket.ServiceContracts
{
    public interface IFinnHubService
    {
        Task<Dictionary<string, object>> GetStockPriceQuote(string stockSymbol);
    }
}
