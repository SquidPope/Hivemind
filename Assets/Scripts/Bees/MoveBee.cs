using UnityEngine;

public class MoveBee : Bee
{
    // Bee that lets the player move around the level

    SwarmManager swarm;
    Camera mainCam;
    float moveSpeed = 5f;

    public override void Init(int id)
    {
        this.id = id;
        type = BeeType.Move;
        description = "Movement bees allow the swarm to fly around. More fuel lets them move faster!"; //ToDo: Actually implement different speeds
        mainCam = Camera.main;
        swarm = SwarmManager.Instance;
    }

    protected override void Update()
    {
        //ToDo: check if we're fueled
        if (Input.GetMouseButton(0))
        {
            //move towards mouse
            Vector3 newPos = Vector3.MoveTowards(swarm.transform.position, mainCam.ScreenToWorldPoint(Input.mousePosition), moveSpeed * Time.deltaTime);
            newPos.z = 0f;
            swarm.transform.position = newPos;
        }

        base.Update();
    }
}
