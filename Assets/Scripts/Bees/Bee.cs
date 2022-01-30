using UnityEngine;

public class Bee : MonoBehaviour
{
    // Base class for a robobee
    [SerializeField] protected int fuelLevelMax;

    protected int id;
    protected BeeType type = BeeType.Storage;
    protected int fuelLevel;
    protected bool isFueled;
    protected Vector2 desiredDist = Vector2.one;
    protected float homingSpeed = 1f;
    Vector3 direction;

    protected static string description = "Storage bees allow you to carry more total fuel.";

    //public float speed = 5f;
    //public float magnitude = 2f;

    public virtual void Init(int id) {}

    public int Id
    {
        get { return id; }
        set { id = value; } //Should only happen once when we're created.
    }

    public BeeType GetBeeType() { return type; }

    public bool IsFueled() { return isFueled; }
    public int GetFuelMax() { return fuelLevelMax; }
    public int GetFuelCurrent() { return fuelLevel; }

    public virtual void ChangeFuelLevel(int amount)
    {
        if (amount > 0 && fuelLevel < fuelLevelMax)
        {
            fuelLevel += amount;

            if (fuelLevel > fuelLevelMax)
                fuelLevel = fuelLevelMax; //ToDo: let the fuel manager thing know we went over, and by how much?
        }
        else if (amount < 0 && fuelLevel > 0)
        {
            fuelLevel += amount; //ToDo: make this cleaner

            if (fuelLevel < 0)
                fuelLevel = 0;
        }

        isFueled = fuelLevel > 0; //ToDo: Some visual indication of whether the bee is fueled or not
    }

    //in update rotate around parent pos?
    protected virtual void Update() 
    {
        //ToDo: If we get knocked away from the swarm center, move back towards it - define a desired dist
        
        if (transform.localPosition.x > desiredDist.x)
        {
            direction.x = -1;
        }
        else if (transform.localPosition.x < -desiredDist.x)
        {
            direction.x = 1;
        }

        if (transform.localPosition.y > desiredDist.y)
        {
            direction.y = -1;
        }
        else if (transform.localPosition.y < -desiredDist.y)
        {
            direction.y = 1;
        }

        if (direction != Vector3.zero)
        {
            transform.localPosition += direction * homingSpeed * Time.deltaTime;
            direction = Vector3.zero;
        }

        //If we end up too far away, or we aren't fueled, deactivate our effect / benefit, and wait for the player to come pick us back up?
        //ToDo: Stop if not fueled?
        //transform.RotateAround(SwarmManager.Instance.transform.position, Vector3.forward, angle);
        //transform.localPosition = new Vector3(Mathf.Cos(Time.fixedDeltaTime * speed) * magnitude, Mathf.Sin(Time.fixedDeltaTime * speed) * magnitude, 0);
    }
    //ToDo: get these moving erratically somehow
}
