using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallTimerController : MonoBehaviour
{
    public Text timerText;
    public Text scoreText;
    public float timeLimit;
    public int difficulty;
    public BoxCollider boxCollider;

    float timeRemains;
    float startTime;

    int score;

    public GameObject gameManagerPrefab;
    GameManager gameManager;

    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            UpdateScore();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.GetGameManager();
        if (gameManager == null)
        {
            Instantiate(gameManagerPrefab);
            gameManager = GameManager.GetGameManager();
        }
        timeRemains = timeLimit;
        startTime = Time.time;
    }

    private void Update()
    {
        UpdateTimer();
    }

    // Update is called once per frame
    void UpdateScore()
    {
        if (scoreText)
        {
            scoreText.text = "Score: " + score;
        }
        
    }

    void UpdateTimer()
    {
        timeRemains = timeLimit - Time.time + startTime;
        if (timerText)
            timerText.text = "Time left: " + timeRemains.ToString("0.0");
        if (timeRemains <= 0)
        {
            Debug.Log("Difficulty: " + difficulty);
            if (difficulty == 0)
            {
                Debug.Log("Training score: " + score);
                if (score > gameManager.ballTrainingHighScore)
                    gameManager.ballTrainingHighScore = score;
            }
            else
            {
                Debug.Log("Score: " + score);
                if (score > gameManager.ballHighScore)
                    gameManager.ballHighScore = score;
            }

            gameManager.LoadGame(0);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(score>gameManager.ballHighScore)
            gameManager.ballHighScore = score;

        gameManager.LoadGame(0);
    }
}
