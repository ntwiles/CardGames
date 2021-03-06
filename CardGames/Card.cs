﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CardGames
{
    public class Card
    {
        public int Value { get; private set; }
        public CardSuit Suit { get; private set; }

        public bool IsRed
        {
            get { return Suit == CardSuit.Diamonds || Suit == CardSuit.Hearts; }
        }

        public Card(int value, CardSuit suit)
        {
            this.Value = value;
            this.Suit = suit;
        }

        public enum CardSuit
        {
            Spades,
            Hearts,
            Clubs,
            Diamonds
        }
    }
}
