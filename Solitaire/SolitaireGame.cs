﻿using System;
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

        private List<StackColumn> columns;

        private int numCardsSelected;
        private int selectedStackIndex;

        public int NumCardsSelected
        {
            get { return numCardsSelected; }
            set
            {
                if (SelectedStackIndex == -1) return;

                CardStack faceUpCards = columns[SelectedStackIndex - 1].FaceUpStack;

                if (faceUpCards.Count > 0)
                {
                    int maxCardsSelectable = faceUpCards.Count;
                    numCardsSelected = Math.Clamp(value, 1, maxCardsSelectable);
                }
            }
        }

        public int SelectedStackIndex
        {
            get { return selectedStackIndex; }
            set
            {
                if (value < columns.Count + 1 && value >= -1)
                    selectedStackIndex = value;
            }
        }

        public List<string> StatusMessages { get; set; }

        private bool playing;

        // TODO: Abstract a lot of this out into a new SolitaireGameBuilder class. 
        public SolitaireGame()
        {
            StatusMessages = new List<string>();

            deck = new DeckBuilder().BuildDeck(true);

            drawStack = new CardStack(true);
            drawer = new SolitaireDrawer();

            columns = new List<StackColumn>();

            // Create 7 stacks of face down cards.
            for (int i = 0; i < 7; i++)
            {
                CardStack stack = new CardStack(false);
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
            for (int i = 0; i < columns.Count; i++)
            {
                StackColumn column = columns[i];
                CardStack faceDownStack = column.FaceDownStack;
                CardStack faceUpStack = new CardStack(true);
                Card topCard = faceDownStack.DrawCard();
                faceUpStack.Add(topCard);
                column.FaceUpStack = faceUpStack;
            }

            TransferCardToDrawStack();
        }

        private void TransferCardToDrawStack()
        {
            Card drawnCard = deck.DrawCard();
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

        public CardStack GetStack(int stackIndex)
        {
            CardStack chosenStack = null;

            if (stackIndex == 0) chosenStack = drawStack;
            else if (stackIndex > 0 && stackIndex <= columns.Count)
            {
                StackColumn chosenColumn = columns[stackIndex - 1];

                if (chosenColumn.FaceUpStack.Count > 0) chosenStack = chosenColumn.FaceUpStack;
                else if (chosenColumn.FaceDownStack.Count > 0) chosenStack = chosenColumn.FaceDownStack;
            }

            return chosenStack;
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

        // TODO FlipCardUp does not do a check to see if it can be done, but relies on the state to check. 
        // This action has a 'canMoveCards' check first though. Make this more consistent.
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

        // Should only be called on one of the 7 columns and when FaceUpStack is empty but FaceDownStack has cards.
        public void FlipCardUp(int stackIndex)
        {
            if (stackIndex < 1 || stackIndex > columns.Count) throw new IndexOutOfRangeException();

            CardStack faceDownStack = columns[stackIndex - 1].FaceDownStack;
            CardStack faceUpStack = columns[stackIndex - 1].FaceUpStack;

            Card selectedCard = faceDownStack.DrawCard();
            faceUpStack.Add(selectedCard);
        }
    }
}
