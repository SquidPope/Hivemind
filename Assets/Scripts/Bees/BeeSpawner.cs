using UnityEngine;

public class BeeSpawner : MonoBehaviour
{
    // Script to control an "inactive" bee on the ground - either a new bee or an existing one that has been deactivated by damage
    public int id = -1; //-1 means this is a new bee otherwise it uses the id of the bee that was damaged so it can just be reactivated.
    public BeeType type;
    public GameObject spawnPrompt;
    SpriteRenderer promptRenderer;

    bool isTriggered = false;

    void Start()
    {
        spawnPrompt.SetActive(false);
        promptRenderer = spawnPrompt.GetComponent<SpriteRenderer>();
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
            promptRenderer.color = Color.white;
        }
    }

    void Update()
    {
        if (isTriggered)
        {
            //check for button press, activate bee
            if (Input.GetMouseButtonUp(1)) //ToDo: Everything else in the game is on mouse, this should be too (somehow)
            {
                bool repaired = SwarmManager.Instance.RepairBee(id, type);

                if (repaired)
                    GameObject.Destroy(gameObject);
                else
                    promptRenderer.color = Color.red; //ToDo: Changing to an exclamation point or something would probably be better?
            }
        }
    }
}
