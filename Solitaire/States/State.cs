using System;
using System.Collections.Generic;
using System.Text;

namespace CardGames.States
{
    public abstract class State
    {
        public abstract void Init(StateMachine state);
        public abstract void Update();
    }
}
