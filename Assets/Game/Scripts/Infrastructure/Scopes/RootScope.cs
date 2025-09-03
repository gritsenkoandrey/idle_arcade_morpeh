using Infrastructure.CoroutineService;
using Infrastructure.Factories.StateMachine;
using Infrastructure.SceneLoadService;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.Scopes
{
    public sealed class RootScope : LifetimeScope
    {
        [SerializeField] private CoroutineService.CoroutineService _coroutineService;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            
            builder.RegisterComponentInNewPrefab(_coroutineService, Lifetime.Singleton).DontDestroyOnLoad().As<ICoroutineService>();

            builder.Register<ISceneLoadService, SceneLoadService.SceneLoadService>(Lifetime.Singleton);
            builder.Register<IStateMachineFactory, StateMachineFactory>(Lifetime.Singleton);
        }
    }
}