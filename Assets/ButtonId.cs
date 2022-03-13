using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonId : MonoBehaviour
{
    public GameObject player;
    public Item item;
    void Start()
    {
        player = GameObject.Find("CatGirlChar");
    }

    public void onClicked()
    {
        player.GetComponent<ThirdPersonMovement>().UseItem(item);
        Debug.Log("clicked");
    }
}
