namespace ClassLibrary;

public class Player
{
    private readonly BattleShipGrid _grid;

    public IEnumerable<IShip> Ships { get; init; } = new List<IShip>();
}