using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public Text highScore;
    public int scoreCount;
    public int highScoreCount;

    // Start is called before the first frame update
    void Start()
    {
        // load high score
        highScoreCount = PlayerPrefs.GetInt("highscore", highScoreCount);
    }

    // Update is called once per frame
    void Update()
    {
        // update high score if current score passes it
        if (scoreCount > highScoreCount)
        {
            highScoreCount = scoreCount;
            PlayerPrefs.SetInt("highscore", highScoreCount);
            PlayerPrefs.Save();
        }

        scoreText.text = scoreCount.ToString();
        highScore.text = "High Score: " + highScoreCount;
    }

    public void AddScore(int pointsToAdd)
    {
        scoreCount += pointsToAdd;
    }
}
