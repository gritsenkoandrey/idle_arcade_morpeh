using Infrastructure.GameStateMachine;

namespace Infrastructure.Factories.StateMachine
{
    public interface IStateMachineFactory
    {
        IGameStateMachine CreateGameStateMachine();
    }
}