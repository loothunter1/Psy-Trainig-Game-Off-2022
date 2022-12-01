using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    public GameObject gameManagerPrefab;
    public Text cardTrainingScoreText;
    public Text cardScoreText;
    public Text ballTrainingScoreText;
    public Text ballScoreText;

    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.GetGameManager();
        if (gameManager == null)
        {
            Instantiate(gameManagerPrefab);
            gameManager = GameManager.GetGameManager();
        }
        UpdateScore(cardTrainingScoreText, gameManager.cardsTrainingHighScore);
        UpdateScore(cardScoreText, gameManager.cardsHighScore);
        UpdateScore(ballTrainingScoreText, gameManager.ballTrainingHighScore);
        UpdateScore(ballScoreText, gameManager.ballHighScore);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGame(int gameIndex)
    {
        gameManager.LoadGame(gameIndex);
    }

    void UpdateScore(Text scoreText, int score)
    {
        if (scoreText)
        {
            scoreText.text = "High Score: " + score.ToString();
        }
    }
}
