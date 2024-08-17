using API_StockMarket.ServiceContracts;
using System.Text.Json;

namespace API_StockMarket.Services
{
    public class FinnHubService : IFinnHubService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public FinnHubService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={_configuration["FinnHubToken"]}"),
                    Method = HttpMethod.Get
                };

                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

                Stream stream = httpResponseMessage.Content.ReadAsStream();

                StreamReader streamReader = new StreamReader(stream);

                string response = streamReader.ReadToEnd();

                Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(response);

                if (responseDictionary == null)
                {
                    throw new InvalidOperationException("No reponse from finnhub server");
                }

                if (responseDictionary.ContainsKey("error"))
                {
                    throw new InvalidOperationException("error");
                }

                return responseDictionary;

            }
        }
    }

    // How to enable secrets key:
    /*
     * 1. Open in terminal
     * 2. Write the project name, which you want to enable secrets key: dotnet user-secrets init --project 'API_StockMarket'
     * 3. Write the key secrets: dotnet user-secrets set 'FinnHubToken' 'cqhhk8pr01qm46d7j9qgcqhhk8pr' --project 'API_StockMarket'
     * 
     * Result => Successfully saved FinnHubToken = cqhhk8pr01qm46d7j9qgcqhhk8pr to the secret store.
     */
}
