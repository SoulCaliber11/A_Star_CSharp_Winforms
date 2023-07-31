namespace AStarPathFinding;
using static AStarPathFinding.CellPanelClass;

public static class HcostCalculator
{
    public static int CalculateHcost(CellPanel currentCell, CellPanel goalCell)
        {


            // Calculate the distance between the two cells using Manhattan distance (sum of vertical and horizontal distances)
            int distanceRows = Math.Abs(currentCell.RowIndex - goalCell.RowIndex);
            int distanceColumns = Math.Abs(currentCell.ColumnIndex - goalCell.ColumnIndex);
            int totalDistance = distanceRows + distanceColumns;

            int Hcost = totalDistance;

            return Hcost;
        }
    
}