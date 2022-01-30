using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Script to control the main menu behavior
    [SerializeField] GameObject helpPanel;

    void Start()
    {
        helpPanel.SetActive(false);
    }

    public void PlayPressed()
    {
        SceneManager.LoadSceneAsync("SampleScene"); //Yes, I left the default name in.
    }

    public void QuitPressed()
    {
        Application.Quit();
    }

    public void HelpPressed()
    {
        helpPanel.SetActive(true);
    }

    public void BackPressed()
    {
        helpPanel.SetActive(false);
    }
}
