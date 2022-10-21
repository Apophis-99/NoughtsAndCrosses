namespace NoughtsAndCrosses.Game.ComputerAI;

public class ComputerAi
{
    private List<List<SlotState>> _boardState = new();
    private readonly SlotState _playerPiece;
    private readonly SlotState _computerPiece;

    public ComputerAi(SlotState playerPiece)
    {
        _playerPiece = playerPiece;
        _computerPiece = (_playerPiece == SlotState.X ? SlotState.O : SlotState.X);
    }
    
    public int CalculateNextMove(List<List<SlotState>> boardState)
    {
        _boardState = boardState;

        if (IsFirstTurn())
            return GetRandom();
        
        var result = GetWinningMove();
        if (result > 0)
            return result;
        
        result = GetPlayerBlock();
        return result > 0 ? result : GetRandom();
    }

    private bool IsFirstTurn()
    {
        var count = _boardState.SelectMany(t => t).Count(t1 => t1 == SlotState.Empty);
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
            if (_boardState[i][0] == _playerPiece && _boardState[i][1] == _playerPiece && _boardState[i][2] == SlotState.Empty)
                return i * 3 + 1 + 2;
            if (_boardState[i][0] == _playerPiece && _boardState[i][2] == _playerPiece && _boardState[i][1] == SlotState.Empty)
                return i * 3 + 1 + 1;
            if (_boardState[i][1] == _playerPiece && _boardState[i][2] == _playerPiece && _boardState[i][0] == SlotState.Empty)
                return i * 3 + 1 + 0;
                
            if (_boardState[0][i] == _playerPiece && _boardState[1][i] == _playerPiece && _boardState[2][i] == SlotState.Empty)
                return 2 * 3 + 1 + i;
            if (_boardState[0][i] == _playerPiece && _boardState[2][i] == _playerPiece && _boardState[1][i] == SlotState.Empty)
                return 1 * 3 + 1 + i;
            if (_boardState[1][i] == _playerPiece && _boardState[2][i] == _playerPiece && _boardState[0][i] == SlotState.Empty)
                return 0 * 3 + 1 + i;
        }
        
        if (_boardState[0][0] == _playerPiece && _boardState[1][1] == _playerPiece && _boardState[2][2] == SlotState.Empty)
            return 9;
        if (_boardState[0][0] == _playerPiece && _boardState[2][2] == _playerPiece && _boardState[1][1] == SlotState.Empty)
            return 5;
        if (_boardState[1][1] == _playerPiece && _boardState[2][2] == _playerPiece && _boardState[0][0] == SlotState.Empty)
            return 1;
        
        if (_boardState[0][2] == _playerPiece && _boardState[1][1] == _playerPiece && _boardState[2][0] == SlotState.Empty)
            return 7;
        if (_boardState[0][2] == _playerPiece && _boardState[2][0] == _playerPiece && _boardState[1][1] == SlotState.Empty)
            return 5;
        if (_boardState[2][0] == _playerPiece && _boardState[1][1] == _playerPiece && _boardState[0][2] == SlotState.Empty)
            return 3;

        return 0;
    }

    private int GetWinningMove()
    {
        // Horizontal
        if (_boardState[0][0] == _computerPiece && _boardState[0][1] == _computerPiece && _boardState[0][2] == SlotState.Empty)
            return 3;
        if (_boardState[0][1] == _computerPiece && _boardState[0][2] == _computerPiece && _boardState[0][0] == SlotState.Empty)
            return 1;
        if (_boardState[0][0] == _computerPiece && _boardState[0][2] == _computerPiece && _boardState[0][1] == SlotState.Empty)
            return 2;
        
        if (_boardState[1][0] == _computerPiece && _boardState[1][1] == _computerPiece && _boardState[1][2] == SlotState.Empty)
            return 6;
        if (_boardState[1][1] == _computerPiece && _boardState[1][2] == _computerPiece && _boardState[1][0] == SlotState.Empty)
            return 4;
        if (_boardState[1][0] == _computerPiece && _boardState[1][2] == _computerPiece && _boardState[1][1] == SlotState.Empty)
            return 5;
        
        if (_boardState[2][0] == _computerPiece && _boardState[2][1] == _computerPiece && _boardState[2][2] == SlotState.Empty)
            return 9;
        if (_boardState[2][1] == _computerPiece && _boardState[2][2] == _computerPiece && _boardState[2][0] == SlotState.Empty)
            return 7;
        if (_boardState[2][0] == _computerPiece && _boardState[2][2] == _computerPiece && _boardState[2][1] == SlotState.Empty)
            return 8;
        
        // Vertical
        if (_boardState[0][0] == _computerPiece && _boardState[1][0] == _computerPiece && _boardState[2][0] == SlotState.Empty)
            return 7;
        if (_boardState[1][0] == _computerPiece && _boardState[2][0] == _computerPiece && _boardState[0][0] == SlotState.Empty)
            return 1;
        if (_boardState[0][0] == _computerPiece && _boardState[2][0] == _computerPiece && _boardState[1][0] == SlotState.Empty)
            return 4;
        
        if (_boardState[0][1] == _computerPiece && _boardState[1][1] == _computerPiece && _boardState[2][1] == SlotState.Empty)
            return 8;
        if (_boardState[1][1] == _computerPiece && _boardState[2][1] == _computerPiece && _boardState[0][1] == SlotState.Empty)
            return 2;
        if (_boardState[0][1] == _computerPiece && _boardState[2][1] == _computerPiece && _boardState[1][1] == SlotState.Empty)
            return 5;
        
        if (_boardState[0][2] == _computerPiece && _boardState[1][2] == _computerPiece && _boardState[2][2] == SlotState.Empty)
            return 9;
        if (_boardState[1][2] == _computerPiece && _boardState[1][2] == _computerPiece && _boardState[2][2] == SlotState.Empty)
            return 3;
        if (_boardState[0][2] == _computerPiece && _boardState[1][2] == _computerPiece && _boardState[2][2] == SlotState.Empty)
            return 6;
        
        // Diagonal
        if (_boardState[0][0] == _computerPiece && _boardState[1][1] == _computerPiece && _boardState[2][2] == SlotState.Empty)
            return 9;
        if (_boardState[1][1] == _computerPiece && _boardState[2][2] == _computerPiece && _boardState[0][0] == SlotState.Empty)
            return 1;
        if (_boardState[0][0] == _computerPiece && _boardState[2][2] == _computerPiece && _boardState[1][1] == SlotState.Empty)
            return 5;
        
        if (_boardState[0][2] == _computerPiece && _boardState[1][1] == _computerPiece && _boardState[2][0] == SlotState.Empty)
            return 7;
        if (_boardState[1][1] == _computerPiece && _boardState[2][0] == _computerPiece && _boardState[0][2] == SlotState.Empty)
            return 3;
        if (_boardState[0][2] == _computerPiece && _boardState[2][0] == _computerPiece && _boardState[1][1] == SlotState.Empty)
            return 5;

        return 0;
    }
}
