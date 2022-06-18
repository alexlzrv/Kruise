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
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Create_InvalidName_ShouldReturnBadRequest(string name)
    {
        // Act
        var accountModel = AccountModel.Create(name);

        // Assert
        Assert.True(accountModel.IsFailure);
    }
}
