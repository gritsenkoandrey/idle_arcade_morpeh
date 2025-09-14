using System;
using Game.Components;
using Game.Events;
using Game.Tags;
using Infrastructure.Utils;
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
    public sealed class CharacterItemCollisionSystem : IInitializer
    {
        private Stash<ItemTag> _itemStash;
        private Stash<CharacterTag> _characterStash;
        private Stash<CollectedTag> _collectedStash;
        private Stash<BackpackComponent> _backpackStash;
        private Stash<TransformComponent> _transformStash;
        private Event<TriggerEvent> _triggerEvent;
        private IDisposable _subscription;

        public World World { get; set; }

        public void OnAwake()
        {
            _itemStash = World.GetStash<ItemTag>();
            _characterStash = World.GetStash<CharacterTag>();
            _collectedStash = World.GetStash<CollectedTag>();
            _backpackStash = World.GetStash<BackpackComponent>();
            _transformStash = World.GetStash<TransformComponent>();
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
                if (_collectedStash.Has(trigger.target))
                {
                    continue;
                }
                
                if (_characterStash.Has(trigger.source) && _itemStash.Has(trigger.target))
                {
                    ref BackpackComponent backpack = ref _backpackStash.Get(trigger.source);
                    ref TransformComponent transform = ref _transformStash.Get(trigger.target);

                    transform.value.gameObject.layer = Layers.Default;
                    transform.SetParent(backpack.parent);
                    
                    Vector3 to = new (0f, _collectedStash.Length * transform.value.localScale.y, 0f);
                    
                    _collectedStash.Add(trigger.target);

                    _ = LSequence.Create()
                        .Append(LMotion.Create(transform.value.localPosition, to, 0.25f)
                            .WithEase(Ease.InOutBack)
                            .BindToLocalPosition(transform.value))
                        .Join(LMotion.Create(transform.value.eulerAngles, Vector3.zero, 0.25f)
                            .BindToLocalEulerAngles(transform.value))
                        .Run()
                        .AddTo(transform.value, LinkBehavior.CancelOnDestroy);
                }
            }
        }
    }
}