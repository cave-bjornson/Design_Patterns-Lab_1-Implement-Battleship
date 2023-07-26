/*
 * Unfinished battleship game demonstrating the Factory, Singleton and strategy pattern.
 */

using ClassLibrary;
using Cocona;
using ConsoleApplication;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;

var builder = CoconaApp.CreateBuilder();
builder.Services.AddSingleton<IShipFactory>(ShipFactory.GetInstance());
var app = builder.Build();

app.Run((IShipFactory shipFactory) =>
{
    var player1 = new Player { PlacementStrategy = new SetPlacementStrategy() };
    var player2 = new Player { PlacementStrategy = new HumanPlacementStrategy() };
    foreach (ShipClass sc in Enum.GetValuesAsUnderlyingType<ShipClass>())
    {
        var ship = shipFactory.CreateShip(sc);
        player1.Ships.Add(ship);
        var (position, horizontal) = player1.PlacementStrategy.PlaceShip(sc);
        player1.Grid.PlaceShip(ship, position.Letter, position.Number, horizontal);
    }

    Menus.WriteGridPanel(player1.Grid.CharArray);

    Menus.WriteGridPanel(player2.Grid.CharArray);
    foreach (ShipClass sc in Enum.GetValuesAsUnderlyingType<ShipClass>())
    {
        var ship = shipFactory.CreateShip(sc);
        player2.Ships.Add(ship);
        AnsiConsole.WriteLine($"Place {sc}");
        var shipPlaced = false;
        while (shipPlaced is false)
        {
            var (position, horizontal) = player2.PlacementStrategy.PlaceShip(ship.ShipClass, Menus.PlaceShipPrompt);
            shipPlaced = player2.Grid.PlaceShip(ship, position.Letter, position.Number, horizontal);
            if (shipPlaced is false) AnsiConsole.WriteLine($"Cannot place {sc} there!");
        }

        Menus.WriteGridPanel(player2.Grid.CharArray);
    }
});