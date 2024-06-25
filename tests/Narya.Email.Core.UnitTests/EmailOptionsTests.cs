using Narya.Email.Core.Builders;
using Narya.Email.Core.Models;
using Xunit;

namespace Narya.Email.Core.UnitTests;

public class EmailOptionsTests
{
    [Fact]
    public void EmailOptionsBuilder_ReturnsSpecificClassType()
    {
        // Arrange
        var result = new EmailOptionsBuilder().Build();

        // Act
        // var result = myClass.MyMethod();

        // Assert
        Assert.IsType<EmailOptions>(result.Value);
    }
}