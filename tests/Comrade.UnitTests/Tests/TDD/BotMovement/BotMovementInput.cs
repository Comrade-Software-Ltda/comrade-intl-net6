namespace Comrade.UnitTests.Tests.TDD.BotMovement;

public class BotMovementInput(int startPositionX, int startPositionY, int finalPositionX, int finalPositionY)
{
    public int StartPositionX { get; set; } = startPositionX;
    public int StartPositionY { get; set; } = startPositionY;
    public int FinalPositionX { get; set; } = finalPositionX;
    public int FinalPositionY { get; set; } = finalPositionY;
}
