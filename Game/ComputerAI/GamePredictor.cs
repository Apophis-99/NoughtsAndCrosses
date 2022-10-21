namespace NoughtsAndCrosses.Game.ComputerAI;

public class GamePredictor
{
    public readonly List<GameData> Games;
    
    public GamePredictor()
    {
        Games = new List<GameData>();
        ManageData();
    }

    public int Calculate()
    {
        // TODO GamePredictor.Calculate - index each game and check for viable data
        
        return 0;
    }
    
    private void ManageData()
    {
        var lines = File.ReadAllLines("../../../Game/ComputerAI/SavedGames.txt");

        var temp = new List<string>(lines)
        { Capacity = 20 }; // Limit number of games that can be imported
        lines = temp.ToArray();

        foreach (var line in lines)
        {
            Games.Add(new GameData().Format(line));
        }
    }
    
}
