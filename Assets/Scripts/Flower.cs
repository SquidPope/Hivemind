using UnityEngine;

public class Flower : MonoBehaviour
{
    // Script to control a basic flower, which is used to give fuel to gatherer bees.
    [SerializeField] int fuelAmount = 1;
    new SpriteRenderer renderer;

    float gatherTimer = 0f;
    float gatherTimerMax = 2f;

    float respawnTimer = 0f;
    float respawnTimerMax = 8f;

    int gathererId = -1;
    bool isRespawning = false;

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
        gatherTimer = 0f;
        currentColor.a = 1f;

        if (renderer != null)
            renderer.color = currentColor;
    }

    void Update()
    {
        if (!isRespawning && gathererId >= 0)
        {
            gatherTimer += Time.deltaTime;
            currentColor.a -= 0.003f;
            renderer.color = currentColor;
            if (gatherTimer >= gatherTimerMax)
            {
                SwarmManager.Instance.ChangeFuel(gathererId, fuelAmount); //ToDo: A little particle with the number of fuel gained would be nifty
                renderer.enabled = false;
                isRespawning = true;
            }
        }
        else if (isRespawning)
        {
            respawnTimer += Time.deltaTime;

            if (respawnTimer >= respawnTimerMax)
            {
                respawnTimer = 0f;
                isRespawning = false;
                renderer.enabled = true;
            }
        }
    }
}
