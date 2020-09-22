using System;
using System.Collections.Generic;

namespace Assets.Script
{
    public class StateMachine<Tstate>
    {
        private Dictionary<Tstate, Action> _stateToAction = new Dictionary<Tstate, Action>();
        public Tstate State { get; set; }
        public void RegisterAction(Tstate state, Action action)
        {
            _stateToAction.Add(state,action);
        }

        public void Run()
        {
            if(_stateToAction.ContainsKey(State))  _stateToAction[State].Invoke();
        }

        public StateMachine()
        {
        }

        public StateMachine(Dictionary<Tstate, Action> d)
        {
            _stateToAction = d;
        }
    }
}
