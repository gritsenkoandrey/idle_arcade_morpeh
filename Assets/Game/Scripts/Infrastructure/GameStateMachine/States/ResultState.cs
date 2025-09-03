using VContainer;

namespace Infrastructure.GameStateMachine.States
{
    public sealed class ResultState : IEnterState<bool>
    {
        private readonly IGameStateMachine _gameStateMachine;

        public ResultState(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        [Inject]
        private void Construct()
        {
        }

        void IEnterState<bool>.Enter(bool param)
        {
        }

        void IExitState.Exit()
        {
        }
    }
}