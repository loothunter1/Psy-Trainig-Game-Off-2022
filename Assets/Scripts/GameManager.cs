using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager;

    public int cardsTrainingHighScore;
    public int cardsTestHighScore;
    // Start is called before the first frame update
    void Start()
    {
        if (gameManager == null)
        {
            gameManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (gameManager != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static GameManager GetGameManager()
    {
        if (gameManager)
        {
            return gameManager;
        }
        else
        {
            gameManager = GameObject.FindObjectOfType<GameManager>();
            return gameManager;
        }
    }

    public void LoadGame(int gameIndex)
    {
        SceneManager.LoadScene(gameIndex);
    }
}
