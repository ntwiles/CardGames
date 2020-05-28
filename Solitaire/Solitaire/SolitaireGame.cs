using System;
using System.Collections.Generic;
using System.Resources;
using System.Text;

using CardGames.States;

namespace CardGames.Solitaire
{
    public class SolitaireGame : StateMachine
    {
        CardStack deck;
        CardStack drawStack;
        SolitaireDrawer drawer;

        //private Card currentDrawnCard;
        private List<CardStack> stacks;

        public int SelectedStackIndex { get; private set; }
        public int NumCardsSelected { get; private set; }
        public string StatusMessage { get; set; }

        private bool playing;

        public SolitaireGame()
        {
            deck = new DeckBuilder().BuildDeck(true, false);
            drawStack = new CardStack();
            drawer = new SolitaireDrawer();

            stacks = new List<CardStack>();

            // Create 7 stacks of cards.
            for (int i = 0; i < 7; i++)
            {
                CardStack stack = new CardStack();
                stacks.Add(stack);
            }

            // Distribute the cards across the stacks.
            for (int i = 0; i < 7; i ++)
            {
                for (int ii = i; ii < 7; ii++)
                {
                    CardStack stack = stacks[ii];
                    Card drawnCard = deck.DrawCard();
                    stack.Add(drawnCard);
                }
            }

            // Flip top cards
            for (int i = 0; i < 7; i++)
            {
                CardStack stack = stacks[i];
                Card topCard = stack.cards[stack.cards.Count - 1];
                topCard.Flip();
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
            drawer.DrawSolitaireGame(this, deck, drawStack, stacks, StatusMessage);
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

            CardStack selectedStack = stacks[SelectedStackIndex - 1];

            return selectedStack;
        }

        private bool canMoveCards(int toStack)
        {
            Card movingCard, receivingCard;
            CardStack movingStack;
            CardStack receivingStack = stacks[toStack - 1];

            if (SelectedStackIndex == 0)
            {
                movingStack = drawStack;
            }
            else
            {
                movingStack = stacks[SelectedStackIndex - 1];
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
                movingCard = stacks[SelectedStackIndex - 1].DrawCard();
            }

            stacks[toStack - 1].Add(movingCard);
        }
    }
}
