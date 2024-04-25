using UnityEngine;
using System.Collections.Generic;

public class BinaryTreeMaze : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    public GameObject wallPrefab;
    public GameObject passagePrefab;

    private GameObject[,] cells;

    void Start()
    {
        GenerateMaze();
    }

    void GenerateMaze()
    {
        cells = new GameObject[width, height];

        // Create the cells
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                cells[x, z] = Instantiate(wallPrefab, new Vector3(x, 0, z), Quaternion.identity) as GameObject;
            }
        }

        // Create the passages (Binary Tree algorithm)
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                if (x == width - 1 && z == height - 1)
                {
                    continue;
                }

                if (x == width - 1)
                {
                    Destroy(cells[x, z]);
                    cells[x, z] = Instantiate(passagePrefab, new Vector3(x, 0, z), Quaternion.identity) as GameObject;
                    continue;
                }

                if (z == height - 1)
                {
                    Destroy(cells[x, z]);
                    cells[x, z] = Instantiate(passagePrefab, new Vector3(x, 0, z), Quaternion.identity) as GameObject;
                    continue;
                }

                if (Random.value < 0.5f)
                {
                    Destroy(cells[x, z]);
                    cells[x, z] = Instantiate(passagePrefab, new Vector3(x, 0, z), Quaternion.identity) as GameObject;
                }
                else
                {
                    Destroy(cells[x, z + 1]);
                    cells[x, z + 1] = Instantiate(passagePrefab, new Vector3(x, 0, z + 1), Quaternion.identity) as GameObject;
                }
            }
        }
    }

}
