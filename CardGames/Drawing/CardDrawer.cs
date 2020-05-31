using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace CardGames.Drawing
{
    public class CardDrawer
    {
        private ConsoleColor highlightColor;
        private ConsoleColor heartDiamondColor;
        private ConsoleColor mainColor;

        public CardDrawer()
        {
            highlightColor = ConsoleColor.Yellow;
            heartDiamondColor = ConsoleColor.Red;
            mainColor = ConsoleColor.White;
        }

        public void DrawCard(int posX, int posY, Card card = null)
        {
            DrawCard(posX, posY, card, false);
        }

        public void DrawCard(int posX, int posY, Card card, bool highlight = false)
        {
            if (card == null || !card.IsFaceUp)
            {
                DrawFaceDownCard(posX, posY);
            }
            else
            {
                DrawFaceUpCard(posX, posY, highlight, card);
            }
        }

        private void DrawFaceDownCard(int posX, int posY)
        {
            Console.SetCursorPosition(posX, posY);
            Console.Write($".--------.");

            Console.SetCursorPosition(posX, posY +1);
            Console.Write($":xxxxxxxx:");

            Console.SetCursorPosition(posX, posY +2);
            Console.Write($":xxxxxxxx:");

            Console.SetCursorPosition(posX, posY + 3);
            Console.Write($":xxxxxxxx:");

            Console.SetCursorPosition(posX, posY + 4);
            Console.Write($":xxxxxxxx:");

            Console.SetCursorPosition(posX, posY + 5);
            Console.Write($":xxxxxxxx:");

            Console.SetCursorPosition(posX, posY + 6);
            Console.Write($"'--------'");
        }

        private void DrawFaceUpCard(int posX, int posY, bool highlight, Card card)
        {
            if (highlight) Console.ForegroundColor = highlightColor;

            // Draw card body.
            Console.SetCursorPosition(posX, posY);
            Console.Write($".--------.");

            for (int i = 1; i < 6; i++)
            {
                Console.SetCursorPosition(posX, posY + i);
                Console.Write($":        :");
            }

            Console.SetCursorPosition(posX, posY + 6);
            Console.Write($"'--------'");

            // Reset color.
            Console.ForegroundColor = mainColor;

            // Draw card numbers and suits.
            if (card.IsRed) Console.ForegroundColor = heartDiamondColor;

            // Top left number
            Console.SetCursorPosition(posX + 2, posY +1);
            char value = GetCardValue(card.Value);
            Console.Write(value);

            // Top left suit symbol
            Console.SetCursorPosition(posX + 2, posY + 2);
            char suitSymbol = GetSuitSymbol(card.Suit);
            Console.Write(suitSymbol);

            // Reset foreground color.
            Console.ForegroundColor = mainColor;
        }

        public void DrawCardSliverTop(int posX, int posY)
        {
            Console.SetCursorPosition(posX, posY);
            Console.Write($".--------.");
        }

        public void DrawFaceUpCardTop(int posX, int posY, Card card, bool highlight = false)
        {
            if (highlight) Console.ForegroundColor = highlightColor;

            Console.SetCursorPosition(posX, posY);
            Console.Write($".--------.");
            Console.SetCursorPosition(posX, posY + 1);
            Console.Write($":        :");

            // Draw card numbers and suits.
            Console.SetCursorPosition(posX + 2, posY + 1);
            char value = GetCardValue(card.Value);
            char suitSymbol = GetSuitSymbol(card.Suit);

            if (card.IsRed) Console.ForegroundColor = heartDiamondColor;
            else Console.ForegroundColor = mainColor;
            Console.Write($"{value} {suitSymbol}");

            // Reset foreground color.
            Console.ForegroundColor = mainColor;
        }

        public void DrawFaceUpCardBottom(int posX, int posY, Card card)
        {
            Console.SetCursorPosition(posX, posY);
            char value = GetCardValue(card.Value);
            char suitSymbol = GetSuitSymbol(card.Suit);
            Console.Write($": ");

            // Draw card numbers and suits.
            if (card.IsRed) Console.ForegroundColor = heartDiamondColor;
            Console.Write($"   {value} {suitSymbol} ");

            // Reset foreground color.
            Console.ForegroundColor = mainColor; 
            Console.Write($":");
            Console.SetCursorPosition(posX, posY + 1);
            Console.Write($"'--------'");
        }

        public void DrawFaceDownCardBottom(int posX, int posY)
        {
            Console.SetCursorPosition(posX, posY);
            Console.Write($":xxxxxxxx:");
            Console.SetCursorPosition(posX, posY + 1);
            Console.Write($"'--------'");
        }

        public char GetSuitSymbol(Card.CardSuit suit)
        {
            switch (suit)
            {
                case Card.CardSuit.Clubs: return '♣';
                case Card.CardSuit.Spades: return '♠';
                case Card.CardSuit.Hearts: return '♥';
                case Card.CardSuit.Diamonds: return '♦';
                default: return '?';
            }
        }

        public char GetCardValue(int value)
        {
            switch (value)
            {
                case 1: return 'A';
                case 11: return 'J';
                case 12: return 'Q';
                case 13: return 'K';
                default: return value.ToString().ToCharArray()[0];
            }
        }
    }
}
