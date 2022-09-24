namespace NoughtsAndCrosses.Game;

public class Game
{
    private List<List<SlotState>> _gridState;
    private bool _computerStarts;
    private SlotState _playerPiece = SlotState.X;
    private SlotState _computerPiece = SlotState.O;

    public Game()
    {
        _computerStarts = false;
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
        
        RequestPlayerPiece();

        while (true)
        {
            List<int> results;
            if (_computerStarts)
            {
                ComputerPlay();
                results = CheckBoard();
                if (results[0] != (int) SlotState.Empty)
                {
                    if (OutputResults(results))
                        continue;
                    break;
                }
            }
            
            DrawGrid();
            PromptUser();
            results = CheckBoard();
            if (results[0] == (int)SlotState.Empty)
            {
                if (!_computerStarts)
                {
                    ComputerPlay();
                    results = CheckBoard();
                    if (results[0] != (int)SlotState.Empty)
                    {
                        if (!OutputResults(results))
                            break;
                    }
                }
                continue;
            }

            if (!OutputResults(results))
                break;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void RequestPlayerPiece()
    {
        Console.Write("Would you like to play X or O? (X by default) ");
        var result = Console.ReadLine();
        if (result is not { Length: > 0 }) return;
        switch (result.ToLower()[0])
        {
            case 'o':
                _playerPiece = SlotState.O;
                _computerPiece = SlotState.X;
                break;
            default:
                _playerPiece = SlotState.X;
                _computerPiece = SlotState.O;
                break;
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
    private static void DrawWinGrid(string winner, int x1, int x2, int x3)
    {
        var output = new List<List<SlotState>> 
            { new() { 0, 0, 0 }, new() { 0, 0, 0 }, new() { 0, 0, 0 } };
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
    /// TRUE - Adds player piece to board;
    /// FALSE - Re-prompts user to enter number as slot is occupied
    /// </summary>
    private void PromptUser(string message = "")
    {
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
            DrawGrid();
            PromptUser("Must enter a number!");
            return;
        }
        catch (IndexOutOfRangeException)
        {
            DrawGrid();
            PromptUser("Number must be between 1 & 9");
            return;
        }
        catch (OverflowException)
        {
            DrawGrid();
            PromptUser("Number must be between 1 & 9");
            return;
        }

        if (_gridState[(value - 1) / 3][(value - 1) % 3] == SlotState.Empty)
        {
            _gridState[(value - 1) / 3][(value - 1) % 3] = _playerPiece;
            return;
        }
        DrawGrid();
        PromptUser("Space already taken! Choose another...");
    }

    /// <summary>
    /// 
    /// </summary>
    private void ComputerPlay()
    {
        var random = new Random();
        var rand = random.Next() % 9 + 1;
        if (_gridState[(rand - 1) / 3][(rand - 1) % 3] == SlotState.Empty)
            _gridState[(rand - 1) / 3][(rand - 1) % 3] = _computerPiece;
        else
            ComputerPlay();
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

        var count = 0;
        foreach (var t in _gridState)
            foreach (var t1 in t)
                if (t1 != SlotState.Empty)
                    count++;

        return count == 9 ? new List<int>{ -1, 0, 0, 0 } : new List<int> { (int) SlotState.Empty, 0, 0, 0 };
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="results"></param>
    /// <returns></returns>
    private bool OutputResults(List<int> results)
    {
        if (results[0] > 0)
        {
            DrawWinGrid(((SlotState) results[0]).ToString(), results[1], results[2], results[3]);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{(SlotState)results[0]} Wins!");
            Console.ForegroundColor = ConsoleColor.White;
        }
        else
        {
            DrawGrid();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Draw!");
            Console.ForegroundColor = ConsoleColor.White;
        }

        if (results[0] == (int) _playerPiece)
            _computerStarts = true;
        else if (results[0] == (int)_computerPiece)
            _computerStarts = false;

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
        if (result is not { Length: > 0 }) return false;
        return result.ToLower()[0] == 'y';
    }
}
