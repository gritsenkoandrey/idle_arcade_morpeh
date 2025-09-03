using Infrastructure.Scopes.EntryPoints;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.Scopes
{
    public sealed class MenuScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            
            builder.RegisterEntryPoint<EntryPointMenuSystem>(Lifetime.Scoped).AsSelf().Build();
        }
    }
}