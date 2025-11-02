using Infrastructure.Factories.StateMachine;
using Infrastructure.GameStateMachine.States;
using JetBrains.Annotations;
using VContainer.Unity;

namespace Infrastructure.Scopes.EntryPoints
{
    [UsedImplicitly]
    public sealed class EntryPoint : IInitializable
    {
        private readonly IStateMachineFactory _stateMachineFactory;

        public EntryPoint(IStateMachineFactory stateMachineFactory)
        {
            _stateMachineFactory = stateMachineFactory;
        }

        void IInitializable.Initialize()
        {
            _stateMachineFactory.CreateGameStateMachine().Enter<BootstrapState>();
        }
    }
}