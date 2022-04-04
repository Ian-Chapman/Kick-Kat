using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Item
{
    public enum ItemType
    {
        PowerUp,
        Points,
        tempPup
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Points: return ItemAssets.Instance.PowerUp1;
            case ItemType.PowerUp: return ItemAssets.Instance.PowerUp2;
            case ItemType.tempPup: return ItemAssets.Instance.PowerUp3;
        }
    }

    public String GetName()
    {
        switch (itemType)
        {
            default:
            case ItemType.Points: return "Points";
            case ItemType.PowerUp: return "PowerUp";
            case ItemType.tempPup: return "TempUp";
        }
    }

    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Points:
            case ItemType.PowerUp:
            case ItemType.tempPup:
                return true;
        }
    }
}