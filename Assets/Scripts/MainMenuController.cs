using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Script to control the main menu behavior

    public void PlayPressed()
    {
        SceneManager.LoadSceneAsync("SampleScene"); //Yes, I left the default name in.
    }

    public void QuitPressed()
    {
        Application.Quit();
    }
}
