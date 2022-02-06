using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonManager : MonoBehaviour
{

    public GameObject pauseMenu;
    private bool isPaused = false;

    private void Start()
    {
        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape") && isPaused == false)
        {
            isPaused = true;
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }

        else if (Input.GetKeyDown("escape") && isPaused == true)
        {
            isPaused = false;
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
    }


    public void OnNewGameButtonPressed()
    {
        SceneManager.LoadScene("Level_Kitchen");
    }

    public void OnContinueButtonPressed()
    {
        // for loading game
    }

    public void OnOptionsButtonPressed()
    {
        SceneManager.LoadScene("OptionsMenu");
    }

    public void OnQuitButtonPressed()
    {
        Application.Quit();
    }


    public void OnOptionsBackButtonPressed()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void OnPauseButtonPressed()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);

        if (Input.GetKeyDown("Escape") && isPaused == false)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }

        else if (Input.GetKeyDown("Escape") && isPaused == true)
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
    }

    public void OnResumeButtonPressed()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void OnSaveButtonPressed()
    {
        //save function to be implimented in future
    }

    public void OnLoadButtonPressed()
    {
        //load function to be implimented in future
        //similar to OnContinueButtonPressed()
    }

    public void OnQuitToMainMenuButtonPressed()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }


}