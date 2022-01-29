using UnityEngine;

public class GatherBee : Bee
{
    // Bee that collects fuel from "flowers"
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Flower")
        {
            Debug.Log("Collecting fuel");
            //start collecting?
            //Flower should have a limit, deactivate when depleted
        }
    }
}
