namespace Comrade.UnitTests.Tests.TDD.BotMovement.Validations;

public class BotMovementCheckIfIsAlreadyAtTheGoal : IBotMovementCheckIfIsAlreadyAtTheGoal
{
    public bool CheckIfIsAlreadyAtTheGoal(BotMovementInput botMovementInput)
    {
        return botMovementInput.StartPositionX == botMovementInput.FinalPositionX &&
               botMovementInput.StartPositionY == botMovementInput.FinalPositionY;
    }
}
