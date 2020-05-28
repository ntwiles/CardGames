using System;
using System.Collections.Generic;
using System.Text;

using CardGames.States;

namespace CardGames.Solitaire
{
    public class SolitaireStateSelectingStack : State
    {
        private SolitaireGame game;
        private ConsoleInput input;

        public override void Init(StateMachine machine)
        {
            game = (SolitaireGame)machine;
            game.SelectStack(-1);
            game.StatusMessage = "Press a number to select a stack.";
            game.DrawGame();
            input = new ConsoleInput();
        }

        public override void Update()
        {
            CardStack selectedStack = game.SelectStack(input.GetStackChoice());
            Card selectedCard = selectedStack[selectedStack.Count - 1];

            if (!selectedCard.IsFaceUp)
            {
                selectedCard.Flip();
                game.SetState(new SolitaireStateSelectingStack());
            }
            else
            {
                game.SetState(new SolitaireStateSelectingDestinationStack());
            }
        }
    }
}
