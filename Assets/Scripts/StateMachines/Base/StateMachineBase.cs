using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace StateMachines.Base {
    public abstract class StateMachineBase {
        public ExitableStateBase ActiveState { get; protected set; }

        public Dictionary<Type, ExitableStateBase> States { get; protected set; }

        public abstract UniTask Enter<TState>() where TState : StateBase;

        public abstract void Cleanup();
    }
}