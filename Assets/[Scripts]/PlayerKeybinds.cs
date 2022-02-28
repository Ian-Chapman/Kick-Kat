using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeybinds : MonoBehaviour
{
    public static Dictionary<int, string> Keybinds;

    public static string PlayerJump = "space";
    public static string PlayerPunch = "mouse 0";
    public static string PlayerKick = "mouse 1";

    private void OnEnable()
    {
        Keybinds = new Dictionary<int, string>();

        Keybinds.Add(0, "space");
        Keybinds.Add(1, "mouse 0");
        Keybinds.Add(2, "mouse 1");
    }
}
