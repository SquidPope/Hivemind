using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeeFuelUI : MonoBehaviour
{
    // Script to control the BeeHUD
    [SerializeField] Text totalFuelDisplay;
    [SerializeField] GameObject beeListPanel;
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
        beeButtons.Find(x => x.GetId() == beeId).UpdateDisplay();
    }

    public void ToggleBeeList()
    {
        beeListPanel.SetActive(!beeListPanel.activeSelf);
    }

    public void MainMenuPressed()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("MainMenu");
    }
}
