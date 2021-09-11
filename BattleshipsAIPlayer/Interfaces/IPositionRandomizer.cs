namespace BattleshipsAIPlayer.Interfaces
{
    public interface IPositionRandomizer
    {
        (int x, int y) ReturnRandomPosition((int x, int y)[] positions);
    }
}