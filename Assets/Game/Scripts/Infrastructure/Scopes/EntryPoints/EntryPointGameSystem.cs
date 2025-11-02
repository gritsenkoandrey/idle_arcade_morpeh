using Game.Systems.Events;
using Game.Systems.Initialize;
using Game.Systems.Update;
using Infrastructure.CameraService;
using Infrastructure.JoystickService;
using JetBrains.Annotations;
using Scellecs.Morpeh;

namespace Infrastructure.Scopes.EntryPoints
{
    [UsedImplicitly]
    public sealed class EntryPointGameSystem : BaseEntryPointSystem
    {
        private readonly ICameraService _cameraService;
        private readonly IJoystickService  _joystickService;
        
        private World _world;
        private SystemsGroup _systemsGroup;
        
        public EntryPointGameSystem(ICameraService cameraService, IJoystickService joystickService)
        {
            _cameraService = cameraService;
            _joystickService = joystickService;
        }
        
        protected override void Initialize()
        {
            _world = World.Default;
            _systemsGroup = _world.CreateSystemsGroup();
            _systemsGroup.AddInitializer(new CharacterInitializeSystem(_cameraService));
            _systemsGroup.AddInitializer(new CollectorInitializeSystem());
            _systemsGroup.AddInitializer(new CharacterItemCollisionSystem());
            _systemsGroup.AddInitializer(new CharacterCollectorCollisionSystem());
            _systemsGroup.AddSystem(new JoystickUpdateSystem(_joystickService));
            _systemsGroup.AddSystem(new CharacterInputSystem(_joystickService));
            _systemsGroup.AddSystem(new CharacterRunSystem(_cameraService));
            _systemsGroup.AddSystem(new CharacterAnimationSystem());
            
            _world.AddSystemsGroup(order: 0, _systemsGroup);
        }
        
        protected override void Dispose()
        {
            Filter filter = _world.Filter.Build();

            foreach (Entity entity in filter)
            {
                _world.RemoveEntity(entity);
            }
            
            _world.RemoveSystemsGroup(_systemsGroup);
        }
    }
}