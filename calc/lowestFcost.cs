namespace AStarPathFinding;
using static AStarPathFinding.CellPanelClass;

public static class LowestFCostCell{
    public static CellPanel GetTheLowestFscore(List<CellPanel> cellList){
        CellPanel lowestFCostCell = cellList[0];
            foreach (CellPanel cell in cellList)
            {
                if (cell.Fcost < lowestFCostCell.Fcost)
                    lowestFCostCell = cell;
            }
            
            return lowestFCostCell;
    }
}