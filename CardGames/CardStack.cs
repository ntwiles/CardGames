using System;
using System.Collections.Generic;
using System.Text;

namespace CardGames
{
    public class CardStack
    {
        public List<Card> cards { get; private set; } = new List<Card>();

        public Card this[int index]
        {
            get { return cards[index]; }
        }

        public int Count
        {
            get { return cards.Count; }
        }

        public bool IsFaceUp { get; private set; }

        public CardStack(bool isFaceUp)
        {
            IsFaceUp = isFaceUp;
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
