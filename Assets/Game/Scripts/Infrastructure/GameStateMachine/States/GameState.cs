using Infrastructure.JoystickService;
using VContainer;

namespace Infrastructure.GameStateMachine.States
{
    public sealed class GameState : IEnterState
    {
        private readonly IGameStateMachine _gameStateMachine;
        
        private IJoystickService _joystickService;

        public GameState(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        [Inject]
        private void Construct(IJoystickService joystickService)
        {
            _joystickService = joystickService;
        }

        void IEnterState.Enter()
        {
            _joystickService.Enable(true);
        }

        void IExitState.Exit()
        {
            _joystickService.Enable(false);
        }
    }
}