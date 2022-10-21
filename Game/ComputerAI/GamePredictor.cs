namespace NoughtsAndCrosses.Game.ComputerAI;

public class GamePredictor
{
    private readonly List<GameData> _games;
    
    public GamePredictor()
    {
        _games = new List<GameData>();
        ManageData();
    }
    
    private void ManageData()
    {
        var lines = File.ReadAllLines("../../Game/ComputerAI/SavedGames.txt");

        var temp = new List<string>(lines)
        { Capacity = 20 }; // Limit number of games that can be imported
        lines = temp.ToArray();

        foreach (var line in lines)
        {
            _games.Add(new GameData().Format(line));
        }
    }
}
