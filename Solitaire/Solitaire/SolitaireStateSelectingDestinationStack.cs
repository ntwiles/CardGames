using System;
using System.Collections.Generic;
using System.Text;

using CardGames.States;

namespace CardGames.Solitaire
{
    public class SolitaireStateSelectingDestinationStack : State
    {
        private SolitaireGame game;
        private ConsoleInput input;

        public override void Init(StateMachine machine)
        {
            game = (SolitaireGame)machine;
            game.StatusMessage = "What would you like to do next?\n";
            game.StatusMessage += "Press up/down arrows to decide how many cards to move.\n";
            game.StatusMessage += "Press a 0-9 to select a destination.\n";
            game.DrawGame();
            input = new ConsoleInput();
        }

        public override void Update()
        {
            int chosenStack = input.GetStackChoice();
            game.MoveCards(chosenStack);
            game.SetState(new SolitaireStateSelectingStack());
        }
    }
}
