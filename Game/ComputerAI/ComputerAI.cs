namespace NoughtsAndCrosses.Game.ComputerAI;

public class ComputerAi
{
    private Grid _grid = new();
    private SlotState _playerPiece; // The piece that is not the AI
    private SlotState _computerPiece; // The piece that the AI is playing as on that particular move

    private readonly GamePredictor _predictor;

    public ComputerAi()
    {
        _predictor = new GamePredictor();
    }

    public int Calculate(Grid grid, SlotState piece)
    {
        _grid = grid;
        _computerPiece = piece;
        _playerPiece = _computerPiece == SlotState.X ? SlotState.O : SlotState.X;

        var index = GetPlayerBlock();
        if (index > 0)
            return index;
        index = GetWinningMove();
        if (index > 0)
            return index;
        index = _predictor.Calculate();
        if (index > 0)
            return index;
        
        return 1;
    }

    private int GetPlayerBlock()
    {
        // TODO GetPlayerBlock - make efficient board checks

        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                if (_grid.GetSlot((j + 1) + j switch
                    {
                        0 => 1,
                        1 => -1,
                        _ => -2
                    } + 3 * i) == _playerPiece && 
                    _grid.GetSlot((j + 1) + j switch
                    {
                        0 => 2,
                        1 => 1,
                        _ => -1
                    } + 3 * i) == _playerPiece && 
                    !_grid.Populated((j + 1) + 3 * i ))
                    return (j + 1) + 3 * i;

                if (_grid.GetSlot((j * 3 + 1) + j switch
                        {
                            0 => 3,
                            1 => -3,
                            _ => -6
                        } + i) == _playerPiece &&
                    _grid.GetSlot((j * 3 + 1) + j switch
                    {
                        0 => 6,
                        1 => 3,
                        _ => -3
                    } + i) == _playerPiece &&
                    !_grid.Populated((j * 3 + 1) + i))
                    return (j * 3 + 1) + i;
            }

            for (var j = 1; j <= 2; j++)
            {
                if (_grid.GetSlot(0) == _playerPiece &&
                    _grid.GetSlot(0) == _playerPiece &&
                    !_grid.Populated(0))
                    return 0;
            }
        }
        
        return 0;
    }

    private int GetWinningMove()
    {
        // TODO GetWinningMove - make efficient board checks
        
        return 0;
    }

    public void SubmitGame(GameData data)
    {
        _predictor.Games.Add(data);

        var file = File.AppendText("../../../Game/ComputerAI/SavedGames.txt");
        file.WriteLine(data.ToString());
        file.Close();
    }

}
