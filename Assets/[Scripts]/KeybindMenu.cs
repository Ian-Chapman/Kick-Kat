using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeybindMenu : MonoBehaviour
{
    public GameObject keyboardBindings;
    public GameObject phoneBindings;

    private void OnEnable()
    {
        if (!(Application.platform == RuntimePlatform.Android ||
            Application.platform == RuntimePlatform.IPhonePlayer))
        {
            phoneBindings.SetActive(true);
            keyboardBindings.SetActive(false);
        }
        else
        {
            phoneBindings.SetActive(false);
            keyboardBindings.SetActive(true);
        }

    }
}
