using Cysharp.Threading.Tasks;

namespace StateMachines.Base {
    public abstract class ExitableStateBase {
        public abstract UniTask Exit();

        public abstract void SetStateMachine(StateMachineBase stateMachine);
    }
}