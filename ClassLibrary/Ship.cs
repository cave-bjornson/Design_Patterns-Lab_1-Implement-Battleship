namespace ClassLibrary;

public interface IShip
{
    ShipClass ShipClass { get; init; }
    string Name { get; init; }
    Tuple<int, int>? BowPosition { get; set; }
    bool Horizontal { get; set; }
    int Size { get; set; }
    int Hits { get; set; }
    bool Destroyed { get; }
}

public class Ship : IShip
{
    public required ShipClass ShipClass { get; init; }
    public required string Name { get; init; }
    public Tuple<int, int>? BowPosition { get; set; }
    public bool Horizontal { get; set; }
    public int Size { get; set; }
    public int Hits { get; set; }
    public bool Destroyed => Hits >= Size;
}

public enum ShipClass
{
    Carrier, //5
    Battleship, //4
    Destroyer, //3
    Submarine, //3
    PatrolBoat //2
}