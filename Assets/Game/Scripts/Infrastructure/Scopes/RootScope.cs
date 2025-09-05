using Infrastructure.CameraService;
using Infrastructure.CoroutineService;
using Infrastructure.Factories.StateMachine;
using Infrastructure.JoystickService;
using Infrastructure.SceneLoadService;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.Scopes
{
    public sealed class RootScope : LifetimeScope
    {
        [SerializeField] private CoroutineService.CoroutineService _coroutineService;
        [SerializeField] private JoystickService.JoystickService _joystickService;
        [SerializeField] private CameraService.CameraService  _cameraService;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            
            builder.RegisterComponentInNewPrefab(_coroutineService, Lifetime.Singleton).DontDestroyOnLoad().As<ICoroutineService>();
            builder.RegisterComponentInNewPrefab(_joystickService, Lifetime.Singleton).DontDestroyOnLoad().As<IJoystickService>();
            builder.RegisterComponentInNewPrefab(_cameraService, Lifetime.Singleton).DontDestroyOnLoad().As<ICameraService>();

            builder.Register<ISceneLoadService, SceneLoadService.SceneLoadService>(Lifetime.Singleton);
            builder.Register<IStateMachineFactory, StateMachineFactory>(Lifetime.Singleton);
        }
    }
}