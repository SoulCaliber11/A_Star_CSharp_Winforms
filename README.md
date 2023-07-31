# A_Star_C-_Winforms
An implementation of A* path finding algorithm in Winforms using C#.

# Pathfinding Visualizer
This is a C# Windows Forms application that allows you to visualize pathfinding algorithms in action on a grid-based environment. The application lets you create obstacles, set a start and goal cell, and visualize the shortest path between them using the A* search algorithm.

# How to Use
Upon running the application, you will see a grid of cells, representing the environment where pathfinding will occur. Here's how to interact with the application:

- Left-click: Set the start and goal cells. The shortest path will be calculated between these cells using the A* algorithm.
- Right-click: Add or remove obstacles. Obstacles are impassable cells that the pathfinding algorithm will avoid.
- Spacebar: Clear the path and reset the grid.
- BackSpace: Clear all the obstacles.

# Code Structure
The main code for the pathfinding visualizer is contained in the MainForm class. Here's an overview of the main components and their functionalities:

Grid Creation: In the Form_Load method, the grid of panels is created based on the specified number of rows and columns. Each panel represents a cell in the grid.

User Interaction: The application handles user interactions, such as mouse clicks, using the CellPanel_MouseClick method. Left-clicking sets the start and goal cells, while right-clicking toggles obstacles.

Pathfinding: The CalculatePath method is responsible for performing the A* algorithm to find the shortest path between the start and goal cells. The algorithm stores the open list and closed list of cells and uses helper methods for calculating G-cost and H-cost.

Visualization: The visualization of the pathfinding process is achieved using asynchronous tasks and delays. The ReconstructPath method animates the pathfinding process by changing cell colors step by step.

Grid Resizing: The grid is automatically adjusted when the form is resized using the MainForm_Resize method.

Dependencies
The application relies on the System.Windows.Forms library for creating the GUI and handling user interactions.
