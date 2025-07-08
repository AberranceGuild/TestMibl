using Cysharp.Threading.Tasks;

namespace StateMachines.Base {
    public abstract class StateBase : ExitableStateBase {
        public abstract UniTask Enter();
    }
}