using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBee : Bee
{
    // Bee that lets the player attack enemies / obstacles
    [SerializeField] GameObject projectilePrefab;

    float timer = 0f;
    float timerMax = 2f;

    public override void Init(int id)
    {
        this.id = id;
        type = BeeType.Attack;
        description = "Attack bees can fire projectiles at opponents and obstacles.";
        timer = timerMax;
    }

    public override void ChangeFuelLevel(int amount)
    {
        base.ChangeFuelLevel(amount);

        isFueled = fuelLevel >= 5; //This and the disruptor bee need a lot of fuel to get started.
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetMouseButton(1))
        {
            timer += Time.deltaTime;

            if (timer >= timerMax)
            {
                timer = 0f;
                //get direction mouse cursor is currently in, aim there (projectile can do that itself)
                //fire a smaller bee
                GameObject.Instantiate(projectilePrefab, transform.position + transform.forward, transform.rotation);
            }
            
        }
    }
}
