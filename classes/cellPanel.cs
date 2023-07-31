


namespace AStarPathFinding{
    public static class CellPanelClass{

        public class CellPanel : Panel
        {
            public int Gcost { get; set; }
            public int Hcost { get; set; }
            public int Fcost => Gcost + Hcost;
            public new CellPanel? Parent { get; set; }

            public int RowIndex { get; set; }
            public int ColumnIndex { get; set; }
        }
    } 
}