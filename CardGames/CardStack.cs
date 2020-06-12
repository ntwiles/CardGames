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

        public void AddStack(CardStack range)
        {
            for (int i = range.Count - 1; i >= 0; i--)
            {
                cards.Add(range[i]);
            }
        }

        public void Insert(int index, Card card)
        {
            cards.Insert(index, card);
        }

        public void RemoveAt(int index)
        {
            cards.RemoveAt(index);
        }

        public void RemoveRange(int index, int count)
        {
            cards.RemoveRange(index, count);
        }

        public Card GetCard(int index)
        {
            return cards[index];
        }

        public CardStack Substack(int start, int length = -1)
        {
            CardStack subStack = new CardStack(this.IsFaceUp);

            int endPoint = length == -1 ? cards.Count : start + length;

            for (int i = start; i < endPoint; i++)
            {
                subStack.Insert(0, cards[i]);
            }

            return subStack;
        }

        public CardStack GetTopNCards(int n)
        {
            return this.Substack(cards.Count - n);
        }
    }
}
