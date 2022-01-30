using UnityEngine;

public class VisionBee : Bee
{
    // Bee that lets the player see farther in the level

    GameObject darkOverlay;

    public override void Init(int id)
    {
        this.id = id;
        type = BeeType.Vision;
        description = "Vision bees allow the swarm to see the area around them. More fuel lets them see farther.";
        darkOverlay = GameObject.FindGameObjectWithTag("DarkOverlay");
    }

    public override void ChangeFuelLevel(int amount)
    {
        base.ChangeFuelLevel(amount);

        if (darkOverlay == null)
            darkOverlay = GameObject.FindGameObjectWithTag("DarkOverlay");

        darkOverlay.SetActive(!isFueled);
    }
}
