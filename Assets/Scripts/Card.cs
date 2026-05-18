using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;

namespace HarryGame
{
    public enum CardType
    {
        Attack,
        Skill,
        Unplayable,
    }




    // [CreateAssetMenu(fileName = "New Card", menuName = "Card")]
    public class Card //: ScriptableObject
    {
        public string cardName;

        public int damage;

        public int block;

        public bool wasChosen;

        public CardType cardType; // List<CardType> cardType;

        public string cardTypeText;

        public Sprite cardSprite;

        public string cardDescription;

        public Card()
        {
            wasChosen = false;
        }
          




    }





}
