using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    public GameObject gameManagerPrefab;
    public Text cardTrainingScoreText;
    public Text cardScoreText;

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
        cardScoreText.text = "High Score: " + gameManager.cardsTrainingHighScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
