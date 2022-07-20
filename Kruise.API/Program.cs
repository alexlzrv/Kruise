using Kruise.API.Controllers;
using Kruise.API.Telegram;
using Kruise.DataAccess.Postgres;
using Kruise.DataAccess.Postgres.Repositories;
using Kruise.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using Serilog.Enrichers.AspNetCore;
using Serilog.Enrichers.Span;
using Telegram.Bot;


var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var telegramConfiguration = builder.Configuration.GetSection(nameof(TelegramConfiguration));
builder.Services.Configure<TelegramConfiguration>(telegramConfiguration);
builder.Services.AddScoped<ITelegramBotClient>(x =>
{
    var token = x.GetRequiredService<IOptions<TelegramConfiguration>>().Value;
    return new TelegramBotClient(token.Token);
});
builder.Services.AddScoped<HandleUpdateService>();

builder.Services.AddDbContext<KruiseDbContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString(nameof(KruiseDbContext))));
builder.Services.AddScoped<IPostsRepository, PostsRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());
builder.Services.AddLogging(x =>
{
    x.ClearProviders();
    var logger = new LoggerConfiguration()
        //.ReadFrom.Services(builder.Services)
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.WithMemoryUsage()
        .Enrich.WithSpan()
        .CreateLogger();
    x.AddSerilog(logger);
});

builder.Services.AddOpenTelemetryTracing(x =>
{
    x.AddSource("Kruise.API");
    x.AddHttpClientInstrumentation();
    x.AddEntityFrameworkCoreInstrumentation(o => o.SetDbStatementForText = true);
    x.AddAspNetCoreInstrumentation();
    x.AddJaegerExporter();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
