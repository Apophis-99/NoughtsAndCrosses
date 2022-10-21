using NoughtsAndCrosses.Game.ComputerAI;

namespace NoughtsAndCrosses.Game;

public class Game
{
    #region Properties

    private const int AiDelay = 600;

    private readonly int _numberOfPlayers; // 0 - computer plays itself; 1 - 1 player against computer; 2 - 2 players against each other
    private readonly ComputerAi _computerAi; // Can play as two players
    private bool _player1Starts; // TRUE - player 1 starts; FALSE - player 2 starts

    private readonly SlotState _player1Piece; // X or O
    private readonly SlotState _player2Piece; // X or O (whatever player 1 isn't)

    private int _player1Score; // The score of player 1
    private int _player2Score; // The score of player 2

    private readonly Grid _grid; // The grid data of the current game

    private List<int> _boardResults;

    #endregion

    #region Methods

    public Game()
    {
        _numberOfPlayers = GetIntegerInput("Enter number of players: ", 0, 2);

        _player1Piece = _numberOfPlayers == 0 ? SlotState.X :
            _numberOfPlayers == 1 ? GetLetterInput("Enter the piece you would like to play: ", "xo") == 'x' ? SlotState.X : SlotState.O : 
            GetLetterInput("Enter the piece Player 1 will play: ") == 'x' ? SlotState.X : SlotState.O;
        _player2Piece = _player1Piece == SlotState.X ? SlotState.O : SlotState.X;
        _player1Starts = true;

        _computerAi = new ComputerAi();
        _player1Score = 0;
        _player2Score = 0;

        _boardResults = new List<int>();

        _grid = new Grid();
        
        GameLoop();
    }

    private void GameLoop()
    {
        while (true)
        {
            Move();
            if (_boardResults[0] == (int)SlotState.Empty) continue;
            if (!OutputResults()) break;
        }
    }
    
    #region Input Methods

    /// <summary>
    /// Gets a character input from the user. It keeps asking for input until a valid argument has been passed.
    /// </summary>
    /// <param name="prompt">The prompt for what the user should be entering</param>
    /// <param name="message">The message that will show if the user has entered an invalid argument</param>
    /// <param name="validChars">The characters that the input should accept</param>
    /// <returns>The valid character that was inputted</returns>
    private char GetLetterInput(string prompt, string validChars = "abcdefghijklmnopqrstuvwxyz", ConsoleMessage message = new())
    {
        message.Print();
        Console.Write(prompt);

        var temp = Console.ReadLine();
        if (string.IsNullOrEmpty(temp))
            return GetLetterInput(prompt, validChars, new ConsoleMessage("Must enter a letter!", Message.Danger));
        if (temp.Length > 1)
            return GetLetterInput(prompt, validChars, new ConsoleMessage("Too many letters were entered", Message.Warning)); 
        var value = temp.ToLower().ToCharArray()[0];

        return !validChars.Contains(value) ? GetLetterInput(prompt, validChars, new ConsoleMessage($"'{value}' is not a valid character for this input.", Message.Warning)) : value;
    }

    /// <summary>
    /// Gets a integer input from the user. It keeps asking for input until a valid argument has been passed.
    /// </summary>
    /// <param name="prompt">The prompt for what the user should be entering</param>
    /// <param name="message">The message that will show if the user has entered an invalid argument</param>
    /// <param name="min">The smallest value the number can be</param>
    /// <param name="max">The largest value the number can be</param>
    /// <returns>The valid number that was inputted</returns>
    /// <exception cref="IndexOutOfRangeException"></exception>
    private int GetIntegerInput(string prompt, int min = 1, int max = 9, ConsoleMessage message = new())
    {
        message.Print();
        Console.Write(prompt);

        int value;
        try
        {
            value = Convert.ToInt32(Console.ReadLine());
            if (value > max || value < min)
                throw new IndexOutOfRangeException();
        }
        catch (FormatException) { return GetIntegerInput(prompt, min, max, new ConsoleMessage("Must enter a number!", Message.Danger)); }
        catch (Exception) { return GetIntegerInput(prompt, min, max, new ConsoleMessage($"Number must be between {min} & {max}", Message.Warning)); }

        return value;
    }

    #endregion

    #region Moves

    private void Move()
    {
        switch (_numberOfPlayers)
        {
            case 0:
                _grid.DrawGrid();
                if (_player1Starts)
                {
                    ComputerMove(1);
                    if ((_boardResults = _grid.CheckBoard())[0] != (int)SlotState.Empty)
                        return;
                    _grid.DrawGrid();
                    ComputerMove(2);
                    _boardResults = _grid.CheckBoard();
                }
                else
                {
                    ComputerMove(2);
                    if ((_boardResults = _grid.CheckBoard())[0] != (int)SlotState.Empty)
                        return;
                    _grid.DrawGrid();
                    ComputerMove(1);
                    _boardResults = _grid.CheckBoard();
                }
                break;
            case 1:
                if (_player1Starts)
                {
                    _grid.DrawGrid();
                    PlayerMove(1);
                    if ((_boardResults = _grid.CheckBoard())[0] != (int)SlotState.Empty)
                        return;
                }
                _grid.DrawGrid();
                ComputerMove(2);
                if ((_boardResults = _grid.CheckBoard())[0] != (int)SlotState.Empty)
                    return;
                if (!_player1Starts)
                {
                    _grid.DrawGrid();
                    PlayerMove(1);
                    _boardResults = _grid.CheckBoard();
                }
                break;
            case 2:
                _grid.DrawGrid();
                if (_player1Starts)
                {
                    PlayerMove(1);
                    if ((_boardResults = _grid.CheckBoard())[0] != (int)SlotState.Empty)
                        return;
                    _grid.DrawGrid();
                    PlayerMove(2);
                    _boardResults = _grid.CheckBoard();
                }
                else
                {
                    PlayerMove(2);
                    if ((_boardResults = _grid.CheckBoard())[0] != (int)SlotState.Empty)
                        return;
                    _grid.DrawGrid();
                    PlayerMove(1);
                    _boardResults = _grid.CheckBoard();
                }
                break;
        }
    }
    
    private void PlayerMove(int player)
    {
        if (_numberOfPlayers == 2)
            Console.Write($"Player {player} ");
        while (!_grid.SetSlot(GetIntegerInput("[1-9] "), (player == 1) ? _player1Piece : _player2Piece)) {}
    }

    private void ComputerMove(int player)
    {
        _grid.DrawGrid();
        var move = _computerAi.Calculate(_grid, player == 1 ? _player1Piece : _player2Piece);
        for (var i = 0; i < 3; i++)
        {
            Console.Write(".");
            Thread.Sleep(AiDelay / 3);
        }
        Console.WriteLine();
        _grid.SetSlot(move, (player == 1) ? _player1Piece : _player2Piece);
    }

    #endregion

    /// <summary>
    /// Outputs the grid in its winning state and prompts user to play again
    /// </summary>
    /// <returns>TRUE - play another game; FALSE - close game</returns>
    private bool OutputResults()
    {
        _computerAi.SubmitGame(new GameData(Convert.ToInt16(_player1Starts), _grid.Order, _boardResults[0] - 1));
        
        if (_boardResults[0] > (int) SlotState.Draw)
        {
            _grid.DrawWinningGrid((SlotState) _boardResults[0], _boardResults[1], _boardResults[2], _boardResults[3]);
            new ConsoleMessage($"{(SlotState) _boardResults[0]} Wins!", Message.Success).Print();
        }
        else
        {
            _grid.DrawGrid();
            new ConsoleMessage("Draw!", Message.Warning).Print();
        }

        if (_boardResults[0] == (int) _player1Piece)
            _player1Score += 1;
        else if (_boardResults[0] == (int) _player2Piece)
            _player2Score += 1;
        
        Console.WriteLine($"Player 1 Score: {_player1Score}\nPlayer 2 Score: {_player2Score}");
        
        if (_boardResults[0] == (int)_player1Piece)
            _player1Starts = false;
        else if (_boardResults[0] == (int)_player2Piece)
            _player1Starts = true;

        _grid.ResetGrid();

        return GetLetterInput("Would you like to play again? (y/n) ", "yn") == 'y';
    }
    
    #endregion
}
