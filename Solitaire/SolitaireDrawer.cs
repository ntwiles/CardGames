using System;
using System.Collections.Generic;

using CardGames;
using CardGames.Drawing;

namespace Solitaire
{
    public class SolitaireDrawer : GameDrawer
    {
        const int DRAW_STACK_OFFSET_Y = 1;
        const int MAIN_STACK_OFFSET_Y = 9;
        const int MAIN_STACK_SECONDARY_OFFSET_Y = 2; // Start of the cards behind the main one.
        const int STACK_NUMBER_LOCAL_OFFSET = 4; // Where to place the numbers above each stack.
        const int STACK_SPAN = 14;

        private string[] solitaireTitle;

        public SolitaireDrawer()
        {
            solitaireTitle = new string[7];
            solitaireTitle[0] = @"  _____         _  _  _           _ ";
            solitaireTitle[1] = @" / ____|       | |(_)| |         (_)";
            solitaireTitle[2] = @"| (___    ___  | | _ | |_   __ _  _  _ __  ___ ";
            solitaireTitle[3] = @" \___ \  / _ \ | || || __| / _` || || '__|/ _ \ ";
            solitaireTitle[4] = @" ____) || (_) || || || |_ | (_| || || |   | __/";
            solitaireTitle[5] = @"|_____/  \___/ |_||_| \__| \__,_||_||_|   \___|";
        }

        public void DrawSolitaireGame(SolitaireGame game, CardStack deck, CardStack drawStack, List<StackColumn> columns, List<string> messages)
        {
            Console.Clear();

            drawGame(game, deck, drawStack, columns);

            drawTitle(solitaireTitle);

            writeMessages(messages);
        }

        // TODO: Abstract each step into its own method.
        private void drawGame(SolitaireGame game, CardStack deck, CardStack drawStack, List<StackColumn> columns)
        {
            var cardDrawer = new CardDrawer();

            // Draw the stack number for the draw stack.
            int posX = GAME_PADDING_X + STACK_SPAN + STACK_NUMBER_LOCAL_OFFSET;
            int posY = GAME_PADDING_Y;
            drawStackNumber(posX, posY, 0, game.SelectedStackIndex == 0);

            // Draw the deck.
            if (deck.Count > 0)
            {
                posX = GAME_PADDING_X;
                posY = GAME_PADDING_Y + DRAW_STACK_OFFSET_Y;
                cardDrawer.DrawCard(posX, posY);
            }

            // Draw the draw stack.
            if (drawStack.Count > 0)
            {
                posX = GAME_PADDING_X + STACK_SPAN;
                posY = GAME_PADDING_Y + DRAW_STACK_OFFSET_Y;
                bool highlight = game.SelectedStackIndex == 0;
                cardDrawer.DrawCard(posX, posY, drawStack[drawStack.Count - 1], highlight);
            }

            for (int i = 0; i < columns.Count; i++)
            {
                CardStack faceDownStack = columns[i].FaceDownStack;
                CardStack faceUpStack = columns[i].FaceUpStack;

                // Draw the stack numbers for the main stack.
                posX = GAME_PADDING_X + i * STACK_SPAN + STACK_NUMBER_LOCAL_OFFSET;
                posY = GAME_PADDING_Y + MAIN_STACK_OFFSET_Y;
                drawStackNumber(posX, posY, i + 1, game.SelectedStackIndex == i + 1);

                // Draw face down cards.
                if (faceDownStack.Count > 0 && faceUpStack.Count == 0)                
                {
                    // There's nothing covering them.
                    posX = GAME_PADDING_X + i * STACK_SPAN;
                    posY = GAME_PADDING_Y + MAIN_STACK_OFFSET_Y + 1;
                    cardDrawer.DrawCard(posX, posY);
                }
                else if (faceDownStack.Count > 0)
                {
                    // There are face up cards on top, only draw a sliver.
                    posX = GAME_PADDING_X + i * STACK_SPAN;
                    posY = GAME_PADDING_Y + MAIN_STACK_OFFSET_Y + 1;
                    cardDrawer.DrawCardSliverTop(posX, posY);
                }
                
                int numDrawnCards = 0;

                bool stackHighlighted = game.SelectedStackIndex == i + 1;

                // Draw face up cards.
                for (int ii = 0; ii < faceUpStack.Count; ii++)
                {
                    Card card = faceUpStack[ii];

                    int invertedCardNum = faceUpStack.Count - ii;
                    bool cardHighlighted = game.NumCardsSelected >= invertedCardNum;
                    bool highlight = stackHighlighted && cardHighlighted;

                    // Draw the top card at full length.
                    if (ii == faceUpStack.Count - 1)
                    {
                        posX = GAME_PADDING_X + i * STACK_SPAN;
                        posY = GAME_PADDING_Y + MAIN_STACK_OFFSET_Y + MAIN_STACK_SECONDARY_OFFSET_Y + numDrawnCards * 2;

                        cardDrawer.DrawCard(posX, posY, card, highlight);
                    }
                    // And the rest just draw partially.
                    else
                    {
                        posX = GAME_PADDING_X + i * STACK_SPAN;
                        posY = GAME_PADDING_Y + MAIN_STACK_OFFSET_Y + MAIN_STACK_SECONDARY_OFFSET_Y + numDrawnCards * 2;

                        cardDrawer.DrawFaceUpCardTop(posX, posY, card, highlight);
                        numDrawnCards++;
                    }
                }

                Console.WriteLine("\n\n\n\n\n\n\n\n");
            }
        }

        private void drawStackNumber(int posX, int posY, int number, bool selected)
        {
            Console.SetCursorPosition(posX, posY);

            if (selected) Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(number);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
