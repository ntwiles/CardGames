using System;
using System.Collections.Generic;
using System.Text;

using CardGames;
using CardGames.States;

namespace Solitaire
{
    public class SolitaireStateSelectingStack : State
    {
        private SolitaireGame game;
        private ConsoleInput input;

        public override void Init(StateMachine machine)
        {
            game = (SolitaireGame)machine;
            game.SelectedStackIndex = -1;
            game.StatusMessages.Clear();
            game.StatusMessages.Add("Press a number to select a stack.");
            game.DrawGame();
            input = new ConsoleInput();
        }

        public override void Update()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            char keyChar = keyInfo.KeyChar;

            if (keyInfo.Key == ConsoleKey.Spacebar)
            {
                game.DrawCard();
            }

            // 0 to 9 to select a stack.
            if (keyChar >= 48 && keyChar <= 57)
            {
                int chosenStackIndex = (int)keyChar - 48;
                CardStack chosenStack = game.GetStack(chosenStackIndex);

                if (chosenStack.IsFaceUp)
                {
                    game.SelectedStackIndex = chosenStackIndex;
                    game.NumCardsSelected = 1;
                    game.SetState(new SolitaireStateSelectingDestinationStack());
                }
                else
                {
                    game.FlipCardUp(chosenStackIndex);
                    game.SetState(new SolitaireStateSelectingStack());
                }
            }
        }
    }
}
