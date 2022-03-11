using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControlsManager : MonoBehaviour
{
    public GameObject rightHandedControls;
    public GameObject leftHandedControls;

    private void OnEnable()
    {
        rightHandedControls.SetActive(PlayerKeybinds.PlayerRightHanded);
        leftHandedControls.SetActive(!PlayerKeybinds.PlayerRightHanded);
    }
}
