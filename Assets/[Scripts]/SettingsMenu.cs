using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public TMP_Dropdown punchDD;
    public TMP_Dropdown jumpDD;
    public TMP_Dropdown kickDD;


    public AudioMixer audioMixer;

    private void OnEnable()
    {
        if (kickDD != null)
            kickDD.onValueChanged.AddListener(SetKickKeyBind);

        if (punchDD != null)
            punchDD.onValueChanged.AddListener(SetPunchKeyBind);

        if (jumpDD != null)
            jumpDD.onValueChanged.AddListener(SetJumpKeyBind);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MixerVolume", volume);
    }

    public void SetJumpKeyBind(int index)
    {
        if (PlayerKeybinds.Keybinds.TryGetValue(index, out string str))
            PlayerKeybinds.PlayerJump = str;
    }

    public void SetKickKeyBind(int index)
    {
        if (PlayerKeybinds.Keybinds.TryGetValue(index, out string str))
            PlayerKeybinds.PlayerKick = str;
    }
    
    public void SetPunchKeyBind(int index)
    {
        if (PlayerKeybinds.Keybinds.TryGetValue(index, out string str))
            PlayerKeybinds.PlayerPunch = str;
    }

}
