using System;
using System.Collections.Generic;

using CardGames;
using CardGames.States;

namespace Solitaire
{
    public class SolitaireGame : CardGame
    {
        CardStack deck;
        CardStack drawStack;
        SolitaireDrawer drawer;

        //private Card currentDrawnCard;
        private List<StackColumn> columns;

        private int numCardsSelected;

        public int SelectedStackIndex { get; private set; }

        public int NumCardsSelected
        {
            get { return numCardsSelected; }
            set
            {
                numCardsSelected = Math.Max(value, 1);
            }
        }
        public List<string> StatusMessages { get; set; }

        private bool playing;

        public SolitaireGame()
        {
            StatusMessages = new List<string>();

            deck = new DeckBuilder().BuildDeck(true, false);
            drawStack = new CardStack();
            drawer = new SolitaireDrawer();

            columns = new List<StackColumn>();

            // Create 7 stacks of cards.
            for (int i = 0; i < 7; i++)
            {
                CardStack stack = new CardStack();
                StackColumn column = new StackColumn();
                column.FaceDownStack = stack;
                columns.Add(column);
            }

            // Distribute the cards across the stacks.
            for (int i = 0; i < 7; i ++)
            {
                for (int ii = i; ii < 7; ii++)
                {
                    CardStack stack = columns[ii].FaceDownStack;
                    Card drawnCard = deck.DrawCard();
                    stack.Add(drawnCard);
                }
            }

            // Transfer top cards to their own stacks.
            for (int i = 0; i < 7; i++)
            {
                StackColumn column = columns[i];
                CardStack faceDownStack = column.FaceDownStack;
                CardStack faceUpStack = new CardStack();
                Card topCard = faceDownStack.DrawCard();
                topCard.Flip();
                faceUpStack.Add(topCard);
                column.FaceUpStack = faceUpStack;
            }

            TransferCardToDrawStack();
        }

        private void TransferCardToDrawStack()
        {
            Card drawnCard = deck.DrawCard();
            drawnCard.Flip();
            drawStack.Add(drawnCard);
        }

        public void Play()
        {
            playing = true;
            SetState(new SolitaireStateSelectingStack());

            while (playing)
            {
                State.Update();
                DrawGame();
            }
        }

        public void DrawGame()
        {
            drawer.DrawSolitaireGame(this, deck, drawStack, columns, StatusMessages);
        }

        public void Quit()
        {
            playing = false;
        }

        public CardStack SelectStack(int newStack)
        {
            SelectedStackIndex = newStack;
            NumCardsSelected = 1;

            if (SelectedStackIndex == -1) return null;
            if (SelectedStackIndex == 0) return drawStack;

            CardStack selectedStack = columns[SelectedStackIndex - 1].FaceUpStack;

            return selectedStack;
        }

        private bool canMoveCards(int toStack)
        {
            Card movingCard, receivingCard;
            CardStack movingStack;
            CardStack receivingStack = columns[toStack - 1].FaceUpStack;

            if (SelectedStackIndex == 0)
            {
                movingStack = drawStack;
            }
            else
            {
                movingStack = columns[SelectedStackIndex - 1].FaceUpStack;
            }

            movingCard = movingStack[movingStack.Count - 1];
            receivingCard = receivingStack[receivingStack.Count - 1];

            bool isAlternatingSuit = movingCard.IsRed != receivingCard.IsRed;
            bool isOneValueLower = movingCard.Value == receivingCard.Value - 1;

            return isOneValueLower && isAlternatingSuit;
        }

        public void MoveCards(int toStack)
        {
            if (!canMoveCards(toStack)) return;

            Card movingCard;

            if (SelectedStackIndex == 0)
            {
                movingCard = drawStack.DrawCard();
            }
            else
            {
                movingCard = columns[SelectedStackIndex - 1].FaceUpStack.DrawCard();
            }

            columns[toStack - 1].FaceUpStack.Add(movingCard);
        }
    }

    public struct StackColumn
    {
        public CardStack FaceDownStack;
        public CardStack FaceUpStack;
    }
}
