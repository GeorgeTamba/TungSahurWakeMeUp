using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public AudioClip pauseClickSound;

    private bool isPaused = false;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        pausePanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                StartCoroutine(PlayClickThenPause());
        }
    }

    public void PauseGame()
    {
        StartCoroutine(PlayClickThenPause());
    }

    private IEnumerator PlayClickThenPause()
    {
        if (pauseClickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(pauseClickSound);
            yield return new WaitForSecondsRealtime(pauseClickSound.length);
        }

        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        AudioListener.pause = true;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        AudioListener.pause = false;
    }

    public void RestartScene()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SceneManager.LoadScene("MainMenu");
    }
}
