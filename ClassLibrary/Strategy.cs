/*
 * Classes for placing ships and shots implemented as strategy patterns.
 */

namespace ClassLibrary;

public interface IPlayStrategy
{
    SquarePosition PlaceShot();
}

public class HumanStrategy : IPlayStrategy
{
    public SquarePosition PlaceShot()
    {
        throw new NotImplementedException();
    }
}

public interface IPlacementStrategy
{
    (SquarePosition, bool) PlaceShip(ShipClass shipClass, Func<(SquarePosition, bool h)>? manipulator = null);
}

public class SetPlacementStrategy : IPlacementStrategy
{
    public (SquarePosition, bool) PlaceShip(ShipClass shipClass, Func<(SquarePosition, bool h)>? manipulator = null)
    {
        return shipClass switch
        {
            ShipClass.Carrier => (new SquarePosition { Letter = 'A', Number = 1 }, true),
            ShipClass.Battleship => (new SquarePosition { Letter = 'A', Number = 10 }, false),
            ShipClass.Destroyer => (new SquarePosition { Letter = 'J', Number = 8 }, true),
            ShipClass.Submarine => (new SquarePosition { Letter = 'H', Number = 1 }, false),
            ShipClass.PatrolBoat => (new SquarePosition { Letter = 'E', Number = 5 }, false),
            _ => throw new ArgumentOutOfRangeException(nameof(shipClass), shipClass, null)
        };
    }
}

public class HumanPlacementStrategy : IPlacementStrategy
{
    public (SquarePosition, bool) PlaceShip(ShipClass shipClass, Func<(SquarePosition, bool h)>? manipulator = null)
    {
        ArgumentNullException.ThrowIfNull(manipulator);

        return manipulator();
    }
}