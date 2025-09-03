using VContainer;

namespace Infrastructure.GameStateMachine.States
{
    public sealed class GameState : IEnterState
    {
        private readonly IGameStateMachine _gameStateMachine;

        public GameState(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        [Inject]
        private void Construct()
        {
        }

        void IEnterState.Enter()
        {
        }

        void IExitState.Exit()
        {
        }
    }
}