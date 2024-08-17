using API_StockMarket.Services;
using API_StockMarket.Services.Helper;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();

builder.Services.AddScoped<FinnHubService>();

builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection(nameof(TradingOptions))); //add IOptions<TradingOptions> as a service

var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.MapControllers();



app.Run();
