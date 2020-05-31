using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace CardGames
{
    public class DeckBuilder
    {
        public CardStack BuildDeck(bool shuffle, bool buildFaceUp = false)
        {
            CardStack deck = new CardStack(buildFaceUp);

            for (int iFace = 0; iFace < 4; iFace++)
            {
                Card.CardSuit suit = (Card.CardSuit)iFace;

                for (int iValue = 1; iValue <= 13; iValue++)
                {
                    Card card = new Card(iValue, suit);
                    deck.Add(card);
                }
            }

            if (shuffle)
            {
                deck = Shuffle(deck);
            }

            return deck;
        }

        public CardStack Shuffle(CardStack deck)
        {
            CardStack shuffled = new CardStack(deck.IsFaceUp);
            int initialDeckCount = deck.Count;

            while (shuffled.Count < initialDeckCount)
            {
                Random rand = new Random();
                int cardPosition = rand.Next(deck.Count);
                Card randomCard = deck[cardPosition];
                deck.RemoveAt(cardPosition);

                shuffled.Add(randomCard);
            }

            return shuffled;
        }
    }
}
