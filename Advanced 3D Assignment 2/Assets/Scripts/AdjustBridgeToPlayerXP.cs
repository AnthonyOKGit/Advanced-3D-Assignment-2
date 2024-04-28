using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustBridgeToPlayerXP : MonoBehaviour
{
    public GameObject player;
    public float defaultWidth;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        defaultWidth = this.transform.localScale.x;
    }

    void Update()
    {
        // For every 100 XP the player has, divide the scale of this object by 2
        int playerXP = player.GetComponent<FPSController>().XP;
        this.transform.localScale = new Vector3(defaultWidth / (1 + playerXP / 100), this.transform.localScale.y, this.transform.localScale.z);

    }
}
