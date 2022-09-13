using Xunit;

namespace Comrade.UnitTests.Tests.BotMovement;

public class BotMovementTests
{
    [Fact]
    public void BotCanReachDestiny()
    {
        var result = BotMovementStack.IsAchievable(1, 1, 104, 61);
        Assert.True(result);
    }

    [Fact]
    public void BotCantReachDestiny()
    {
        var result = BotMovementStack.IsAchievable(1, 1, 50, 20);
        Assert.False(result);
    }
}