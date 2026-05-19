using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using HarryGame;

public class HandManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public Card cardData1, cardData2, cardData3;

    int drawCard1 = 0, drawCard2 = 0, drawCard3 = 0;

    [Header("Card1")]

    public TMP_Text card1Name;
    public TMP_Text card1Type;
    public TMP_Text card1Description;
    public Image card1Image;

    [Header("Card2")]

    public TMP_Text card2Name;
    public TMP_Text card2Type;
    public TMP_Text card2Description;
    public Image card2Image;

    [Header("Card3")]

    public TMP_Text card3Name;
    public TMP_Text card3Type;
    public TMP_Text card3Description;
    public Image card3Image;


    public Color[] typeColours =
{
        Color.red, //attack
        Color.blue, //skill
        Color.gray, //power
    };

    public Card[] deckOfCards = new Card[10];

    //** card **
    // image
    // card name text 
    // card type text 
    // card description text



    void Start()
    {
        //initialise the card array
        deckOfCards[0] = new Card();
        deckOfCards[1] = new Card();
        deckOfCards[2] = new Card();
        deckOfCards[3] = new Card();
        deckOfCards[4] = new Card();
        deckOfCards[5] = new Card();

        StarterCards(); //if floor == 1
       
    }

    public void AddCardToHand()
    {
        /*GameObject newCard = Instantiate(cardPrefab, handTransform.position, Quaternion.identity, handTransform);
        preparedCards.Add(newCard);
        UpdateHandVisuals();
        */
    }

    private void StarterCards()
    {
        deckOfCards[0].cardName = "Punch";
        deckOfCards[0].cardDescription = "Deals 4 damage";
        deckOfCards[0].cardTypeText = "Attack";
        deckOfCards[0].cardType = CardType.Attack;
        //deckOfCards[0].cardSprite = listOfCardsScript.punch.cardSprite;

        deckOfCards[1].cardName = "Punch";
        deckOfCards[1].cardDescription = "Deals 4 damage";
        deckOfCards[1].cardTypeText = "Attack";
        deckOfCards[1].cardType = CardType.Attack;
        //deckOfCards[1].cardSprite = listOfCardsScript.punch.cardSprite;

        deckOfCards[2].cardName = "Kick";
        deckOfCards[2].cardDescription = "Deals 6 damage";
        deckOfCards[2].cardTypeText = "Attack";
        deckOfCards[2].cardType = CardType.Attack;
        //deckOfCards[2].cardSprite = listOfCardsScript.punch.cardSprite;

        deckOfCards[3].cardName = "Dodge";
        deckOfCards[3].cardDescription = "Gain 4 Block";
        deckOfCards[3].cardTypeText = "Skill";
        deckOfCards[3].cardType = CardType.Skill;
        //deckOfCards[3].cardSprite = listOfCardsScript.dodge.cardSprite;

        deckOfCards[4].cardName = "Dodge";
        deckOfCards[4].cardDescription = "Gain 4 Block";
        deckOfCards[4].cardTypeText = "Skill";
        deckOfCards[4].cardType = CardType.Skill;
        //deckOfCards[4].cardSprite = listOfCardsScript.dodge.cardSprite;

        deckOfCards[5].cardName = "Dodge";
        deckOfCards[5].cardDescription = "Gain 4 Block";
        deckOfCards[5].cardTypeText = "Skill";
        deckOfCards[5].cardType = CardType.Skill;
        //deckOfCards[5].cardSprite = listOfCardsScript.dodge.cardSprite;
    }

    
    public void UpdateCard1()
    {
        card1Name.text = deckOfCards[drawCard1].cardName;
        card1Description.text = deckOfCards[drawCard1].cardDescription;
        card1Type.text = deckOfCards[drawCard1].cardTypeText; //String
        card1Image.color = typeColours[(int)deckOfCards[drawCard1].cardType]; //Colour
    }

    public void UpdateCard2()
    {
        card2Name.text = deckOfCards[drawCard2].cardName;
        card2Description.text = deckOfCards[drawCard2].cardDescription;
        card2Type.text = deckOfCards[drawCard2].cardTypeText;
        card2Image.color = typeColours[(int)deckOfCards[drawCard2].cardType];
    }

    public void UpdateCard3()
    {
        card3Name.text = deckOfCards[drawCard3].cardName;
        card3Description.text = deckOfCards[drawCard3].cardDescription;
        card3Type.text = deckOfCards[drawCard3].cardTypeText;
        card3Image.color = typeColours[(int)deckOfCards[drawCard3].cardType];
    }


    #region DeckLogic

    public void RedrawCards()
    {
        int resetCount = 0;
        bool validDraw = false;

        for (int i = 0; i < 6; i++)
        {
            if (deckOfCards[i].wasChosen == true)
            {
                resetCount++;
            }
        }

        if (resetCount > 4)
        {
            for (int i = 0; i < 6; i++)
            {
                deckOfCards[i].wasChosen = false;
                resetCount = 0;
            }
        }

        while (validDraw != true)
        {
            drawCard1 = Random.Range(0, 6);
            drawCard2 = Random.Range(0, 6);
            drawCard3 = Random.Range(0, 6);

            if (deckOfCards[drawCard1].wasChosen != true && deckOfCards[drawCard2].wasChosen != true && deckOfCards[drawCard3].wasChosen != true && (drawCard1 != drawCard2 || drawCard1 != drawCard3 || drawCard2 != drawCard3))
            {
                validDraw = true;

                deckOfCards[drawCard1].wasChosen = true;
                deckOfCards[drawCard2].wasChosen = true;
                deckOfCards[drawCard3].wasChosen = true;
            }
        }
        resetCount = 0;
    }


    #endregion

}
