using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonManager : MonoBehaviour
{

    public GameObject keybindsMenu;
    public GameObject optionsMenu;

    public GameObject pauseMenu;
    private bool isPaused = false;
    //public AudioSource audioSource;


    private void Start()
    {
        if (keybindsMenu != null)
            keybindsMenu.SetActive(false);


        //audioSource = gameObject.GetComponent<AudioSource>();

        if(pauseMenu != null) //should fix issues regarding the pause menu not being pressent on the main menu
        {
            pauseMenu.SetActive(false); 
        }

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


    public void ToggleKeybindsMenu()
    {
        if (keybindsMenu == null)
            return;

        keybindsMenu.SetActive(!keybindsMenu.activeSelf);
        optionsMenu.SetActive(!optionsMenu.activeSelf);
    }

    public void OnNewGameButtonPressed()
    {
        //audioSource.Stop();
        SceneManager.LoadScene("Level_Kitchen");
       //audioSource.Stop();
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

        if (Input.GetKeyDown("escape") && isPaused == false)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }

        else if (Input.GetKeyDown("escape") && isPaused == true)
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
