using Kruise.Domain;

namespace Kruise.UnitTests;

public class AccountModelTests
{
    [Fact]
    public void Create_ShouldReturnNewAccountModel()
    {
        // Arrange
        var name = "Name";

        // Act
        var accountModel = AccountModel.Create(name);

        // Assert
        Assert.False(accountModel.IsFailure);
    }

    [Theory]
    [MemberData(nameof(GenerateInvalidName))]
    public void Create_InvalidName_ShouldReturnBadRequest(string name)
    {
        // Act
        var accountModel = AccountModel.Create(name);

        // Assert
        Assert.True(accountModel.IsFailure);
    }

    private static IEnumerable<object[]> GenerateInvalidName()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return new string[] { " " };
            yield return new string[] { string.Empty };
            yield return new string[] { null };
            var invalidString = Enumerable.Range(0, AccountModel.MaxNameLength + 5);
            yield return new string[] { string.Join(string.Empty, invalidString) };
        }
    }
}
