using static AStarPathFinding.CellPanelClass;

namespace AStarPathFinding
{
    public class GcostCalculator{
        public static int CalculateGcost(CellPanel currentCell, CellPanel adjacentCell, int cellWidth, int cellHeight)
        {
            // If you only allow horizontal and vertical movement (not diagonals), set Gcost to 1 for each step
            int GcostPerStep = 1;
            // Check if the adjacent cell is horizontally or vertically adjacent to the current cell
            bool isHorizontal = currentCell.Location.Y == adjacentCell.Location.Y && Math.Abs(currentCell.Location.X - adjacentCell.Location.X) == cellWidth + 1;
            bool isVertical = currentCell.Location.X == adjacentCell.Location.X && Math.Abs(currentCell.Location.Y - adjacentCell.Location.Y) == cellHeight + 1;

            // Calculate the Gcost based on the type of movement
            if (isHorizontal || isVertical)
            {
                return currentCell.Gcost + GcostPerStep;
            }

            // If the cells are not horizontally or vertically adjacent, return a higher Gcost
            // to discourage diagonal movement (you can adjust this value as needed)
            return currentCell.Gcost + 0 * GcostPerStep;
        }
    }
}