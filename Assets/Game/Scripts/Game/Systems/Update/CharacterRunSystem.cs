using Game.Components;
using Game.Markers;
using Game.Tags;
using Infrastructure.CameraService;
using Infrastructure.Utils;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Game.Systems.Update
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class CharacterRunSystem : ISystem
    {
        private readonly ICameraService _cameraService;
        
        private Filter _filter;
        private Stash<CharacterControllerComponent> _characterViewStash;
        private Stash<InputComponent> _inputStash;

        private const float SPEED = 2.5f;
        private const float RAY_DISTANCE = 5f;
        private const float LERP_ROTATE = 0.25f;

        public CharacterRunSystem(ICameraService cameraService)
        {
            _cameraService = cameraService;
        }

        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter
                .With<CharacterTag>()
                .With<CharacterControllerComponent>()
                .With<InputComponent>()
                .With<RunMarker>()
                .Without<IdleMarker>()
                .Build();
            
            _characterViewStash = World.GetStash<CharacterControllerComponent>();
            _inputStash = World.GetStash<InputComponent>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (Entity entity in _filter)
            {
                ref CharacterControllerComponent characterController = ref _characterViewStash.Get(entity);
                ref InputComponent input = ref _inputStash.Get(entity);
                
                float angle = Mathf.Atan2(input.axis.x, input.axis.y) * Mathf.Rad2Deg + _cameraService.GetEulerAngleY();
                float lerpAngle = Mathf.LerpAngle(characterController.value.transform.eulerAngles.y, angle, LERP_ROTATE);
                characterController.value.transform.rotation = Quaternion.Euler(0f, lerpAngle, 0f);
                
                Vector3 move = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
                Vector3 next = characterController.value.transform.position + move * SPEED * deltaTime;
                
                Ray ray = new() { origin = next, direction = Vector3.down };
                
                if (Physics.Raycast(ray, RAY_DISTANCE, Layers.GroundShift) == false)
                {
                    return;
                }
                
                move.y = characterController.value.isGrounded ? 0f : Physics.gravity.y;

                characterController.value.Move(move * SPEED * deltaTime);
            }
        }
        
        public void Dispose()
        {
        }
    }
}