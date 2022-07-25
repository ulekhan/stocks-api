using Alpaca.Markets;
using Hellang.Middleware.ProblemDetails;
using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using StocksData.Api.Extensions;
using StocksData.Api.Handlers;
using StocksData.Api.Mappers;
using StocksData.Api.Services;
using StocksData.Api.Settings;
using StocksData.Api.Validators;
using StocksData.Data;
using StocksData.Data.Db;
using StocksData.Data.Repositories;
using StocksData.Domain;
using StocksData.Domain.Abstractions;
using Environments = Alpaca.Markets.Environments;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureServices(services =>
{
    services.AddControllers();
    services.AddProblemDetails(options =>
    {
        options.MapFluentValidationException();
        options.IncludeExceptionDetails = (_, _) => false;
    });

    var alpacaApiSettings = builder.Configuration.GetSection("AlpacaApi").Get<AlpacaApiSettings>();
    var secret = new SecretKey(alpacaApiSettings.ClientId!, alpacaApiSettings.ClientSecret!);

    services
        .AddEntityFrameworkSqlite()
        .AddDbContext<DataContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("SqlLite")));

    services.AddScoped<IRepository<HistoricalBar>, StocksDataRepository>();
    services.AddScoped<IUnitOfWork, UnitOfWork>();
    services.AddScoped<IStocksDataProvider, StocksDataProvider>();

    services.AddScoped(_ => Environments.Paper.GetAlpacaDataClient(secret));
    services.AddScoped(_ => Environments.Paper.GetAlpacaTradingClient(secret));
    services.AddScoped(_ => Environments.Paper.GetAlpacaCryptoDataClient(secret));
    services.AddScoped<PerformanceCalculator>();

    services.AddMediatR(typeof(CompareStockQueryHandler));
    services.AddFluentValidation(new[] { typeof(CompareStockQueryValidator).Assembly });

    services.AddAutoMapper(typeof(HistoricalBarMappingProfile));
});

var app = builder.Build();

app.UseProblemDetails();
app.UseRouting();
app.UseEndpoints(b => b.MapControllers());

app.Run();