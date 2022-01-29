using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Script to control a fired projectile
    [SerializeField] float lifespan;
    Rigidbody2D rigid;
    float timer = 0f;

    void Start()
    {
        if (lifespan <= 0f)
            lifespan = 5f;

        //Point at the mouse cursor and then launch.
        rigid = gameObject.GetComponent<Rigidbody2D>();
        transform.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition)); //Make sure we only rotate on the z axis!
        rigid.AddForce(50f * (transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition)));
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "Bee")
        {
            //do damage
            Debug.Log("POW");
            GameObject.Destroy(gameObject);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > lifespan)
            GameObject.Destroy(gameObject);
    }
}
