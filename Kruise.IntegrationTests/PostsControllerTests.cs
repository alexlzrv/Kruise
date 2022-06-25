using System.Net;
using System.Net.Http.Json;
using Kruise.API.Contracts;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit.Abstractions;

namespace Kruise.IntegrationTests;

public class PostsControllerTests : BaseControllerTests
{
    public PostsControllerTests(ITestOutputHelper outputHelper)
        : base(outputHelper)
    {
    }

    [Fact]
    public async Task Create_ShouldReturnPostId()
    {
        // Arrange
        var request = new CreatePostRequest(Guid.NewGuid().ToString());

        // Act
        var responce = await Client.PostAsJsonAsync("api/posts", request);

        // Assert
        responce.EnsureSuccessStatusCode();
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public async Task Create_InvalidTitle_ShouldReturnBadRequest(string title)
    {
        // Arrange
        var request = new CreatePostRequest(title);

        // Act
        var responce = await Client.PostAsJsonAsync("api/posts", request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, responce.StatusCode);
    }
}
