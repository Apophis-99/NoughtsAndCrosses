namespace NoughtsAndCrosses.Game.ComputerAI;

public struct GameData
{
    public int Starter;
    public readonly List<List<int>> Order;
    public int Winner;

    public GameData()
    {
        Starter = 0;
        Order = new List<List<int>>();
        Winner = 0;
    }

    public GameData Format(string data)
    {
        var split = data.Split(" ");
        Starter = Convert.ToInt16(split[0]);
        
        // TODO rotate board and save to order
        //for (var i = 1; i < split.Length - 2; i++)
            //Order.Add(Convert.ToInt16(split[i]));
            
        Winner = Convert.ToInt16(split[^1]);
        return this;
    }
}
