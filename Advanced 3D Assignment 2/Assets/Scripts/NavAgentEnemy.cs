using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAgentEnemy : MonoBehaviour
{
    // List of waypoints
    public Transform[] waypoints;
    private NavMeshAgent agent;
    public GameObject player;
    // Current waypoint
    public int currentWaypoint = 0;
    // Speed of the enemy
    public float baseSpeed = 2.0f;
    public float speed = 2.0f;
    // Distance to the current waypoint
    public float distanceToWaypoint = 0.5f;
    // Player dectection radius
    public float basePlayerDetectionRadius = 5.0f;
    public GameObject detectionRadiusSphere;
    public float playerDetectionRadius = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        // Set the initial position of the enemy
        transform.position = waypoints[currentWaypoint].position;
        // Get the NavMeshAgent component
        agent = GetComponent<NavMeshAgent>();
        // Set the destination of the NavMeshAgent to the first waypoint
        agent.SetDestination(waypoints[currentWaypoint].position);

    }

    // Update is called once per frame
    void Update()
    {
        // Get the player position
        player = GameObject.FindGameObjectWithTag("Player");

        // Get the the players current xp (for each 100 xp they have multiply speed and detection radius by 1.5)
        int playerXP = player.GetComponent<FPSController>().XP;
        speed = baseSpeed * (1 + playerXP / 100 * 2.5f);
        playerDetectionRadius = basePlayerDetectionRadius * (1 + playerXP / 100 * 0.5f);
        // Set the scale of the detection radius sphere
        detectionRadiusSphere.transform.localScale = new Vector3(playerDetectionRadius, playerDetectionRadius, playerDetectionRadius);
        // Set the speed of the NavMeshAgent
        agent.speed = speed;

        if (Vector3.Distance(transform.position, player.transform.position) < playerDetectionRadius)
        {
            // Set the destination of the NavMeshAgent to the player
            agent.SetDestination(player.transform.position);
        }
        else
        {
            // Set the destination of the NavMeshAgent to the next waypoint
            agent.SetDestination(waypoints[currentWaypoint].position);
            // Check if the enemy is close to the current waypoint
            if (Vector3.Distance(transform.position, waypoints[currentWaypoint].position) < distanceToWaypoint)
            {
                // Move to the next waypoint
                currentWaypoint++;
                // Check if the enemy has reached the last waypoint
                if (currentWaypoint >= waypoints.Length)
                {
                    // Reset the current waypoint to the first waypoint
                    currentWaypoint = 0;
                }
                // Set the destination of the NavMeshAgent to the next waypoint
                agent.SetDestination(waypoints[currentWaypoint].position);
            }
        }
        
    }

    public void SetupWaypoints(Transform[] waypoints)
    {
        this.waypoints = waypoints;
    }
}
