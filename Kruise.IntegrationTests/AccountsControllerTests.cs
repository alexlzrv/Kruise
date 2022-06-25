using System.Net;
using System.Net.Http.Json;
using Kruise.API.Contracts;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Kruise.IntegrationTests;

public class AccountsControllerTests
{
    [Fact]
    public async Task Create_ShouldReturnAccountId()
    {
        // Arrange
        var application = new WebApplicationFactory<Program>();
        var client = application.CreateClient();
        var request = new CreateAccountRequest(Guid.NewGuid().ToString());

        // Act
        var responce = await client.PostAsJsonAsync("api/accounts", request);

        // Assert
        responce.EnsureSuccessStatusCode();
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public async Task Create_InvalidName_ShouldReturnBadRequest(string name)
    {
        // Arrange
        var application = new WebApplicationFactory<Program>();
        var client = application.CreateClient();
        var request = new CreateAccountRequest(name);

        // Act
        var responce = await client.PostAsJsonAsync("api/accounts", request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, responce.StatusCode);
    }
}
