using VContainer;

namespace Infrastructure.GameStateMachine.States
{
    public sealed class PrepareState : IEnterState
    {
        private readonly IGameStateMachine _gameStateMachine;

        public PrepareState(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        [Inject]
        private void Construct()
        {
        }

        void IEnterState.Enter()
        {
            _gameStateMachine.Enter<MenuState, string>(SceneName.MENU);
        }

        void IExitState.Exit()
        {
        }
    }
}