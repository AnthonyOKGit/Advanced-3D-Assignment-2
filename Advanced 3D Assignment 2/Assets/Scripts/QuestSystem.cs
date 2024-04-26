using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    // Public Instance
    public static QuestSystem instance;

    // 


    // Start is called before the first frame update
    void Start()
    {
        // Singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
