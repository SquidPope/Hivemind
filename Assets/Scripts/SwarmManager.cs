using System.Collections.Generic;
using UnityEngine;

public enum BeeType { Attack, Disruptor, Gatherer, Hearing, Move, Storage, Vision} //It's a beenum!
public class SwarmManager : MonoBehaviour
{
    // Script to keep track of all bees, and control them as a unit (hivemind mode)
    [SerializeField] List<GameObject> beefabs;
    List<Bee> bees;
    List<Bee> storageBees;

    //ToDo: Consider making additional storage bees progressively more expensive?
    public static readonly int[] beeCosts = {6, 7, 3, 2, 4, 9, 1}; //Currently set to one more than the bee type's max, although it's effectively equal to max because all bees start with one fuel.

    int totalFuelMax = 0; //Total fuel we can carry before we need to spend or drop off some - increased by getting more bees, especially storage bees
    int totalFuelCurrent = 0; //How much fuel we have on hand right now - increased by gathering
    
    
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

    void Start()
    {
        // ToDo: start the game with a move bee, a gather bee, and a vision bee. Storage bee should be unlocked, let the player make one
        bees = new List<Bee>();
        storageBees = new List<Bee>();

        CreateBee(BeeType.Gatherer);
        CreateBee(BeeType.Move);
        CreateBee(BeeType.Storage);
        CreateBee(BeeType.Vision);
    }

    public void ChangeFuel(int id, int amount)
    {
        //ToDo: We don't need to mess with totalFuelCurrent if we're just moving fuel between bees
        if (id >= 0 && id <= bees.Count)
        {
            Bee current = bees[id];
            totalFuelCurrent -= current.GetFuelCurrent(); //Remove this bee's fuel from the total, since it's about to change.
            current.ChangeFuelLevel(amount); //ToDo: Check if this succeeded!
            totalFuelCurrent += current.GetFuelCurrent(); //Add how much fuel the bee has, not the amount we just added/subtracted, in case we went under 0 or over the max

            BeeFuelUI.Instance.UpdateFuelTotal(totalFuelCurrent, totalFuelMax);
            BeeFuelUI.Instance.UpdateBeeDisplay(id);
        }
        else
        {
            Debug.LogError($"BeeId {id} out of range!");
        }
    }

    public void MoveFuel(Bee bee, int amount)
    {
        if (amount == 0)
            return;

        Bee storage;
        if (amount > 0)
        {
            //We're taking fuel out of storage
            storage = storageBees.Find(x => x.GetFuelCurrent() > 1);

            if (storage == null)
            {
                Debug.LogError("No fuel in storage!");
                return;
            }
        }
        else
        {
            //We're moving fuel into storage
            storage = storageBees.Find(x => x.GetFuelCurrent() < x.GetFuelMax());

            if (storage == null)
            {
                Debug.LogError("Storage is full!");
                return;
            }
        }

        //Move fuel between our selected storage bee and the one we're trying to change.
        storage.ChangeFuelLevel(-amount);
        bee.ChangeFuelLevel(amount);

        BeeFuelUI.Instance.UpdateBeeDisplay(storage.Id);
        BeeFuelUI.Instance.UpdateBeeDisplay(bee.Id);
    }

    public bool RepairBee(int id, BeeType type)
    {
        //get fuel in storage
        int fuelInStorage = 0;
        int cost = beeCosts[(int)type];
        foreach (Bee b in storageBees)
        {
            fuelInStorage += b.GetFuelCurrent();
            if (fuelInStorage >= cost + storageBees.Count) //Don't let the player spend ALL the storage bee's fuel, it needs one to stay active!
            {
                //activate/spawn the bee
                if (id >= 0)
                {
                    //this is an existing bee, reactivate them.
                    bees[id].ChangeFuelLevel(1);
                    //ToDo: Reactivate them in the list on the side!
                }
                else
                {
                    CreateBee(type);
                }
                //remove fuel from storage bee (make sure each storage bee is left with at least one!)
                int subtractedFuel = 0;
                for (int i = 0; i > storageBees.Count; i++)
                {
                    int fuel = storageBees[i].GetFuelCurrent(); //Get how much spare fuel this storage bee has.
                    int remainingCost = cost - subtractedFuel;
                    if (fuel == 1)
                    {
                        //Skip this bee if it only has 1 fuel.
                        continue;
                    }
                    else if (fuel > remainingCost) //Check if the spare fuel covers the cost of the new bee, if so just subtract that much.
                    {
                        storageBees[i].ChangeFuelLevel(-(remainingCost));
                        subtractedFuel += remainingCost;
                    }
                    else
                    {
                        storageBees[i].ChangeFuelLevel(fuel - 1); //Leave the bee with one fuel so it doesn't deactivate.
                        subtractedFuel += fuel - 1;
                    }
                
                    if (subtractedFuel == cost)
                        break;
                }

                return true;
            }
        }

        return false; //The player couldn't afford to spawn the bee, do red text or something to tell them that
    }

    public void CreateBee(BeeType type)
    {
        Bee newBee = GameObject.Instantiate(beefabs[(int)type], transform.position, Quaternion.identity).GetComponent<Bee>();
        newBee.Init(bees.Count);
        bees.Add(newBee);
        BeeFuelUI.Instance.AddBeeDisplay(newBee);
        totalFuelMax += newBee.GetFuelMax();
        ChangeFuel(newBee.Id, 1); //Start the new bee off with at least one fuel.
        newBee.transform.parent = transform;

        if (type == BeeType.Storage)
            storageBees.Add(newBee);
    }

    void Update()
    {
        transform.Rotate(0f, 0f, 30f * Time.deltaTime);

        if (Input.GetKeyUp(KeyCode.B))
        {
            //Fill all storage bees for testing
            foreach(Bee b in storageBees)
            {
                b.ChangeFuelLevel(b.GetFuelMax());
            }
        }
    }
}
