namespace AStarPathFinding;
using static AStarPathFinding.CellPanelClass;

public static class ToggleObstacle
{
    public static void obstacleToggle(CellPanel cell, Dictionary<CellPanel, bool> obstacleMap)
    {
         if (!obstacleMap.ContainsKey(cell))
                obstacleMap[cell] = true; // Set as obstacle if not already marked
            else
                obstacleMap.Remove(cell); // Remove obstacle mark if already marked

            cell.BackColor = obstacleMap.ContainsKey(cell) ? Color.Red : Color.Black;
    }
}