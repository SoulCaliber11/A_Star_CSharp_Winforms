using static AStarPathFinding.CellPanelClass;


namespace AStarPathFinding;
public partial class PathReconstruct{
    public async Task ReconstructPath(CellPanel? startCell, CellPanel goalCell)
    {
        List<CellPanel> pathCells = new();
        CellPanel? currentCell = goalCell;
        while (currentCell != startCell)
        {
            pathCells.Add(currentCell!);
            currentCell = currentCell!.Parent;

        }
        pathCells.Add(startCell!);

        for (int i = pathCells.Count - 2; i >= 0; i--)
        {
            pathCells[i].BackColor = Color.Blue;

            // Introduce a delay here to visualize the path step by step
            await Task.Delay(20); // Adjust the delay time as needed (in milliseconds)
        }

        goalCell.BackColor = Color.Green;
        startCell!.BackColor = Color.Green;
    }
}