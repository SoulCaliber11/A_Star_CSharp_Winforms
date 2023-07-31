using static AStarPathFinding.HcostCalculator;
using static AStarPathFinding.GcostCalculator;
using static AStarPathFinding.CellPanelClass;
using static AStarPathFinding.ToggleObstacle;
using static AStarPathFinding.LowestFCostCell;

namespace AStarPathFinding;

public partial class MainForm : Form
{
    public int cellWidth;
    public int cellHeight;
    private CellPanel? lastClickedPanel;
    private bool isCalculatingPath = false;
    public int rows = 15;
    public int columns = 20;

    private readonly Dictionary<CellPanel, bool> obstacleMap = new();
    private readonly List<CellPanel> openList = new();
    private readonly List<CellPanel> closedList = new();
    public MainForm()
    {
        InitializeComponent();
        // Subscribe to the KeyDown event of the form
        this.KeyDown += MainForm_KeyDown;
        this.Resize += MainForm_Resize;
        // Set the form's KeyPreview property to true to receive key events before controls.
        this.KeyPreview = true;
    }
    private void MainForm_Load(object? sender, EventArgs e)
    {
        // Calculate the number of rows and columns for the grid

        // Set the size of each cell in the grid
        cellWidth = (this.ClientSize.Width - 3) / columns; // 3 is the gap size
        cellHeight = (this.ClientSize.Height - 3) / rows;   // 3 is the gap size

        // Create the grid of panels
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Color backColor = Color.FromArgb(0, 0, 0);
                CellPanel cellPanel = new()
                {
                    Location = new Point(j * (cellWidth + 1), i * (cellHeight + 1)), // 1 is the border size
                    Size = new Size(cellWidth, cellHeight),
                    BackColor = backColor,
                    BorderStyle = BorderStyle.FixedSingle, // You can use any color you want here
                    RowIndex = i, // Store the row index in the CellPanel
                    ColumnIndex = j
                };

                cellPanel.MouseClick += new MouseEventHandler(CellPanel_MouseClick);
                this.Controls.Add(cellPanel);
            }
        }
    }

    private void MainForm_KeyDown(object? sender, KeyEventArgs e)
    {
        // Check if the space bar is pressed
        if (e.KeyCode == Keys.Space)
        {
            // Print something when the space bar is clicked
            foreach (CellPanel cell in this.Controls)
            {
                if (IsCellObstacle(cell))
                    continue;
                cell.BackColor = Color.FromArgb(0, 0, 0);
            }
        }
        if (e.KeyCode == Keys.Back)
        {
            foreach (CellPanel cell in this.Controls)
            {
                if (IsCellObstacle(cell))
                {
                    cell.BackColor = Color.FromArgb(0, 0, 0);
                    obstacleMap.Remove(cell);
                }
            }
        }
    }

    private void MainForm_Resize(object? sender, EventArgs e)
    {
        // Update the grid size when the form is resized
        UpdateGridSize();
    }

    private void UpdateGridSize()
    {
        // Calculate the number of rows and columns for the grid based on the new form size
        cellWidth = (this.ClientSize.Width - 3) / columns; // 3 is the gap size
        cellHeight = (this.ClientSize.Height - 3) / rows;   // 3 is the gap size

        // Update the size and location of each cell panel in the grid
        foreach (CellPanel cell in this.Controls)
        {
            cell.Location = new Point(cell.ColumnIndex * (cellWidth + 1), cell.RowIndex * (cellHeight + 1));
            cell.Size = new Size(cellWidth, cellHeight);
        }
    }

    private void CellPanel_MouseClick(object? sender, MouseEventArgs e)
    {
        CellPanel? clickedCell = (CellPanel)sender!;

        if (isCalculatingPath) return;

        if (e.Button == MouseButtons.Left)
        {
            clickedCell.BackColor = Color.Green;
            // Left-click: Calculate path
            if (lastClickedPanel != null && lastClickedPanel != clickedCell)
            {
                CalculatePath(lastClickedPanel, clickedCell);
                lastClickedPanel = null;
                clickedCell = null;
            }
        }
        else if (e.Button == MouseButtons.Right)
        {
            // Right-click: Toggle obstacle state
            obstacleToggle(clickedCell, obstacleMap);
            lastClickedPanel = null;
            clickedCell = null;

        }

        lastClickedPanel ??= clickedCell;


    }

    private bool IsCellObstacle(CellPanel cell)
    {
        return obstacleMap.ContainsKey(cell);
    }


    private async void CalculatePath(CellPanel startCell, CellPanel goalCell)
    {
        isCalculatingPath = true;
        openList.Clear();
        closedList.Clear();

        foreach (CellPanel cell in this.Controls)
        {
            cell.Gcost = int.MaxValue;
            cell.Parent = null;
        }

        startCell.Gcost = 0;
        openList.Add(startCell);
        while (openList.Count > 0)
        {
            CellPanel currentCell = GetTheLowestFscore(openList);
            openList.Remove(currentCell);
            closedList.Add(currentCell);

            if (currentCell == goalCell)
            {
                await ReconstructPath(startCell, goalCell);
                isCalculatingPath = false;
                MessageBox.Show("Shortest Path has been found!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            List<CellPanel> neighbors = GetNeighbors(currentCell);

            foreach (CellPanel neighbor in neighbors)
            {
                if (closedList.Contains(neighbor) || IsCellObstacle(neighbor))
                    continue;

                int tentativeGCost = CalculateGcost(currentCell, neighbor, cellHeight, cellWidth);

                if (!openList.Contains(neighbor) || tentativeGCost < neighbor.Gcost)
                {
                    neighbor.Gcost = tentativeGCost;
                    neighbor.Hcost = CalculateHcost(neighbor, goalCell);
                    neighbor.Parent = currentCell;
                    currentCell.BackColor = Color.Orange;
                    await Task.Delay(10); // Introduce a delay to visualize the path step by step

                    if (!openList.Contains(neighbor))
                    {
                        openList.Add(neighbor);
                        neighbor.BackColor = Color.Yellow;
                    }
                }
            }
        }

        isCalculatingPath = false;
        MessageBox.Show("Couldn't find the shortest path", "Path Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }




    private List<CellPanel> GetNeighbors(CellPanel cell)
    {
        List<CellPanel> neighbors = new();

        Point[] offsets = {
        new Point(-cellWidth - 1, 0),     // Left
        new Point(cellWidth + 1, 0),      // Right
        new Point(0, -cellHeight - 1),    // Up
        new Point(0, cellHeight + 1),     // Down
        new Point(-cellWidth - 1, -cellHeight - 1), // Diagonal Up-Left
        new Point(cellWidth + 1, -cellHeight - 1),  // Diagonal Up-Right
        new Point(-cellWidth - 1, cellHeight + 1),  // Diagonal Down-Left
        new Point(cellWidth + 1, cellHeight + 1)    // Diagonal Down-Right
    };

        foreach (Point offset in offsets)
        {
            Point neighborLocation = new(cell.Location.X + offset.X, cell.Location.Y + offset.Y);
            foreach (CellPanel neighbor in this.Controls)
            {
                if (neighbor.Location == neighborLocation)
                    neighbors.Add(neighbor);
            }
        }

        return neighbors;
    }

    public static async Task ReconstructPath(CellPanel? startCell, CellPanel goalCell){
        // Create an instance of PathReconstruct
            PathReconstruct pathReconstruct = new();

            // Call the ReconstructPath method on the instance
            await pathReconstruct.ReconstructPath(startCell, goalCell);
    }



}
