using System;
using System.Collections.Generic;
using System.Text;

using CardGames.States;

namespace Solitaire
{
    public class SolitaireStateSelectingDestinationStack : State
    {
        private SolitaireGame game;
        private ConsoleInput input;

        public override void Init(StateMachine machine)
        {
            game = (SolitaireGame)machine;
            game.StatusMessages.Clear();
            game.StatusMessages.Add("What would you like to do next?");
            game.StatusMessages.Add("Press up/down arrows to decide how many cards to move.");
            game.StatusMessages.Add("Press a 0-9 to select a destination.");
            game.DrawGame();
            input = new ConsoleInput();
        }

        public override void Update()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            char keyChar = keyInfo.KeyChar;

            // up or down to select an amount of cards.
            if (keyInfo.Key == ConsoleKey.DownArrow) 
            {
                game.NumCardsSelected--;
            }
            else if (keyInfo.Key == ConsoleKey.UpArrow)
            {
                game.NumCardsSelected++;
            }

            // 0 to 9 to select a stack.
            else if (keyChar >= 48 && keyChar <= 57)
            {
                int chosenStack;
                chosenStack = (int)keyChar - 48;
                game.MoveCards(chosenStack);
                game.SelectedStackIndex = -1;
                game.SetState(new SolitaireStateSelectingStack());
            }
        }
    }
}
