using System;
using System.Collections.Generic;

namespace CardGames.Solitaire
{
    public class SolitaireDrawer
    {
        const int GAME_PADDING_X = 4;
        const int GAME_PADDING_Y = 1;
        const int DRAW_STACK_OFFSET_Y = 1;
        const int MAIN_STACK_OFFSET_Y = 9;
        const int MAIN_STACK_SECONDARY_OFFSET_Y = 2; // Start of the cards behind the main one.
        const int STACK_NUMBER_LOCAL_OFFSET = 4; // Where to place the numbers above each stack.
        const int STACK_SPAN = 14;
        const int INFO_OFFSET_X = 100;
        const int INFO_OFFSET_MAIN_Y = 8;

        public void DrawSolitaireGame(SolitaireGame game, CardStack deck, CardStack drawStack, List<CardStack> stacks, string statusMessage)
        {
            Console.Clear();

            var cardDrawer = new CardDrawer();

            // Draw the stack number for the draw stack.
            int posX = GAME_PADDING_X + STACK_SPAN + STACK_NUMBER_LOCAL_OFFSET;
            int posY = GAME_PADDING_Y;
            drawStackNumber(posX, posY, 0, game.SelectedStackIndex == 0);

            // Draw the draw stack.
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

            for (int i = 0; i < stacks.Count; i++)
            {
                CardStack stack = stacks[i];

                // Draw the stack numbers for the main stack.
                posX = GAME_PADDING_X + i * STACK_SPAN + STACK_NUMBER_LOCAL_OFFSET;
                posY = GAME_PADDING_Y + MAIN_STACK_OFFSET_Y;
                drawStackNumber(posX, posY, i + 1, game.SelectedStackIndex == i + 1);

                // Draw face down cards.
                if (stack.Count > 0 && !stack.cards[0].IsFaceUp)
                {
                    // They're all face down.
                    if (stack.AllFacingSame)
                    {
                        posX = GAME_PADDING_X + i * STACK_SPAN;
                        posY = GAME_PADDING_Y + MAIN_STACK_OFFSET_Y + 1;
                        cardDrawer.DrawCard(posX, posY);
                    }
                    else // Some are face up, just draw a hint of the face down ones.
                    { 
                        posX = GAME_PADDING_X + i * STACK_SPAN;
                        posY = GAME_PADDING_Y + MAIN_STACK_OFFSET_Y + 1;
                        cardDrawer.DrawCardSliverTop(posX, posY);
                    }
                }

                int numDrawnCards = 0;

                bool stackHighlighted = game.SelectedStackIndex == i + 1;

                // Draw face up cards.
                for (int ii = 0; ii < stack.Count; ii++)
                {
                    Card card = stack[ii];

                    if (!card.IsFaceUp) continue;

                    bool cardHighlighted = true;
                    bool highlight = stackHighlighted && cardHighlighted;

                    if (ii == stack.Count - 1)
                    {
                        posX = GAME_PADDING_X + i * STACK_SPAN;
                        posY = GAME_PADDING_Y + MAIN_STACK_OFFSET_Y + MAIN_STACK_SECONDARY_OFFSET_Y + numDrawnCards * 2;

                        cardDrawer.DrawCard(posX, posY, card, highlight);
                    }
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

            // Draw information to the right.;

            string[] solitaireArt = new string[7];
            solitaireArt[0] = @"  _____         _  _  _           _ ";
            solitaireArt[1] = @" / ____|       | |(_)| |         (_)";
            solitaireArt[2] = @"| (___    ___  | | _ | |_   __ _  _  _ __  ___ ";
            solitaireArt[3] = @" \___ \  / _ \ | || || __| / _` || || '__|/ _ \ ";
            solitaireArt[4] = @" ____) || (_) || || || |_ | (_| || || |   | __/";
            solitaireArt[5] = @"|_____/  \___/ |_||_| \__| \__,_||_||_|   \___|";

            drawAsciiArt(GAME_PADDING_X + INFO_OFFSET_X, GAME_PADDING_Y, solitaireArt);
            
            Console.SetCursorPosition(GAME_PADDING_X + INFO_OFFSET_X, GAME_PADDING_Y + INFO_OFFSET_MAIN_Y);
            Console.Write(statusMessage);
        }

        private void drawStackNumber(int posX, int posY, int number, bool selected)
        {
            Console.SetCursorPosition(posX, posY);

            if (selected) Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(number);
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void drawAsciiArt(int posX, int posY, string[] asciiLines)
        {
            for (int i = 0; i < asciiLines.Length; i++)
            {
                Console.SetCursorPosition(posX, posY + i);
                Console.Write(asciiLines[i]);
            }
        }
    }
}
