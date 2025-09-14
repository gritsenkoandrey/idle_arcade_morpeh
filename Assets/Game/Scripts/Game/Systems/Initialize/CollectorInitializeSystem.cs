using Game.Components;
using Game.Providers;
using Game.Tags;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Game.Systems.Initialize
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class CollectorInitializeSystem : IInitializer
    {
        private Filter _filter;
        private Stash<CollectorTag> _collectorStash;
        private Stash<IdComponent> _idStash;
        private Stash<ColliderComponent> _colliderStash;

        public World World { get; set; }

        public void OnAwake() 
        {
            _filter = World.Filter
                .With<CollectorTag>()
                .Build();

            _collectorStash = World.GetStash<CollectorTag>();
            _idStash = World.GetStash<IdComponent>();
            _colliderStash = World.GetStash<ColliderComponent>();
            
            foreach (Entity entity in _filter)
            {
                ref CollectorTag collector = ref _collectorStash.Get(entity);

                _idStash.Add(entity).value = entity.Id;
                
                foreach (ColliderProvider collider in collector.colliders)
                {
                    ref ColliderComponent component = ref _colliderStash.Get(collider.Entity);
                    
                    component.Disable();
                }

                foreach (RendererProvider renderer in collector.renderers)
                {
                    _idStash.Add(renderer.Entity).value = entity.Id;
                }
            }
        }

        public void Dispose()
        {
        }
    }
}