using UnityEngine;

public class GatherBee : Bee
{
    // Bee that collects fuel from "flowers"
    Flower currentFlower;
    public override void Init(int id)
    {
        this.id = id;
        type = BeeType.Gatherer;
        desiredDist *= 3f;
        description = "Gather bees collect fuel from flowers.";
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isFueled)
            return;
            
        if (other.tag == "Flower")
        {
            Debug.Log("Collecting fuel");
            if (fuelLevel >= fuelLevelMax)
            {
                //display something to let the player know the gatherer is full and they need to make space
                Debug.Log("Oops, gatherer is full");
            }
            else
            {
                //tell flower it's depleted
                currentFlower = other.GetComponent<Flower>();
                currentFlower.StartGathering(id);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Flower")
        {
            currentFlower.StopGathering();
        }
    }
}
