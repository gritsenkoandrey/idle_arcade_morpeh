using Game.Components;
using Game.Tags;
using Infrastructure.CameraService;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Game.Systems.Initialize
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class CharacterInitializeSystem : IInitializer
    {
        private readonly ICameraService _cameraService;
        
        private Filter _filter;
        private Stash<CharacterControllerComponent> _characterStash;
        private Stash<InputComponent> _inputStash;

        public CharacterInitializeSystem(ICameraService cameraService)
        {
            _cameraService = cameraService;
        }

        public World World { get; set; }

        public void OnAwake() 
        {
            _filter = World.Filter
                .With<CharacterTag>()
                .With<CharacterControllerComponent>()
                .Build();
            
            _characterStash = World.GetStash<CharacterControllerComponent>();
            _inputStash = World.GetStash<InputComponent>();

            foreach (Entity entity in _filter)
            {
                ref CharacterControllerComponent characterController = ref _characterStash.Get(entity);
                
                _cameraService.SetTarget(characterController.value.transform);
                
                _inputStash.Add(entity);
            }
        }

        public void Dispose()
        {
        }
    }
}