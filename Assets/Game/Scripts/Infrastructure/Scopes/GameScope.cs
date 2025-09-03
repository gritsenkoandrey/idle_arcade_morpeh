using Infrastructure.Scopes.EntryPoints;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.Scopes
{
    public sealed class GameScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            
            builder.RegisterEntryPoint<EntryPointGameSystem>(Lifetime.Scoped).AsSelf().Build();
        }
    }
}