using System.Net;
using System.Net.Http.Json;
using AutoFixture;
using Kruise.API.Contracts;
using Kruise.Domain;
using Xunit.Abstractions;

namespace Kruise.IntegrationTests;

public class AccountsControllerTests : BaseControllerTests
{
    public AccountsControllerTests(ITestOutputHelper outputHelper)
        : base(outputHelper)
    {
    }

    [Fact]
    public async Task Create_ShouldReturnAccountId()
    {
        // Arrange
        var fixture = new Fixture();
        var request = new CreateAccountRequest(fixture.Create<string>().Substring(0, AccountModel.MaxNameLength));

        // Act
        var responce = await Client.PostAsJsonAsync("api/accounts", request);

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
        var request = new CreateAccountRequest(name);

        // Act
        var responce = await Client.PostAsJsonAsync("api/accounts", request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, responce.StatusCode);
    }
}
