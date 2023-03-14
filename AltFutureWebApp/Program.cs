using AltFuture.CoinMarketCapAPI;
using AltFuture.CoinMarketCapAPI.Models;
using AltFuture.DataAccessLayer.Data;
using AltFuture.DataAccessLayer.Interfaces;
using AltFuture.DataAccessLayer.Repository;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICryptoRepository, CryptoRepository>();
builder.Services.AddScoped<IExchangeRepository, ExchangeRepository>();
builder.Services.AddScoped<ICryptoPriceRepository, CryptoPriceRepository>();
builder.Services.AddScoped<IExchangeTransactionTypeRepository, ExchangeTransactionTypeRepository>();    
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IPortfolioSummaryRepository, PortfolioSummaryRepository>();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<CoinMarketCapEndPointsV2>(builder.Configuration.GetSection("CoinMarketCapSettings:EndPointsV2"));

builder.Services.AddHttpClient();
builder.Services.AddHttpClient("CoinMarketCapPro", config =>
{
    config.BaseAddress = new Uri(builder.Configuration.GetValue<string>("CoinMarketCapSettings:BaseUrls:Pro"));
    config.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", builder.Configuration.GetValue<string>("CoinMarketCapSettings:ApiKeys:Pro"));
});
builder.Services.AddHttpClient("CoinMarketCapSandbox", config =>
{
    config.BaseAddress = new Uri(builder.Configuration.GetValue<string>("CoinMarketCapSettings:BaseUrls:Sandbox"));
    config.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", builder.Configuration.GetValue<string>("CoinMarketCapSettings:ApiKeys:Sandbox"));
});

builder.Services.AddScoped<CoinMarketCapAPI>();

//builder.Services.AddMemoryCache();

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
