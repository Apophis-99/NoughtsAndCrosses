namespace NoughtsAndCrosses.Game;

public class Game
{
    private List<List<SlotState>> _gridState;

    public Game()
    {
        _gridState = new List<List<SlotState>>
        {
            new List<SlotState>
            {
                SlotState.Empty,
                SlotState.Empty,
                SlotState.Empty
            },
            new List<SlotState>
            {
                SlotState.Empty,
                SlotState.Empty,
                SlotState.Empty
            },
            new List<SlotState>
            {
                SlotState.Empty,
                SlotState.Empty,
                SlotState.Empty
            }
        };

        while (true)
        {
            PromptUser();
        }
    }

    /// <summary>
    /// Draws 3x3 grid with current game moves mapped onto it
    /// </summary>
    private void DrawGrid()
    {
        Console.Clear();
        Console.WriteLine(
            $" {(_gridState[0][0] == SlotState.Empty ? " " : _gridState[0][0])} | {(_gridState[0][1] == SlotState.Empty ? " " : _gridState[0][1])} | {(_gridState[0][2] == SlotState.Empty ? " " : _gridState[0][2])} ");
        Console.WriteLine("---|---|---");
        Console.WriteLine(
            $" {(_gridState[1][0] == SlotState.Empty ? " " : _gridState[1][0])} | {(_gridState[1][1] == SlotState.Empty ? " " : _gridState[1][1])} | {(_gridState[1][2] == SlotState.Empty ? " " : _gridState[1][2])} ");
        Console.WriteLine("---|---|---");
        Console.WriteLine(
            $" {(_gridState[2][0] == SlotState.Empty ? " " : _gridState[2][0])} | {(_gridState[2][1] == SlotState.Empty ? " " : _gridState[2][1])} | {(_gridState[2][2] == SlotState.Empty ? " " : _gridState[2][2])} ");
    }

    /// <summary>
    /// Prompts user to enter a number between 1 - 9 relating to a slot within the grid.
    /// Checks whether slot is available:
    /// TRUE - Adds player piece (X) to board;
    /// FALSE - Re-prompts user to enter number as slot is occupied
    /// </summary>
    private void PromptUser()
    {
        DrawGrid();
        Console.WriteLine();
        int value = 0;
        while (value is > 9 or < 1)
        {
            try
            {
                Console.Write("[1-9] ");
                var temp = Console.ReadLine();
                if (!string.IsNullOrEmpty(temp))
                    value = Convert.ToInt32(temp);
                else
                    throw new Exception("You must enter a number!");

                if (value is > 9 or < 1)
                    throw new Exception("Number must be between 1 & 9");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        if (_gridState[(value - 1) / 3][(value - 1) % 3] == SlotState.Empty)
            _gridState[(value - 1) / 3][(value - 1) % 3] = SlotState.X;
    }
}
