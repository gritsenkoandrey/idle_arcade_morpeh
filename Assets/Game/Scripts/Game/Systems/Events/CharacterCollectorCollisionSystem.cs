using System;
using Game.Components;
using Game.Events;
using Game.Providers;
using Game.Tags;
using LitMotion;
using LitMotion.Extensions;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Collections;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Game.Systems.Events
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class CharacterCollectorCollisionSystem : IInitializer
    {
        private Filter _collectedFilter;
        private Filter _rendererFilter;
        private Stash<CharacterTag> _characterStash;
        private Stash<CollectedTag> _collectedStash;
        private Stash<TransformComponent> _transformStash;
        private Stash<CollectorTag> _collectorStash;
        private Stash<RendererComponent> _rendererStash;
        private Stash<IdComponent> _idStash;
        private Stash<ColliderComponent> _colliderStash;
        private Event<TriggerEvent> _triggerEvent;
        private IDisposable _subscription;

        public World World { get; set; }

        public void OnAwake()
        {
            _collectedFilter = World.Filter
                .With<CollectedTag>()
                .With<TransformComponent>()
                .With<RendererComponent>()
                .Build();

            _rendererFilter = World.Filter
                .With<RendererComponent>()
                .With<IdComponent>()
                .Build();
            
            _characterStash = World.GetStash<CharacterTag>();
            _collectedStash = World.GetStash<CollectedTag>();
            _transformStash = World.GetStash<TransformComponent>();
            _collectorStash = World.GetStash<CollectorTag>();
            _rendererStash = World.GetStash<RendererComponent>();
            _idStash = World.GetStash<IdComponent>();
            _colliderStash = World.GetStash<ColliderComponent>();
            _triggerEvent = World.GetEvent<TriggerEvent>();
            _subscription = _triggerEvent.Subscribe(OnTriggerEvent);
        }

        public void Dispose()
        {
            _subscription.Dispose();
        }

        private void OnTriggerEvent(FastList<TriggerEvent> triggers)
        {
            foreach (TriggerEvent trigger in triggers)
            {
                if (_characterStash.Has(trigger.source) && _collectorStash.Has(trigger.target))
                {
                    ref CollectorTag collector = ref _collectorStash.Get(trigger.target);
                    ref IdComponent id = ref _idStash.Get(trigger.target);
                    
                    foreach (Entity entity in _collectedFilter)
                    {
                        ref TransformComponent transform = ref _transformStash.Get(entity);
                        ref RendererComponent renderer = ref _rendererStash.Get(entity);

                        foreach (ColliderProvider collider in collector.colliders)
                        {
                            ref ColliderComponent component = ref _colliderStash.Get(collider.Entity);
                            
                            if (component.IsEnabled)
                            {
                                continue;
                            }

                            _collectedStash.Remove(entity);
                            
                            component.Enable();
                            transform.SetParent(component.value.transform);
                            transform.value.localPosition = Vector3.zero;
                            transform.value.localRotation = Quaternion.identity;
                            
                            Vector3 toScale = Vector3.one - new Vector3(0f, 0f, 0.075f);

                            _ = LMotion.Create(Vector3.zero, toScale, 0.5f)
                                .WithEase(Ease.OutBack)
                                .BindToLocalScale(transform.value)
                                .AddTo(transform.value, LinkBehavior.CancelOnDestroy);
                            
                            break;
                        }

                        if (IsFullCollected(ref collector))
                        {
                            Complete(ref id, ref renderer);
                        }
                    }
                }
            }
        }
        
        private bool IsFullCollected(ref CollectorTag collector)
        {
            foreach (ColliderProvider element in collector.colliders)
            {
                ref ColliderComponent collider = ref _colliderStash.Get(element.Entity);
                
                if (collider.IsEnabled == false)
                {
                    return false;
                }
            }
            
            return true;
        }
        
        private void Complete(ref IdComponent collectorId, ref RendererComponent collectedRenderer)
        {
            foreach (Entity entity in _rendererFilter)
            {
                ref RendererComponent renderer = ref _rendererStash.Get(entity);
                ref IdComponent id = ref _idStash.Get(entity);
                
                if (id.value == collectorId.value)
                {
                    renderer.value.sharedMaterial = collectedRenderer.value.sharedMaterial;
                }
            }
        }
    }
}