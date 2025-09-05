using Game.Components;
using Game.Tags;
using Infrastructure.JoystickService;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Game.Systems.Update
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class JoystickUpdateSystem : ISystem
    {
        private readonly IJoystickService _joystickService;
        
        private Filter _filter;
        private Stash<InputComponent> _inputStash;

        public JoystickUpdateSystem(IJoystickService joystickService)
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
            
            _inputStash = World.GetStash<InputComponent>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            _joystickService.Execute();

            foreach (Entity entity in _filter)
            {
                ref InputComponent input = ref _inputStash.Get(entity);

                input.axis = _joystickService.GetAxis();
            }
        }
        
        public void Dispose()
        {
        }
    }
}