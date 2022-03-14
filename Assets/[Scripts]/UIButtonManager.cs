using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonManager : MonoBehaviour
{
    public AudioSource audioSource;

    [Header("Option Panels")]
    public GameObject allOptionsMenu;
    public GameObject resolutionPanel;
    public GameObject audioPanel;
    public GameObject keybindsPanel;

    [Header ("Back Buttons")]
    public GameObject backToMainMenuButton;
    public GameObject backFromResolutionPanel;
    public GameObject backFromAudioPanelButton;
    public GameObject backFromKeybindsPanelButton;

    [Header("Hidden UI on Pause")]
    public GameObject miniMap;
    public GameObject lifeCounter;
    public GameObject pawsButton; //pause button
    public GameObject inventory;
    public GameObject healthBar;
    public GameObject onScreenControls;
    //add screen mounted controls here later.


    public GameObject pauseMenu;
    private bool isPaused = false;
    //public AudioSource audioSource;

    private int newGameCheck;

    public GameObject saveLoad;

    private void Start()
    {
        if (keybindsPanel != null)
            keybindsPanel.SetActive(false);
        

        //audioSource = gameObject.GetComponent<AudioSource>();

        if(pauseMenu != null) //should fix issues regarding the pause menu not being pressent on the main menu
        {
            pauseMenu.SetActive(false); 
        }

        allOptionsMenu.SetActive(true);
        backToMainMenuButton.SetActive(true);

        resolutionPanel.SetActive(false);
        audioPanel.SetActive(false);
        keybindsPanel.SetActive(false);
        backFromResolutionPanel.SetActive(false);
        backFromAudioPanelButton.SetActive(false);
        backFromKeybindsPanelButton.SetActive(false);

        miniMap.SetActive(true);
        lifeCounter.SetActive(true);
        pawsButton.SetActive(true);
        inventory.SetActive(true);
        healthBar.SetActive(true);
        onScreenControls.SetActive(true);



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
        if (keybindsPanel == null)
            return;

        keybindsPanel.SetActive(!keybindsPanel.activeSelf);
        allOptionsMenu.SetActive(!allOptionsMenu.activeSelf);
    }

    public void OnNewGameButtonPressed()
    {
        //audioSource.Stop();
        newGameCheck = 1;
        PlayerPrefs.SetInt("NewGameCheck", newGameCheck); // when we load the play scene we need to see if the game is new or loaded
        SceneManager.LoadScene("Level_Kitchen");
       //audioSource.Stop();
    }

    public void OnContinueButtonPressed()
    {
        newGameCheck = 0;
        PlayerPrefs.SetInt("NewGameCheck", newGameCheck); // when we load the play scene we need to see if the game is new or loaded
        SceneManager.LoadScene("Level_Kitchen");
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
        audioSource.Pause();

        //if (Input.GetKeyDown("escape") && isPaused == false)
        //{
        //    Time.timeScale = 0;
        //    pauseMenu.SetActive(true);
        //}

        //else if (Input.GetKeyDown("escape") && isPaused == true)
        //{
        //    Time.timeScale = 1;
        //    pauseMenu.SetActive(false);
        //}

        

        miniMap.SetActive(false);
        lifeCounter.SetActive(false);
        pawsButton.SetActive(false);
        inventory.SetActive(false);
        healthBar.SetActive(false);
        onScreenControls.SetActive(false);
    }

    public void OnResumeButtonPressed()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        audioSource.Play();

        miniMap.SetActive(true);
        lifeCounter.SetActive(true);
        pawsButton.SetActive(true);
        inventory.SetActive(true);
        healthBar.SetActive(true);
        onScreenControls.SetActive(true);
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


    //Delay for button sound
    //private IEnumerator DelayForUIButtonSound(float delay)
    //{
    //    yield return new WaitForSeconds(delay);

    //}

    //=========================================================================== OPTION MENU BUTTONS =====================================================================

    public void OnResolutionButtonPressed()
    {
        allOptionsMenu.SetActive(false);
        backToMainMenuButton.SetActive(false);

        resolutionPanel.SetActive(true);
        backFromResolutionPanel.SetActive(true);

    }

    public void OnAudioButtonPressed()
    {
        allOptionsMenu.SetActive(false);
        backToMainMenuButton.SetActive(false);


        audioPanel.SetActive(true);
        backFromAudioPanelButton.SetActive(true);

    }

    public void OnKeybindingsButtonPressed()
    {
        allOptionsMenu.SetActive(false);
        backToMainMenuButton.SetActive(false);

        keybindsPanel.SetActive(true);
        backFromKeybindsPanelButton.SetActive(true);
    }



    //========================================================================= OPTIONS MENU BACK BUTTONS ==================================================================

    public void OnKeybindsBackButtonPressed() 
    {
        allOptionsMenu.SetActive(true);
        backToMainMenuButton.SetActive(true);

        keybindsPanel.SetActive(false);
        backFromKeybindsPanelButton.SetActive(false);
    }

    public void OnAudioBackButtonPressed()
    {
        allOptionsMenu.SetActive(true);
        backToMainMenuButton.SetActive(true);

        audioPanel.SetActive(false);;
        backFromAudioPanelButton.SetActive(false);
    }

    public void OnResolutionBackButtonPressed()
    {
        allOptionsMenu.SetActive(true);
        backToMainMenuButton.SetActive(true);

        resolutionPanel.SetActive(false);
        backFromResolutionPanel.SetActive(false);
    }

    //========================================================================= WIN SCREEN BUTTONS ==================================================================

    public void OnReplayKitchenLevelButtonPressed()
    {
        SceneManager.LoadScene("Level_Kitchen");
    }

    public void OnNextLevelButtonPressed()
    {
        //load next level
    }


    //========================================================================= BUTTON AUDIO ==================================================================

    public void PlayButtonAudio()
    {
        MusicManager.musicManagerInstance.PlayMenuSFX();
    }

}
