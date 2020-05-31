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
            int chosenStackIndex = input.GetStackChoice();
            CardStack chosenStack = game.GetStack(chosenStackIndex);

            if (chosenStack == null) return;

            if (chosenStack.IsFaceUp)
            {
                game.SelectedStackIndex = chosenStackIndex;
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
