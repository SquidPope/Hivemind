using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBee : Bee
{
    // Bee that lets the player attack enemies / obstacles
    [SerializeField] GameObject projectilePrefab;

    float timer = 0f;
    float timerMax = 2f;

    void Start()
    {
        timer = timerMax;
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
