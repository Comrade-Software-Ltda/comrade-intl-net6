namespace Comrade.UnitTests.Tests.TDD.BotMovement.Validations;

public class BotMovementCheckValidInitialPosition : IBotMovementCheckValidInitialPosition
{
    public bool CheckValidInitialPosition(BotMovementInput botMovementInput)
    {
        if (botMovementInput is {StartPositionX: 0, StartPositionY: 0})
        {
            return false;
        }

        if (botMovementInput.FinalPositionY < botMovementInput.StartPositionY)
        {
            return false;
        }

        if (botMovementInput.FinalPositionX < botMovementInput.StartPositionX)
        {
            return false;
        }

        return true;
    }
}
