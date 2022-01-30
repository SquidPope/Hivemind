using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeeFuelUI : MonoBehaviour
{
    // Script to control the BeeHUD
    [SerializeField] Text totalFuelDisplay;
    [SerializeField] GameObject beeButtonScrollView;
    [SerializeField] GameObject buttonBeefab;
    
    List<BeeButton> beeButtons;

    static BeeFuelUI instance;
    public static BeeFuelUI Instance
    {
        get
        {
            GameObject go = GameObject.FindGameObjectWithTag("BeeFuelUI");
            instance = go.GetComponent<BeeFuelUI>();
            return instance;
        }
    }

    void Awake()
    {
        beeButtons = new List<BeeButton>();
    }

    public void AddBeeDisplay (Bee bee)
    {
        BeeButton newButton = GameObject.Instantiate(buttonBeefab).GetComponent<BeeButton>();
        newButton.transform.SetParent(beeButtonScrollView.transform, false);
        beeButtons.Add(newButton);
        newButton.Init(bee);
    }

    public void UpdateFuelTotal(int current, int max) 
    { 
        totalFuelDisplay.text = $"{current} / {max}";
    }

    public void UpdateBeeDisplay(int beeId)
    {
        Debug.Log($"updating display for {beeId}");
        beeButtons.Find(x => x.GetId() == beeId).UpdateDisplay();
    }
}
