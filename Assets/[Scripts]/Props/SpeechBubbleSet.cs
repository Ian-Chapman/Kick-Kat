using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBubbleSet", menuName = "New Speech Bubble Set")]
public class SpeechBubbleSet : ScriptableObject
{
    public List<Material> speechBubbleMaterials = new List<Material>();

    public Material GetRandomBubble()
    {
        if (speechBubbleMaterials != null && speechBubbleMaterials.Count > 0)
        {
            int index = Random.Range(0, speechBubbleMaterials.Count);
            return speechBubbleMaterials[index];
        }

        return null;
    }
}
