using ClassLibrary;
using Cocona;
using Microsoft.Extensions.DependencyInjection;

var builder = CoconaApp.CreateBuilder();
builder.Services.AddSingleton<IShipFactory>(ShipFactory.GetInstance());
var app = builder.Build();

app.Run((IShipFactory shipFactory) =>
{
    var ship = shipFactory.CreateShip(ShipClass.Carrier);
    Console.WriteLine(ship.Dump());
});