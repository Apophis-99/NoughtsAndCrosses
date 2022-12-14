namespace NoughtsAndCrosses.Game;

public class Grid
{
    private List<List<SlotState>> _grid;
    public List<int> Order;

    public Grid()
    {
        _grid = new List<List<SlotState>>();
        Order = new List<int>();
        ResetGrid();
    }

    /// <summary>
    /// Outputs the grid formatted correctly
    /// </summary>
    public void DrawGrid()
    {
        Console.Clear();
        Console.WriteLine(
            $" {(GetSlot(1) == SlotState.Empty ? " " : GetSlot(1))} | {(GetSlot(2) == SlotState.Empty ? " " : GetSlot(2))} | {(GetSlot(3) == SlotState.Empty ? " " : GetSlot(3))} ");
        Console.WriteLine("---|---|---");
        Console.WriteLine(
            $" {(GetSlot(4) == SlotState.Empty ? " " : GetSlot(4))} | {(GetSlot(5) == SlotState.Empty ? " " : GetSlot(5))} | {(GetSlot(6) == SlotState.Empty ? " " : GetSlot(6))} ");
        Console.WriteLine("---|---|---");
        Console.WriteLine(
            $" {(GetSlot(7) == SlotState.Empty ? " " : GetSlot(7))} | {(GetSlot(8) == SlotState.Empty ? " " : GetSlot(8))} | {(GetSlot(9) == SlotState.Empty ? " " : GetSlot(9))} ");
    }

    /// <summary>
    /// Sets the grid to only the winning slots and then outputs the grid
    /// </summary>
    /// <param name="winner">The piece that won the game</param>
    /// <param name="x1">The first winning slot</param>
    /// <param name="x2">The second winning slot</param>
    /// <param name="x3">The third winning slot</param>
    public void DrawWinningGrid(SlotState winner, int x1, int x2, int x3)
    {
        ResetGrid();
        SetSlot(x1, winner);
        SetSlot(x2, winner);
        SetSlot(x3, winner);
        DrawGrid();
    }

    /// <summary>
    /// Checks the board to see if anyone has won or if the board is filled resulting in a draw
    /// </summary>
    /// <returns>SlotState.X if X wins; SlotState.O if O wins; SlotState.Draw if grid is filled but there is no winner; SlotState.Empty if game is still ongoing.</returns>
    public List<int> CheckBoard()
    {
        for (var i = 0; i < 3; i++)
        {
            if (GetSlot(2 + 3 * i) == SlotState.X && GetSlot((2 - 1) + 3 * i) == SlotState.X && GetSlot((2 + 1) + 3 * i) == SlotState.X)
                return new List<int> { (int) SlotState.X, (2 - 1) + 3 * i, 2 + 3 * i, (2 + 1) + 3 * i };
            if (GetSlot(2 + 3 * i) == SlotState.O && GetSlot((2 - 1) + 3 * i) == SlotState.O && GetSlot((2 + 1) + 3 * i) == SlotState.O)
                return new List<int> { (int) SlotState.O, (2 - 1) + 3 * i, 2 + 3 * i, (2 + 1) + 3 * i };
            
            if (GetSlot(4 + i) == SlotState.X && GetSlot((4 + i) - 3) == SlotState.X && GetSlot((4 + i) + 3) == SlotState.X)
                return new List<int> { (int) SlotState.X, (4 + i) - 3, 4 + i, (4 + i) + 3 };
            if (GetSlot(4 + i) == SlotState.O && GetSlot((4 + i) - 3) == SlotState.O && GetSlot((4 + i) + 3) == SlotState.O)
                return new List<int> { (int) SlotState.O, (4 + i) - 3, 4 + i, (4 + i) + 3 };
        }

        for (var i = 1; i <= 2; i++)
        {
            if (GetSlot(5) == SlotState.X && GetSlot(5 - i * 2) == SlotState.X && GetSlot(5 + i * 2) == SlotState.X)
                return new List<int> { (int)SlotState.X, 5 - i * 2, 5, 5 + i * 2 };
            if (GetSlot(5) == SlotState.O && GetSlot(5 - i * 2) == SlotState.O && GetSlot(5 + i * 2) == SlotState.O)
                return new List<int> { (int)SlotState.O, 5 - i * 2, 5, 5 + i * 2 };
        }

        var count = 0;
        for (var i = 1; i <= 9; i++)
            if (GetSlot(i) != SlotState.Empty)
                count++;

        return count == 9 ? new List<int> { (int) SlotState.Draw, 0, 0, 0 } : new List<int> { (int)SlotState.Empty };
    }

    /// <summary>
    /// Gets the data held in the grid square at the argument index
    /// </summary>
    /// <param name="index">The index of the grid slot to acquire</param>
    /// <returns></returns>
    public SlotState GetSlot(int index)
    {
        return _grid[(index - 1) % 3][(index - 1) / 3];
    }
    /// <summary>
    /// Sets grid square according to index.
    /// </summary>
    /// <param name="index">The grid square to set</param>
    /// <param name="piece">The piece to pass into the slot</param>
    /// <returns>TRUE - slot was empty and has now been filled by the piece passed in; FALSE - slot was already populated and was not changed</returns>
    public bool SetSlot(int index, SlotState piece)
    {
        var temp = _grid[(index - 1) % 3][(index - 1) / 3];
        if (temp != SlotState.Empty)
            return false;
        _grid[(index - 1) % 3][(index - 1) / 3] = piece;
        Order.Add(index);
        return true;
    }

    /// <summary>
    /// Checks whether a particular grid slot is occupied by either player
    /// </summary>
    /// <param name="index">The index to check</param>
    /// <returns>TRUE - it is being used by X or O; FALSE - it is empty</returns>
    public bool Populated(int index)
    {
        return GetSlot(index) != SlotState.Empty;
    }

    /// <summary>
    /// Resets grid data to a 3x3 grid populated with SlotState.Empty
    /// </summary>
    public void ResetGrid()
    {
        _grid = new List<List<SlotState>>
        {
            new() { SlotState.Empty, SlotState.Empty, SlotState.Empty },
            new() { SlotState.Empty, SlotState.Empty, SlotState.Empty },
            new() { SlotState.Empty, SlotState.Empty, SlotState.Empty }
        };
        Order = new List<int>();
    }
}
