using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public void StartMenu()
    {
        StartCoroutine(MMenuDelay());
    }

    //takes player to the how to play screen
    public void HowToScreen()
    {
        StartCoroutine(HowToDelay());
    }

    //starts a new game session and loads game scene
    public void LoadGame()
    {
        StartCoroutine(PlayDelay());

    }

    public void GameOver()
    {
        StartCoroutine(GameOverDelay());
    }

    private IEnumerator GameOverDelay()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(1.5f);
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameOverScene");
    }

    private IEnumerator MMenuDelay()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("MenuScene");
    }

    private IEnumerator HowToDelay()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("HowToScene");
    }

    private IEnumerator PlayDelay()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("PlayScene");

        //if (FindObjectsOfType<ScoreCounter>().Length > 0)
        //{
        //    Destroy(FindObjectOfType<ScoreCounter>().gameObject);
        //} if we need score to reset!
    }
}
