namespace Comrade.UnitTests.Tests.TDD.BotMovement;

public class BotMovementInput
{
    public BotMovementInput(int startPositionX, int startPositionY, int finalPositionX, int finalPositionY)
    {
        StartPositionX = startPositionX;
        StartPositionY = startPositionY;
        FinalPositionX = finalPositionX;
        FinalPositionY = finalPositionY;
    }

    public int StartPositionX { get; set; }
    public int StartPositionY { get; set; }
    public int FinalPositionX { get; set; }
    public int FinalPositionY { get; set; }
}
