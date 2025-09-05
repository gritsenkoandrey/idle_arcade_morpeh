using Infrastructure.JoystickService;
using VContainer;

namespace Infrastructure.GameStateMachine.States
{
    public sealed class PrepareState : IEnterState
    {
        private readonly IGameStateMachine _gameStateMachine;

        private IJoystickService  _joystickService;
        
        public PrepareState(IGameStateMachine gameStateMachine)
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
            _joystickService.Init();
            
            _gameStateMachine.Enter<MenuState, string>(SceneName.MENU);
        }

        void IExitState.Exit()
        {
        }
    }
}