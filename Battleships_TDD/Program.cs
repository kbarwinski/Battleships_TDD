
using Battleships_TDD;
using System.Text.RegularExpressions;

int boardSize = 10;
var exampleBoard = new Board(boardSize, boardSize);
var exampleShips = new List<Ship>()
{
    new Ship("Battleship",1,5),
    new Ship("Destroyer 1",1,4),
    new Ship("Destroyer 2",1,4)
};

var exampleGame = new Game(exampleBoard, exampleShips);

var fieldsToCharDict = new Dictionary<FieldType, char>
{
    [FieldType.Miss] = 'M',
    [FieldType.Hit] = 'H',
    [FieldType.Sunk] = 'S',
    [FieldType.Ship] = 'X',
    [FieldType.Empty] = 'X',
};

void PrintBoard()
{
    Console.WriteLine("* * * LEGEND * * *".PadLeft(34));
    foreach (var fieldType in fieldsToCharDict.Keys)
        Console.WriteLine((fieldType.ToString() + ":" + fieldsToCharDict[fieldType] + " ").PadLeft(28));
    Console.WriteLine();

    Console.Write("".PadLeft(10));
    for(int i=0; i<boardSize; i++)
        Console.Write(" " + Convert.ToChar(i + (int)'A') + " ");
    Console.WriteLine();
   
    for(int i=0; i<boardSize; i++)
    {
        Console.Write((i + 1).ToString().PadLeft(10));
        for(int j=0; j<boardSize; j++)
        {
            var sign = fieldsToCharDict[exampleBoard.Fields.ElementAt(j + i * boardSize).FieldType];
            Console.Write(" " + sign + " ");
        }
        Console.WriteLine();
    }
}

int turnCounter = 0;
string prompt = "";
while (!exampleGame.HasFinished)
{
    try
    {
        Console.Clear();
        PrintBoard();

        Console.WriteLine("\n" + prompt);
        Console.WriteLine("Type in coordinates e.g. A2 or b3 (or quit to end the game):");
        string input = Console.ReadLine();

        if (input.ToLower().Equals("quit"))
            Environment.Exit(0);

        var coords = Regex.Split(input, "([A-z]\\d+)")[1];
        var row = int.Parse(coords[1..]);
        var col = (int)coords[0] % 32;

        exampleGame.Shoot(row - 1, col - 1);
        turnCounter++;
        Console.WriteLine();
        prompt = "";
        
    }
    catch(InvalidOperationException)
    {
        prompt = "Coordinates out of range!";
    }
    catch(Exception)
    {
        prompt = "Wrong input!";
    }
}
Console.Clear();
PrintBoard();
Console.WriteLine($"\nCongratulations! You've won in {turnCounter} turns.");