using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostArea : MonoBehaviour
{
    public GameObject ghostPrefab;
    public int numberOfGhosts = 3;
    public GameObject[] Ghosts;
    public GameObject player;
    public Transform[] waypoints;
    public float baseSpawnRate = 3.0f;
    public float spawnRate = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        Ghosts = new GameObject[numberOfGhosts];

        StartCoroutine(SpawnGhosts());
    }

    // Update is called once per frame
    void Update()
    {
        // Get the player XP (for every 100 XP, decrease the spawn rate by 0.5)
        int playerXP = player.GetComponent<FPSController>().XP;
        spawnRate = baseSpawnRate - playerXP / 100 * 0.5f;
    }

    IEnumerator SpawnGhosts()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            for (int i = 0; i < numberOfGhosts; i++)
            {
                if (Ghosts[i] == null)
                {
                    Ghosts[i] = Instantiate(ghostPrefab, waypoints[0].position, Quaternion.identity);
                    Ghosts[i].GetComponent<NavAgentEnemy>().waypoints = waypoints;
                }
            }
        }
    }
}
