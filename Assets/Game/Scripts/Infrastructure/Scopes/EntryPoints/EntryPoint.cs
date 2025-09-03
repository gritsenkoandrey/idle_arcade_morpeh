using Infrastructure.Factories.StateMachine;
using Infrastructure.GameStateMachine.States;
using VContainer.Unity;

namespace Infrastructure.Scopes.EntryPoints
{
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