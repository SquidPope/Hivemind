using UnityEngine;

public class VisionBee : Bee
{
    // Bee that lets the player see farther in the level

    [SerializeField] GameObject darkOverlay;

    void Start()
    {
        darkOverlay.SetActive(false); //ToDo: set this to true and use fueling it as tutorial for fueling bees???
    }

    public override void ChangeFuelLevel(float amount)
    {
        base.ChangeFuelLevel(amount);

        darkOverlay.SetActive(!isFueled);
    }
}
