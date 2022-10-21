namespace NoughtsAndCrosses.Game.ComputerAI;

public struct GameData
{
    public int Starter;
    public List<List<int>> Order;
    public int Winner;

    /// <summary>
    /// Sets the properties to default values that render the data invalid for use
    /// </summary>
    public GameData()
    {
        Starter = 0;
        Order = new List<List<int>>();
        Winner = 0;
    }

    /// <summary>
    /// Sets up properties with proper values
    /// </summary>
    /// <param name="starter">Player 1 or 2 started?</param>
    /// <param name="order">The slots that were chosen in order</param>
    /// <param name="winner">The player that won the game</param>
    public GameData(int starter, List<int> order, int winner)
    {
        Order = new List<List<int>>();
        
        Starter = starter;
        Winner = winner;
        
        Order.Add(order);
        RotateOrder();
    }
    
    /// <summary>
    /// Parses a string into the struct properties
    /// </summary>
    /// <param name="data">The raw string of data to parse</param>
    /// <returns>(this) - returns the struct with all properties now parsed into it</returns>
    public GameData Format(string data)
    {
        var split = data.Split(" ");
        Starter = Convert.ToInt16(split[0]);
        
        for (var i = 1; i < split.Length - 2; i++)
            Order[0].Add(Convert.ToInt16(split[i]));
        RotateOrder();
            
        Winner = Convert.ToInt16(split[^1]);
        return this;
    }

    /// <summary>
    /// Rotates the Order 90 degrees recursively so each orientation of the Order is stored
    /// </summary>
    private void RotateOrder()
    {
        var temp = Order[0];
        for (var _ = 0; _ < 3; _++)
        {
            for (var i = 0; i < Order.Count; i++)
            {
                temp[i] = temp[i] switch
                {
                    1 => 3,
                    2 => 6,
                    3 => 9,
                    4 => 2,
                    // 5 can be ignored as it will never change
                    6 => 8,
                    7 => 1,
                    8 => 4,
                    9 => 7,
                    _ => temp[i]
                };
            }
            Order.Add(temp);
        }
    }

    public override string ToString()
    {
        var orderStr = Order[0].Aggregate(" ", (current, num) => current + (num + " "));
        return Starter + orderStr + Winner;
    }
}
