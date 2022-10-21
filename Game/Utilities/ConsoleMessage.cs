namespace NoughtsAndCrosses.Game;

/// <summary>
/// NORMAL - White;
/// SUCCESS - Green;
/// WARNING - Yellow;
/// DANGER - Red
/// </summary>
public enum Message
{
    Normal = 0,
    Success = 1,
    Warning = 2,
    Danger = 3
}

public readonly struct ConsoleMessage
{
    private readonly string _message;
    private readonly Message _type;
    
    /// <summary>
    /// Takes a message and a type and stores to be printed later
    /// </summary>
    /// <param name="message">The message to print</param>
    /// <param name="type">The type of the message which determines the colour it will be outputted as</param>
    public ConsoleMessage(string message, Message type = Message.Normal)
    {
        _message = message;
        _type = type;
    }

    /// <summary>
    /// Outputs the stored message in different colours depending on the Message type
    /// </summary>
    public void Print()
    {
        Console.ForegroundColor = _type switch
        {
            Message.Success => ConsoleColor.Green,
            Message.Warning => ConsoleColor.Yellow,
            Message.Danger => ConsoleColor.Red,
            _ => Console.ForegroundColor
        };
        Console.WriteLine(_message);
        Console.ForegroundColor = ConsoleColor.White;
    }
}
