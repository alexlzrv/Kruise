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
            for (int i = 0; i < 10; i++)
            {
                yield return new string[] { " " };
                yield return new string[] { string.Empty };
                yield return new string[] { null };
                var invalidString = Enumerable.Range(0, PostModel.MaxTitleLength + 5);
                yield return new string[] { string.Join(string.Empty, invalidString) };
            }
        }
    }
}
