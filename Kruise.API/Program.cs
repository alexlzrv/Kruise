using Kruise.API.Controllers;
using Kruise.API.Telegram;
using Kruise.DataAccess.Postgres;
using Kruise.DataAccess.Postgres.Repositories;
using Kruise.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using Telegram.Bot;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<HandleUpdateService>();

var telegramConfiguration = builder.Configuration.GetSection(nameof(TelegramConfiguration));
builder.Services.Configure<TelegramConfiguration>(telegramConfiguration);
builder.Services.AddScoped<ITelegramBotClient>(x =>
{
    var token = x.GetRequiredService<IOptions<TelegramConfiguration>>().Value;
    return new TelegramBotClient(token.Token);
});

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
        .CreateLogger();
    x.AddSerilog(logger);
});

builder.Services.AddScoped<ModelServiceA>();
builder.Services.AddScoped<ModelServiceB>();
builder.Services.AddTransient<ModelRepositoryA>();
builder.Services.AddScoped<ModelRepositoryB>();

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
