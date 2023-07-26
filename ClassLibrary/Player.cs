namespace ClassLibrary;

public class Player
{
    public BattleShipGrid Grid { get; init; } = new();

    public ICollection<IShip> Ships { get; init; } = new List<IShip>();

    public IPlayStrategy PlayStrategy { get; set; }

    public IPlacementStrategy PlacementStrategy { get; set; }
}