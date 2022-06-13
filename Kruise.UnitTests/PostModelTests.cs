using Kruise.Domain;

namespace Kruise.UnitTests
{
    public class PostModelTests
    {
        [Fact]
        public void Create_ShouldReturnNewPostModel()
        {
            // Arrange
            var title = "Title";

            // Act
            var postModel = PostModel.Create(title);

            // Assert
            Assert.False(postModel.IsFailure);
        }

        [Theory]
        [MemberData(nameof(GenerateInvalidTitle))]
        public void Create_InvalidTitle_ShouldReturnBadRequest(string title)
        {
            // Act
            var postModel = PostModel.Create(title);

            // Assert
            Assert.True(postModel.IsFailure);
        }

        private static IEnumerable<object[]> GenerateInvalidTitle()
        {
            Random random = new Random();
            for (int i = 0; i < 10; i++)
            {
                yield return new[] { "" };
                yield return new[] { "".PadRight(random.Next(1, 100), ' ') };
                yield return new[] { (object)null };
            }
        }
    }
}
