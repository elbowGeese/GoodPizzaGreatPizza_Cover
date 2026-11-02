using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Image blackFade;
    public float fadeTime = 1f;
    private Coroutine currentRoutine = null;

    public void StartGame()
    {
        if (currentRoutine == null)
        {
            currentRoutine = StartCoroutine(StartGameRoutine());
        }
    }

    IEnumerator StartGameRoutine()
    {
        float timer = 0f;
        while (timer <= fadeTime)
        {
            yield return null;
            timer += Time.deltaTime;

            float lerpAlpha = Mathf.Lerp(0f, 1f, timer / fadeTime);
            Color targetColor = blackFade.color;
            targetColor.a = lerpAlpha;
            blackFade.color = targetColor;
        }

        Color finalColor = blackFade.color;
        finalColor.a = 1f;
        blackFade.color = finalColor;

        // goes to play scene on first play
        // goes to news scene on return play
        SceneManager.LoadScene(1);
    }
}
