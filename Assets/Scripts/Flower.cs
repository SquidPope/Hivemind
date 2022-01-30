using UnityEngine;

public class Flower : MonoBehaviour
{
    // Script to control a basic flower, which is used to give fuel to gatherer bees.
    [SerializeField] int fuelAmount = 1;
    new SpriteRenderer renderer;

    float timer;
    float timerMax = 4f;

    int gathererId = -1;

    Color currentColor;

    void Start()
    {
        renderer = gameObject.GetComponent<SpriteRenderer>();
        currentColor = renderer.color;
    }

    public void StartGathering(int id) { gathererId = id; }
    public void StopGathering() 
    {
        gathererId = -1;
        timer = 0f;
        currentColor.a = 1f;
        
        if (renderer != null)
            renderer.color = currentColor;
    }

    void Update()
    {
        if (gathererId >= 0)
        {
            timer += Time.deltaTime;
            currentColor.a -= 0.0003f;
            renderer.color = currentColor;
            if (timer >= timerMax)
            {
                SwarmManager.Instance.ChangeFuel(gathererId, fuelAmount); //ToDo: A little particle with the number of fuel gained would be nifty
                GameObject.Destroy(gameObject); //ToDo: Deactivate for a period instead?
            }
        }
    }
}
