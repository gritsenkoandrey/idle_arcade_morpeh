using Game.Components;
using Game.Markers;
using Game.Tags;
using Infrastructure.Utils;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Game.Systems.Update
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class CharacterAnimationSystem : ISystem
    {
        private Filter _filter;
        private Stash<AnimatorComponent> _animatorStash;
        private Stash<RunMarker> _runStash;
        
        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter
                .With<CharacterTag>()
                .With<AnimatorComponent>()
                .Build();

            _animatorStash = World.GetStash<AnimatorComponent>();
            _runStash = World.GetStash<RunMarker>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (Entity entity in _filter)
            {
                ref AnimatorComponent animator = ref _animatorStash.Get(entity);

                animator.value.SetFloat(Animations.Run, _runStash.Has(entity) ? 1f : 0f);
            }
        }
        
        public void Dispose()
        {
        }
    }
}