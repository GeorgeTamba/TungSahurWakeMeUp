using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void OnPlayButtonPressed()
    {
        SceneManager.LoadScene("Kota");
    }

    public void OnExitButtonPressed()
    {
        Application.Quit();
    }
}
