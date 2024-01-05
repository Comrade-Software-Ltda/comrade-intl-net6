using Comrade.UnitTests.Tests.TDD.BotMovement.Validations;

namespace Comrade.UnitTests.Tests.TDD.BotMovement;

public class BotMovement(
    IBotMovementCheckIfIsAlreadyAtTheGoal botMovementCheckIfIsAlreadyAtTheGoal,
    IBotMovementCheckValidInitialPosition botMovementCheckValidInitialPosition)
{
    public bool IsAchievable(BotMovementInput botMovementInput)
    {
        var isAlreadyAtTheGoal = botMovementCheckIfIsAlreadyAtTheGoal.CheckIfIsAlreadyAtTheGoal(botMovementInput);

        if (isAlreadyAtTheGoal)
        {
            return true;
        }

        var validInitialPosition = botMovementCheckValidInitialPosition.CheckValidInitialPosition(botMovementInput);

        if (!validInitialPosition)
        {
            return false;
        }

        var positions = new Stack<(int XAxis, int YAxis)>();
        positions.Push((botMovementInput.StartPositionX, botMovementInput.StartPositionY));


        while (positions.TryPeek(out var item))
        {
            if (positions.TryPop(out item))
            {
                if (item.XAxis == botMovementInput.FinalPositionX && item.YAxis == botMovementInput.FinalPositionY)
                {
                    return true;
                }

                NewMethod(botMovementInput, item, positions);
            }
        }

        return false;
    }

    private static void NewMethod(BotMovementInput botMovementInput, (int XAxis, int YAxis) item,
        Stack<(int XAxis, int YAxis)> positions)
    {
        if (item.XAxis < botMovementInput.FinalPositionX)
        {
            positions.Push((item.XAxis + item.YAxis, item.YAxis));
        }

        if (item.YAxis < botMovementInput.FinalPositionY)
        {
            positions.Push((item.XAxis, item.XAxis + item.YAxis));
        }
    }
}
