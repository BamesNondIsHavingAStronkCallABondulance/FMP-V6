using UnityEngine;
using UnityEngine.EventSystems;
public class PlayCards : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    public LineRenderer lineRenderer;

    public GameObject[] cards;

    public EventSystem eventSystem;

    private Vector2 originalPosition;
    private Vector2 originalScale;

    private Vector2 startMousePos;
    private Vector2 mousePos;

    public bool enemyIsSelected;
    public bool cardIsPlayed;

    bool canCardBePlayed = true;

    public bool card1Selected, card2Selected, card3Selected;

    private float cardDelay = 1f;



    //Cannon Unselect enemy when playing a card, try to fix

    //Need to put this script on card manager with access to all 3 cards to prevent multiple instances running


    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = transform.position;
        originalScale = transform.localScale;

        //transform.localScale += new Vector3(0.2f, 0.2f, 0);

        startMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector2 worldPoint2d = new Vector2(worldPoint.x, worldPoint.y);

    }

    public void OnDrag(PointerEventData eventData)
    {

        Transform cardSelected=null;

        //reset scale to normal
        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].transform.localScale = originalScale;
        }

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Input.mousePosition;

        /* if (mousePos.x >= -2.7f)
         {
             lineRenderer.SetPosition(0, new Vector3(-2.7f, -2.2f));
         }
        */

        // Try to get line to only start if above the card

        
        if (mousePos.y > 0 && (mousePos.x < 1 && mousePos.x > -1)) //When mouse is near enemy
        {
            if (-3.7f < startMousePos.x && startMousePos.x < -1.7f) //What card is selected
            {
                lineRenderer.SetPosition(0, new Vector3(-2.7f, -2.2f));
                lineRenderer.SetPosition(1, new Vector3(0, 1));

                card1Selected = true;
                cardSelected = cards[0].transform;
            }

            if (-1f < startMousePos.x && startMousePos.x < 1f)
            {
                lineRenderer.SetPosition(0, new Vector3(0f, -2.2f));
                lineRenderer.SetPosition(1, new Vector3(0, 1));

                card2Selected = true;
                cardSelected = cards[1].transform;

            }

            if (1.7f < startMousePos.x && startMousePos.x < 3.7f)
            {
                lineRenderer.SetPosition(0, new Vector3(2.7f, -2.2f));
                lineRenderer.SetPosition(1, new Vector3(0, 1));

                card3Selected = true;
                cardSelected = cards[2].transform;

            }



          //  enemyIsSelected = true;
        }
        else
        {

            if (-3.7f < startMousePos.x && startMousePos.x < -1.7f) //when mouse is not near enemy
            {
                lineRenderer.SetPosition(0, new Vector3(-2.7f, -2.2f));
                lineRenderer.SetPosition(1, new Vector3(mousePos.x, mousePos.y));
                cardSelected = cards[0].transform;
            }

            if (-1f < startMousePos.x && startMousePos.x < 1f)
            {
                lineRenderer.SetPosition(0, new Vector3(0, -2.2f));
                lineRenderer.SetPosition(1, new Vector3(mousePos.x, mousePos.y));
                cardSelected = cards[1].transform;
            }
            if (1.7f < startMousePos.x && startMousePos.x < 3.7f)
            {
                lineRenderer.SetPosition(0, new Vector3(2.7f, -2.2f));
                lineRenderer.SetPosition(1, new Vector3(mousePos.x, mousePos.y));
                cardSelected = cards[2].transform;
            }


            enemyIsSelected = false;
        }

        //set scale of currently selected card
        if( cardSelected != null )
        {
            cardSelected.transform.localScale = Vector3.one * 1.2f;
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (mousePos.y > 0 && (mousePos.x < 1 && mousePos.x > -1))
        {
            if (-3.7f < startMousePos.x && startMousePos.x < -1.7f)
            {
                enemyIsSelected = true;
            }

            if (-1f < startMousePos.x && startMousePos.x < 1f)
            {
                enemyIsSelected = true;
            }

            if (1.7f < startMousePos.x && startMousePos.x < 3.7f)
            {
                enemyIsSelected = true;
            }
        }

        transform.position = new Vector2(originalPosition.x, originalPosition.y);
        transform.localScale = originalScale;

        lineRenderer.SetPosition(0, new Vector3(0, 0));
        lineRenderer.SetPosition(1, new Vector3(0, 0));
    }

    private void Start()
    {

        cardIsPlayed = false;

        //cards = GameObject.FindGameObjectsWithTag("card");
    }

    private void Update()
    {
        DelayNewCard();
    }

    void DelayNewCard()
    {
        if (enemyIsSelected)
        {
            if (cardDelay <= 0)
            {
                cardDelay = 1f;
                enemyIsSelected = false;
                eventSystem.enabled = true;
                cardIsPlayed = false;

                enemyIsSelected = false;

                card1Selected = false;
                card2Selected = false;
                card3Selected = false;

                canCardBePlayed = true;
            }
            else
            {
                cardDelay -= Time.deltaTime;
                eventSystem.enabled = false;

                if(canCardBePlayed == true)
                {
                    cardIsPlayed = true;
                    canCardBePlayed = false;
                }
                else
                {
                    cardIsPlayed = false;
                }

            }
        }
    }
}
