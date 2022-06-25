using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Xunit.Abstractions;

namespace Kruise.IntegrationTests;

public class BaseControllerTests
{
    protected HttpClient Client { get; }

    public BaseControllerTests(ITestOutputHelper outputHelper)
    {
        var application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration((_, configurationBuilder) =>
                {
                    configurationBuilder.AddUserSecrets<BaseControllerTests>();
                });
            });
        Client = application.CreateDefaultClient(new LoggingHandler(outputHelper));
    }
}

public class LoggingHandler : DelegatingHandler
{
    private readonly ITestOutputHelper _outputHelper;

    public LoggingHandler(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }

    protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        _outputHelper.WriteLine(request.Method + " " + request.RequestUri?.ToString());
        PrintContent(request.Content).GetAwaiter().GetResult();
        var response = base.Send(request, cancellationToken);
        PrintContent(response.Content).GetAwaiter().GetResult();
        _outputHelper.WriteLine(response.StatusCode.ToString());
        return response;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        _outputHelper.WriteLine(request.Method + " " + request.RequestUri?.ToString());
        await PrintContent(request.Content);
        var response = await base.SendAsync(request, cancellationToken);
        await PrintContent(response.Content);
        _outputHelper.WriteLine(response.StatusCode.ToString());
        return response;
    }

    private async Task PrintContent(HttpContent? content)
    {
        if (content is null)
        {
            return;
        }

        var json = await content.ReadAsStringAsync();
        try
        {
            _outputHelper.WriteLine(JToken.Parse(json).ToString());
        }
        catch
        {
            _outputHelper.WriteLine(json);
        }
    }
}
