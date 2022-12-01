using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardConstructor : MonoBehaviour
{
    public Sprite[] cardBackCover;
    public Sprite[] cardFrontCover;
    public Sprite[] cardDecorativeFrame;
    public Sprite[] cardDecorativeShadow;
    public Sprite[] cardDecorativeElement;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string ConstructCard(GameObject cardTemplate)
    {
        CardPick cardPick = cardTemplate.GetComponent<CardPick>();
        string cardName = "Card";
        // Choose random card cover
        int randomNumber = Random.Range(0, cardFrontCover.Length);
        cardPick.cardCover.sprite = cardBackCover[randomNumber];
        cardName += randomNumber.ToString();
        // Choose random decorative frame
        randomNumber = Random.Range(0, 10);
        if (randomNumber > 2)
        {
            randomNumber = Random.Range(0, cardDecorativeFrame.Length);
            cardPick.cardDecorativeFrame.sprite = cardDecorativeFrame[randomNumber];
            cardName += "F"+randomNumber.ToString();
        }
        else
        {
            cardPick.cardDecorativeFrame.gameObject.SetActive(false);
            cardName += "Fn";
        }
        // Choose random decorative shadow
        randomNumber = Random.Range(0, 10);
        if (randomNumber > 4)
        {
            randomNumber = Random.Range(0, cardDecorativeShadow.Length);
            cardPick.cardDecorativeShadow.sprite = cardDecorativeShadow[randomNumber];
            cardName += "S" + randomNumber.ToString();
        }
        else
        {
            cardPick.cardDecorativeShadow.gameObject.SetActive(false);
            cardName += "Sn";
        }
        // Choose random decorative element
        randomNumber = Random.Range(0, 10);
        if (randomNumber > 0)
        {
            randomNumber = Random.Range(0, cardDecorativeElement.Length);
            cardPick.cardDecorativeElement.sprite = cardDecorativeElement[randomNumber];
            cardName += "E" + randomNumber.ToString();
        }
        else
        {
            cardPick.cardDecorativeShadow.gameObject.SetActive(false);
            cardName += "Sn";
        }

        cardPick.cardName = cardName;
        return cardName;
    }
}
