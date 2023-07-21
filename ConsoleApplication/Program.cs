using ClassLibrary;
using Cocona;
using Microsoft.Extensions.DependencyInjection;

var builder = CoconaApp.CreateBuilder();
builder.Services.AddSingleton<ShipFactory>();
var app = builder.Build();

app.Run((ShipFactory shipFactory) =>
{
    var ship = shipFactory.CreateShip(ShipClass.Carrier);
    Console.WriteLine(ship.Dump());
});