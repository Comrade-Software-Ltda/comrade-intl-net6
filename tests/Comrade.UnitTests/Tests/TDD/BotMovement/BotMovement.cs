namespace Comrade.UnitTests.Tests.TDD.BotMovement;

public class BotMovement
{
    private readonly IBotMovementCheckIfIsAlreadyAtTheGoal _botMovementCheckIfIsAlreadyAtTheGoal;
    private readonly IBotMovementCheckValidInitialPosition _botMovementCheckValidInitialPosition;

    public BotMovement(IBotMovementCheckIfIsAlreadyAtTheGoal botMovementCheckIfIsAlreadyAtTheGoal,
        IBotMovementCheckValidInitialPosition botMovementCheckValidInitialPosition)
    {
        _botMovementCheckIfIsAlreadyAtTheGoal = botMovementCheckIfIsAlreadyAtTheGoal;
        _botMovementCheckValidInitialPosition = botMovementCheckValidInitialPosition;
    }


    public bool IsAchievable(BotMovementInput botMovementInput)
    {
        var isAlreadyAtTheGoal = _botMovementCheckIfIsAlreadyAtTheGoal.CheckIfIsAlreadyAtTheGoal(botMovementInput);

        if (isAlreadyAtTheGoal)
        {
            return true;
        }

        var validInitialPosition = _botMovementCheckValidInitialPosition.CheckValidInitialPosition(botMovementInput);

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

        return false;
    }
}
