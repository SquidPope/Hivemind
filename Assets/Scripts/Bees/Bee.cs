using UnityEngine;

public class Bee : MonoBehaviour
{
    // Base class for a robobee
    protected float fuelLevel;
    protected float fuelLevelMax;
    protected bool isFueled;

    //public float speed = 5f;
    //public float magnitude = 2f;

    public virtual void ChangeFuelLevel(float amount)
    {
        if (amount > 0f && fuelLevel < fuelLevelMax)
        {
            fuelLevel += amount;

            if (fuelLevel > fuelLevelMax)
                fuelLevel = fuelLevelMax; //ToDo: let the fuel manager thing know we went over, and by how much?
        }
        else if (amount < 0f && fuelLevel > 0f)
        {
            fuelLevel += amount; //ToDo: make this cleaner

            if (fuelLevel < 0f)
                fuelLevel = 0f;
        }

        isFueled = fuelLevel > 0f;
    }

    //in update rotate around parent pos?
    protected virtual void Update() //ToDo: If we get knocked away from the swarm center, move back towards it - define a desired dist
    {
        //ToDo: Stop if not fueled?
        //transform.RotateAround(SwarmManager.Instance.transform.position, Vector3.forward, angle);
        //transform.localPosition = new Vector3(Mathf.Cos(Time.fixedDeltaTime * speed) * magnitude, Mathf.Sin(Time.fixedDeltaTime * speed) * magnitude, 0);
    }
    //ToDo: get these moving erratically somehow
}
