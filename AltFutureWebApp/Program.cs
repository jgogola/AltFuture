using AltFuture.CoinMarketCapAPI.Interfaces;
using AltFuture.CoinMarketCapAPI.Models.EndPoints;
using AltFuture.DataAccessLayer.Data;
using AltFuture.DataAccessLayer.Interfaces;
using AltFuture.DataAccessLayer.Repository;
using Microsoft.EntityFrameworkCore;
using AltFuture.CoinMarketCapAPI.Services;
using AltFuture.BusinessLogicLayer.Interfaces;
using AltFuture.BusinessLogicLayer.Services;
using AltFuture.DataAccessLayer.Services;
using AltFuture.DataAccessLayer.Interfaces.Services;
using AltFuture.BusinessLogicLayer.AutoMapper.CoinbaseTransactionHistoryToTransaction;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

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
builder.Services.AddSingleton<ExchangeTransactionTypeDataService>();
builder.Services.AddSingleton<CryptoDataService>();

//* IOptions:
builder.Services.Configure<CoinMarketCapEndPoints>(builder.Configuration.GetSection("CoinMarketCapSettings:EndPoints"));


//* Named HttpClients for API calls:
builder.Services.AddHttpClient();
builder.Services.AddHttpClient("CoinMarketCapPro", config =>
{
    config.BaseAddress = new Uri(builder.Configuration.GetValue<string>("CoinMarketCapSettings:BaseUrls:Pro"));
    config.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", builder.Configuration.GetValue<string>("CoinMarketCapSettings:ApiKeys:Pro"));
    //config.DefaultRequestHeaders.Add("Accept-Encoding", "deflate, gzip");
});
builder.Services.AddHttpClient("CoinMarketCapSandbox", config =>
{
    config.BaseAddress = new Uri(builder.Configuration.GetValue<string>("CoinMarketCapSettings:BaseUrls:Sandbox"));
    config.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", builder.Configuration.GetValue<string>("CoinMarketCapSettings:ApiKeys:Sandbox"));
    //config.DefaultRequestHeaders.Add("Accept-Encoding", "deflate, gzip");
});


//* BLL Services:
builder.Services.AddScoped<ICoinMarketCapAPI, CoinMarketCapAPI>();
builder.Services.AddScoped<ITransactionCsvImports, TransactionCsvImports>();
builder.Services.AddAutoMapper(typeof(CoinbaseTransactionHistoryProfile));


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
