using API_StockMarket.Models;
using API_StockMarket.ServiceContracts.DTO;
using API_StockMarket.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace API_StockMarket.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly FinnHubService _finnhubService;
        


        public HomeController(FinnHubService finnhubService)
        {
            _finnhubService = finnhubService;
           
        }

        [HttpGet]
        [Route("/")]
        [Route("/SearchStockSymbol")]
        public async Task<IActionResult> Index()
        {
           
            string DefaultStockSymbol = "MSFT";
            

            Dictionary<string, object>? responseDictionary = await _finnhubService.GetStockPriceQuote(DefaultStockSymbol);

            Stock stock = new Stock()
            {
                StockSymbol = DefaultStockSymbol,
                CurrentPrice = Convert.ToDouble(responseDictionary["c"].ToString()),
                HighestPrice = Convert.ToDouble(responseDictionary["h"].ToString()),
                LowestPrice = Convert.ToDouble(responseDictionary["l"].ToString()),
                OpenPrice = Convert.ToDouble(responseDictionary["o"].ToString())
            };

            ViewBag.Stock = stock;

            return View();
        }

        [HttpPost]
        [Route("/SearchStockSymbol")]
    public async Task<IActionResult> Index([FromForm]string stockSymbol)
        {
            if (stockSymbol == null)
            {
                stockSymbol = "MSFT";
            }

            Dictionary<string, object>? responseDictionary = await _finnhubService.GetStockPriceQuote(stockSymbol);

            Stock stock = new Stock()
            {
                StockSymbol = stockSymbol,
                CurrentPrice = Convert.ToDouble(responseDictionary["c"].ToString()),
                HighestPrice = Convert.ToDouble(responseDictionary["h"].ToString()),
                LowestPrice = Convert.ToDouble(responseDictionary["l"].ToString()),
                OpenPrice = Convert.ToDouble(responseDictionary["o"].ToString())
            };

            ViewBag.Stock = stock;

            return View();
        }
    }
}
