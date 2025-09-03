using Infrastructure.SceneLoadService;
using VContainer;

namespace Infrastructure.GameStateMachine.States
{
    public sealed class BootstrapState : IEnterState
    {
        private readonly IGameStateMachine _gameStateMachine;
        
        private ISceneLoadService _sceneLoadService;

        public BootstrapState(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        [Inject]
        private void Construct(ISceneLoadService sceneLoadService)
        {
            _sceneLoadService = sceneLoadService;
        }

        void IEnterState.Enter()
        {
            _sceneLoadService.Load(SceneName.BOOTSTRAP, () => _gameStateMachine.Enter<PrepareState>());
        }

        void IExitState.Exit()
        {
        }
    }
}