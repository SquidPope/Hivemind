using UnityEngine;

public class QuitMenu : MonoBehaviour
{
    // Script to control a little quit popup
    [SerializeField] GameObject quitPanel;

    void Start() { quitPanel.SetActive(false); }
    public void CancelPressed() { quitPanel.SetActive(false); }
    public void YesPressed() { Application.Quit(); }

    void Update() 
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            quitPanel.SetActive(!quitPanel.activeSelf);
    }
}
