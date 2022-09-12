using System.Linq;

namespace Comrade.UnitTests.Tests.BotMovement;

public class BotMovement
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

        var positions = new List<(int, int)> {(startX, startY)};

        var i = 0;

        while (positions.Count > i)
        {
            if (positions[i].Item1 < finalX)
            {
                var resultMovementX = MovementX(positions[i].Item1, positions[i].Item2, positions);
                if (resultMovementX.Item1 == finalX && resultMovementX.Item2 == finalY)
                {
                    break;
                }
            }

            if (positions[i].Item2 < finalY)
            {
                var resultMovementY = MovementY(positions[i].Item1, positions[i].Item2, positions);
                if (resultMovementY.Item1 == finalX && resultMovementY.Item2 == finalY)
                {
                    break;
                }
            }

            i++;
        }


        var oto = positions.Where(x => x.Item1 == finalX && x.Item2 == finalY).Any();
        return oto;
    }

    private static (int, int) MovementX(int positionX, int positionY, List<(int, int)> positions)
    {
        positions.Add((positionX + positionY, positionY));
        return (positionX + positionY, positionY);
    }

    private static (int, int) MovementY(int positionX, int positionY, List<(int, int)> positions)
    {
        positions.Add((positionX, positionX + positionY));
        return (positionX, positionX + positionY);
    }
}