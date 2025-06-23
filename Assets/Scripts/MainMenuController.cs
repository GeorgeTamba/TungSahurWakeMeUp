using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void OnPlayButtonPressed()
    {
        SceneManager.LoadScene("Level Kampung");
    }

    public void OnExitButtonPressed()
    {
        Application.Quit();
    }
}
