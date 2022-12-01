using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlacer : MonoBehaviour
{
    public GameObject gridTopLeft;
    public GameObject gridBottomRight;

    int gridX;
    int gridY;
    int cardsShuffled;
    float stepX;
    float stepY;

    int[] shuffledX;
    int[] shuffledY;
    public int[] shuffledIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(int gX, int gY, int cS)
    {
        gridX = gX;
        gridY = gY;
        stepX = (gridBottomRight.transform.position.x - gridTopLeft.transform.position.x) / gridX;
        stepY = (gridBottomRight.transform.position.y - gridTopLeft.transform.position.y) / gridY;
        cardsShuffled = cS;
        ShuffleCoords();
    }

    void ShuffleCoords()
    {
        shuffledX = new int[gridX * gridY];
        shuffledY = new int[gridX * gridY];
        shuffledIndex = new int[gridX * gridY];
        ShuffleCards();
        for (int i = 0; i < gridX; i++)
        {
            for (int j = 0; j < gridY; j++)
            {
                int cardIndex = shuffledIndex[i + gridX * j];
                shuffledX[cardIndex] = i;
                shuffledY[cardIndex] = j;
            }

        }
    }

    void ShuffleCards()
    {
        for (int i = 0; i < shuffledIndex.Length; i++)
        {
            shuffledIndex[i] = i;
        }
        if (cardsShuffled > shuffledIndex.Length)
        {
            cardsShuffled = shuffledIndex.Length;
        }
        int cards = cardsShuffled;
        int[] indexLeft= new int[cards];
        for (int i = 0; i < cards; i++)
        {
            indexLeft[i] = -1;
            shuffledIndex[i] = -1;
        }
        for (int i = 0; i < cards; i++)
        {
            int n = Random.Range(0, cards - i);
            int index = -1;
            for (int j = 0; j <= n; j++)
            {
                index++;
                while (indexLeft[index]>=0)
                {
                    index++;
                }
            }
            shuffledIndex[i] = index;
            indexLeft[index] = i;
        }
    }

    public Vector3 CardPlace(int x, int y)
    {
        int cardIndex = x + gridX * y;
        x = shuffledX[cardIndex];
        y = shuffledY[cardIndex];
        Vector3 cardPlace = new Vector3(gridTopLeft.transform.position.x + stepX * x, gridTopLeft.transform.position.y + stepY * y, gridTopLeft.transform.position.z);
        return cardPlace;
    }
}
