using Infrastructure.SceneLoadService;
using VContainer;

namespace Infrastructure.GameStateMachine.States
{
    public sealed class LoadLevelState : IEnterState<string>
    {
        private readonly IGameStateMachine _gameStateMachine;
        
        private ISceneLoadService  _sceneLoadService;

        public LoadLevelState(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        [Inject]
        private void Construct(ISceneLoadService sceneLoadService)
        {
            _sceneLoadService = sceneLoadService;
        }

        void IEnterState<string>.Enter(string param)
        {
            _sceneLoadService.Load(param,  () => _gameStateMachine.Enter<GameState>());
        }

        void IExitState.Exit()
        {
        }
    }
}