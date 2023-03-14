using Comrade.UnitTests.Tests.TDD.BotMovement.TestDatas;
using Comrade.UnitTests.Tests.TDD.BotMovement.Validations;
using Xunit;

namespace Comrade.UnitTests.Tests.TDD.BotMovement;

public class BotMovementTests
{
    private readonly Mock<IBotMovementCheckIfIsAlreadyAtTheGoal> _botMovementCheckIfIsAlreadyAtTheGoal = new();
    private readonly Mock<IBotMovementCheckValidInitialPosition> _botMovementCheckValidInitialPosition = new();

    [Theory]
    [ClassData(typeof(BotMovementTestData))]
    public void BotCanReachDestiny(BotMovementInput inputValue, bool expectedResult)
    {
        var botMovementCheckIfIsAlreadyAtTheGoal = new BotMovementCheckIfIsAlreadyAtTheGoal();
        var botMovementCheckValidInitialPosition = new BotMovementCheckValidInitialPosition();
        var botMovement = new BotMovement(botMovementCheckIfIsAlreadyAtTheGoal, botMovementCheckValidInitialPosition);

        var result = botMovement.IsAchievable(inputValue);
        Assert.Equal(result, expectedResult);
    }


    [Fact]
    public void BotCanReachDestiny_WithMocks()
    {
        _botMovementCheckIfIsAlreadyAtTheGoal.Setup(x => x.CheckIfIsAlreadyAtTheGoal(It.IsAny<BotMovementInput>()))
            .Returns(false);
        _botMovementCheckValidInitialPosition.Setup(x => x.CheckValidInitialPosition(It.IsAny<BotMovementInput>()))
            .Returns(true);

        var inputValue = new BotMovementInput(1, 1, 104, 61);


        var botMovement = new BotMovement(_botMovementCheckIfIsAlreadyAtTheGoal.Object,
            _botMovementCheckValidInitialPosition.Object);

        var result = botMovement.IsAchievable(inputValue);
        Assert.True(result);
    }
}
