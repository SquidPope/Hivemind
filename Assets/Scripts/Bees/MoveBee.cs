using UnityEngine;

public class MoveBee : Bee
{
    // Bee that lets the player move around the level

    SwarmManager swarm;
    Camera mainCam;
    float moveSpeed = 5f;
    float speedMultiplier = 1.5f;
    float fullSpeedMultiplier = 3f;

    public override void Init(int id)
    {
        this.id = id;
        type = BeeType.Move;
        desiredDist *= 3f;
        description = "Movement bees allow the swarm to fly around. More fuel lets them move faster!"; //ToDo: Actually implement different speeds
        mainCam = Camera.main;
        swarm = SwarmManager.Instance;
    }

    protected override void Update()
    {
        if (isFueled && Input.GetMouseButton(0))
        {
            //move towards mouse
            float speed = moveSpeed;
            if (GetFuelCurrent() == GetFuelMax())
                speed *= fullSpeedMultiplier;
            else if (GetFuelCurrent() > 1)
                speed *= speedMultiplier;

            Vector3 newPos = Vector3.MoveTowards(swarm.transform.position, mainCam.ScreenToWorldPoint(Input.mousePosition), speed * Time.deltaTime);
            newPos.z = 0f;
            swarm.transform.position = newPos;
        }

        base.Update();
    }
}
