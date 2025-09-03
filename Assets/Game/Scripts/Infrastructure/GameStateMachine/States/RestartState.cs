using VContainer;

namespace Infrastructure.GameStateMachine.States
{
    public sealed class RestartState : IEnterState<string>
    {
        private readonly IGameStateMachine _gameStateMachine;

        public RestartState(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        [Inject]
        private void Construct()
        {
        }
        
        void IEnterState<string>.Enter(string param)
        {
        }
        
        void IExitState.Exit()
        {
        }
    }
}