using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class BinaryTreeMaze : MonoBehaviour
{
    [SerializeField]
    private GameObject pillarPrefab;
    [SerializeField]
    private MazeCell cellPrefab;
    [SerializeField]
    private int mazeWidth;
    [SerializeField]
    private int mazeHeight;

    private MazeCell[,] mazeGrid;

    void Start()
    {
        mazeGrid = new MazeCell[mazeWidth, mazeHeight];

        // Instantiate the cells
        for (int i = 0; i < mazeWidth; i++)
        {
            for (int j = 0; j < mazeHeight; j++)
            {
                // Instantiate a cell prefab at the current position in the grid (Adjusted for the cell's size (4, 4, 4))
                mazeGrid[i, j] = Instantiate(cellPrefab, new Vector3(i*4, 0, j*4), Quaternion.identity);
                mazeGrid[i, j].name = "MazeCell_" + i + "_" + j;
            }
        }

        // Instantiate the pillars (starting the exact top left corner) + (2 more columns and rows for the pillars so they align with the cell walls)
        for (int i = 0; i < mazeWidth + 1; i++)
        {
            for (int j = 0; j < mazeHeight + 1; j++)
            {
                Instantiate(pillarPrefab, new Vector3(i*4-2, 0, j*4-2), Quaternion.identity);
            }
        }

        // Destroy the bottom wall of the first cell (Entrance of the maze)
        mazeGrid[0, 0].RemoveBottomWall();

        // Start generating the maze from the top left cell
        StartCoroutine(GenerateMaze(null, mazeGrid[0, 0]));
    }

    private IEnumerator GenerateMaze(MazeCell lastCell, MazeCell currentCell) {

        currentCell.markAsVisited();
        DestroyWalls(lastCell, currentCell);

        yield return new WaitForSeconds(0.1f);

        MazeCell nextCell;

        do {
            nextCell = GetNextCell(currentCell);

            if (nextCell != null) {
                yield return GenerateMaze(currentCell, nextCell);
            }
        } while (nextCell != null);

    }

    private void DestroyWalls(MazeCell lastCell, MazeCell currentCell) {
        
        if (lastCell == null) {
            return;
        }

        if (lastCell.transform.position.x < currentCell.transform.position.x) {
            lastCell.RemoveRightWall();
            currentCell.RemoveLeftWall();
        }

        if (lastCell.transform.position.x > currentCell.transform.position.x) {
            lastCell.RemoveLeftWall();
            currentCell.RemoveRightWall();
        }

        if (lastCell.transform.position.z < currentCell.transform.position.z) {
            lastCell.RemoveTopWall();
            currentCell.RemoveBottomWall();
        }

        if (lastCell.transform.position.z > currentCell.transform.position.z) {
            lastCell.RemoveBottomWall();
            currentCell.RemoveTopWall();
        }
    }

    private MazeCell GetNextCell(MazeCell currentCell) {
        var unvisitedCells = GetUnvistedNeighbouringCells(currentCell);

        return unvisitedCells.OrderBy(x => Random.Range(1,10)).FirstOrDefault();
    }

    private IEnumerable<MazeCell> GetUnvistedNeighbouringCells(MazeCell currentCell) {
        int x = (int)currentCell.transform.position.x / 4;
        int z = (int)currentCell.transform.position.z / 4;

        // Check Right
        if (x + 1 < mazeWidth) {
            var cell = mazeGrid[x + 1, z];
            if (!cell.visited) {
                yield return cell;
            }
        }

        // Check Left
        if (x - 1 >= 0) {
            var cell = mazeGrid[x - 1, z];
            if (!cell.visited) {
                yield return cell;
            }
        }

        // Check Top
        if (z + 1 < mazeHeight) {
            var cell = mazeGrid[x, z + 1];
            if (!cell.visited) {
                yield return cell;
            }
        }

        // Check Bottom
        if (z - 1 >= 0) {
            var cell = mazeGrid[x, z - 1];
            if (!cell.visited) {
                yield return cell;
            }
        }
    }

}
