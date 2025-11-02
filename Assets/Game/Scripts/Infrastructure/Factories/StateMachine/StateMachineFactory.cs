using Infrastructure.GameStateMachine;
using Infrastructure.Utils;
using JetBrains.Annotations;
using VContainer;

namespace Infrastructure.Factories.StateMachine
{
    [UsedImplicitly]
    public sealed class StateMachineFactory : IStateMachineFactory
    {
        private readonly IObjectResolver _objectResolver;

        public StateMachineFactory(IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
        }

        IGameStateMachine IStateMachineFactory.CreateGameStateMachine()
        {
            GameStateMachine.GameStateMachine gameStateMachine = new ();
            gameStateMachine.States.Values.Foreach(_objectResolver.Inject);
            return gameStateMachine;
        }
    }
}