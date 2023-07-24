using static ClassLibrary.ShipClass;

namespace ClassLibrary;

public interface IShipFactory
{
    Ship CreateShip(ShipClass shipClass);
}

public class ShipFactory : IShipFactory
{
    private static ShipFactory? _instance;

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

        switch (shipClass)
        {
            case Carrier:
                ship.Size = 5;
                ship.Symbol = 'C';
                break;
            case Battleship:
                ship.Size = 4;
                ship.Symbol = 'B';
                break;
            case Destroyer:
                ship.Size = 3;
                ship.Symbol = 'D';
                break;
            case Submarine:
                ship.Size = 3;
                ship.Symbol = 'S';
                break;
            case PatrolBoat:
                ship.Size = 2;
                ship.Symbol = 'P';
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(shipClass), shipClass, null);
        }

        ship.DamagedSymbol = char.ToLower(ship.Symbol);
        return ship;
    }

    public static ShipFactory GetInstance()
    {
        return _instance ??= new ShipFactory();
    }
}