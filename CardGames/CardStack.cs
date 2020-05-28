using System;
using System.Collections.Generic;
using System.Text;

namespace CardGames
{
    public class CardStack
    {
        public List<Card> cards { get; private set; } = new List<Card>();

        public bool AllFacingSame
        {
            get
            {
                if (cards.Count < 1) return true;

                bool firstFacing = cards[0].IsFaceUp;

                foreach (Card card in cards)
                {
                    if (card.IsFaceUp != firstFacing) return false;
                }

                return true;
            }
        }

        public Card this[int index]
        {
            get { return cards[index]; }
        }

        public int Count
        {
            get { return cards.Count; }
        }

        // Draws a card using LIFO.
        public Card DrawCard()
        {
            Card drawnCard = cards[cards.Count - 1];
            cards.Remove(drawnCard);
            return drawnCard;
        }

        public void Add(Card card)
        {
            cards.Add(card);
        }

        public void RemoveAt(int index)
        {
            cards.RemoveAt(index);
        }

        public Card GetCard(int index)
        {
            return cards[index];
        }
    }
}
