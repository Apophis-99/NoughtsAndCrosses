using NoughtsAndCrosses.Game;

namespace NoughtsAndCrosses.Game;
public enum Message
{
    Normal = 0,
    
    Warning = 2,
    Danger = 3
}

public readonly struct ConsoleMessage
{
    private readonly string _message;
    private readonly Message _type;
    
    public ConsoleMessage(string message, Message type = Message.Normal)
    {
        _message = message;
        _type = type;
    }

    public void Print()
    {
        Console.ForegroundColor = _type switch
        {
            Message.Warning => ConsoleColor.Yellow,
            Message.Danger => ConsoleColor.Red,
            _ => Console.ForegroundColor
        };
        Console.WriteLine(_message);
        Console.ForegroundColor = ConsoleColor.White;
    }
}
