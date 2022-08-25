using Kruise.API;
using Kruise.BusinessLogic.Senders;
using Kruise.BusinessLogic.Services;
using Kruise.DataAccess.Postgres;
using Kruise.DataAccess.Postgres.Repositories;
using Kruise.Domain;
using Kruise.Domain.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Trace;
using Serilog;
using Serilog.Enrichers.Span;
using Telegram.Bot;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "KruiseAPI", Version = "v1" });
            //var filePath = Path.Combine(System.AppContext.BaseDirectory, "Kruise.API.xml");
            //c.IncludeXmlComments(filePath);
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please insert JWT with Bearer into field",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement 
            {
                   {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                       }
                     },
                     new string[] { }
                   }
            });
        });

var telegramConfiguration = builder.Configuration.GetSection(nameof(TelegramConfiguration));
builder.Services.Configure<TelegramConfiguration>(telegramConfiguration);
builder.Services.AddSingleton<ITelegramBotClient>(x =>
{
    var token = x.GetRequiredService<IOptions<TelegramConfiguration>>().Value;
    return new TelegramBotClient(token.Token);
});

builder.Services.AddDbContext<KruiseDbContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString(nameof(KruiseDbContext))));
builder.Services.AddIdentityCore<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<KruiseDbContext>();

builder.Services.AddSingleton<ISender, TelegaramSender>();
builder.Services.AddScoped<IPublishService, PublishService>();

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

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey()
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(c =>
{
    c.WithHeaders().AllowAnyHeader();
    c.WithOrigins().AllowAnyOrigin();
});

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
