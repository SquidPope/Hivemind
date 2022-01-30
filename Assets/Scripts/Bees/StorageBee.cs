using UnityEngine;

public class StorageBee : Bee
{
    // Bee that stores extra fuel.
    public override void Init(int id)
    {
        this.id = id;
        type = BeeType.Storage;
        desiredDist *= 3f;
        description = "Storage Bees can carry a lot of fuel.";
    }
}
