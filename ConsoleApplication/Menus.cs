using ClassLibrary;
using Spectre.Console;

namespace ConsoleApplication;

public static class Menus
{
    public static (SquarePosition, bool h) PlaceShipPrompt()
    {
        var c = AnsiConsole.Ask<char>("Enter Letter");
        var n = AnsiConsole.Ask<int>("Enter Number");
        var h = AnsiConsole.Ask<bool>("Horizontal?");

        return (new SquarePosition { Letter = c, Number = n }, h);
    }

    public static void WriteGridPanel(char[,] charArray)
    {
        var panel = new Panel(charArray.ArrayToString());
        AnsiConsole.Write(panel);
    }
}