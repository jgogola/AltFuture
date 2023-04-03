using AltFuture.DataAccessLayer.Data;
using AltFuture.DataAccessLayer.Interfaces;
using AltFuture.DataAccessLayer.Repository;
using Microsoft.EntityFrameworkCore;
using AltFuture.BusinessLogicLayer.Interfaces;
using AltFuture.BusinessLogicLayer.Services;
using AltFuture.DataAccessLayer.Services;
using AltFuture.DataAccessLayer.Interfaces.Services;
using Newtonsoft.Json;
using AltFuture.MarketDataConsumer.Interfaces;
using AltFuture.MarketDataConsumer.Services;
using AltFuture.MarketDataConsumer.AutoMapper;
using AltFuture.MarketDataConsumer.Models.CoinMarketCap;
using AltFuture.BusinessLogicLayer.AutoMapper;
using AltFuture.BusinessLogicLayer.Services.MarketData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    // Add other settings if needed
});

//* DB Context:
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//* Repositories:
builder.Services.AddScoped<ICryptoRepository, CryptoRepository>();
builder.Services.AddScoped<ICryptoPriceRepository, CryptoPriceRepository>();
builder.Services.AddScoped<IExchangeTransactionTypeRepository, ExchangeTransactionTypeRepository>();    
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IPortfolioSummaryRepository, PortfolioSummaryRepository>();

//* Singletons:
builder.Services.AddSingleton<Func<IServiceScope>>(_ => () => builder.Services.BuildServiceProvider().CreateScope());
builder.Services.AddSingleton<IExchangeTransactionTypeDataService, ExchangeTransactionTypeDataService>();
builder.Services.AddSingleton<ICryptoDataService, CryptoDataService>();

//* IOptions:




//* Named HttpClients for API calls:
//builder.Services.AddHttpClient();
//builder.Services.AddHttpClient("CoinMarketCapPro", config =>
//{
//    config.BaseAddress = new Uri(builder.Configuration.GetValue<string>("CoinMarketCapSettings:BaseUrls:Pro"));
//    config.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", builder.Configuration.GetValue<string>("CoinMarketCapSettings:ApiKeys:Pro"));
//    //config.DefaultRequestHeaders.Add("Accept-Encoding", "deflate, gzip");
//});
//builder.Services.AddHttpClient("CoinMarketCapSandbox", config =>
//{
//    config.BaseAddress = new Uri(builder.Configuration.GetValue<string>("CoinMarketCapSettings:BaseUrls:Sandbox"));
//    config.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", builder.Configuration.GetValue<string>("CoinMarketCapSettings:ApiKeys:Sandbox"));
//    //config.DefaultRequestHeaders.Add("Accept-Encoding", "deflate, gzip");
//});




//* BLL Services:
builder.Services.AddScoped<ITransactionCsvImports, TransactionCsvImports>();
builder.Services.AddScoped<IPortfolioChartData, PortfolioChartData>();
builder.Services.AddScoped<IMarketDataService, MarketDataService>();
builder.Services.AddAutoMapper(typeof(MarketDataPriceToCryptoPriceProfile));


//* MarketDataClient Layer:
builder.Services.AddAutoMapper(typeof(CoinMarektCapPlanUsageProfile));
builder.Services.AddAutoMapper(typeof(CoinMarketCapPriceDataProfile));
builder.Services.Configure<CoinMarketCapEndPointOptions>(builder.Configuration.GetSection(CoinMarketCapEndPointOptions.SettingsSection));

builder.Services.AddScoped<IMarketDataClient, CoinMarketCapDataClient>();
builder.Services.AddHttpClient<IMarketDataClient, CoinMarketCapDataClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("CoinMarketCapSettings:BaseUrls:Pro"));
    client.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", builder.Configuration.GetValue<string>("CoinMarketCapSettings:ApiKeys:Pro"));
});

var app = builder.Build();


if (args.Length == 1 && args[0].ToLower() == "seeddata")
{
    //await Seed.SeedUsersAndRolesAsync(app);
    Seed.SeedData(app);
}



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
