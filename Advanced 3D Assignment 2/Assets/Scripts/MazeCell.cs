using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell : MonoBehaviour
{
    [SerializeField]
    private GameObject leftWall;
    [SerializeField]
    private GameObject rightWall;
    [SerializeField]
    private GameObject topWall;
    [SerializeField]
    private GameObject bottomWall;
    [SerializeField]
    private GameObject debugBlock;

    public bool visited { get; set; }

    public void markAsVisited()
    {
        visited = true;
        debugBlock.SetActive(false);
    }
    
    public void RemoveLeftWall()
    {
        leftWall.SetActive(false);
    }

    public void RemoveRightWall()
    {
        rightWall.SetActive(false);
    }

    public void RemoveTopWall()
    {
        topWall.SetActive(false);
    }

    public void RemoveBottomWall()
    {
        bottomWall.SetActive(false);
    }
    
}
