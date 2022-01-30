using UnityEngine;
using UnityEngine.UI;

public class Hive : MonoBehaviour
{
    // Script to control the end goal of the level
    [SerializeField] int fuelGoal; //Amount of fuel you need to deposit to finish the level.
    [SerializeField] Slider fuelLevel;
    [SerializeField] GameObject winPanel;

    int currentFuel = 0;

     public GameObject spawnPrompt;

    bool isTriggered = false;

    void Start()
    {
        spawnPrompt.SetActive(false);
        fuelLevel.maxValue = fuelGoal;
        winPanel.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        // if other is a bee let the player choose to spend fuel on reactivating.
        if (other.tag == "Bee")
        {
            isTriggered = true;
            spawnPrompt.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Bee")
        {
            isTriggered = false;
            spawnPrompt.SetActive(false);
        }
    }

    void Update()
    {
        if (isTriggered)
        {
            //check for button press, activate bee
            if (Input.GetMouseButtonUp(1))
            {
                //take fuel from storage
                currentFuel += SwarmManager.Instance.SpendFuel(fuelGoal - currentFuel);
                fuelLevel.value = currentFuel;

                if (currentFuel >= fuelGoal)
                {
                    winPanel.SetActive(true);
                }
            }
        }
    }
}
