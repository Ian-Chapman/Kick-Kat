using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager musicManagerInstance;
    public bool isKitchenLevelLoaded = false;

    public AudioSource buttonSFXsource;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (musicManagerInstance == null)
        {
            musicManagerInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMenuSFX()
    {
        buttonSFXsource.Stop();

        buttonSFXsource.Play();
    }

}
