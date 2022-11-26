using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    public Material cardShirtMaterial;

    public float cardShirtOpacity = 1f;
    public float fullXRayTime = 2f;

    public GameObject cardPrefab;
    public CardPlacer cardPlacer;

    public int gridX=5;
    public int gridY=4;
    public int cardsTotal = 10;
    public int cardsLeft;

    public int difficulty;

    //int lastPickedCardIndex=-1;
    string lastPickedCardName = "";
    CardPick lastPickedCard;
    int gameScore;

    CardConstructor cardConstructor;

    // Start is called before the first frame update
    void Start()
    {
        cardConstructor = GetComponent<CardConstructor>();
        if(difficulty==0)
        cardPlacer.Initialize(gridX, gridY, gridX * gridY / 2);
        else
            cardPlacer.Initialize(gridX, gridY, gridX * gridY);
    //cardsOnTable = new GameObject[cardsTotal];
    gameScore = 0;
        if (cardConstructor)
        {
            DeployCards();
        }
    }

    void DeployCards()
    {
        cardsTotal = 0;
        for (int i = 0; i < gridX; i++)
            for (int j = 0; j < gridY/2; j++)
            {
                GameObject newCard = Instantiate(cardPrefab);
                cardConstructor.ConstructCard(newCard);
                Vector3 cardPosition = cardPlacer.CardPlace(i, j);
                newCard.transform.position = cardPosition;
                newCard.GetComponent<CardPick>().cardIndex = i;
                //cardsOnTable[newCard.GetComponent<CardPick>().cardIndex] = newCard;
                GameObject newCardDouble = Instantiate(newCard);
                newCardDouble.GetComponent<CardPick>().cardIndex = cardsTotal - 1 - i;
                if(difficulty==0)
                newCardDouble.GetComponent<CardPick>().cardShirt.gameObject.SetActive(false);

                cardPosition = cardPlacer.CardPlace(gridX - 1 - i, gridY - 1-j);
                newCardDouble.transform.position = cardPosition;
                cardsTotal += 2;
                //cardsOnTable[newCardDouble.GetComponent<CardPick>().cardIndex] = newCardDouble;
            }
        {
        }
        cardsLeft = cardsTotal;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetMouseButton(1))
        {
            cardShirtOpacity -= Time.deltaTime / fullXRayTime;
            if (cardShirtOpacity < 0)
            {
                cardShirtOpacity = 0;
            }
            Color materialColor = cardShirtMaterial.color;
            materialColor.a = cardShirtOpacity;
            Debug.Log("Opacity: " + cardShirtOpacity);
            cardShirtMaterial.color = materialColor;
        }*/
        
    }

    public bool CardPicked(GameObject card)
    {
        CardPick currentlyPickedCard = card.GetComponent<CardPick>();
        if (currentlyPickedCard != lastPickedCard)
        {
            if (currentlyPickedCard.cardName == lastPickedCardName)
            {
                Debug.Log("Cards match: " + lastPickedCardName + " last card: " + lastPickedCard);
                Destroy(lastPickedCard.gameObject);
                CountCards(-2);
                lastPickedCard = null;
                lastPickedCardName = "";
                return true;
            }
            else
            {
                if (lastPickedCard)
                    lastPickedCard.Selected = false;
                lastPickedCard = currentlyPickedCard;
                lastPickedCardName = currentlyPickedCard.cardName;
                return false;
            }
        }
        else
        {
            if (lastPickedCard)
                lastPickedCard.Selected = false;
            lastPickedCard = null;
            lastPickedCardName = "";
            return false;
        }
    }

    void CountCards(int change)
    {
        cardsLeft += change;
        gameScore -= change;
        if (cardsLeft <= 0)
        {
            DeployCards();
        }
    }

    public int GameScore()
    {
        return gameScore;
    }
}
