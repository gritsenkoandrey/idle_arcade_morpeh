using Game.Components;
using Game.Markers;
using Game.Tags;
using Infrastructure.JoystickService;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Game.Systems.Update
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class CharacterInputSystem : ISystem 
    {
        private readonly IJoystickService _joystickService;
        
        private Filter _filter;
        private Stash<RunMarker> _runStash;

        public CharacterInputSystem(IJoystickService joystickService)
        {
            _joystickService = joystickService;
        }

        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter
                .With<CharacterTag>()
                .With<InputComponent>()
                .Build();
            
            _runStash = World.GetStash<RunMarker>();
        }

        public void OnUpdate(float deltaTime) 
        {
            foreach (Entity entity in _filter)
            {
                TryAddOrRemoveRunComponent(entity);
            }
        }

        public void Dispose()
        {
        }

        private void TryAddOrRemoveRunComponent(Entity entity)
        {
            if (_joystickService.HasInput())
            {
                if (_runStash.Has(entity))
                {
                    return;
                }

                _runStash.Add(entity);
            }
            else
            {
                if (_runStash.Has(entity))
                {
                    _runStash.Remove(entity);
                }
            }
        }
    }
}