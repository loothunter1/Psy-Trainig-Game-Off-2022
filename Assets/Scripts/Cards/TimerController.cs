using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public Text timerText;
    public Text cardText;
    public float timeLimit;
    public CardController cardController;

    float timeRemains;
    float startTime;

    public GameObject gameManagerPrefab;
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
        timeRemains = timeLimit;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
        UpdateCardStatus();
    }

    void UpdateTimer()
    {
        timeRemains = timeLimit - Time.time + startTime;
        if(timerText)
        timerText.text = "Time left: " + timeRemains.ToString("0.0");
        if (timeRemains <= 0)
        {
            if (cardController.difficulty == 0)
            {
                if(cardController.GameScore()> gameManager.cardsTrainingHighScore)
                gameManager.cardsTrainingHighScore = cardController.GameScore();
            }
            else
            {
                if (cardController.GameScore() > gameManager.cardsHighScore)
                    gameManager.cardsHighScore = cardController.GameScore();
            }

            gameManager.LoadGame(0);
        }
    }

    void UpdateCardStatus()
    {
        if (cardText && cardController)
            cardText.text = "Cards left: " + cardController.cardsLeft.ToString();
    }
}
