using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class ScoreManager : MonoBehaviour
{
    private TMP_Text scoreLabel;
    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        scoreLabel = GameObject.Find("ScoreLabel").GetComponent<TMP_Text>();
        UpdateScore();
    }

    public int GetScore()
    {
        return score;
    }

    public void AddPoints(int points)
    {
        score += points;
        UpdateScore();
    }

    public void SetScore(int newScore)
    {
        score = newScore;
        UpdateScore();
    }

    public void UpdateScore()
    {
        scoreLabel.text = $"Score: {score}";
    }

}
