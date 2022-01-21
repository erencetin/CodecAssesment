
using Codec.WallE.Logic;

string gridSize = "5x5";
string commands = "FFRFLFLF";

var navigation = new Navigation(gridSize, commands);

var result = navigation.Run();

Console.WriteLine(result);
Console.Read();



