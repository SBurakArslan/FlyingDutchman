using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public Text highScoreText;
    private int score;
    private int highScore;
    public GameObject pause;
    public GameObject gameOver;


    void Start()
    {
        score = 0;
        LoadHighScore();
        UpdateScoreText();
        
    }

    void Update() {
        if (!pause.activeSelf && !gameOver.activeSelf ) {
            score += 1;
            UpdateScoreText();
        } else {
            
        }
    }

    void UpdateScoreText() {

        scoreText.text = "Score: " + score;
        highScoreText.text = "High Score: " + highScore;

    }

    public void CheckHighScore()
    {
        if (score > highScore) {
            highScore = score;
            SaveHighScore();
        }
        }
    void LoadHighScore() {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }
    void SaveHighScore() {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }
}
