using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeeButton : MonoBehaviour
{
    // Script to control the button for each bee in the list
    [SerializeField] Image icon;
    [SerializeField] List<Image> honeycombIcons;
    [SerializeField] Button increaseButton;
    [SerializeField] Button decreaseButton;

    [SerializeField] BeeType type;
    [SerializeField] List<Sprite> beeSprites;
    [SerializeField] Sprite honeycombFull;
    [SerializeField] Sprite honeycombEmpty;

    Bee bee;

    //Basically, check what type we are
    //Then set our bee icon to the sprite for our type
    //take all the honeycomb images and turn off the ones we don't need, since some bees hold more than others
    public void Init(Bee bee)
    {
        this.bee = bee;
        type = bee.GetBeeType();

        icon.sprite = beeSprites[(int)type];

        //Iterate through the extra honeycomb images, turning them off.
        for (int i = bee.GetFuelMax() - 1; i < honeycombIcons.Count; i++)
        {
            honeycombIcons[i].gameObject.SetActive(false);
        }

        //if we are storage, disable increase and  decrease buttons.
        if (type == BeeType.Storage)
        {
            increaseButton.gameObject.SetActive(false);
            decreaseButton.gameObject.SetActive(false);
        }

        UpdateDisplay();
    }

    //ToDo: Disable the button entirely if the bee we're connected to goes down.

    public int GetId() { return bee.Id; }

    public void UpdateDisplay()
    {
        for (int i = 0; i < bee.GetFuelMax(); i++)
        {
            if (i > bee.GetFuelCurrent() - 1)
                honeycombIcons[i].sprite = honeycombEmpty;
            else
                honeycombIcons[i].sprite = honeycombFull;
        }

        if (type != BeeType.Storage)
        {
            if (bee.GetFuelCurrent() > 1)
                decreaseButton.interactable = true;
            else
                decreaseButton.interactable = false;

            if (bee.GetFuelCurrent() < bee.GetFuelMax() - 1)
                increaseButton.interactable = true;
            else
                increaseButton.interactable = false;
        }
    }
    
    //When we change our amount, check if we've hit maximum or minimum (1) and grey out the corrosponding button.
    public void IncreasePressed()
    {
        //Tell swarm manager to add one to bee and take one from a storage bee, the bee's value changing should trigger UpdateDisplay() for us.
        SwarmManager.Instance.MoveFuel(bee, 1);

        //ToDo: make sure the move actually went through.

        //decreaseButton.interactable = true;

        //if (bee.GetFuelCurrent() >= bee.GetFuelMax())
        //    increaseButton.interactable = false;
    }

    public void DecreasePressed()
    {
        //Same as increase but opposite.
        SwarmManager.Instance.MoveFuel(bee, -1);

        //increaseButton.interactable = true;

        //if (bee.GetFuelCurrent() <= 1)
        //    decreaseButton.interactable = false;
    }
}
