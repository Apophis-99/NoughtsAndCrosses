namespace NoughtsAndCrosses.Game;

public class ComputerAi
{
    private List<List<SlotState>> _boardState = new();
    private SlotState _playerPiece;

    public ComputerAi(SlotState playerPiece)
    {
        _playerPiece = playerPiece;
    }
    
    public int CalculateNextMove(List<List<SlotState>> boardState)
    {
        _boardState = boardState;
        return IsFirstTurn() ? GetRandom() : GetPlayerBlock();
    }

    private bool IsFirstTurn()
    {
        var count = 0;
        foreach (var t in _boardState)
            foreach (var t1 in t)
                if (t1 == SlotState.Empty)
                    count++;
        return (count == 9);
    }

    private int GetRandom()
    {
        var rand = new Random().Next() % 9 + 1;
        return _boardState[(rand - 1) / 3][(rand - 1) % 3] != SlotState.Empty ? GetRandom() : rand;
    }

    private int GetPlayerBlock()
    {
        for (var i = 0; i < 3; i++)
        {
            if (_boardState[i][0] == _playerPiece && _boardState[i][1] == _playerPiece)
                return i * 3 + 1 + 2;
            if (_boardState[i][0] == _playerPiece && _boardState[i][2] == _playerPiece)
                return i * 3 + 1 + 1;
            if (_boardState[i][1] == _playerPiece && _boardState[i][2] == _playerPiece)
                return i * 3 + 1 + 0;
        }
        
        return 1;
    }
}
