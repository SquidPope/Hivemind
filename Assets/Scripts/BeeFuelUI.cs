using UnityEngine;
using UnityEngine.UI;

public class BeeFuelUI : MonoBehaviour
{
    // Script to control the refueling screen for a bee.
    [SerializeField] Text beeName;
    [SerializeField] Image beeSprite;
    [SerializeField] Slider fuelLevel;

    //ToDo: I'd love to hold down the button and refill/empty slowly, but that would need a custom button script
    
    public void AddFuel()
    {
        //add in set increments.
    }

    public void RemoveFuel()
    {
        //remove in increments
    }
}
