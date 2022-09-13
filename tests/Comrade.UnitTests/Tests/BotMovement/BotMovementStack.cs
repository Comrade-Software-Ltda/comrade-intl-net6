using System.Linq;

namespace Comrade.UnitTests.Tests.BotMovement;

public class BotMovementStack
{
    public static bool IsAchievable(int startX, int startY, int finalX, int finalY)
    {
        if (startX == finalX && startY == finalY)
        {
            return true;
        }

        if (startX == 0 && startY == 0)
        {
            return false;
        }

        if (finalY < startY)
        {
            return false;
        }

        if (finalX < startX)
        {
            return false;
        }


        var positions = new Stack<(int, int)>();
        positions.Push((startX, startY));


        (int, int) item;

        while (positions.TryPeek(out item))
        {
            var success = positions.TryPop(out item);

            if (success)
            {
                if (item.Item1 == finalX && item.Item2 == finalY)
                {
                    return true;
                }

                if (item.Item1 < finalX)
                {
                    positions.Push((item.Item1 + item.Item2, item.Item2));
                }

                if (item.Item2 < finalY)
                {
                    positions.Push((item.Item1, item.Item1 + item.Item2));
                }
            }
        }
        
        return false;
    }
}