using System.Net;
using System.Net.Http.Json;
using Kruise.API.Contracts;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Kruise.IntegrationTests
{
    public class PostsControllerTests
    {
        [Fact]
        public async Task Create_ShouldReturnPostId()
        {
            // Arrange
            var application = new WebApplicationFactory<Program>();
            var client = application.CreateClient();
            var request = new CreatePostRequest { Title = Guid.NewGuid().ToString() };

            // Act
            var responce = await client.PostAsJsonAsync("api/posts", request);

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
            var application = new WebApplicationFactory<Program>();
            var client = application.CreateClient();
            var request = new CreatePostRequest { Title = title };

            // Act
            var responce = await client.PostAsJsonAsync("api/posts", request);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, responce.StatusCode);
        }
    }
}
