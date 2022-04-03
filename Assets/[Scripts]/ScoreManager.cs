using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; set; }

    [SerializeField]
    private int scoreGoal;
    public static UnityEvent scoreGoalAchievedEvent = new UnityEvent();
    bool goalActivated = false;

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
        scoreTxt.text = score.ToString();
    }

    public void IncreaseScore(int amount)
    {
        score += amount;

        CheckScoreGoal();
    }

    private void CheckScoreGoal()
    {
        if (goalActivated) return;

        if (score >= scoreGoal)
        {
            scoreGoalAchievedEvent.Invoke();
            goalActivated = true;
        }
    }

    public void ClearScore()
    {
        score = 0;
    }
}
