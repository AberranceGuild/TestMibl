using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using StateMachines.Base;

namespace StateMachines {
    public class GameStateMachine : StateMachineBase {
        public GameStateMachine(Dictionary<Type, ExitableStateBase> states) {
            States = states ?? throw new ArgumentNullException(nameof(states));
            foreach ((Type _, ExitableStateBase value) in States)
                value.SetStateMachine(this);
        }

        public override async UniTask Enter<TState>() {
            if (States.TryGetValue(typeof(TState), out ExitableStateBase _) is false)
                return;

            StateBase state = await ChangeState<TState>();
            await state.Enter();
        }

        public override void Cleanup() {
            States.Clear();
            ActiveState = null;
        }

        protected async UniTask<TState> ChangeState<TState>() where TState : ExitableStateBase {
            if (ActiveState is not null)
                await ActiveState.Exit();

            TState state = GetState<TState>();
            ActiveState = state;
            return state;
        }

        protected TState GetState<TState>() where TState : ExitableStateBase =>
            States[typeof(TState)] as TState;
    }
}