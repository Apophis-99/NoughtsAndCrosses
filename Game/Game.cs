namespace NoughtsAndCrosses.Game;

public class Game
{
    private readonly List<List<SlotState>> _gridState;

    public Game()
    {
        _gridState = new List<List<SlotState>>
        {
            new List<SlotState>
            {
                SlotState.Empty,
                SlotState.Empty,
                SlotState.Empty
            },
            new List<SlotState>
            {
                SlotState.Empty,
                SlotState.Empty,
                SlotState.Empty
            },
            new List<SlotState>
            {
                SlotState.Empty,
                SlotState.Empty,
                SlotState.Empty
            }
        };

        while (true)
        {
            if (PromptUser())
            {
                _gridState = new List<List<SlotState>>
                {
                    new List<SlotState>
                    {
                        SlotState.Empty,
                        SlotState.Empty,
                        SlotState.Empty
                    },
                    new List<SlotState>
                    {
                        SlotState.Empty,
                        SlotState.Empty,
                        SlotState.Empty
                    },
                    new List<SlotState>
                    {
                        SlotState.Empty,
                        SlotState.Empty,
                        SlotState.Empty
                    }
                };
                Console.Write("Would you like to play again? (y/n) ");
                var result = Console.ReadLine();
                if (result is not { Length: > 0 }) break;
                if (result.ToLower()[0] == 'y')
                    break;
            }
        }
    }

    /// <summary>
    /// Draws 3x3 grid with current game moves mapped onto it
    /// </summary>
    private void DrawGrid()
    {
        Console.Clear();
        Console.WriteLine(
            $" {(_gridState[0][0] == SlotState.Empty ? " " : _gridState[0][0])} | {(_gridState[0][1] == SlotState.Empty ? " " : _gridState[0][1])} | {(_gridState[0][2] == SlotState.Empty ? " " : _gridState[0][2])} ");
        Console.WriteLine("---|---|---");
        Console.WriteLine(
            $" {(_gridState[1][0] == SlotState.Empty ? " " : _gridState[1][0])} | {(_gridState[1][1] == SlotState.Empty ? " " : _gridState[1][1])} | {(_gridState[1][2] == SlotState.Empty ? " " : _gridState[1][2])} ");
        Console.WriteLine("---|---|---");
        Console.WriteLine(
            $" {(_gridState[2][0] == SlotState.Empty ? " " : _gridState[2][0])} | {(_gridState[2][1] == SlotState.Empty ? " " : _gridState[2][1])} | {(_gridState[2][2] == SlotState.Empty ? " " : _gridState[2][2])} ");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="winner"></param>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="x3"></param>
    private void DrawWinGrid(string winner, int x1, int x2, int x3)
    {
        List<List<SlotState>> output = new List<List<SlotState>> 
            { new List<SlotState> { 0, 0, 0 }, new List<SlotState> { 0, 0, 0 }, new List<SlotState> { 0, 0, 0 } };
        output[(x1 - 1) / 3][(x1 - 1) % 3] = SlotState.X;
        output[(x2 - 1) / 3][(x2 - 1) % 3] = SlotState.X;
        output[(x3 - 1) / 3][(x3 - 1) % 3] = SlotState.X;
        
        Console.Clear();
        Console.WriteLine(
            $" {(output[0][0] == SlotState.Empty ? " " : winner)} | {(output[0][1] == SlotState.Empty ? " " : winner)} | {(output[0][2] == SlotState.Empty ? " " : winner)}"
            );
        Console.WriteLine("---|---|---");
        Console.WriteLine(
            $" {(output[1][0] == SlotState.Empty ? " " : winner)} | {(output[1][1] == SlotState.Empty ? " " : winner)} | {(output[1][2] == SlotState.Empty ? " " : winner)}"
        );
        Console.WriteLine("---|---|---");
        Console.WriteLine(
            $" {(output[2][0] == SlotState.Empty ? " " : winner)} | {(output[2][1] == SlotState.Empty ? " " : winner)} | {(output[2][2] == SlotState.Empty ? " " : winner)}"
        );
    }

    /// <summary>
    /// Prompts user to enter a number between 1 - 9 relating to a slot within the grid.
    /// Checks whether slot is available:
    /// TRUE - Adds player piece (X) to board;
    /// FALSE - Re-prompts user to enter number as slot is occupied
    /// </summary>
    private bool PromptUser(string message = "")
    {
        DrawGrid();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("[1-9] ");
        int value;
        try
        {
            value = Convert.ToInt32(Console.ReadLine());
            if (value is > 9 or < 1)
                throw new IndexOutOfRangeException();
        }
        catch (FormatException)
        {
            PromptUser("Must enter a number!");
            return false;
        }
        catch (IndexOutOfRangeException)
        {
            PromptUser("Number must be between 1 & 9");
            return false;
        }

        if (_gridState[(value - 1) / 3][(value - 1) % 3] == SlotState.Empty)
        {
            _gridState[(value - 1) / 3][(value - 1) % 3] = SlotState.X;
        }
        else
        {
            PromptUser("Space already taken! Choose another...");
            return false;
        }

        var results = CheckBoard();
        if (results[0] == (int)SlotState.Empty)
            return false;
        DrawWinGrid(((SlotState) results[0]).ToString(), results[1], results[2], results[3]);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"{ (SlotState) results[0] } Wins!");
        Console.ForegroundColor = ConsoleColor.White;

        return true;
    }
    
    /// <summary>
    /// Calculates whether the game has been won. It returns the grid cells that contain the winning line.
    /// </summary>
    /// <returns>{ 0: SlotState.Empty if game hasn't been won. SlotState.X / SlotState.O if game has been won.
    /// 1-3: The positions of the winning slots. }</returns>
    private List<int> CheckBoard()
    {
        if (_gridState[0][0] == SlotState.X && _gridState[0][1] == SlotState.X && _gridState[0][2] == SlotState.X)
            return new List<int> { (int) SlotState.X, 1, 2, 3 };
        if (_gridState[0][0] == SlotState.O && _gridState[0][1] == SlotState.O && _gridState[0][2] == SlotState.O)
            return new List<int> { (int) SlotState.O, 1, 2, 3 };
        if (_gridState[1][0] == SlotState.X && _gridState[1][1] == SlotState.X && _gridState[1][2] == SlotState.X)
            return new List<int> { (int) SlotState.X, 4, 5, 6 };
        if (_gridState[1][0] == SlotState.O && _gridState[1][1] == SlotState.O && _gridState[1][2] == SlotState.O)
            return new List<int> { (int) SlotState.O, 4, 5, 6 };
        if (_gridState[2][0] == SlotState.X && _gridState[2][1] == SlotState.X && _gridState[2][2] == SlotState.X)
            return new List<int> { (int) SlotState.X, 7, 8, 9 };
        if (_gridState[2][0] == SlotState.O && _gridState[2][1] == SlotState.O && _gridState[2][2] == SlotState.O)
            return new List<int> { (int) SlotState.O, 7, 8, 9 };
        
        if (_gridState[0][0] == SlotState.X && _gridState[1][0] == SlotState.X && _gridState[2][0] == SlotState.X)
            return new List<int> { (int) SlotState.X, 1, 4, 7 };
        if (_gridState[0][0] == SlotState.O && _gridState[1][0] == SlotState.O && _gridState[2][0] == SlotState.O)
            return new List<int> { (int) SlotState.O, 1, 4, 7 };
        if (_gridState[0][1] == SlotState.X && _gridState[1][1] == SlotState.X && _gridState[2][1] == SlotState.X)
            return new List<int> { (int) SlotState.X, 2, 5, 8 };
        if (_gridState[0][1] == SlotState.O && _gridState[1][1] == SlotState.O && _gridState[2][1] == SlotState.O)
            return new List<int> { (int) SlotState.O, 2, 5, 8 };
        if (_gridState[0][2] == SlotState.X && _gridState[1][2] == SlotState.X && _gridState[2][2] == SlotState.X)
            return new List<int> { (int) SlotState.X, 3, 6, 9 };
        if (_gridState[0][2] == SlotState.O && _gridState[1][2] == SlotState.O && _gridState[2][2] == SlotState.O)
            return new List<int> { (int) SlotState.O, 3, 6, 9 };
        
        if (_gridState[0][0] == SlotState.X && _gridState[1][1] == SlotState.X && _gridState[2][2] == SlotState.X)
            return new List<int> { (int) SlotState.X, 1, 5, 9 };
        if (_gridState[0][0] == SlotState.O && _gridState[1][1] == SlotState.O && _gridState[2][2] == SlotState.O)
            return new List<int> { (int) SlotState.O, 1, 5, 9 };
        if (_gridState[0][2] == SlotState.X && _gridState[1][1] == SlotState.X && _gridState[2][0] == SlotState.X)
            return new List<int> { (int) SlotState.X, 3, 5, 7 };
        if (_gridState[0][2] == SlotState.O && _gridState[1][1] == SlotState.O && _gridState[2][0] == SlotState.O)
            return new List<int> { (int) SlotState.O, 3, 5, 7 };

        return new List<int> { (int) SlotState.Empty, 0, 0, 0 };
    }
}
