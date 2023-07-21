using static ClassLibrary.ShipClass;

namespace ClassLibrary;

public interface IShipFactory
{
    Ship CreateShip(ShipClass shipClass);
}

public class ShipFactory : IShipFactory
{
    private static ShipFactory? _instance;

    public static ShipFactory GetInstance()
    {
        return _instance ??= new ShipFactory();
    }
    
    private ShipFactory()
    {
    }

    public Ship CreateShip(ShipClass shipClass)
    {
        Ship ship = new()
        {
            ShipClass = shipClass,
            Name = shipClass.ToString()
        };

        ship.Size = shipClass switch
        {
            Carrier => 5,
            Battleship => 4,
            Destroyer => 3,
            Submarine => 3,
            PatrolBoat => 2,
            _ => throw new ArgumentOutOfRangeException(nameof(shipClass), shipClass, null)
        };

        return ship;
    }
}