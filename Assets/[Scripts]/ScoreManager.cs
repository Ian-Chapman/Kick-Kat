using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; set; }
    public int score;
    public TextMeshProUGUI scoreTxt;
    private void Awake()
    {
        score = 0;
        Instance = this;
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        scoreTxt.text = "Score: " + score;
    }

    public void IncreaseScore(int amount)
    {
        score += amount;    
    }

    public void ClearScore()
    {
        score = 0;
    }
}
