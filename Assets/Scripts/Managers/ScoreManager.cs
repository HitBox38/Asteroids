using System;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    private int currentScore = 0;

    public float CurrentScore
    {
        get { return currentScore; }
    }

    public static event Action<float> OnScoreChanged;

    private void OnEnable()
    {
        Asteroid.OnHit += AddScore;
    }

    private void OnDisable()
    {
        Asteroid.OnHit -= AddScore;
    }

    void Start()
    {
        PlayerPrefs.SetInt("Score", currentScore);
    }

    void Update()
    {
        scoreText.text = currentScore.ToString();
    }

    public void AddScore(int points)
    {
        currentScore += points;
        PlayerPrefs.SetInt("Score", currentScore);
        OnScoreChanged?.Invoke(currentScore);
    }
}
