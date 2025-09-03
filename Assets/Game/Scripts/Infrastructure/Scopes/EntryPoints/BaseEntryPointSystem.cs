using System;
using Scellecs.Morpeh;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.Scopes.EntryPoints
{
    public abstract class BaseEntryPointSystem : IInitializable, ITickable, IFixedTickable, ILateTickable, IDisposable
    {
        private readonly IObjectResolver _objectResolver;

        protected BaseEntryPointSystem(IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
        }

        protected abstract ISystem[] CreateGameSystems();

        void IInitializable.Initialize()
        {
        }

        void ITickable.Tick()
        {
        }

        void IFixedTickable.FixedTick()
        {
        }

        void ILateTickable.LateTick()
        {
        }

        void IDisposable.Dispose()
        {
        }
    }
}