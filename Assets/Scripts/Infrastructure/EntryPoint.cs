using Cysharp.Threading.Tasks;
using StateMachines.Base;
using StateMachines.Base.Collection;
using UnityEngine;
using Zenject;

namespace Infrastructure {
    public class EntryPoint : MonoBehaviour {
        private StateMachineBase _stateMachine;

        [Inject]
        public void SetDependencies(StateMachineBase stateMachine) {
            _stateMachine = stateMachine;
            _stateMachine.Enter<IdleStateBase>()
                         .Forget();
        }
    }
}