using ClassLibrary;
using Cocona;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;

var builder = CoconaApp.CreateBuilder();
builder.Services.AddSingleton<IShipFactory>(ShipFactory.GetInstance());
var app = builder.Build();

app.Run((IShipFactory shipFactory) =>
{
    var grid = new BattleShipGrid();
    var carrier = shipFactory.CreateShip(ShipClass.Carrier);
    var sub = shipFactory.CreateShip(ShipClass.Submarine);
    grid.PlaceShip(carrier, 'A', 1, true);
    grid.PlaceShip(sub, 'A', 10, false);
    grid.PlaceShot('A', 1);
    var panel = new Panel(grid.Grid.ArrayToString());
    AnsiConsole.Write(panel);
    var panel2 = new Panel(grid.MaskedGrid.ArrayToString());
    AnsiConsole.Write(panel2);
});