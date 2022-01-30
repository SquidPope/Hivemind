using UnityEngine;

public class Beericade : MonoBehaviour
{
    // Script for an anti-bee wall. should block bees but take damage from projectiles.

    [SerializeField] int hpMax = 5;

    int hp;

    void Start()
    {
        hp = hpMax;
    }

    void OnCollisionEnter2D (Collision2D other)
    {
        if (other.gameObject.tag == "Projectile") //ToDo: Show damage
            hp--;

        Debug.Log("Bonk");

        if (hp <= 0)
        {
            GameObject.Destroy(gameObject);
        }
    }
}
