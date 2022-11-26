using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPick : MonoBehaviour
{
    public SpriteRenderer cardCover;
    public SpriteRenderer cardDecorativeFrame;
    public SpriteRenderer cardDecorativeShadow;
    public SpriteRenderer cardDecorativeElement;
    public SpriteRenderer cardShirt;
    public GameObject cardMark;
    CardController cardController;

    float cardShirtOpacity;
    float fullXRayTime;
    bool focus;
    bool cardSelected;

    public int cardIndex;
    public string cardName = "0";

    bool selected;

    public bool Selected {
        get
        {
            return selected;
        }
        set
        {
            selected = value;
            cardMark.SetActive(selected);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        GameObject findObject;
        findObject = GameObject.FindGameObjectWithTag("GameController");
        if (findObject)
        {
            cardController = findObject.GetComponent<CardController>();
            if(cardController)
                fullXRayTime = cardController.fullXRayTime;
        }

        if (cardShirt)
            cardShirtOpacity = cardShirt.color.a;

        Selected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (cardShirt)
        {
            if (focus && (Input.GetMouseButton(0)||Input.GetMouseButton(1)) )
            {
                cardShirtOpacity -= Time.deltaTime / fullXRayTime;
                if (cardShirtOpacity < 0)
                {
                    cardShirtOpacity = 0;
                }
                Color rendererColor = cardShirt.color;
                rendererColor.a = cardShirtOpacity;
                //Debug.Log("Opacity: " + cardShirtOpacity);
                cardShirt.color = rendererColor;
            }
            else if (cardShirtOpacity < 1&& (!focus))
            {
                cardShirtOpacity += Time.deltaTime / fullXRayTime;
                if (cardShirtOpacity > 1)
                {
                    cardShirtOpacity = 1;
                }
                Color rendererColor = cardShirt.color;
                rendererColor.a = cardShirtOpacity;
                //Debug.Log("Opacity: " + cardShirtOpacity);
                cardShirt.color = rendererColor;
            }
        }
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButton(0))
        {
            Selected = !Selected;
            bool cardMatch=cardController.CardPicked(gameObject);
            if (cardMatch)
            {
                Destroy(gameObject);
            }
        }
        Debug.Log("Card #"+cardIndex+" Picked: "+cardName);
    }

    private void OnMouseEnter()
    {
        focus = true;
    }

    private void OnMouseExit()
    {
        focus = false;
    }
}
