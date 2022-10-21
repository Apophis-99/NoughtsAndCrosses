using NoughtsAndCrosses.Game.ComputerAI;

namespace NoughtsAndCrosses.Game;

public class Game
{
    #region Properties

    private int _numberOfPlayers; // 0 - computer plays itself; 1 - 1 player against computer; 2 - 2 players against each other
    private ComputerAi _computerAi1; // Player 1 if number of players is 0
    private ComputerAi _computerAi2; // Player 2 if number of players is 0 or 1
    private bool _player1Starts; // TRUE - player 1 starts; FALSE - player 2 starts

    private SlotState _player1Piece; // X or O
    private SlotState _player2Piece; // X or O (whatever player 1 isn't)

    private int _player1Score; // The score of player 1
    private int _player2Score; // The score of player 2
    
    private GameData _currentGameTracking; // The game data of the current game. The data is then sent the database and reset for the next game.

    private Grid _grid; // The grid data of the current game

    #endregion

    #region Methods
    
    #region Input Methods

    // TODO Switch ConsoleMessage and other arguments around
    
    /// <summary>
    /// Gets a character input from the user. It keeps asking for input until a valid argument has been passed.
    /// </summary>
    /// <param name="prompt">The prompt for what the user should be entering</param>
    /// <param name="message">The message that will show if the user has entered an invalid argument</param>
    /// <param name="validChars">The characters that the input should accept</param>
    /// <returns>The valid character that was inputted</returns>
    private char GetLetterInput(string prompt, ConsoleMessage message, string validChars = "abcdefghijklmnopqrstuvwxyz")
    {
        message.Print();
        Console.Write(prompt);

        char value;
        var temp = Console.ReadLine();
        if (temp == null)
            return GetLetterInput(prompt, new ConsoleMessage("Must enter a letter!", Message.Danger), validChars);
        if (temp.Length > 1)
            return GetLetterInput(prompt, new ConsoleMessage("Too many letters were entered", Message.Warning), validChars); 
        value = temp.ToLower().ToCharArray()[0];

        return !validChars.Contains(value) ? GetLetterInput(prompt, new ConsoleMessage($"'{value}' is not a valid character for this input.", Message.Warning), validChars) : value;
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
    private int GetIntegerInput(string prompt, ConsoleMessage message, int min = 1, int max = 9)
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
        catch (FormatException) { return GetIntegerInput(prompt, new ConsoleMessage("Must enter a number!", Message.Danger)); }
        catch (Exception) { return GetIntegerInput(prompt, new ConsoleMessage("Number must be between 1 & 9", Message.Warning)); }

        return value;
    }

    #endregion
    
    private void PlayerMove()
    {
        
    }

    private bool OutputResults(IReadOnlyList<int> results)
    {
        if (results[0] > (int) SlotState.Draw)
        {
            _grid.DrawWinningGrid((SlotState) results[0], results[1], results[2], results[3]);
            new ConsoleMessage($"{(SlotState) results[0]} Wins!", Message.Normal).Print();
        }
        else
        {
            _grid.DrawGrid();
            new ConsoleMessage("Draw!", Message.Warning).Print();
        }

        if (results[0] == (int)_player1Piece)
            _player1Starts = false;
        else if (results[0] == (int)_player2Piece)
            _player1Starts = true;
        
        _grid.ResetGrid();

        return GetLetterInput("Would you like to play again? (y/n) ", new ConsoleMessage(""), "yn") == 'y';
    }
    
    #endregion
}
