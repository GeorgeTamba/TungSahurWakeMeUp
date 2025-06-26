using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeTransition : MonoBehaviour
{
    public Image fadePanel;
    public float fadeDuration = 1f;

    public void FadeAndLoadScene(string sceneName)
    {
        StartCoroutine(FadeOutAndLoad(sceneName));
    }

    IEnumerator FadeOutAndLoad(string sceneName)
    {
        float t = 0;
        Color panelColor = fadePanel.color;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            panelColor.a = Mathf.Lerp(0, 1, t / fadeDuration);
            fadePanel.color = panelColor;
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }
}
