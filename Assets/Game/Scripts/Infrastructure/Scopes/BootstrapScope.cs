using Infrastructure.Scopes.EntryPoints;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.Scopes
{
    public sealed class BootstrapScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            
            builder.RegisterEntryPoint<EntryPoint>(Lifetime.Scoped).AsSelf().Build();
        }
    }
}