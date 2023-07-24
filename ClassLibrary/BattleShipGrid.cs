using System.Text;
using CommunityToolkit.HighPerformance;
using static ClassLibrary.SquareType;

namespace ClassLibrary;

public enum SquareType
{
    Water,
    Splash,
    Ship,
    DamagedShip
}

public readonly struct SquarePosition : IEquatable<SquarePosition>
{
    public char Letter { get; init; }
    public int Number { get; init; }

    public int ColIdx => Number - 1;

    public int RowIdx => Letter - 'A';

    public static SquarePosition operator +(SquarePosition s, int x)
    {
        return s with { Number = s.Number + x };
    }

    public static SquarePosition operator +(SquarePosition s, char y)
    {
        return s with { Letter = (char)(s.Letter + y) };
    }

    public bool Equals(SquarePosition other)
    {
        return Letter == other.Letter && Number == other.Number;
    }

    public override bool Equals(object? obj)
    {
        return obj is SquarePosition other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Letter, Number);
    }

    public static bool operator ==(SquarePosition left, SquarePosition right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(SquarePosition left, SquarePosition right)
    {
        return !left.Equals(right);
    }

    public void Deconstruct(out int rowIdx, out int colIdx)
    {
        rowIdx = RowIdx;
        colIdx = ColIdx;
    }
}

public class Square
{
    public SquarePosition Position { get; set; }
    public SquareType SquareType { get; set; }
    public Ship? Ship { get; set; }

    public char GetChar()
    {
        return SquareType switch
        {
            Water => '.',
            Splash => '0',
            SquareType.Ship => Ship!.Symbol,
            DamagedShip => Ship!.DamagedSymbol,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}

public class BattleShipGrid
{
    private readonly Dictionary<SquarePosition, Square> _gridUpdates = new();

    public BattleShipGrid()
    {
        Grid = new char[10, 10];
        MaskedGrid = new char[10, 10];
        Grid.AsSpan2D().Fill('.');
        Grid.AsSpan2D().CopyTo(MaskedGrid);
    }

    public char[,] Grid { get; }
    public char[,] MaskedGrid { get; }

    public Square GetSquare(char letter, int number)
    {
        var position = new SquarePosition
        {
            Letter = letter,
            Number = number
        };

        if (_gridUpdates.TryGetValue(position, out var square)) return square;

        return new Square { Position = position };
    }

    public void UpdateSquare(Square square)
    {
        _gridUpdates[square.Position] = square;

        var (rowIdx, colIdx) = square.Position;

        Grid[rowIdx, colIdx] = square.GetChar();

        MaskedGrid[rowIdx, colIdx] = square.SquareType switch
        {
            Splash => '0',
            DamagedShip => '*',
            _ => '.'
        };
    }
}

public static class GridExtensions
{
    public static bool PlaceShip(this BattleShipGrid grid, Ship ship, char letter, int number, bool horizontal)
    {
        var shipSquares = new SquarePosition[ship.Size];
        var currentSquare = new SquarePosition { Letter = letter, Number = number };
        if (grid.GetSquare(letter, number).SquareType == SquareType.Ship) return false;
        shipSquares[0] = currentSquare;

        for (var squareIdx = 1; squareIdx < ship.Size; squareIdx++)
        {
            currentSquare = horizontal
                ? currentSquare + 1
                : currentSquare + (char)1;

            // Check if position is occupied
            if (grid.GetSquare(currentSquare.Letter, currentSquare.Number).SquareType == SquareType.Ship) return false;

            shipSquares[squareIdx] = currentSquare;
        }

        foreach (var squarePosition in shipSquares)
            grid.UpdateSquare(new Square
            {
                Position = squarePosition,
                SquareType = SquareType.Ship,
                Ship = ship
            });

        return true;
    }

    public static bool PlaceShot(this BattleShipGrid grid, char letter, int number)
    {
        var square = grid.GetSquare(letter, number);
        if (square.SquareType == Water)
        {
            square.SquareType = Splash;
            grid.UpdateSquare(square);
            return false;
        }

        square.SquareType = DamagedShip;
        square.Ship.Hits++;
        grid.UpdateSquare(square);
        return true;
    }

    public static string ArrayToString(this char[,] array)
    {
        StringBuilder sb = new("  ");
        for (var col = 1; col <= array.GetLength(1); col++) sb.Append($"{col,2}");
        sb.AppendLine();

        for (var row = 0; row <= array.GetUpperBound(0); row++)
        {
            sb.Append($"{(char)('A' + row)} ");
            foreach (var square in array.GetRow(row)) sb.Append($"{square,2}");

            sb.AppendLine();
        }

        return sb.ToString();
    }
}