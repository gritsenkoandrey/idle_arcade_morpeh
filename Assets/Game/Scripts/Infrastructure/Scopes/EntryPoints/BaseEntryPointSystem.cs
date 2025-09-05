using System;
using VContainer.Unity;

namespace Infrastructure.Scopes.EntryPoints
{
    public abstract class BaseEntryPointSystem : IStartable, IDisposable
    {
        void IStartable.Start()
        {
            Initialize();
        }
        
        void IDisposable.Dispose()
        {
            Dispose();
        }
        
        protected abstract void Initialize();
        
        protected abstract void Dispose();
    }
}