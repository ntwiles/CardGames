using System;
using System.Collections.Generic;
using System.Text;

namespace CardGames.States
{
    public abstract class StateMachine
    {
        protected State State;

        public void SetState(State state)
        {
            State = state;
            State.Init(this);
        }
    }
}
