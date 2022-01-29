using System.Collections.Generic;
using UnityEngine;

public class SwarmManager : MonoBehaviour
{
    // Script to keep track of all bees, and control them as a unit (hivemind mode)
    List<Bee> bees;
    //Keep track of total fuel? or should a specific bee do that?
    
    //  get our current abilities based on bee type?

    private static SwarmManager instance;
    public static SwarmManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = GameObject.FindGameObjectWithTag("SwarmManager");
                instance = go.GetComponent<SwarmManager>();
            }

            return instance;
        }
    }

    void Update()
    {
        transform.Rotate(0f, 0f, 30f * Time.deltaTime);
    }
    
}
